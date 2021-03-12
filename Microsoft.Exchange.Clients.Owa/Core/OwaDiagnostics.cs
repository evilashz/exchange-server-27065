using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000180 RID: 384
	public sealed class OwaDiagnostics
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x0005AFFB File Offset: 0x000591FB
		private OwaDiagnostics()
		{
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0005B003 File Offset: 0x00059203
		public static ExEventLog Logger
		{
			get
			{
				return OwaDiagnostics.logger;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0005B00A File Offset: 0x0005920A
		public static bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] eventLogParams)
		{
			return OwaDiagnostics.logger.LogEvent(tuple, periodicKey, eventLogParams);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0005B019 File Offset: 0x00059219
		public static bool LogEvent(ExEventLog.EventTuple tuple)
		{
			return OwaDiagnostics.logger.LogEvent(tuple, string.Empty, new object[0]);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0005B031 File Offset: 0x00059231
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string format, params object[] parameters)
		{
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0005B035 File Offset: 0x00059235
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0005B038 File Offset: 0x00059238
		public static void TracePfd(int lid, string message, params object[] parameters)
		{
			if (!ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.PfdTrace))
			{
				return;
			}
			ExTraceGlobals.CoreTracer.TracePfd((long)lid, string.Concat(new object[]
			{
				"PFD OWA ",
				lid,
				" - ",
				message
			}), parameters);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0005B08C File Offset: 0x0005928C
		internal static void CheckAndSetThreadTracing(string userDN)
		{
			if (userDN == null)
			{
				throw new ArgumentNullException("userDN");
			}
			ExTraceConfiguration instance = ExTraceConfiguration.Instance;
			lock (OwaDiagnostics.traceLock)
			{
				if (OwaDiagnostics.traceVersion != instance.Version)
				{
					OwaDiagnostics.traceVersion = instance.Version;
					OwaDiagnostics.traceUserTable = null;
					List<string> list = null;
					if (instance.CustomParameters.TryGetValue(OwaDiagnostics.userDNKeyName, out list) && list != null)
					{
						OwaDiagnostics.traceUserTable = new Hashtable(list.Count, StringComparer.OrdinalIgnoreCase);
						for (int i = 0; i < list.Count; i++)
						{
							OwaDiagnostics.traceUserTable[list[i]] = list[i];
						}
					}
				}
				if (instance.PerThreadTracingConfigured && OwaDiagnostics.traceUserTable != null && OwaDiagnostics.traceUserTable[userDN] != null)
				{
					BaseTrace.CurrentThreadSettings.EnableTracing();
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Enabled filtered tracing for user DN = '{0}'", userDN);
				}
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0005B188 File Offset: 0x00059388
		internal static void ClearThreadTracing()
		{
			if (BaseTrace.CurrentThreadSettings.IsEnabled)
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}

		// Token: 0x04000995 RID: 2453
		private static readonly string userDNKeyName = "UserLegacyDN";

		// Token: 0x04000996 RID: 2454
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.CoreTracer.Category, "MSExchange OWA");

		// Token: 0x04000997 RID: 2455
		private static int traceVersion = -1;

		// Token: 0x04000998 RID: 2456
		private static object traceLock = new object();

		// Token: 0x04000999 RID: 2457
		private static Hashtable traceUserTable;
	}
}
