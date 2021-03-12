using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002D4 RID: 724
	internal abstract class InstantMessageConversationCommand : InstantMessageCommandBase<int>
	{
		// Token: 0x06001889 RID: 6281 RVA: 0x00054520 File Offset: 0x00052720
		public InstantMessageConversationCommand(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0005452C File Offset: 0x0005272C
		protected override int InternalExecute()
		{
			InstantMessageOperationError instantMessageOperationError = this.ExecuteInstantMessagingCommand();
			if (instantMessageOperationError <= InstantMessageOperationError.Success)
			{
				OwaApplication.GetRequestDetailsLogger.Set(InstantMessagingLogMetadata.OperationErrorCode, instantMessageOperationError);
			}
			else
			{
				OwaApplication.GetRequestDetailsLogger.Set(InstantMessagingLogMetadata.ConversationId, (int)instantMessageOperationError);
			}
			return (int)instantMessageOperationError;
		}

		// Token: 0x0600188B RID: 6283
		protected abstract InstantMessageOperationError ExecuteInstantMessagingCommand();
	}
}
