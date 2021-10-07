using UnityEngine;

namespace Eatable
{
    internal sealed class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            ListControllers.Init();
            ListControllers.Add(new GameController());
        }

        private void Start()
        {
            ListControllers.Initialization();
        }

        private void Update()
        {
            ListControllers.Execute(Time.deltaTime);
        }

        private void LateUpdate()
        {
            ListControllers.LateExecute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            ListControllers.FixedExecute(Time.deltaTime);
        }
    }
}
