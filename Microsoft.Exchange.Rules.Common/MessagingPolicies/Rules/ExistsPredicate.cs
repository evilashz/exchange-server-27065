using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000019 RID: 25
	public class ExistsPredicate : PredicateCondition
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000350D File Offset: 0x0000170D
		public ExistsPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003518 File Offset: 0x00001718
		public override string Name
		{
			get
			{
				return "exists";
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003520 File Offset: 0x00001720
		public override bool Evaluate(RulesEvaluationContext context)
		{
			object value = base.Property.GetValue(context);
			if (value == null)
			{
				return false;
			}
			IEnumerable<string> enumerable = value as IEnumerable<string>;
			if (enumerable != null)
			{
				bool flag = enumerable.Any<string>();
				base.UpdateEvaluationHistory(context, flag, enumerable.ToList<string>(), 0);
				return flag;
			}
			base.UpdateEvaluationHistory(context, true, null, 0);
			return true;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000356C File Offset: 0x0000176C
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			if (entries.Count != 0)
			{
				throw new RulesValidationException(RulesStrings.ValueIsNotAllowed(this.Name));
			}
			return null;
		}
	}
}
