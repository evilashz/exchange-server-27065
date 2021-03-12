using System;

namespace Microsoft.Exchange.Diagnostics.Components.OrgIdAuthentication
{
	// Token: 0x020003DD RID: 989
	public static class ExTraceGlobals
	{
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0005A0A6 File Offset: 0x000582A6
		public static Trace OrgIdAuthenticationTracer
		{
			get
			{
				if (ExTraceGlobals.orgIdAuthenticationTracer == null)
				{
					ExTraceGlobals.orgIdAuthenticationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.orgIdAuthenticationTracer;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0005A0C4 File Offset: 0x000582C4
		public static Trace OrgIdBasicAuthTracer
		{
			get
			{
				if (ExTraceGlobals.orgIdBasicAuthTracer == null)
				{
					ExTraceGlobals.orgIdBasicAuthTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.orgIdBasicAuthTracer;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0005A0E2 File Offset: 0x000582E2
		public static Trace OrgIdConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.orgIdConfigurationTracer == null)
				{
					ExTraceGlobals.orgIdConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.orgIdConfigurationTracer;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0005A100 File Offset: 0x00058300
		public static Trace OrgIdUserValidationTracer
		{
			get
			{
				if (ExTraceGlobals.orgIdUserValidationTracer == null)
				{
					ExTraceGlobals.orgIdUserValidationTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.orgIdUserValidationTracer;
			}
		}

		// Token: 0x04001C2C RID: 7212
		private static Guid componentGuid = new Guid("BD7A7CA1-6659-4EB0-A477-8F89F9A7D983");

		// Token: 0x04001C2D RID: 7213
		private static Trace orgIdAuthenticationTracer = null;

		// Token: 0x04001C2E RID: 7214
		private static Trace orgIdBasicAuthTracer = null;

		// Token: 0x04001C2F RID: 7215
		private static Trace orgIdConfigurationTracer = null;

		// Token: 0x04001C30 RID: 7216
		private static Trace orgIdUserValidationTracer = null;
	}
}
