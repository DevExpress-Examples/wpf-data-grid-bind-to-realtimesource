using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Data;
using System.Windows.Threading;
using System.ComponentModel;

namespace RealTimeSourceExample {
	public partial class MainWindow: Window {
		ObservableCollection<Data> Persons;
		int Count = 50;
		Random Random = new Random();
		public MainWindow() {
			InitializeComponent();
			Persons = new ObservableCollection<Data>();
			for (int i = 0; i < Count; i++)
				Persons.Add(new Data {
					Id = i,
					Text = "Text" + i,
					Progress = GetNumber()
				});

			grid.ItemsSource = new RealTimeSource() {
				DataSource = Persons
			};

			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(1);
			timer.Tick += Tick;
			timer.Start();
		}

		private void Tick(object sender, EventArgs e) {
			int index = Random.Next(0, Count);
			Persons[index].Id = GetNumber();
			Persons[index].Text = "Text" + GetNumber();
			Persons[index].Progress = GetNumber();
		}
		int GetNumber() {
			return Random.Next(0, Count);
		}
	}
	public class Data: INotifyPropertyChanged {
		private int _Id;
		public string _Text;
		public double _Progress;

		public int Id {
			get {
				return _Id;
			}
			set {
				_Id = value;
				NotifyPropertyChanged("Id");
			}
		}
		public string Text {
			get {
				return _Text;
			}
			set {
				_Text = value; 
				NotifyPropertyChanged("Text");
			}
		}
		public double Progress {
			get {
				return _Progress;
			}
			set {
				_Progress = value; 
				NotifyPropertyChanged("Progress");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		void NotifyPropertyChanged(string name) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}
}