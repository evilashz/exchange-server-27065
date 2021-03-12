using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B80 RID: 2944
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AssignCategoriesAction : ActionBase
	{
		// Token: 0x06006A35 RID: 27189 RVA: 0x001C5F75 File Offset: 0x001C4175
		private AssignCategoriesAction(ActionType actionType, string[] categories, Rule rule) : base(actionType, rule)
		{
			this.categories = categories;
		}

		// Token: 0x06006A36 RID: 27190 RVA: 0x001C5F88 File Offset: 0x001C4188
		public static AssignCategoriesAction Create(string[] categories, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule
			});
			return new AssignCategoriesAction(ActionType.AssignCategoriesAction, categories, rule);
		}

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x06006A37 RID: 27191 RVA: 0x001C5FAF File Offset: 0x001C41AF
		public string[] Categories
		{
			get
			{
				return this.categories;
			}
		}

		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x06006A38 RID: 27192 RVA: 0x001C5FB7 File Offset: 0x001C41B7
		public override Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.Exchange14;
			}
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x001C5FBC File Offset: 0x001C41BC
		internal override RuleAction BuildRuleAction()
		{
			PropTag propTag = base.Rule.PropertyDefinitionToPropTagFromCache(Rule.NamedDefinitions[0]);
			return new RuleAction.Tag(new PropValue(propTag, this.categories));
		}

		// Token: 0x04003C6C RID: 15468
		private string[] categories;
	}
}
