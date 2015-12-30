using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using NationalInstruments.Vision.WindowsForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vision_Assistant.Utilities;

namespace Vision_Assistant
{
    internal static class Image_Processing
    {
        public static Collection<PointContour> simpleEdges;
        public static double caliperDistance;

        private static Collection<PointContour> IVA_SimpleEdge(VisionImage image,
                                                            Roi roi,
                                                            SimpleEdgeOptions simpleEdgeOptions,
                                                            IVA_Data ivaData,
                                                            int stepIndex)
        {
            // Calculates the profile of the pixels along the edge of each contour in the region of interest.
            using (VisionImage monoImage = new VisionImage(ImageType.U8, 7))
            {
                if (image.Type == ImageType.Rgb32 || image.Type == ImageType.Hsl32)
                {
                    Algorithms.ExtractColorPlanes(image, ColorMode.Hsl, null, null, monoImage);
                }
                else
                {
                    Algorithms.Copy(image, monoImage);
                }

                RoiProfileReport roiProfile = Algorithms.RoiProfile(monoImage, roi);

                // Finds prominent edges along the array of pixel coordinates.
                Collection<PointContour> edges = Algorithms.SimpleEdge(monoImage, roiProfile.Pixels, simpleEdgeOptions);

                // Store the results in the data structure.

                // First, delete all the results of this step (from a previous iteration)
                Functions.IVA_DisposeStepResults(ivaData, stepIndex);

                ivaData.stepResults[stepIndex].results.Add(new IVA_Result("# of Edges", edges.Count));

                for (int i = 0; i < edges.Count; ++i)
                {
                    ivaData.stepResults[stepIndex].results.Add(new IVA_Result(String.Format("Edge {0}.X Position (Pix.)", i + 1), edges[i].X));
                    ivaData.stepResults[stepIndex].results.Add(new IVA_Result(String.Format("Edge {0}.Y Position (Pix.)", i + 1), edges[i].Y));

                    // If the image is calibrated, convert the pixel values to real world coordinates.
                    if ((image.InfoTypes & InfoTypes.Calibration) != 0)
                    {
                        PointContour edgeLocation = new PointContour(edges[i].X, edges[i].Y);
                        CoordinatesReport realWorldPosition = Algorithms.ConvertPixelToRealWorldCoordinates(image, new Collection<PointContour>(new PointContour[] { edgeLocation }));

                        ivaData.stepResults[stepIndex].results.Add(new IVA_Result(String.Format("Edge {0}.X Position (World)", i + 1), realWorldPosition.Points[0].X));
                        ivaData.stepResults[stepIndex].results.Add(new IVA_Result(String.Format("Edge {0}.Y Position (World)", i + 1), realWorldPosition.Points[0].Y));
                    }
                }
                return edges;
            }
        }

        private static Collection<double> IVA_GetDistance(VisionImage image,
                                                        IVA_Data ivaData,
                                                        int stepIndex,
                                                        int stepIndex1,
                                                        int resultIndex1,
                                                        int stepIndex2,
                                                        int resultIndex2)
        {
            Collection<PointContour> points = new Collection<PointContour>();
            points.Add(Functions.IVA_GetPoint(ivaData, stepIndex1, resultIndex1));
            points.Add(Functions.IVA_GetPoint(ivaData, stepIndex2, resultIndex2));

            // Computes the distance between the points.
            Collection<double> distances = Algorithms.FindPointDistances(points);

            // Store the results in the data structure.
            ivaData.stepResults[stepIndex].results.Add(new IVA_Result("Distance (Pix.)", distances[0]));

            // If the image is calibrated, compute the real world distance.
            if ((image.InfoTypes & InfoTypes.Calibration) != 0)
            {
                CoordinatesReport realWorldPosition = Algorithms.ConvertPixelToRealWorldCoordinates(image, points);
                Collection<double> calibratedDistance = Algorithms.FindPointDistances(realWorldPosition.Points);
                ivaData.stepResults[stepIndex].results.Add(new IVA_Result("Distance (Calibrated)", calibratedDistance[0]));
                distances.Add(calibratedDistance[0]);
            }

            return distances;
        }

        public static PaletteType ProcessImage(VisionImage image)
        {
            // Initialize the IVA_Data structure to pass results and coordinate systems.
            IVA_Data ivaData = new IVA_Data(2, 0);

            // Creates a new, empty region of interest.
            Roi roi = new Roi();

            // 建一條巡邊線Star(229,40) End(229,300)
            PointContour vaStartPoint = new PointContour(229, 40);
            PointContour vaEndPoint = new PointContour(229, 300);
            LineContour vaLine = new LineContour(vaStartPoint, vaEndPoint);
            roi.Add(vaLine);

            // 邊緣偵測;採用簡單邊緣偵測方式
            SimpleEdgeOptions vaSimpleEdgeOptions = new SimpleEdgeOptions();
            vaSimpleEdgeOptions.Process = EdgeProcess.All;
            vaSimpleEdgeOptions.Type = LevelType.Absolute;
            vaSimpleEdgeOptions.Threshold = 128;
            vaSimpleEdgeOptions.Hysteresis = 2;
            vaSimpleEdgeOptions.SubPixel = true;
            simpleEdges = IVA_SimpleEdge(image, roi, vaSimpleEdgeOptions, ivaData, 0);
            roi.Dispose();

            // Caliper
            // Delete all the results of this step (from a previous iteration)
            Functions.IVA_DisposeStepResults(ivaData, 1);

            // Computes the vaDistance between two points.
            Collection<double> vaDistance = IVA_GetDistance(image, ivaData, 1, 0, 3, 0, 5);
            caliperDistance = vaDistance[0];

            // Dispose the IVA_Data structure.
            ivaData.Dispose();

            //列出相關AOI資料
            MessageBox.Show("座標點1" + simpleEdges[2].ToString() + "\r\n" +
                            "座標點2" + simpleEdges[0].ToString() + "\r\n" +
                            "座標點3" + simpleEdges[1].ToString() + "\r\n" + "\r\n" +
                            "間距量測" + caliperDistance.ToString());

            //繪出檢測直線(巡邊線)
            //Graphics g = Graphics.FromImage(FileName)
            //g.DrawLine(0, 0, 100, 100);

            // Return the palette type of the final image.
            return PaletteType.Gray;
        }
    }
}