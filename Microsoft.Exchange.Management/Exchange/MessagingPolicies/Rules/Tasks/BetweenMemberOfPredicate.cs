using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BC5 RID: 3013
	[Serializable]
	public class BetweenMemberOfPredicate : BifurcationInfoPredicate, IEquatable<BetweenMemberOfPredicate>
	{
		// Token: 0x0600717E RID: 29054 RVA: 0x001CEE28 File Offset: 0x001CD028
		public override int GetHashCode()
		{
			int num = 0;
			if (this.Addresses != null)
			{
				num += Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
			}
			if (this.Addresses2 != null)
			{
				num += Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses2);
			}
			return num;
		}

		// Token: 0x0600717F RID: 29055 RVA: 0x001CEE64 File Offset: 0x001CD064
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as BetweenMemberOfPredicate)));
		}

		// Token: 0x06007180 RID: 29056 RVA: 0x001CEEA0 File Offset: 0x001CD0A0
		public bool Equals(BetweenMemberOfPredicate other)
		{
			if (this.Addresses == null && this.Addresses2 == null)
			{
				return other.Addresses == null && null == other.Addresses2;
			}
			if (this.Addresses == null && this.Addresses2 != null)
			{
				return other.Addresses == null && this.Addresses2.SequenceEqual(other.Addresses2);
			}
			if (this.Addresses != null && this.Addresses2 == null)
			{
				return this.Addresses.SequenceEqual(other.Addresses) && null == other.Addresses2;
			}
			return this.Addresses.SequenceEqual(other.Addresses) && this.Addresses2.SequenceEqual(other.Addresses2);
		}

		// Token: 0x17002326 RID: 8998
		// (get) Token: 0x06007181 RID: 29057 RVA: 0x001CEF52 File Offset: 0x001CD152
		// (set) Token: 0x06007182 RID: 29058 RVA: 0x001CEF5A File Offset: 0x001CD15A
		[LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[ConditionParameterName("BetweenMemberOf1")]
		[ExceptionParameterName("ExceptIfBetweenMemberOf1")]
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

		// Token: 0x17002327 RID: 8999
		// (get) Token: 0x06007183 RID: 29059 RVA: 0x001CEF63 File Offset: 0x001CD163
		// (set) Token: 0x06007184 RID: 29060 RVA: 0x001CEF6B File Offset: 0x001CD16B
		[LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[ConditionParameterName("BetweenMemberOf2")]
		[ExceptionParameterName("ExceptIfBetweenMemberOf2")]
		public virtual SmtpAddress[] Addresses2
		{
			get
			{
				return this.addresses2;
			}
			set
			{
				this.addresses2 = value;
			}
		}

		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x06007185 RID: 29061 RVA: 0x001CEF74 File Offset: 0x001CD174
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionBetweenMemberOf(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses, false), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength), RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses2, false), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x06007186 RID: 29062 RVA: 0x001CEFD0 File Offset: 0x001CD1D0
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo1, RuleBifurcationInfo bifInfo2)
		{
			if (bifInfo1.ADAttributesForTextMatch.Count > 0 || bifInfo1.ADAttributes.Count > 0 || bifInfo1.Managers.Count > 0 || bifInfo1.Recipients.Count > 0 || bifInfo1.Lists.Count == 0 || bifInfo1.FromRecipients.Count > 0 || bifInfo1.FromLists.Count == 0 || bifInfo1.Partners.Count > 0 || bifInfo1.RecipientAddressContainsWords.Count > 0 || bifInfo1.RecipientDomainIs.Count > 0 || bifInfo1.RecipientMatchesPatterns.Count > 0 || bifInfo1.RecipientAttributeContains.Count > 0 || bifInfo1.RecipientAttributeMatches.Count > 0 || bifInfo1.SenderInRecipientList.Count > 0 || bifInfo1.RecipientInSenderList.Count > 0)
			{
				return null;
			}
			if (bifInfo2.ADAttributesForTextMatch.Count > 0 || bifInfo2.ADAttributes.Count > 0 || bifInfo2.Managers.Count > 0 || bifInfo2.Recipients.Count > 0 || bifInfo2.Lists.Count == 0 || bifInfo2.FromRecipients.Count > 0 || bifInfo2.FromLists.Count == 0 || bifInfo2.Partners.Count > 0 || bifInfo2.RecipientAddressContainsWords.Count > 0 || bifInfo2.RecipientDomainIs.Count > 0 || bifInfo2.RecipientMatchesPatterns.Count > 0 || bifInfo2.RecipientAttributeContains.Count > 0 || bifInfo2.RecipientAttributeMatches.Count > 0 || bifInfo2.SenderInRecipientList.Count > 0 || bifInfo2.RecipientInSenderList.Count > 0)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(bifInfo1.ManagementRelationship) || !string.IsNullOrEmpty(bifInfo2.ManagementRelationship))
			{
				return null;
			}
			if (bifInfo1.Exception != bifInfo2.Exception)
			{
				return null;
			}
			if (bifInfo1.InternalRecipients || bifInfo1.ExternalRecipients || bifInfo1.ExternalPartnerRecipients || bifInfo1.ExternalNonPartnerRecipients)
			{
				return null;
			}
			if (bifInfo2.InternalRecipients || bifInfo2.ExternalRecipients || bifInfo2.ExternalPartnerRecipients || bifInfo2.ExternalNonPartnerRecipients)
			{
				return null;
			}
			if (bifInfo1.Lists.Count != bifInfo2.FromLists.Count || bifInfo2.Lists.Count != bifInfo1.FromLists.Count)
			{
				return null;
			}
			for (int i = 0; i < bifInfo1.Lists.Count; i++)
			{
				if (bifInfo1.Lists[i] != bifInfo2.FromLists[i])
				{
					return null;
				}
			}
			for (int j = 0; j < bifInfo2.Lists.Count; j++)
			{
				if (bifInfo2.Lists[j] != bifInfo1.FromLists[j])
				{
					return null;
				}
			}
			List<SmtpAddress> list = new List<SmtpAddress>(bifInfo1.FromLists.Count);
			for (int k = 0; k < bifInfo1.FromLists.Count; k++)
			{
				SmtpAddress item = new SmtpAddress(bifInfo1.FromLists[k]);
				if (item.IsValidAddress)
				{
					list.Add(item);
				}
			}
			List<SmtpAddress> list2 = new List<SmtpAddress>(bifInfo1.Lists.Count);
			for (int l = 0; l < bifInfo1.Lists.Count; l++)
			{
				SmtpAddress item2 = new SmtpAddress(bifInfo1.Lists[l]);
				if (item2.IsValidAddress)
				{
					list2.Add(item2);
				}
			}
			BetweenMemberOfPredicate betweenMemberOfPredicate = new BetweenMemberOfPredicate();
			if (list.Count > 0)
			{
				betweenMemberOfPredicate.Addresses = list.ToArray();
			}
			if (list2.Count > 0)
			{
				betweenMemberOfPredicate.Addresses2 = list2.ToArray();
			}
			return betweenMemberOfPredicate;
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x001CF383 File Offset: 0x001CD583
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007188 RID: 29064 RVA: 0x001CF386 File Offset: 0x001CD586
		internal override void Reset()
		{
			this.addresses = null;
			this.addresses2 = null;
			base.Reset();
		}

		// Token: 0x06007189 RID: 29065 RVA: 0x001CF39C File Offset: 0x001CD59C
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
			if (this.Addresses2 == null || this.Addresses2.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			foreach (SmtpAddress smtpAddress2 in this.Addresses2)
			{
				if (!smtpAddress2.IsValidAddress)
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidRecipient(smtpAddress2.ToString()), base.Name));
					return;
				}
			}
			base.ValidateRead(errors);
		}

		// Token: 0x0600718A RID: 29066 RVA: 0x001CF4AC File Offset: 0x001CD6AC
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			RuleBifurcationInfo ruleBifurcationInfo2 = new RuleBifurcationInfo();
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				if (!smtpAddress.IsValidAddress)
				{
					throw new ArgumentException(RulesTasksStrings.InvalidRecipient(smtpAddress.ToString()), "Address");
				}
				ruleBifurcationInfo.FromLists.Add(smtpAddress.ToString());
				ruleBifurcationInfo2.Lists.Add(smtpAddress.ToString());
			}
			foreach (SmtpAddress smtpAddress2 in this.Addresses2)
			{
				if (!smtpAddress2.IsValidAddress)
				{
					throw new ArgumentException(RulesTasksStrings.InvalidRecipient(smtpAddress2.ToString()), "Address");
				}
				ruleBifurcationInfo.Lists.Add(smtpAddress2.ToString());
				ruleBifurcationInfo2.FromLists.Add(smtpAddress2.ToString());
			}
			additionalBifurcationInfo = ruleBifurcationInfo2;
			return ruleBifurcationInfo;
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x001CF5D4 File Offset: 0x001CD7D4
		internal override string ToCmdletParameter(bool isException = false)
		{
			return string.Format("-{0} {1} -{2} {3}", new object[]
			{
				isException ? "ExceptIfBetweenMemberOf1" : "BetweenMemberOf1",
				string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true)),
				isException ? "ExceptIfBetweenMemberOf2" : "BetweenMemberOf2",
				string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses2, true))
			});
		}

		// Token: 0x0600718C RID: 29068 RVA: 0x001CF64C File Offset: 0x001CD84C
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Addresses = SuppressingPiiData.Redact(this.Addresses, out array, out array2);
			this.Addresses2 = SuppressingPiiData.Redact(this.Addresses2, out array, out array2);
		}

		// Token: 0x04003A3C RID: 14908
		private SmtpAddress[] addresses;

		// Token: 0x04003A3D RID: 14909
		private SmtpAddress[] addresses2;
	}
}
