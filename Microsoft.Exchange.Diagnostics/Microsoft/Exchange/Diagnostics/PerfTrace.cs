using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A6 RID: 166
	public class PerfTrace : BaseTrace
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000EF89 File Offset: 0x0000D189
		internal PerfTrace(Guid componentGuid, int traceTag) : base(componentGuid, traceTag)
		{
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000EF94 File Offset: 0x0000D194
		public void TraceEvent(IPerfEventData perfEventData)
		{
			if (ETWTrace.IsEnabled && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				int num = 0;
				ETWTrace.WriteBinary(TraceType.PerformanceTrace, this.category, this.traceTag, perfEventData.ToBytes(), out num);
			}
		}
	}
}
