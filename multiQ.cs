/*
Hannah Chang
multiQ.cs

CLASS INVARIANTS:               multiQ object is only active if at least one closePrime object
                                is active. The minimum and maximum stepped prime can be the same if
                                there is only 1 closePrime object that is active or if all of the
                                hidden numbers in a closePrime object are the same. closePrime objects
                                can have the same hidden numbers. Is active until all closePrime objects
                                are removed from the array (can call ping, add, remove, getstats).

INTERFACE INVARIANTS:           Cannot add an object if the client has not decreased the
                                array by removing an object. Client cannot select which 
                                object to remove, the object chosen is based on the least
                                indexed closePrime that's inactive or if all closePrime
                                objects are active, then the last closePrime object is 
                                selected to be removed. Cumulative statistics min, max, avg,
                                and count refers to the valid pings that the client enters. 
                           
IMPLEMENTATION INVARIANTS:      Tracks a constant number of closePrime objects (i.e.,
                                arrSize = 5). multiQ instance: "size" refers to the 
                                number of closePrime objects that occupy the array
                                (can be 0,1,2,3,4,5). removeObj() either: sets the least 
                                indexed closePrime that's inactive to null, decreases size, and
                                shifts the array; if all closePrime objects are active,
                                then the last closePrime object is set to null and the
                                size is decreased. multiPing(int) returns an array that
                                includes the ultimate min and max stepped primes that were found
                                and compared among the ACTIVE closePrime objects inside the array;
                                Index 0 = minimum stepped prime, Index 1 = maximum stepped prime.
                                The cumulative stats: minPing refers to the minimum ping val that
                                did not return an empty array after calling multiPing(int).
                                maxPing refers to the maximum ping val that did not return an empty
                                array after calling multiPing(int). Count refers to the total number 
                                of ping vals that did not return an empty array from multiPing(int), 
                                and the avg refers to the sum of all ping vals (that resulted a
                                nonempty array from multiPing(int))/count.
*/
namespace p2
{
    public class multiQ
    {        
        private const int arrSize = 5;
        private closePrime [] myPrimeArr;
        private int minPing;
        private int maxPing;
        private float avg;
        private int size;
        private int count = 0;
        private bool isActive;

        public multiQ()
        {
            const int initialize = 0;
            size = arrSize;
            initializeArr();
            checkActive();
            updateStats(initialize);
        }
        /*
        ADDOBJ:
        PRECONDITIONS:  Object can be active or inactive.
        POSTCONDITIONS: If the number of closePrime objects that occupy
                            the array are equal to the size of the array, then
                            no objects are added and return false. Otherwise, a
                            new closePrime object is added to the array and the
                            number of closePrime objects that occupy the array
                            increases.
        */
        public bool addObj()
        {
            if (size == arrSize)
                return false;

            myPrimeArr[size] = new closePrime();
            size++;
            return true;
        }
        /*
        REMOVEOBJ:
        PRECONDITIONS:  Object can be active or inactive.
        POSTCONDITIONS: If size is 0, then return false. Otherwise, set
                        the least indexed closePrime that's inactive to
                        null, decrease size, shift the array, and return true.
                        If all closePrime objects are active, then set the last
                        closePrime object to null and decrease the size and 
                        return true.
        */
        public void removeObj()
        {
            int i = 0;

            if (size == 0)
                return;
            
            while(i < size && myPrimeArr[i].query())
                i++;

            // no inActive objs found
            if (i == size)
            {
                myPrimeArr[size-1] = null;
                size--;
                return;
            }

            // if last obj is inActive
            else if (i == size-1)
            {
                myPrimeArr[size] = null;
                size--;
                return;
            }

            shiftArray(i);          
        }
        /*
        SHIFTARRAY:
        PRECONDITIONS:  Object needs to be active.
        POSTCONDITIONS: Shifts the array so that the object with index int is
                        in index = 4. 
        */
        private void shiftArray(int index)
        {
            closePrime tempClosePrime;
            for (int i = index; i < size; i++)
            {    
                int j = i + 1;
                if (j < size)
                {
                    tempClosePrime = myPrimeArr[i];
                    myPrimeArr[i] = myPrimeArr[j];
                    myPrimeArr[j] = tempClosePrime;
                    j++;
                }
            }
            size--;
            myPrimeArr[size] = null;
        }
        /*
        MULTIPING:
        PRECONDITIONS:  Object needs to be active.
        POSTCONDITIONS: If preconditions are not met or the user's number is invalid
                        (nonpositive int) then an empty array representing the min
                        and max stepped prime is returned. Otherwise, an array 
                        containing the min and max stepped prime is returned. 
        */
        public int[] multiPing(int userNum)
        {
            const int SIZE = 2;
            int [] minMaxArr = new int[SIZE];
            for (int i = 0; i < SIZE; i++)
                minMaxArr[i] = 0;
            
            if (!getState() || userNum <= 0)
                return minMaxArr;

            minMaxArr = findMinMaxPing(minMaxArr, userNum);
            return minMaxArr;   
        }
        /*
        FINDMINMAXPING: 
        PRECONDITIONS:  Object needs to be active.
        POSTCONDITIONS: An array containing the ultimate min and max stepped prime is
                        returned; Index 0 = min, Index 1 = max. Count is incremented
                        and the stats are updated.
        */
        private int [] findMinMaxPing(int [] arr, int num)
        {
            int minPrime = -1;
            int maxPrime = -1;
            int currNum = 0;
            
            for (int i = 0; i < size; i++)
            {   
                currNum = myPrimeArr[i].ping(num);

                // if it's a valid input or isActive
                if (currNum > 1)
                {   
                    if (minPrime == -1 || maxPrime == -1)
                    {
                        if (minPrime == -1)
                            minPrime = currNum;
                        if (maxPrime == -1)
                            maxPrime = currNum;
                    }
                    else if (currNum < minPrime)
                        minPrime = currNum;
                    
                    else if (currNum > maxPrime)
                        maxPrime = currNum;
                }
            } 
            updateStats(num);
            count++;
            arr[0] = minPrime;
            arr[1] = maxPrime;
            return arr;
        }
        private void updateStats(int num)
        {
            updateMin(num);
            updateMax(num);
            updateAvg(num);
        }
        public int getCount()
        {
            return count;
        }
        public int getMax()
        {
            return maxPing;
        }
        public int getMin()
        {
            return minPing;
        }
        public float getAvg()
        {
            return avg;
        }
        /*
        UPDATEAVG:
        PRECONDITIONS:  Object needs to be active.
        POSTCONDITIONS: The function call happens only if the object is
                        active. The average is recalulated using the new
                        int.
        */
        private void updateAvg(int num)
        {
            if (count == 0)
                avg = num/1;
            else
            {
                avg *= count;
                avg += num;
                avg /= (count+1);
            }
        }
        /*
        UPDATEMAX:
        PRECONDITIONS:  Object needs to be active.
        POSTCONDITIONS: The function call happens only if the object is
                        active. The maximum is compared to the new integer
                        and is updated if the new int is greater
                        than the previous max.
        */
        private void updateMax(int num)
        {
            if (num > maxPing)
                maxPing = num;
        }
        /*
        UPDATEMIN:
        PRECONDITIONS:  Object needs to be active.
        POSTCONDITIONS: The function call happens only if the object is
                        active. The minimum is compared to the new integer
                        and is updated if the new int is smaller
                        than the previous min.
        */
        private void updateMin(int num)
        {
            if (num < minPing || minPing == 0)
                minPing = num;
        }
        /*
        CHECKACTIVE:
        PRECONDITIONS:  Object can be inactive or active.
        POSTCONDITIONS: If at least one closePrime object is active in the
                        array, then the multiQ object is set to true. Otherwise,
                        the multiQ object is set to false.
        */
        private void checkActive()
        {
            for (int i = 0; i < size; i++)
            {
                if (myPrimeArr[i].query())
                {
                    isActive = true;
                    return;
                }
            }
            isActive = false;
        }
        public bool getState() 
        {
            checkActive();
            return isActive;
        }
        /*
        INITIALIZEARR:
        PRECONDITIONS:  Object is active.
        POSTCONDITIONS: An array of closePrime objects with size arrSize is created and each element is
                        initialized to a new closePrime object.
        */
        private void initializeArr()
        {   
            myPrimeArr = new closePrime[arrSize];
           
            for (int i = 0; i < arrSize; i++)
            {
                myPrimeArr[i] = new closePrime();
            }
        }
    }
}