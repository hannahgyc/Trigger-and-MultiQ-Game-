/*
Hannah Chang
p2.cs
WQ CPSC 3200
CREATED: 01/17/19 - REVISIONS: 01/18/19, 01/19/19, 01/22/19
Visual Studio Code: p2.cs, closeprime.cs version 2, multiQ.cs, trigger.cs

DRIVER OVERVIEW:    Test Trigger:   I've created an array of Trigger objects of size 10. This means
                                    that I am testing 10 trigger objects. We use switch statements 
                                    where case 1 retrieves the cumulative stats and case 2 pings 
                                    a random integer (1,101) and we use a while loop and a random 
                                    number generator (1,3) to switch between these cases. To move 
                                    onto the next trigger object in the trigger array, the trigger
                                    object needs to be deactivated. A trigger object deactivates when
                                    the number of pings reach/exceed the smallest countLimit of the 
                                    two hidden closePrimes. Once the end of the trigger array is 
                                    reached, the program transitions into testing the multiQ class.
                    TestMultiQ:     I've created an array of multiQ object of size 10. This means that
                                    I am testing 10 multiQ objects. We use switch statements where
                                    case 1 pings a random integer (1,101) and case 2 retrieves the
                                    cumulative statistics and case 3 adds a closePrime object into the
                                    array and case 4 removes a closePrime object from the array; we use
                                    a while loop and a random number generator (1,5) to switch between
                                    these cases. To move onto the next multiQ object in the multiQ
                                    array, the client (driver) must remove all closePrime objects
                                    from the array inside the multiQ object. The program ends 
                                    (while loop stops) when all of the closePrime objects are 
                                    removed from the array encapsulated inside the multiQ object. 
                                    A closePrime object is removed only when the driver program 
                                    calls removeObj through the multiQ object. Note that
                                    the multiQ object is active until all closePrime objects
                                    are removed from the array (can call ping, add, remove, getstats).
*/
using System;
namespace p2
{
    class Driver
    {
        static void Main()
        {
            Console.WriteLine("Welcome!");
            const int SIZE = 10;
            trigger[] myTrigger = new trigger[SIZE]; 
            multiQ[] myMultiQ = new multiQ[SIZE];

            initializeTrigger(myTrigger, SIZE);
            initializeMultiQ(myMultiQ, SIZE);
            testMyClasses(myTrigger, SIZE, myMultiQ, SIZE);
        }
        static void initializeMultiQ(multiQ [] arr, int S)
        {
            for (int i = 0; i < S; i++)
            {
                arr[i] = new multiQ();
            }
        }
        static void initializeTrigger(trigger [] myArr, int S)
        {
            for (int i = 0; i < S; i++)
            {
                myArr[i] = new trigger();
            }
        }

        static void testMyClasses(trigger [] myArr, int S, multiQ [] myMultiQ, int size)
        {
            testTrigger(myArr, S);

            Console.WriteLine("Let's play a new game!");
            Console.WriteLine();

            testMultiQ(myMultiQ, size);
        }

        static void testTrigger(trigger [] myArr, int size)
        {
            Random rndNum = new Random();
            int caseNum = 0;

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("I've created two new hidden numbers for you, let's play!");

                while (myArr[i].getState())
                {
                    caseNum = rndNum.Next(1, 3);
                    switch (caseNum)
                    {
                        case 1:
                            getTriggerStats(myArr, size, i);
                            break;

                        case 2: 
                            triggerPing(myArr, size, i);
                            break;
                    }
                }
            }
        }
        static void triggerPing(trigger [] myArr, int size, int i)
        {
            int pingNum = 0;
            Random rndNum = new Random();
            pingNum = rndNum.Next(1, 101);
            Console.Write("Pinging " + pingNum + "... ");
            Console.WriteLine(myArr[i].triggerPing(pingNum));
            Console.WriteLine();
        }
        static void getTriggerStats(trigger [] myArr, int size, int i)
        {
            Console.WriteLine("Cumulative Statistics: ");
            Console.WriteLine("Min Successful Ping Value: " + myArr[i].getMin());
            Console.WriteLine("Max Successful Ping Value: " + myArr[i].getMax());
            Console.WriteLine("Avg Successful Ping Value: " + myArr[i].getAvg());
            Console.WriteLine("# of Successful Ping Values: " + myArr[i].getCount());
            Console.WriteLine();
        }

        static void testMultiQ(multiQ [] myArr, int size)
        {
            Random rndNum = new Random();
            int caseNum = 0;
            
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("I have created a new list of hidden numbers, let's play!");


                while (myArr[i].getState())
                {
                    caseNum = rndNum.Next(1, 5); 
                    switch (caseNum)
                    {
                        case 1:
                            testMultiQPing(myArr, i);
                            break;

                        case 2:
                            getMultiQStats(myArr, i);
                            break;

                        case 3:
                            addObjMultiQ(myArr, i);
                            break;

                        case 4:
                            removeObjMultiQ(myArr, i);
                            break;
                    }   
                }
            }
        }
        static void removeObjMultiQ(multiQ [] myMultiQ, int i)
        {
            Console.WriteLine("Removing a hidden number... ");
            myMultiQ[i].removeObj();
            Console.WriteLine();
        }
        
        static void addObjMultiQ(multiQ [] myMultiQ, int i)
        {
            Console.WriteLine("Adding a new hidden number... " + myMultiQ[i].addObj());
            Console.WriteLine();
        }
        static void getMultiQStats(multiQ [] myMultiQ, int i)
        {
            Console.WriteLine("Cumulative Statistics: ");
            Console.WriteLine("Min Successful Ping Value: " + myMultiQ[i].getMin());
            Console.WriteLine("Max Successful Ping Value: " + myMultiQ[i].getMax());
            Console.WriteLine("Avg Successful Ping Value: " + myMultiQ[i].getAvg());
            Console.WriteLine("# of Successful Ping Values: " + myMultiQ[i].getCount());
            Console.WriteLine();
        }

        static void testMultiQPing(multiQ [] myMultiQ, int i)
        {
            const int SIZE = 2;
            int [] minMaxArr = new int[SIZE];
            int pingNum = 0;
            Random rndNum = new Random();

            pingNum = rndNum.Next(1, 101);
            Console.WriteLine("Pinging " + pingNum + "... ");

            minMaxArr = myMultiQ[i].multiPing(pingNum);
            Console.WriteLine("Minimum Stepped Prime: " + minMaxArr[0]);
            Console.WriteLine("Maximum Stepped Prime: " + minMaxArr[1]);
            Console.WriteLine();
        }
    }
}