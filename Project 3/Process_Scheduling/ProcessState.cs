namespace Process_Scheduling
{
    public class ProcessState
    {
        public enum State { NEW, READY, RUNNING, WAITING, TERMINATED, INIT };
        public enum Action { admitted, dispatch, interrupt, IO_EventCompletion, IO_EventWait, Exit };

    }

}
