   M S B A M L   `   `   `  ����G  CLab_1_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Lab_1_ClientLab_1_Client  5   	       Lab_1_Client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Lab_1_Client     5   	   -   5          � Title$  Client��5   	   $��435.178��6   $��748.219��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ messageLogBox��5
      $
��Left=�6)   $	��192��6D   $��	10,41,0,0q�6Q   $	��Top=�6d   $	��720��6|   �� 5   
   $��Сообщения:��6   $
��Left=�6%   $��	10,10,0,0q�6@   $	��Top=�6S   �� 5   
   $��Ваше имя:��6   $
��Left=�6$   $��
10,233,0,0q�6?   $	��Top=�6S   �� 5   
   $��Собеседник:��6   $
��Left=�6&   $��228,233,0,0q�6A   $	��Top=�6V   �� 5   
   -   5         ��Name$ sendAllCheck��5      $4��.Отправить сообщение всем��6)   $
��Left=�6L   $��465,240,0,0q�6g   $	��Top=�6|   {�.  6�   �� 5   
   -   5         ��Name$ nameTB��5      $
��Left=�6"   $��20��6=   $��
81,239,0,0q�6I    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $	��142��6�   �� 5   
   -   5        $ 	recieveTB��5      $
��Left=�6%   $��20��6@   $��312,239,0,0q�6L   $
 Wrap=�6a   $	��Top=�6u   $	��142��6�   {�.  6�   �� 5   
   -   5         ��Name$ sendBtn��5      $��Отправить��6"   $
��Left=�66   $��39��6Q   $��555,355,0,0q�6]   $	��Top=�6r   $	��175��6�   {�.  6�   �� 5   
   -   5        $ 
connectBtn��5      $��Подключиться��6%   $
��Left=�6<   $��26��6W   $��
487,10,0,0q�6c   $	��Top=�6w   $	��119��6�   6�   �� 5   
   -   5        $ disconnectBtn��5      $��Отключиться��6(   $
��Left=�6>   $��26��6Y   $��
611,10,0,0q�6e   $	��Top=�6y   $	��119��6�   {�.  6�   �� 5   
   -	   5        $ 	messageTB��5      $
��Left=�6%   $��86��6@   $��
10,264,0,0q�6L   $
 Wrap=�6`   $	��Top=�6t   $	��720��6�   {�.  6�   5      5      