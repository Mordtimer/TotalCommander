﻿using System.ComponentModel;

namespace TC.ViewModel {
    class BaseVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged(params string[] namesOfProperties) {
            if(PropertyChanged != null)
                foreach(var prop in namesOfProperties)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}