   M S B A M L   `   `   `  ����  	Bookstore    Bookstore.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          �Name$  window��5   	    � Title$ 
Ksiegarnia��5   	   $	��500��5   	   $
��1000��5	   	   ���	#FF0B0D40$	��500��5   	   $
��1000��5   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $	��56* �6   �� 5      $	��46* �6   �� 5      $	��56* �6   �� 5      $	��46* �6   �� 5      $	��46* �6   �� 5      $	��46* �6   �� 5      $	��60* �6   �� 5      $	��60* �6   �� 5      $	��46* �6    � ColumnDefinitions 5   
   �� 5       $
��250* �6   �� 5!      $
��200* �6   �� 5"      $
��100* �6   �� 5#      $
��100* �6   .+�5(   
   � $��8��6   $��0��6   $��2��6,   $��	0,0,301,0q�6@    5)      �� 5+      $
��100* �6#   �� 5,      $
��100* �6#   �� 5-      $
��100* �6#   .+�52      �� -   5        $��Button1��55      $��0��52      $��0��53      $��Nowa książka��54         BT#��[  57      �� 59      -   5        $��Button2��5<      $��0��59      $��1��5:      $��Edytuj��5;      #��[  5>      �� 5@      -   5        $��Button3��5C      $��0��5@      $��2��5A      $��Zapisz do bazy��5B      #��[  5E      5F      � 5G   
   $��6��5H      $��1��5I      $��	0,56,0,46q�6    5J      �� 5L      $��* �6#   �� 5M      $��* �6#    5P      �� 5R      $	��50* �6    �� 5S      $	��50* �6    �� 5T      $	��50* �6    �� 5U      $	��50* �6    .+�5Y      �� $��AUTOR��6   $��0��5Z      $��0��5[      $��2��5\      $��Center=�5]      $��Center=�5^      
�������$��0,0,0,10q�5`      �� 5b      $��GATUNEK��6   $��2��5c      $��0��5d      $��2��5e      $��Center=�5f      $��Center=�5g      
�������$��0,0,0,10q�5i      �� 5k      -   5        $��ilosc��5q      $��1��5k      $��0��5l      $��2��5m      ���	#FF1B1D4F${�False��5o      $��	10,0,10,0q�5p       �� TextAlignment$ Center=�5r      
�������5s      �� 5u      -   5        $��	kategoria��5{      $��4��5u      $��0��5v      $��2��5w      ���	#FF1B1D4F${�False��5y      $��	10,0,10,0q�5z      $ Center=�5|      
�������5}      5�      �� 5�   
   $��1��6   $��2��5�      $��2��5�      $��ZDJĘCIE��5�      $��Center=�5�      $	��Top=�5�      
�������$��0,5,0,0q�5�      �� 5�   
   $��5��6   $��1��5�      $��1��5�      $
��OPIS��5�      $��Center=�5�      $	��Top=�5�      $��0,10,0,0q�5�      
�������5�      �� 5�   
   $��5��6   $��2��5�      $��1��5�      $��ILOŚĆ SZTUK��5�      $��Center=�5�      $	��Top=�5�      $��0,10,0,0q�5�      
�������5�      �� 5�   
   $��5��6   $��3��5�      $��1��5�      $��ILE ZMIEJSZ��5�      $��Center=�5�      $	��Top=�5�      $��0,10,0,0q�5�      
�������5�      �� 5�   
   -   5         ��Name$
 opis��5�      $��6��5�      $��2��5�      $��1��5�      $��1��5�      ���	#FF1B1D4F${�False��5�      $
��Wrap=�5�      $��	10,0,10,0q�5�      
�������5�      �� 5�   
   -   5        $��ilosc_sztuk��5�      $��6��5�      $��2��5�      $��1��5�      ���	#FF1B1D4F${�False��5�      $��	10,0,10,5q�5�      $ Center=�5�      
�������5�      �� 5�   
   -	   5        $��ilosc_sztuk_zmniejsz��5�      $��6��5�      $��3��5�      $��1��5�      ���	#FF1B1D4F$
{�True��5�      $��	10,0,10,5q�5�       �� TextAlignment$ Center=�5�      
�������5�      �� 5�   
   -
   5        $��Zmniejsz��5�      $��7��5�      $��2��5�      $��Zmniejsz��5�      $��10, 0, 10, 0q�5�      #��[  5�      $��2��5�   0   �� 5�   
   -   5         ��Name$ 	SearchBox��5�      $��0��5�      $��2��5�      $��0��5�      
������ $��Center=�5�      
����������	#FF1C217A �� 
CaretBrush
 �����  MTB#��[ 5�      �� 5�   
   -   5        	 ��Name$	 obraz��5�      $��2��5�      $��3��5�      $��2��5�      $��3��5�      5�      �� 5�   
   -   5        $��Szukaj��5�      $��0��5�      $��2��5�      $��Szukaj��5�      #��[  5�      $��2��5�   $   �� 5�   
   -   5        
 ��Name$
 lista��5�      $��0��5�      $��1��5�      $��7��5�      
�������$
��Bold"�5�      ���	#FF1B1D4F$��0q�5�      $��1��5�      $��0,0,5,5q�5�      ��5�      �� 5�       �� 
TargetType" ��6    �� Triggers 5�      P� 5�       P� Property �@��
IsSelected P� Value$
 True��68   . �5�      ��  �� Property � �� �� Value
 �  ��6;   �� 5�       � �� �	#FF1C217A6;   5�      P� 5�       �@J�IsMouseOver$
 True��69   . �5�      ��  � �� �	#FF0B0D406;   5�      5�       �� View 5     �� 5      �� ColumnHeaderContextMenu 5     �� 5     .@�5     m� -   5        $��Rosnąco Id��5  '   6;   m� 5     -   5        $��Malejąco Id��5  '   6<   �� 5     m� 5     -   5        $��Rosnąco Nazwa��5  '   6>   m� 5	     -   5        $��Malejąco Nazwa��5	  '   6?   5
     ! �� ColumnHeaderContainerStyle 5     �� 5     " ��6    .�5     ��  � �� �	#FF1B1D4F6;   5     .P�5     �� $��Id��6%    �� Width$	 100��5       HS �� HeaderContainerStyle# [ 5      �� DisplayMemberBinding 5     �� *Id+�� 5     $��Nazwa��6%   $	 200��5     # [ 5      5     �� *Title+5     5     5     5     