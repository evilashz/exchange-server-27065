using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000018 RID: 24
	public class IsPredicate : PredicateCondition
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003436 File Offset: 0x00001636
		public IsPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsString || !base.Value.IsString)
			{
				throw new RulesValidationException(RulesStrings.StringPropertyOrValueRequired(this.Name));
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000346C File Offset: 0x0000166C
		public override string Name
		{
			get
			{
				return "is";
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003473 File Offset: 0x00001673
		public override Version MinimumVersion
		{
			get
			{
				if (string.CompareOrdinal(base.Property.Name, "Message.AttachmentTypes") == 0 || string.CompareOrdinal(base.Property.Name, "Message.AttachmentExtensions") == 0)
				{
					return IsPredicate.AttachmentPropertiesVersion;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000034B0 File Offset: 0x000016B0
		public override bool Evaluate(RulesEvaluationContext context)
		{
			object value = base.Property.GetValue(context);
			object value2 = base.Value.GetValue(context);
			List<string> list = new List<string>();
			bool flag = RuleUtils.CompareStringValues(value2, value, CaseInsensitiveStringComparer.Instance, base.EvaluationMode, list);
			base.UpdateEvaluationHistory(context, flag, list, 0);
			return flag;
		}

		// Token: 0x04000029 RID: 41
		public static readonly Version AttachmentPropertiesVersion = new Version("15.00.0001.001");
	}
}
