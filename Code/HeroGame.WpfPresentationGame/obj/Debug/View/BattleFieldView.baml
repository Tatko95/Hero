   M S B A M L   `   `   `  ����W  SHeroGame.WpfPresentationGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:HeroGame.WpfPresentationGame.View!HeroGame.WpfPresentationGame.View  5   	   7    1HeroGame.WpfPresentationGame.View.BattleFieldView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @View/clr-namespace:HeroGame.WpfPresentationGame.View     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F     � Title$  BattleFieldView��5   	   $	��500��6!   $	��444��6.    � WindowStartupLocation$ CenterOwner=�6:    � WindowStyle$
 None=�6^    � 	Resources 5         ButtonAnderField �� 
TargetType �� Property �� Value �� Path3  -System.Windows.Media.Effects.DropShadowEffect �� 
TargetType��%9  &	        �� 5   
   " ��6)   .�5	      ��  � y� 6+   �� $ 
IsYouTurnV��� 5
       � ��$ 40��6'   �� 5       � ��$ 75��6&   �� 5       � ��$ 	8,10,8,10q�6'   �� 5       �@J�Effect 5        5      5      �� 5       � �� 6(   �� *AddNewFighterCommand+�� 5       � �� 5      �� 5      " ��6&   .R�5      � .+�5      A� 
���   �
��� ���5      �� 5      #��z��6$   $��Center=�6H   $��Center=�6e   
���   �6�   5      5      5      5      5      ��5       
� 5!   
   	 �� RelativeTransform	 5"      V� 5#      .~�5$      �� 
 �� CenterY$	
 0.5Q�6%    �� CenterX$	 0.5Q�63   �� 5%       �� CenterY$	 0.5Q�6$    �� CenterX$	 0.5Q�62   �� 5&       �� CenterY$	 0.5Q�6&    �� CenterX$	 0.5Q�64   T� 5'      5(      .��5*      �  � Color$ 	#FF00BDBD��6    � Offset$ 0.154Q�6-   � 5+      $ 	#FF28A0A0��6   $ 1Q�6-   � 5,      $ 	#FF37C5C5��6   $ 0.577Q�6-   � 5-      $ 	#FF2BC7C7��6   $ 0.342Q�6-   � 5.      $ 	#FF32B4B4��6   $ 0.803Q�6-   5/      .��51      �  � ColumnDefinitions 52   
   �� 53      $	��25* �6   �� 54      $
��140* �6   �� 55      $	��25* �6    � RowDefinitions 57   
   �� 58      $
��102* �6   �� 59      $
��311* �6   �� 5:      $	��79* �6   .+�5=   
   1   +HeroGame.WpfPresentationGame.View.FieldView  $��1��6@   $��1��6Q   $��	10,5,10,0q�6e   $��1��6x   $��1��6�    � DataContext 6   �� *FieldViewModel+6�   (� 5?   
   $��3��6   $��1��6&   $��2��67   .��5@      �  5A      �� 5B      $	��50* �6'   �� 5C      $	��50* �6'   �� 5D      $	��50* �6'   �� 5E      $	��50* �6'   .+�5G      �� $��Barbarian(4)��6   $��0��60   $��1��6@   #��[  6T   $��	Barbarian��6~   �� 5H      $��	Archer(7)��6   $��1��6-   $��1��6=   #��[  6Q   $��Archer��6{   �� 5I      $��
Wizard(10)��6   $��2��6.   $��1��6>   #��[  6R   $��Wizard��6|   �� 5J      $��End turn��6   $��3��6,   $��1��6<   #��[  6P   ��6z   �� $ EndTurnCommand�5K      5L      (� 5N   
   $��3��6   .��5O      � -   5        $��97��5O      
������  5P      �� 5Q      .+�5S      �� -   5        $��Здатись��5S      
���   �$��14(�6>   $
��Bold"�6L   
���   �$��2q�6r   
������ $��407,3,-83,70q�6�   z�5T      V� 5U      .~�5V      �� �� 5W      �� 5X       �� Angle$ 90Q�6.   T� 5Y      5Z      5\      �� 5]      $��Center=�6E    �� TextAlignment$ Center=�6b   $��142,9,165,47q�6y   
��� ���$��30(�6�   $��41��6�   $	��104��6�   ��6   �� $ NickNameTurnPlayer�6�   �� 5^      $
��TURN��6   $��142,40,165,21q�6(   
��� ���$��30(�6S   $	��104��6a   $ Center=�6m   �� 5_      $��Energy:��6   $��41,40,330,31q�6+   
���   �6A   �� 5`      $��Energy:��6   $��273,40,95,31q�6+   
���   �6A   �� 5a      $��87,39,287,34q�66   $��15(�6L   $
��Bold"�6Z   
���   ���6   �� *
EnergyG1+6l   �� 5b      $��324,38,38,35q�66   $��15(�6L   $
��Bold"�6Z   
���   ���6   �� *
EnergyG2+6l   �� 5c      $��EnergyPlused:��6   $��41,69,296,9q�61   
���   �6F   �� 5d      $��EnergyPlused:��6   $��272,69,65,9q�61   
���   �6F   �� 5e      $��121,66,0,10q�6<   $��15(�6Q   $
��Bold"�6_   
���   �$
��Left=�6�   $��21��6�   ��6   �� *EnergyPlusedG1+6�   �� 5f      $��356,66,37,10q�6<   $��15(�6R   $
��Bold"�6`   
���   ���6   �� *EnergyPlusedG2+6r   �� 5g      $��Nick:��6   $��41,11,341,60q�6)   
���   �6?   �� 5h      $��Nick:��6   $��272,11,106,60q�6)   
���   �6@   �� 5i      $��77,9,269,64q�68   $��15(�6M   $
��Bold"�6[   
���   ���6   �� *
NickNameG1+6m   �� 5j      $��305,9,37,64q�68   $��15(�6M   $
��Bold"�6[   
���   ���6   �� *
NickNameG2+6m   �� 5k      
���   �$��2q�6-   $
��Left=�6A   $��90��6\   $	��Top=�6h   $	��348��6�   $��31,0,0,0q�6�   5l      5m      5n      5o      