   M S B A M L   `   `   `  ���� B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35X;clr-namespace:System.Windows.Threading;assembly=WindowsBaseSystem.Windows.Threading     WpfApp2.GameWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp2     Q	threading;clr-namespace:System.Windows.Threading;assembly=WindowsBase       � AllowsTransparency  .  � WindowStyle$
 None=� � Title$ Game of Life�� � WindowState$ 	Maximized=�.���� -    ��Name$ WindowBorder�� �� Padding$ 10q�._��  � ColumnDefinitions �� $��* ��� $��6* �.+��� -    ��Name$ TableCanvas��$��1���� $ 6q�._��� $��0��.+���  �� Orientation$ 
Horizontal=�$��Right=�.+��� -   	 ��Name$	 MinimizeButton��$��3 3 3 3q�$��	ExtraBold"�$�� – ���� -   $	 CloseButton��$��3 3 3 3q�$
��Bold"�$	�� X ���� -   
 ��Name$
 CommandsText��$��0��$��0 0 0 6q�$��Commands��$��DemiBold"�$��16(�$��Center=��� $ 3q�._��  �� $��* ��� $��* �.+��� -   $	 StartButton��$��0 0 3 0q�$��0��$��Start���� -   $	 
StopButton��$��3 0 0 0q�$��1��$
��Stop���� $ 3q�._��  �� $��* ��� $��* �.+��� -   $	 PrevStepButton��$��0 0 3 0q�$��0��$��	Prev step���� -	   $	 NextStepButton��$��3 0 0 0q�$��1��$��	Next step���� $ 3q�._��� -
   $	 ClearButton��$��	  Clear  ��$��Center=��� -   $
 OptionsText��$��0 6 0 0q�$��Game options��$��DemiBold"�$��14(�$��Center=��� $ 3q�._��  �� $��2* ��� $��* �.+��� -   $
 SpeedModifierText��$��0��$
��Left=�$��13(�$��Speed modifier���� -    ��Name$ SpeedComboBox�� �� SelectedIndex$ 2��$��1��.@��� .��0.15x�� .��0.5x�� .��1x�� .��2x�� .��4x�� -   $
 ColoursText��$��0 6 0 0q�$��Colours��$��DemiBold"�$��14(�$��Center=��� $ 3q�._��  �� $��* ��� $��* �.+��� -   $
 	ThemeText��$��0��$��13(�$��Theme���� -   $ ThemeComboBox��$ 0��$��1��.@��� .��
Light Blue�� .��	Light Red�� .��Light Green�� .��Light Contrast�� .��	Light R�� .��	Dark Blue�� .��
Dark Red�� .��
Dark Green�� .��Dark Contrast�� .��Dark R�� .��
User Theme�� $ 3q�._��  �� $��* ��� $��* �.+��� -   $
 AliveColorText��$��0��$��13(�$��
Foreground���� -   $ AliveColorComboBox��$ 0��$��1��{�.  .@��� .��
Light Blue�� .��	Dark Blue�� $ 3q�._��  �� $��* ��� $��* �.+��� -   $
 BackgroundColorText��$��0��$��13(�$��
Background���� -   $ BackgroundColorComboBox��$ 1��$��1��{�.  .@��� .��
Light Blue�� .��	Dark Blue�� $ 3q�._��� -   $	 ChangeUserThemeButton��{�.  $�� Change User Theme ��$��Center=��� -   $
 CompleteSetsText��$��0 6 0 0q�$��Complete sets��$��DemiBold"�$��14(�$��Center=��� $ 3q�._�� .+��� $ 0��$��1��.@��� .��None�� $ 3q�._��  �� $��* ��� $��* �.+��� -    ��Name$ PatternsText��$��0 3 3 0q�$��12(�$��Medium"�$��0��$��Use pattern���� $��3 0 0 0q�$��1��$��Use once���� $ 3q�._��� .+��  �� $��5* ��� $��2* �.+��� $��0 0 3 0q�$��0��.+��� $
��Rle:���� -    ��Name$ RleText��$��0 3 0 6q��� $��3 0 0 0q�$��1��.+��� $��Coords:���� -   $ 
CoordsText��$��0 3 0 6q��� -   $	 AddRleButton��$��	 Add rle ��$��Center=�