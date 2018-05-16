using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SpellingCheck;

namespace WpfApp
{
    public class ViewModel : ViewModelBase
    {
        private string _inputString;
        private ObservableCollection<Misspelling> _misspellings;
        private ICommand _SubmitCommand;

        public string InputString
        {
            get
            {
                return _inputString;
            }
            set
            {
                _inputString = value;
                NotifyPropertyChanged("InputString");
            }
        }
        public ObservableCollection<Misspelling> Misspellings
        {
            get
            {
                return _misspellings;
            }
            set
            {
                _misspellings = value;
                NotifyPropertyChanged("Misspellings");
            }
        }
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(param => this.Submit(),
                        null);
                }
                return _SubmitCommand;
            }
        }


        public ViewModel()
        {
            InputString = "";
            Misspellings = new ObservableCollection<Misspelling>();
            Misspellings.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Misspellings_CollectionChanged);


        }

        void Misspellings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Misspellings");
        }
        
        private void Submit()
        {
            var missSpellingList = SpellCheck.DefaultSpellCheck.CheckText(InputString);
            Misspellings = new ObservableCollection<Misspelling>(missSpellingList);
            //foreach(var missSpell in missSpellingList)
            //{
            //    Misspellings.Add(missSpell);
            //}

        }
    }
}
