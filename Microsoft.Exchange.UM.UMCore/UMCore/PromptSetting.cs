using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001B2 RID: 434
	internal struct PromptSetting
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x000372DE File Offset: 0x000354DE
		public PromptSetting(PromptConfigBase promptConfig)
		{
			this.ProsodyRate = promptConfig.ProsodyRate;
			this.PromptName = promptConfig.PromptName;
			this.Language = promptConfig.Language;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00037304 File Offset: 0x00035504
		public PromptSetting(string name)
		{
			this.PromptName = string.Intern(name.Trim());
			this.Language = string.Empty;
			this.ProsodyRate = "+0%";
		}

		// Token: 0x04000A43 RID: 2627
		public string ProsodyRate;

		// Token: 0x04000A44 RID: 2628
		public string PromptName;

		// Token: 0x04000A45 RID: 2629
		public string Language;
	}
}
