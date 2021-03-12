using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA8 RID: 2984
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MessageClassificationCondition : StringCondition
	{
		// Token: 0x06006ACB RID: 27339 RVA: 0x001C7C65 File Offset: 0x001C5E65
		private MessageClassificationCondition(Rule rule, string[] text) : base(ConditionType.MessageClassificationCondition, rule, text)
		{
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x001C7C74 File Offset: 0x001C5E74
		public static MessageClassificationCondition Create(Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new MessageClassificationCondition(rule, text);
		}

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x06006ACD RID: 27341 RVA: 0x001C7C99 File Offset: 0x001C5E99
		public override Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.Exchange14;
			}
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x001C7C9C File Offset: 0x001C5E9C
		internal override Restriction BuildRestriction()
		{
			PropTag propertyTag = base.Rule.PropertyDefinitionToPropTagFromCache(Rule.NamedDefinitions[1]);
			if (base.Text == null || base.Text.Length == 0)
			{
				return Condition.CreatePropertyRestriction<bool>(propertyTag, true);
			}
			PropTag propertyTag2 = base.Rule.PropertyDefinitionToPropTagFromCache(Rule.NamedDefinitions[2]);
			Restriction restriction = (1 == base.Text.Length) ? Condition.CreatePropertyRestriction<string>(propertyTag2, base.Text[0]) : Condition.CreateAndStringPropertyRestriction(propertyTag2, base.Text);
			return Condition.CreateAndRestriction(new Restriction[]
			{
				Condition.CreatePropertyRestriction<bool>(propertyTag, true),
				restriction
			});
		}
	}
}
