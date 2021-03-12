using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB9 RID: 3001
	[Serializable]
	public class AnyOfToHeaderPredicate : SmtpAddressesPredicate, IEquatable<AnyOfToHeaderPredicate>
	{
		// Token: 0x0600710B RID: 28939 RVA: 0x001CDE29 File Offset: 0x001CC029
		public AnyOfToHeaderPredicate() : base("isSameUser", "Message.To")
		{
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x001CDE3B File Offset: 0x001CC03B
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x001CDE48 File Offset: 0x001CC048
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AnyOfToHeaderPredicate)));
		}

		// Token: 0x0600710E RID: 28942 RVA: 0x001CDE81 File Offset: 0x001CC081
		public bool Equals(AnyOfToHeaderPredicate other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x0600710F RID: 28943 RVA: 0x001CDEA6 File Offset: 0x001CC0A6
		// (set) Token: 0x06007110 RID: 28944 RVA: 0x001CDEAE File Offset: 0x001CC0AE
		[ConditionParameterName("AnyOfToHeader")]
		[ExceptionParameterName("ExceptIfAnyOfToHeader")]
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

		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x06007111 RID: 28945 RVA: 0x001CDEB7 File Offset: 0x001CC0B7
		protected override SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressesPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAnyOfToHeader);
			}
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x001CDEC5 File Offset: 0x001CC0C5
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SmtpAddressesPredicate.CreateFromInternalCondition<AnyOfToHeaderPredicate>(condition, "isSameUser", "Message.To");
		}

		// Token: 0x04003A30 RID: 14896
		private const string InternalPredicateName = "isSameUser";

		// Token: 0x04003A31 RID: 14897
		private const string InternalPropertyName = "Message.To";
	}
}
