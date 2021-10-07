using System;

namespace Eatable
{
    public sealed class ListControllers
    {

        #region Fields

        private static event Action _init;
        private static event Action<float> _execute;
        private static event Action<float> _fixedExecute;
        private static event Action<float> _lateExecute;
        private static bool isInitHasPassed = false;
        public static int countAddListControllers;

        #endregion


        #region Init

        public static void Init()
        {
            _init = delegate { };
            _execute = delegate { };
            _lateExecute = delegate { };
            _fixedExecute = delegate { };
            isInitHasPassed = false;
            countAddListControllers = 0;
        }

        #endregion


        #region Process

        public static void Execute(float deltaTime)
        {
            _execute(deltaTime);
        }

        internal static void FixedExecute(float deltaTime)
        {
            _fixedExecute(deltaTime);
        }

        public static void Initialization()
        {
            isInitHasPassed = true;
            _init();
        }

        public static void LateExecute(float deltaTime)
        {
            _lateExecute(deltaTime);
        }

        public static void Add(IController controller, string name = "")
        {
            countAddListControllers++;
            if (controller is IInitialization init)
            {
                _init += init.Initialization;
                if (isInitHasPassed) init.Initialization();
            }
            if (controller is IExecute execute)
            {
                _execute += execute.Execute;
            }
            if (controller is IFixedExecute fixedExecute)
            {
                _fixedExecute += fixedExecute.FixedExecute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute += lateExecute.LateExecute;
            }
        }

        public static void Delete(IController controller)
        {
            countAddListControllers--;
            if (controller is IInitialization init)
            {
                _init -= init.Initialization;
            }
            if (controller is IExecute execute)
            {
                _execute -= execute.Execute;
            }
            if (controller is IFixedExecute fixedExecute)
            {
                _fixedExecute -= fixedExecute.FixedExecute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute -= lateExecute.LateExecute;
            }
        }

        #endregion
    }
}
