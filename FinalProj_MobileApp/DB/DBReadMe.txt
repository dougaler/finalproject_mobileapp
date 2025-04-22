##In our main code we will need to add these interfaces to use the database:##
    
##Instantiate the database##
    
    private readonly LocalDBService _dbService;
    private int _editCrimeId;
	public WhateverTheHomePageIsNamed(LocalDBService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        Task.Run(async() => Lv.ItemsSource = await _dbService.GetCrimes());
	}

##For a save button. We will need to loop this if we find a list or api that we will use to populate the database.##
##Easiest way may be to simply save whatever list we get as a json, and iterate over it from there.##

    private async void saveButton_Clicked(object sender, EventArgs e)
    {
        if (_editCrimeId == 0)
        {
            await _dbService.Create(new Crime
            {
                CrimeName = nameEntry.Text,
                CrimeEmail = emailEntry.Text,
                CrimeMobileNumb = mobileEntry.Text
            });
        }
        else 
        {
            await _dbService.Update(new Crime
            {
                Id = _editCrimeId,
                CrimeName = nameEntry.Text,
                CrimeEmail = emailEntry.Text,
                CrimeMobileNumb = mobileEntry.Text
            });
            _editCrimeId = 0;
        }
        nameEntry.Text = "";
        emailEntry.Text = "";
        mobileEntry.Text = "";
        Lv.ItemsSource = await _dbService.GetCrimes();
    }

##Code for opening up changes on tapping a list item##

    private async void Lv_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var crime = e.Item as Crime;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
        switch(action)
        {
            case "Edit":
                _editCrimeId = crime.Id;
                nameEntry.Text = crime.CrimeName;
                emailEntry.Text = crime.CrimeEmail;
                mobileEntry.Text = crime.CrimeMobileNumb;
                break;
            case "Delete":
                await _dbService.Delete(crime);
                Lv.ItemsSource = await _dbService.GetCrimes();
                break;
        }
    }
}