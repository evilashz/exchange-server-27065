using System;

namespace Microsoft.Exchange.Diagnostics.Components.ADRecipientPermission
{
	// Token: 0x02000326 RID: 806
	public static class ExTraceGlobals
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0004C2E2 File Offset: 0x0004A4E2
		public static Trace ADPermissionTracer
		{
			get
			{
				if (ExTraceGlobals.aDPermissionTracer == null)
				{
					ExTraceGlobals.aDPermissionTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.aDPermissionTracer;
			}
		}

		// Token: 0x040015C9 RID: 5577
		private static Guid componentGuid = new Guid("CA1F9293-1DBD-4762-895F-8A3DC190B239");

		// Token: 0x040015CA RID: 5578
		private static Trace aDPermissionTracer = null;
	}
}
