using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000091 RID: 145
	internal class DialPlanGrammar : DirectoryGrammar
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x00013E94 File Offset: 0x00012094
		public DialPlanGrammar(Guid dialPlanGuid)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "DialPlanGrammar constructor - dialPlanGuid='{0}'", new object[]
			{
				dialPlanGuid
			});
			this.dialPlanGuid = dialPlanGuid;
			this.fileName = dialPlanGuid.ToString();
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00013EE2 File Offset: 0x000120E2
		public override string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00013EEA File Offset: 0x000120EA
		protected override bool ShouldAcceptGrammarEntry(ADEntry entry)
		{
			return entry.DialPlanGuid == this.dialPlanGuid;
		}

		// Token: 0x0400032A RID: 810
		private readonly Guid dialPlanGuid;

		// Token: 0x0400032B RID: 811
		private readonly string fileName;
	}
}
