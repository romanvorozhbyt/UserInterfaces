using System;
using System.IO;
using System.Threading.Tasks;

namespace Lab1
{
    using ImageConverter;
     /*
      This program implemented for converting images to different formats resizing possibility.
      Powered by Roman Vorozhbyt KI-4 SA
      */
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to Image Converter2000!");
                var answer = 'n';
                do
                {
                    Console.WriteLine("Type the full path to image that you'd like to convert");
                    string file;
                    do
                    {
                        Console.Write("File: ");
                        file = Console.ReadLine();
                        if(!File.Exists(file))
                            Console.WriteLine("File not exist, check name and try again");
                    } while (!File.Exists(file));

                    Console.WriteLine(
                        "Type one of the following destination extensions: .jpg, .jpeg, .bmp, .gif, .png, .ico, .tif");
                    var extension = Console.ReadLine();
                    int width = -1, height = -1;
                    do
                    {
                        Console.WriteLine("Set image size in format 100x100 in pixels or press :Enter to left as is");
                        var size = Console.ReadLine();
                        if (size == String.Empty)
                        {
                            break;
                        }

                        (width, height) = ParseSize(size);
                    } while (width < 0);

                    Task.Run(() => ImageConverter.ConvertTo(file, extension, width, height)).ConfigureAwait(false);
                    Console.WriteLine("Would like to try again? press 'y' if yes and any key otherwise");
                    answer = Console.ReadKey().KeyChar;
                } while (answer == 'y');

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();

        }

        private static (int, int) ParseSize(string size)
        {
            var split = size.Split('x');
            if (split.Length == 2)
            {
                var isHeightCorrect = ushort.TryParse(split[0], out var width);
                var isWidthCorrect = ushort.TryParse(split[1], out var height);
                if (split.Length == 2 && isHeightCorrect && isWidthCorrect)
                {
                    return (width, height);
                }
            }
            return (-1, -1);

        }
    }
}
