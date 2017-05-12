﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Content.PM;

namespace Evolve13
{
	[Activity (Label = "Evolve13", 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class Activity1 : Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		// I apologize in advance for this awful hack, I'm sure there's a better way...
		public static Activity1 ShareActivityContext;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ShareActivityContext = this; // HACK: for SpeakButtonRenderer to get an Activity/Context reference

			Xamarin.Forms.Forms.Init (this, bundle);
			//Xamarin.QuickUIMaps.Init (this, bundle);

			var sqliteFilename = "EvolveSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);

			// This is where we copy in the prepopulated database
			Console.WriteLine (path);
			if (!File.Exists(path))
			{
				var s = Resources.OpenRawResource(Evolve13.Resource.Raw.EvolveSQLite);  // RESOURCE NAME ###

				// create a write stream
				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
				// write to the stream
				ReadWriteStream(s, writeStream);
			}


			var conn = new SQLite.SQLiteConnection(path);

			// Set the database connection string
			App.SetDatabaseConnection (conn);

			LoadApplication (new App ());
		}

		void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
	}
}


