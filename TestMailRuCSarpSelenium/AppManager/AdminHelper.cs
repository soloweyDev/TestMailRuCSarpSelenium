namespace UnitTest
{
    class AdminHelper : BaseHelper
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, string baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }
    }
}
