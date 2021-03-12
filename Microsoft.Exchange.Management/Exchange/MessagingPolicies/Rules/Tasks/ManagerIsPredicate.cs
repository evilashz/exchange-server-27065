using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD9 RID: 3033
	[Serializable]
	public class ManagerIsPredicate : BifurcationInfoPredicate, IEquatable<ManagerIsPredicate>
	{
		// Token: 0x0600722C RID: 29228 RVA: 0x001D0AB9 File Offset: 0x001CECB9
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses) + this.EvaluatedUser.GetHashCode();
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x001D0AD7 File Offset: 0x001CECD7
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as ManagerIsPredicate)));
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x001D0B10 File Offset: 0x001CED10
		public bool Equals(ManagerIsPredicate other)
		{
			if (this.Addresses == null)
			{
				return other.Addresses == null && this.EvaluatedUser.Equals(other.EvaluatedUser);
			}
			return this.Addresses.SequenceEqual(other.Addresses) && this.EvaluatedUser.Equals(other.EvaluatedUser);
		}

		// Token: 0x1700234E RID: 9038
		// (get) Token: 0x0600722F RID: 29231 RVA: 0x001D0B7B File Offset: 0x001CED7B
		// (set) Token: 0x06007230 RID: 29232 RVA: 0x001D0B83 File Offset: 0x001CED83
		[ExceptionParameterName("ExceptIfManagerAddresses")]
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[ConditionParameterName("ManagerAddresses")]
		public SmtpAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
			set
			{
				this.addresses = value;
			}
		}

		// Token: 0x1700234F RID: 9039
		// (get) Token: 0x06007231 RID: 29233 RVA: 0x001D0B8C File Offset: 0x001CED8C
		// (set) Token: 0x06007232 RID: 29234 RVA: 0x001D0B94 File Offset: 0x001CED94
		[ConditionParameterName("ManagerForEvaluatedUser")]
		[LocDescription(RulesTasksStrings.IDs.EvaluatedUserDescription)]
		[ExceptionParameterName("ExceptIfManagerForEvaluatedUser")]
		[LocDisplayName(RulesTasksStrings.IDs.EvaluatedUserDisplayName)]
		public EvaluatedUser EvaluatedUser
		{
			get
			{
				return this.evaluatedUser;
			}
			set
			{
				this.evaluatedUser = value;
			}
		}

		// Token: 0x17002350 RID: 9040
		// (get) Token: 0x06007233 RID: 29235 RVA: 0x001D0BA0 File Offset: 0x001CEDA0
		internal override string Description
		{
			get
			{
				string evaluatesUser = LocalizedDescriptionAttribute.FromEnum(typeof(EvaluatedUser), this.EvaluatedUser);
				string text = RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses, false), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength);
				return RulesTasksStrings.RuleDescriptionManagerIs(evaluatesUser, text);
			}
		}

		// Token: 0x06007234 RID: 29236 RVA: 0x001D0BF8 File Offset: 0x001CEDF8
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count == 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
			{
				return null;
			}
			if (bifInfo.InternalRecipients || bifInfo.ExternalRecipients || bifInfo.ExternalPartnerRecipients || bifInfo.ExternalNonPartnerRecipients)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(bifInfo.ManagementRelationship))
			{
				return null;
			}
			List<SmtpAddress> list = new List<SmtpAddress>(bifInfo.Managers.Count);
			for (int i = 0; i < bifInfo.Managers.Count; i++)
			{
				SmtpAddress item = new SmtpAddress(bifInfo.Managers[i]);
				if (item.IsValidAddress)
				{
					list.Add(item);
				}
			}
			ManagerIsPredicate managerIsPredicate = new ManagerIsPredicate();
			if (list.Count > 0)
			{
				managerIsPredicate.Addresses = list.ToArray();
				if (bifInfo.IsSenderEvaluation)
				{
					managerIsPredicate.EvaluatedUser = EvaluatedUser.Sender;
				}
				else
				{
					managerIsPredicate.EvaluatedUser = EvaluatedUser.Recipient;
				}
				return managerIsPredicate;
			}
			return null;
		}

		// Token: 0x06007235 RID: 29237 RVA: 0x001D0D9A File Offset: 0x001CEF9A
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007236 RID: 29238 RVA: 0x001D0D9D File Offset: 0x001CEF9D
		internal override void Reset()
		{
			this.addresses = null;
			base.Reset();
		}

		// Token: 0x06007237 RID: 29239 RVA: 0x001D0DAC File Offset: 0x001CEFAC
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Addresses == null || this.Addresses.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				if (!smtpAddress.IsValidAddress)
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidRecipient(smtpAddress.ToString()), base.Name));
					return;
				}
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007238 RID: 29240 RVA: 0x001D0E3C File Offset: 0x001CF03C
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				string text = smtpAddress.ToString();
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException(RulesTasksStrings.InvalidRecipient(text), "Address");
				}
				ruleBifurcationInfo.Managers.Add(text);
			}
			ruleBifurcationInfo.IsSenderEvaluation = (this.EvaluatedUser == EvaluatedUser.Sender);
			return ruleBifurcationInfo;
		}

		// Token: 0x06007239 RID: 29241 RVA: 0x001D0EC4 File Offset: 0x001CF0C4
		internal override string ToCmdletParameter(bool isException = false)
		{
			return string.Format("-{0} {1} -{2} {3}", new object[]
			{
				isException ? "ExceptIfManagerAddresses" : "ManagerAddresses",
				string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true)),
				isException ? "ExceptIfManagerForEvaluatedUser" : "ManagerForEvaluatedUser",
				Enum.GetName(typeof(EvaluatedUser), this.EvaluatedUser)
			});
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x001D0F40 File Offset: 0x001CF140
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Addresses = SuppressingPiiData.Redact(this.Addresses, out array, out array2);
		}

		// Token: 0x04003A5B RID: 14939
		private SmtpAddress[] addresses;

		// Token: 0x04003A5C RID: 14940
		private EvaluatedUser evaluatedUser;
	}
}
