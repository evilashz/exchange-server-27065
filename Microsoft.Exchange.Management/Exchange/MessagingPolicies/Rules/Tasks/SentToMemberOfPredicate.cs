using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BEB RID: 3051
	[Serializable]
	public class SentToMemberOfPredicate : BifurcationInfoPredicate, IEquatable<SentToMemberOfPredicate>
	{
		// Token: 0x06007310 RID: 29456 RVA: 0x001D443D File Offset: 0x001D263D
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06007311 RID: 29457 RVA: 0x001D444A File Offset: 0x001D264A
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SentToMemberOfPredicate)));
		}

		// Token: 0x06007312 RID: 29458 RVA: 0x001D4483 File Offset: 0x001D2683
		public bool Equals(SentToMemberOfPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x17002371 RID: 9073
		// (get) Token: 0x06007313 RID: 29459 RVA: 0x001D44A8 File Offset: 0x001D26A8
		// (set) Token: 0x06007314 RID: 29460 RVA: 0x001D44B0 File Offset: 0x001D26B0
		[ExceptionParameterName("ExceptIfSentToMemberOf")]
		[ConditionParameterName("SentToMemberOf")]
		[LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
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

		// Token: 0x17002372 RID: 9074
		// (get) Token: 0x06007315 RID: 29461 RVA: 0x001D44B9 File Offset: 0x001D26B9
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionSentToMemberOf(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses, false), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x06007316 RID: 29462 RVA: 0x001D44E6 File Offset: 0x001D26E6
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007317 RID: 29463 RVA: 0x001D44EC File Offset: 0x001D26EC
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count == 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			List<SmtpAddress> list = new List<SmtpAddress>(bifInfo.Lists.Count);
			for (int i = 0; i < bifInfo.Lists.Count; i++)
			{
				SmtpAddress item = new SmtpAddress(bifInfo.Lists[i]);
				if (item.IsValidAddress)
				{
					list.Add(item);
				}
			}
			SentToMemberOfPredicate sentToMemberOfPredicate = new SentToMemberOfPredicate();
			if (list.Count > 0)
			{
				sentToMemberOfPredicate.Addresses = list.ToArray();
			}
			return sentToMemberOfPredicate;
		}

		// Token: 0x06007318 RID: 29464 RVA: 0x001D4672 File Offset: 0x001D2872
		internal override void Reset()
		{
			this.addresses = null;
			base.Reset();
		}

		// Token: 0x06007319 RID: 29465 RVA: 0x001D4684 File Offset: 0x001D2884
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

		// Token: 0x0600731A RID: 29466 RVA: 0x001D4714 File Offset: 0x001D2914
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
				ruleBifurcationInfo.Lists.Add(smtpAddress.ToString());
			}
			return ruleBifurcationInfo;
		}

		// Token: 0x0600731B RID: 29467 RVA: 0x001D4790 File Offset: 0x001D2990
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true));
		}

		// Token: 0x0600731C RID: 29468 RVA: 0x001D47A8 File Offset: 0x001D29A8
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Addresses = SuppressingPiiData.Redact(this.Addresses, out array, out array2);
		}

		// Token: 0x04003A8A RID: 14986
		private SmtpAddress[] addresses;
	}
}
