using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //I created multiple lists with different scenarios for testing.

            List<ColorChip> multiValidList = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Purple, Color.Green),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Green, Color.Yellow),
                new ColorChip(Color.Red, Color.Green)
            };

            List<ColorChip> noBlueStartList = new List<ColorChip>
            {
                new ColorChip(Color.Green, Color.Orange),
                new ColorChip(Color.Purple, Color.Green),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Purple, Color.Green),
                new ColorChip(Color.Green, Color.Red),
                new ColorChip(Color.Red, Color.Green)
            };

            List<ColorChip> multiBlueList = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Purple, Color.Green),
                new ColorChip (Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Purple, Color.Green),
                new ColorChip(Color.Green, Color.Red),
                new ColorChip(Color.Red, Color.Green)
            };

            List<ColorChip> nonValidList = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Purple, Color.Blue),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Purple, Color.Yellow),
                new ColorChip(Color.Green, Color.Red),
                new ColorChip(Color.Red, Color.Orange)
            };

            List<ColorChip> singleValidList = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green)
            };

            List<ColorChip> singleInvalidList = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Orange)
            };

            List<ColorChip> emptyList = new List<ColorChip>();

            //Call to validation service
            var response = ChipValidationService.ValidateChips(multiValidList);

            if (response != null && response.Any())
            {
                Console.WriteLine("Master Panel unlocked with the following chip(s):");

                foreach (var chip in response)
                {
                    Console.WriteLine(chip.StartColor + " " + chip.EndColor);
                }
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }

            Console.ReadLine();
        }

    }
}
