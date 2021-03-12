using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BCA RID: 3018
	[Serializable]
	public class ContentCharacterSetContainsWordsPredicate : SinglePropertyContainsPredicate, IEquatable<ContentCharacterSetContainsWordsPredicate>
	{
		// Token: 0x060071AC RID: 29100 RVA: 0x001CF840 File Offset: 0x001CDA40
		public ContentCharacterSetContainsWordsPredicate() : base("Message.ContentCharacterSets")
		{
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x001CF84D File Offset: 0x001CDA4D
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x001CF85A File Offset: 0x001CDA5A
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as ContentCharacterSetContainsWordsPredicate)));
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x001CF893 File Offset: 0x001CDA93
		public bool Equals(ContentCharacterSetContainsWordsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x060071B0 RID: 29104 RVA: 0x001CF8B8 File Offset: 0x001CDAB8
		// (set) Token: 0x060071B1 RID: 29105 RVA: 0x001CF8C0 File Offset: 0x001CDAC0
		[ExceptionParameterName("ExceptIfContentCharacterSetContainsWords")]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("ContentCharacterSetContainsWords")]
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

		// Token: 0x17002331 RID: 9009
		// (get) Token: 0x060071B2 RID: 29106 RVA: 0x001CF8C9 File Offset: 0x001CDAC9
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new ContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionContentCharacterSetContainsWords);
			}
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x001CF8D7 File Offset: 0x001CDAD7
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SinglePropertyContainsPredicate.CreateFromInternalCondition<ContentCharacterSetContainsWordsPredicate>(condition, "Message.ContentCharacterSets");
		}

		// Token: 0x04003A43 RID: 14915
		private const string InternalPropertyName = "Message.ContentCharacterSets";
	}
}
