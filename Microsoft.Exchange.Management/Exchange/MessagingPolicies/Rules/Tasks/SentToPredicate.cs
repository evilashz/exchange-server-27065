using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BEC RID: 3052
	[Serializable]
	public class SentToPredicate : BifurcationInfoPredicate, IEquatable<SentToPredicate>
	{
		// Token: 0x0600731E RID: 29470 RVA: 0x001D47D2 File Offset: 0x001D29D2
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x0600731F RID: 29471 RVA: 0x001D47DF File Offset: 0x001D29DF
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SentToPredicate)));
		}

		// Token: 0x06007320 RID: 29472 RVA: 0x001D4818 File Offset: 0x001D2A18
		public bool Equals(SentToPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x17002373 RID: 9075
		// (get) Token: 0x06007321 RID: 29473 RVA: 0x001D483D File Offset: 0x001D2A3D
		// (set) Token: 0x06007322 RID: 29474 RVA: 0x001D4845 File Offset: 0x001D2A45
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[ExceptionParameterName("ExceptIfSentTo")]
		[ConditionParameterName("SentTo")]
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		public virtual SmtpAddress[] Addresses
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

		// Token: 0x17002374 RID: 9076
		// (get) Token: 0x06007323 RID: 29475 RVA: 0x001D484E File Offset: 0x001D2A4E
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionSentTo(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses, false), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x17002375 RID: 9077
		// (get) Token: 0x06007324 RID: 29476 RVA: 0x001D487C File Offset: 0x001D2A7C
		[LocDisplayName(RulesTasksStrings.IDs.SubTypeDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.SubTypeDescription)]
		public override IEnumerable<RuleSubType> RuleSubTypes
		{
			get
			{
				return new RuleSubType[]
				{
					RuleSubType.None,
					RuleSubType.Dlp
				};
			}
		}

		// Token: 0x06007325 RID: 29477 RVA: 0x001D4899 File Offset: 0x001D2A99
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007326 RID: 29478 RVA: 0x001D489C File Offset: 0x001D2A9C
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count == 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			List<SmtpAddress> list = new List<SmtpAddress>(bifInfo.Recipients.Count);
			for (int i = 0; i < bifInfo.Recipients.Count; i++)
			{
				SmtpAddress item = new SmtpAddress(bifInfo.Recipients[i]);
				if (item.IsValidAddress)
				{
					list.Add(item);
				}
			}
			SentToPredicate sentToPredicate = new SentToPredicate();
			if (list.Count > 0)
			{
				sentToPredicate.Addresses = list.ToArray();
			}
			return sentToPredicate;
		}

		// Token: 0x06007327 RID: 29479 RVA: 0x001D4A22 File Offset: 0x001D2C22
		internal override void Reset()
		{
			this.addresses = null;
			base.Reset();
		}

		// Token: 0x06007328 RID: 29480 RVA: 0x001D4A34 File Offset: 0x001D2C34
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

		// Token: 0x06007329 RID: 29481 RVA: 0x001D4AC4 File Offset: 0x001D2CC4
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				if (!smtpAddress.IsValidAddress)
				{
					throw new ArgumentException(RulesTasksStrings.InvalidRecipient(smtpAddress.ToString()), "Address");
				}
				ruleBifurcationInfo.Recipients.Add(smtpAddress.ToString());
			}
			return ruleBifurcationInfo;
		}

		// Token: 0x0600732A RID: 29482 RVA: 0x001D4B40 File Offset: 0x001D2D40
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true));
		}

		// Token: 0x0600732B RID: 29483 RVA: 0x001D4B58 File Offset: 0x001D2D58
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Addresses = SuppressingPiiData.Redact(this.Addresses, out array, out array2);
		}

		// Token: 0x04003A8B RID: 14987
		private SmtpAddress[] addresses;
	}
}
