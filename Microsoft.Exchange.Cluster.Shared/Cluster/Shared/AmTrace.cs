using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200000A RID: 10
	internal static class AmTrace
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002E75 File Offset: 0x00001075
		internal static void Debug(string format, params object[] args)
		{
			ExTraceGlobals.ActiveManagerTracer.TraceDebug(0L, format, args);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E85 File Offset: 0x00001085
		internal static void Info(string format, params object[] args)
		{
			ExTraceGlobals.ActiveManagerTracer.TraceInformation(0, 0L, format, args);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E96 File Offset: 0x00001096
		internal static void Warning(string format, params object[] args)
		{
			ExTraceGlobals.ActiveManagerTracer.TraceWarning(0L, format, args);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EA6 File Offset: 0x000010A6
		internal static void Error(string format, params object[] args)
		{
			ExTraceGlobals.ActiveManagerTracer.TraceError(0L, format, args);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002EB8 File Offset: 0x000010B8
		internal static void Entering(string format, params object[] args)
		{
			string formatString = "Entering " + format;
			ExTraceGlobals.ActiveManagerTracer.TraceFunction(0L, formatString, args);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EE0 File Offset: 0x000010E0
		internal static void Leaving(string format, params object[] args)
		{
			string formatString = "Leaving " + format;
			ExTraceGlobals.ActiveManagerTracer.TraceFunction(0L, formatString, args);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F08 File Offset: 0x00001108
		internal static void Diagnostic(string format, params object[] args)
		{
			string text = string.Format(format, args);
			AmTrace.Debug(text, new object[0]);
			ReplayCrimsonEvents.GenericMessage.Log<string>(text);
		}
	}
}
