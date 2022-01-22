namespace FoxHen {
    internal sealed class SlowTrap: AbstractTrap {

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            collision.gameObject.GetComponent<PlayerAttributes>()?.AddStatus(Status.slowed);
        }
    }
}