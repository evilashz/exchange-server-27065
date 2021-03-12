using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000112 RID: 274
	internal class PhoneNumberCallerId : CallerId<string>
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x00023A45 File Offset: 0x00021C45
		internal PhoneNumberCallerId(string callerid) : base(CallerIdTypeEnum.Number, callerid)
		{
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00023A4F File Offset: 0x00021C4F
		internal string PhoneNumberString
		{
			get
			{
				return base.GetData();
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00023A57 File Offset: 0x00021C57
		internal override int EvaluationCost
		{
			get
			{
				return PAAConstants.PhoneNumberCallerIdEvaluationCost;
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00023A60 File Offset: 0x00021C60
		public override bool Validate(IDataValidator validator)
		{
			IDataValidationResult dataValidationResult;
			bool result = validator.ValidatePhoneNumberCallerId(this.PhoneNumberString, out dataValidationResult);
			base.ValidationResult = dataValidationResult.PAAValidationResult;
			return result;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00023A8C File Offset: 0x00021C8C
		internal override bool Evaluate(IRuleEvaluator evaluator)
		{
			CallerIdRuleEvaluator callerIdRuleEvaluator = evaluator as CallerIdRuleEvaluator;
			PhoneNumber callerId = callerIdRuleEvaluator.GetCallerId();
			PIIMessage piimessage = PIIMessage.Create(PIIType._Caller, callerId);
			PIIMessage piimessage2 = PIIMessage.Create(PIIType._PhoneNumber, this.PhoneNumberString);
			PIIMessage[] data = new PIIMessage[]
			{
				piimessage,
				piimessage2
			};
			CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "PhoneNumberCallerId:Evaluate() CallerId = '_Caller' Condition = '_PhoneNumber'", new object[0]);
			PhoneNumber phoneNumber = null;
			if (!PhoneNumber.TryParse(this.PhoneNumberString, out phoneNumber))
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, piimessage2, "PhoneNumberCallerId:Evaluate() PhoneNumberCallerId condition '_PhoneNumber' failed to parse as a PhoneNumber. Condition will fail evaluation", new object[0]);
				return false;
			}
			UMSubscriber umsubscriber = callerIdRuleEvaluator.GetUMSubscriber();
			bool flag = callerId.IsMatch(phoneNumber.Number, callerId.GetOptionalPrefixes(umsubscriber.DialPlan));
			CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PhoneNumberCallerId:Evaluate() {2} Comparing Parsed CallerId = '{0}' Parsed Condition = '{1}'", new object[]
			{
				callerId.Number,
				phoneNumber.Number,
				flag ? "PASS" : "FAIL"
			});
			return flag;
		}
	}
}
