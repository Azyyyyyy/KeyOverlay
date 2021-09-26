using Silk.NET.Input;
using Silk.NET.Windowing;

namespace KeyOverlay.Input
{
    public class GlobalInput : IInputPlatform
    {
        public bool IsApplicable(IView view) => true;

        public IInputContext CreateInput(IView view) => new GlobalInputContext();
    }
}