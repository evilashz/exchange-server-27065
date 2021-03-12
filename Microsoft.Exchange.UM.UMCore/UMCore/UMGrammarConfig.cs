using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000127 RID: 295
	internal abstract class UMGrammarConfig
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00022C28 File Offset: 0x00020E28
		internal UMGrammarConfig(string name, string rule, string conditionString, ActivityManagerConfig managerConfig)
		{
			this.grammarName = name;
			this.grammarRule = rule;
			if (conditionString.Length > 0)
			{
				this.conditionTree = ConditionParser.Instance.Parse(conditionString, managerConfig);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00022C5A File Offset: 0x00020E5A
		internal ExpressionParser.Expression Condition
		{
			get
			{
				return this.conditionTree;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00022C62 File Offset: 0x00020E62
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00022C6A File Offset: 0x00020E6A
		protected internal string GrammarName
		{
			internal get
			{
				return this.grammarName;
			}
			set
			{
				this.grammarName = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00022C73 File Offset: 0x00020E73
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x00022C7B File Offset: 0x00020E7B
		protected internal string GrammarRule
		{
			internal get
			{
				return this.grammarRule;
			}
			set
			{
				this.grammarRule = value;
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00022C84 File Offset: 0x00020E84
		internal static UMGrammarConfig Create(XmlNode node, string conditionString, ActivityManagerConfig managerConfig)
		{
			string value = node.Attributes["name"].Value;
			string value2 = node.Attributes["type"].Value;
			string value3 = node.Attributes["condition"].Value;
			string value4 = node.Attributes["rule"].Value;
			return UMGrammarConfig.Create(value, value2, value3, value4, conditionString, managerConfig);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00022CF4 File Offset: 0x00020EF4
		internal static UMGrammarConfig Create(string name, string type, string condition, string rule, string conditionString, ActivityManagerConfig managerConfig)
		{
			UMGrammarConfig umgrammarConfig = null;
			if (type != null)
			{
				if (!(type == "static"))
				{
					if (type == "dynamic")
					{
						umgrammarConfig = new DynamicUmGrammarConfig(name, rule, condition, managerConfig);
					}
				}
				else
				{
					umgrammarConfig = new StaticUmGrammarConfig(name, rule, condition, managerConfig);
				}
			}
			foreach (CultureInfo cultureInfo in GlobCfg.VuiCultures)
			{
				umgrammarConfig.Validate();
			}
			return umgrammarConfig;
		}

		// Token: 0x06000843 RID: 2115
		internal abstract UMGrammar GetGrammar(ActivityManager manager, CultureInfo culture);

		// Token: 0x06000844 RID: 2116
		internal abstract void Validate();

		// Token: 0x0400088D RID: 2189
		private string grammarName;

		// Token: 0x0400088E RID: 2190
		private string grammarRule;

		// Token: 0x0400088F RID: 2191
		private ExpressionParser.Expression conditionTree;
	}
}
