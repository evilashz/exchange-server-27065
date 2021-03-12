using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000E6 RID: 230
	internal class ADContactCallerId : CallerId<string>
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x0001DEBE File Offset: 0x0001C0BE
		internal ADContactCallerId(string legacyExchangeDN) : base(CallerIdTypeEnum.ADContact, legacyExchangeDN)
		{
			if (legacyExchangeDN == null)
			{
				throw new ArgumentNullException("legacyExchangeDN");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001DED6 File Offset: 0x0001C0D6
		internal string LegacyExchangeDN
		{
			get
			{
				return base.GetData();
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001DEDE File Offset: 0x0001C0DE
		internal override int EvaluationCost
		{
			get
			{
				return PAAConstants.ADContactCallerIdEvaluationCost;
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		public override bool Validate(IDataValidator validator)
		{
			IDataValidationResult dataValidationResult;
			bool result = validator.ValidateADContactCallerId(this.LegacyExchangeDN, out dataValidationResult);
			base.ValidationResult = dataValidationResult.PAAValidationResult;
			return result;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001DF14 File Offset: 0x0001C114
		internal override bool Evaluate(IRuleEvaluator evaluator)
		{
			CallerIdRuleEvaluator callerIdRuleEvaluator = evaluator as CallerIdRuleEvaluator;
			PIIMessage piimessage = PIIMessage.Create(PIIType._PII, this.LegacyExchangeDN);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, piimessage, "ADContactCallerId:Evaluate() ExchangeLegacyDN = \"PII\"", new object[0]);
			if (callerIdRuleEvaluator.MatchedADContact == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "ADContactCallerId:Evaluate() There was no ADContact match for callerid. This evaluation will fail", new object[0]);
				return false;
			}
			ADContactInfo matchedADContact = callerIdRuleEvaluator.MatchedADContact;
			PIIMessage piimessage2 = PIIMessage.Create(PIIType._User, matchedADContact.LegacyExchangeDN);
			PIIMessage[] data = new PIIMessage[]
			{
				piimessage,
				piimessage2
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "ADContactCallerId:Evaluate() Comparing ExchangeLegacyDN = \"_PII\" with ADContact \"_User\"", new object[0]);
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromADRecipient(matchedADContact.ADOrgPerson, true);
			ADRecipient adrecipient = iadrecipientLookup.LookupByLegacyExchangeDN(this.LegacyExchangeDN);
			if (adrecipient != null && adrecipient.Guid == matchedADContact.ADOrgPerson.Guid)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, piimessage, "ADContactCallerId:Evaluate() PASSED ExchangeLegacyDN = \"_PII\"", new object[0]);
				return true;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, piimessage, "ADContactCallerId:Evaluate() FAILED ExchangeLegacyDN = \"_PII\"", new object[0]);
			return false;
		}
	}
}
