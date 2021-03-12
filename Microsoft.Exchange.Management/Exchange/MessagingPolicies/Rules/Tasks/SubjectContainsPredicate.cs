using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BEF RID: 3055
	[Serializable]
	public class SubjectContainsPredicate : SinglePropertyContainsPredicate, IEquatable<SubjectContainsPredicate>
	{
		// Token: 0x0600733A RID: 29498 RVA: 0x001D4E80 File Offset: 0x001D3080
		public SubjectContainsPredicate() : base("Message.Subject")
		{
		}

		// Token: 0x0600733B RID: 29499 RVA: 0x001D4E8D File Offset: 0x001D308D
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x0600733C RID: 29500 RVA: 0x001D4E9A File Offset: 0x001D309A
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SubjectContainsPredicate)));
		}

		// Token: 0x0600733D RID: 29501 RVA: 0x001D4ED3 File Offset: 0x001D30D3
		public bool Equals(SubjectContainsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002379 RID: 9081
		// (get) Token: 0x0600733E RID: 29502 RVA: 0x001D4EF8 File Offset: 0x001D30F8
		// (set) Token: 0x0600733F RID: 29503 RVA: 0x001D4F00 File Offset: 0x001D3100
		[ConditionParameterName("SubjectContainsWords")]
		[ExceptionParameterName("ExceptIfSubjectContainsWords")]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
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

		// Token: 0x1700237A RID: 9082
		// (get) Token: 0x06007340 RID: 29504 RVA: 0x001D4F09 File Offset: 0x001D3109
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new ContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionSubjectContains);
			}
		}

		// Token: 0x06007341 RID: 29505 RVA: 0x001D4F17 File Offset: 0x001D3117
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SinglePropertyContainsPredicate.CreateFromInternalCondition<SubjectContainsPredicate>(condition, "Message.Subject");
		}

		// Token: 0x04003A92 RID: 14994
		private const string InternalPropertyName = "Message.Subject";
	}
}
