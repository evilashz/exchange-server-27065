using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BBC RID: 3004
	[ConditionParameterName("AttachmentHasExecutableContent")]
	[ExceptionParameterName("ExceptIfAttachmentHasExecutableContent")]
	[Serializable]
	public class AttachmentHasExecutableContentPredicate : TransportRulePredicate, IEquatable<AttachmentHasExecutableContentPredicate>
	{
		// Token: 0x0600712E RID: 28974 RVA: 0x001CE2F0 File Offset: 0x001CC4F0
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x001CE2F3 File Offset: 0x001CC4F3
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentHasExecutableContentPredicate)));
		}

		// Token: 0x06007130 RID: 28976 RVA: 0x001CE32C File Offset: 0x001CC52C
		public bool Equals(AttachmentHasExecutableContentPredicate other)
		{
			return true;
		}

		// Token: 0x17002319 RID: 8985
		// (get) Token: 0x06007131 RID: 28977 RVA: 0x001CE32F File Offset: 0x001CC52F
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionAttachmentHasExecutableContent;
			}
		}

		// Token: 0x06007132 RID: 28978 RVA: 0x001CE33C File Offset: 0x001CC53C
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("is") || !predicateCondition.Property.Name.Equals("Message.AttachmentTypes") || predicateCondition.Value.RawValues.Count != 1 || !predicateCondition.Value.RawValues[0].Equals("executable"))
			{
				return null;
			}
			return new AttachmentHasExecutableContentPredicate();
		}

		// Token: 0x06007133 RID: 28979 RVA: 0x001CE3BC File Offset: 0x001CC5BC
		internal override Condition ToInternalCondition()
		{
			return TransportRuleParser.Instance.CreatePredicate("is", TransportRuleParser.Instance.CreateProperty("Message.AttachmentTypes"), new ShortList<string>
			{
				"executable"
			});
		}

		// Token: 0x06007134 RID: 28980 RVA: 0x001CE3F9 File Offset: 0x001CC5F9
		internal override string GetPredicateParameters()
		{
			return "$true";
		}
	}
}
