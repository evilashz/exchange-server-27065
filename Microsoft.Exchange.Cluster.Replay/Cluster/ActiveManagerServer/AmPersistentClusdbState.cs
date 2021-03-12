using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000063 RID: 99
	internal class AmPersistentClusdbState : IDisposable
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x00016D00 File Offset: 0x00014F00
		internal AmPersistentClusdbState(IAmCluster cluster, string keyName)
		{
			using (IDistributedStoreKey clusterKey = DistributedStore.Instance.GetClusterKey(cluster.Handle, null, null, DxStoreKeyAccessMode.Write, false))
			{
				string keyName2 = "ExchangeActiveManager\\" + keyName;
				this.m_regHandle = clusterKey.OpenKey(keyName2, DxStoreKeyAccessMode.CreateIfNotExist, false, null);
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00016D60 File Offset: 0x00014F60
		internal T ReadProperty<T>(string propertyName, out bool doesEntryExist)
		{
			return this.m_regHandle.GetValue(propertyName, default(T), out doesEntryExist, null);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00016D86 File Offset: 0x00014F86
		internal void WriteProperty<T>(string propertyName, T propertyValue)
		{
			this.m_regHandle.SetValue(propertyName, propertyValue, false, null);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00016D98 File Offset: 0x00014F98
		internal void DeleteProperty(string propertyName)
		{
			this.m_regHandle.DeleteValue(propertyName, true, null);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00016DA9 File Offset: 0x00014FA9
		public void Dispose()
		{
			if (!this.m_fDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00016DC0 File Offset: 0x00014FC0
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_fDisposed)
				{
					if (disposing && this.m_regHandle != null)
					{
						this.m_regHandle.Dispose();
					}
					this.m_fDisposed = true;
				}
			}
		}

		// Token: 0x040001E1 RID: 481
		private bool m_fDisposed;

		// Token: 0x040001E2 RID: 482
		private IDistributedStoreKey m_regHandle;
	}
}
