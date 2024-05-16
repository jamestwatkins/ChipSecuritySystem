using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public class ChipValidationService
    {
        //I decided I wanted to keep the Program file cleaner by seperating the validation logic into its own class
        public static List<ColorChip> ValidateChips(List<ColorChip> chips)
        {
            List<ColorChip> currentSequence = new List<ColorChip>();

            List<ColorChip> longestSequence = new List<ColorChip>();

            List<int> usedIndexes = new List<int>();

            //I wrote multiple checks to end the validation early if possible
            if (chips == null || !chips.Any())
            {
                return null;
            }
            else if (!CheckForBlueStartAndGreenEnd(chips))
            {
                return null;
            }
            else if (chips.Count == 1 && chips[0].StartColor == Color.Blue && chips[0].EndColor == Color.Green)
            {
                return chips;
            }
            else if (chips.Count == 1)
            {
                return null;
            }


            return GetLongestValidSequence(chips, currentSequence, longestSequence, usedIndexes);
        }
        //If there isn't a chip that starts with blue and one that ends with green, the validation is guarenteed to fail. So I made that one of the checks
        //I believe the time it takes to check each chip for this is worth it, considering how long the full validation takes
        private static bool CheckForBlueStartAndGreenEnd(List<ColorChip> chips)
        {
            bool blueStart = false;
            bool greenEnd = false;

            foreach (ColorChip chip in chips)
            {
                if (chip.StartColor == Color.Blue)
                {
                    blueStart = true;
                }

                if (chip.EndColor == Color.Green)
                {
                    greenEnd = true;
                }

                if (blueStart && greenEnd)
                {
                    return true;
                }
            }

            return false;
        }
        //For validating the chips, I used a permutation algorithm to check all possible ways the chips can be sorted
        //As the algorithm goes on I save the longest valid sequence
        //If I find a valid sequence that is the length of list, I end the validation early
        //If not, I return the longest valid sequence at the end of the validation.
        private static List<ColorChip> GetLongestValidSequence(List<ColorChip> chips, List<ColorChip> currentSequence, List<ColorChip> longestSequence, List<int> usedIndexes)
        {
            if (currentSequence.Count == chips.Count && currentSequence[currentSequence.Count() - 1].EndColor == Color.Green) 
            {
                longestSequence = currentSequence.ToList();

                return longestSequence;
            }
            else if (currentSequence.Count > longestSequence.Count && currentSequence[0].StartColor == Color.Blue && currentSequence[currentSequence.Count - 1].EndColor == Color.Green)
            {
                longestSequence = currentSequence.ToList();
            }

            bool chipAdded = false;

            for (int i = 0; i < chips.Count; i++)
            {
                if (!usedIndexes.Contains(i))
                {
                    if (!currentSequence.Any() && chips[i].StartColor == Color.Blue)
                    {
                        currentSequence.Add(chips[i]);

                        chipAdded = true;
                    }
                    else if (currentSequence.Any() && currentSequence[currentSequence.Count - 1].EndColor == chips[i].StartColor)
                    {
                        currentSequence.Add(chips[i]);

                        chipAdded = true;
                    }

                    if (chipAdded)
                    {
                        usedIndexes.Add(i);

                        longestSequence = GetLongestValidSequence(chips, currentSequence, longestSequence, usedIndexes);

                        if (longestSequence.Count == chips.Count)
                        {
                            break;
                        }

                        if (currentSequence.Any())
                        {
                            currentSequence.RemoveAt(currentSequence.Count - 1);
                        }

                        usedIndexes.Remove(i);
                    }
                }
            }

            return longestSequence;

        }
    }
}
