# Robocode Tactics

[Predictive implementation in Java](https://github.com/mgalushka/robocode-robots/blob/master/src/main/java/com/maximgalushka/robocode/MaximBot.java)


# Moving

**Basic**  
[Oscillator Movement](http://robowiki.net/wiki/Oscillator_Movement): Like the Strafer  
[Stop and Go](http://robowiki.net/wiki/Stop_And_Go): Stand still and move when shot fired  
[Mirror Movement](http://robowiki.net/wiki/Mirror_Movement)  
[GoTo](http://robowiki.net/wiki/GoTo): Go to a point on the battlefield  
[Enemy Dodging Movement](http://robowiki.net/wiki/Enemy_Dodging_Movement)  


**Advanced**  
[Energy Drop](http://robowiki.net/wiki/Energy_Drop): Find out when the enemy fired  
[Wall Smoothing](http://robowiki.net/wiki/Wall_Smoothing): No abrupt turning when hitting a wall  
[Wave Surfing](http://robowiki.net/wiki/Wave_Surfing): Observe bullet [Waves](http://robowiki.net/wiki/Waves) and move to safe locations. [Tutorial](http://robowiki.net/wiki/Wave_Surfing_Tutorial)  

[Enemy movement tracking](https://www.ibm.com/developerworks/java/library/j-movement/index.html)  
[Anti-gravity movement](https://www.ibm.com/developerworks/library/j-antigrav/index.html): Create a heat map with locations you want to avoid / go to. [Tutorial](http://robowiki.net/wiki/Anti-Gravity_Tutorial)  
[Bullet Dodging](https://www.ibm.com/developerworks/library/j-dodge/index.html)  



# Targeting

**Basic**  
[Head-On Targeting](http://robowiki.net/wiki/Head-On_Targeting): Good against Oscillator and (sometimes) random movement  
[Lineair targeting](http://robowiki.net/wiki/Linear_Targeting): Good against wallers  
[Circular targeting](https://www.ibm.com/developerworks/library/j-circular/index.html) / [Tutorial](http://robowiki.net/wiki/Circular_Targeting/Walkthrough) 
[Pattern Matching](http://robowiki.net/wiki/Pattern_Matching): Match current movement against collected data  

**Advanced**  
[GuessFactor](http://robowiki.net/wiki/GuessFactor): Measure the likely angle(s) to shoot based on enemy data & bullet travel time. [Tutorial](http://robowiki.net/wiki/GuessFactor_Targeting_Tutorial)  
[GuessFactor Targeting](http://robowiki.net/wiki/GuessFactor_Targeting_(traditional)): Statistically generate a good firing angle  
[Dynamic Clustering](http://robowiki.net/wiki/Dynamic_Clustering): Search log for entries similar to the current situation. [Tutorial](http://robowiki.net/wiki/Dynamic_Clustering_Tutorial)  



# Debugging

[RoboRunner](http://robowiki.net/wiki/RoboRunner) / [RoboJogger](http://robowiki.net/wiki/RoboJogger): Automated battle running  
[Graphical Debugging](http://robowiki.net/wiki/Robocode/Graphical_Debugging): Draw what your bot is up to on the battlefield  
[SuperSampleBots](http://robowiki.net/wiki/Category:Super_Sample_Bots): To learn from and practice against  

# DrussGT

[Understanding DrussGT](http://robowiki.net/wiki/DrussGT/Understanding_DrussGT): The current world champion  
