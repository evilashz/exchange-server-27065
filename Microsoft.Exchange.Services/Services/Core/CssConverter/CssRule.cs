using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Services.Core.CssConverter
{
	// Token: 0x020000C3 RID: 195
	public class CssRule
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0001CA9A File Offset: 0x0001AC9A
		public CssRule(IList<string> selectors, IList<CssProperty> definitions)
		{
			this.Selectors = selectors.ToList<string>().ConvertAll<CssSelector>((string str) => new CssSelector(str));
			this.Definitions = definitions;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001CAD7 File Offset: 0x0001ACD7
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0001CADF File Offset: 0x0001ACDF
		public IList<CssSelector> Selectors { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		public IList<CssProperty> Definitions { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001CB01 File Offset: 0x0001AD01
		public bool IsDirective
		{
			get
			{
				return this.Selectors.Any((CssSelector s) => s.IsDirective);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001CB33 File Offset: 0x0001AD33
		public bool ContainsUnsafePseudoClasses
		{
			get
			{
				return this.Selectors.Any((CssSelector s) => s.ContainsUnsafePseudoClasses);
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001CB60 File Offset: 0x0001AD60
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (CssSelector value in this.Selectors)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(value);
				flag = false;
			}
			stringBuilder.Append("\n{ ");
			foreach (CssProperty value2 in this.Definitions)
			{
				stringBuilder.Append(value2);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
