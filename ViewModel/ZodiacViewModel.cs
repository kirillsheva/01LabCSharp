using System;
using System.Threading.Tasks;
using System.Windows;
using _01LabShevchenko.Models;
using _01LabShevchenko.ViewModel;

namespace _01LabShevchenko
{
    internal class ZodiacViewModel : BaseViewModel
    {
        #region Fields
        private readonly User _user = new User();

        #region Commands

        private RelayCommand<object> _submitCommand;

        #endregion

        #endregion

        #region Properties

        public string UserAge
        {
            get => _user.UserAge;
            set
            {
                _user.UserAge = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get => _user.Date;
            set
            {
                _user.Date = value;
                OnPropertyChanged();
            }
        }

        public string WesternZodiac
        {
            get => _user.WesternZodiac;
            set
            {
                _user.WesternZodiac = value;
                OnPropertyChanged();
            }
        }

        public string ChineseZodiac
        {
            get => _user.ChineseZodiac;
            set
            {
                _user.ChineseZodiac = value;
                OnPropertyChanged();
            }
        }
       

        public RelayCommand<object> SubmitCommand
        {
            get
            {
                return _submitCommand ??= new RelayCommand<object>(
                    ProcessingInf, o => CanExecuteCommand());
            }
        }

        #endregion

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(Date);
        }

        private async void ProcessingInf(object obj)
        {
            bool success = false;
            WesternZodiac = "";
            ChineseZodiac = "";
            //await Task.Run(() => Thread.Sleep(2000));
            await Task.Run(() =>
                {
                    try
                    {
                        CalculateAge();
                        success = true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    if (!success) return;
                    WesternZodiac = "Your western zodiac is : " + CalcWesternZodiac();
                    ChineseZodiac = "Your chinese zodiac is : " + CalcChineseZodiac();
                });
            
         
        }

        private void CalculateAge()
        {
            var date = Convert.ToDateTime(Date);
            var currentDate = DateTime.Today;
            var fullYears = currentDate.Year - date.Year;
            if (date.Month > currentDate.Month)
            {
                UserAge="Your age is: "+ --fullYears;
            }
            else if (date.Month == currentDate.Month)
            {
                if (date.Day > currentDate.Day)
                {
                    UserAge = "Your age is: " + --fullYears;
                }
                //if (date.Day < currentDate.Day || date.Day == currentDate.Day)
                //{
                //    UserAge = "Your age is: " + fullYears;
                //}
            }
            if (date.Month == currentDate.Month && date.Day == currentDate.Day)
            { 
                MessageBox.Show("Mister, You are older today, than yesterday! Congrats,you are " + fullYears + " years old!!!");
            }

            if (fullYears < 0)
            {
             throw new Exception("Wait a bit, were you even born? Try again.");
            }
            else if (fullYears > 135)
            { 
             throw new Exception("You have picked the wrong date... Try again. ");
            }
            UserAge = "Your age is: " + fullYears;
        }

        private string CalcWesternZodiac()
        {
            var date = Convert.ToDateTime(Date);

            switch (date.Month)
            {
                case 1 when date.Day >= 21:
                case 2 when date.Day <= 20:
                    return "Aquarius";
                case 2 when date.Day >= 21:
                case 3 when date.Day <= 20:
                    return "Pisces";
                case 3 when date.Day >= 21:
                case 4 when date.Day <= 20:
                    return "Aries";
                case 4 when date.Day >= 21:
                case 5 when date.Day <= 20:
                    return "Taurus";
                case 5 when date.Day >= 21:
                case 6 when date.Day <= 21:
                    return "Gemini";
                case 6 when date.Day >= 22:
                case 7 when date.Day <= 22:
                    return "Cancer";
                case 7 when date.Day >= 23:
                case 8 when date.Day <= 23:
                    return "Leo";
                case 8 when date.Day >= 24:
                case 9 when date.Day <= 23:
                    return "Virgo";
                case 9 when date.Day >= 24:
                case 10 when date.Day <= 22:
                    return "Libra";
                case 10 when date.Day >= 23:
                case 11 when date.Day <= 22:
                    return "Scorpio";
                case 11 when date.Day >= 23:
                case 12 when date.Day <= 21:
                    return "Sagittarius";
                case 12 when date.Day >= 22:
                case 1 when date.Day <= 20:
                    return "Capricorn";
                default:
                    return "";
            }
        }

        private string CalcChineseZodiac() {
            var date = Convert.ToDateTime(Date).Year % 12+1;
            switch (date)
            {
                case 1:
                    return "Monkey";
                case 2:
                    return "Rooster";
                case 3:
                    return "Dog";
                case 4:
                    return "Pig";
                case 5:
                    return "Rat";
                case 6:
                    return "Ox";
                case 7:
                    return "Tiger";
                case 8:
                    return "Rabbit";
                case 9:
                    return "Dragon";
                case 10:
                    return "Snake";
                case 11:
                    return "Horse";
                case 12:
                    return "Goat";
                default:
                    return "Secret animal 0_0";
            }
        }
    }
}
