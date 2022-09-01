using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private ShowNHide _hider;

    private EcsWorld _world = null;

    private void Start()
    {
        _world = WorldHandler.GetWorld();

        _world.NewEntity().Get<DeathScreenComponent>().DeathScreen = this;
    }

    public void Show()
    {
        _hider.Show();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
