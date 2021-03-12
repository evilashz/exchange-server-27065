using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB1 RID: 2993
	[ConditionParameterName("AnyOfRecipientAddressContainsWords")]
	[ExceptionParameterName("ExceptIfAnyOfRecipientAddressContainsWords")]
	[Serializable]
	public class AnyOfRecipientAddressContainsPredicate : SinglePropertyContainsPredicate, IEquatable<AnyOfRecipientAddressContainsPredicate>
	{
		// Token: 0x060070CD RID: 28877 RVA: 0x001CD731 File Offset: 0x001CB931
		public AnyOfRecipientAddressContainsPredicate() : base("Message.EnvelopeRecipients")
		{
		}

		// Token: 0x060070CE RID: 28878 RVA: 0x001CD73E File Offset: 0x001CB93E
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x001CD74B File Offset: 0x001CB94B
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AnyOfRecipientAddressContainsPredicate)));
		}

		// Token: 0x060070D0 RID: 28880 RVA: 0x001CD784 File Offset: 0x001CB984
		public bool Equals(AnyOfRecipientAddressContainsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002305 RID: 8965
		// (get) Token: 0x060070D1 RID: 28881 RVA: 0x001CD7A9 File Offset: 0x001CB9A9
		// (set) Token: 0x060070D2 RID: 28882 RVA: 0x001CD7B1 File Offset: 0x001CB9B1
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[ExceptionParameterName("ExceptIfAnyOfRecipientAddressContainsWords")]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("AnyOfRecipientAddressContainsWords")]
		public override Word[] Words
		{
			get
			{
				return this.words;
			}
			set
			{
				this.words = value;
			}
		}

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x060070D3 RID: 28883 RVA: 0x001CD7BA File Offset: 0x001CB9BA
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new ContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAnyOfRecipientAddressContains);
			}
		}

		// Token: 0x060070D4 RID: 28884 RVA: 0x001CD7C8 File Offset: 0x001CB9C8
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SinglePropertyContainsPredicate.CreateFromInternalCondition<AnyOfRecipientAddressContainsPredicate>(condition, "Message.EnvelopeRecipients");
		}

		// Token: 0x04003A24 RID: 14884
		private const string InternalPropertyName = "Message.EnvelopeRecipients";
	}
}
