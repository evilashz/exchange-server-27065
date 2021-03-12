using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000070 RID: 112
	internal static class MExCounters
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00011689 File Offset: 0x0000F889
		public static MExCountersInstance GetInstance(string instanceName)
		{
			return (MExCountersInstance)MExCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001169B File Offset: 0x0000F89B
		public static void CloseInstance(string instanceName)
		{
			MExCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public static bool InstanceExists(string instanceName)
		{
			return MExCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000116B5 File Offset: 0x0000F8B5
		public static string[] GetInstanceNames()
		{
			return MExCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000116C1 File Offset: 0x0000F8C1
		public static void RemoveInstance(string instanceName)
		{
			MExCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000116CE File Offset: 0x0000F8CE
		public static void ResetInstance(string instanceName)
		{
			MExCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000116DB File Offset: 0x0000F8DB
		public static void RemoveAllInstances()
		{
			MExCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000116E7 File Offset: 0x0000F8E7
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MExCountersInstance(instanceName, (MExCountersInstance)totalInstance);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000116F5 File Offset: 0x0000F8F5
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MExCountersInstance(instanceName);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000116FD File Offset: 0x0000F8FD
		public static void SetCategoryName(string categoryName)
		{
			if (MExCounters.counters == null)
			{
				MExCounters.CategoryName = categoryName;
				MExCounters.counters = new PerformanceCounterMultipleInstance(MExCounters.CategoryName, new CreateInstanceDelegate(MExCounters.CreateInstance));
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011727 File Offset: 0x0000F927
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MExCounters.counters == null)
			{
				return;
			}
			MExCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000447 RID: 1095
		public static string CategoryName;

		// Token: 0x04000448 RID: 1096
		private static PerformanceCounterMultipleInstance counters;
	}
}
