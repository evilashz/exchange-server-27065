using System;

namespace Microsoft.Exchange.Diagnostics.Components.Inference.GroupingModel
{
	// Token: 0x02000362 RID: 866
	public static class ExTraceGlobals
	{
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00053A46 File Offset: 0x00051C46
		public static Trace RecommendedGroupsInfoWriterTracer
		{
			get
			{
				if (ExTraceGlobals.recommendedGroupsInfoWriterTracer == null)
				{
					ExTraceGlobals.recommendedGroupsInfoWriterTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.recommendedGroupsInfoWriterTracer;
			}
		}

		// Token: 0x04001923 RID: 6435
		private static Guid componentGuid = new Guid("C7C1EF44-5F48-42B9-868D-E25A72067991");

		// Token: 0x04001924 RID: 6436
		private static Trace recommendedGroupsInfoWriterTracer = null;
	}
}
