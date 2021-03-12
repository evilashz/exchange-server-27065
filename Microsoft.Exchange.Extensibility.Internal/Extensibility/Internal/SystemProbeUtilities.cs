using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000058 RID: 88
	internal static class SystemProbeUtilities
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00011162 File Offset: 0x0000F362
		public static Guid GetProbeGuid(MailItem mailItem)
		{
			if (mailItem == null)
			{
				return Guid.Empty;
			}
			return mailItem.SystemProbeId;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00011173 File Offset: 0x0000F373
		public static void InitForThread(MailItem mailItem)
		{
			SystemProbe.ActivityId = SystemProbeUtilities.GetProbeGuid(mailItem);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00011180 File Offset: 0x0000F380
		public static void TracePass(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string message)
		{
			tracer.TracePass(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, message);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00011190 File Offset: 0x0000F390
		public static void TracePass<T0>(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string formatString, T0 arg0)
		{
			tracer.TracePass<T0>(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, formatString, arg0);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000111A2 File Offset: 0x0000F3A2
		public static void TracePass<T0, T1>(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TracePass<T0, T1>(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, formatString, arg0, arg1);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000111B6 File Offset: 0x0000F3B6
		public static void TracePass<T0, T1, T2>(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TracePass<T0, T1, T2>(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000111CC File Offset: 0x0000F3CC
		public static void TracePass(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string formatString, params object[] args)
		{
			tracer.TracePass(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, formatString, args);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000111DE File Offset: 0x0000F3DE
		public static void TraceFail(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string message)
		{
			tracer.TraceFail(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, message);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000111EE File Offset: 0x0000F3EE
		public static void TraceFail<T0>(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string formatString, T0 arg0)
		{
			tracer.TraceFail<T0>(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, formatString, arg0);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00011200 File Offset: 0x0000F400
		public static void TraceFail<T0, T1>(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceFail<T0, T1>(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, formatString, arg0, arg1);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00011214 File Offset: 0x0000F414
		public static void TraceFail<T0, T1, T2>(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceFail<T0, T1, T2>(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001122A File Offset: 0x0000F42A
		public static void TraceFail(this SystemProbeTrace tracer, MailItem mailItem, long etlTraceId, string formatString, params object[] args)
		{
			tracer.TraceFail(SystemProbeUtilities.GetProbeGuid(mailItem), etlTraceId, formatString, args);
		}
	}
}
