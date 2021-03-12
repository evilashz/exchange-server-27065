using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA3 RID: 2979
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AssignedCategoriesCondition : StringCondition
	{
		// Token: 0x06006ABE RID: 27326 RVA: 0x001C7A21 File Offset: 0x001C5C21
		private AssignedCategoriesCondition(Rule rule, string[] text) : base(ConditionType.AssignedCategoriesCondition, rule, text)
		{
		}

		// Token: 0x06006ABF RID: 27327 RVA: 0x001C7A30 File Offset: 0x001C5C30
		public static AssignedCategoriesCondition Create(Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				text
			});
			return new AssignedCategoriesCondition(rule, text);
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x001C7A5C File Offset: 0x001C5C5C
		internal override Restriction BuildRestriction()
		{
			PropTag propertyTag = base.Rule.PropertyDefinitionToPropTagFromCache(Rule.NamedDefinitions[0]);
			return (1 == base.Text.Length) ? Condition.CreatePropertyRestriction<string>(propertyTag, base.Text[0]) : Condition.CreateAndStringPropertyRestriction(propertyTag, base.Text);
		}
	}
}
