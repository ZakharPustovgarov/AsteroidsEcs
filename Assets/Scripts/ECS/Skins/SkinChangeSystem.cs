using Leopotam.Ecs;

sealed class SkinChangeSystem : IEcsRunSystem
{
    private readonly EcsFilter<SkinChangeEvent> _eventFilter = null;
    private readonly EcsFilter<SkinChangerComponent> _changersFilter = null;

    private GraphicType _graphicType = GraphicType.SPRITE;


    public void Run()
    {
        if (_eventFilter.IsEmpty() || _changersFilter.IsEmpty()) return;

        if(_graphicType == GraphicType.SPRITE)
        {
            _graphicType = GraphicType.POLY;
        }
        else if(_graphicType == GraphicType.POLY)
        {
            _graphicType = GraphicType.SPRITE;
        }

        foreach(var i in _changersFilter)
        {
            _changersFilter.Get1(i).SkinChanger.ChangeGraphic(_graphicType);
        }
    }
}

