using App5.Models;
using CognitiveServices.Converters;
using CognitiveServices.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CognitiveServices.Views
{

    public class ComputerVisionPage : ContentPage
    {
        public ComputerVisionPage()
        {

            Title = "Analyse";

            BindingContext = new ComputerVisionViewModel();
            
            List<string> chooseTag = new List<string>();

            var takePhotoButton = new Button
            {
                Text = "Take Photo",
                TextColor = Color.White,
                BackgroundColor = Color.Navy,
                FontSize = 24
            };
            takePhotoButton.SetBinding(Button.CommandProperty, "TakePhotoCommand");

            var pickPhotoButton = new Button
            {
                Text = "Pick Photo",
                TextColor = Color.White,
                BackgroundColor = Color.Olive,
                FontSize = 24
            };
            pickPhotoButton.SetBinding(Button.CommandProperty, "PickPhotoCommand");

            var imageUrlEntry = new Entry();
            imageUrlEntry.SetBinding(Entry.TextProperty, "ImageUrl");

            var image = new Image
            {
                HeightRequest = 200
            };
            image.SetBinding(Image.SourceProperty, "ImageUrl");
            
            var analyseImageUrlButton = new Button
            {
                Text = "Analyse Image Url",
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                FontSize = 24
            };
            analyseImageUrlButton.SetBinding(Button.CommandProperty, "AnalyseImageUrlCommand");

            var analyseImageStreamButton = new Button
            {
                Text = "Analyse Image Stream",
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                FontSize = 24
            };
            analyseImageStreamButton.SetBinding(Button.CommandProperty, "AnalyseImageStreamCommand");

            var extractTextFromImageUrlButton = new Button
            {
                Text = "Extract Text from Image Url",
                TextColor = Color.White,
                BackgroundColor = Color.Silver,
                FontSize = 24
            };
            extractTextFromImageUrlButton.SetBinding(Button.CommandProperty, "ExtractTextFromImageUrlCommand");

      
            var isBusyActivityIndicator = new ActivityIndicator();
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsBusy");
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");

            var errorMessageLabel = new Label
            {
                TextColor = Color.Red,
                FontSize = 20
            };
            errorMessageLabel.SetBinding(Label.TextProperty, "ErrorMessage");

            var captionsLabel = new Label
            {
                TextColor = Color.Maroon,
                FontSize = 20
            };
            captionsLabel.SetBinding(Label.TextProperty, new Binding(
                "ImageResult.Description.Captions[0].Text",
                BindingMode.Default,
                null,
                null,
                "CAPTIONS: {0:F0}"));

            var isAdultContentLabel = new Label
            {
                TextColor = Color.Teal,
                FontSize = 20
            };
            isAdultContentLabel.SetBinding(Label.TextProperty, new Binding(
                "ImageResult.Adult.IsAdultContent",
                BindingMode.Default,
                null,
                null,
                "IsAdultContent: {0:F0}"));

            var isRacyContentLabel = new Label
            {
                TextColor = Color.Teal,
                FontSize = 20
            };
            isRacyContentLabel.SetBinding(Label.TextProperty, new Binding(
                "ImageResult.Adult.IsRacyContent",
                BindingMode.Default,
                null,
                null,
                "IsRacyContent: {0:F0}"));

            var tagsLabel = new Label
            {
                TextColor = Color.Green,
                FontSize = 20
            };
            tagsLabel.SetBinding(Label.TextProperty, new Binding(
                  "ImageResult.Description.Tags",
                  BindingMode.Default,
                  new ListOfStringToOneStringConverter(),
                  null,
                  "TAGS: {0:F0}"));
                  //my code
            BindablePicker picker = new BindablePicker
            {
                Title = "標籤",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            picker.SetBinding(BindablePicker.ItemsSourceProperty,
                  "ImageResult.Description.Tags"
                 );


            var UploadButton = new Button
            {
                Text = "Upload image to server",
                TextColor = Color.White,
                BackgroundColor = Color.Silver,
                FontSize = 24
            };
            UploadButton.SetBinding(Button.CommandProperty, "UploadImageCommand");

            var faceDataTemplate = new DataTemplate(() =>
            {
                var ageLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                ageLabel.SetBinding(Label.TextProperty, new Binding(
                    "Age",
                    BindingMode.Default,
                    null,
                    null,
                    "Age: {0:F0}"));

                var genderLabel = new Label
                {
                    TextColor = Color.Gray,
                    FontSize = 20
                };
                genderLabel.SetBinding(Label.TextProperty, new Binding(
                    "Gender",
                    BindingMode.Default,
                    null,
                    null,
                    "Gender: {0:F0}"));

                var faceStackLayout = new StackLayout
                {
                    Padding = 5,
                    Children =
                    {
                        ageLabel,
                        genderLabel
                    }
                };

                return new ViewCell
                {
                    View = faceStackLayout
                };
            });

            var facesListView = new ListView()
            {
                HasUnevenRows = true,
                ItemTemplate = faceDataTemplate
            };
            facesListView.SetBinding(ListView.ItemsSourceProperty, "ImageResult.Faces");

            var stackLayout = new StackLayout
            {
                Padding = new Thickness(10, 0),
                Children =
                {

                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            takePhotoButton,
                            pickPhotoButton
                           
                        }
                    },
                    imageUrlEntry,
                    //image,
                    analyseImageUrlButton,
                    analyseImageStreamButton,
                     UploadButton,
                    picker,
                    isBusyActivityIndicator,
                    errorMessageLabel,
                    captionsLabel,
                    isAdultContentLabel,
                    isRacyContentLabel,
                    tagsLabel,
                    facesListView
                }
            };
            
            Content = new ScrollView
            {
                Content = stackLayout
            };
        }
    }
}