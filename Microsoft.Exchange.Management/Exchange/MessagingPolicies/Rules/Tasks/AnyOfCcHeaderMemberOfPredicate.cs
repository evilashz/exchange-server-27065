using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BAC RID: 2988
	[Serializable]
	public class AnyOfCcHeaderMemberOfPredicate : SmtpAddressesPredicate, IEquatable<AnyOfCcHeaderMemberOfPredicate>
	{
		// Token: 0x060070AA RID: 28842 RVA: 0x001CD326 File Offset: 0x001CB526
		public AnyOfCcHeaderMemberOfPredicate() : base("isMemberOf", "Message.Cc")
		{
		}

		// Token: 0x060070AB RID: 28843 RVA: 0x001CD338 File Offset: 0x001CB538
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x001CD345 File Offset: 0x001CB545
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AnyOfCcHeaderMemberOfPredicate)));
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x001CD37E File Offset: 0x001CB57E
		public bool Equals(AnyOfCcHeaderMemberOfPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022FE RID: 8958
		// (get) Token: 0x060070AE RID: 28846 RVA: 0x001CD3A3 File Offset: 0x001CB5A3
		// (set) Token: 0x060070AF RID: 28847 RVA: 0x001CD3AB File Offset: 0x001CB5AB
		[LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[ExceptionParameterName("ExceptIfAnyOfCcHeaderMemberOf")]
		[ConditionParameterName("AnyOfCcHeaderMemberOf")]
		[LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
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

		// Token: 0x170022FF RID: 8959
		// (get) Token: 0x060070B0 RID: 28848 RVA: 0x001CD3B4 File Offset: 0x001CB5B4
		protected override SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressesPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAnyOfCcHeaderMemberOf);
			}
		}

		// Token: 0x060070B1 RID: 28849 RVA: 0x001CD3C2 File Offset: 0x001CB5C2
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SmtpAddressesPredicate.CreateFromInternalCondition<AnyOfCcHeaderMemberOfPredicate>(condition, "isMemberOf", "Message.Cc");
		}

		// Token: 0x04003A1D RID: 14877
		private const string InternalPredicateName = "isMemberOf";

		// Token: 0x04003A1E RID: 14878
		private const string InternalPropertyName = "Message.Cc";
	}
}
