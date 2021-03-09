using Bas.EuroSing.ScoreBoard.Messages;
using Bas.EuroSing.ScoreBoard.Model;
using Bas.EuroSing.ScoreBoard.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bas.EuroSing.ScoreBoard.ViewModels
{
    internal class VotersViewModel : ViewModelBase
    {
        private IDataService dataService;

        public ObservableCollection<CountryListItemViewModel> Countries { get; set; }

        public RelayCommand<DragEventArgs> VotersDropCommand { get; set; }
        public RelayCommand DeleteAllVotersCommand { get; set; }

        public VotersViewModel(IDataService dataService)
        {
            this.dataService = dataService;

            VotersDropCommand = new RelayCommand<DragEventArgs>(OnDropCommandAsync);
            DeleteAllVotersCommand = new RelayCommand(OnDeleteAllVotesCommandAsync);

            Messenger.Default.Register<RemoveCountryMessage>(this, (message) => {
                var countryViewModel = this.Countries.SingleOrDefault(c => c.Id == message.CountryId);

                if (countryViewModel != null)
                {
                    this.Countries.Remove(countryViewModel);
                }
            });
           
            Countries = new ObservableCollection<CountryListItemViewModel>(from c in dataService.GetAllCountries()
                                                                           orderby c.Name
                                                                           select new CountryListItemViewModel(c, this.dataService));
        }

        private void OnBackCommand()
        {
            Messenger.Default.Send(new CountriesUpdatedMessage());
            Messenger.Default.Send(new BackMessage());
        }

        private async void OnDeleteAllVotesCommandAsync()
        {
            if (MessageBox.Show("Are you sure you want to delete all voters?", "Eurovision 2020", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                await this.dataService.DeleteAllVotersAsync();
            }
        }

        private async void OnDropCommandAsync(DragEventArgs e)
        {
            var filePaths = e.Data.GetData(DataFormats.FileDrop) as IEnumerable<string>;

            foreach (var filePath in filePaths)
            {
                var countryName = Path.GetFileNameWithoutExtension(filePath);
                var imageBytes = File.ReadAllBytes(filePath);

                var newCountry = new Country() { Name = countryName, FlagImage = imageBytes };
                await this.dataService.AddCountryAsync(newCountry);

                InsertCountryOrdered(new CountryListItemViewModel(newCountry, this.dataService));
            }
        }

        private void InsertCountryOrdered(CountryListItemViewModel newCountry)
        {
            bool isInserted = false;
            foreach (var country in Countries)
            {
                if (string.Compare(country.Name, newCountry.Name) == 1)
                {
                    Countries.Insert(Countries.IndexOf(country), newCountry);
                    isInserted = true;
                    break;
                }
            }

            if (!isInserted)
            {
                Countries.Add(newCountry);
            }
        }
    }
}

