using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200009B RID: 155
	internal sealed class InternalBypassTrace
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x0000E0A5 File Offset: 0x0000C2A5
		private InternalBypassTrace(Guid guid, int traceTag)
		{
			this.category = guid;
			this.traceTag = traceTag;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E0BB File Offset: 0x0000C2BB
		public void TraceDebug(int lid, long id, string formatString, params object[] args)
		{
			if (ETWTrace.IsInternalTraceEnabled)
			{
				ETWTrace.InternalWrite(lid, TraceType.DebugTrace, this.category, this.traceTag, id, string.Format(formatString, args));
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public void TraceError(int lid, long id, string formatString, params object[] args)
		{
			if (ETWTrace.IsInternalTraceEnabled)
			{
				ETWTrace.InternalWrite(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, string.Format(formatString, args));
			}
		}

		// Token: 0x04000323 RID: 803
		internal static readonly InternalBypassTrace TracingConfigurationTracer = new InternalBypassTrace(CommonTags.guid, 9);

		// Token: 0x04000324 RID: 804
		internal static readonly InternalBypassTrace FaultInjectionConfigurationTracer = new InternalBypassTrace(CommonTags.guid, 10);

		// Token: 0x04000325 RID: 805
		private readonly Guid category;

		// Token: 0x04000326 RID: 806
		private readonly int traceTag;
	}
}
