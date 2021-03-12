using System;

namespace Microsoft.Exchange.Diagnostics.Components.SecurityCommon
{
	// Token: 0x0200031F RID: 799
	public static class ExTraceGlobals
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0004C0DF File Offset: 0x0004A2DF
		public static Trace CertificateEnrollmentTracer
		{
			get
			{
				if (ExTraceGlobals.certificateEnrollmentTracer == null)
				{
					ExTraceGlobals.certificateEnrollmentTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.certificateEnrollmentTracer;
			}
		}

		// Token: 0x040015B7 RID: 5559
		private static Guid componentGuid = new Guid("10DD2D62-2034-4F06-AD46-CCA14D1F30CE");

		// Token: 0x040015B8 RID: 5560
		private static Trace certificateEnrollmentTracer = null;
	}
}
