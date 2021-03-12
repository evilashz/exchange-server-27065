using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002D3 RID: 723
	internal sealed class DisableApp : SingleStepServiceCommand<DisableAppRequest, ServiceResultNone>
	{
		// Token: 0x06001410 RID: 5136 RVA: 0x00064534 File Offset: 0x00062734
		public DisableApp(CallContext callContext, DisableAppRequest request) : base(callContext, request)
		{
			this.Id = request.ID;
			ServiceCommandBase.ThrowIfNull(this.Id, "Id", "DisableApp::ctor");
			this.DisableReason = request.DisableReason;
			ServiceCommandBase.ThrowIfNull(this.DisableReason, "DisableReason", "DisableApp::ctor");
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00064590 File Offset: 0x00062790
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new DisableAppResponse(base.Result.Code, base.Result.Error);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000646B4 File Offset: 0x000628B4
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			OrgEmptyMasterTableCache orgEmptyMailboxSessionCache = null;
			bool isUserScope = true;
			bool cannotDisableMandatoryExtension = false;
			bool extensionNotFound = false;
			ServiceError serviceError = GetExtensibilityContext.RunClientExtensionAction(delegate
			{
				MailboxSession mailboxIdentityMailboxSession = this.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
				using (InstalledExtensionTable installedExtensionTable = InstalledExtensionTable.CreateInstalledExtensionTable(null, isUserScope, orgEmptyMailboxSessionCache, mailboxIdentityMailboxSession))
				{
					try
					{
						installedExtensionTable.DisableExtension(this.Id, this.DisableReason);
						RequestDetailsLogger.Current.AppendGenericInfo("DisableApp", this.Id);
					}
					catch (CannotDisableMandatoryExtensionException ex)
					{
						cannotDisableMandatoryExtension = true;
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(this.CallContext.ProtocolLog, ex, "DisableApp_Execute");
					}
					catch (ExtensionNotFoundException ex2)
					{
						extensionNotFound = true;
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(this.CallContext.ProtocolLog, ex2, "DisableApp_Execute");
					}
				}
			});
			if (cannotDisableMandatoryExtension)
			{
				serviceError = new ServiceError(CoreResources.IDs.ErrorCannotDisableMandatoryExtension, ResponseCodeType.ErrorCannotDisableMandatoryExtension, 0, ExchangeVersion.Exchange2012);
			}
			else if (extensionNotFound)
			{
				serviceError = new ServiceError(CoreResources.ErrorExtensionNotFound(this.Id), ResponseCodeType.ErrorExtensionNotFound, 0, ExchangeVersion.Exchange2012);
			}
			if (serviceError != null)
			{
				return new ServiceResult<ServiceResultNone>(serviceError);
			}
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x04000D81 RID: 3457
		private readonly string Id;

		// Token: 0x04000D82 RID: 3458
		private readonly DisableReasonType DisableReason;
	}
}
