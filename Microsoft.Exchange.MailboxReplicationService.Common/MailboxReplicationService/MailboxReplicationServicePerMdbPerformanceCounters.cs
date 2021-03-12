using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000391 RID: 913
	internal static class MailboxReplicationServicePerMdbPerformanceCounters
	{
		// Token: 0x0600278D RID: 10125 RVA: 0x000554F2 File Offset: 0x000536F2
		public static MailboxReplicationServicePerMdbPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (MailboxReplicationServicePerMdbPerformanceCountersInstance)MailboxReplicationServicePerMdbPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00055504 File Offset: 0x00053704
		public static void CloseInstance(string instanceName)
		{
			MailboxReplicationServicePerMdbPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x00055511 File Offset: 0x00053711
		public static bool InstanceExists(string instanceName)
		{
			return MailboxReplicationServicePerMdbPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x0005551E File Offset: 0x0005371E
		public static string[] GetInstanceNames()
		{
			return MailboxReplicationServicePerMdbPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x0005552A File Offset: 0x0005372A
		public static void RemoveInstance(string instanceName)
		{
			MailboxReplicationServicePerMdbPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x00055537 File Offset: 0x00053737
		public static void ResetInstance(string instanceName)
		{
			MailboxReplicationServicePerMdbPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00055544 File Offset: 0x00053744
		public static void RemoveAllInstances()
		{
			MailboxReplicationServicePerMdbPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00055550 File Offset: 0x00053750
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MailboxReplicationServicePerMdbPerformanceCountersInstance(instanceName, (MailboxReplicationServicePerMdbPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0005555E File Offset: 0x0005375E
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MailboxReplicationServicePerMdbPerformanceCountersInstance(instanceName);
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x00055566 File Offset: 0x00053766
		public static MailboxReplicationServicePerMdbPerformanceCountersInstance TotalInstance
		{
			get
			{
				return (MailboxReplicationServicePerMdbPerformanceCountersInstance)MailboxReplicationServicePerMdbPerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00055577 File Offset: 0x00053777
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MailboxReplicationServicePerMdbPerformanceCounters.counters == null)
			{
				return;
			}
			MailboxReplicationServicePerMdbPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040010C6 RID: 4294
		public const string CategoryName = "MSExchange Mailbox Replication Service Per Mdb";

		// Token: 0x040010C7 RID: 4295
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Mailbox Replication Service Per Mdb", new CreateInstanceDelegate(MailboxReplicationServicePerMdbPerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(MailboxReplicationServicePerMdbPerformanceCounters.CreateTotalInstance));
	}
}
