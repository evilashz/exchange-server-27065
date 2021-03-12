using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000D9 RID: 217
	internal class DagConfigurationStore : IDisposable
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00029110 File Offset: 0x00027310
		public static bool ClusterEventKeyNotificationMatchesNetworkConfig(string changedObjectName)
		{
			int num = string.Compare(changedObjectName, "Exchange\\DagNetwork", StringComparison.OrdinalIgnoreCase);
			return num == 0;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0002912E File Offset: 0x0002732E
		private AmClusterHandle ClusterHandle
		{
			get
			{
				return this.m_hCluster;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00029138 File Offset: 0x00027338
		public void Open(string targetServer)
		{
			this.m_hCluster = ClusapiMethods.OpenCluster(targetServer);
			if (this.m_hCluster == null || this.m_hCluster.IsInvalid)
			{
				Marshal.GetLastWin32Error();
				throw new Win32Exception();
			}
			using (IDistributedStoreKey clusterKey = DistributedStore.Instance.GetClusterKey(this.ClusterHandle, null, targetServer, DxStoreKeyAccessMode.Write, false))
			{
				this.m_regHandle = clusterKey.OpenKey("Exchange\\DagNetwork", DxStoreKeyAccessMode.CreateIfNotExist, false, null);
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000291B8 File Offset: 0x000273B8
		public void Open()
		{
			this.Open(null);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000291C4 File Offset: 0x000273C4
		public PersistentDagNetworkConfig LoadNetworkConfig(out string xmlText)
		{
			bool flag = false;
			xmlText = this.m_regHandle.GetValue("Configuration", null, out flag, null);
			if (flag)
			{
				return PersistentDagNetworkConfig.Deserialize(xmlText);
			}
			return null;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000291F8 File Offset: 0x000273F8
		public PersistentDagNetworkConfig LoadNetworkConfig()
		{
			string text = null;
			return this.LoadNetworkConfig(out text);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00029210 File Offset: 0x00027410
		public string StoreNetworkConfig(PersistentDagNetworkConfig cfg)
		{
			string text = cfg.Serialize();
			this.StoreNetworkConfig(text);
			return text;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002922C File Offset: 0x0002742C
		public void StoreNetworkConfig(string serializedConfig)
		{
			this.m_regHandle.SetValue("Configuration", serializedConfig, false, null);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00029244 File Offset: 0x00027444
		protected void CloseHandles()
		{
			if (this.m_regHandle != null)
			{
				this.m_regHandle.Dispose();
				this.m_regHandle = null;
			}
			if (this.m_hCluster != null && !this.m_hCluster.IsInvalid)
			{
				this.m_hCluster.Dispose();
				this.m_hCluster = null;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00029292 File Offset: 0x00027492
		public void Dispose()
		{
			if (!this.m_fDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000292AC File Offset: 0x000274AC
		protected void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_fDisposed)
				{
					if (disposing)
					{
						this.CloseHandles();
					}
					this.m_fDisposed = true;
				}
			}
		}

		// Token: 0x040003B7 RID: 951
		internal const string RootKeyName = "Exchange\\DagNetwork";

		// Token: 0x040003B8 RID: 952
		private const string NetConfigValueName = "Configuration";

		// Token: 0x040003B9 RID: 953
		private bool m_fDisposed;

		// Token: 0x040003BA RID: 954
		private IDistributedStoreKey m_regHandle;

		// Token: 0x040003BB RID: 955
		private AmClusterHandle m_hCluster;
	}
}
