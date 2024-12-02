public class Breakable : Health
{
    internal override void Die()
    {
        Destroy(gameObject);
    }
}