using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BCD RID: 3021
	[Serializable]
	public class FromMemberOfPredicate : SmtpAddressesPredicate, IEquatable<FromMemberOfPredicate>
	{
		// Token: 0x060071C5 RID: 29125 RVA: 0x001CFA36 File Offset: 0x001CDC36
		public FromMemberOfPredicate() : base("isMemberOf", "Message.From")
		{
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x001CFA48 File Offset: 0x001CDC48
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x060071C7 RID: 29127 RVA: 0x001CFA55 File Offset: 0x001CDC55
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as FromMemberOfPredicate)));
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x001CFA8E File Offset: 0x001CDC8E
		public bool Equals(FromMemberOfPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x17002336 RID: 9014
		// (get) Token: 0x060071C9 RID: 29129 RVA: 0x001CFAB3 File Offset: 0x001CDCB3
		// (set) Token: 0x060071CA RID: 29130 RVA: 0x001CFABB File Offset: 0x001CDCBB
		[ExceptionParameterName("ExceptIfFromMemberOf")]
		[LocDisplayName(RulesTasksStrings.IDs.FromDLAddressDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.FromDLAddressDescription)]
		[ConditionParameterName("FromMemberOf")]
		public override SmtpAddress[] Addresses
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

		// Token: 0x17002337 RID: 9015
		// (get) Token: 0x060071CB RID: 29131 RVA: 0x001CFAC4 File Offset: 0x001CDCC4
		protected override SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressesPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionFromMemberOf);
			}
		}

		// Token: 0x17002338 RID: 9016
		// (get) Token: 0x060071CC RID: 29132 RVA: 0x001CFAD4 File Offset: 0x001CDCD4
		[LocDescription(RulesTasksStrings.IDs.SubTypeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.SubTypeDisplayName)]
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

		// Token: 0x060071CD RID: 29133 RVA: 0x001CFAF1 File Offset: 0x001CDCF1
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SmtpAddressesPredicate.CreateFromInternalCondition<FromMemberOfPredicate>(condition, "isMemberOf", "Message.From");
		}

		// Token: 0x04003A46 RID: 14918
		private const string InternalPredicateName = "isMemberOf";

		// Token: 0x04003A47 RID: 14919
		private const string InternalPropertyName = "Message.From";
	}
}
