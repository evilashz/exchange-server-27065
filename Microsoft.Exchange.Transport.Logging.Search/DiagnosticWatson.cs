using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000002 RID: 2
	internal class DiagnosticWatson
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private static KeyType TryReadRegistryKey<KeyType>(string value, KeyType defaultValue)
		{
			Exception ex = null;
			try
			{
				object value2 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\DeliveryReports", value, defaultValue);
				if (value2 == null || !(value2 is KeyType))
				{
					return defaultValue;
				}
				return (KeyType)((object)value2);
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.MessageTrackingTracer.TraceError<string, string, Exception>(0L, "Failed to read registry key: {0}\\{1}, {2}", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\DeliveryReports", value, ex);
			}
			return defaultValue;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002150 File Offset: 0x00000350
		public static void SendWatsonWithoutCrash(Exception e, string key, TimeSpan interval)
		{
			DiagnosticWatson.SendWatsonWithoutCrash(e, key, interval, null);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000215C File Offset: 0x0000035C
		public static void SendWatsonWithoutCrash(Exception e, string key, TimeSpan interval, string extraData)
		{
			if (DiagnosticWatson.reportNonFatalBugs == 0)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			lock (DiagnosticWatson.dumpLock)
			{
				DateTime dateTime;
				if (!DiagnosticWatson.watsonHistory.TryGetValue(key, out dateTime) || !(utcNow < dateTime))
				{
					ExWatson.SendReport(e, ReportOptions.None, extraData);
					dateTime = DateTime.UtcNow + interval;
					DiagnosticWatson.watsonHistory[key] = dateTime;
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const string DeliveryReportsRegkey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\DeliveryReports";

		// Token: 0x04000002 RID: 2
		private static object dumpLock = new object();

		// Token: 0x04000003 RID: 3
		private static Dictionary<string, DateTime> watsonHistory = new Dictionary<string, DateTime>(5);

		// Token: 0x04000004 RID: 4
		private static int reportNonFatalBugs = DiagnosticWatson.TryReadRegistryKey<int>("ReportNonFatalBugs", 0);
	}
}
