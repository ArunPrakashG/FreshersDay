   M S B A M L   `   `   `  ����F  BFreshersDay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:FreshersDayFreshersDay  5      H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5          FreshersDay.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      +localclr-namespace:FreshersDay     5      6mah,http://metro.mahapps.com/winfx/xaml/controls  5      JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5      -   5           AccentColorBrush#���   5	      (  "MahApps.Metro.Controls.MetroWindow    	GlowBrush
  �   � � Title$5 /BCA Freshers day program | Made by arun prakash��5      $	��741��5      $	��868��5       � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5      .��5      � -   5        $
��grid��5      .+�5      �� $��tasks remaining��6
   $��Stretch=�5      $��Center=�5      $��773,22,13,0q�5      $	��Top=�5      $��Nirmala UI Semilight)�5      $��10(�5      �� 5      -   5         ��Name$ remainingLabel��5   
   $��48/48��5      $��Stretch=�5      $��Center=�5      $��
779,4,10,0q�5      $	��Top=�5       $��20(�5!       J� RenderTransformOrigin$ 0.521,0.467&�5"      $��Lithos Pro Regular)�5#       J� Effect 5$      3  -System.Windows.Media.Effects.DropShadowEffect  5%         
BlurRadius$ 29Q�6      Opacity$ 0.895Q�5&      5(      �� 5)      -   5        $ lastTaskLabel��5)   
   $��Previous task��5*      $��Stretch=�5+      $��Center=�5,      $��16,667,14,0q�5-      $	��Top=�5.      $��Yu Gothic UI Semibold)�5/      $��18(�50      $��36��51      
���   �52      �� 53      -   5        $��windowTopTitle��53   
   $��🖥compriderzzz🌍��54      $��Center=�55      $��Center=�56      $��117,14,97,0q�57      $	��Top=�58      $��Kozuka Mincho Pr6N B)�59      $��36(�5:      $��67��5;      $	��648��5<      ���	#FF2540F1��5=      Z� 5>      �� 5?      $��Previous task:��6
   $
��Left=�5@      $��	11,0,0,39q�5A      $��Bottom=�5B      $��10(�5C      �� 5D      
�������$
��Left=�5E      $��17��5F      $��	16,13,0,0q�5G      
���   �$	��Top=�5I      $��25��5J      �� 5K      -   5        $��	for girls��5K   
   $
��Left=�5L      $��47,8,0,0q�5M      $	��Top=�5N      $��27��5O      5P      �� 5Q      
����� �$
��Left=�5R      $��17��5S      $��	16,38,0,0q�5T      
���   �$	��Top=�5V      $��25��5W      �� 5X      -   5        $��for boys��5X   
   $
��Left=�5Y      $��	47,33,0,0q�5Z      $	��Top=�5[      5\      � 5]      -   5        $��taskGrid��5]   	   $	��833��5^      $	��381��5_      $��	16,96,0,0q�5`      ���	#FFE4E4E4$	��Top=�5b      $
��Left=�5c      	 � ColumnDefinitions	 5d      �� 5e      $	��69* �6   �� 5f      $
��764* �6   .+�5h      �� $��TASKS��6   $
��Left=�5i      $��29,40,0,-29q�5j      $	��Top=�5k      $��Myriad Hebrew)�5l      $	��300(�5m      
 J� Opacity$
 0.125Q�5n      $��2��5o       5p      -  'System.Windows.Media.Effects.BlurEffect  5q         Radius$ 25Q�6   5s      �� 5t      -	   5         ��Name$ task1��5t      $��1��5u      $��	10,14,0,0q�5v      $��1q�5w        SquareButtonStyle#���  5x      +  %MahApps.Metro.Controls.ControlsHelper   ContentCharacterCasing$ Upper=�5y      $��44��5z      $
��Left=�5{      $��85��5|      $	��Top=�5}      $��2��5~      �� 5      -
   5        $ task3��5      $��3��5�      $��
10,119,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��2��5�      �� 5�      -   5        $ task2��5�      $��2��5�      $��	10,66,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��2��5�      �� 5�      -   5        $ task4��5�      $��4��5�      $��
10,171,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��2��5�      �� 5�      -   5        $ task5��5�      $��5��5�      $��
10,223,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��2��5�      �� 5�      -   5        $ task6��5�      $��6��5�      $��
10,275,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��2��5�      �� 5�      -   5        $ task9��5�      $��9��5�      $��	85,14,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��1��5�      �� 5�      -   5        $ task10��5�      $��10��5�      $��	85,66,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��1��5�      �� 5�      -   5        $ task11��5�      $��11��5�      $��
85,119,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��1��5�      �� 5�      -   5        $ task12��5�      $��12��5�      $��
85,171,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��1��5�      �� 5�      -   5        $ task13��5�      $��13��5�      $��
85,223,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��1��5�      �� 5�      -   5        $ task14��5�      $��14��5�      $��
85,275,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5�      $	��Top=�5�      $��1��5�      �� 5�      -   5        $ task16��5�      $��16��5�      $��
235,66,0,0q�5�      $��1q�5�      #���  5�      $ Upper=�5�      $
��Left=�5�      $��85��5�      $��44��5      $	��Top=�5     $��1��5     �� 5     -   5        $ task17��5     $��17��5     $��235,119,0,0q�5     $��1q�5     #���  5     $ Upper=�5     $
��Left=�5	     $��85��5
     $��44��5     $	��Top=�5     $��1��5     �� 5     -   5        $ task18��5     $��18��5     $��235,171,0,0q�5     $��1q�5     #���  5     $ Upper=�5     $
��Left=�5     $��85��5     $��44��5     $	��Top=�5     $��1��5     �� 5     -   5        $ task19��5     $��19��5     $��235,223,0,0q�5     $��1q�5     #���  5     $ Upper=�5     $
��Left=�5     $��85��5      $��44��5!     $	��Top=�5"     $��1��5#     �� 5$     -   5        $ task20��5$     $��20��5%     $��235,275,0,0q�5&     $��1q�5'     #���  5(     $ Upper=�5)     $
��Left=�5*     $��85��5+     $��44��5,     $	��Top=�5-     $��1��5.     �� 5/     -   5        $ task21��5/     $��21��50     $��235,328,0,0q�51     $��1q�52     #���  53     $ Upper=�54     $
��Left=�55     $��85��56     $��44��57     $	��Top=�58     $��1��59     �� 5:     -   5        $ task7��5:     $��7��5;     $��
10,328,0,0q�5<     $��1q�5=     #���  5>     $ Upper=�5?     $
��Left=�5@     $��85��5A     $��44��5B     $	��Top=�5C     $��2��5D     �� 5E     -   5        $ task15��5E     $��15��5F     $��
85,328,0,0q�5G     $��1q�5H     #���  5I     $ Upper=�5J     $
��Left=�5K     $��85��5L     $��44��5M     $	��Top=�5N     $��1��5O     �� 5P     -   5        $ task22��5P     $��22��5Q     $��
390,14,0,0q�5R     $��1q�5S     #���  5T     $ Upper=�5U     $
��Left=�5V     $��85��5W     $��44��5X     $	��Top=�5Y     $��1��5Z     �� 5[     -   5        $ task8��5[     $��8��5\     $��
235,14,0,0q�5]     $��1q�5^     #���  5_     $ Upper=�5`     $
��Left=�5a     $��85��5b     $��44��5c     $	��Top=�5d     $��1��5e     �� 5f     -   5        $ task23��5f     $��23��5g     $��
390,66,0,0q�5h     $��1q�5i     #���  5j     $ Upper=�5k     $
��Left=�5l     $��85��5m     $��44��5n     $	��Top=�5o     $��1��5p     �� 5q     -    5        $ task24��5q     $��24��5r     $��390,119,0,0q�5s     $��1q�5t     #���  5u     $ Upper=�5v     $
��Left=�5w     $��85��5x     $��44��5y     $	��Top=�5z     $��1��5{     �� 5|     -!   5        $ task25��5|     $��25��5}     $��390,171,0,0q�5~     $��1q�5     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -"   5        $ task26��5�     $��26��5�     $��390,223,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -#   5        $ task27��5�     $��27��5�     $��390,275,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -$   5        $ task28��5�     $��28��5�     $��390,328,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -%   5        $ task29��5�     $��29��5�     $��
530,14,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -&   5        $ task30��5�     $��30��5�     $��
530,66,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -'   5        $ task33��5�     $��33��5�     $��530,223,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -(   5        $ task34��5�     $��34��5�     $��530,275,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -)   5        $ task35��5�     $��35��5�     $��530,328,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -*   5        $ task36��5�     $��36��5�     $��
669,14,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -+   5        $ task37��5�     $��37��5�     $��
669,66,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5�     -,   5        $ task38��5�     $��38��5�     $��669,119,0,0q�5�     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     $��1��5�     �� 5      --   5        $ task41��5      $��41��5     $��669,275,0,0q�5     $��1q�5     #���  5     $ Upper=�5     $
��Left=�5     $��85��5     $��44��5     $	��Top=�5	     $��1��5
     �� 5     -.   5        $ task42��5     $��42��5     $��669,328,0,0q�5     $��1q�5     #���  5     $ Upper=�5     $
��Left=�5     $��85��5     $��44��5     $	��Top=�5     $ 0.647,0.409&�5     $��1��5     �� 5     -/   5        $ task31��5     $��31��5     $��530,119,0,0q�5     $��1q�5     #���  5     $ Upper=�5     $
��Left=�5     $��85��5     $��44��5     $	��Top=�5      $��1��5!     �� 5"     -0   5        $ task39��5"     $��39��5#     $��669,171,0,0q�5$     $��1q�5%     #���  5&     $ Upper=�5'     $
��Left=�5(     $��85��5)     $��44��5*     $	��Top=�5+     $��1��5,     �� 5-     -1   5        $ task32��5-     $��32��5.     $��530,171,0,0q�5/     $��1q�50     #���  51     $ Upper=�52     $
��Left=�53     $��85��54     $��44��55     $	��Top=�56     $��1��57     �� 58     -2   5        $ task40��58     $��40��59     $��669,223,0,0q�5:     $��1q�5;     #���  5<     $ Upper=�5=     $
��Left=�5>     $��85��5?     $��44��5@     $	��Top=�5A     $��1��5B     5C     �� 5D     ���	#FFFFE9B8$
��Left=�5E     $	��160��5F     $��
15,487,0,0q�5G     
���   �$	��Top=�5I     $	��833��5J     �� 5K     -3   5        $ punTask1��5K     $��1��5L     $��
98,530,0,0q�5M     $��1q�5N     #���  5O     $ Upper=�5P     $
��Left=�5Q     $��85��5R     $��44��5S     $	��Top=�5T     �� 5U     -4   5        $ punTask2��5U     $��2��5V     $��242,530,0,0q�5W     $��1q�5X     #���  5Y     $ Upper=�5Z     $
��Left=�5[     $��85��5\     $��44��5]     $	��Top=�5^     �� 5_     -5   5        $ punTask3��5_     $��3��5`     $��397,530,0,0q�5a     $��1q�5b     #���  5c     $ Upper=�5d     $
��Left=�5e     $��85��5f     $��44��5g     $	��Top=�5h     �� 5i     -6   5        $ punTask4��5i     $��4��5j     $��544,530,0,0q�5k     $��1q�5l     #���  5m     $ Upper=�5n     $
��Left=�5o     $��85��5p     $��44��5q     $	��Top=�5r     �� 5s     -7   5        $ punTask5��5s     $��5��5t     $��684,530,0,0q�5u     $��1q�5v     #���  5w     $ Upper=�5x     $
��Left=�5y     $��85��5z     $��44��5{     $	��Top=�5|     �� 5}     -8   5        $ punTask6��5}     $��6��5~     $��397,593,0,0q�5     $��1q�5�     #���  5�     $ Upper=�5�     $
��Left=�5�     $��85��5�     $��44��5�     $	��Top=�5�     �� 5�     $��PUNISHMENT TASKS��6
   $
��Left=�5�     $��363,498,0,0q�5�     $	��Top=�5�     $��Myriad Pro Light)�5�     $��16(�5�     $
��Bold"�5�     �� 5�     
���P��$
��Left=�5�     $��17��5�     $��	16,64,0,0q�5�     
���   �$	��Top=�5�     $��25��5�     �� 5�     -9   5        $��punishment tasks��5�  
   $
��Left=�5�     $��	47,58,0,0q�5�     $	��Top=�5�     5�     �� 5�     -:   5        $��Random��5�     $��1q�5�       AccentedSquareButtonStyle#���  5�     $ Upper=�5�     $
��Left=�5�     $��
773,54,0,0q�5�     $	��Top=�5�     $��75��5�     $��34��5�     $ 0.493,0.882&�5�     5�     5�     