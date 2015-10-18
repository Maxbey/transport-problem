namespace transport_problem.Classes
{
    public class Сonsumer
    {
        public Сonsumer(int req)
        {
            SetRequirement(req);
        }

        private int requirement;

        public int GetRequirement()
        {
            return requirement;
        }

        public void SetRequirement(int req)
        {
            requirement = req;
        }
    }
}