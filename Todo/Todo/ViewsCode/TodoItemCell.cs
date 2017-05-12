﻿using System;
using Xamarin.Forms;

namespace Todo
{
	public class TodoItemCell : ViewCell
	{
		public TodoItemCell ()
		{
			StyleId = "Cell";

			var label = new Label {
				StyleId = "CellLabel",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};
			label.SetBinding (Label.TextProperty, "Name");

			var tick = new Image {
				StyleId = "CellTick",
				Source = FileImageSource.FromFile ("check"),
				HorizontalOptions = LayoutOptions.End
			};
			tick.SetBinding (Image.IsVisibleProperty, "Done");

			var layout = new StackLayout {
				Padding = new Thickness(20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {label, tick}
			};
			View = layout;
		}
	}
}

