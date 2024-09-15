using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class ResetTableSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsCustomInject<Map> _map;
        private EcsFilterInject<Inc<CMarkPool>> _cMarkPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var _ in _bus.Value.GetEventBodies<EResetTable>(out _))
            {
                var winRow = _bus.Value.GetEventBodySingleton<EWin>().MarkRow;
                var unnecessaryMarks = GetUnnecessaryMarks(winRow);
                _bus.Value.NewEvent(new EFadeOutMarks(winRow, unnecessaryMarks));
                _map.Value.Clear();
            }    
        }

        private MarkMb[] GetUnnecessaryMarks(MarkMb[] winRow)
        {
            var necessary = winRow.Select(m => m.gameObject).ToArray();
            var unnecessary = new List<MarkMb>();
            foreach (var entity in _cMarkPool.Value)
            {
                var markPools = _cMarkPool.Pools.Inc1.Get(entity);
                AddUnnecessaryMarks(markPools.XPool, necessary, unnecessary);
                AddUnnecessaryMarks(markPools.OPool, necessary, unnecessary);
            }
            return unnecessary.ToArray();
        }

        private void AddUnnecessaryMarks(Component pool, GameObject[] necessaryList, List<MarkMb> unnecessaryList)
        {
            foreach (var child in pool.gameObject.GetActiveChildren())
            {
                if (!necessaryList.Contains(child))
                    unnecessaryList.Add(child.GetComponent<MarkMb>());
            }
        }
    }
}