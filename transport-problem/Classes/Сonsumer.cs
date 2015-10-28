namespace transport_problem.Table
{
    public class Consumer
    {
        public Consumer(int req)
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