using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AB0 RID: 2736
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SequenceCompositePropertyRule : PropertyRule
	{
		// Token: 0x060063EB RID: 25579 RVA: 0x001A75D0 File Offset: 0x001A57D0
		public SequenceCompositePropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, params PropertyRule[] propertyRules) : base(name, onSetWriteEnforceLocationIdentifier, new PropertyReference[0])
		{
			IEnumerable<PropertyDefinition> enumerable = new List<PropertyDefinition>();
			IEnumerable<PropertyDefinition> enumerable2 = new List<PropertyDefinition>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PropertyRule propertyRule in propertyRules)
			{
				foreach (PropertyDefinition propertyDefinition in propertyRule.WriteProperties)
				{
					if (enumerable.Contains(propertyDefinition) || enumerable2.Contains(propertyDefinition))
					{
						throw new ArgumentException("ordered rule set has circular write to previous listed propreties. Property: " + propertyDefinition.Name);
					}
				}
				enumerable = enumerable.Union(propertyRule.ReadProperties);
				enumerable2 = enumerable2.Union(propertyRule.WriteProperties);
				stringBuilder.AppendFormat("{0};", propertyRule.ToString());
			}
			base.ReadProperties = enumerable.ToArray<PropertyDefinition>();
			base.WriteProperties = enumerable2.ToArray<PropertyDefinition>();
			this.ruleSequence = propertyRules;
			this.subRuleString = stringBuilder.ToString();
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x001A76E8 File Offset: 0x001A58E8
		protected override bool WriteEnforceRule(ICorePropertyBag propertyBag)
		{
			bool flag = false;
			for (int i = 0; i < this.ruleSequence.Length; i++)
			{
				flag |= this.ruleSequence[i].WriteEnforce(propertyBag);
			}
			return flag;
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x001A771C File Offset: 0x001A591C
		public override string ToString()
		{
			return this.Name + "(" + this.subRuleString + ")";
		}

		// Token: 0x040038A3 RID: 14499
		private readonly PropertyRule[] ruleSequence;

		// Token: 0x040038A4 RID: 14500
		private readonly string subRuleString = string.Empty;
	}
}
