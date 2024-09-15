using PoolSystem.Alternative;

namespace Game
{
    public struct CMarkPool
    {
        public PoolContainer XPool;
        public PoolContainer OPool;

        public void Invoke(PoolContainer xPool, PoolContainer oPool)
        {
            XPool = xPool;
            OPool = oPool;
        }
    }
}