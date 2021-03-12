using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceTraining
{
	// Token: 0x02000365 RID: 869
	public static class ExTraceGlobals
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00053B2D File Offset: 0x00051D2D
		public static Trace AssistantTracer
		{
			get
			{
				if (ExTraceGlobals.assistantTracer == null)
				{
					ExTraceGlobals.assistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.assistantTracer;
			}
		}

		// Token: 0x0400192B RID: 6443
		private static Guid componentGuid = new Guid("FB0FDD38-E81F-49B7-AEF0-80A9433ED4C2");

		// Token: 0x0400192C RID: 6444
		private static Trace assistantTracer = null;
	}
}
