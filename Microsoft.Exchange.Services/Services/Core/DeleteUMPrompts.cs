using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.Prompts.Provisioning;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002D0 RID: 720
	internal sealed class DeleteUMPrompts : SingleStepServiceCommand<DeleteUMPromptsRequest, DeleteUMPromptsResponseMessage>
	{
		// Token: 0x06001407 RID: 5127 RVA: 0x00064355 File Offset: 0x00062555
		public DeleteUMPrompts(CallContext callContext, DeleteUMPromptsRequest request) : base(callContext, request)
		{
			this.configurationObject = request.ConfigurationObject;
			this.promptNames = request.PromptNames;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00064377 File Offset: 0x00062577
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new DeleteUMPromptsResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00064394 File Offset: 0x00062594
		internal override ServiceResult<DeleteUMPromptsResponseMessage> Execute()
		{
			using (XSOUMPromptStoreAccessor xsoumpromptStoreAccessor = new XSOUMPromptStoreAccessor(base.MailboxIdentityMailboxSession, this.configurationObject))
			{
				if (this.promptNames != null)
				{
					xsoumpromptStoreAccessor.DeletePrompts(this.promptNames);
				}
				else
				{
					xsoumpromptStoreAccessor.DeleteAllPrompts();
				}
			}
			return new ServiceResult<DeleteUMPromptsResponseMessage>(new DeleteUMPromptsResponseMessage());
		}

		// Token: 0x04000D7E RID: 3454
		private readonly Guid configurationObject;

		// Token: 0x04000D7F RID: 3455
		private string[] promptNames;
	}
}
