   M S B A M L   `   `   `  ����G  CUIFramework, Version=20.0.0.0, Culture=neutral, PublicKeyToken=nullX;clr-namespace:UIFramework.PropertyGrid;assembly=UIFrameworkUIFramework.PropertyGrid  5       RM   RM.FloorDialogBox   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      TPropertyGrid;clr-namespace:UIFramework.PropertyGrid;assembly=UIFramework     5      {�.   � Title$  Create rooms finishes��5       � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=�5	   	   #��� ��6.   #��� ��6z   $	��389��6�   $	��516��6�   $	��550��6�   $	��500��6�   $	��900��6  $
��1200��6   � DataContext 5   8   ��  � 	Resources 5
       �� 
TargetType �� Property �� Value��%�   '��       '	� �     �� 5   
   " ��6   .�5      ��  � ��$ 88��6&   �� 5       � ��$ 22��6'   �� 5       � ��$ 	10,10,0,0q�6'   5      �� 5   
   " 	�6   .�5      ��  � ��$ 0,5,0,5q�6'   5      5      .��5      � $��
10,10,10,5q�6   .+�5   
   �� $��	0,0,0,115q�6   $��60��6(   $��Bottom=�67   #��� ��6R   $��60��6�   .+�5      	� -   5         	�Name$ allRoomsRadio��5   4   $��all_rooms_radio��6   	 d� 	IsChecked$
	 TrueY�6K   $��10,5,0,5q�6\   	� 5      -   5        $ selectRoomRadio��5   9   $��selected_rooms_radio��6   $
��Left=�6R   $��10,5,0,5q�6m   5      ]� 5   
   ��. $��0,0,0,36q�69   .+�5      �� -   5        
 ��Name$
 selectFoorLabel��5   F   $	��Top=�6   $��select_floor_label��6)   6F   �� 5      -   5         ��Name$ FloorTypeListBox��5      $��Hidden=�61   #��� ��6e   #��� ��6�   $��	0,0,0,170q�6�   ��5      �� 5      .R�5      �� ��6    �� *Name+5      5!      5"      �� 5#   
   -   5         ��Name$ groupboxParam��5#      $��groupboxParam��6*   #��� ��6A   $��1q�6�   $��55��6�   $��0,0,0,50q�6�   #��� ��6�   $��Bottom=�6  $��73��6+   � IsSharedSizeScope . .��5$      �� $��2��6    �� Orientation$ 
Horizontal=�6&   $��	0,0,-12,0q�6?   $��Bottom=�6R   $��37��6m   $
��Left=�6y   $	��522��6�   .+�5&      ]� ��.  $
��Left=�62   #��� ��6M   $��Center=�6�   $��45��6�   $��0,-8,0,0q�6�   .+�5'      �� -   5         ��Name$ paramSelector��5'      $	��5,0q�66   {�. $	��180��5(   0   $��22��6?   $	��229��6N   $��Center=�6Z   $
��Left=�6u   $��20��6�   $��Center=�6�   5)      �� 5*      $��+��6   $��Bottom=�6$   $
��Left=�6?   $��45��6Z   $��Center=�6f   $��0q�6�   ]� 5+      $��0,-8,0,0q�6   $	��231��6.   $��45��6:   $
��Left=�6F   $��Bottom=�6a   #��� ��6|   .+�5,      �� -   5         ��Name$ Height_TextBox��5,   <    �� TextWrapping$
 Wrap=�6   $��0��63   $	��5,0q�6T   {�. $
��Left=�5-   0   $��Center=�6K    �� 	UndoLimit$ 0��6m   $��Center=�6{   $	��100��6�   $��20��6�   $	��100��6�   $��20��6�   #��� ��6�   #��� ��6  5.      5/      50      �� 53   
   $ 
Horizontal=�6   $��45��6.   $��45��6=   $��Bottom=�6I   $
��Left=�6d   $	��522��6   .+�54      �� -   5         ��Name$ okButton��54   :   $��	Ok_Button��6    �� 	IsDefault . $��25��6d   $��10q�6p   $��25��6|   $��88��6�   $��Center=�6�   #��� ��6�   #��� ��6   4  .System.Windows.Automation.AutomationProperties   	LabeledBy 55      +   %UIFramework.PropertyGrid.PropertyGrid  56      58      �� 59      -	   5        $ cancelButton��59   =   $��Cancel_Button��6    �� IsCancel . $��Center=�6o   $��Center=�6�   $��10q�6�   $��25��6�   $��25��6�   $��88��6�   $��Center=�6�   #��� ��6  #��� ��6Q   5:        5;      5=      5>      �� 5?   
   -
   5        $ groupboxRoomSelect��5?      $��GroupBox��6/   $��83��6A   $��	0,0,0,123q�6M   $��Bottom=�6`   #��� ��6{   5A      5B      