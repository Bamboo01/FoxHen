namespace Bamboo.Utility
{
    public sealed class CoroutineManager : Singleton<CoroutineManager> , IPreloadedObject
    {
        bool _isDoneLoading = false;
        public bool isDoneLoading => _isDoneLoading;

        protected override void OnAwake()
        {
            _persistent = true;
            _isDoneLoading = true;
        }
    }
}
    