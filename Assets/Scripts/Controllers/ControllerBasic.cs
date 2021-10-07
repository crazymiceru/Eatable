using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Eatable
{

    public abstract class ControllerBasic : IDisposable, IController, IControllerAddressable
    {
        #region Fields

        public event Action EvtAddressableCompleted;
        public event Action<IController> EvtNeedDestroy;
        protected int _numCfg = 0;

        public bool IsEndCreateAddressable => _isCreateAddressableComplete;

        protected List<GameObjectData> _gameObjects = new List<GameObjectData>();
        protected List<IController> _iControllers = new List<IController>();
        protected Dictionary<string, (AsyncOperationHandle handle, ScriptableObject cfg)> _dataCfg = new Dictionary<string, (AsyncOperationHandle, ScriptableObject)>();
        protected bool _isCreateAddressableComplete;

        private int _countCompleteAddressableObject;
        private List<AsyncOperationHandle<GameObject>> _addressableAll = new List<AsyncOperationHandle<GameObject>>();

         #endregion


        #region Util

        public ControllerBasic SetNumCfg(int numCfg)
        {
            _numCfg = numCfg;
            return this;
        }

        protected void NeedDestroy()
        {
            EvtNeedDestroy.Invoke(this);
        }


        protected IController AddController(IController controller)
        {
            _iControllers.Add(controller);
            ListControllers.Add(controller);
            if (controller is IControllerAddressable controllerAddressable)
            {
                _countCompleteAddressableObject++;
                controllerAddressable.EvtAddressableCompleted += ControllerAddressableCompleted;
            }
            return controller;
        }

        protected void DestroyController(IController controller)
        {
            ListControllers.Delete(controller);
            _iControllers.Remove(controller);

            if (controller is IDisposable disposeController)
                disposeController.Dispose();
        }

        private void ControllerAddressableCompleted()
        {
            if (--_countCompleteAddressableObject == 0)
            {
                EvtAddressableCompleted?.Invoke();
                EvtAddressableCompleted = null;
            }
        }

        public GameObjectData this[int index]
        {
            get => _gameObjects[index];
        }

        public void Dispose()
        {
            OnDispose();
            Clear();
            EvtAddressableCompleted = null;
        }

        protected void DestroyGameObject(GameObjectData gameObjectData)
        {
            if (!gameObjectData.isAddressable) Object.Destroy(gameObjectData.gameObject);
            else Addressables.ReleaseInstance(gameObjectData.gameObject);
            _gameObjects.Remove(gameObjectData);
        }

        protected void Clear()
        {
            foreach (var item in _addressableAll) Addressables.Release(item);
            _addressableAll.Clear();

            for (int i = 0; i < _iControllers.Count; i++)
            {
                ListControllers.Delete(_iControllers[i]);
                if (_iControllers[i] is IDisposable disposeController)
                    disposeController.Dispose();
            }
            _iControllers.Clear();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i].gameObject != null)
                {
                    //_gameObjects[i].gameObject.transform.DOComplete();
                    if (!_gameObjects[i].isAddressable) Object.Destroy(_gameObjects[i].gameObject);
                    else Addressables.ReleaseInstance(_gameObjects[i].gameObject);
                }
            }
            _gameObjects.Clear();

            foreach (var item in _dataCfg)
            {
                Addressables.Release(item.Value.handle);
            }
            _dataCfg.Clear();


        }

        protected virtual void OnDispose()
        {
        }

        #endregion


        #region Builds


        protected void CreateGameObjectAddressable(string key, Transform folder, Action<GameObjectData> evt = null)
        {
            key = key.Replace("##", $"{_numCfg}");
            var addressable = Addressables.InstantiateAsync(key, folder);
            SetCompleteAddressable(evt, addressable);
        }

        protected void CreateGameObjectAddressable(string key, Transform folder, Vector3 position, Quaternion rotation, Action<GameObjectData> evt = null)
        {
            key = key.Replace("##", $"{_numCfg}");
            var addressable = Addressables.InstantiateAsync(key, position, rotation, folder);
            SetCompleteAddressable(evt, addressable);
        }

        protected void CreateGameObjectAddressable(AssetReference assetReference, Transform folder, Action<GameObjectData> evt = null)
        {
            var addressable = Addressables.InstantiateAsync(assetReference, folder);
            SetCompleteAddressable(evt, addressable);
        }


        protected void CreateGameObjectAddressable(AssetReference assetReference, Transform folder, Vector3 position, Quaternion rotation, Action<GameObjectData> evt = null)
        {

            var addressable = Addressables.InstantiateAsync(assetReference, position, rotation, folder);
            SetCompleteAddressable(evt, addressable);
        }

        private void SetCompleteAddressable(Action<GameObjectData> evt, AsyncOperationHandle<GameObject> addressable)
        {
            _addressableAll.Add(addressable);
            _countCompleteAddressableObject++;
            var data = new GameObjectData();
            _gameObjects.Add(data);

            addressable.Completed += (obj) =>
            {
                _addressableAll.Remove(addressable);
                data.isAddressable = true;
                data.gameObject = obj.Result;
                DecCountForCompeteAddressable();
                evt?.Invoke(data);
            };
        }

        protected void LoadCfg(string key, Action<ScriptableObject> evt = null)
        {
            var addreessable = Addressables.LoadAssetAsync<ScriptableObject>(key);
            _countCompleteAddressableObject++;

            addreessable.Completed += (obj) =>
            {
                if (!_dataCfg.ContainsKey(key))
                    _dataCfg.Add(key, (addreessable, obj.Result));

                DecCountForCompeteAddressable();
                evt?.Invoke(obj.Result);
            };
        }

        private void DecCountForCompeteAddressable()
        {
            _countCompleteAddressableObject--;
            if (_countCompleteAddressableObject == 0)
            {
                EvtAddressableCompleted?.Invoke();
                EvtAddressableCompleted = null;
                _isCreateAddressableComplete = true;
            }
        }

        public ControllerBasic SetGameObject(GameObject gameObject)
        {
            var data = new GameObjectData();
            data.gameObject = gameObject;
            _gameObjects.Add(data);
            return this;
        }

        internal virtual ControllerBasic CreateControllers()
        {
            return this;
        }

        #endregion

    }
}
