/*
Hannah Chang
trigger.cs

CLASS INVARIANTS:           The smallest countLimit out of the two hidden numbers
                            is the count limit for the entire trigger object, the
                            object is deactivated if at least one closePrime object
                            is inactive. The maximum index can be is the smallest
                            countLimit of the two hidden numbers. The two hidden 
                            Numbers can be the same.

INTERFACE INVARIANTS:       Cumulative statistics refers to releasing the min,
                            max, avg, and total count of the pinged values that
                            resulted in a true from triggerPing(int). 
                            triggerPing(int) returns a bool; true if the stepped
                            primes have the same last digit, false otherwise.
                            Cumulative statistics refers to the valid pings that
                            the client enters.

IMPLEMENTATION INVARIANTS:  Child of the closePrime class. Trigger object is
                            considered active if exactly both of the closePrime
                            objects are active. triggerPing(int) returns true if
                            the stepped primes have the same last digit, and
                            false if the stepped primes do not have the same
                            last digit or if the trigger object is not active.
                            The integers that the client enters when calling
                            triggerPing(int) are stored in an array (that 
                            is initially size = 5 but can expand) only if the
                            two stepped primes have the same last digit (aka if 
                            triggerPing(int) returns true). The cumulative stats: 
                            minIndex refers to the minimum ping val that resulted a true
                            from triggerPing(int), maxIndex refers to the maximum ping
                            val that resulted a true from triggerPing(int), count
                            refers to the total number of ping vals that resulted a
                            true from triggerPing(int), and the avg refers to the
                            sum of all ping vals (that resulted a true from
                            triggerPing(int))/count. Index refers to the amount of
                            values inside the array.                            
*/
namespace p2
{
    public class trigger
    {
        private closePrime hiddenNum1;
        private closePrime hiddenNum2;
        private const int ARR_SIZE = 5;
        private int [] validValsArr;
        private int index = 0;
        private float avg;
        private int minIndex;
        private int maxIndex;
        private bool isActive;

        public trigger()
        {
            hiddenNum1 = new closePrime();
            hiddenNum2 = new closePrime();
            areBothActive();
            initializeArr(); 
            updateStats();
        }
        public bool getState() 
        {
            areBothActive();
            return isActive;
        }
        /*
        TRIGGERPING:
        PRECONDITIONS:  Object must be active.
        POSTCONDITIONS: If preconditions are not met or the user enters an invalid input
                        (nonpositive number), then the function will return false. If the
                        preconditions are met and the two stepped primes have the same
                        last digit, then function returns true. Otherwise, it returns false.
        */
        public bool triggerPing(int userNum)
        {
            if (!getState())
                return false;
            
            int ping1 = hiddenNum1.ping(userNum);
            int ping2 = hiddenNum2.ping(userNum);
            
            // invalid input
            if (ping1 <= 0 || ping2 <= 0)
                return false;
           
            if ((ping1 % 10) == (ping2 % 10))
                {
                    if (!isDuplicate(userNum))
                    {
                        if (index >= ARR_SIZE)
                            sizeUpArr(index);
                        
                        validValsArr[index] = userNum;
                        updateStats();
                        index++;
                    }
                    return true;
                }
            return false;
        }
        /*
        SIZEUPARR:
        PRECONDITIONS:  Trigger object needs to be active. 
        POSTCONDITIONS: The function call happens only if the object is
                        active. The array increases in size by 1.
        */
        private void sizeUpArr(int index)
        {
            int [] tempArr = new int[index+1];
            for (int i = 0; i < index; i++)
            {
                tempArr[i] = validValsArr[i];
            }

            for (int i = index; i < index+1; i++)
            {
                tempArr[i] = 0;
            }
            validValsArr = tempArr;
            tempArr = null;
        }

        private bool isDuplicate(int num)
        {
            for (int i = 0; i < index; i++)
            {
                if (num == validValsArr[i])
                    return true;
            }
            return false;
        }
        /*
        UPDATEMIN:
        PRECONDITIONS:  Trigger object needs to be active.
        POSTCONDITIONS: The function call happens only if the object is
                        active. The minimum is compared to the new integer
                        that is stored into the array, and is updated if
                        the new int is smaller than the previous min.
        */
        private void updateMin()
        {
            if (index == 0)
                minIndex = 0;
            else
            {
                if (validValsArr[index] < validValsArr[minIndex])
                    minIndex = index;
            }
        }
        /*
        UPDATEMAX:
        PRECONDITIONS:  Trigger object needs to be active.
        POSTCONDITIONS: The function call happens only if the object is
                        active. The maximum is compared to the new integer
                        that is stored into the array, and is updated if
                        the new int is greater than the previous max.
        */
        private void updateMax()
        {
            if (index == 0)
                maxIndex = 0;
            else
            {
                if (validValsArr[index] > validValsArr[maxIndex])
                    maxIndex = index;
            }
        }
        /*
        UPDATEAVG:
        PRECONDITIONS:  Trigger object needs to be active.
        POSTCONDITIONS: The function call happens only if the object is
                        active. The average is recalulated using the new
                        int that was added to the array.
        */
        private void updateAvg()
        {
            if (index == 0)
                avg = validValsArr[index]/1;
            else
            {
                avg *= index;
                avg += validValsArr[index];
                avg /= (index+1);
            }
        }
        private void updateStats()
        {
            updateMin();
            updateMax();
            updateAvg();
        }
        public int getCount()
        {
            return index;
        }
        public int getMax()
        {
            return validValsArr[maxIndex];
        }
        public int getMin()
        {
            return validValsArr[minIndex];
        }
        public float getAvg()
        {
            return avg;
        }
        /*
        AREBOTHACTIVE:
        PRECONDITIONS:  Object can be inactive or active.
        POSTCONDITIONS: If both closePrime objects are active, then isActive
                        is set to true, otherwise isActive is set to false.
        */
        private void areBothActive()
        {
            if (hiddenNum1.query() && hiddenNum2.query())
                isActive = true;
            else
                isActive = false;
        }
        /*
        INITIALIZEARR:
        PRECONDITIONS:  Object is active.
        POSTCONDITIONS: An array of ints with size ARR_SIZE is created and each element is initialized to 0.
        */
        private void initializeArr()
        {
            validValsArr = new int[ARR_SIZE];
            for (int i = 0; i < ARR_SIZE; i++)
            {
                validValsArr[i] = 0;
            }
        }

    }
}