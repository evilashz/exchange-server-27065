using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000101 RID: 257
	internal class ConstTextPromptConfig : PromptConfigBase
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x0001BC34 File Offset: 0x00019E34
		internal ConstTextPromptConfig(string promptName, string promptText, string conditionString, CultureInfo culture, ActivityManagerConfig managerConfig) : base(promptName, "text", conditionString, managerConfig)
		{
			TextPrompt textPrompt = new TextPrompt();
			PromptSetting config = new PromptSetting(this);
			textPrompt.Initialize(config, culture, promptText);
			this.textPrompts = new ArrayList();
			this.textPrompts.Add(textPrompt);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001BC80 File Offset: 0x00019E80
		internal override void AddPrompts(ArrayList promptList, ActivityManager manager, CultureInfo culture)
		{
			promptList.AddRange(this.textPrompts);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001BC8E File Offset: 0x00019E8E
		internal override void Validate()
		{
		}

		// Token: 0x040007DA RID: 2010
		private ArrayList textPrompts;
	}
}
