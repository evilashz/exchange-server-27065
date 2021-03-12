using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000046 RID: 70
	internal static class MSExchangeStoreDriverSubmissionAgent
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000DC3F File Offset: 0x0000BE3F
		public static MSExchangeStoreDriverSubmissionAgentInstance GetInstance(string instanceName)
		{
			return (MSExchangeStoreDriverSubmissionAgentInstance)MSExchangeStoreDriverSubmissionAgent.counters.GetInstance(instanceName);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000DC51 File Offset: 0x0000BE51
		public static void CloseInstance(string instanceName)
		{
			MSExchangeStoreDriverSubmissionAgent.counters.CloseInstance(instanceName);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000DC5E File Offset: 0x0000BE5E
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeStoreDriverSubmissionAgent.counters.InstanceExists(instanceName);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000DC6B File Offset: 0x0000BE6B
		public static string[] GetInstanceNames()
		{
			return MSExchangeStoreDriverSubmissionAgent.counters.GetInstanceNames();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000DC77 File Offset: 0x0000BE77
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeStoreDriverSubmissionAgent.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000DC84 File Offset: 0x0000BE84
		public static void ResetInstance(string instanceName)
		{
			MSExchangeStoreDriverSubmissionAgent.counters.ResetInstance(instanceName);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000DC91 File Offset: 0x0000BE91
		public static void RemoveAllInstances()
		{
			MSExchangeStoreDriverSubmissionAgent.counters.RemoveAllInstances();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000DC9D File Offset: 0x0000BE9D
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeStoreDriverSubmissionAgentInstance(instanceName, (MSExchangeStoreDriverSubmissionAgentInstance)totalInstance);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000DCAB File Offset: 0x0000BEAB
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeStoreDriverSubmissionAgentInstance(instanceName);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000DCB3 File Offset: 0x0000BEB3
		public static MSExchangeStoreDriverSubmissionAgentInstance TotalInstance
		{
			get
			{
				return (MSExchangeStoreDriverSubmissionAgentInstance)MSExchangeStoreDriverSubmissionAgent.counters.TotalInstance;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeStoreDriverSubmissionAgent.counters == null)
			{
				return;
			}
			MSExchangeStoreDriverSubmissionAgent.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000195 RID: 405
		public const string CategoryName = "MSExchange Submission Store Driver Agents";

		// Token: 0x04000196 RID: 406
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Submission Store Driver Agents", new CreateInstanceDelegate(MSExchangeStoreDriverSubmissionAgent.CreateInstance), new CreateTotalInstanceDelegate(MSExchangeStoreDriverSubmissionAgent.CreateTotalInstance));
	}
}
