#if CUKU_ASSETS
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Cuku.Assets
{
    /// <summary>
    /// Addressable assets utilities.
    /// </summary>
    public static class Extensions
    {
        #region Asset

        /// <summary>
        /// Load addressable assets of type <typeparamref name="T"/> and Name or Label <paramref name="key"/>.
        /// </summary>
        public static async UniTask<IList<T>> LoadAssets<T>(this object key)
            => await Addressables.LoadAssetsAsync<T>(key, null).ToUniTask();

        /// <summary>
        /// Load addressable asset of type <typeparamref name="T"/> and Name or Label as <paramref name="key"/>.
        /// </summary>
        /// <returns>Handle to use when releasing the asset and the Asset.</returns>
        public static async UniTask<KeyValuePair<AsyncOperationHandle, T>> LoadAsset<T>(this object key)
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            var asset = await handle.ToUniTask();
            return new KeyValuePair<AsyncOperationHandle, T>(handle, asset);
        }

        public static void UnloadAsset(this AsyncOperationHandle handle)
            => Addressables.Release(handle);

        #endregion

        #region GameObject

        /// <summary>
        /// InstantiateAsync <see cref="GameObject"/> from addressables with Name or Label <paramref name="key"/>.
        /// </summary>
        public static async UniTask<GameObject> InstantiateAsync(
            this object key,
            Transform parent = null,
            bool instantiateInWorldSpace = false,
            bool trackHandle = true)

            => await Addressables.InstantiateAsync(
                key,
                parent,
                instantiateInWorldSpace,
                trackHandle).ToUniTask();

        public static void UnloadInstance(this GameObject instance)
            => Addressables.ReleaseInstance(instance);

        #endregion

        #region Scene

        public static async UniTask<AsyncOperationHandle> LoadScene(
            this object key,
            LoadSceneMode loadMode = LoadSceneMode.Additive,
            bool activateOnLoad = true)
        {
            var handle = Addressables.LoadSceneAsync(key, loadMode, activateOnLoad);
            await handle.ToUniTask();
            return handle;
        }

        public static void UnloadScene(this AsyncOperationHandle handle)
            => Addressables.UnloadSceneAsync(handle);

        #endregion
    }
}
#endif
