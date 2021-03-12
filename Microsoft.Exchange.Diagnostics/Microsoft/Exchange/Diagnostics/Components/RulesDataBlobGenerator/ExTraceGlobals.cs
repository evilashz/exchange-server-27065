using System;

namespace Microsoft.Exchange.Diagnostics.Components.RulesDataBlobGenerator
{
	// Token: 0x020003D4 RID: 980
	public static class ExTraceGlobals
	{
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00059E5D File Offset: 0x0005805D
		public static Trace RulesDataBlobGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.rulesDataBlobGeneratorTracer == null)
				{
					ExTraceGlobals.rulesDataBlobGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.rulesDataBlobGeneratorTracer;
			}
		}

		// Token: 0x04001C17 RID: 7191
		private static Guid componentGuid = new Guid("EBFB2854-BBAC-4746-9ABB-C8C0B7190D60");

		// Token: 0x04001C18 RID: 7192
		private static Trace rulesDataBlobGeneratorTracer = null;
	}
}
