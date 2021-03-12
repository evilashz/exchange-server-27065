using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.Prompts.Provisioning;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002C6 RID: 710
	internal sealed class CreateUMPrompt : SingleStepServiceCommand<CreateUMPromptRequest, CreateUMPromptResponseMessage>
	{
		// Token: 0x060013A7 RID: 5031 RVA: 0x00062594 File Offset: 0x00060794
		public CreateUMPrompt(CallContext callContext, CreateUMPromptRequest request) : base(callContext, request)
		{
			this.configurationObject = request.ConfigurationObject;
			this.promptName = request.PromptName;
			this.audioData = request.AudioData;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000625C2 File Offset: 0x000607C2
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new CreateUMPromptResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000625E0 File Offset: 0x000607E0
		internal override ServiceResult<CreateUMPromptResponseMessage> Execute()
		{
			using (XSOUMPromptStoreAccessor xsoumpromptStoreAccessor = new XSOUMPromptStoreAccessor(base.MailboxIdentityMailboxSession, this.configurationObject))
			{
				xsoumpromptStoreAccessor.CreatePrompt(this.promptName, this.audioData);
			}
			return new ServiceResult<CreateUMPromptResponseMessage>(new CreateUMPromptResponseMessage());
		}

		// Token: 0x04000D61 RID: 3425
		private readonly Guid configurationObject;

		// Token: 0x04000D62 RID: 3426
		private readonly string promptName;

		// Token: 0x04000D63 RID: 3427
		private readonly string audioData;
	}
}
