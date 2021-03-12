using System;

namespace Microsoft.Exchange.Diagnostics.Components.DelegatedAuthentication
{
	// Token: 0x02000372 RID: 882
	public static class ExTraceGlobals
	{
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x00053FD6 File Offset: 0x000521D6
		public static Trace DelegatedAuthTracer
		{
			get
			{
				if (ExTraceGlobals.delegatedAuthTracer == null)
				{
					ExTraceGlobals.delegatedAuthTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.delegatedAuthTracer;
			}
		}

		// Token: 0x04001953 RID: 6483
		private static Guid componentGuid = new Guid("d05b1f84-6a69-45ca-887c-60d0e07efb93");

		// Token: 0x04001954 RID: 6484
		private static Trace delegatedAuthTracer = null;
	}
}
