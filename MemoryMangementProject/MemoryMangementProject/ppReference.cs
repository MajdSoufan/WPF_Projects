namespace MemoryMangementProject
{
    class PPReference
    {
        public int PID;
        public int pageRef;

        public PPReference()
        {
        }

        public PPReference(int processID, int pageReferenceNum)
        {
            PID = processID;
            pageRef = pageReferenceNum;
        }

        public bool isEqual(PPReference other)
        {
            return ((this.PID == other.PID) && (this.pageRef == other.pageRef));
        }
    }
}