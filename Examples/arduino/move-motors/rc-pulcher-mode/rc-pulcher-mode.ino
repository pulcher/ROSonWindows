//On Roboclaw set switch 1 and 6 on. // <-- what does this refer to?
//mode 2 option 4 // <-- my note based on user manual pg 26

#include <Servo.h> 

Servo myservo1;  // create servo object to control a Roboclaw channel
Servo myservo2;  // create servo object to control a Roboclaw channel

//int pos = 0;    // variable to store the servo position  //<-- left-over from arduino ide servo sweep example?

#define MINMIN 1120
#define MIN 1250
#define MINHALF 1320
#define MAXMAX 1920
#define MAX 1750 
#define MAXHALF 1625
#define STOP 1520

void setup() 
{ 
  Serial.begin(115200);
    while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
    }
  myservo1.attach(5);  // attaches the RC signal on pin 5 to the servo object 
  myservo2.attach(6);  // attaches the RC signal on pin 6 to the servo object 
} 
 

void loop() 
{ 
  myservo1.writeMicroseconds(STOP);  //Stop   
  myservo2.writeMicroseconds(STOP);  //Stop   
  delay(2000);

  Serial.println("MIN forward");
  myservo1.writeMicroseconds(MIN);  //full forward   
  delay(1000); 

  myservo1.writeMicroseconds(STOP);  //Stop   
  myservo2.writeMicroseconds(STOP);  //Stop   
  delay(2000);

  Serial.println("MINHALF forward");
  myservo1.writeMicroseconds(MINHALF);  //half forward   
  delay(1000); 

  myservo1.writeMicroseconds(STOP);  //Stop   
  myservo2.writeMicroseconds(STOP);  //Stop   
  delay(2000);

  Serial.println("MINMIN forward");
  myservo1.writeMicroseconds(MINMIN);  //full forward   
  delay(1000); 
/*
  Serial.println("stop");
  myservo1.writeMicroseconds(STOP);  //stop   
  delay(2000); 

  Serial.println("full reverse");
  myservo1.writeMicroseconds(MAX);  //full reverse
  delay(1000); 

  Serial.println("stop");
  myservo1.writeMicroseconds(STOP);  //Stop   
  delay(2000); 

  Serial.println("full turn left");
  myservo2.writeMicroseconds(MIN);  //full turn left   
  delay(1000); 

  Serial.println("stop");
  myservo2.writeMicroseconds(STOP);  //Stop   
  delay(2000); 

  Serial.println("full turn right");
  myservo2.writeMicroseconds(MAX);  //full turn right   
  delay(1000); 
  */
}
