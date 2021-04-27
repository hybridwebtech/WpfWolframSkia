using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra;

namespace WpfWolframSkia
{
    public static class FileUtil
    {
        public static Matrix<double> ReadGreyScaleFile(string imageFilename)
        {
            if (!File.Exists(imageFilename))
            {
                return null;
            }

            Matrix<double> imageData = null;

            try
            {
                using (BinaryReader r = new BinaryReader(File.Open(imageFilename, FileMode.Open)))
                {
                    int maxI = r.ReadInt32();
                    int maxJ = r.ReadInt32();

                    imageData = CreateMatrix.Dense<double>(maxI, maxJ);

                    for (int i = 0; i < maxI; i++)
                    {
                        for (int j = 0; j < maxJ; j++)
                        {
                            imageData[i, j] = (double)r.ReadUInt16();
                        }
                    }
                }

                return imageData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;            
        }
    }
}