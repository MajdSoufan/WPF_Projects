   M S B A M L   `   `   `  ����Q  MMemoryMangementProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MemoryMangementProjectMemoryMangementProject  5   	   '    !MemoryMangementProject.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:MemoryMangementProject     5   	     � Title$  
MainWindow��5   	   $��336.842��6   $��428.946��6-    � 
ResizeMode$ NoResize=�6=   .��5	      � .+�5
   
   �� -   5         ��Name$ label��5
      $��Input File Name:��6   $
��Left=�6:   $	��Top=�6U   $��	15,10,0,0q�6m   �� 5   
   -   5        $ label1��5      $��# of Frames:��6    $
��Left=�67   $	��Top=�6R   $��	17,46,0,0q�6j   $��83��6}   �� 5   
   -   5         ��Name$ inputFileTextBox��5      $
��Left=�6,   $��23��6G    �� TextWrapping$
 Wrap=�6S   $�� ��6g   $	��Top=�6p   $	��120��6�   $��
116,13,0,0q�6�   6�   �� 5   
   -   5        $ numberOfFramestextBox��5      $
��Left=�61   $��23��6L   $
 Wrap=�6X   $�� ��6l   $	��Top=�6t   $	��120��6�   $��
116,46,0,0q�6�   6  �� 5   
   -   5         ��Name$ button��5      $	��Run��6!   $
��Left=�6/   $	��Top=�6J   $	��101��6b   $��
261,25,0,0q�6n   $��32��6�   6�   �� 5   
   -   5        $ label2��5      $��Input file content:��6    $
��Left=�6>   $	��Top=�6Y   $��	19,77,0,0q�6q   $��25��6�   $	��103��6�   �� 5   
   -   5         ��Name$ listBox��5      $
��Left=�6#   $	��176��6>   $	��Top=�6K   $	��142��6c   $��
17,107,0,0q�6o   �� 5   
   -   5        $ label21��5      $��	Strategy:��6!   $
��Left=�65   $	��Top=�6P   $��
199,76,0,0q�6h   $��28��6|   �� 5   
   -	   5        $ label20��5      $��# of Page Faults:��6!   $
��Left=�6=   $	��Top=�6X   $��
299,76,0,0q�6p   $��28��6�   �� 5   
   -
   5        $ label3��5      $
��FIFO��6    $
��Left=�6/   $	��Top=�6J   $��211,124,0,0q�6b   $��29��6w   �� 5   
   -   5        $ FIFOfaultPagesCount��5      $��0��6-   $
��Left=�69   $	��Top=�6T   $��336,124,0,0q�6l   $��29��6�   �� 5   
   -   5        $ label5��5      $	��LRU��6    $
��Left=�6.   $	��Top=�6I   $��211,177,0,0q�6a   $��28��6v   �� 5   
   -   5        $ LRUfaultPagesCount��5      $��0��6,   $
��Left=�68   $	��Top=�6S   $��336,177,0,0q�6k   $��28��6�   �� 5   
   -   5        $ label7��5      $	��OPT��6    $
��Left=�6.   $	��Top=�6I   $��211,236,0,0q�6a   $��28��6v   �� 5   
   -   5        $ OPTfaultPagesCount��5      $��0��6,   $
��Left=�68   $	��Top=�6S   $��336,236,0,0q�6k   $��28��6�   �� 5   
   -   5        $ HButton��5      $��H��6"   $
��Left=�6/   $	��Top=�6J   $	��Top=�6b   $��30��6�   $��381,275,0,0q�6�   $��21��6�   6�   5      5      