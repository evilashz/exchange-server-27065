using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000100 RID: 256
	internal class FreeBusyRuleEvaluator : IRuleEvaluator
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x0001FA90 File Offset: 0x0001DC90
		public FreeBusyRuleEvaluator(FreeBusyStatusEnum freeBusy)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "FreeBusyRuleEvaluator:ctor(FB = {0})", new object[]
			{
				freeBusy.ToString()
			});
			this.freeBusyCondition = freeBusy;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
		public bool Evaluate(IDataLoader dataLoader)
		{
			if (this.freeBusyCondition == FreeBusyStatusEnum.None)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "FreeBusyRuleEvaluator:Evaluate() no conditions defined. Returning true", new object[0]);
				return true;
			}
			FreeBusyStatusEnum freeBusyStatusEnum = FreeBusyStatusEnum.None;
			dataLoader.GetFreeBusyInformation(out freeBusyStatusEnum);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "FreeBusyRuleEvaluator:Evaluate() input status = {0} condition value = {1}", new object[]
			{
				freeBusyStatusEnum,
				this.freeBusyCondition
			});
			return (this.freeBusyCondition & freeBusyStatusEnum) != FreeBusyStatusEnum.None;
		}

		// Token: 0x040004C8 RID: 1224
		private FreeBusyStatusEnum freeBusyCondition;
	}
}
