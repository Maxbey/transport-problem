using transport_problem.Table;

namespace transport_problem.SolutionMethods
{
    public class VogelsDiff
    {
        public TableParentElement ParentElement;
        public int Diff;

        public VogelsDiff(TableParentElement p, int d)
        {
            ParentElement = p;
            Diff = d;
        }

        public TableParentElement GetParentElement()
        {
            return ParentElement;
        }

        public int GetDIff()
        {
            return Diff;
        }
    }
}