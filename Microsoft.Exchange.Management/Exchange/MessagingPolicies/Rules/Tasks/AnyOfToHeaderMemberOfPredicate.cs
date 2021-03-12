using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB8 RID: 3000
	[Serializable]
	public class AnyOfToHeaderMemberOfPredicate : SmtpAddressesPredicate, IEquatable<AnyOfToHeaderMemberOfPredicate>
	{
		// Token: 0x06007103 RID: 28931 RVA: 0x001CDD7B File Offset: 0x001CBF7B
		public AnyOfToHeaderMemberOfPredicate() : base("isMemberOf", "Message.To")
		{
		}

		// Token: 0x06007104 RID: 28932 RVA: 0x001CDD8D File Offset: 0x001CBF8D
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06007105 RID: 28933 RVA: 0x001CDD9A File Offset: 0x001CBF9A
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AnyOfToHeaderMemberOfPredicate)));
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x001CDDD3 File Offset: 0x001CBFD3
		public bool Equals(AnyOfToHeaderMemberOfPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x17002311 RID: 8977
		// (get) Token: 0x06007107 RID: 28935 RVA: 0x001CDDF8 File Offset: 0x001CBFF8
		// (set) Token: 0x06007108 RID: 28936 RVA: 0x001CDE00 File Offset: 0x001CC000
		[LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[ExceptionParameterName("ExceptIfAnyOfToHeaderMemberOf")]
		[LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[ConditionParameterName("AnyOfToHeaderMemberOf")]
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

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x06007109 RID: 28937 RVA: 0x001CDE09 File Offset: 0x001CC009
		protected override SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressesPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAnyOfToHeaderMemberOf);
			}
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x001CDE17 File Offset: 0x001CC017
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SmtpAddressesPredicate.CreateFromInternalCondition<AnyOfToHeaderMemberOfPredicate>(condition, "isMemberOf", "Message.To");
		}

		// Token: 0x04003A2E RID: 14894
		private const string InternalPredicateName = "isMemberOf";

		// Token: 0x04003A2F RID: 14895
		private const string InternalPropertyName = "Message.To";
	}
}
