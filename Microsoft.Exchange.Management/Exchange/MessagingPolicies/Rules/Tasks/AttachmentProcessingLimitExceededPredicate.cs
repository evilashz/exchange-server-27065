using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BC1 RID: 3009
	[ExceptionParameterName("ExceptIfAttachmentProcessingLimitExceeded")]
	[ConditionParameterName("AttachmentProcessingLimitExceeded")]
	[Serializable]
	public class AttachmentProcessingLimitExceededPredicate : TransportRulePredicate, IEquatable<AttachmentProcessingLimitExceededPredicate>
	{
		// Token: 0x0600715A RID: 29018 RVA: 0x001CE846 File Offset: 0x001CCA46
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x001CE849 File Offset: 0x001CCA49
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentProcessingLimitExceededPredicate)));
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x001CE882 File Offset: 0x001CCA82
		public bool Equals(AttachmentProcessingLimitExceededPredicate other)
		{
			return true;
		}

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x0600715D RID: 29021 RVA: 0x001CE885 File Offset: 0x001CCA85
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionAttachmentProcessingLimitExceeded;
			}
		}

		// Token: 0x0600715E RID: 29022 RVA: 0x001CE894 File Offset: 0x001CCA94
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("attachmentProcessingLimitExceeded"))
			{
				return null;
			}
			return new AttachmentProcessingLimitExceededPredicate();
		}

		// Token: 0x0600715F RID: 29023 RVA: 0x001CE8CC File Offset: 0x001CCACC
		internal override Condition ToInternalCondition()
		{
			return TransportRuleParser.Instance.CreatePredicate("attachmentProcessingLimitExceeded", null, new ShortList<string>());
		}

		// Token: 0x06007160 RID: 29024 RVA: 0x001CE8E3 File Offset: 0x001CCAE3
		internal override string GetPredicateParameters()
		{
			return "$true";
		}
	}
}
