﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DashSOS.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryContactView
	{
		public EntryContactView(string emergency)
		{
			InitializeComponent ();
		}
	}
}