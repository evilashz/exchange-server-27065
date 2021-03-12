using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.Prompts.Provisioning;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000338 RID: 824
	internal sealed class GetUMPromptNames : SingleStepServiceCommand<GetUMPromptNamesRequest, GetUMPromptNamesResponseMessage>
	{
		// Token: 0x06001713 RID: 5907 RVA: 0x0007AAF4 File Offset: 0x00078CF4
		public GetUMPromptNames(CallContext callContext, GetUMPromptNamesRequest request) : base(callContext, request)
		{
			this.configurationObject = request.ConfigurationObject;
			this.hoursElapsedSinceLastModified = request.HoursElapsedSinceLastModified;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0007AB16 File Offset: 0x00078D16
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetUMPromptNamesResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0007AB40 File Offset: 0x00078D40
		internal override ServiceResult<GetUMPromptNamesResponseMessage> Execute()
		{
			string[] promptNames = null;
			using (XSOUMPromptStoreAccessor xsoumpromptStoreAccessor = new XSOUMPromptStoreAccessor(base.MailboxIdentityMailboxSession, this.configurationObject))
			{
				promptNames = ((this.hoursElapsedSinceLastModified == 0) ? xsoumpromptStoreAccessor.GetPromptNames() : xsoumpromptStoreAccessor.GetPromptNames(TimeSpan.FromHours((double)this.hoursElapsedSinceLastModified)));
			}
			return new ServiceResult<GetUMPromptNamesResponseMessage>(new GetUMPromptNamesResponseMessage
			{
				PromptNames = promptNames
			});
		}

		// Token: 0x04000FAB RID: 4011
		private readonly Guid configurationObject;

		// Token: 0x04000FAC RID: 4012
		private readonly int hoursElapsedSinceLastModified;
	}
}
