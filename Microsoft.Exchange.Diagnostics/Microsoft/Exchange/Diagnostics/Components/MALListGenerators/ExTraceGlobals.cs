using System;

namespace Microsoft.Exchange.Diagnostics.Components.MALListGenerators
{
	// Token: 0x020003CE RID: 974
	public static class ExTraceGlobals
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00059CB3 File Offset: 0x00057EB3
		public static Trace MALListGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.mALListGeneratorTracer == null)
				{
					ExTraceGlobals.mALListGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.mALListGeneratorTracer;
			}
		}

		// Token: 0x04001C08 RID: 7176
		private static Guid componentGuid = new Guid("cf0a0f23-0ada-4c01-a8b9-bf1a0cfbcdb7");

		// Token: 0x04001C09 RID: 7177
		private static Trace mALListGeneratorTracer = null;
	}
}
