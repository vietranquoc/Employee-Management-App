   M S B A M L   `   `   `  ����A  =WPFApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFAppWPFApp  5   	       WPFApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:WPFApp     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ 
lblManager��5      $��
MANAGEMENT��6$   $��0��69   $��2��6I   $��0��5      $��45(�6   $��Center=�6,   �� 5   
   -   5         ��Name$ btnDepartment��5      $��
Department��6(   $��1��6=   $��0��6J   $��
10 30 0 30q�5      $	��120��6%   
��� � �
�������5      �� 5   
   -   5        $ btnEmployee��5      $��Employee��6&   $��1��69   $��0��6F   $��2��6V   $��	0 30 0 30q�5      $	��120��6$   
��� �|�
�������5       �� 5"   
   -   5        $ btnJob��5"      $	��Job��6!   $��1��6/   $��1��6<   $��
0 30 10 30q�5#      $	��120��6%   
����� �
�������5$      �� 5&   
   -   5        $ 	btnRegion��5&      $��Region��6$   $��2��65   $��0��6B   $��
10 30 0 30q�5'      $	��120��6%   
����ح�
�������5(      �� 5*   
   -   5        $ 
btnCountry��5*      $��Country��6%   $��2��67   $��0��6D   $��2��6T   $��	0 30 0 30q�5+      $	��120��6$   
����d�
�������5,      �� 5.   
   -   5        $ btnLocation��5.      $��Location��6&   $��2��69   $��1��6F   $��
0 30 10 30q�5/      $	��120��6%   
�����_�
�������50      �� 52   
   -   5        $ btnClose��52      $��Close��6#   $��3��63   $��0��6@   $��2��6P   $��	0 30 0 30q�53      $	��120��6$   
��� E��
�������54      55      56      