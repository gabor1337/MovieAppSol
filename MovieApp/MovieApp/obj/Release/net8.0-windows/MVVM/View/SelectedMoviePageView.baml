   M S B A M L   `   `   `  ����   MovieApp.    (MovieApp.MVVM.View.SelectedMoviePageView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .���� ���#272537  �� CornerRadius$  12�� �� Padding$ 20q�._��  � ColumnDefinitions �� $	��400 ��� $��* �.+��� $  10������  j� Stretch$ UniformToFill=� �� ImageSource �� *CurrentMovie.PrimaryImage+�� $��1��$��20,0,0,0q�.+��� $��28(�$
��Bold"�
����������� *CurrentMovie.PrimaryTitle+�� $��20(�$
��Bold"�
����������� *CurrentMovie.StartYear+�� $��20(�$
��Bold"�
����������� *CurrentMovie.RunTime+�� ���� *CurrentMovie.Genres+���� .R���  �� Orientation$ 
Horizontal=����� .R��� $��20(�$
��Bold"�
�������$��0,0,10,0q����� �� $ 
Horizontal=�$��0,15,0,0q�.+��� $��20(�$
��Bold"�
����������� *CurrentMovie.AverageRating+�� $	��★��$��20(�
��� ���$��5,0,0,0q��� $��0,15,0,0q�.+��� $��0,5,0,0q�$ 5q����#333142$  5��._��� $��14(�
�������$
��Wrap=����� *CurrentMovie.Description+�� $��Back to Movies��$��0,30,0,0q�$	��150��$
��Left=����� *!DataContext.NavigateBackCommand+ �� RelativeSource ��  �� AncestorType" �