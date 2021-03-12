using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.PeopleRelevance
{
	// Token: 0x02000367 RID: 871
	public static class ExTraceGlobals
	{
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00053B97 File Offset: 0x00051D97
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x0400192F RID: 6447
		private static Guid componentGuid = new Guid("30440839-91AA-4107-9C09-6FA7DFFFCF39");

		// Token: 0x04001930 RID: 6448
		private static Trace generalTracer = null;
	}
}
