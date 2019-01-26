/*
Hannah Chang 
closePrime.cs version 2

CLASS INVARIANTS:           Every closePrime object is unique. If the client
                            pings more than the countLimit, then the object 
                            switches to inactive. Reviving an active object switches
                            the object's deactivateForever to true. 
                            
INTERFACE INVARIANTS:       Clients are unable to ping non-positive integers
                            (i.e., negative ints or zero), if they do the program
                            outputs -1 (not a prime number). Clients are unable to
                            permanently deactivate an object unless they revive
                            an active object. Clients are able to query the state
                            and reset the object. The object becomes inactive after
                            the countLimit is reached.
                                                      
IMPLEMENTATION INVARIANTS:  ping(int) does not support nonpositive integers,
                            the program will return a -1 (not a prime) to client.
                            The hidden number is generated through a random number
                            generator that generates a number from 1 to 10 in
                            setHiddenNumber(). The Count Limit is set to 2 * hidden
                            number in the setCountLimit. reset() does not generate
                            a new hidden number or new count limit.
*/
using System;
namespace p2
{
    public class closePrime
    {
        private int hiddenNum;
        private bool isActive;
        private int count;
        private int countLimit;
        private bool deactivateForever;
        public closePrime()
        {
            reset();
            setHiddenNum();
            setCountLimit();
        }
        public bool query()
        {
            checkLimit();
            return isActive;
        }
        public bool getPermDeactivated()
        {
            return deactivateForever;
        }

        /*
        PING:
        PRECONDITIONS:  Object needs to be active and the ping number needs
                        to be a positive number (0 < int)
        POSTCONDITIONS: If preconditions are not met, then return -1 
                        (-1 is not a prime). If preconditions are met, then
                        return the stepped prime.
        */
        public int ping(int pingVal)
        {   
            if (!query()) 
                return -1;
           
            count++;

            if (pingVal <= 0) // Invalid input
                return -1;
           
            int closestPrime = pingVal;
            
             for (int i = 0; i <= hiddenNum; i++)
            {   closestPrime++;
                for (int j = 0; !checkPrime(closestPrime); j++)
                {
                    closestPrime++;
                }       
            }
            return closestPrime;
        }
        /*
        RESET:
        PRECONDITIONS:  All states are valid.
        POSTCONDITIONS: Calls setActive(), re-initializes closePrime instances:
                        count = 0, deactivateForever = false.
        */
        public void reset()
        {
            setActive();
            count = 0;
            deactivateForever = false;
        }
        /*
        REVIVE:
        PRECONDITIONS:  Object needs to be inactive.
        POSTCONDITIONS: If preconditions are not met, then the object is
                        permanently deleted. If preconditions are met, then
                        we call setActive(), and re-initialize closePrime
                        instances: count = 0.
        */
        public void revive()
        {
            if (isActive)
                permanentlyDeactivate();
            else
            {
                setActive();
                count = 0;
            }
        }

        /*
        CHECKLIMIT:
        PRECONDITIONS:  All states are valid.
        POSTCONDITIONS: If count is greater than or equal to countLimit AND
                        the object is acitve, then we call deactivate().
        */
        private void checkLimit()
        {
            if (count >= countLimit)
                deactivate();
        } 
        private bool checkPrime(int val) 
        {
            if (val == 2)
                return true;
                
            if (val%2 == 0)
                return false;            

            int i = 3;
            while((val % i) != 0)
            {
                i = i + 2;
            }

            if (i >= val) // IS PRIME
                return true;

            else 
                return false;
        }
        /*
        DEACTIVATE:
        PRECONDITIONS:  All states are valid.
        POSTCONDITIONS: Sets isActive to false.
        */
        private void deactivate()
        {
            isActive = false;
        }
        /*
        PERMANENTLYDEACTIVATE:
        PRECONDITIONS:  Object needs to be active.
        POSTCONDITIONS: Only called by revive() if the object is already active.
                        If object is active, then call deactivate and set
                        foreverDeactivate to true. 
        */
        private void permanentlyDeactivate()
        {
            if (isActive)
                deactivate();

            deactivateForever = true;
        }
        /*
        SETACTIVE:
        PRECONDITIONS:  Object can be inactive or active.
        POSTCONDITIONS: isActive is set to active.
        */
        private void setActive()
        {
            isActive = true;
        }
        private void setCountLimit()
        {
            const int multiplier = 2;
            countLimit = (multiplier * hiddenNum);
        }
        private void setHiddenNum()
        {
            Random random = new Random();
            hiddenNum = random.Next(1,11);
        }
    }
}