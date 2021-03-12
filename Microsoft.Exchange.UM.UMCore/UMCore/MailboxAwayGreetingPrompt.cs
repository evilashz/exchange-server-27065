using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000171 RID: 369
	internal class MailboxAwayGreetingPrompt : MailboxGreetingPrompt
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002DED9 File Offset: 0x0002C0D9
		protected override string PromptType
		{
			get
			{
				return "mbxAwayGreeting";
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002DEE0 File Offset: 0x0002C0E0
		protected override PromptConfigBase PromptConfig
		{
			get
			{
				return GlobCfg.DefaultPrompts.AwayGreeting;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002DEEC File Offset: 0x0002C0EC
		protected override PromptConfigBase PreviewConfig
		{
			get
			{
				return GlobCfg.DefaultPromptsForPreview.MbxAwayGreeting;
			}
		}
	}
}
