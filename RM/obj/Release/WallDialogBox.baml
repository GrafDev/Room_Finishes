   M S B A M L   `   `   `  ����   RM    RM.WallDialogBox   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     {�.   � Title$  Create rooms finishes�� � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=�$	��500��$	��500��#��� ��$	��500��$��802.3��$	��900��$
��1200�� � DataContext ��  � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       '	� U     �� " ��.���  � ��$ 88����  � ��$ 22����  � ��$ 10,0,0,0q��� " 	�.���  � ��$ 0,5,0,5q�.��� $��10,10,10,10q�.+��� $��0��$��	0,0,0,163q�$��50��$��Bottom=�$��60�� J� OpacityMask# � ��#��� ��.+�	� -   $��all_rooms_radio��$��all_rooms_radio��	 d� 	IsChecked$
	 TrueY�$��10,5,0,5q�$	��Top=�	� -   $��selected_rooms_radio��$��selected_rooms_radio��$��10,5,0,5q�$��Bottom=�#��� ��]� $��	0,0,0,251q�.+��� -   $��select_wall_label��$	��Top=�$��select_wall_label��$	��Top=�$
��Left=�$��Stretch=�$	��291���� -   $��WallTypeListBox��$��Hidden=�$��0,0,0,10q�#��� ��#��� ������ .R��� ���� *Name+�� 
 �� Orientation$
 
Horizontal=�$
��Left=�$	��472��$��Bottom=�$��40��.+��� -   $��	Ok_Button��$��	Ok_Button�� �� 	IsDefault . $��25��$��25��$��88��#��� ��#��� ���� -   $��Cancel_Button��$��Cancel_Button�� �� IsCancel . $��25��$��25��#��� ��#��� ���� -    ��Name$ groupboxParam��$��groupboxParam��
���   �$��1q� � IsSharedSizeScope . $��80��$��0,0,0,66q�#��� ��$��Bottom=�$��92��# � ��.���� $��2��$
 
Horizontal=�$��	0,2,-2,-3q�$��Center=�$��70��$
��Left=�$	��462��$��60��.+��� $��0,0,0,5q�$��58��$��Bottom=�$��60��# � ��#��� ��$
��Left=�$	��309��.+�	� -    	�Name$ from_level_radio��$��from_level_radio��$
	 TrueY�$��10,5,0,5q�$	��Top=�	� -	   $ to_height_radio��$��to_height_radio��$��10,5,0,5q�$��Bottom=��� $��+��$��Center=�$
��Left=�$��37��$��Center=�]� $	��0,5q�$	��125��$��60��$
��Left=�$��Center=�#��� ��.+��� -
    ��Name$ HeightTextBox�� �� TextWrapping$
 Wrap=�$��0��$��5,8,5,9q�{�. $
��Left=�$��Center=� �� 	UndoLimit$ 0��$��Center=�$	��100��$��20��$	��100��$��20��#��� ��#��� ��" �� VerticalScrollBarVisibility$ Disabled=��� -   $ groupBoxRoom��$��groupBoxRoom��$��92��$��Bottom=�#��� ��$��Bottom=�$��	0,0,0,163q�