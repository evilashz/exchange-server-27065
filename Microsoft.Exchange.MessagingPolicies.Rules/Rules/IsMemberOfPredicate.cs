using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200009D RID: 157
	internal class IsMemberOfPredicate : PredicateCondition
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x00016983 File Offset: 0x00014B83
		public IsMemberOfPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsString)
			{
				throw new RulesValidationException(RulesStrings.StringPropertyOrValueRequired(this.Name));
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000169AC File Offset: 0x00014BAC
		public override string Name
		{
			get
			{
				return "isMemberOf";
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000169B4 File Offset: 0x00014BB4
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			BaseTransportRulesEvaluationContext baseTransportRulesEvaluationContext = (BaseTransportRulesEvaluationContext)baseContext;
			if (baseTransportRulesEvaluationContext == null)
			{
				throw new ArgumentException("context is either null or not of type: BaseTransportRulesEvaluationContext");
			}
			baseTransportRulesEvaluationContext.PredicateName = this.Name;
			object value = base.Property.GetValue(baseTransportRulesEvaluationContext);
			object value2 = base.Value.GetValue(baseTransportRulesEvaluationContext);
			if (value == null || baseTransportRulesEvaluationContext.MembershipChecker == null)
			{
				return false;
			}
			List<string> list = new List<string>();
			bool flag = RuleUtils.CompareStringValues(value2, value, baseTransportRulesEvaluationContext.MembershipChecker, base.EvaluationMode, list);
			base.UpdateEvaluationHistory(baseContext, flag, list, 0);
			return flag;
		}
	}
}
