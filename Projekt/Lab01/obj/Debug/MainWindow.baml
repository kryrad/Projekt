   M S B A M L   `   `   `  ����@  <Lab01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab01Lab01  5   	       Lab01.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab01     5   	     � Title$  
MainWindow��5   	   $	��550��6   $	��800��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��20q�6   ��6   �� *Items+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $	��200��6:   $��Center=�6F   ��6$   �� *Name+6F   �� 5      $	��100��6;   $	��100��6G   ��6    �� *	Picture+6G   5      5      5      �� 5   
   $��1��6   $��20q�6%   $ Vertical=�61    � DataContext 6H   �� .+�5      �� $��1��6   $	��Top=�6)   $��Stretch=�6A   $ 
Horizontal=�6_   $
��Auto��6x   .+�5      �� -   5         ��Name$ image��5      $��10q�6'   $	��150��63   $	��150��6?   �� 5      -   5        $��Wybierz��5      $��75��6+   $��22��66   $��10q�6B   $��Right=�6N   $��Bottom=�6j   6�   5      �� 5      $
��Wrap=�6   $
��Name��6,   �� 5      -   5        $��nameTextBox��5      $��23��6*    �� TextWrapping$
 Wrap=�66   $��	Jarosław��6J   $��0,0,0,10q�6Z   �� 5       -   5        $��addNewPersonButton��5       $��Add new person��61   6J   � 5!      $��50��6    5"      �� 5#      �� 5$      $��3* �6'    � RowDefinitions 5&      �� 5'      �� 5(      .+�5*      �� -   5        $!��Perform stupid calculations��5*      $��1��6?   $��5q�6O   $��0��6Z   $��2��6g   ��5+      �� 5,       �� Triggers 5-      �� 5.   "    �� Binding 5/   *   �� 	 �� ElementName$	 finalNumberTextBox��
 �� Path$
 (Validation.HasError)� �� Value true50   *   .a�51   &   ��  �� Property � {� �� Value .  6I   52   #   54      56      �� 57      $��Final number��6    $��0��64   $��0��6D   $��0q�6Q   $
��Left=�6\   $��Bottom=�6w   �� 58      -   5         ��Name$ finalNumberTextBox��58      $��0��67   $��1��6G   $	��Top=�6T   .��6l   ��59      �� 5:      $
 Number�6"    �� UpdateSourceTrigger$ PropertyChanged=�60    �� ValidationRules 5;         Lab01.NumberValidation  5<   "   ,  &System.Windows.Controls.ValidationRule   ValidatesOnTargetUpdated . 69   5>      .��5?   $   5@      5A      �� 5B      
���  ����5C      �� $	 finalNumberTextBox��$)
 #(Validation.Errors)[0].ErrorContent�$ PropertyChanged=� �� Mode$ OneWay=� �� NotifyOnValidationError .  �� NotifyOnSourceUpdated .  �� ValidatesOnExceptions . �� 5F      -   5         ��Name$ progressTextBlock��5F      $
��Wrap=�63   $�� ��6G    J� RenderTransformOrigin$ 0.351,5.393&�6O   �� 5G      -   5        $ progressTextBlock2��5G      $
��Wrap=�64   $�� ��6H   � 5H      -	   5         �Name$ progressBar��5H      $��20��6/   �� 5I      -
   5        $��Cancel��5I      6'   5J      5K      5L      