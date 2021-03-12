using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceDataCollection
{
	// Token: 0x02000366 RID: 870
	public static class ExTraceGlobals
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00053B62 File Offset: 0x00051D62
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

		// Token: 0x0400192D RID: 6445
		private static Guid componentGuid = new Guid("C36CD9F9-4104-4C85-B1FD-2B432FEE369C");

		// Token: 0x0400192E RID: 6446
		private static Trace generalTracer = null;
	}
}
