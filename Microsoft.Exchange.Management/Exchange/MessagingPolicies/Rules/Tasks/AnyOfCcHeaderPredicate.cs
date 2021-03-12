using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BAD RID: 2989
	[Serializable]
	public class AnyOfCcHeaderPredicate : SmtpAddressesPredicate, IEquatable<AnyOfCcHeaderPredicate>
	{
		// Token: 0x060070B2 RID: 28850 RVA: 0x001CD3D4 File Offset: 0x001CB5D4
		public AnyOfCcHeaderPredicate() : base("isSameUser", "Message.Cc")
		{
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x001CD3E6 File Offset: 0x001CB5E6
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x060070B4 RID: 28852 RVA: 0x001CD3F3 File Offset: 0x001CB5F3
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AnyOfCcHeaderPredicate)));
		}

		// Token: 0x060070B5 RID: 28853 RVA: 0x001CD42C File Offset: 0x001CB62C
		public bool Equals(AnyOfCcHeaderPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x17002300 RID: 8960
		// (get) Token: 0x060070B6 RID: 28854 RVA: 0x001CD451 File Offset: 0x001CB651
		// (set) Token: 0x060070B7 RID: 28855 RVA: 0x001CD459 File Offset: 0x001CB659
		[ExceptionParameterName("ExceptIfAnyOfCcHeader")]
		[ConditionParameterName("AnyOfCcHeader")]
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
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

		// Token: 0x17002301 RID: 8961
		// (get) Token: 0x060070B8 RID: 28856 RVA: 0x001CD462 File Offset: 0x001CB662
		protected override SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressesPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAnyOfCcHeader);
			}
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x001CD470 File Offset: 0x001CB670
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SmtpAddressesPredicate.CreateFromInternalCondition<AnyOfCcHeaderPredicate>(condition, "isSameUser", "Message.Cc");
		}

		// Token: 0x04003A1F RID: 14879
		private const string InternalPredicateName = "isSameUser";

		// Token: 0x04003A20 RID: 14880
		private const string InternalPropertyName = "Message.Cc";
	}
}
