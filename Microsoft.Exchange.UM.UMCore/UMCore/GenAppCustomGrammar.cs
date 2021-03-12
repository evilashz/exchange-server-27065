using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000145 RID: 325
	internal class GenAppCustomGrammar : CustomGrammarBase
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x00027158 File Offset: 0x00025358
		internal GenAppCustomGrammar(CultureInfo transctiptionLanguage, List<CustomGrammarBase> genAppGrammars) : base(transctiptionLanguage)
		{
			this.genAppGrammars = new List<CustomGrammarBase>(genAppGrammars.Count);
			foreach (CustomGrammarBase item in genAppGrammars)
			{
				this.genAppGrammars.Add(item);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x000271C4 File Offset: 0x000253C4
		internal override string FileName
		{
			get
			{
				return "ExtGenAppRule.grxml";
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x000271CB File Offset: 0x000253CB
		internal override string Rule
		{
			get
			{
				return "ExtGenAppRule";
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000271D4 File Offset: 0x000253D4
		internal override void WriteCustomGrammar(string customGrammarDir)
		{
			base.WriteCustomGrammar(customGrammarDir);
			foreach (CustomGrammarBase customGrammarBase in this.genAppGrammars)
			{
				customGrammarBase.WriteCustomGrammar(customGrammarDir);
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00027230 File Offset: 0x00025430
		protected override List<GrammarItemBase> GetItems()
		{
			List<GrammarItemBase> list = new List<GrammarItemBase>(this.genAppGrammars.Count);
			foreach (CustomGrammarBase grammar in this.genAppGrammars)
			{
				list.Add(new GrammarRuleRef(grammar, true));
			}
			return list;
		}

		// Token: 0x040008C7 RID: 2247
		private readonly List<CustomGrammarBase> genAppGrammars;
	}
}
