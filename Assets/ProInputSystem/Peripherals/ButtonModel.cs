
using System.Collections.Generic;

namespace ProInputSystem {
    /**
   * Button Model is Data used to CREATE a button, this data is kept simple to write.
   */
    public class ButtonModel {
        public const string BUTTON = "bt";
        public const string ANALOGUE = "an";
        public const string ANALOGUE_REVERSED = "ar";

        AccessType _inputAccessType = AccessType.NONE;
        int _id = 0;
        ButtonType _typeOfButton = ButtonType.BUTTON;
        //in case we need a multibutton, this button will be empty but if you use createMultibutton you will retrieve all
        List<ButtonModel> ifWeUseItForMultiButtons;
        bool isMultibuttonModel = false;

        public ButtonModel(AccessType inputType = AccessType.NONE, int index = 0, ButtonType typeOfSwitch = ButtonType.BUTTON) {

            _inputAccessType = inputType;
            _id = index;
            _typeOfButton = typeOfSwitch;
        }
        public ButtonModel(List<ButtonModel> multiButtons) {

            ifWeUseItForMultiButtons = multiButtons;
            isMultibuttonModel = true;
        }

        public ButtonModel(int inputType, int id, string typeOfSwitch) {

            switch (inputType) {
                case 0:
                    _inputAccessType = AccessType.PC;
                    break;
                case 1:
                    _inputAccessType = AccessType.CONTROLLER;
                    break;
                case 3:
                    _inputAccessType = AccessType.NONE;
                    break;
            }
            _id = id;
            switch (typeOfSwitch) {
                case "bt":
                    _typeOfButton = ButtonType.BUTTON;
                    break;
                case "an":
                    _typeOfButton = ButtonType.ANALOGUE;
                    break;
                case "ar":
                    _typeOfButton = ButtonType.ANALOGUE_REVERSED;
                    break;
            }
        }

        public IButton CreateButton() {

            if (isMultibuttonModel) {
                List<IButton> buttonsForMultiButton = new List<IButton>();
                foreach (ButtonModel buttonModel in ifWeUseItForMultiButtons) {
                    buttonsForMultiButton.Add(buttonModel.CreateButton());
                }
                return new MultiButton(buttonsForMultiButton);
            }
            else {
                return new Button(Id, _typeOfButton, InputAccessType);
            }

        }

        /**
         * Use only to create multibuttons if the constructor IS
        */

        AccessType InputAccessType {
            get {
                return _inputAccessType;
            }
            set {
                _inputAccessType = value;
            }
        }
        int Id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }
        ButtonType TypeOfButon {
            get {
                return _typeOfButton;
            }
            set {
                _typeOfButton = value;
            }
        }

    }
}
