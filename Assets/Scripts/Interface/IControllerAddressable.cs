using System;

namespace Eatable
{
    public interface IControllerAddressable
    {
        event Action EvtAddressableCompleted;
    }
}