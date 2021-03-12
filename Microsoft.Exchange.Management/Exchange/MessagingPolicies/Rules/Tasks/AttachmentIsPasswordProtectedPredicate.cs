using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BBD RID: 3005
	[ExceptionParameterName("ExceptIfAttachmentIsPasswordProtected")]
	[ConditionParameterName("AttachmentIsPasswordProtected")]
	[Serializable]
	public class AttachmentIsPasswordProtectedPredicate : TransportRulePredicate, IEquatable<AttachmentIsPasswordProtectedPredicate>
	{
		// Token: 0x06007136 RID: 28982 RVA: 0x001CE408 File Offset: 0x001CC608
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06007137 RID: 28983 RVA: 0x001CE40B File Offset: 0x001CC60B
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentIsPasswordProtectedPredicate)));
		}

		// Token: 0x06007138 RID: 28984 RVA: 0x001CE444 File Offset: 0x001CC644
		public bool Equals(AttachmentIsPasswordProtectedPredicate other)
		{
			return true;
		}

		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x06007139 RID: 28985 RVA: 0x001CE447 File Offset: 0x001CC647
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionAttachmentIsPasswordProtected;
			}
		}

		// Token: 0x0600713A RID: 28986 RVA: 0x001CE454 File Offset: 0x001CC654
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("attachmentIsPasswordProtected"))
			{
				return null;
			}
			return new AttachmentIsPasswordProtectedPredicate();
		}

		// Token: 0x0600713B RID: 28987 RVA: 0x001CE48C File Offset: 0x001CC68C
		internal override Condition ToInternalCondition()
		{
			return TransportRuleParser.Instance.CreatePredicate("attachmentIsPasswordProtected", null, new ShortList<string>());
		}

		// Token: 0x0600713C RID: 28988 RVA: 0x001CE4A3 File Offset: 0x001CC6A3
		internal override string GetPredicateParameters()
		{
			return "$true";
		}
	}
}
