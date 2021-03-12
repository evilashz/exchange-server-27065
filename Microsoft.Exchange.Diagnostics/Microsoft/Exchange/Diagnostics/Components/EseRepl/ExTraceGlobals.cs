using System;

namespace Microsoft.Exchange.Diagnostics.Components.EseRepl
{
	// Token: 0x02000378 RID: 888
	public static class ExTraceGlobals
	{
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00054D62 File Offset: 0x00052F62
		public static Trace DagNetEnvironmentTracer
		{
			get
			{
				if (ExTraceGlobals.dagNetEnvironmentTracer == null)
				{
					ExTraceGlobals.dagNetEnvironmentTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.dagNetEnvironmentTracer;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x00054D80 File Offset: 0x00052F80
		public static Trace DagNetChooserTracer
		{
			get
			{
				if (ExTraceGlobals.dagNetChooserTracer == null)
				{
					ExTraceGlobals.dagNetChooserTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.dagNetChooserTracer;
			}
		}

		// Token: 0x040019B4 RID: 6580
		private static Guid componentGuid = new Guid("173045e6-18eb-4bbe-907a-fc8170f0b837");

		// Token: 0x040019B5 RID: 6581
		private static Trace dagNetEnvironmentTracer = null;

		// Token: 0x040019B6 RID: 6582
		private static Trace dagNetChooserTracer = null;
	}
}
