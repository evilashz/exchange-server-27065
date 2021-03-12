using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Services.Core.CssConverter
{
	// Token: 0x020000C5 RID: 197
	public class CssStyleSheet
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x0001CF29 File Offset: 0x0001B129
		public CssStyleSheet(List<CssRule> rules)
		{
			this.Rules = rules;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001CF38 File Offset: 0x0001B138
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001CF40 File Offset: 0x0001B140
		public List<CssRule> Rules { get; private set; }

		// Token: 0x06000574 RID: 1396 RVA: 0x0001CF4C File Offset: 0x0001B14C
		public void ScopeRulesToClass(string className)
		{
			foreach (CssRule cssRule in this.Rules)
			{
				foreach (CssSelector cssSelector in cssRule.Selectors)
				{
					if (!cssSelector.IsDirective)
					{
						cssSelector.PrependClass(className);
					}
				}
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		public void SanitizeRules()
		{
			List<CssRule> list = new List<CssRule>();
			foreach (CssRule cssRule in this.Rules)
			{
				if (!cssRule.IsDirective)
				{
					if (cssRule.ContainsUnsafePseudoClasses)
					{
						List<CssSelector> list2 = new List<CssSelector>();
						foreach (CssSelector cssSelector in cssRule.Selectors)
						{
							if (!cssSelector.ContainsUnsafePseudoClasses)
							{
								list2.Add(cssSelector);
							}
						}
						cssRule.Selectors = list2;
					}
					if (cssRule.Selectors.Count > 0)
					{
						list.Add(cssRule);
					}
				}
			}
			this.Rules = list;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (CssRule value in this.Rules)
			{
				stringBuilder.Append(value);
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}
	}
}
