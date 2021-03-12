using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001B4 RID: 436
	internal class PromptConfig<TPrompt> : PromptConfigBase where TPrompt : Prompt, new()
	{
		// Token: 0x06000CBC RID: 3260 RVA: 0x00037360 File Offset: 0x00035560
		internal PromptConfig(string name, string type, string conditionString, ActivityManagerConfig managerConfig) : base(name, type, conditionString, managerConfig)
		{
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00037370 File Offset: 0x00035570
		internal override void AddPrompts(ArrayList promptList, ActivityManager manager, CultureInfo culture)
		{
			TPrompt tprompt = Activator.CreateInstance<TPrompt>();
			PromptSetting config = new PromptSetting(this);
			tprompt.Initialize(config, culture);
			if (manager != null)
			{
				tprompt.SetProsodyRate(manager.ProsodyRate);
				tprompt.TTSLanguage = manager.MessagePlayerContext.Language;
			}
			promptList.Add(tprompt);
		}
	}
}
