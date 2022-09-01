public class Laser : DamagingObject
{
    public override void Spawn()
    {
        gameObject.SetActive(true);
        base.Spawn();
    }
}
