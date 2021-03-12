using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000EA RID: 234
	internal class ContactFolderCallerId : CallerIdBase
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x0001E184 File Offset: 0x0001C384
		internal ContactFolderCallerId() : base(CallerIdTypeEnum.DefaultContactFolder)
		{
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001E18D File Offset: 0x0001C38D
		internal override int EvaluationCost
		{
			get
			{
				return PAAConstants.ContactFolderCallerIdEvaluationCost;
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001E194 File Offset: 0x0001C394
		public override bool Validate(IDataValidator validator)
		{
			IDataValidationResult dataValidationResult;
			bool result = validator.ValidateContactFolderCallerId(out dataValidationResult);
			base.ValidationResult = dataValidationResult.PAAValidationResult;
			return result;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001E1B8 File Offset: 0x0001C3B8
		internal override bool Evaluate(IRuleEvaluator evaluator)
		{
			CallerIdRuleEvaluator callerIdRuleEvaluator = evaluator as CallerIdRuleEvaluator;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "ContactFolderCallerId:Evaluate() MatchedContacts.Length == {0}", new object[]
			{
				callerIdRuleEvaluator.MatchedPersonalContacts.Length
			});
			if (callerIdRuleEvaluator.MatchedPersonalContacts.Length > 0)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "ContactFolderCallerId:Evaluate() Evaluation PASSED", new object[0]);
				return true;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "ContactFolderCallerId:Evaluate() Evaluation FAILED", new object[0]);
			return false;
		}
	}
}
