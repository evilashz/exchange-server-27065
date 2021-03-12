using System;

namespace Microsoft.Exchange.Diagnostics.Components.PackageManager
{
	// Token: 0x020003D5 RID: 981
	public static class ExTraceGlobals
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x00059E92 File Offset: 0x00058092
		public static Trace PackageManagerTracer
		{
			get
			{
				if (ExTraceGlobals.packageManagerTracer == null)
				{
					ExTraceGlobals.packageManagerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.packageManagerTracer;
			}
		}

		// Token: 0x04001C19 RID: 7193
		private static Guid componentGuid = new Guid("1C570B41-B6CF-4319-AD48-B2DD9D192CC7");

		// Token: 0x04001C1A RID: 7194
		private static Trace packageManagerTracer = null;
	}
}
