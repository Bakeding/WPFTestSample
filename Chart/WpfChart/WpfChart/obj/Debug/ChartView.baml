   M S B A M L   `   `   `  ����  WpfChart    WpfChart.ChartView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^ ZCaliburn.Micro.Platform, Version=2.0.2.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      .��5
      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ tbTitle��5      $��2q�6   $��1��60   $��0��6@    J� RenderTransformOrigin$ 0.5,0.5&�5      $��14(�64   $
��Bold"�6B   $��Stretch=�5      $��Stretch=�62    �� TextAlignment$ Center=�5      $��Title��6+   �� 5   
   -   5        $ tbXLabel��5      $��2q�6   $��1��61   $��2��6A   $ 0.5,0.5&�5      $ Center=�64   $��X Axis��5      �� 5   
   -   5        $��tbYLabel��5      $��2q�6   $��0��6/   $��1��6?   $ 0.5,0.5&�5      $ Center=�64   $��Y Axis��5       � LayoutTransform 5      �� 5        �� Angle$	 -90Q�6"   5"      � 5$   
   -   5         �Name$ 	chartGrid��5$   !   $��0,0,0,0q�6   $��1��65   $��1��6E   }�.  
������   Caliburn.Micro.Message   Attach$M G[Event SizeChanged]=[Action AddChart]; [Event Loaded]=[Action AddChart]��5&      .+�5(      �� -   5        $��
textCanvas��5(       $��2q�6   $��1��62   $��1��6B   }�. ��5)      �� 	 �� ElementName$	 	chartGrid��
 �� Path$
 ActualWidth���6J   �� $	 	chartGrid��$
 ActualHeight�.+�5*      �� -   5        $��chartCanvas��5*      }�. 6,   5+      5,      5-      5/      