using System.Collections.Generic;

namespace Process_Scheduling
{
    class Process
    {
        public int PID { get; set; }
        public int quantum { get; set; }
        public int burstTime { get; set; }
        public int quantumTime { get; set; }
        public Process parentProcess { get; set; }
        public List<Process> children { get; set; }
        public ProcessState.State processState { get; set; }
        public ProcessState.Action processAction { get; set; }

        public int waitID { get; set; }
        private const int NOT_WAITING = -5;

        public Process(int PID, int burstTime, int quantumTime, ProcessState.State state)
        {


            this.PID = PID;
            this.burstTime = burstTime;
            this.quantumTime = quantumTime;
            this.processState = state;
            this.quantum = quantumTime;
            this.children = new List<Process>();
            waitID = NOT_WAITING;

        }


        public void TurnPast()
        {
            burstTime--;
            quantumTime--;
        }

        public Process ProcessWait(int eventID)
        {
            waitID = eventID;
            processState = ProcessState.State.WAITING;
            return this;
        }

        public void RefreshQuantumTime()
        {
            quantumTime = quantum;
        }

        public void TerminateChildren()
        {
            children.Clear();
        }

    }
}
