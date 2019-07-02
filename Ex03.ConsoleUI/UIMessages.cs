using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public class UIMessages
    {
        private const string k_MainMenuMsg = @"              Welcome to the garage!

Select the operation you wish to do:
1. Add a new vehicle to the garage.
2. View a list of vehicle licenses numbers.
3. Change a vehicle status.
4. Inflate wheels of a vehicle to the maximum.
5. Fuel a gas vehicle.
6. Charge an electric vehicle.
7. Display vehicle data.
8. Exit
";

        private const string k_SeperatorMsg = "--------------------------------------------------";

        private const string k_OperationSuccessfulMsg = "Operation successfully completed!";
        private const string k_PressAnyKeyToContinueMsg = "press any key to continue...";
        private const string k_InvalidInputMsg = "Invalid input, please try again.";
        private const string k_InvalidSelectionMsg = "Invalid selection, please try again.";

        public enum eGeneralMessages
        {
            MainMenu,
            OperationSuccess,
            PressAnyKeyToContinue,
            InvalidInput,
            InvalidSelection,
            Seperator,
        }

        public static void DisplayMessages(eGeneralMessages i_MessageToPrint)
        {
            Console.ForegroundColor = ConsoleColor.White;
            switch (i_MessageToPrint)
            {
                case eGeneralMessages.MainMenu:
                    Console.WriteLine(k_MainMenuMsg);
                    break;
                case eGeneralMessages.OperationSuccess:
                    Console.WriteLine(k_OperationSuccessfulMsg);
                    break;
                case eGeneralMessages.PressAnyKeyToContinue:
                    Console.WriteLine(k_PressAnyKeyToContinueMsg);
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case eGeneralMessages.InvalidInput:
                    Console.WriteLine(k_InvalidInputMsg);
                    break;
                case eGeneralMessages.InvalidSelection:
                    Console.WriteLine(k_InvalidSelectionMsg);
                    break;
                case eGeneralMessages.Seperator:
                    Console.WriteLine(k_SeperatorMsg);
                    break;
                default:
                    break;
            }
        }
    }
}
