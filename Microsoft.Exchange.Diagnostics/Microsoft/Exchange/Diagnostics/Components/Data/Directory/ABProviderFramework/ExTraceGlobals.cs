using System;

namespace Microsoft.Exchange.Diagnostics.Components.Data.Directory.ABProviderFramework
{
	// Token: 0x0200033C RID: 828
	public static class ExTraceGlobals
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00050B2E File Offset: 0x0004ED2E
		public static Trace FrameworkTracer
		{
			get
			{
				if (ExTraceGlobals.frameworkTracer == null)
				{
					ExTraceGlobals.frameworkTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.frameworkTracer;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00050B4C File Offset: 0x0004ED4C
		public static Trace ActiveDirectoryTracer
		{
			get
			{
				if (ExTraceGlobals.activeDirectoryTracer == null)
				{
					ExTraceGlobals.activeDirectoryTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.activeDirectoryTracer;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00050B6A File Offset: 0x0004ED6A
		public static Trace ExchangeWebServicesTracer
		{
			get
			{
				if (ExTraceGlobals.exchangeWebServicesTracer == null)
				{
					ExTraceGlobals.exchangeWebServicesTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.exchangeWebServicesTracer;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00050B88 File Offset: 0x0004ED88
		public static Trace OwaUrlsTracer
		{
			get
			{
				if (ExTraceGlobals.owaUrlsTracer == null)
				{
					ExTraceGlobals.owaUrlsTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.owaUrlsTracer;
			}
		}

		// Token: 0x040017C7 RID: 6087
		private static Guid componentGuid = new Guid("9E009811-D5D4-434b-B1BC-85C64CE57046");

		// Token: 0x040017C8 RID: 6088
		private static Trace frameworkTracer = null;

		// Token: 0x040017C9 RID: 6089
		private static Trace activeDirectoryTracer = null;

		// Token: 0x040017CA RID: 6090
		private static Trace exchangeWebServicesTracer = null;

		// Token: 0x040017CB RID: 6091
		private static Trace owaUrlsTracer = null;
	}
}
