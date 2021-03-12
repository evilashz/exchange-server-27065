using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000128 RID: 296
	internal class DynamicUmGrammarConfig : UMGrammarConfig
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x00022D84 File Offset: 0x00020F84
		internal DynamicUmGrammarConfig(string name, string rule, string condition, ActivityManagerConfig managerConfig) : base(name, rule, condition, managerConfig)
		{
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00022D94 File Offset: 0x00020F94
		internal override UMGrammar GetGrammar(ActivityManager manager, CultureInfo culture)
		{
			SearchGrammarFile searchGrammarFile = (SearchGrammarFile)manager.ReadVariable(base.GrammarName);
			return new UMGrammar(searchGrammarFile.FilePath, base.GrammarRule, culture, searchGrammarFile.BaseUri, false);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00022DCC File Offset: 0x00020FCC
		internal override void Validate()
		{
		}
	}
}
