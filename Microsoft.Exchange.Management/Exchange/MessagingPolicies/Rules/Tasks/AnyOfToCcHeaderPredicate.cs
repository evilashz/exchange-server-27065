using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB7 RID: 2999
	[Serializable]
	public class AnyOfToCcHeaderPredicate : SmtpAddressesPredicate, IEquatable<AnyOfToCcHeaderPredicate>
	{
		// Token: 0x060070FB RID: 28923 RVA: 0x001CDCCD File Offset: 0x001CBECD
		public AnyOfToCcHeaderPredicate() : base("isSameUser", "Message.ToCc")
		{
		}

		// Token: 0x060070FC RID: 28924 RVA: 0x001CDCDF File Offset: 0x001CBEDF
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x060070FD RID: 28925 RVA: 0x001CDCEC File Offset: 0x001CBEEC
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AnyOfToCcHeaderPredicate)));
		}

		// Token: 0x060070FE RID: 28926 RVA: 0x001CDD25 File Offset: 0x001CBF25
		public bool Equals(AnyOfToCcHeaderPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x1700230F RID: 8975
		// (get) Token: 0x060070FF RID: 28927 RVA: 0x001CDD4A File Offset: 0x001CBF4A
		// (set) Token: 0x06007100 RID: 28928 RVA: 0x001CDD52 File Offset: 0x001CBF52
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[ExceptionParameterName("ExceptIfAnyOfToCcHeader")]
		[ConditionParameterName("AnyOfToCcHeader")]
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
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

		// Token: 0x17002310 RID: 8976
		// (get) Token: 0x06007101 RID: 28929 RVA: 0x001CDD5B File Offset: 0x001CBF5B
		protected override SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressesPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAnyOfToCcHeader);
			}
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x001CDD69 File Offset: 0x001CBF69
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SmtpAddressesPredicate.CreateFromInternalCondition<AnyOfToCcHeaderPredicate>(condition, "isSameUser", "Message.ToCc");
		}

		// Token: 0x04003A2C RID: 14892
		private const string InternalPredicateName = "isSameUser";

		// Token: 0x04003A2D RID: 14893
		private const string InternalPropertyName = "Message.ToCc";
	}
}
