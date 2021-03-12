using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x0200035B RID: 859
	public static class ExTraceGlobals
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x000524BF File Offset: 0x000506BF
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

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x000524DD File Offset: 0x000506DD
		public static Trace ADCrawlerTracer
		{
			get
			{
				if (ExTraceGlobals.aDCrawlerTracer == null)
				{
					ExTraceGlobals.aDCrawlerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.aDCrawlerTracer;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x000524FB File Offset: 0x000506FB
		public static Trace DtmfMapGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.dtmfMapGeneratorTracer == null)
				{
					ExTraceGlobals.dtmfMapGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.dtmfMapGeneratorTracer;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00052519 File Offset: 0x00050719
		public static Trace GrammarGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.grammarGeneratorTracer == null)
				{
					ExTraceGlobals.grammarGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.grammarGeneratorTracer;
			}
		}

		// Token: 0x0400188A RID: 6282
		private static Guid componentGuid = new Guid("C585941C-EDA7-11E0-A0A8-1BCA4724019B");

		// Token: 0x0400188B RID: 6283
		private static Trace generalTracer = null;

		// Token: 0x0400188C RID: 6284
		private static Trace aDCrawlerTracer = null;

		// Token: 0x0400188D RID: 6285
		private static Trace dtmfMapGeneratorTracer = null;

		// Token: 0x0400188E RID: 6286
		private static Trace grammarGeneratorTracer = null;
	}
}
