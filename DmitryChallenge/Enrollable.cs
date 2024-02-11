namespace DmitryChallenge
{
    public interface Enrollable
    {
        public bool Drop(Course course);

        public bool Enroll(Course course);
    }
}
