using System;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200038F RID: 911
	internal sealed class UninstallApp : SingleStepServiceCommand<UninstallAppRequest, ServiceResultNone>
	{
		// Token: 0x06001981 RID: 6529 RVA: 0x000912ED File Offset: 0x0008F4ED
		public UninstallApp(CallContext callContext, UninstallAppRequest request) : base(callContext, request)
		{
			this.ID = request.ID;
			ServiceCommandBase.ThrowIfNull(this.ID, "ID", "UninstallApp::ctor");
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0009137C File Offset: 0x0008F57C
		internal static ServiceResult<ServiceResultNone> InternalExecute(CallContext callContext, bool isUserScope, OrgEmptyMasterTableCache orgEmptyMasterTableCache, string extensionId)
		{
			ServiceError serviceError = GetExtensibilityContext.RunClientExtensionAction(delegate
			{
				MailboxSession mailboxIdentityMailboxSession = callContext.SessionCache.GetMailboxIdentityMailboxSession();
				using (InstalledExtensionTable installedExtensionTable = InstalledExtensionTable.CreateInstalledExtensionTable(null, isUserScope, orgEmptyMasterTableCache, mailboxIdentityMailboxSession))
				{
					installedExtensionTable.UninstallExtension(extensionId);
				}
			});
			if (serviceError != null)
			{
				return new ServiceResult<ServiceResultNone>(serviceError);
			}
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000913D4 File Offset: 0x0008F5D4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new UninstallAppResponse(base.Result.Code, base.Result.Error);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000913FE File Offset: 0x0008F5FE
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			return UninstallApp.InternalExecute(base.CallContext, true, null, this.ID);
		}

		// Token: 0x04001120 RID: 4384
		private readonly string ID;
	}
}
