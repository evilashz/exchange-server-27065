using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A4F RID: 2639
	internal static class MSExchangeResourceLoad
	{
		// Token: 0x06007895 RID: 30869 RVA: 0x0018FBB8 File Offset: 0x0018DDB8
		public static MSExchangeResourceLoadInstance GetInstance(string instanceName)
		{
			return (MSExchangeResourceLoadInstance)MSExchangeResourceLoad.counters.GetInstance(instanceName);
		}

		// Token: 0x06007896 RID: 30870 RVA: 0x0018FBCA File Offset: 0x0018DDCA
		public static void CloseInstance(string instanceName)
		{
			MSExchangeResourceLoad.counters.CloseInstance(instanceName);
		}

		// Token: 0x06007897 RID: 30871 RVA: 0x0018FBD7 File Offset: 0x0018DDD7
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeResourceLoad.counters.InstanceExists(instanceName);
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x0018FBE4 File Offset: 0x0018DDE4
		public static string[] GetInstanceNames()
		{
			return MSExchangeResourceLoad.counters.GetInstanceNames();
		}

		// Token: 0x06007899 RID: 30873 RVA: 0x0018FBF0 File Offset: 0x0018DDF0
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeResourceLoad.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600789A RID: 30874 RVA: 0x0018FBFD File Offset: 0x0018DDFD
		public static void ResetInstance(string instanceName)
		{
			MSExchangeResourceLoad.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x0018FC0A File Offset: 0x0018DE0A
		public static void RemoveAllInstances()
		{
			MSExchangeResourceLoad.counters.RemoveAllInstances();
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x0018FC16 File Offset: 0x0018DE16
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeResourceLoadInstance(instanceName, (MSExchangeResourceLoadInstance)totalInstance);
		}

		// Token: 0x0600789D RID: 30877 RVA: 0x0018FC24 File Offset: 0x0018DE24
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeResourceLoadInstance(instanceName);
		}

		// Token: 0x0600789E RID: 30878 RVA: 0x0018FC2C File Offset: 0x0018DE2C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeResourceLoad.counters == null)
			{
				return;
			}
			MSExchangeResourceLoad.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004F6E RID: 20334
		public const string CategoryName = "MSExchange Resource Load";

		// Token: 0x04004F6F RID: 20335
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Resource Load", new CreateInstanceDelegate(MSExchangeResourceLoad.CreateInstance));
	}
}
