using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001A RID: 26
	public class NotExistsPredicate : ExistsPredicate
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003588 File Offset: 0x00001788
		public NotExistsPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003593 File Offset: 0x00001793
		public override string Name
		{
			get
			{
				return "notExists";
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000359A File Offset: 0x0000179A
		public override Version MinimumVersion
		{
			get
			{
				return new Version("2.7");
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000035A8 File Offset: 0x000017A8
		public override bool Evaluate(RulesEvaluationContext context)
		{
			object value = base.Property.GetValue(context);
			if (value == null)
			{
				return true;
			}
			IEnumerable<string> enumerable = value as IEnumerable<string>;
			if (enumerable != null)
			{
				bool flag = !enumerable.Any<string>();
				base.UpdateEvaluationHistory(context, flag, new List<string>(), 0);
				return flag;
			}
			base.UpdateEvaluationHistory(context, true, null, 0);
			return false;
		}
	}
}
