using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000050 RID: 80
	internal static class Imap4Counters
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0001475F File Offset: 0x0001295F
		public static Imap4CountersInstance GetInstance(string instanceName)
		{
			return (Imap4CountersInstance)Imap4Counters.counters.GetInstance(instanceName);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00014771 File Offset: 0x00012971
		public static void CloseInstance(string instanceName)
		{
			Imap4Counters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001477E File Offset: 0x0001297E
		public static bool InstanceExists(string instanceName)
		{
			return Imap4Counters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001478B File Offset: 0x0001298B
		public static string[] GetInstanceNames()
		{
			return Imap4Counters.counters.GetInstanceNames();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00014797 File Offset: 0x00012997
		public static void RemoveInstance(string instanceName)
		{
			Imap4Counters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000147A4 File Offset: 0x000129A4
		public static void ResetInstance(string instanceName)
		{
			Imap4Counters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000147B1 File Offset: 0x000129B1
		public static void RemoveAllInstances()
		{
			Imap4Counters.counters.RemoveAllInstances();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000147BD File Offset: 0x000129BD
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new Imap4CountersInstance(instanceName, (Imap4CountersInstance)totalInstance);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000147CB File Offset: 0x000129CB
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new Imap4CountersInstance(instanceName);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000147D3 File Offset: 0x000129D3
		public static Imap4CountersInstance TotalInstance
		{
			get
			{
				return (Imap4CountersInstance)Imap4Counters.counters.TotalInstance;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000147E4 File Offset: 0x000129E4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (Imap4Counters.counters == null)
			{
				return;
			}
			Imap4Counters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000221 RID: 545
		public const string CategoryName = "MSExchangeImap4";

		// Token: 0x04000222 RID: 546
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeImap4", new CreateInstanceDelegate(Imap4Counters.CreateInstance), new CreateTotalInstanceDelegate(Imap4Counters.CreateTotalInstance));
	}
}
