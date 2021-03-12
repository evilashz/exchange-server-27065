using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000544 RID: 1348
	internal static class SmtpProxyPerfCounters
	{
		// Token: 0x06003E74 RID: 15988 RVA: 0x00109DC8 File Offset: 0x00107FC8
		public static SmtpProxyPerfCountersInstance GetInstance(string instanceName)
		{
			return (SmtpProxyPerfCountersInstance)SmtpProxyPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x00109DDA File Offset: 0x00107FDA
		public static void CloseInstance(string instanceName)
		{
			SmtpProxyPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x00109DE7 File Offset: 0x00107FE7
		public static bool InstanceExists(string instanceName)
		{
			return SmtpProxyPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x00109DF4 File Offset: 0x00107FF4
		public static string[] GetInstanceNames()
		{
			return SmtpProxyPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x00109E00 File Offset: 0x00108000
		public static void RemoveInstance(string instanceName)
		{
			SmtpProxyPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x00109E0D File Offset: 0x0010800D
		public static void ResetInstance(string instanceName)
		{
			SmtpProxyPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x00109E1A File Offset: 0x0010801A
		public static void RemoveAllInstances()
		{
			SmtpProxyPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00109E26 File Offset: 0x00108026
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SmtpProxyPerfCountersInstance(instanceName, (SmtpProxyPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x00109E34 File Offset: 0x00108034
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SmtpProxyPerfCountersInstance(instanceName);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x00109E3C File Offset: 0x0010803C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SmtpProxyPerfCounters.counters == null)
			{
				return;
			}
			SmtpProxyPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400228A RID: 8842
		public const string CategoryName = "MSExchangeFrontEndTransport Smtp Blind Proxy";

		// Token: 0x0400228B RID: 8843
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeFrontEndTransport Smtp Blind Proxy", new CreateInstanceDelegate(SmtpProxyPerfCounters.CreateInstance));
	}
}
