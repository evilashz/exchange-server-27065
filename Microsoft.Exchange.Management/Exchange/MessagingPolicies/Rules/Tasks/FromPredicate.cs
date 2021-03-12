using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BCE RID: 3022
	[Serializable]
	public class FromPredicate : SmtpAddressesPredicate, IEquatable<FromPredicate>
	{
		// Token: 0x060071CE RID: 29134 RVA: 0x001CFB03 File Offset: 0x001CDD03
		public FromPredicate() : base("isSameUser", "Message.From")
		{
		}

		// Token: 0x060071CF RID: 29135 RVA: 0x001CFB15 File Offset: 0x001CDD15
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x001CFB22 File Offset: 0x001CDD22
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as FromPredicate)));
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x001CFB5B File Offset: 0x001CDD5B
		public bool Equals(FromPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x17002339 RID: 9017
		// (get) Token: 0x060071D2 RID: 29138 RVA: 0x001CFB80 File Offset: 0x001CDD80
		// (set) Token: 0x060071D3 RID: 29139 RVA: 0x001CFB88 File Offset: 0x001CDD88
		[LocDescription(RulesTasksStrings.IDs.FromAddressesDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.FromAddressesDisplayName)]
		[ConditionParameterName("From")]
		[ExceptionParameterName("ExceptIfFrom")]
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

		// Token: 0x1700233A RID: 9018
		// (get) Token: 0x060071D4 RID: 29140 RVA: 0x001CFB91 File Offset: 0x001CDD91
		protected override SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressesPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionFrom);
			}
		}

		// Token: 0x1700233B RID: 9019
		// (get) Token: 0x060071D5 RID: 29141 RVA: 0x001CFBA0 File Offset: 0x001CDDA0
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

		// Token: 0x060071D6 RID: 29142 RVA: 0x001CFBBD File Offset: 0x001CDDBD
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SmtpAddressesPredicate.CreateFromInternalCondition<FromPredicate>(condition, "isSameUser", "Message.From");
		}

		// Token: 0x04003A48 RID: 14920
		private const string InternalPredicateName = "isSameUser";

		// Token: 0x04003A49 RID: 14921
		private const string InternalPropertyName = "Message.From";
	}
}
