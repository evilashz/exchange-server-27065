using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BBE RID: 3006
	[ConditionParameterName("AttachmentIsUnsupported")]
	[ExceptionParameterName("ExceptIfAttachmentIsUnsupported")]
	[Serializable]
	public class AttachmentIsUnsupportedPredicate : TransportRulePredicate, IEquatable<AttachmentIsUnsupportedPredicate>
	{
		// Token: 0x0600713E RID: 28990 RVA: 0x001CE4B2 File Offset: 0x001CC6B2
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600713F RID: 28991 RVA: 0x001CE4B5 File Offset: 0x001CC6B5
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentIsUnsupportedPredicate)));
		}

		// Token: 0x06007140 RID: 28992 RVA: 0x001CE4EE File Offset: 0x001CC6EE
		public bool Equals(AttachmentIsUnsupportedPredicate other)
		{
			return true;
		}

		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x06007141 RID: 28993 RVA: 0x001CE4F1 File Offset: 0x001CC6F1
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionAttachmentIsUnsupported;
			}
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x001CE500 File Offset: 0x001CC700
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("attachmentIsUnsupported"))
			{
				return null;
			}
			return new AttachmentIsUnsupportedPredicate();
		}

		// Token: 0x06007143 RID: 28995 RVA: 0x001CE53C File Offset: 0x001CC73C
		internal override Condition ToInternalCondition()
		{
			ShortList<string> valueEntries = new ShortList<string>();
			return TransportRuleParser.Instance.CreatePredicate("attachmentIsUnsupported", null, valueEntries);
		}

		// Token: 0x06007144 RID: 28996 RVA: 0x001CE562 File Offset: 0x001CC762
		internal override string GetPredicateParameters()
		{
			return "$true";
		}
	}
}
