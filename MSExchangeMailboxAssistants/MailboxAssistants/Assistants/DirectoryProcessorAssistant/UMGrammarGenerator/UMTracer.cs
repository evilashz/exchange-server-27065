using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001C3 RID: 451
	internal class UMTracer
	{
		// Token: 0x0600116C RID: 4460 RVA: 0x000662DC File Offset: 0x000644DC
		public static void DebugTrace(string formatString, params object[] formatObjects)
		{
			Utilities.DebugTrace(ExTraceGlobals.UMGrammarGeneratorTracer, formatString, formatObjects);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000662EA File Offset: 0x000644EA
		public static void ErrorTrace(string formatString, params object[] formatObjects)
		{
			Utilities.ErrorTrace(ExTraceGlobals.UMGrammarGeneratorTracer, formatString, formatObjects);
		}
	}
}
