Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.Data
Imports System.Windows.Threading
Imports System.ComponentModel

Namespace RealTimeSourceExample
	Partial Public Class MainWindow
		Inherits Window
		Private Persons As ObservableCollection(Of Data)
		Private Count As Integer = 50
		Private Random As New Random()
		Public Sub New()
			InitializeComponent()
			Persons = New ObservableCollection(Of Data)()
			For i As Integer = 0 To Count - 1
				Persons.Add(New Data With {.Id = i, .Text = "Text" & i, .Progress = GetNumber()})
			Next i

			grid.ItemsSource = New RealTimeSource() With {.DataSource = Persons}

			Dim timer As New DispatcherTimer()
			timer.Interval = TimeSpan.FromMilliseconds(1)
			AddHandler timer.Tick, AddressOf Tick
			timer.Start()
		End Sub

		Private Sub Tick(ByVal sender As Object, ByVal e As EventArgs)
			Dim index As Integer = Random.Next(0, Count)
			Persons(index).Id = GetNumber()
			Persons(index).Text = "Text" & GetNumber()
			Persons(index).Progress = GetNumber()
		End Sub
		Private Function GetNumber() As Integer
			Return Random.Next(0, Count)
		End Function
	End Class
	Public Class Data
		Implements INotifyPropertyChanged
		Private _Id As Integer
		Public _Text As String
		Public _Progress As Double

		Public Property Id() As Integer
			Get
				Return _Id
			End Get
			Set(ByVal value As Integer)
				_Id = value
				NotifyPropertyChanged("Id")
			End Set
		End Property
		Public Property Text() As String
			Get
				Return _Text
			End Get
			Set(ByVal value As String)
				_Text = value
				NotifyPropertyChanged("Text")
			End Set
		End Property
		Public Property Progress() As Double
			Get
				Return _Progress
			End Get
			Set(ByVal value As Double)
				_Progress = value
				NotifyPropertyChanged("Progress")
			End Set
		End Property

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
		Private Sub NotifyPropertyChanged(ByVal name As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
		End Sub
	End Class
End Namespace