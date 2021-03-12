using System;

namespace Microsoft.Exchange.Diagnostics.Components.DsnAndQuotaGeneration
{
	// Token: 0x02000321 RID: 801
	public static class ExTraceGlobals
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0004C16D File Offset: 0x0004A36D
		public static Trace DsnTracer
		{
			get
			{
				if (ExTraceGlobals.dsnTracer == null)
				{
					ExTraceGlobals.dsnTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.dsnTracer;
			}
		}

		// Token: 0x040015BC RID: 5564
		private static Guid componentGuid = new Guid("D15942F5-51BD-41f5-956D-1E47626ADFB6");

		// Token: 0x040015BD RID: 5565
		private static Trace dsnTracer = null;
	}
}
