   M S B A M L   `   `   `  ����T  PContent Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Content_Management_SystemContent_Management_System  5   	   +    %Content_Management_System.TableWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Content_Management_System     5   	   -   5          � Title$  TableWindow��5   	   $	��400��6   $	��750��6*    � AllowsTransparency . 
������  � SizeToContent$ WidthAndHeight=�6�   $��Courier)�6�    � WindowStyle$
 None=�6�   .��5	      �� $	��750��5
   	   $	��400��5   	   .+�5   
   F� -   5        $��UIPath��5      
�������$��2��5      ��5      �� 5       �� ImageSource$ /Images/black.jpg��6   ��5      B� 5      .��5      E�  E� 
StartPoint$ 50,200&�6!   .��5      ��  �� Point$ 0,100&�6&   �� 5      $ 100,0&�6&   �� 5      $ 650,0&�6&   �� 5      $ 750,100&�6&   �� 5      $ 600,400&�6&   �� 5      $ 150,400&�6&   �� 5      $ 30,160&�6&   �� 5      $ 50,200&�6&   5      5      5!      � 5#   
   $	��400��5$      $	��550��5%       �� Left$	 100��5&       � ColumnDefinitions 5'      �� 5(      $��* �6#   �� 5)      $��* �6#   �� 5*      $��* �6#   �� 5+      $��* �6#   �� 5,      $��* �6#   �� 5-      $��* �6#   �� 5.      $��* �6#   	 � RowDefinitions	 50      �� 51      $��* �6    �� 52      $��* �6    �� 53      $��* �6    �� 54      $��* �6    �� 55      $��* �6    �� 56      $��* �6    �� 57      $��* �6    �� 58      $��* �6    �� 59      $��* �6    �� 5:      $��* �6    �� 5;      $��* �6    .+�5>      �� -   5        
 ��Name$
 
LabelTitle��5?      $��Center=�5@      $��Center=�5A      $��Center=�5B      $��0��5C      $��7��5D      $��0��5E      $��2��5F      
�������$��Models��5H      $��25(�5I      $
��Bold"�5J      &   System.Windows.Controls.DataGrid  5L      -   5          Name$ DataGridModels��5Q      $��0��5M      $��7��5N      $��2��5O      $��6��5P         AutoGenerateColumns .     CanUserAddRows .     CanUserResizeColumns .     CanUserReorderColumns .  ��5V      �� *Specifications+   Columns 5X      4  .System.Windows.Controls.DataGridTemplateColumn  5Y      -   5        CheckBoxSelect��,  &System.Windows.Controls.DataGridColumn   Header$ Select��5[         CellTemplate 5\      �� 5]      .R�5^   "   �� -   5        5^   +   5_      5a      5  /System.Windows.Controls.DataGridHyperlinkColumn  5b      $ Model��5c         
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 5d      �� *Model+   ElementStyle 5f      �� 5g       �� 
TargetType" ��6$   .�5h   "   -   5        5j      5l        5n      $ Picture��6-    5o      �� 5p      .R�5q   "   �� ��6(   �� *	Picture+5r      5t      0  *System.Windows.Controls.DataGridTextColumn  5u      $ Date of post��5v       5w      �� *	DateAdd+ 5x      �� 5y      " ��6$   5z      5|        5~      $
 Fuel��5       5�      �� *Fuel+  5�      $ Number��5�       5�      �� *NumberInRow+5�      �� 5�      -   5         ��Name$ ButtonAddModel��5�      $��1��5�      $��2��5�      $��9��5�      $	��100��5�      $��13(�5�      $��Add new model��5�      $��1q�5�      
������� � Cursor$
 Hand��5�      $��Center=�5�      
�������5�      �� 5�      -	   5        $ ButtonRemoveModel��5�      $��4��5�      $��2��5�      $��9��5�      $	��100��5�      $��Center=�5�      $��13(�5�      $��Remove model��5�      $��1q�5�      
�������
�������$
 Hand��5�      5�      �� 5�      -
   5        $ ButtonExitTable��5�      $��6��5�      $��0��5�      $��13(�5�      $��X��5�      $��20��5�      $��20��5�      $��1q�5�      
����İ�
�������$
 Hand��5�      5�      5�      5�      5�      