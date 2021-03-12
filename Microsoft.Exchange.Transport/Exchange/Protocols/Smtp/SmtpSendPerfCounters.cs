using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000546 RID: 1350
	internal static class SmtpSendPerfCounters
	{
		// Token: 0x06003E82 RID: 16002 RVA: 0x0010A5E0 File Offset: 0x001087E0
		public static SmtpSendPerfCountersInstance GetInstance(string instanceName)
		{
			return (SmtpSendPerfCountersInstance)SmtpSendPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0010A5F2 File Offset: 0x001087F2
		public static void CloseInstance(string instanceName)
		{
			SmtpSendPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x0010A5FF File Offset: 0x001087FF
		public static bool InstanceExists(string instanceName)
		{
			return SmtpSendPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x0010A60C File Offset: 0x0010880C
		public static string[] GetInstanceNames()
		{
			return SmtpSendPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x0010A618 File Offset: 0x00108818
		public static void RemoveInstance(string instanceName)
		{
			SmtpSendPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x0010A625 File Offset: 0x00108825
		public static void ResetInstance(string instanceName)
		{
			SmtpSendPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x0010A632 File Offset: 0x00108832
		public static void RemoveAllInstances()
		{
			SmtpSendPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x0010A63E File Offset: 0x0010883E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SmtpSendPerfCountersInstance(instanceName, (SmtpSendPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x0010A64C File Offset: 0x0010884C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SmtpSendPerfCountersInstance(instanceName);
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x0010A654 File Offset: 0x00108854
		public static void SetCategoryName(string categoryName)
		{
			if (SmtpSendPerfCounters.counters == null)
			{
				SmtpSendPerfCounters.CategoryName = categoryName;
				SmtpSendPerfCounters.counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal(SmtpSendPerfCounters.CategoryName, new CreateInstanceDelegate(SmtpSendPerfCounters.CreateInstance));
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x0010A67E File Offset: 0x0010887E
		public static SmtpSendPerfCountersInstance TotalInstance
		{
			get
			{
				return (SmtpSendPerfCountersInstance)SmtpSendPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x0010A68F File Offset: 0x0010888F
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SmtpSendPerfCounters.counters == null)
			{
				return;
			}
			SmtpSendPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400229A RID: 8858
		public static string CategoryName;

		// Token: 0x0400229B RID: 8859
		private static PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters;
	}
}
