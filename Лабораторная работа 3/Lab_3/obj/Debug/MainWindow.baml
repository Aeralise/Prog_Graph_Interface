   M S B A M L   `   `   `  ����@  <Lab_3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab_3Lab_3  5   	       Lab_3.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab_3     5   	     � Title$  
MainWindow��5   	   $��683.014��6   $��1172.329��6-   .��5	      � .+�5
   
   r� -   5         r�Name$ mediaElement��5
      $
��Left=�6-   $	��522��6H   $��	10,58,0,0q�6U   $	��Top=�6h   $
��1144��6�    r� LoadedBehavior$ Manual=�6�   �� 5   
   -   5         ��Name$ 
timeSlider��5      $
��Left=�6%   $��
10,585,0,0q�6@   $	��Top=�6T   $��23��6l   $
��1144��6x   6�   �� 5   
   -   5         ��Name$ playBtn��5      $
��Left=�6"   $��176,612,0,0q�6=   $	��Top=�6R   $��78��6j   $��29��6u   .��5      �� $
��Left=�6   $��23��6/   $	��Top=�6;   $��23��6S   $��play.png��6^   5      �� 5   
   -   5        $ pauseBtn��5      $
��Left=�6#   $��
93,612,0,0q�6>   $	��Top=�6R   $��78��6j   $��29��6u   .��5      �� $��23��6   $��	pause.png��6    $
��Fill=�63   $��23��6B   5      �� 5   
   -   5        $ stopBtn��5      $
��Left=�6"   $��
10,612,0,0q�6=   $	��Top=�6Q   $��78��6i   $��29��6t   .��5      �� $��24��6   $��stop.png��6    $
��Fill=�62   $��24��6A   5      �� 5   
   -   5        $ volumeSlider��5      $
��Left=�6'   $��925,615,0,0q�6B   $	��Top=�6W   $	��229��6o   $��14(�6{    � Maximum$ 1Q�6�    � Value$	 0.5Q�6�   6�   �� 5   
   -   5         ��Name$ currentTimeLbl��5      $��0:00:00��6(   $
��Left=�6:   $��277,613,0,0q�6U   $	��Top=�6j   $��14(�6�   �� 5   
   -   5        $ fileTimeLbl��5      $��0:00:00��6%   $
��Left=�67   $��378,613,0,0q�6R   $	��Top=�6g   $��14(�6   o� 5   
   $
��Left=�6   $��34��6*   $��	10,10,0,0q�66   $	��Top=�6I   $
��1144��6a   .@�5      m� $��20(�6   $
��File��6%   $��89��63   .@�5      m� -	   5         m�Name$ openMI��5      $
��Open��6+   69   5      5      �� 5   
   $��896,612,244,16q�6   $��
volume.png��6(   $
��Fill=�6<   5      5       