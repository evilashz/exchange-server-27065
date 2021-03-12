using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000EB RID: 235
	internal class ContactItemCallerId : CallerId<StoreObjectId>
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0001E22E File Offset: 0x0001C42E
		internal ContactItemCallerId(StoreObjectId id) : base(CallerIdTypeEnum.ContactItem, id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001E246 File Offset: 0x0001C446
		internal StoreObjectId StoreObjectId
		{
			get
			{
				return base.GetData();
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001E24E File Offset: 0x0001C44E
		internal override int EvaluationCost
		{
			get
			{
				return PAAConstants.ContactItemCallerIdEvaluationCost;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001E258 File Offset: 0x0001C458
		public static ContactItemCallerId Parse(string representation)
		{
			byte[] entryId = Convert.FromBase64String(representation);
			StoreObjectId id = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Contact);
			return new ContactItemCallerId(id);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001E27C File Offset: 0x0001C47C
		public override string ToString()
		{
			byte[] providerLevelItemId = this.StoreObjectId.ProviderLevelItemId;
			return Convert.ToBase64String(providerLevelItemId, Base64FormattingOptions.None);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001E29C File Offset: 0x0001C49C
		public override bool Validate(IDataValidator validator)
		{
			IDataValidationResult dataValidationResult;
			bool result = validator.ValidateContactItemCallerId(this.StoreObjectId, out dataValidationResult);
			base.ValidationResult = dataValidationResult.PAAValidationResult;
			return result;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		internal override bool Evaluate(IRuleEvaluator evaluator)
		{
			CallerIdRuleEvaluator callerIdRuleEvaluator = evaluator as CallerIdRuleEvaluator;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "ContactItemCallerId:Evaluate() MatchedPersonalContacts.Length == {0}", new object[]
			{
				callerIdRuleEvaluator.MatchedPersonalContacts.Length
			});
			if (callerIdRuleEvaluator.MatchedPersonalContacts.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < callerIdRuleEvaluator.MatchedPersonalContacts.Length; i++)
			{
				PersonalContactInfo personalContactInfo = callerIdRuleEvaluator.MatchedPersonalContacts[i];
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "ContactItemCallerId:Evaluate() Comparing this = \"{0}\" that = \"{1}\"", new object[]
				{
					this.StoreObjectId.ToBase64String(),
					personalContactInfo.Id
				});
				if (personalContactInfo.Id.Equals(this.StoreObjectId.ToBase64String()))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "ContactItemCallerId:Evaluate() Evaluation PASSED", new object[0]);
					return true;
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "ContactItemCallerId:Evaluate() Evaluation FAILED", new object[0]);
			return false;
		}
	}
}
