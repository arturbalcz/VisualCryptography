using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PGM_Reader;

namespace VisualCryptography
{
    class VisualSecretSplit
    {
        private static Random random = new Random(); 

        private static int[] GenerateTwoPixels()
        {
            int[] result = new int[2]; 

            result[0] = random.Next(2);
            if (result[0] == 0) result[1] = 1;
            else result[1] = 0;

            return result; 
        }

        private static int[] GenerateFourPixels()
        {
            int[] result = new int[4];

            int[] first_row = GenerateTwoPixels();
            int[] second_row = GenerateTwoPixels();

            result[0] = first_row[0];
            result[1] = first_row[1];
            result[2] = second_row[0];
            result[3] = second_row[1];

            return result;
        }

        private static int[] DivideWhiteIntoTwoPixels()
        {
            int[] result = new int[4];
            int[] pixels = GenerateTwoPixels();

            result[0] = pixels[0];
            result[1] = pixels[1];
            result[2] = pixels[0];
            result[3] = pixels[1];

            return result;
        }

        private static int[] DivideBlackIntoTwoPixels()
        {
            int[] result = new int[4]; 
            int[] pixels = GenerateTwoPixels();

            result[0] = pixels[0];
            result[1] = pixels[1];
            result[2] = pixels[1];
            result[3] = pixels[0];

            return result; 
        }

        private static int[] DivideWhiteIntoFourPixels()
        {
            int[] result = new int[8];
            int[] pixels = GenerateFourPixels();

            result[0] = pixels[0];
            result[1] = pixels[1];
            result[2] = pixels[2];
            result[3] = pixels[3];

            result[4] = pixels[0];
            result[5] = pixels[1];
            result[6] = pixels[2];
            result[7] = pixels[3];

            return result;
        }

        private static int[] DivideBlackIntoFourPixels()
        {
            int[] result = new int[8];
            int[] pixels = GenerateFourPixels();

            result[0] = pixels[0];
            result[1] = pixels[1];
            result[2] = pixels[2];
            result[3] = pixels[3];

            result[4] = pixels[1];
            result[5] = pixels[0];
            result[6] = pixels[3];
            result[7] = pixels[2];

            return result;
        }

        private static int[] DivideIntoTwoPixels(int pixel)
        {
            int[] result;

            if (pixel > 0) result = DivideBlackIntoTwoPixels();
            else result = DivideWhiteIntoTwoPixels();

            return result; 
        }

        private static int[] DivideIntoFourPixels(int pixel)
        {
            int[] result;

            if (pixel > 0) result = DivideWhiteIntoFourPixels();
            else result = DivideBlackIntoFourPixels();

            return result;
        }


        public static PGM_File[] DivideIntoTwoShares(PGM_File file)
        {
            PGM_File[] shares = new PGM_File[2];

            int[,] first_share_data = new int[file.width * 2, file.height * 2];
            int[,] second_share_data = new int[file.width * 2, file.height * 2];

            int current_y = 0;
            int current_x = 0;

            foreach (int pixel in file.data_matrix)
            {
                int[] divided = DivideIntoFourPixels(pixel);

                first_share_data[current_y, current_x] = divided[0];
                first_share_data[current_y, current_x + 1] = divided[1];
                first_share_data[current_y + 1, current_x] = divided[2];
                first_share_data[current_y + 1, current_x + 1] = divided[3];

                second_share_data[current_y, current_x] = divided[4];
                second_share_data[current_y, current_x + 1] = divided[5];
                second_share_data[current_y + 1, current_x] = divided[6];
                second_share_data[current_y + 1, current_x + 1] = divided[7];

                current_y+=2;
                if(current_y >= file.width * 2)
                {
                    current_y = 0;
                    current_x += 2;
                }

            }
            
            shares[0] = new PGM_File(file.format, file.width * 2, file.height *2, file.max_value, first_share_data);
            shares[1] = new PGM_File(file.format, file.width * 2, file.height *2, file.max_value, second_share_data);


            return shares; 
        }
    }
}
