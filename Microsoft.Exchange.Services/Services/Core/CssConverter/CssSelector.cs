using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.CssConverter
{
	// Token: 0x020000C4 RID: 196
	public class CssSelector
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		public CssSelector()
		{
			this.Tag = string.Empty;
			this.Id = string.Empty;
			this.PseudoClass = string.Empty;
			this.Classes = new List<string>();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001CC60 File Offset: 0x0001AE60
		public CssSelector(string selectorString) : this()
		{
			this.selectorString = selectorString;
			this.Parse(selectorString);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001CC76 File Offset: 0x0001AE76
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001CC7E File Offset: 0x0001AE7E
		public string Tag { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001CC87 File Offset: 0x0001AE87
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0001CC8F File Offset: 0x0001AE8F
		public string Id { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001CC98 File Offset: 0x0001AE98
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
		public string PseudoClass { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001CCA9 File Offset: 0x0001AEA9
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001CCB1 File Offset: 0x0001AEB1
		public IList<string> Classes { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0001CCBA File Offset: 0x0001AEBA
		public bool IsDirective
		{
			get
			{
				return this.selectorString.StartsWith("@");
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001CCCC File Offset: 0x0001AECC
		public bool ContainsUnsafePseudoClasses
		{
			get
			{
				return !string.IsNullOrEmpty(this.PseudoClass) && !CssSelector.SafePseudoClasses.Contains(this.PseudoClass);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		public override string ToString()
		{
			return this.selectorString;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		public void PrependClass(string className)
		{
			this.selectorString = string.Format(".{0} {1}", className, this.selectorString);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001CD14 File Offset: 0x0001AF14
		private void Parse(string selectorString)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			for (int i = 0; i < selectorString.Length; i++)
			{
				char c = selectorString[i];
				if (c != '#')
				{
					if (c != '.')
					{
						if (c == ':')
						{
							num3 = i;
						}
					}
					else if (num2 == -1)
					{
						num2 = i;
					}
				}
				else
				{
					num = i;
				}
			}
			int[] array = new int[]
			{
				num,
				num2,
				num3,
				selectorString.Length
			};
			Array.Sort<int>(array);
			int num4 = -1;
			int num5 = 0;
			while (num5 + 1 < array.Length)
			{
				if (array[num5] != -1)
				{
					if (num4 == -1)
					{
						num4 = array[num5];
					}
					int length = array[num5 + 1] - array[num5];
					string text = selectorString.Substring(array[num5], length);
					if (!string.IsNullOrEmpty(text))
					{
						if (array[num5] == num)
						{
							this.Id = text.Substring(1);
						}
						else if (array[num5] == num2)
						{
							this.Classes = CssParser.SplitToList(text, CssParser.ClassDelimiter, int.MaxValue);
						}
						else if (array[num5] == num3)
						{
							this.PseudoClass = text.Substring(1);
						}
					}
				}
				num5++;
			}
			if (num4 == -1)
			{
				num4 = selectorString.Length;
			}
			this.Tag = selectorString.Substring(0, num4).ToLowerInvariant();
		}

		// Token: 0x0400068B RID: 1675
		private static readonly HashSet<string> SafePseudoClasses = new HashSet<string>
		{
			"after",
			"before",
			"empty",
			"first-child",
			"first-letter",
			"first-line",
			"first-of-type",
			"last-child",
			"last-of-type",
			"nth-child",
			"nth-last-child",
			"nth-last-of-type",
			"nth-of-type",
			"only-child",
			"only-of-type"
		};

		// Token: 0x0400068C RID: 1676
		private string selectorString;
	}
}
