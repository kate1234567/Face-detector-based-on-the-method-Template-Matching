using System;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
namespace Face_detector_VD
{
    class Program
    {
        static void Main(string[] args)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>("pattern.jpg");
            Image<Bgr, byte> template = new Image<Bgr, byte>("img.jpg");

            // Поиск совпадений между шаблоном и изображением
            Image<Gray, float> result = image.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

            // Локальные максимумы в результатах совпадения
            double[] minValues, maxValues;
            Point[] minLocations, maxLocations;
            result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

            // Отображение найденных областей лица на исходном изображении
            foreach (Point maxLoc in maxLocations)
            {
                Rectangle rect = new Rectangle(maxLoc, new Size(template.Width, template.Height));
                image.Draw(rect, new Bgr(Color.Red), 1);
            }

            // Отображение результата
            CvInvoke.Resize(image, image, new Size(Convert.ToInt32(image.Width * 4), Convert.ToInt32(image.Height * 4)));
            CvInvoke.Resize(template, template, new Size(Convert.ToInt32(image.Width * 4), Convert.ToInt32(image.Height * 4))); ;
            CvInvoke.Imshow("Template", template);
            CvInvoke.Imshow("Result", image);
            CvInvoke.WaitKey(0);
        }
    }
}
