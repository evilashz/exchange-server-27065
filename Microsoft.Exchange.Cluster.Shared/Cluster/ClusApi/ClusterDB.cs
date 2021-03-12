using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200001E RID: 30
	public class ClusterDB : DisposeTrackableBase, IClusterDB, IDisposable
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00005E49 File Offset: 0x00004049
		public static IClusterDB Open()
		{
			return ClusterDB.Open(AmServerName.LocalComputerName);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005E55 File Offset: 0x00004055
		internal static IClusterDB Open(AmServerName serverName)
		{
			return ClusterDB.hookableFactory.Value(serverName);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005E67 File Offset: 0x00004067
		internal static IDisposable SetTestHook(Func<AmServerName, IClusterDB> newFactory)
		{
			return ClusterDB.hookableFactory.SetTestHook(newFactory);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005E74 File Offset: 0x00004074
		private static IClusterDB Factory(AmServerName serverName)
		{
			return new ClusterDB(serverName);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005E7C File Offset: 0x0000407C
		private ClusterDB(AmServerName serverName)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<ClusterDB>(this);
				this.isInstalled = AmCluster.IsInstalled(serverName);
				if (AmCluster.IsRunning(serverName))
				{
					this.amCluster = AmCluster.OpenByName(serverName);
					this.rootHandle = DistributedStore.Instance.GetClusterKey(this.amCluster.Handle, null, serverName.Fqdn, DxStoreKeyAccessMode.Read, false);
					this.openWriteBatches = new List<ClusterDB.WriteBatch>(10);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005F18 File Offset: 0x00004118
		public bool IsInstalled
		{
			get
			{
				return this.isInstalled;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005F20 File Offset: 0x00004120
		public bool IsInitialized
		{
			get
			{
				return this.amCluster != null;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005F30 File Offset: 0x00004130
		public IEnumerable<string> GetSubKeyNames(string registryKey)
		{
			IEnumerable<string> subkeyNames;
			using (IDistributedStoreKey distributedStoreKey = this.OpenRegKey(registryKey, false, false))
			{
				subkeyNames = distributedStoreKey.GetSubkeyNames(null);
			}
			return subkeyNames;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005F6C File Offset: 0x0000416C
		public IEnumerable<Tuple<string, RegistryValueKind>> GetValueInfos(string registryKey)
		{
			IEnumerable<Tuple<string, RegistryValueKind>> valueInfos;
			using (IDistributedStoreKey distributedStoreKey = this.OpenRegKey(registryKey, false, false))
			{
				valueInfos = distributedStoreKey.GetValueInfos(null);
			}
			return valueInfos;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005FA8 File Offset: 0x000041A8
		public T GetValue<T>(string keyName, string valueName, T defaultValue)
		{
			T result;
			try
			{
				using (IDistributedStoreKey distributedStoreKey = this.OpenRegKey(keyName, false, false))
				{
					result = distributedStoreKey.GetValue(valueName, defaultValue, null);
				}
			}
			catch (ClusterApiException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005FF8 File Offset: 0x000041F8
		public void SetValue<T>(string keyName, string propertyName, T propetyValue)
		{
			using (IDistributedStoreKey distributedStoreKey = this.OpenRegKey(keyName, true, false))
			{
				distributedStoreKey.SetValue(propertyName, propetyValue, false, null);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006038 File Offset: 0x00004238
		public void DeleteValue(string keyName, string propertyName)
		{
			using (IDistributedStoreKey distributedStoreKey = this.OpenRegKey(keyName, false, true))
			{
				distributedStoreKey.DeleteValue(propertyName, true, null);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006078 File Offset: 0x00004278
		public IClusterDBWriteBatch CreateWriteBatch(string registryKey)
		{
			ClusterDB.WriteBatch writeBatch = new ClusterDB.WriteBatch(this, registryKey);
			this.openWriteBatches.Add(writeBatch);
			return writeBatch;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000609A File Offset: 0x0000429A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ClusterDB>(this);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000060A4 File Offset: 0x000042A4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.openWriteBatches != null)
				{
					this.openWriteBatches = null;
				}
				if (this.rootHandle != null)
				{
					this.rootHandle.Dispose();
					this.rootHandle = null;
				}
				if (this.amCluster != null)
				{
					this.amCluster.Dispose();
					this.amCluster = null;
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000060F7 File Offset: 0x000042F7
		private IDistributedStoreKey OpenRegKey(string keyName, bool createIfNotExists, bool ignoreIfNotExits = false)
		{
			return this.OpenRegKey(this.rootHandle, keyName, createIfNotExists, false);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006108 File Offset: 0x00004308
		private IDistributedStoreKey OpenRegKey(IDistributedStoreKey rootKey, string keyName, bool createIfNotExists, bool ignoreIfNotExits = false)
		{
			if (rootKey == null)
			{
				throw AmExceptionHelper.ConstructClusterApiException(5004, "OpenRegKey()", new object[0]);
			}
			if (!createIfNotExists)
			{
				return rootKey.OpenKey(keyName, DxStoreKeyAccessMode.Write, ignoreIfNotExits, null);
			}
			return rootKey.OpenKey(keyName, DxStoreKeyAccessMode.CreateIfNotExist, false, null);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000613C File Offset: 0x0000433C
		private void RemoveWriteBatch(ClusterDB.WriteBatch toRemove)
		{
			int num = this.openWriteBatches.IndexOf(toRemove);
			if (num >= 0)
			{
				this.openWriteBatches.RemoveAt(num);
			}
		}

		// Token: 0x04000047 RID: 71
		private static Hookable<Func<AmServerName, IClusterDB>> hookableFactory = Hookable<Func<AmServerName, IClusterDB>>.Create(false, new Func<AmServerName, IClusterDB>(ClusterDB.Factory));

		// Token: 0x04000048 RID: 72
		private readonly bool isInstalled;

		// Token: 0x04000049 RID: 73
		private AmCluster amCluster;

		// Token: 0x0400004A RID: 74
		private IDistributedStoreKey rootHandle;

		// Token: 0x0400004B RID: 75
		private List<ClusterDB.WriteBatch> openWriteBatches;

		// Token: 0x02000020 RID: 32
		private class WriteBatch : DisposeTrackableBase, IClusterDBWriteBatch, IDisposable
		{
			// Token: 0x0600012F RID: 303 RVA: 0x00006180 File Offset: 0x00004380
			public WriteBatch(ClusterDB parent, string registryKey)
			{
				this.parent = parent;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					disposeGuard.Add<ClusterDB.WriteBatch>(this);
					this.regKeyHandle = parent.OpenRegKey(registryKey, true, false);
					this.batchHandle = this.regKeyHandle.CreateBatchUpdateRequest();
					disposeGuard.Success();
				}
			}

			// Token: 0x06000130 RID: 304 RVA: 0x000061F4 File Offset: 0x000043F4
			public void CreateOrOpenKey(string keyName)
			{
				this.batchHandle.CreateKey(keyName);
			}

			// Token: 0x06000131 RID: 305 RVA: 0x00006202 File Offset: 0x00004402
			public void DeleteKey(string keyName)
			{
				this.batchHandle.DeleteKey(keyName);
			}

			// Token: 0x06000132 RID: 306 RVA: 0x00006210 File Offset: 0x00004410
			public void SetValue(string valueName, string value)
			{
				this.batchHandle.SetValue(valueName, value, RegistryValueKind.Unknown);
			}

			// Token: 0x06000133 RID: 307 RVA: 0x00006220 File Offset: 0x00004420
			public void SetValue(string valueName, IEnumerable<string> value)
			{
				this.batchHandle.SetValue(valueName, value.ToArray<string>(), RegistryValueKind.Unknown);
			}

			// Token: 0x06000134 RID: 308 RVA: 0x00006235 File Offset: 0x00004435
			public void SetValue(string valueName, int value)
			{
				this.batchHandle.SetValue(valueName, value, RegistryValueKind.Unknown);
			}

			// Token: 0x06000135 RID: 309 RVA: 0x0000624A File Offset: 0x0000444A
			public void SetValue(string valueName, long value)
			{
				this.batchHandle.SetValue(valueName, value, RegistryValueKind.Unknown);
			}

			// Token: 0x06000136 RID: 310 RVA: 0x0000625F File Offset: 0x0000445F
			public void DeleteValue(string valueName)
			{
				this.batchHandle.DeleteValue(valueName);
			}

			// Token: 0x06000137 RID: 311 RVA: 0x00006270 File Offset: 0x00004470
			public void Execute()
			{
				try
				{
					if (this.batchHandle != null)
					{
						try
						{
							this.batchHandle.Execute(null);
						}
						finally
						{
							try
							{
								this.batchHandle.Dispose();
							}
							finally
							{
								this.batchHandle = null;
							}
						}
					}
				}
				finally
				{
					this.parent.RemoveWriteBatch(this);
				}
			}

			// Token: 0x06000138 RID: 312 RVA: 0x000062E0 File Offset: 0x000044E0
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<ClusterDB.WriteBatch>(this);
			}

			// Token: 0x06000139 RID: 313 RVA: 0x000062E8 File Offset: 0x000044E8
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					try
					{
						if (this.batchHandle != null)
						{
							try
							{
								this.batchHandle.Dispose();
							}
							finally
							{
								this.batchHandle = null;
							}
						}
					}
					finally
					{
						if (this.regKeyHandle != null)
						{
							this.regKeyHandle.Dispose();
							this.regKeyHandle = null;
						}
						this.parent.RemoveWriteBatch(this);
					}
				}
			}

			// Token: 0x0400004C RID: 76
			private IDistributedStoreKey regKeyHandle;

			// Token: 0x0400004D RID: 77
			private IDistributedStoreBatchRequest batchHandle;

			// Token: 0x0400004E RID: 78
			private ClusterDB parent;
		}
	}
}
