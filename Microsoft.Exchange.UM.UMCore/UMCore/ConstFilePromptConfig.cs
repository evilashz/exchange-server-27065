using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020000FF RID: 255
	internal class ConstFilePromptConfig : PromptConfigBase
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x0001BB94 File Offset: 0x00019D94
		internal ConstFilePromptConfig(string fileName, string conditionString, CultureInfo culture, ActivityManagerConfig managerConfig) : base(fileName, "wave", conditionString, managerConfig)
		{
			this.filePrompt = new FilePrompt();
			PromptSetting config = new PromptSetting(this);
			this.filePrompt.Initialize(config, culture);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001BBD0 File Offset: 0x00019DD0
		internal override void AddPrompts(ArrayList promptList, ActivityManager manager, CultureInfo culture)
		{
			promptList.Add(this.filePrompt);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001BBDF File Offset: 0x00019DDF
		internal override void Validate()
		{
		}

		// Token: 0x040007D8 RID: 2008
		private FilePrompt filePrompt;
	}
}
