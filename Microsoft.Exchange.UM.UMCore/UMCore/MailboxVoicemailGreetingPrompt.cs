using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000172 RID: 370
	internal class MailboxVoicemailGreetingPrompt : MailboxGreetingPrompt
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002DF00 File Offset: 0x0002C100
		protected override string PromptType
		{
			get
			{
				return "mbxVoicemailGreeting";
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002DF07 File Offset: 0x0002C107
		protected override PromptConfigBase PromptConfig
		{
			get
			{
				return GlobCfg.DefaultPrompts.VoicemailGreeting;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002DF13 File Offset: 0x0002C113
		protected override PromptConfigBase PreviewConfig
		{
			get
			{
				return GlobCfg.DefaultPromptsForPreview.MbxVoicemailGreeting;
			}
		}
	}
}
