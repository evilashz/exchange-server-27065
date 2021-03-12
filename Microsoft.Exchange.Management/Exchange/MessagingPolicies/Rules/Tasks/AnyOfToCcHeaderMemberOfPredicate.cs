using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB6 RID: 2998
	[Serializable]
	public class AnyOfToCcHeaderMemberOfPredicate : SmtpAddressesPredicate, IEquatable<AnyOfToCcHeaderMemberOfPredicate>
	{
		// Token: 0x060070F3 RID: 28915 RVA: 0x001CDC1F File Offset: 0x001CBE1F
		public AnyOfToCcHeaderMemberOfPredicate() : base("isMemberOf", "Message.ToCc")
		{
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x001CDC31 File Offset: 0x001CBE31
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x001CDC3E File Offset: 0x001CBE3E
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AnyOfToCcHeaderMemberOfPredicate)));
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x001CDC77 File Offset: 0x001CBE77
		public bool Equals(AnyOfToCcHeaderMemberOfPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x060070F7 RID: 28919 RVA: 0x001CDC9C File Offset: 0x001CBE9C
		// (set) Token: 0x060070F8 RID: 28920 RVA: 0x001CDCA4 File Offset: 0x001CBEA4
		[LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[ExceptionParameterName("ExceptIfAnyOfToCcHeaderMemberOf")]
		[LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[ConditionParameterName("AnyOfToCcHeaderMemberOf")]
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

		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x060070F9 RID: 28921 RVA: 0x001CDCAD File Offset: 0x001CBEAD
		protected override SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressesPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAnyOfToCcHeaderMemberOf);
			}
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x001CDCBB File Offset: 0x001CBEBB
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SmtpAddressesPredicate.CreateFromInternalCondition<AnyOfToCcHeaderMemberOfPredicate>(condition, "isMemberOf", "Message.ToCc");
		}

		// Token: 0x04003A2A RID: 14890
		private const string InternalPredicateName = "isMemberOf";

		// Token: 0x04003A2B RID: 14891
		private const string InternalPropertyName = "Message.ToCc";
	}
}
