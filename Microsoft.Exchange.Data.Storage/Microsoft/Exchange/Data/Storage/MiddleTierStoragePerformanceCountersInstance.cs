using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001AF RID: 431
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MiddleTierStoragePerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060017B4 RID: 6068 RVA: 0x00072754 File Offset: 0x00070954
		internal MiddleTierStoragePerformanceCountersInstance(string instanceName, MiddleTierStoragePerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Middle-Tier Storage")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NamedPropertyCacheEntries = new ExPerformanceCounter(base.CategoryName, "Named Property cache entries.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NamedPropertyCacheEntries);
				this.NamedPropertyCacheMisses = new ExPerformanceCounter(base.CategoryName, "Named Property cache misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NamedPropertyCacheMisses);
				this.NamedPropertyCacheMisses_Base = new ExPerformanceCounter(base.CategoryName, "Base counter for Named Property cache misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NamedPropertyCacheMisses_Base);
				this.DumpsterSessionsActive = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Sessions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterSessionsActive);
				this.DumpsterDelegateSessionsActive = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Delegate Sessions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterDelegateSessionsActive);
				this.DumpsterADSettingCacheSize = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Directory Settings Cache Entries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterADSettingCacheSize);
				this.DumpsterADSettingRefreshRate = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Directory Settings Refresh/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterADSettingRefreshRate);
				this.DumpsterMoveItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Moved to Dumpster/sec.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterMoveItemsRate);
				this.DumpsterCopyItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Copied to Dumpster/sec.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterCopyItemsRate);
				this.DumpsterCalendarLogsRate = new ExPerformanceCounter(base.CategoryName, "Dumpster Calendar Log Entries/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterCalendarLogsRate);
				this.DumpsterDeletionsItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Dumpster Deletions/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterDeletionsItemsRate);
				this.DumpsterPurgesItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Dumpster Purges/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterPurgesItemsRate);
				this.DumpsterVersionsItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Dumpster Versions/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterVersionsItemsRate);
				this.DumpsterFolderEnumRate = new ExPerformanceCounter(base.CategoryName, "Folder Enumerations for Dumpster/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterFolderEnumRate);
				this.DumpsterForceCopyItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Forced Copied into Dumpster/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterForceCopyItemsRate);
				this.DumpsterMoveNoKeepItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Moved in Dumpster not Kept/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterMoveNoKeepItemsRate);
				this.DumpsterCopyNoKeepItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Copy in Dumpster not Kept/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterCopyNoKeepItemsRate);
				this.AuditFolderBindRate = new ExPerformanceCounter(base.CategoryName, "Audit records for folder bind/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AuditFolderBindRate);
				this.AuditGroupChangeRate = new ExPerformanceCounter(base.CategoryName, "Audit records for group change/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AuditGroupChangeRate);
				this.AuditItemChangeRate = new ExPerformanceCounter(base.CategoryName, "Audit records for item change/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AuditItemChangeRate);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Audits saved/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Average time for saving audits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalAuditSaveTime = new ExPerformanceCounter(base.CategoryName, "Total time for saving audits", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalAuditSaveTime);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Base of average time for saving audits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalAuditSave = new ExPerformanceCounter(base.CategoryName, "Total audits saved", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter,
					exPerformanceCounter3
				});
				list.Add(this.TotalAuditSave);
				this.DiscoveryCopyItemsRate = new ExPerformanceCounter(base.CategoryName, "Items copied to discovery mailbox/sec.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryCopyItemsRate);
				this.DiscoveryMailboxSearchesQueued = new ExPerformanceCounter(base.CategoryName, "Number of mailbox searches that are queued", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryMailboxSearchesQueued);
				this.DiscoveryMailboxSearchesActive = new ExPerformanceCounter(base.CategoryName, "Number of mailbox searches that are active", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryMailboxSearchesActive);
				this.DiscoveryMailboxSearchSourceMailboxesActive = new ExPerformanceCounter(base.CategoryName, "Number of mailboxes being searched", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryMailboxSearchSourceMailboxesActive);
				this.DumpsterVersionRollback = new ExPerformanceCounter(base.CategoryName, "Dumpster versions reverted on failure.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterVersionRollback);
				this.DumpsterADSettingCacheMisses = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Directory Settings cache misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterADSettingCacheMisses);
				this.DumpsterADSettingCacheMisses_Base = new ExPerformanceCounter(base.CategoryName, "Base counter for Dumpster Active Directory Settings cache misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterADSettingCacheMisses_Base);
				this.ActivityLogsActivityCount = new ExPerformanceCounter(base.CategoryName, "Activity Logger Activity Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsActivityCount);
				this.ActivityLogsSelectedForStore = new ExPerformanceCounter(base.CategoryName, "Activity Logger Selected Activities For Store", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsSelectedForStore);
				this.ActivityLogsStoreWriteExceptions = new ExPerformanceCounter(base.CategoryName, "Activity Logger Exception Count on Store Submit", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsStoreWriteExceptions);
				this.ActivityLogsFileWriteExceptions = new ExPerformanceCounter(base.CategoryName, "Activity Logger Exception Count on File Log", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsFileWriteExceptions);
				this.ActivityLogsFileWriteCount = new ExPerformanceCounter(base.CategoryName, "Activity Logger Activity Count written to File Log", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsFileWriteCount);
				long num = this.NamedPropertyCacheEntries.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter4 in list)
					{
						exPerformanceCounter4.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00072E40 File Offset: 0x00071040
		internal MiddleTierStoragePerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchange Middle-Tier Storage")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NamedPropertyCacheEntries = new ExPerformanceCounter(base.CategoryName, "Named Property cache entries.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NamedPropertyCacheEntries);
				this.NamedPropertyCacheMisses = new ExPerformanceCounter(base.CategoryName, "Named Property cache misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NamedPropertyCacheMisses);
				this.NamedPropertyCacheMisses_Base = new ExPerformanceCounter(base.CategoryName, "Base counter for Named Property cache misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NamedPropertyCacheMisses_Base);
				this.DumpsterSessionsActive = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Sessions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterSessionsActive);
				this.DumpsterDelegateSessionsActive = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Delegate Sessions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterDelegateSessionsActive);
				this.DumpsterADSettingCacheSize = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Directory Settings Cache Entries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterADSettingCacheSize);
				this.DumpsterADSettingRefreshRate = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Directory Settings Refresh/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterADSettingRefreshRate);
				this.DumpsterMoveItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Moved to Dumpster/sec.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterMoveItemsRate);
				this.DumpsterCopyItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Copied to Dumpster/sec.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterCopyItemsRate);
				this.DumpsterCalendarLogsRate = new ExPerformanceCounter(base.CategoryName, "Dumpster Calendar Log Entries/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterCalendarLogsRate);
				this.DumpsterDeletionsItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Dumpster Deletions/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterDeletionsItemsRate);
				this.DumpsterPurgesItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Dumpster Purges/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterPurgesItemsRate);
				this.DumpsterVersionsItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Dumpster Versions/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterVersionsItemsRate);
				this.DumpsterFolderEnumRate = new ExPerformanceCounter(base.CategoryName, "Folder Enumerations for Dumpster/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterFolderEnumRate);
				this.DumpsterForceCopyItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Forced Copied into Dumpster/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterForceCopyItemsRate);
				this.DumpsterMoveNoKeepItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Moved in Dumpster not Kept/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterMoveNoKeepItemsRate);
				this.DumpsterCopyNoKeepItemsRate = new ExPerformanceCounter(base.CategoryName, "Items Copy in Dumpster not Kept/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterCopyNoKeepItemsRate);
				this.AuditFolderBindRate = new ExPerformanceCounter(base.CategoryName, "Audit records for folder bind/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AuditFolderBindRate);
				this.AuditGroupChangeRate = new ExPerformanceCounter(base.CategoryName, "Audit records for group change/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AuditGroupChangeRate);
				this.AuditItemChangeRate = new ExPerformanceCounter(base.CategoryName, "Audit records for item change/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AuditItemChangeRate);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Audits saved/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Average time for saving audits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalAuditSaveTime = new ExPerformanceCounter(base.CategoryName, "Total time for saving audits", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalAuditSaveTime);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Base of average time for saving audits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalAuditSave = new ExPerformanceCounter(base.CategoryName, "Total audits saved", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter,
					exPerformanceCounter3
				});
				list.Add(this.TotalAuditSave);
				this.DiscoveryCopyItemsRate = new ExPerformanceCounter(base.CategoryName, "Items copied to discovery mailbox/sec.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryCopyItemsRate);
				this.DiscoveryMailboxSearchesQueued = new ExPerformanceCounter(base.CategoryName, "Number of mailbox searches that are queued", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryMailboxSearchesQueued);
				this.DiscoveryMailboxSearchesActive = new ExPerformanceCounter(base.CategoryName, "Number of mailbox searches that are active", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryMailboxSearchesActive);
				this.DiscoveryMailboxSearchSourceMailboxesActive = new ExPerformanceCounter(base.CategoryName, "Number of mailboxes being searched", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryMailboxSearchSourceMailboxesActive);
				this.DumpsterVersionRollback = new ExPerformanceCounter(base.CategoryName, "Dumpster versions reverted on failure.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterVersionRollback);
				this.DumpsterADSettingCacheMisses = new ExPerformanceCounter(base.CategoryName, "Dumpster Active Directory Settings cache misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterADSettingCacheMisses);
				this.DumpsterADSettingCacheMisses_Base = new ExPerformanceCounter(base.CategoryName, "Base counter for Dumpster Active Directory Settings cache misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DumpsterADSettingCacheMisses_Base);
				this.ActivityLogsActivityCount = new ExPerformanceCounter(base.CategoryName, "Activity Logger Activity Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsActivityCount);
				this.ActivityLogsSelectedForStore = new ExPerformanceCounter(base.CategoryName, "Activity Logger Selected Activities For Store", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsSelectedForStore);
				this.ActivityLogsStoreWriteExceptions = new ExPerformanceCounter(base.CategoryName, "Activity Logger Exception Count on Store Submit", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsStoreWriteExceptions);
				this.ActivityLogsFileWriteExceptions = new ExPerformanceCounter(base.CategoryName, "Activity Logger Exception Count on File Log", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsFileWriteExceptions);
				this.ActivityLogsFileWriteCount = new ExPerformanceCounter(base.CategoryName, "Activity Logger Activity Count written to File Log", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityLogsFileWriteCount);
				long num = this.NamedPropertyCacheEntries.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter4 in list)
					{
						exPerformanceCounter4.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0007352C File Offset: 0x0007172C
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000C13 RID: 3091
		public readonly ExPerformanceCounter NamedPropertyCacheEntries;

		// Token: 0x04000C14 RID: 3092
		public readonly ExPerformanceCounter NamedPropertyCacheMisses;

		// Token: 0x04000C15 RID: 3093
		public readonly ExPerformanceCounter NamedPropertyCacheMisses_Base;

		// Token: 0x04000C16 RID: 3094
		public readonly ExPerformanceCounter DumpsterSessionsActive;

		// Token: 0x04000C17 RID: 3095
		public readonly ExPerformanceCounter DumpsterDelegateSessionsActive;

		// Token: 0x04000C18 RID: 3096
		public readonly ExPerformanceCounter DumpsterADSettingCacheSize;

		// Token: 0x04000C19 RID: 3097
		public readonly ExPerformanceCounter DumpsterADSettingRefreshRate;

		// Token: 0x04000C1A RID: 3098
		public readonly ExPerformanceCounter DumpsterMoveItemsRate;

		// Token: 0x04000C1B RID: 3099
		public readonly ExPerformanceCounter DumpsterCopyItemsRate;

		// Token: 0x04000C1C RID: 3100
		public readonly ExPerformanceCounter DumpsterCalendarLogsRate;

		// Token: 0x04000C1D RID: 3101
		public readonly ExPerformanceCounter DumpsterDeletionsItemsRate;

		// Token: 0x04000C1E RID: 3102
		public readonly ExPerformanceCounter DumpsterPurgesItemsRate;

		// Token: 0x04000C1F RID: 3103
		public readonly ExPerformanceCounter DumpsterVersionsItemsRate;

		// Token: 0x04000C20 RID: 3104
		public readonly ExPerformanceCounter DumpsterFolderEnumRate;

		// Token: 0x04000C21 RID: 3105
		public readonly ExPerformanceCounter DumpsterForceCopyItemsRate;

		// Token: 0x04000C22 RID: 3106
		public readonly ExPerformanceCounter DumpsterMoveNoKeepItemsRate;

		// Token: 0x04000C23 RID: 3107
		public readonly ExPerformanceCounter DumpsterCopyNoKeepItemsRate;

		// Token: 0x04000C24 RID: 3108
		public readonly ExPerformanceCounter AuditFolderBindRate;

		// Token: 0x04000C25 RID: 3109
		public readonly ExPerformanceCounter AuditGroupChangeRate;

		// Token: 0x04000C26 RID: 3110
		public readonly ExPerformanceCounter AuditItemChangeRate;

		// Token: 0x04000C27 RID: 3111
		public readonly ExPerformanceCounter TotalAuditSave;

		// Token: 0x04000C28 RID: 3112
		public readonly ExPerformanceCounter TotalAuditSaveTime;

		// Token: 0x04000C29 RID: 3113
		public readonly ExPerformanceCounter DiscoveryCopyItemsRate;

		// Token: 0x04000C2A RID: 3114
		public readonly ExPerformanceCounter DiscoveryMailboxSearchesQueued;

		// Token: 0x04000C2B RID: 3115
		public readonly ExPerformanceCounter DiscoveryMailboxSearchesActive;

		// Token: 0x04000C2C RID: 3116
		public readonly ExPerformanceCounter DiscoveryMailboxSearchSourceMailboxesActive;

		// Token: 0x04000C2D RID: 3117
		public readonly ExPerformanceCounter DumpsterVersionRollback;

		// Token: 0x04000C2E RID: 3118
		public readonly ExPerformanceCounter DumpsterADSettingCacheMisses;

		// Token: 0x04000C2F RID: 3119
		public readonly ExPerformanceCounter DumpsterADSettingCacheMisses_Base;

		// Token: 0x04000C30 RID: 3120
		public readonly ExPerformanceCounter ActivityLogsActivityCount;

		// Token: 0x04000C31 RID: 3121
		public readonly ExPerformanceCounter ActivityLogsSelectedForStore;

		// Token: 0x04000C32 RID: 3122
		public readonly ExPerformanceCounter ActivityLogsStoreWriteExceptions;

		// Token: 0x04000C33 RID: 3123
		public readonly ExPerformanceCounter ActivityLogsFileWriteExceptions;

		// Token: 0x04000C34 RID: 3124
		public readonly ExPerformanceCounter ActivityLogsFileWriteCount;
	}
}
