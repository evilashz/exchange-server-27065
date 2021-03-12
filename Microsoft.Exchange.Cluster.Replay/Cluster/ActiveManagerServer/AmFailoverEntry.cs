using System;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000096 RID: 150
	internal class AmFailoverEntry
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		internal AmDbActionReason ReasonCode { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0001E4E5 File Offset: 0x0001C6E5
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0001E4ED File Offset: 0x0001C6ED
		internal AmServerName ServerName { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001E4F6 File Offset: 0x0001C6F6
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0001E4FE File Offset: 0x0001C6FE
		internal ExDateTime TimeCreated { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001E507 File Offset: 0x0001C707
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0001E50F File Offset: 0x0001C70F
		internal TimeSpan Delay { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0001E518 File Offset: 0x0001C718
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0001E520 File Offset: 0x0001C720
		internal Timer Timer { get; set; }

		// Token: 0x06000621 RID: 1569 RVA: 0x0001E529 File Offset: 0x0001C729
		private AmFailoverEntry()
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001E531 File Offset: 0x0001C731
		private static string GetPersistentStateKeyName(AmServerName serverName)
		{
			return "SystemState\\" + serverName.NetbiosName + "\\DeferredFailover";
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001E548 File Offset: 0x0001C748
		internal AmFailoverEntry(AmDbActionReason reasonCode, AmServerName serverName)
		{
			this.ReasonCode = reasonCode;
			this.ServerName = serverName;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001E57C File Offset: 0x0001C77C
		internal static AmFailoverEntry ReadFromPersistentStoreBestEffort(AmServerName serverName)
		{
			AmFailoverEntry entry = null;
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				entry = AmFailoverEntry.ReadFromPersistentStore(serverName);
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.TransientFailoverSuppressionPersistentStoreFailure.Log<string, AmServerName, string>("Read", serverName, ex.Message);
			}
			return entry;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001E5D4 File Offset: 0x0001C7D4
		internal static AmFailoverEntry ReadFromPersistentStore(AmServerName serverName)
		{
			AmFailoverEntry amFailoverEntry = null;
			using (AmPersistentClusdbState amPersistentClusdbState = new AmPersistentClusdbState(AmSystemManager.Instance.Config.DagConfig.Cluster, AmFailoverEntry.GetPersistentStateKeyName(serverName)))
			{
				bool flag = false;
				string text = amPersistentClusdbState.ReadProperty<string>("TimeCreated", out flag);
				if (string.IsNullOrEmpty(text))
				{
					text = ExDateTime.MinValue.ToString("o");
				}
				if (flag)
				{
					amFailoverEntry = new AmFailoverEntry();
					bool flag2;
					string value = amPersistentClusdbState.ReadProperty<string>("ReasonCode", out flag2);
					if (string.IsNullOrEmpty(value))
					{
						value = AmDbActionReason.NodeDown.ToString();
					}
					AmDbActionReason reasonCode;
					EnumUtility.TryParse<AmDbActionReason>(value, out reasonCode, AmDbActionReason.NodeDown, true);
					amFailoverEntry.ServerName = serverName;
					amFailoverEntry.TimeCreated = ExDateTime.Parse(ExTimeZone.CurrentTimeZone, text);
					amFailoverEntry.ReasonCode = reasonCode;
					amFailoverEntry.Delay = TimeSpan.FromSeconds((double)RegistryParameters.TransientFailoverSuppressionDelayInSec);
				}
			}
			return amFailoverEntry;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		internal void WriteToPersistentStoreBestEffort()
		{
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.WriteToPersistentStore();
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.TransientFailoverSuppressionPersistentStoreFailure.Log<string, AmServerName, string>("Write", this.ServerName, ex.Message);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001E700 File Offset: 0x0001C900
		internal void WriteToPersistentStore()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config != null && config.DagConfig != null)
			{
				using (AmPersistentClusdbState amPersistentClusdbState = new AmPersistentClusdbState(config.DagConfig.Cluster, AmFailoverEntry.GetPersistentStateKeyName(this.ServerName)))
				{
					amPersistentClusdbState.WriteProperty<string>("ReasonCode", this.ReasonCode.ToString());
					amPersistentClusdbState.WriteProperty<string>("TimeCreated", this.TimeCreated.ToString("o"));
					return;
				}
			}
			throw new AmServiceShuttingDownException();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001E7A4 File Offset: 0x0001C9A4
		internal void DeleteFromPersistentStoreBestEffort()
		{
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.DeleteFromPersistentStore();
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.TransientFailoverSuppressionPersistentStoreFailure.Log<string, AmServerName, string>("Delete", this.ServerName, ex.Message);
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
		internal void DeleteFromPersistentStore()
		{
			using (AmPersistentClusdbState amPersistentClusdbState = new AmPersistentClusdbState(AmSystemManager.Instance.Config.DagConfig.Cluster, AmFailoverEntry.GetPersistentStateKeyName(this.ServerName)))
			{
				amPersistentClusdbState.DeleteProperty("TimeCreated");
				amPersistentClusdbState.DeleteProperty("ReasonCode");
			}
		}
	}
}
