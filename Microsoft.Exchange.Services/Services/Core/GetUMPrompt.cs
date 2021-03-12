using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.Prompts.Provisioning;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000337 RID: 823
	internal sealed class GetUMPrompt : SingleStepServiceCommand<GetUMPromptRequest, GetUMPromptResponseMessage>
	{
		// Token: 0x06001710 RID: 5904 RVA: 0x0007AA48 File Offset: 0x00078C48
		public GetUMPrompt(CallContext callContext, GetUMPromptRequest request) : base(callContext, request)
		{
			this.configurationObject = request.ConfigurationObject;
			this.promptName = request.PromptName;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0007AA6A File Offset: 0x00078C6A
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetUMPromptResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0007AA94 File Offset: 0x00078C94
		internal override ServiceResult<GetUMPromptResponseMessage> Execute()
		{
			string audioData = null;
			using (XSOUMPromptStoreAccessor xsoumpromptStoreAccessor = new XSOUMPromptStoreAccessor(base.MailboxIdentityMailboxSession, this.configurationObject))
			{
				audioData = xsoumpromptStoreAccessor.GetPrompt(this.promptName);
			}
			return new ServiceResult<GetUMPromptResponseMessage>(new GetUMPromptResponseMessage
			{
				AudioData = audioData
			});
		}

		// Token: 0x04000FA9 RID: 4009
		private readonly Guid configurationObject;

		// Token: 0x04000FAA RID: 4010
		private readonly string promptName;
	}
}
