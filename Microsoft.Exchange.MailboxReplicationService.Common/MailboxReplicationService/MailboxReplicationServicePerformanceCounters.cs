using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000390 RID: 912
	internal static class MailboxReplicationServicePerformanceCounters
	{
		// Token: 0x0600278B RID: 10123 RVA: 0x00054CA4 File Offset: 0x00052EA4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MailboxReplicationServicePerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MailboxReplicationServicePerformanceCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04001094 RID: 4244
		public const string CategoryName = "MSExchange Mailbox Replication Service";

		// Token: 0x04001095 RID: 4245
		public static readonly ExPerformanceCounter UnreachableDatabases = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Unreachable Databases", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001096 RID: 4246
		public static readonly ExPerformanceCounter LastScanTime = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Last Scan Timestamp (UTC)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001097 RID: 4247
		public static readonly ExPerformanceCounter LastScanDuration = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Last Scan Duration (msec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001098 RID: 4248
		public static readonly ExPerformanceCounter ReadTransferRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Transfer Rate: Read (KB/sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001099 RID: 4249
		public static readonly ExPerformanceCounter ReadTransferRateBase = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Transfer Rate: Read (KB/sec) (base)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400109A RID: 4250
		public static readonly ExPerformanceCounter WriteTransferRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Transfer Rate: Write (KB/sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400109B RID: 4251
		public static readonly ExPerformanceCounter WriteTransferRateBase = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Transfer Rate: Write (KB/sec) (base)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400109C RID: 4252
		public static readonly ExPerformanceCounter MRSTransferRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Transfer Rate: Transmission (KB/sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400109D RID: 4253
		public static readonly ExPerformanceCounter MRSTransferRateBase = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Transfer Rate: Transmission (KB/sec) (base)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400109E RID: 4254
		public static readonly ExPerformanceCounter LocalCPUResourceHealthHiPri = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Resource Health: Local CPU (high priority)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400109F RID: 4255
		public static readonly ExPerformanceCounter LocalCPUDynamicCapacityHiPri = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Dynamic Capacity: Local CPU (high priority)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A0 RID: 4256
		public static readonly ExPerformanceCounter LocalCPUResourceHealthCustomerExpectation = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Resource Health: Local CPU (customer expectation)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A1 RID: 4257
		public static readonly ExPerformanceCounter LocalCPUDynamicCapacityCustomerExpectation = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Dynamic Capacity: Local CPU (customer expectation)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A2 RID: 4258
		public static readonly ExPerformanceCounter LocalCPUResourceHealthInternalMaintenance = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Resource Health: Local CPU (internal maintenance)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A3 RID: 4259
		public static readonly ExPerformanceCounter LocalCPUDynamicCapacityInternalMaintenance = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Dynamic Capacity: Local CPU (internal maintenance)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A4 RID: 4260
		public static readonly ExPerformanceCounter LocalCPUResourceHealth = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Resource Health: Local CPU", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A5 RID: 4261
		public static readonly ExPerformanceCounter LocalCPUDynamicCapacity = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Dynamic Capacity: Local CPU", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A6 RID: 4262
		public static readonly ExPerformanceCounter ADReplicationResourceHealthHiPri = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Resource Health: AD Replication (high priority)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A7 RID: 4263
		public static readonly ExPerformanceCounter ADReplicationDynamicCapacityHiPri = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Dynamic Capacity: AD Replication (high priority)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A8 RID: 4264
		public static readonly ExPerformanceCounter ADReplicationResourceHealthCustomerExpectation = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Resource Health: AD Replication (customer expectation)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010A9 RID: 4265
		public static readonly ExPerformanceCounter ADReplicationDynamicCapacityCustomerExpectation = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Dynamic Capacity: AD Replication (customer expectation)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010AA RID: 4266
		public static readonly ExPerformanceCounter ADReplicationResourceHealthInternalMaintenance = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Resource Health: AD Replication (internal maintenance)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010AB RID: 4267
		public static readonly ExPerformanceCounter ADReplicationDynamicCapacityInternalMaintenance = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Dynamic Capacity: AD Replication (internal maintenance)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010AC RID: 4268
		public static readonly ExPerformanceCounter ADReplicationResourceHealth = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Resource Health: AD Replication", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010AD RID: 4269
		public static readonly ExPerformanceCounter ADReplicationDynamicCapacity = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Dynamic Capacity: AD Replication", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010AE RID: 4270
		public static readonly ExPerformanceCounter UtilizationMRSHiPri = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: MRS jobs (high priority)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010AF RID: 4271
		public static readonly ExPerformanceCounter UtilizationMRSCustomerExpectation = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: MRS jobs (customer expectation)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B0 RID: 4272
		public static readonly ExPerformanceCounter UtilizationMRSInternalMaintenance = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: MRS jobs (internal maintenance)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B1 RID: 4273
		public static readonly ExPerformanceCounter UtilizationMRS = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: MRS jobs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B2 RID: 4274
		public static readonly ExPerformanceCounter UtilizationReadHiPri = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: Read jobs (high priority)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B3 RID: 4275
		public static readonly ExPerformanceCounter UtilizationReadCustomerExpectation = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: Read jobs (customer expectation)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B4 RID: 4276
		public static readonly ExPerformanceCounter UtilizationReadInternalMaintenance = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: Read jobs (internal maintenance)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B5 RID: 4277
		public static readonly ExPerformanceCounter UtilizationRead = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: Read jobs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B6 RID: 4278
		public static readonly ExPerformanceCounter UtilizationWriteHiPri = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: Write jobs (high priority)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B7 RID: 4279
		public static readonly ExPerformanceCounter UtilizationWriteCustomerExpectation = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: Write jobs (customer expectation)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B8 RID: 4280
		public static readonly ExPerformanceCounter UtilizationWriteInternalMaintenance = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: Write jobs (internal maintenance)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010B9 RID: 4281
		public static readonly ExPerformanceCounter UtilizationWrite = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Utilization: Write jobs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010BA RID: 4282
		public static readonly ExPerformanceCounter PSTReadBytesRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Read Bytes/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010BB RID: 4283
		public static readonly ExPerformanceCounter PSTWriteBytesRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Write Bytes/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010BC RID: 4284
		public static readonly ExPerformanceCounter PSTReadRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Read Operations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010BD RID: 4285
		public static readonly ExPerformanceCounter PSTWriteRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Write Operations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010BE RID: 4286
		public static readonly ExPerformanceCounter PSTReadMessageRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Read Messages/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010BF RID: 4287
		public static readonly ExPerformanceCounter PSTWriteMessageRate = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Write Messages/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010C0 RID: 4288
		public static readonly ExPerformanceCounter PSTHeapCacheBytes = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Heap Cache Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010C1 RID: 4289
		public static readonly ExPerformanceCounter PSTBlockBTreeCacheBytes = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Block BTree Cache Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010C2 RID: 4290
		public static readonly ExPerformanceCounter PSTNodeBTreeCacheBytes = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST Node BTree Cache Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010C3 RID: 4291
		public static readonly ExPerformanceCounter PSTAMapCacheBytes = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "PST AMap Cache Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010C4 RID: 4292
		public static readonly ExPerformanceCounter NamedPropertyCacheEntries = new ExPerformanceCounter("MSExchange Mailbox Replication Service", "Named Property Cache Entries.", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040010C5 RID: 4293
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MailboxReplicationServicePerformanceCounters.UnreachableDatabases,
			MailboxReplicationServicePerformanceCounters.LastScanTime,
			MailboxReplicationServicePerformanceCounters.LastScanDuration,
			MailboxReplicationServicePerformanceCounters.ReadTransferRate,
			MailboxReplicationServicePerformanceCounters.ReadTransferRateBase,
			MailboxReplicationServicePerformanceCounters.WriteTransferRate,
			MailboxReplicationServicePerformanceCounters.WriteTransferRateBase,
			MailboxReplicationServicePerformanceCounters.MRSTransferRate,
			MailboxReplicationServicePerformanceCounters.MRSTransferRateBase,
			MailboxReplicationServicePerformanceCounters.LocalCPUResourceHealthHiPri,
			MailboxReplicationServicePerformanceCounters.LocalCPUDynamicCapacityHiPri,
			MailboxReplicationServicePerformanceCounters.LocalCPUResourceHealthCustomerExpectation,
			MailboxReplicationServicePerformanceCounters.LocalCPUDynamicCapacityCustomerExpectation,
			MailboxReplicationServicePerformanceCounters.LocalCPUResourceHealthInternalMaintenance,
			MailboxReplicationServicePerformanceCounters.LocalCPUDynamicCapacityInternalMaintenance,
			MailboxReplicationServicePerformanceCounters.LocalCPUResourceHealth,
			MailboxReplicationServicePerformanceCounters.LocalCPUDynamicCapacity,
			MailboxReplicationServicePerformanceCounters.ADReplicationResourceHealthHiPri,
			MailboxReplicationServicePerformanceCounters.ADReplicationDynamicCapacityHiPri,
			MailboxReplicationServicePerformanceCounters.ADReplicationResourceHealthCustomerExpectation,
			MailboxReplicationServicePerformanceCounters.ADReplicationDynamicCapacityCustomerExpectation,
			MailboxReplicationServicePerformanceCounters.ADReplicationResourceHealthInternalMaintenance,
			MailboxReplicationServicePerformanceCounters.ADReplicationDynamicCapacityInternalMaintenance,
			MailboxReplicationServicePerformanceCounters.ADReplicationResourceHealth,
			MailboxReplicationServicePerformanceCounters.ADReplicationDynamicCapacity,
			MailboxReplicationServicePerformanceCounters.UtilizationMRSHiPri,
			MailboxReplicationServicePerformanceCounters.UtilizationMRSCustomerExpectation,
			MailboxReplicationServicePerformanceCounters.UtilizationMRSInternalMaintenance,
			MailboxReplicationServicePerformanceCounters.UtilizationMRS,
			MailboxReplicationServicePerformanceCounters.UtilizationReadHiPri,
			MailboxReplicationServicePerformanceCounters.UtilizationReadCustomerExpectation,
			MailboxReplicationServicePerformanceCounters.UtilizationReadInternalMaintenance,
			MailboxReplicationServicePerformanceCounters.UtilizationRead,
			MailboxReplicationServicePerformanceCounters.UtilizationWriteHiPri,
			MailboxReplicationServicePerformanceCounters.UtilizationWriteCustomerExpectation,
			MailboxReplicationServicePerformanceCounters.UtilizationWriteInternalMaintenance,
			MailboxReplicationServicePerformanceCounters.UtilizationWrite,
			MailboxReplicationServicePerformanceCounters.PSTReadBytesRate,
			MailboxReplicationServicePerformanceCounters.PSTWriteBytesRate,
			MailboxReplicationServicePerformanceCounters.PSTReadRate,
			MailboxReplicationServicePerformanceCounters.PSTWriteRate,
			MailboxReplicationServicePerformanceCounters.PSTReadMessageRate,
			MailboxReplicationServicePerformanceCounters.PSTWriteMessageRate,
			MailboxReplicationServicePerformanceCounters.PSTHeapCacheBytes,
			MailboxReplicationServicePerformanceCounters.PSTBlockBTreeCacheBytes,
			MailboxReplicationServicePerformanceCounters.PSTNodeBTreeCacheBytes,
			MailboxReplicationServicePerformanceCounters.PSTAMapCacheBytes,
			MailboxReplicationServicePerformanceCounters.NamedPropertyCacheEntries
		};
	}
}
