using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Performance
{
	// Token: 0x02000025 RID: 37
	internal static class MailboxOperators
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00009958 File Offset: 0x00007B58
		public static MailboxOperatorsInstance GetInstance(string instanceName)
		{
			return (MailboxOperatorsInstance)MailboxOperators.counters.GetInstance(instanceName);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000996A File Offset: 0x00007B6A
		public static void CloseInstance(string instanceName)
		{
			MailboxOperators.counters.CloseInstance(instanceName);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009977 File Offset: 0x00007B77
		public static bool InstanceExists(string instanceName)
		{
			return MailboxOperators.counters.InstanceExists(instanceName);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009984 File Offset: 0x00007B84
		public static string[] GetInstanceNames()
		{
			return MailboxOperators.counters.GetInstanceNames();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009990 File Offset: 0x00007B90
		public static void RemoveInstance(string instanceName)
		{
			MailboxOperators.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000999D File Offset: 0x00007B9D
		public static void ResetInstance(string instanceName)
		{
			MailboxOperators.counters.ResetInstance(instanceName);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000099AA File Offset: 0x00007BAA
		public static void RemoveAllInstances()
		{
			MailboxOperators.counters.RemoveAllInstances();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000099B6 File Offset: 0x00007BB6
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MailboxOperatorsInstance(instanceName, (MailboxOperatorsInstance)totalInstance);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000099C4 File Offset: 0x00007BC4
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MailboxOperatorsInstance(instanceName);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000099CC File Offset: 0x00007BCC
		public static MailboxOperatorsInstance TotalInstance
		{
			get
			{
				return (MailboxOperatorsInstance)MailboxOperators.counters.TotalInstance;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000099DD File Offset: 0x00007BDD
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MailboxOperators.counters == null)
			{
				return;
			}
			MailboxOperators.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000160 RID: 352
		public const string CategoryName = "MSExchangeSearch Mailbox Operators";

		// Token: 0x04000161 RID: 353
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeSearch Mailbox Operators", new CreateInstanceDelegate(MailboxOperators.CreateInstance), new CreateTotalInstanceDelegate(MailboxOperators.CreateTotalInstance));
	}
}
