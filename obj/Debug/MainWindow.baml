   M S B A M L   `   `   `  ����W  SIzzi_Statistics_Override_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Izzi_Statistics_Override_WPFIzzi_Statistics_Override_WPF  5   	   -    'Izzi_Statistics_Override_WPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:Izzi_Statistics_Override_WPF     5   	   -   5          �Name$  MainWindow1��5   	    � Title$3 -Izzi - Sustitución de Duración de actividad��5   	   $	��400��6=   $	��600��6J    � 
ResizeMode$ CanMinimize=�6V    � Icon$ Izzi.ico��6o   $	��550��6   $	��390��6�   $
��Auto=�6�   $��10q�6�   .��5	      � .+�5
   
   �� -   5         ��Name$ Button_Close��5
      $��Salir��6'   $
��Left=�67   $��504,326,0,0q�6R   $	��Top=�6g   $��75��6   6�   �� 5   
   -   5        $ Button_UploadFile��5      $��	Buscar...��6,   $
��Left=�6@   $��504,285,0,0q�6[   $	��Top=�6p   $��75��6�    �� 	IsDefault . 6�   �� 5   
   -   5         ��Name$ TextBox_FileName��5      $
��Left=�6,   $��20��6G   $��
10,285,0,0q�6S    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��489��6�    � ToolTip$ Ruta del archivo��6�   	 �� 
IsReadOnly	 . {�.  6�   &   System.Windows.Controls.DataGrid  5   
   -   5        
  Name$
 DG_FileContent��5      $
��Left=�6+   $	��Top=�6F   $	��200��6^   $	��569��6k   $��	10,45,0,0q�6w   $ Contenido del archivo...��6�      CanUserReorderColumns .     CanUserSortColumns .     CanUserResizeColumns .  6�   �� 5   
   -   5         ��Name$ Label_Title��5      $B��<Modificación de campo Sustituir para Duración de Actividad��6%   $
��Left=�6j   $��	10,10,0,0q�6�   $	��Top=�6�   $	��497��6�   $��14.667(�6�   $
��Bold"�6�   �� 5   
   -   5        $ Button_Execute��5      $��Ejecutar��6)   $
��Left=�6<   $��239,326,0,0q�6W   $	��Top=�6l   $��75��6�    . $y�Hidden=�6�   �� 5   
   -   5        $ Button_CleanApp��5      $��Limpiar��6*   $
��Left=�6<   $��
10,326,0,0q�6W   $	��Top=�6k   $��75��6�   $y�Hidden=�6�   � 5   
   -	   5         �Name$ 
PB_Loading��5      $��Center=�6*   $��30��6G   $	��Top=�6S   $	��500��6k   $��51,250,43,0q�6w    � IsIndeterminate . $y�	Collapsed=�6�   5      5      