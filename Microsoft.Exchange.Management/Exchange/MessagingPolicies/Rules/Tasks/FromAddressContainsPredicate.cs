using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BCB RID: 3019
	[Serializable]
	public class FromAddressContainsPredicate : SinglePropertyContainsPredicate, IEquatable<FromAddressContainsPredicate>
	{
		// Token: 0x060071B4 RID: 29108 RVA: 0x001CF8E4 File Offset: 0x001CDAE4
		public FromAddressContainsPredicate() : base("Message.From")
		{
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x001CF8F1 File Offset: 0x001CDAF1
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x060071B6 RID: 29110 RVA: 0x001CF8FE File Offset: 0x001CDAFE
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as FromAddressContainsPredicate)));
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x001CF937 File Offset: 0x001CDB37
		public bool Equals(FromAddressContainsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002332 RID: 9010
		// (get) Token: 0x060071B8 RID: 29112 RVA: 0x001CF95C File Offset: 0x001CDB5C
		// (set) Token: 0x060071B9 RID: 29113 RVA: 0x001CF964 File Offset: 0x001CDB64
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("FromAddressContainsWords")]
		[ExceptionParameterName("ExceptIfFromAddressContainsWords")]
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

		// Token: 0x17002333 RID: 9011
		// (get) Token: 0x060071BA RID: 29114 RVA: 0x001CF96D File Offset: 0x001CDB6D
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new ContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionFromAddressContains);
			}
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x001CF97B File Offset: 0x001CDB7B
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SinglePropertyContainsPredicate.CreateFromInternalCondition<FromAddressContainsPredicate>(condition, "Message.From");
		}

		// Token: 0x04003A44 RID: 14916
		private const string InternalPropertyName = "Message.From";
	}
}
