using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000335 RID: 821
	internal class InstantMessageSignOut : InstantMessageCommandBase<int>
	{
		// Token: 0x06001B20 RID: 6944 RVA: 0x00066F24 File Offset: 0x00065124
		public InstantMessageSignOut(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00066F30 File Offset: 0x00065130
		protected override int InternalExecute()
		{
			InstantMessageOperationError instantMessageOperationError = this.SignOut();
			OwaApplication.GetRequestDetailsLogger.Set(InstantMessagingLogMetadata.OperationErrorCode, instantMessageOperationError);
			return (int)instantMessageOperationError;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00066F5C File Offset: 0x0006515C
		protected InstantMessageOperationError SignOut()
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			if (!userContext.IsInstantMessageEnabled)
			{
				return InstantMessageOperationError.NotEnabled;
			}
			if (userContext.InstantMessageManager == null)
			{
				return InstantMessageOperationError.NotConfigured;
			}
			userContext.InstantMessageManager.SignOut();
			InstantMessageUtilities.SetSignedOutFlag(base.MailboxIdentityMailboxSession, true);
			return InstantMessageOperationError.Success;
		}
	}
}
