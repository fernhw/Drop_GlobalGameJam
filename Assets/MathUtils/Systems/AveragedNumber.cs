


using System.Collections.Generic;
using UnityEngine;

namespace MathUtils.Systems {
    using MathUtils.Consts;

    /// <summary> Debuging numbers with style, get an average of the last X numbers
    /// you input, it also returns a nice preworked percentage.  </summary>

    public class AveragedNumber {
        

        List<double> floatsToAverage = new List<double>();
        double _lastNumber = 0;
        int ammountOfNumbersAveraged = 0;
        bool _isActive = false;


        public AveragedNumber(int ammountRequiredToAverage) {
            floatsToAverage = new List<double>();
            ammountOfNumbersAveraged = ammountRequiredToAverage;
        }


        public bool IsActive() {
            return _isActive;
        }


        public void Add(double numberToAdd) {
            _lastNumber = numberToAdd;
            floatsToAverage.Add(numberToAdd);
            int ammount = floatsToAverage.Count;
            if (ammount > ammountOfNumbersAveraged) {
                floatsToAverage.RemoveAt(0);
                _isActive = true;
            }
        }


        public double GetAverageNumber() {
            int ammount = floatsToAverage.Count;
            if (ammount < ammountOfNumbersAveraged || ammount == 0) {
                return 0;
            }
            int index = 0;
            double addedNumber = 0;
            while (index < ammount) {
                addedNumber += floatsToAverage[index];
                index++;
            }
            return addedNumber / ammount;
        }


        public string GetPercentage(AveragedNumber baseAverageToGetPercentageNumber,
                                  int decimalsMinimum = 2, int minimumIntegers = 2) {
            if (!baseAverageToGetPercentageNumber.IsActive()) {
                return "##/##";
            }
            double number = baseAverageToGetPercentageNumber.GetAverageNumber();
            return GetPercentage(number, decimalsMinimum, minimumIntegers);
        }


        public string GetPercentage(double baseNumberToGetPercentageNumber,
                                    int decimalsMinimum = 2, int minimumIntegers = 2) {
            if (IsActive()) {
                double baseNumber = (GetAverageNumber() / baseNumberToGetPercentageNumber) * 100;
                return StringMathUtils.AddDecimals(baseNumber, decimalsMinimum, minimumIntegers, false) + "%";
            }
            else {
                int ammount = floatsToAverage.Count;
                string stringAmmount = ammount.ToString();
                string addedZeroes = "";
                while (stringAmmount.Length + addedZeroes.Length < ammountOfNumbersAveraged.ToString().Length) {
                    addedZeroes += "0";
                }
                return "[" + addedZeroes + stringAmmount + "/" + ammountOfNumbersAveraged + "]";
            }
        }


        public double GetFPS() {
            if (!IsActive()) {
                return 0d;
            }
            return ((MathConsts.ONE_SIXTIETH_OF_ONE_THOUSAND / GetAverageNumber()) * 60d);
        }


        public string GetFPSString(int decimalsMinimum = 2, int minimumIntegers = 3, bool addSymbol = false, string symbol = "FPS") {
            if (!IsActive()) {
                int ammount = floatsToAverage.Count;
                if (ammount == 0)
                    return "";

                string stringAmmount = ammount.ToString();
                string addedZeroes = "";
                while (stringAmmount.Length + addedZeroes.Length < ammountOfNumbersAveraged.ToString().Length) {
                    addedZeroes += "0";
                }
                return "[" + addedZeroes + stringAmmount + "/" + ammountOfNumbersAveraged + "]";
            }
            double baseNumber = ((MathConsts.ONE_SIXTIETH_OF_ONE_THOUSAND / GetAverageNumber()) * 60d);
            if (addSymbol) {
                return StringMathUtils.AddDecimals(baseNumber, decimalsMinimum, minimumIntegers, false, " ") + symbol;
            }
            else {
                return StringMathUtils.AddDecimals(baseNumber, decimalsMinimum, minimumIntegers, false, " ");
            }
        }


        public string FixedAverage(int decimalsMinimum = 2, int minimumIntegers = 2, string fillerSymbol = " ") {
            if (!IsActive()) {
                float ammount = (float)floatsToAverage.Count;
                return "[" + Mathf.Floor((ammount / ((float)ammountOfNumbersAveraged)) * 9) + "/" + 9 + "]";
            }
            return StringMathUtils.AddDecimals(GetAverageNumber(), decimalsMinimum, minimumIntegers, false, fillerSymbol);
        }


        public string FixedLastNumber(int decimalsMinimum = 2, int minimumIntegers = 2) {
            return StringMathUtils.AddDecimals(_lastNumber, decimalsMinimum, minimumIntegers, false, " ");
        }


		public double LastNumber {
			get {
				return _lastNumber;
			}
		}
    }
}