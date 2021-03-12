using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000048 RID: 72
	internal static class MSExchangeStoreDriverSubmissionDatabase
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000DF34 File Offset: 0x0000C134
		public static MSExchangeStoreDriverSubmissionDatabaseInstance GetInstance(string instanceName)
		{
			return (MSExchangeStoreDriverSubmissionDatabaseInstance)MSExchangeStoreDriverSubmissionDatabase.counters.GetInstance(instanceName);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000DF46 File Offset: 0x0000C146
		public static void CloseInstance(string instanceName)
		{
			MSExchangeStoreDriverSubmissionDatabase.counters.CloseInstance(instanceName);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000DF53 File Offset: 0x0000C153
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeStoreDriverSubmissionDatabase.counters.InstanceExists(instanceName);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000DF60 File Offset: 0x0000C160
		public static string[] GetInstanceNames()
		{
			return MSExchangeStoreDriverSubmissionDatabase.counters.GetInstanceNames();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeStoreDriverSubmissionDatabase.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000DF79 File Offset: 0x0000C179
		public static void ResetInstance(string instanceName)
		{
			MSExchangeStoreDriverSubmissionDatabase.counters.ResetInstance(instanceName);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000DF86 File Offset: 0x0000C186
		public static void RemoveAllInstances()
		{
			MSExchangeStoreDriverSubmissionDatabase.counters.RemoveAllInstances();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000DF92 File Offset: 0x0000C192
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeStoreDriverSubmissionDatabaseInstance(instanceName, (MSExchangeStoreDriverSubmissionDatabaseInstance)totalInstance);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeStoreDriverSubmissionDatabaseInstance(instanceName);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		public static MSExchangeStoreDriverSubmissionDatabaseInstance TotalInstance
		{
			get
			{
				return (MSExchangeStoreDriverSubmissionDatabaseInstance)MSExchangeStoreDriverSubmissionDatabase.counters.TotalInstance;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000DFB9 File Offset: 0x0000C1B9
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeStoreDriverSubmissionDatabase.counters == null)
			{
				return;
			}
			MSExchangeStoreDriverSubmissionDatabase.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000198 RID: 408
		public const string CategoryName = "MSExchange Submission Store Driver Database";

		// Token: 0x04000199 RID: 409
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Submission Store Driver Database", new CreateInstanceDelegate(MSExchangeStoreDriverSubmissionDatabase.CreateInstance), new CreateTotalInstanceDelegate(MSExchangeStoreDriverSubmissionDatabase.CreateTotalInstance));
	}
}
