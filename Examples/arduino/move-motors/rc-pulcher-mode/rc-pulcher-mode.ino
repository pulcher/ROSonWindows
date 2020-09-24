//On Roboclaw set switch 1 and 6 on. // <-- what does this refer to?
//mode 2 option 4 // <-- my note based on user manual pg 26

#include <Servo.h> 

Servo myservo1;  // create servo object to control a Roboclaw channel
Servo myservo2;  // create servo object to control a Roboclaw channel

//int pos = 0;    // variable to store the servo position  //<-- left-over from arduino ide servo sweep example?

#define MINMIN 1120
#define MIN 1250
#define MINHALF 1320
#define MINHALFHALF 1410
#define MAXMAX 1920
#define MAX 1750 
#define MAXHALF 1625
#define MAXHALFHALF 1563
#define STOP 1500

void setup() 
{ 
  Serial.begin(115200);
    while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port onlyUFG
    }
  myservo1.attach(5);  // attaches the RC signal on pin 5 to the servo object 
  myservo2.attach(6);  // attaches the RC signal on pin 6 to the servo object 

  myservo1.writeMicroseconds(STOP);  //Stop   
  myservo2.writeMicroseconds(STOP);  //Stop   
  delay(2000);
} 
 

void loop() 
{ 
  
  stop();

  Serial.println("FULL reveree");
  myservo1.writeMicroseconds(MINMIN);  //full reverse
  delay(1000); 

  stop();

  Serial.println("FULL forward");
  myservo1.writeMicroseconds(MAXMAX);  //full forward   
  delay(1000); 

  stop();
  
  Serial.println("Half forward");
  myservo1.writeMicroseconds(MAXHALF);  //half forward
  delay(1000); 

  stop();
  
  Serial.println("slow forward");
  myservo1.writeMicroseconds(MAXHALFHALF);  //slow forward
  delay(1000); 

  stop();
  
  Serial.println("Half Reverse");
  myservo1.writeMicroseconds(MINHALF);  // Half Reverse
  delay(1000); 

  stop();

  Serial.println("Slow Reverse");
  myservo1.writeMicroseconds(MINHALFHALF);  // Slow Reverse
  delay(1000); 

  stop();

  Serial.println("spin turn right full");
  myservo2.writeMicroseconds(MINMIN);  // full Spin Right
  delay(1000); 

  stop();

  Serial.println("spin turn left full");
  myservo2.writeMicroseconds(MAXMAX);  // full Spin left   
  delay(1000); 

  stop();

  Serial.println("spin turn right slow");
  myservo2.writeMicroseconds(MINHALFHALF);  // slow Spin Right
  delay(1000); 

  stop();

  Serial.println("spin turn left slow");
  myservo2.writeMicroseconds(MAXHALFHALF);  // slow Spin left   
  delay(1000); 

  stop();
  
  Serial.println("full forward, hard right turn");
  myservo1.writeMicroseconds(MAXMAX);  //full forward   
  myservo2.writeMicroseconds(MINMIN);  //full right
  delay(1000);

  stop();

  Serial.println("full forward, slow right turn");
  myservo1.writeMicroseconds(MAXMAX);  //full forward   
  myservo2.writeMicroseconds(MINHALFHALF);  //full right
  delay(1000);

  stop();

    Serial.println("full reverse, hard right turn");
  myservo1.writeMicroseconds(MINMIN);  //full reverse   
  myservo2.writeMicroseconds(MINMIN);  //full right
  delay(1000);

  stop();

  Serial.println("full reverse, slow right turn");
  myservo1.writeMicroseconds(MINMIN);  //full reverse   
  myservo2.writeMicroseconds(MINHALFHALF);  //full right
  delay(1000);

  stop();
}

void stop() 
{
  Serial.println("stop");
  myservo1.writeMicroseconds(STOP);  //Stop   
  myservo2.writeMicroseconds(STOP);  //Stop   
  delay(2000);
}
