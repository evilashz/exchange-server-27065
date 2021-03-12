using System;
using System.Linq;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay.Dumpster
{
	// Token: 0x0200017A RID: 378
	internal class SafetyNetRegKeyStore : DisposeTrackableBase
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x00041DC7 File Offset: 0x0003FFC7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SafetyNetRegKeyStore>(this);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00041DD0 File Offset: 0x0003FFD0
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing && this.m_dbRegKeyHandle != null)
				{
					this.m_dbRegKeyHandle.Dispose();
					this.m_dbRegKeyHandle = null;
				}
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00041E24 File Offset: 0x00040024
		public SafetyNetRegKeyStore(string dbGuidStr, string dbName)
		{
			this.m_dbGuidStr = dbGuidStr;
			this.m_dbName = dbName;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00041E3C File Offset: 0x0004003C
		public string[] ReadRequestKeyNames()
		{
			this.EnsureOpen();
			return this.m_dbRegKeyHandle.GetValueNames(null).ToArray<string>();
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00041E64 File Offset: 0x00040064
		public SafetyNetInfo ReadRequestInfo(SafetyNetRequestKey requestKey, SafetyNetInfo prevInfo)
		{
			this.EnsureOpen();
			string valueName = requestKey.ToString();
			string text = this.ReadString(valueName);
			SafetyNetInfo safetyNetInfo = null;
			if (prevInfo != null && SharedHelper.StringIEquals(text, prevInfo.GetSerializedForm()))
			{
				safetyNetInfo = prevInfo;
			}
			if (safetyNetInfo == null && !string.IsNullOrEmpty(text))
			{
				safetyNetInfo = SafetyNetInfo.Deserialize(this.m_dbName, text, ExTraceGlobals.DumpsterTracer, true);
			}
			return safetyNetInfo;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00041EBC File Offset: 0x000400BC
		public void WriteRequestInfo(SafetyNetInfo info)
		{
			this.EnsureOpen();
			SafetyNetRequestKey safetyNetRequestKey = new SafetyNetRequestKey(info);
			string valueName = safetyNetRequestKey.ToString();
			string value = info.Serialize();
			this.WriteString(valueName, value);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00041EEC File Offset: 0x000400EC
		public void DeleteRequest(SafetyNetInfo info)
		{
			this.EnsureOpen();
			SafetyNetRequestKey safetyNetRequestKey = new SafetyNetRequestKey(info);
			string valueName = safetyNetRequestKey.ToString();
			this.DeleteValue(valueName);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00041F14 File Offset: 0x00040114
		private void EnsureOpen()
		{
			if (!this.m_open)
			{
				this.Open();
				this.m_open = true;
			}
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00041F2C File Offset: 0x0004012C
		private void Open()
		{
			AmClusterHandle amClusterHandle = null;
			AmSystemManager instance = AmSystemManager.Instance;
			if (instance != null)
			{
				AmConfig config = instance.Config;
				if (config != null)
				{
					AmDagConfig dagConfig = config.DagConfig;
					if (dagConfig != null)
					{
						IAmCluster cluster = dagConfig.Cluster;
						if (cluster != null)
						{
							amClusterHandle = cluster.Handle;
						}
					}
				}
			}
			if (amClusterHandle == null || amClusterHandle.IsInvalid)
			{
				throw new AmClusterNotRunningException();
			}
			using (IDistributedStoreKey clusterKey = DistributedStore.Instance.GetClusterKey(amClusterHandle, null, null, DxStoreKeyAccessMode.Write, false))
			{
				string keyName = string.Format("{0}\\SafetyNet2\\{1}", "ExchangeActiveManager", this.m_dbGuidStr);
				this.m_dbRegKeyHandle = clusterKey.OpenKey(keyName, DxStoreKeyAccessMode.CreateIfNotExist, false, null);
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00041FD8 File Offset: 0x000401D8
		private void WriteString(string valueName, string value)
		{
			this.EnsureOpen();
			this.m_dbRegKeyHandle.SetValue(valueName, value, false, null);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00041FF0 File Offset: 0x000401F0
		private string ReadString(string valueName)
		{
			this.EnsureOpen();
			return this.m_dbRegKeyHandle.GetValue(valueName, null, null);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00042013 File Offset: 0x00040213
		private void DeleteValue(string valueName)
		{
			this.EnsureOpen();
			this.m_dbRegKeyHandle.DeleteValue(valueName, true, null);
		}

		// Token: 0x04000649 RID: 1609
		private const string SafetyNetKeyFormatStr = "{0}\\SafetyNet2\\{1}";

		// Token: 0x0400064A RID: 1610
		private readonly string m_dbGuidStr;

		// Token: 0x0400064B RID: 1611
		private readonly string m_dbName;

		// Token: 0x0400064C RID: 1612
		private bool m_open;

		// Token: 0x0400064D RID: 1613
		private IDistributedStoreKey m_dbRegKeyHandle;
	}
}
