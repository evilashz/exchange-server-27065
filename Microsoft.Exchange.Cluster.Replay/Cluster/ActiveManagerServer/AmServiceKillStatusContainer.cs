using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200006B RID: 107
	internal class AmServiceKillStatusContainer
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x000184C4 File Offset: 0x000166C4
		internal void NotifyServiceKillToOtherServers(string serviceName, ExDateTime timeInUtc)
		{
			AmLastKnownGoodConfig lastKnownGoodConfig = AmSystemManager.Instance.LastKnownGoodConfig;
			if (lastKnownGoodConfig != null && lastKnownGoodConfig.Role != AmRole.Unknown && lastKnownGoodConfig.Members != null)
			{
				timeInUtc.ToString("s");
				AmServerName[] members = lastKnownGoodConfig.Members;
				for (int i = 0; i < members.Length; i++)
				{
					AmServerName serverName = members[i];
					ThreadPool.QueueUserWorkItem(delegate(object param0)
					{
						AmServerName serverToRpc = serverName;
						AmServerName reportingServer = AmServerName.LocalComputerName;
						AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
						{
							AmRpcClientHelper.AmReportServiceKill(serverToRpc.NetbiosName, AmRpcClientHelper.RpcTimeoutShort, serviceName, reportingServer.NetbiosName, timeInUtc.ToString("u"));
						});
					});
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00018564 File Offset: 0x00016764
		internal void UpdateStatus(string serviceName, AmServerName serverName, ExDateTime timeStampInUtc)
		{
			lock (this.m_locker)
			{
				Dictionary<AmServerName, ExDateTime> dictionary = null;
				if (!this.m_killHistoryMap.TryGetValue(serviceName, out dictionary))
				{
					dictionary = new Dictionary<AmServerName, ExDateTime>();
					this.m_killHistoryMap[serviceName] = dictionary;
				}
				dictionary[serverName] = timeStampInUtc;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000185CC File Offset: 0x000167CC
		internal ExDateTime GetLastServiceKillTimeMax(string serviceName, out AmServerName serverName)
		{
			ExDateTime exDateTime = ExDateTime.MinValue;
			serverName = AmServerName.Empty;
			lock (this.m_locker)
			{
				Dictionary<AmServerName, ExDateTime> dictionary = null;
				if (this.m_killHistoryMap.TryGetValue(serviceName, out dictionary))
				{
					foreach (KeyValuePair<AmServerName, ExDateTime> keyValuePair in dictionary)
					{
						if (keyValuePair.Value > exDateTime)
						{
							serverName = keyValuePair.Key;
							exDateTime = keyValuePair.Value;
						}
					}
				}
			}
			return exDateTime;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00018680 File Offset: 0x00016880
		internal void Cleanup()
		{
			lock (this.m_locker)
			{
				foreach (KeyValuePair<string, Dictionary<AmServerName, ExDateTime>> keyValuePair in this.m_killHistoryMap)
				{
					if (keyValuePair.Value != null)
					{
						keyValuePair.Value.Clear();
					}
				}
				this.m_killHistoryMap.Clear();
			}
		}

		// Token: 0x040001F0 RID: 496
		private object m_locker = new object();

		// Token: 0x040001F1 RID: 497
		private Dictionary<string, Dictionary<AmServerName, ExDateTime>> m_killHistoryMap = new Dictionary<string, Dictionary<AmServerName, ExDateTime>>();
	}
}
