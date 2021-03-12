using System;

namespace Microsoft.Exchange.Diagnostics.Components.AntispamCommon
{
	// Token: 0x020003CB RID: 971
	public static class ExTraceGlobals
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00059A7D File Offset: 0x00057C7D
		public static Trace UtilitiesTracer
		{
			get
			{
				if (ExTraceGlobals.utilitiesTracer == null)
				{
					ExTraceGlobals.utilitiesTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.utilitiesTracer;
			}
		}

		// Token: 0x04001BF7 RID: 7159
		private static Guid componentGuid = new Guid("99F80BFB-F09F-41C9-8D3E-89A1C6CD9CD1");

		// Token: 0x04001BF8 RID: 7160
		private static Trace utilitiesTracer = null;
	}
}
