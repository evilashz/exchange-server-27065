using System;

namespace Microsoft.Exchange.Diagnostics.Components.SenderId
{
	// Token: 0x02000383 RID: 899
	public static class ExTraceGlobals
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x000559A1 File Offset: 0x00053BA1
		public static Trace ValidationTracer
		{
			get
			{
				if (ExTraceGlobals.validationTracer == null)
				{
					ExTraceGlobals.validationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.validationTracer;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x000559BF File Offset: 0x00053BBF
		public static Trace ParsingTracer
		{
			get
			{
				if (ExTraceGlobals.parsingTracer == null)
				{
					ExTraceGlobals.parsingTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.parsingTracer;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x000559DD File Offset: 0x00053BDD
		public static Trace MacroExpansionTracer
		{
			get
			{
				if (ExTraceGlobals.macroExpansionTracer == null)
				{
					ExTraceGlobals.macroExpansionTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.macroExpansionTracer;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000559FB File Offset: 0x00053BFB
		public static Trace AgentTracer
		{
			get
			{
				if (ExTraceGlobals.agentTracer == null)
				{
					ExTraceGlobals.agentTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.agentTracer;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00055A19 File Offset: 0x00053C19
		public static Trace OtherTracer
		{
			get
			{
				if (ExTraceGlobals.otherTracer == null)
				{
					ExTraceGlobals.otherTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.otherTracer;
			}
		}

		// Token: 0x04001A0F RID: 6671
		private static Guid componentGuid = new Guid("AA6A0F4B-6EC1-472d-84BA-FDCB84F20449");

		// Token: 0x04001A10 RID: 6672
		private static Trace validationTracer = null;

		// Token: 0x04001A11 RID: 6673
		private static Trace parsingTracer = null;

		// Token: 0x04001A12 RID: 6674
		private static Trace macroExpansionTracer = null;

		// Token: 0x04001A13 RID: 6675
		private static Trace agentTracer = null;

		// Token: 0x04001A14 RID: 6676
		private static Trace otherTracer = null;
	}
}
