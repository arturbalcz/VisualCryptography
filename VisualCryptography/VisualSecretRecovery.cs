using PGM_Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCryptography
{
    class VisualSecretRecovery
    {
        private static int ResolvePixel(int[] first_pixels, int[] second_pixels)
        {
            for (int i = 0; i < 4; i++)
            {
                if (first_pixels[i] * second_pixels[i] > 0) return 1;
            }

            return 0; 
        }

        public static PGM_File RecoverSecretFromTwoShares(PGM_File first_share, PGM_File second_share)
        {
            int[] result_data = new int[(first_share.width/2) * (first_share.height/2)];

            int current_y = 0;
            int current_x = 0;


            for (int i = 0; i < result_data.Length; i++)
            {
                int[] first_pixels = new int[4];
                int[] second_pixels = new int[4];

                first_pixels[0] = first_share.data_matrix[current_y, current_x];
                first_pixels[1] = first_share.data_matrix[current_y, current_x + 1];
                first_pixels[2] = first_share.data_matrix[current_y + 1, current_x];
                first_pixels[3] = first_share.data_matrix[current_y + 1, current_x + 1];

                second_pixels[0] = second_share.data_matrix[current_y, current_x];
                second_pixels[1] = second_share.data_matrix[current_y, current_x + 1];
                second_pixels[2] = second_share.data_matrix[current_y + 1, current_x];
                second_pixels[3] = second_share.data_matrix[current_y + 1, current_x + 1];

                current_y += 2;
                if (current_y >= first_share.width)
                {
                    current_y = 0;
                    current_x += 2;
                }

                result_data[i] = ResolvePixel(first_pixels, second_pixels); 
            }

            return new PGM_File(first_share.format, first_share.width / 2, first_share.height / 2, 1, result_data); 
        }
    }
}
