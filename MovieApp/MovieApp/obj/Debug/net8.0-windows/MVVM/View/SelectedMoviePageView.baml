   M S B A M L   `   `   `  ����  MovieApp.    (MovieApp.MVVM.View.SelectedMoviePageView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5	      �� ���#272537  �� CornerRadius$  12��6"    �� Padding$ 20q�64   ._�5
   
   �  � ColumnDefinitions 5      �� 5      $	��400 �6#   �� 5      $��* �6#   .+�5      �� $  10��6   ��5      �� 5       j� Stretch$ UniformToFill=�6S    �� ImageSource 6!   �� *CurrentMovie.PrimaryImage+6S   5      �� 5      $��1��6   $��20,0,0,0q�6)   .+�5      �� $��28(�5      $
��Bold"�6*   
���������5      �� *CurrentMovie.PrimaryTitle+5   <   �� 5      $��20(�5      $
��Bold"�6   
���������5      �� *CurrentMovie.StartYear+5   1   �� 5       $��20(�5!      $
��Bold"�6   
���������5       �� *CurrentMovie.RunTime+5!   1   �� 5#      ��6   �� *CurrentMovie.Genres+��5$      �� 5%      .R�5&      ��  �� Orientation$ 
Horizontal=�6)   5'      ��5)      �� 5*      .R�5+      �� $��20(�5,      $
��Bold"�5-      
�������$��0,0,10,0q�5/      ��5+   (   �� 5/      50      52      �� 54      $ 
Horizontal=�6   $��0,15,0,0q�66   .+�56      �� $��20(�57       $
��Bold"�6.   
���������56       �� *CurrentMovie.AverageRating+57   @   �� 58      $	��★��6    $��20(�59       
��� ���$��5,0,0,0q�6@   5:      �� 5=      $��0,15,0,0q�6   .+�5>      �� $��0,5,0,0q�6   $ 5q�6.   ���#333142$  5��6O   ._�5@      �� $��14(�5A   (   
�������$
��Wrap=�5B   (   ��5@   (   �� *CurrentMovie.Description+5B   (   5D      5E      �� 5G      $��Back to Movies��6   $��0,30,0,0q�5J      $	��150��6+   $
��Left=�67   ��5H      �� *!DataContext.NavigateBackCommand+ �� RelativeSource ��  �� AncestorType" �5J   7   5K      5L      5M      5N      