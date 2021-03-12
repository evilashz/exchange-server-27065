using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD3 RID: 3027
	[ConditionParameterName("HasSenderOverride")]
	[ExceptionParameterName("ExceptIfHasSenderOverride")]
	[Serializable]
	public class HasSenderOverridePredicate : TransportRulePredicate, IEquatable<HasSenderOverridePredicate>
	{
		// Token: 0x060071F7 RID: 29175 RVA: 0x001D01CD File Offset: 0x001CE3CD
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x001D01D0 File Offset: 0x001CE3D0
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as HasSenderOverridePredicate)));
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x001D0209 File Offset: 0x001CE409
		public bool Equals(HasSenderOverridePredicate other)
		{
			return true;
		}

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x060071FA RID: 29178 RVA: 0x001D020C File Offset: 0x001CE40C
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionHasSenderOverride;
			}
		}

		// Token: 0x17002343 RID: 9027
		// (get) Token: 0x060071FB RID: 29179 RVA: 0x001D0218 File Offset: 0x001CE418
		[LocDescription(RulesTasksStrings.IDs.SubTypeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.SubTypeDisplayName)]
		public override IEnumerable<RuleSubType> RuleSubTypes
		{
			get
			{
				return new RuleSubType[]
				{
					RuleSubType.None
				};
			}
		}

		// Token: 0x060071FC RID: 29180 RVA: 0x001D0234 File Offset: 0x001CE434
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("hasSenderOverride") || !predicateCondition.Property.Name.Equals("X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification"))
			{
				return null;
			}
			return new HasSenderOverridePredicate();
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x001D0284 File Offset: 0x001CE484
		internal override Condition ToInternalCondition()
		{
			ShortList<string> valueEntries = new ShortList<string>();
			TransportRuleParser instance = TransportRuleParser.Instance;
			return instance.CreatePredicate("hasSenderOverride", instance.CreateProperty("Message.Headers:X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification"), valueEntries);
		}

		// Token: 0x060071FE RID: 29182 RVA: 0x001D02B4 File Offset: 0x001CE4B4
		internal override string GetPredicateParameters()
		{
			return "$true";
		}

		// Token: 0x04003A50 RID: 14928
		private const string PropertyBaseName = "X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification";

		// Token: 0x04003A51 RID: 14929
		private const string PropertyName = "Message.Headers:X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification";
	}
}
