namespace Bamboo.Utility
{
    public sealed class CoroutineManager : Singleton<CoroutineManager>
    {
        private void Awake()
        {
            _persistent = true;
        }
    }
}
