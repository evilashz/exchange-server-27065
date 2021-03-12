using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EF2 RID: 3826
	internal class CopyMessageCommand : ExchangeServiceCommand<CopyMessageRequest, CopyMessageResponse>
	{
		// Token: 0x06006299 RID: 25241 RVA: 0x00134427 File Offset: 0x00132627
		public CopyMessageCommand(CopyMessageRequest request) : base(request)
		{
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x00134430 File Offset: 0x00132630
		protected override CopyMessageResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			Message result = messageProvider.Copy(base.Request.Id, base.Request.DestinationId);
			return new CopyMessageResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
