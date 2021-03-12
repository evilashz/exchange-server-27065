using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200001B RID: 27
	internal class AmClusterNodeStatusAccessor : IDisposable
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000068E0 File Offset: 0x00004AE0
		public AmClusterNodeStatusAccessor(IAmCluster cluster, AmServerName nodeName, DxStoreKeyAccessMode mode = DxStoreKeyAccessMode.Read)
		{
			this.ServerName = nodeName;
			string keyName = string.Format("{0}\\{1}", "ExchangeActiveManager\\NodeState", nodeName.NetbiosName);
			using (IDistributedStoreKey clusterKey = DistributedStore.Instance.GetClusterKey(cluster.Handle, null, null, mode, false))
			{
				this.distributedStoreKey = clusterKey.OpenKey(keyName, mode, false, null);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006954 File Offset: 0x00004B54
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterEventsTracer;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000695B File Offset: 0x00004B5B
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006963 File Offset: 0x00004B63
		public AmServerName ServerName { get; private set; }

		// Token: 0x060000F7 RID: 247 RVA: 0x0000696C File Offset: 0x00004B6C
		public static AmClusterNodeNetworkStatus Read(IAmCluster cluster, AmServerName srvName, out Exception ex)
		{
			ex = null;
			AmClusterNodeNetworkStatus amClusterNodeNetworkStatus = null;
			try
			{
				using (AmClusterNodeStatusAccessor amClusterNodeStatusAccessor = new AmClusterNodeStatusAccessor(cluster, srvName, DxStoreKeyAccessMode.Read))
				{
					amClusterNodeNetworkStatus = amClusterNodeStatusAccessor.Read();
				}
			}
			catch (SerializationException ex2)
			{
				ex = ex2;
			}
			catch (ClusterException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				AmClusterNodeStatusAccessor.Tracer.TraceError<AmServerName, Exception>(0L, "AmClusterNodeNetworkStatus.Read({0}) failed: {1}", srvName, ex);
			}
			else if (amClusterNodeNetworkStatus == null)
			{
				AmClusterNodeStatusAccessor.Tracer.TraceError<AmServerName>(0L, "AmClusterNodeNetworkStatus.Read({0}) No status has yet been published", srvName);
			}
			return amClusterNodeNetworkStatus;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006A00 File Offset: 0x00004C00
		public static Exception Write(IAmCluster cluster, AmServerName srvName, AmClusterNodeNetworkStatus status)
		{
			Exception ex = null;
			try
			{
				using (AmClusterNodeStatusAccessor amClusterNodeStatusAccessor = new AmClusterNodeStatusAccessor(cluster, srvName, DxStoreKeyAccessMode.CreateIfNotExist))
				{
					amClusterNodeStatusAccessor.Write(status);
				}
			}
			catch (SerializationException ex2)
			{
				ex = ex2;
			}
			catch (ClusterException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				AmClusterNodeStatusAccessor.Tracer.TraceError<AmServerName, AmClusterNodeNetworkStatus, Exception>(0L, "AmClusterNodeNetworkStatus.Write({0},{1}) failed: {2}", srvName, status, ex);
			}
			return ex;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006A78 File Offset: 0x00004C78
		public AmClusterNodeNetworkStatus Read()
		{
			string value = this.distributedStoreKey.GetValue("NetworkStatus", null, null);
			if (!string.IsNullOrEmpty(value))
			{
				return AmClusterNodeNetworkStatus.Deserialize(value);
			}
			return null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public void Write(AmClusterNodeNetworkStatus state)
		{
			state.LastUpdate = DateTime.UtcNow;
			string propertyValue = state.Serialize();
			this.distributedStoreKey.SetValue("NetworkStatus", propertyValue, false, null);
			AmClusterNodeStatusAccessor.Tracer.TraceDebug<AmServerName, AmClusterNodeNetworkStatus>(0L, "AmClusterNodeNetworkStatus.Write({0}):{1}", this.ServerName, state);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006AF3 File Offset: 0x00004CF3
		public void Dispose()
		{
			if (!this.m_fDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006B0C File Offset: 0x00004D0C
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_fDisposed)
				{
					if (disposing)
					{
						this.distributedStoreKey.Dispose();
					}
					this.m_fDisposed = true;
				}
			}
		}

		// Token: 0x04000065 RID: 101
		private const string RootKeyName = "ExchangeActiveManager\\NodeState";

		// Token: 0x04000066 RID: 102
		private const string ValueName = "NetworkStatus";

		// Token: 0x04000067 RID: 103
		private IDistributedStoreKey distributedStoreKey;

		// Token: 0x04000068 RID: 104
		private bool m_fDisposed;
	}
}
