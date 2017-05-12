﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Todo
{
	public class BasePage : ContentPage 
	{
	}

	public class TodoItemPage : BasePage
	{
		public TodoItemPage ()
		{
			this.SetBinding (ContentPage.TitleProperty, "Name");

			NavigationPage.SetHasNavigationBar (this, true);
			var nameLabel = new Label { Text = "Name" };
			var nameEntry = new Entry { StyleId = "TodoName", Placeholder="Todo action" };

			nameEntry.SetBinding (Entry.TextProperty, "Name");

			var notesLabel = new Label { Text = "Notes" };
			var notesEntry = new Entry { StyleId = "TodoNotes", Placeholder = "More info" };
			notesEntry.SetBinding (Entry.TextProperty, "Notes");

			var doneLabel = new Label { Text = "Done" };
			var doneEntry = new Switch  { StyleId = "TodoDone" };
			doneEntry.SetBinding (Switch.IsToggledProperty, "Done");


			AccessibilityEffect.SetInAccessibleTree(nameLabel, false);
			AccessibilityEffect.SetAccessibilityHint(nameEntry, "Todo name");

			AccessibilityEffect.SetInAccessibleTree(notesLabel, false);
			AccessibilityEffect.SetAccessibilityHint(notesEntry, "Additional notes");

			AccessibilityEffect.SetInAccessibleTree(doneLabel, false);

			AccessibilityEffect.SetAccessibilityLabel (doneEntry, "whether todo item is done");


			var saveButton = new Button { Text = "Save", 
				BackgroundColor = Color.Green, 
				BorderRadius = 0, 
				TextColor = Color.White,
				StyleId = "TodoSave"
			};
			saveButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				App.Database.SaveItem(todoItem);
				this.Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete", 
				BackgroundColor = Color.Red, 
				BorderRadius = 0, 
				TextColor = Color.White,
				StyleId = "TodoDelete"
			};
			deleteButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				App.Database.DeleteItem(todoItem.ID);
				this.Navigation.PopAsync();
			};
							
			var cancelButton = new Button { Text = "Cancel", 
				BackgroundColor = Color.Gray, 
				BorderRadius = 0, 
				TextColor = Color.White,
				StyleId = "TodoCancel"
			};
			cancelButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				this.Navigation.PopAsync();
			};


			var speakButton = new Button { Text = "Speak", StyleId = "TodoSpeak"};
			speakButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				DependencyService.Get<ITextToSpeech>().Speak(todoItem.Name + " " + todoItem.Notes);
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry, 
					notesLabel, notesEntry,
					doneLabel, doneEntry,
					saveButton, deleteButton, cancelButton,
					speakButton
				}
			};
		}
	}
}