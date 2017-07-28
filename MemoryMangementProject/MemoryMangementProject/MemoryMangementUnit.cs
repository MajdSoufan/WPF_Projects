using System.Collections.Generic;
using System.Windows;

namespace MemoryMangementProject
{
    class MemoryMangementUnit
    {
        public int numOfFrames;
        public int numPageFaultsLRU { get; set; }
        public int numPageFaultsOPT { get; set; }
        public int numPageFaultsFIFO { get; set; }


        public List<PPReference> processPagesList = new List<PPReference>();

        public MemoryMangementUnit(int numberofFrames)
        {
            numPageFaultsLRU = 0;
            numPageFaultsOPT = 0;
            numPageFaultsFIFO = 0;
            this.numOfFrames = numberofFrames;
        }


        //-----------------------------------------------------    LRU Start       ----------------------------------------//

        public void RunLRU()
        {
            List<PPReference> theFrames = new List<PPReference>();
            theFrames.Capacity = numOfFrames;
            List<int> framesIndexCounters = new List<int>(numOfFrames);
            framesIndexCounters.Capacity = numOfFrames;
            instantiatingCounters(framesIndexCounters);

            for (int i = 0; i < processPagesList.Count; i++)
            {
                if (!isContainedInFrames(processPagesList[i], theFrames))
                {
                    if (theFrames.Count < numOfFrames)
                    {
                        theFrames.Add(processPagesList[i]);
                    }
                    else
                    {
                        for (int j = 0; j < theFrames.Count; j++)
                        {
                            for (int k = i; k >= 0; k--)
                            {
                                if (theFrames[j].isEqual(processPagesList[k]))
                                {
                                    framesIndexCounters[j] = k;
                                    break;
                                }
                            }
                        }

                        theFrames[getTheLRUprocessPageIndex(framesIndexCounters)] = processPagesList[i];

                    }
                    numPageFaultsLRU++;

                }

            }
        }

        private bool isContainedInFrames(PPReference processPage, List<PPReference> theFrames)
        {
            foreach (var process in theFrames)
            {
                if (process.isEqual(processPage))
                    return true;
            }
            return false;
        }

        private int getTheLRUprocessPageIndex(List<int> framesCounter)
        {
            int theLRUnum = framesCounter[0];
            int theLeastIndex = 0;
            for (int i = 0; i < framesCounter.Count; i++)
            {
                if (framesCounter[i] < theLRUnum)
                {
                    theLRUnum = framesCounter[i];
                    theLeastIndex = i;
                }
            }

            return theLeastIndex;
        }

        private void instantiatingCounters(List<int> counters)
        {
            for (int i = 0; i < numOfFrames; i++)
            {
                counters.Add(0);
            }
        }

        //-----------------------------------------------------    LRU END   ----------------------------------------//

        //-----------------------------------------------------    OPT START   ----------------------------------------//
        public void RunOPT()
        {
            List<PPReference> theFrames = new List<PPReference>(numOfFrames);
            theFrames.Capacity = numOfFrames;
            List<int> framesIndexCounters = new List<int>(numOfFrames);
            framesIndexCounters.Capacity = numOfFrames;
            instantiatingCounters(framesIndexCounters);

            for (int i = 0; i < processPagesList.Count; i++)
            {
                if (!isContainedInFrames(processPagesList[i], theFrames))
                {
                    if (theFrames.Count < numOfFrames)
                    {
                        theFrames.Add(processPagesList[i]);
                    }
                    else
                    {
                        for (int j = 0; j < theFrames.Count; j++)
                        {
                            for (int k = i; k < processPagesList.Count; k++)
                            {
                                
                            }
                        }



                       // theFrames[indexOfLowestRankItem(theFrames, processPagesList.IndexOf(processPagesList[i]))] = processPagesList[i];

                    }
                    numPageFaultsOPT++;

                }

            }

        }

        //private int indexOfLowestRankItem(List<PPReference> VRList, int startIndex)
        //{
        //    if ((startIndex + 1) >= processPagesList.Count)
        //        return 500000;
        //}

        //-----------------------------------------------------    OPT END   ----------------------------------------//


        //-----------------------------------------------------    FIFO START   ----------------------------------------//
        public void RunFIFO()
        {
            Queue<PPReference> VRQueue = new Queue<PPReference>(numOfFrames);
            foreach (var listItem in processPagesList)
            {

                if (!isItemInQueue(VRQueue, listItem))
                {
                    numPageFaultsFIFO++;
                    if (VRQueue.Count == numOfFrames)
                        VRQueue.Dequeue();
                    VRQueue.Enqueue(listItem);
                }

            }

        }

        private bool isItemInQueue(Queue<PPReference> VRQueue, PPReference item)
        {
            foreach (var QueueItem in VRQueue)
            {
                if (item.isEqual(QueueItem))
                {
                    return true;
                }
            }
            return false;
        }


        //-----------------------------------------------------    FIFO END   ----------------------------------------//


    }
}