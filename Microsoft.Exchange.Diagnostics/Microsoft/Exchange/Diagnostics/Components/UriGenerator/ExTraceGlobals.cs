using System;

namespace Microsoft.Exchange.Diagnostics.Components.UriGenerator
{
	// Token: 0x020003DC RID: 988
	public static class ExTraceGlobals
	{
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0005A071 File Offset: 0x00058271
		public static Trace UriGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.uriGeneratorTracer == null)
				{
					ExTraceGlobals.uriGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.uriGeneratorTracer;
			}
		}

		// Token: 0x04001C2A RID: 7210
		private static Guid componentGuid = new Guid("FAE9B52D-2EC7-4168-9668-5A3B88DCAACA");

		// Token: 0x04001C2B RID: 7211
		private static Trace uriGeneratorTracer = null;
	}
}
