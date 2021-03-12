using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.DxStore.HA.Events;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000095 RID: 149
	public class DistributedStore
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x00014028 File Offset: 0x00012228
		public DistributedStore(string componentName)
		{
			this.IsRestartProcessOnDxStoreModeChange = true;
			this.StoreSettings = DataStoreSettings.GetStoreConfig();
			this.PerfTracker = new PerformanceTracker();
			if (this.StoreSettings.Primary == StoreKind.DxStore || this.StoreSettings.Shadow == StoreKind.DxStore)
			{
				this.DxStoreKeyFactoryInstance = new DxStoreKeyFactory(componentName, new Func<Exception, Exception>(this.ConstructClusterApiException), null, null, null, false);
			}
			this.BaseKeyGenerator = new Func<DxStoreKeyAccessMode, DistributedStore.Context, StoreKind, IDistributedStoreKey>(this.GetBaseKeyByStoreKind);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000140DC File Offset: 0x000122DC
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00014148 File Offset: 0x00012348
		public static DistributedStore Instance
		{
			get
			{
				if (DistributedStore.instance == null)
				{
					lock (DistributedStore.instanceLock)
					{
						if (DistributedStore.instance == null)
						{
							DistributedStore.instance = new DistributedStore("ExchangeHA");
							DistributedStore.instance.PerfTracker.Start();
						}
					}
				}
				return DistributedStore.instance;
			}
			set
			{
				DistributedStore.instance = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00014150 File Offset: 0x00012350
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00014158 File Offset: 0x00012358
		public bool IsRestartProcessOnDxStoreModeChange { get; set; }

		// Token: 0x06000553 RID: 1363 RVA: 0x00014164 File Offset: 0x00012364
		public Exception ConstructClusterApiException(Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (ex != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(ex.Message);
				ex = ex.InnerException;
			}
			return new ClusterApiException(stringBuilder.ToString());
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x000141AC File Offset: 0x000123AC
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x000141B4 File Offset: 0x000123B4
		public Func<DxStoreKeyAccessMode, DistributedStore.Context, StoreKind, IDistributedStoreKey> BaseKeyGenerator { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x000141BD File Offset: 0x000123BD
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x000141C5 File Offset: 0x000123C5
		public bool IsStopping { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000141CE File Offset: 0x000123CE
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x000141D6 File Offset: 0x000123D6
		public PerformanceTracker PerfTracker { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x000141DF File Offset: 0x000123DF
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x000141E7 File Offset: 0x000123E7
		public DataStoreSettings StoreSettings { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x000141F0 File Offset: 0x000123F0
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x000141F8 File Offset: 0x000123F8
		public DxStoreKeyFactory DxStoreKeyFactoryInstance { get; set; }

		// Token: 0x0600055E RID: 1374 RVA: 0x00014204 File Offset: 0x00012404
		public DistributedStore.Context ConstructContext(string nodeName)
		{
			return new DistributedStore.Context
			{
				ChannelFactory = null,
				ClusterHandle = ClusapiMethods.OpenCluster(nodeName),
				NodeName = nodeName
			};
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00014250 File Offset: 0x00012450
		public void ExecuteRequest(DistributedStoreKey key, OperationCategory operationCategory, OperationType operationType, string dbgInfo, Action<IDistributedStoreKey, bool, StoreKind> action)
		{
			this.ExecuteRequest<int>(key, operationCategory, operationType, dbgInfo, delegate(IDistributedStoreKey storeKey, bool isPrimary, StoreKind storeKind)
			{
				action(storeKey, isPrimary, storeKind);
				return 0;
			});
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001429C File Offset: 0x0001249C
		public T ExecuteRequest<T>(DistributedStoreKey key, OperationCategory operationCategory, OperationType operationType, string dbgInfo, Func<IDistributedStoreKey, T> func)
		{
			return this.ExecuteRequest<T>(key, operationCategory, operationType, dbgInfo, (IDistributedStoreKey storeKey, bool isPrimary, StoreKind storeKind) => func(storeKey));
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000142D0 File Offset: 0x000124D0
		public T ExecuteRequest<T>(DistributedStoreKey key, OperationCategory operationCategory, OperationType operationType, string dbgInfo, Func<IDistributedStoreKey, bool, StoreKind, T> func)
		{
			RequestInfo req = new RequestInfo(operationCategory, operationType, dbgInfo);
			this.EnqueueShadowAction<T>(key, req, func);
			return this.PerformAction<T>(key, req, true, func);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00014320 File Offset: 0x00012520
		public void TriggerDistributedStoreManagerRefreshBestEffort(string reason, bool isForceRefreshCache = true)
		{
			if (this.StoreSettings.Primary == StoreKind.DxStore || this.StoreSettings.Shadow == StoreKind.DxStore)
			{
				Exception ex = Utils.RunBestEffort(delegate
				{
					this.TriggerDistributedStoreManagerRefresh(reason, isForceRefreshCache);
				});
				if (ex != null)
				{
					DxStoreHACrimsonEvents.TriggerRefreshFailed.Log<string>(ex.Message);
				}
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00014390 File Offset: 0x00012590
		private void TriggerDistributedStoreManagerRefresh(string reason, bool isForceRefreshCache)
		{
			if (this.DxStoreKeyFactoryInstance != null)
			{
				if (this.managerClientFactory == null)
				{
					this.managerClientFactory = new ManagerClientFactory(this.DxStoreKeyFactoryInstance.ConfigProvider.ManagerConfig, null);
				}
				if (this.managerClientFactory != null)
				{
					DxStoreManagerClient localClient = this.managerClientFactory.LocalClient;
					localClient.TriggerRefresh(reason, isForceRefreshCache, null);
				}
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000143F0 File Offset: 0x000125F0
		internal bool IsKeyExist(string keyName, AmCluster cluster = null)
		{
			bool result = false;
			bool flag = false;
			if (cluster == null)
			{
				cluster = AmCluster.Open();
				flag = true;
			}
			try
			{
				using (IDistributedStoreKey clusterKey = this.GetClusterKey(cluster.Handle, null, null, DxStoreKeyAccessMode.Read, true))
				{
					if (clusterKey != null)
					{
						using (IDistributedStoreKey distributedStoreKey = clusterKey.OpenKey(keyName, DxStoreKeyAccessMode.Read, true, null))
						{
							result = (distributedStoreKey != null);
						}
					}
				}
			}
			finally
			{
				if (flag && cluster != null)
				{
					cluster.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00014484 File Offset: 0x00012684
		public void StartProcessRestartTimer()
		{
			if (this.restartTimer == null)
			{
				lock (this.restartTimerLock)
				{
					if (this.restartTimer == null)
					{
						DistributedStore.ProcessRestartTimer processRestartTimer = new DistributedStore.ProcessRestartTimer();
						processRestartTimer.Start();
						this.restartTimer = processRestartTimer;
					}
				}
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000144E8 File Offset: 0x000126E8
		public void StopProcessRestartTimer()
		{
			lock (this.restartTimerLock)
			{
				if (this.restartTimer != null)
				{
					this.restartTimer.Stop();
					this.restartTimer = null;
				}
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00014580 File Offset: 0x00012780
		public IDistributedStoreKey GetBaseKey(DxStoreKeyAccessMode mode, DistributedStore.Context context)
		{
			if (this.IsRestartProcessOnDxStoreModeChange)
			{
				this.StartProcessRestartTimer();
			}
			DistributedStoreKey compositeKey = new DistributedStoreKey(string.Empty, string.Empty, mode, context);
			compositeKey.IsBaseKey = true;
			try
			{
				this.ExecuteRequest(compositeKey, OperationCategory.GetBaseKey, OperationType.Read, string.Empty, delegate(IDistributedStoreKey key, bool isPrimary, StoreKind storeKind)
				{
					this.SetKeyByRole(compositeKey, isPrimary, this.BaseKeyGenerator(mode, context, storeKind));
				});
			}
			finally
			{
				if (compositeKey.PrimaryStoreKey == null)
				{
					compositeKey.CloseKey();
					compositeKey = null;
				}
			}
			return compositeKey;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00014644 File Offset: 0x00012844
		public void SetKeyByRole(DistributedStoreKey compositeKey, bool isPrimary, IDistributedStoreKey key)
		{
			if (isPrimary)
			{
				compositeKey.PrimaryStoreKey = key;
				return;
			}
			compositeKey.ShadowStoreKey = key;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00014658 File Offset: 0x00012858
		public IDistributedStoreKey GetBaseKeyByStoreKind(DxStoreKeyAccessMode mode, DistributedStore.Context context, StoreKind storeKind)
		{
			IDistributedStoreKey result;
			switch (storeKind)
			{
			case StoreKind.Clusdb:
				result = ClusterDbKey.GetBaseKey(context.ClusterHandle, mode);
				break;
			case StoreKind.DxStore:
				result = this.DxStoreKeyFactoryInstance.GetBaseKey(mode, context.ChannelFactory, context.NodeName, false);
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000146AC File Offset: 0x000128AC
		internal IDistributedStoreKey GetClusterKey(AmClusterHandle clusterHandle, CachedChannelFactory<IDxStoreAccess> channelFactory = null, string nodeName = null, DxStoreKeyAccessMode mode = DxStoreKeyAccessMode.Read, bool isBestEffort = false)
		{
			IDistributedStoreKey result = null;
			try
			{
				if (this.StoreSettings.IsCompositeModeEnabled || RegistryParameters.DistributedStoreIsLogPerformanceForSingleStore)
				{
					result = this.GetClusterCompositeKey(clusterHandle, channelFactory, nodeName, mode);
				}
				else
				{
					DistributedStore.Context context = new DistributedStore.Context
					{
						ClusterHandle = clusterHandle,
						ChannelFactory = channelFactory,
						NodeName = nodeName
					};
					result = this.GetBaseKeyByStoreKind(mode, context, this.StoreSettings.Primary);
				}
			}
			catch (ClusterException)
			{
				if (!isBestEffort)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001472C File Offset: 0x0001292C
		private IDistributedStoreKey GetClusterCompositeKey(AmClusterHandle clusterHandle, CachedChannelFactory<IDxStoreAccess> channelFactory = null, string nodeName = null, DxStoreKeyAccessMode mode = DxStoreKeyAccessMode.Read)
		{
			DistributedStore.Context context = new DistributedStore.Context
			{
				ClusterHandle = clusterHandle,
				ChannelFactory = channelFactory,
				NodeName = nodeName
			};
			return this.GetBaseKey(mode, context);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00014760 File Offset: 0x00012960
		private T PerformAction<T>(DistributedStoreKey key, RequestInfo req, bool isPrimary, Func<IDistributedStoreKey, bool, StoreKind, T> func)
		{
			IDistributedStoreKey arg = isPrimary ? key.PrimaryStoreKey : key.ShadowStoreKey;
			StoreKind storeKind = isPrimary ? this.StoreSettings.Primary : this.StoreSettings.Shadow;
			this.PerfTracker.UpdateStart(key, storeKind, isPrimary);
			Exception exception = null;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			T result;
			try
			{
				result = func(arg, isPrimary, storeKind);
			}
			catch (Exception ex)
			{
				exception = ex;
				throw;
			}
			finally
			{
				this.PerfTracker.UpdateFinish(key, storeKind, isPrimary, req, stopwatch.ElapsedMilliseconds, exception, false);
			}
			return result;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000148AC File Offset: 0x00012AAC
		private void EnqueueShadowAction<T>(DistributedStoreKey key, RequestInfo req, Func<IDistributedStoreKey, bool, StoreKind, T> func)
		{
			if (!this.StoreSettings.IsShadowConfigured)
			{
				return;
			}
			lock (this.shadowLock)
			{
				Queue<Action> queue;
				int maxAllowedLimit;
				if (req.OperationType == OperationType.Write)
				{
					queue = this.shadowWriteQueue;
					maxAllowedLimit = RegistryParameters.DistributedStoreShadowMaxAllowedWriteQueueLength;
				}
				else
				{
					maxAllowedLimit = RegistryParameters.DistributedStoreShadowMaxAllowedReadQueueLength;
					if (req.OperationCategory == OperationCategory.OpenKey)
					{
						queue = this.shadowOpenRequestsQueue;
					}
					else
					{
						queue = this.shadowReadQueue;
					}
				}
				if (this.EnsureQueueLengthInLimit(queue, maxAllowedLimit, key, req))
				{
					if (!req.IsGetBaseKeyRequest && !req.IsCloseKeyRequest && key.ShadowStoreKey == null)
					{
						queue.Enqueue(delegate
						{
							using (IDistributedStoreKey baseKeyByStoreKind = this.GetBaseKeyByStoreKind(DxStoreKeyAccessMode.Write, key.Context, this.StoreSettings.Shadow))
							{
								if (baseKeyByStoreKind != null)
								{
									key.ShadowStoreKey = baseKeyByStoreKind.OpenKey(key.FullKeyName, DxStoreKeyAccessMode.Write, false, null);
								}
							}
						});
					}
					queue.Enqueue(delegate
					{
						this.PerformAction<T>(key, req, false, func);
					});
					if (!this.isShadowActionExecuting)
					{
						this.isShadowActionExecuting = true;
						ThreadPool.QueueUserWorkItem(delegate(object o)
						{
							this.ExecuteShadowActions();
						});
					}
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00014A0C File Offset: 0x00012C0C
		private bool EnsureQueueLengthInLimit(Queue<Action> queue, int maxAllowedLimit, DistributedStoreKey key, RequestInfo req)
		{
			if (queue.Count < maxAllowedLimit)
			{
				return true;
			}
			this.PerfTracker.UpdateStart(key, this.StoreSettings.Shadow, false);
			this.PerfTracker.UpdateFinish(key, this.StoreSettings.Shadow, false, req, 0L, null, true);
			return false;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00014A5C File Offset: 0x00012C5C
		private void ExecuteShadowActions()
		{
			int num = 0;
			while (!this.IsStopping)
			{
				Action action = null;
				lock (this.shadowLock)
				{
					if (this.shadowWriteQueue.Count + this.shadowOpenRequestsQueue.Count + this.shadowReadQueue.Count == 0)
					{
						this.isShadowActionExecuting = false;
						break;
					}
					if (this.shadowOpenRequestsQueue.Count > 0)
					{
						action = this.shadowOpenRequestsQueue.Dequeue();
					}
					else if (this.shadowWriteQueue.Count > 0)
					{
						action = this.shadowWriteQueue.Dequeue();
					}
					else if (this.shadowReadQueue.Count > 0)
					{
						action = this.shadowReadQueue.Dequeue();
					}
				}
				this.AttemptBestEffort(action);
				if (++num % 5 == 0)
				{
					num = 0;
					Thread.Yield();
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00014B50 File Offset: 0x00012D50
		private bool AttemptBestEffort(Action action)
		{
			bool result = false;
			try
			{
				if (action != null)
				{
					action();
				}
				result = true;
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x040002F2 RID: 754
		public const string ExchangeHighAvailabilityComponentName = "ExchangeHA";

		// Token: 0x040002F3 RID: 755
		private static readonly object instanceLock = new object();

		// Token: 0x040002F4 RID: 756
		private readonly object shadowLock = new object();

		// Token: 0x040002F5 RID: 757
		private readonly Queue<Action> shadowReadQueue = new Queue<Action>();

		// Token: 0x040002F6 RID: 758
		private readonly Queue<Action> shadowOpenRequestsQueue = new Queue<Action>();

		// Token: 0x040002F7 RID: 759
		private readonly Queue<Action> shadowWriteQueue = new Queue<Action>();

		// Token: 0x040002F8 RID: 760
		private static DistributedStore instance;

		// Token: 0x040002F9 RID: 761
		private bool isShadowActionExecuting;

		// Token: 0x040002FA RID: 762
		private ManagerClientFactory managerClientFactory;

		// Token: 0x040002FB RID: 763
		private object restartTimerLock = new object();

		// Token: 0x040002FC RID: 764
		private volatile DistributedStore.ProcessRestartTimer restartTimer;

		// Token: 0x02000096 RID: 150
		public class DxStoreRegistryProviderWithVariantConfig : DxStoreRegistryConfigProvider
		{
			// Token: 0x06000573 RID: 1395 RVA: 0x00014B8C File Offset: 0x00012D8C
			public override CommonSettings UpdateDefaultCommonSettings(CommonSettings input)
			{
				IActiveManagerSettings settings = DxStoreSetting.Instance.GetSettings();
				input.IsUseHttpTransportForInstanceCommunication = settings.DxStoreIsUseHttpForInstanceCommunication;
				input.IsUseHttpTransportForClientCommunication = settings.DxStoreIsUseHttpForClientCommunication;
				input.IsUseBinarySerializerForClientCommunication = settings.DxStoreIsUseBinarySerializerForClientCommunication;
				input.IsUseEncryption = settings.DxStoreIsEncryptionEnabled;
				return input;
			}
		}

		// Token: 0x02000097 RID: 151
		internal class ProcessRestartTimer : TimerComponent
		{
			// Token: 0x06000575 RID: 1397
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

			// Token: 0x06000576 RID: 1398 RVA: 0x00014BDD File Offset: 0x00012DDD
			public ProcessRestartTimer() : base(TimeSpan.Zero, TimeSpan.FromSeconds(10.0), "Process restart timer")
			{
				this.initialMode = DxStoreSetting.Instance.GetSettings().DxStoreRunMode;
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x00014C14 File Offset: 0x00012E14
			protected override void TimerCallbackInternal()
			{
				DxStoreMode dxStoreRunMode = DxStoreSetting.Instance.GetSettings().DxStoreRunMode;
				if (dxStoreRunMode != this.initialMode)
				{
					Process.GetCurrentProcess();
					DistributedStore.ProcessRestartTimer.TerminateProcess(Process.GetCurrentProcess().Handle, 0U);
				}
			}

			// Token: 0x04000303 RID: 771
			private DxStoreMode initialMode;
		}

		// Token: 0x02000098 RID: 152
		public class Context
		{
			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000578 RID: 1400 RVA: 0x00014C51 File Offset: 0x00012E51
			// (set) Token: 0x06000579 RID: 1401 RVA: 0x00014C59 File Offset: 0x00012E59
			public CachedChannelFactory<IDxStoreAccess> ChannelFactory { get; set; }

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x00014C62 File Offset: 0x00012E62
			// (set) Token: 0x0600057B RID: 1403 RVA: 0x00014C6A File Offset: 0x00012E6A
			public string NodeName { get; set; }

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x0600057C RID: 1404 RVA: 0x00014C73 File Offset: 0x00012E73
			// (set) Token: 0x0600057D RID: 1405 RVA: 0x00014C7B File Offset: 0x00012E7B
			internal AmClusterHandle ClusterHandle { get; set; }
		}
	}
}
