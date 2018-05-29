namespace SharpyJson.Scripts.Modules.Queue
{
    public class RequestQueue
    {
        private static RequestQueue instance;

        private RequestQueue() { }

        public static RequestQueue get() {
            if (instance == null) {
                instance = new RequestQueue();
            }

            return instance;
        }
    }
}