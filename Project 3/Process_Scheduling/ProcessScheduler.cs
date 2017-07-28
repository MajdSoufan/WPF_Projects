using System;
using System.Collections.Generic;
using System.Windows;

namespace Process_Scheduling
{
    class ProcessScheduler
    {
        public Queue<Process> ReadyQueue { get; set; }
        public Queue<Process> WaitQueue { get; set; }
        public List<Process> processesList { get; set; }
        public List<string> inputList { get; set; }
        public List<string> outputList { get; set; }
        private int mainQuantam;
        private string outputFileName;
        private Process initProcess;

        public ProcessScheduler(int quantum, string outputFile)
        {
            initProcess = new Process(0, 1000000, quantum, ProcessState.State.RUNNING);
            ReadyQueue = new Queue<Process>();
            WaitQueue = new Queue<Process>();
            processesList = new List<Process>();
            inputList = new List<string>();
            outputList = new List<string>();
            mainQuantam = quantum;
            outputFileName = outputFile;
            processesList.Add(initProcess);
            outputList.Add("PID 0 running");
            outputList.Add("Ready Queue:");
            outputList.Add("Wait Queue:");

        }

        public void ProcessingInput()
        {
            foreach (string line in inputList)
            {
                try
                {
                    switch (line[0])
                    {
                        case 'C':
                            outputList.Add(line);
                            string[] subString = line.Split(' ');
                            int n = Int32.Parse(subString[1]);
                            int b = Int32.Parse(subString[2]);

                            Process newProcess = new Process(n, b, mainQuantam, ProcessState.State.NEW);
                            newProcess.parentProcess = getRunningProcess();
                            getRunningProcess().children.Add(newProcess);
                            processesList.Add(newProcess);
                            ReadyQueue.Enqueue(newProcess);
                            outputList.Add("PID " + newProcess.PID + " " + newProcess.burstTime + " placed on Ready Queue");

                            if (getRunningProcess().PID == 0)
                            {
                                getRunningProcess().processState = ProcessState.State.INIT;
                                newProcess.processState = ProcessState.State.RUNNING;
                                ReadyQueue.Dequeue();

                            }
                            else
                            {
                                getRunningProcess().TurnPast();
                                newProcess.processState = ProcessState.State.READY;
                                ReadyQueue.Enqueue(newProcess);

                            }
                            break;
                        case 'D':
                            outputList.Add(line);

                            string[] subString1 = line.Split(' ');
                            int n1 = Int32.Parse(subString1[1]);
                            break;
                        case 'I':
                            outputList.Add(line);
                            getRunningProcess().TurnPast();

                            break;
                        case 'W':
                            outputList.Add(line);

                            string[] subString2 = line.Split(' ');
                            int n2 = Int32.Parse(subString2[1]);

                            getRunningProcess().TurnPast();

                            if (getRunningProcess().burstTime != 0)
                            {
                                WaitQueue.Enqueue(getRunningProcess().ProcessWait(n2));
                                setNewRunningProcess().processState = ProcessState.State.RUNNING;
                            }

                            break;
                        case 'E':
                            outputList.Add(line);

                            string[] subString3 = line.Split(' ');
                            int n3 = Int32.Parse(subString3[1]);
                            break;
                        case 'X':
                            outputList.Add(line);
                            break;

                    }

                    if (getRunningProcess().burstTime == 0)
                    {
                        terminateRunningProcess();
                        setNewRunningProcess().processState = ProcessState.State.RUNNING;
                    }
                    else
                    {
                        if (getRunningProcess().quantumTime == 0)
                        {
                            getRunningProcess().RefreshQuantumTime();
                            Process oldRunningProcess = getRunningProcess();

                            if (ReadyQueue.Count == 0)
                            {
                                initProcess.processState = ProcessState.State.RUNNING;
                                oldRunningProcess.processState = ProcessState.State.READY;
                                ReadyQueue.Enqueue(oldRunningProcess);
                            }
                            else
                            {
                                ReadyQueue.Dequeue().processState = ProcessState.State.RUNNING;
                                oldRunningProcess.processState = ProcessState.State.READY;
                                ReadyQueue.Enqueue(oldRunningProcess);
                            }


                        }
                    }

                    if (getRunningProcess().PID != 0)
                        outputList.Add("PID " + getRunningProcess().PID + " " + getRunningProcess().burstTime + " running with " + getRunningProcess().quantumTime + " left");
                    else
                        outputList.Add("PID 0 running");

                    PrintQueueContent(ReadyQueue, "Ready");
                    PrintQueueContent(WaitQueue, "Wait");


                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message + "\nSChe Please  try again!!!");

                }


            }
            MessageBox.Show("HSHSHHS");

        }
        private Process getRunningProcess()
        {
            foreach (var process in processesList)
            {
                if (process.processState == ProcessState.State.RUNNING)
                {
                    return process;
                }
            }

            return null;
        }

        private Process setNewRunningProcess()
        {
            if (ReadyQueue.Count > 0)
                return ReadyQueue.Dequeue();
            else
                return initProcess;
        }

        private void terminateRunningProcess()
        {
            Queue<Process> newReadyQueue = new Queue<Process>();
            Queue<Process> newWaitQueue = new Queue<Process>();

            if (getRunningProcess().children.Count > 0)
            {
                
                    foreach (var process in ReadyQueue)
                    {
                        if (process.processState != ProcessState.State.TERMINATED)
                        {
                            newReadyQueue.Enqueue(process);

                        }

                    }
                    foreach (var process in WaitQueue)
                    {
                        if (process.processState != ProcessState.State.TERMINATED)
                        {
                            newWaitQueue.Enqueue(process);
                        }

                    }
                
                    foreach (var process in processesList)
                    {
                        if (process.processState != ProcessState.State.TERMINATED)
                        {
                            processesList.Remove(process);
                        }
                    }


                

                this.ReadyQueue = newReadyQueue;
                this.WaitQueue = newWaitQueue;


            }

            processesList.Remove(getRunningProcess());
            getRunningProcess().processState = ProcessState.State.TERMINATED;
        }

        private void PrintQueueContent(Queue<Process> queue, string state)
        {
            string queueLine = state + " Queue: ";



            foreach (Process process in queue)
            {
                if (state.Equals("Wait"))
                    queueLine += "PID " + process.PID + " " + process.burstTime + " " + process.waitID + " ";
                else
                    queueLine += "PID " + process.PID + " " + process.burstTime + " ";

            }

            outputList.Add(queueLine);
        }


    }
}
