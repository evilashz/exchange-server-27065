using System;
using System.IO;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A4 RID: 164
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NullTracer : ITracer
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0000EE20 File Offset: 0x0000D020
		private NullTracer()
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000EE28 File Offset: 0x0000D028
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000EE2A File Offset: 0x0000D02A
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000EE2C File Offset: 0x0000D02C
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000EE2E File Offset: 0x0000D02E
		public void TraceDebug(long id, string formatString, params object[] args)
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000EE30 File Offset: 0x0000D030
		public void TraceDebug(long id, string message)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000EE32 File Offset: 0x0000D032
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000EE34 File Offset: 0x0000D034
		public void TraceWarning(long id, string message)
		{
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000EE36 File Offset: 0x0000D036
		public void TraceWarning(long id, string formatString, params object[] args)
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000EE38 File Offset: 0x0000D038
		public void TraceError(long id, string message)
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000EE3A File Offset: 0x0000D03A
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000EE3C File Offset: 0x0000D03C
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000EE3E File Offset: 0x0000D03E
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000EE40 File Offset: 0x0000D040
		public void TraceError(long id, string formatString, params object[] args)
		{
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000EE42 File Offset: 0x0000D042
		public void TracePerformance(long id, string message)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000EE44 File Offset: 0x0000D044
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000EE46 File Offset: 0x0000D046
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000EE48 File Offset: 0x0000D048
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000EE4A File Offset: 0x0000D04A
		public void TracePerformance(long id, string formatString, params object[] args)
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000EE4E File Offset: 0x0000D04E
		public ITracer Compose(ITracer other)
		{
			return other;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000EE51 File Offset: 0x0000D051
		public bool IsTraceEnabled(TraceType traceType)
		{
			return false;
		}

		// Token: 0x04000338 RID: 824
		public static readonly NullTracer Instance = new NullTracer();
	}
}
