package itenium;
import java.util.Random;

import robocode.*;
import static robocode.util.Utils.normalRelativeAngleDegrees;
import java.awt.Color;
 

import robocode.HitByBulletEvent;
import robocode.HitWallEvent;
import robocode.Robot;
import robocode.ScannedRobotEvent;

public class Brauwer extends Robot {
	int count = 0;
    Random random = new Random();
    double gunTurnAmt;
 
    public void run() {
        setBodyColor(Color.green);
        setGunColor(Color.green);
        setRadarColor(Color.green);
        setScanColor(Color.white);
        setBulletColor(Color.blue);
 
        setAdjustGunForRobotTurn(true); // Keep the gun still when we turn
        gunTurnAmt = 10;
        while(true) {
            count++;
            if(count > 2){
                gunTurnAmt = -20;
            }
            if(count> 5){
                turnGunRight(360);
                gunTurnAmt = 20;
            }
            if(count>5){
                gunTurnAmt = 10;
            }
            turnGunRight(gunTurnAmt);
            turnGunRight(gunTurnAmt);
        }
    }
 
    public void onScannedRobot(ScannedRobotEvent e) {
 
        if(e.getDistance() > 150){
            gunTurnAmt = normalRelativeAngleDegrees(e.getBearing() + (getHeading() - getRadarHeading()));
 
            turnGunRight(gunTurnAmt);
            turnRight(e.getBearing());
        }
        gunTurnAmt = normalRelativeAngleDegrees(e.getBearing() + (getHeading() - getRadarHeading()));
        turnGunRight(gunTurnAmt);
        fire(3);
 
        if (e.getDistance() < 100) {
            if (e.getBearing() > -90 && e.getBearing() <= 90) {
                back(40);
            } else {
                ahead(40);
            }
        }
 
        if(e.getDistance() < 75)
        {
            ahead(75);
        }
        scan();
    }
 
    public void onHitByBullet(HitByBulletEvent e) {
        gunTurnAmt = normalRelativeAngleDegrees(e.getBearing() + (getHeading() - getRadarHeading()));
        turnGunRight(gunTurnAmt);
        fire(3);
        back(random.nextInt(100));
        count=0;
    }
 
 
    public void onHitRobot(HitRobotEvent e) {
        gunTurnAmt = normalRelativeAngleDegrees(e.getBearing() + (getHeading() - getRadarHeading()));
        turnGunRight(gunTurnAmt);
        fire(3);
        back(50);
        count=0;
    }
}
