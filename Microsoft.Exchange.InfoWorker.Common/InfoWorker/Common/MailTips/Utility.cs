using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.MailTips;
using Microsoft.Win32;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x0200013B RID: 315
	public static class Utility
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x000251D5 File Offset: 0x000233D5
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x000251DC File Offset: 0x000233DC
		internal static TimeSpan RegistryCheckInterval
		{
			get
			{
				return Utility.registryCheckInterval;
			}
			set
			{
				Utility.nextRegistryCheck = DateTime.MinValue;
				Utility.registryCheckInterval = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x000251EE File Offset: 0x000233EE
		internal static bool RenderingDisabled
		{
			get
			{
				Utility.CheckDisableSwitch();
				return Utility.renderingDisabled;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000251FC File Offset: 0x000233FC
		internal static void CheckDisableSwitch()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow > Utility.nextRegistryCheck)
			{
				Utility.Tracer.TraceDebug(0L, "Checking disable-rendering switch");
				Utility.nextRegistryCheck = utcNow.Add(Utility.registryCheckInterval);
				Utility.renderingDisabled = Utility.IsDisabled("DisableGroupMetricsRendering");
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00025250 File Offset: 0x00023450
		private static bool IsDisabled(string name)
		{
			bool flag = false;
			try
			{
				object value = Registry.GetValue(Utility.RegistryKeyName, name, 0);
				Utility.Tracer.TraceDebug<string, object>(0L, "IsDisabled {0} raw value {1}", name, value);
				if (value is int && (int)value > 0)
				{
					flag = true;
				}
			}
			catch (IOException arg)
			{
				Utility.Tracer.TraceDebug<string, IOException>(0L, "IsDisabled {0} exception {1}", name, arg);
			}
			catch (SecurityException arg2)
			{
				Utility.Tracer.TraceDebug<string, SecurityException>(0L, "IsDisabled {0} exception {1}", name, arg2);
			}
			Utility.Tracer.TraceDebug<string, bool>(0L, "IsDisabled {0} result {1}", name, flag);
			return flag;
		}

		// Token: 0x040006AE RID: 1710
		internal const int RetryADLimit = 3;

		// Token: 0x040006AF RID: 1711
		internal const string DisableGroupMetricsRendering = "DisableGroupMetricsRendering";

		// Token: 0x040006B0 RID: 1712
		internal static readonly Trace Tracer = ExTraceGlobals.GroupMetricsTracer;

		// Token: 0x040006B1 RID: 1713
		internal static readonly ExEventLog Logger = new ExEventLog(Utility.Tracer.Category, "MSExchange MailTips");

		// Token: 0x040006B2 RID: 1714
		internal static string RegistryKeyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040006B3 RID: 1715
		private static TimeSpan registryCheckInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x040006B4 RID: 1716
		private static DateTime nextRegistryCheck = DateTime.MinValue;

		// Token: 0x040006B5 RID: 1717
		private static bool renderingDisabled = false;
	}
}
