namespace DmitryChallenge
{
    public abstract class UniversityMember
    {
        private static int _numberOfMembers;
        private int _memberId;
        private string _memberName;

        public UniversityMember(string memberName)
        {
            _memberId = ++_numberOfMembers;
            _memberName = memberName;
        }

        // refactor theese 3 methods
        public void PrintId()
        {
            Console.WriteLine(_memberId);
        }

        public void PrintName()
        {
            Console.WriteLine(_memberName);
        }

        public void PrintNumberOfMembers()
        {
            Console.WriteLine(_numberOfMembers);
        }
    }
}
