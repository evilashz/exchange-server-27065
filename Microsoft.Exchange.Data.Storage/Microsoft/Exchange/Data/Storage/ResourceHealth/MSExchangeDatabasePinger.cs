using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x020001B0 RID: 432
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MSExchangeDatabasePinger
	{
		// Token: 0x060017B7 RID: 6071 RVA: 0x000735D4 File Offset: 0x000717D4
		public static MSExchangeDatabasePingerInstance GetInstance(string instanceName)
		{
			return (MSExchangeDatabasePingerInstance)MSExchangeDatabasePinger.counters.GetInstance(instanceName);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000735E6 File Offset: 0x000717E6
		public static void CloseInstance(string instanceName)
		{
			MSExchangeDatabasePinger.counters.CloseInstance(instanceName);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000735F3 File Offset: 0x000717F3
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeDatabasePinger.counters.InstanceExists(instanceName);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00073600 File Offset: 0x00071800
		public static string[] GetInstanceNames()
		{
			return MSExchangeDatabasePinger.counters.GetInstanceNames();
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0007360C File Offset: 0x0007180C
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeDatabasePinger.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00073619 File Offset: 0x00071819
		public static void ResetInstance(string instanceName)
		{
			MSExchangeDatabasePinger.counters.ResetInstance(instanceName);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00073626 File Offset: 0x00071826
		public static void RemoveAllInstances()
		{
			MSExchangeDatabasePinger.counters.RemoveAllInstances();
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00073632 File Offset: 0x00071832
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeDatabasePingerInstance(instanceName, (MSExchangeDatabasePingerInstance)totalInstance);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00073640 File Offset: 0x00071840
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeDatabasePingerInstance(instanceName);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00073648 File Offset: 0x00071848
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeDatabasePinger.counters == null)
			{
				return;
			}
			MSExchangeDatabasePinger.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000C35 RID: 3125
		public const string CategoryName = "MSExchange Database Pinger";

		// Token: 0x04000C36 RID: 3126
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Database Pinger", new CreateInstanceDelegate(MSExchangeDatabasePinger.CreateInstance));
	}
}
