using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x0200010F RID: 271
	internal class PersonaContactCallerId : CallerId<EmailAddress>
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x0002366C File Offset: 0x0002186C
		internal PersonaContactCallerId(EmailAddress emailAddress) : base(CallerIdTypeEnum.PersonaContact, emailAddress)
		{
			if (emailAddress == null)
			{
				throw new ArgumentNullException("emailAddress");
			}
			if (!SmtpAddress.IsValidSmtpAddress(emailAddress.Address))
			{
				throw new InvalidSmtpAddressException(emailAddress.Address);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0002369D File Offset: 0x0002189D
		internal EmailAddress EmailAddress
		{
			get
			{
				return base.GetData();
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x000236A5 File Offset: 0x000218A5
		internal override int EvaluationCost
		{
			get
			{
				return PAAConstants.PersonaContactCallerIdEvaluationCost;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000236AC File Offset: 0x000218AC
		public override bool Validate(IDataValidator validator)
		{
			IDataValidationResult dataValidationResult;
			bool result = validator.ValidatePersonaContactCallerId(this.EmailAddress.Address, out dataValidationResult);
			base.ValidationResult = dataValidationResult.PAAValidationResult;
			return result;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000236DC File Offset: 0x000218DC
		public override string ToString()
		{
			return this.EmailAddress.Address + ":" + this.EmailAddress.Name;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00023700 File Offset: 0x00021900
		internal override bool Evaluate(IRuleEvaluator evaluator)
		{
			CallerIdRuleEvaluator callerIdRuleEvaluator = evaluator as CallerIdRuleEvaluator;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonaContactCallerId:Evaluate() MatchedPersonaEmails.Length == {0}", new object[]
			{
				callerIdRuleEvaluator.MatchedPersonaEmails.Count
			});
			if (callerIdRuleEvaluator.MatchedPersonaEmails != null && callerIdRuleEvaluator.MatchedPersonaEmails.Count > 0)
			{
				if (callerIdRuleEvaluator.MatchedPersonaEmails.Contains(this.EmailAddress.Address.ToLower()))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonaContactCallerId:Evaluate() Evaluation PASSED", new object[0]);
					return true;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonaContactCallerId:Evaluate() Evaluation FAILED", new object[0]);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonaContactCallerId:Evaluate() Evaluation FAILED", new object[0]);
			return false;
		}
	}
}
