﻿using System;
using Xamarin.Forms;
using Todo.Resx;
using System.Globalization;

namespace Todo
{
	public static class App
	{
		static TodoItemDatabase database;

		public static Page GetMainPage ()
		{
			//L10n.SetLocale ();

            if (Device.OS != TargetPlatform.WinPhone)
            {
                var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
                AppResources.Culture = new CultureInfo(netLanguage);
            }

			var mainNav = new NavigationPage (new TodoListPage ());

			return mainNav;
		}

		public static TodoItemDatabase Database {
			get { 
				if (database == null) {
					database = new TodoItemDatabase ();
				}
				return database; 
			}
		}
	}
}

