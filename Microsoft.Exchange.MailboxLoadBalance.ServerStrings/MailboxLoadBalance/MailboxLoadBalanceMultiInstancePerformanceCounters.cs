using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000023 RID: 35
	internal static class MailboxLoadBalanceMultiInstancePerformanceCounters
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003366 File Offset: 0x00001566
		public static MailboxLoadBalanceMultiInstancePerformanceCountersInstance GetInstance(string instanceName)
		{
			return (MailboxLoadBalanceMultiInstancePerformanceCountersInstance)MailboxLoadBalanceMultiInstancePerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003378 File Offset: 0x00001578
		public static void CloseInstance(string instanceName)
		{
			MailboxLoadBalanceMultiInstancePerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003385 File Offset: 0x00001585
		public static bool InstanceExists(string instanceName)
		{
			return MailboxLoadBalanceMultiInstancePerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003392 File Offset: 0x00001592
		public static string[] GetInstanceNames()
		{
			return MailboxLoadBalanceMultiInstancePerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000339E File Offset: 0x0000159E
		public static void RemoveInstance(string instanceName)
		{
			MailboxLoadBalanceMultiInstancePerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000033AB File Offset: 0x000015AB
		public static void ResetInstance(string instanceName)
		{
			MailboxLoadBalanceMultiInstancePerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000033B8 File Offset: 0x000015B8
		public static void RemoveAllInstances()
		{
			MailboxLoadBalanceMultiInstancePerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000033C4 File Offset: 0x000015C4
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MailboxLoadBalanceMultiInstancePerformanceCountersInstance(instanceName, (MailboxLoadBalanceMultiInstancePerformanceCountersInstance)totalInstance);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000033D2 File Offset: 0x000015D2
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MailboxLoadBalanceMultiInstancePerformanceCountersInstance(instanceName);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000033DA File Offset: 0x000015DA
		public static MailboxLoadBalanceMultiInstancePerformanceCountersInstance TotalInstance
		{
			get
			{
				return (MailboxLoadBalanceMultiInstancePerformanceCountersInstance)MailboxLoadBalanceMultiInstancePerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000033EB File Offset: 0x000015EB
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MailboxLoadBalanceMultiInstancePerformanceCounters.counters == null)
			{
				return;
			}
			MailboxLoadBalanceMultiInstancePerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400003D RID: 61
		public const string CategoryName = "MSExchange Mailbox Load Balancing Queues";

		// Token: 0x0400003E RID: 62
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Mailbox Load Balancing Queues", new CreateInstanceDelegate(MailboxLoadBalanceMultiInstancePerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(MailboxLoadBalanceMultiInstancePerformanceCounters.CreateTotalInstance));
	}
}
