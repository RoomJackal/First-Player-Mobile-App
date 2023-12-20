using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayerMobileApp.MVVM.ViewModel
{
    //Костыль :) Ибо эта сука не любит повторяющийся BindingContext и берёт последний 
    public class RegistratoinAndLoginViewModel: INotifyPropertyChanged
    {
        public RegistrationViewModel _RegistrationViewModel { get; }
        public LoginViewModel _LoginViewModel { get; }
        public RegistrationViewModel RegistrationViewModel
        {
            get => _RegistrationViewModel;
            set
            {
                RegistrationViewModel = value;
                OnPropertyChanged(nameof(RegistrationViewModel));
            }
        }
        public LoginViewModel LoginViewModel
        {
            get => _LoginViewModel;
            set
            {
                LoginViewModel = value;
                OnPropertyChanged(nameof(LoginViewModel));
            }
        }
        public RegistratoinAndLoginViewModel(INavigation Navigation)
        {
            _RegistrationViewModel = new RegistrationViewModel(Navigation);
            _LoginViewModel = new LoginViewModel(Navigation);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
