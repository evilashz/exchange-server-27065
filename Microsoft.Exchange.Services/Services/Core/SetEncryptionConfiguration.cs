using System;
using Microsoft.Exchange.Data.ApplicationLogic.E4E;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000374 RID: 884
	internal sealed class SetEncryptionConfiguration : SingleStepServiceCommand<SetEncryptionConfigurationRequest, ServiceResultNone>
	{
		// Token: 0x060018C7 RID: 6343 RVA: 0x00088968 File Offset: 0x00086B68
		public SetEncryptionConfiguration(CallContext callContext, SetEncryptionConfigurationRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00088974 File Offset: 0x00086B74
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new SetEncryptionConfigurationResponse(base.Result.Code, base.Result.Error);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000889A0 File Offset: 0x00086BA0
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			ServiceError serviceError = null;
			bool flag = false;
			try
			{
				EncryptionConfigurationData encryptionConfigurationData = new EncryptionConfigurationData(base.Request.ImageBase64, base.Request.EmailText, base.Request.PortalText, base.Request.DisclaimerText, base.Request.OTPEnabled);
				string xml = encryptionConfigurationData.Serialize();
				MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
				flag = EncryptionConfigurationHelper.SetMessageItem(mailboxIdentityMailboxSession, xml);
			}
			catch (Exception e)
			{
				serviceError = EncryptionConfigurationHelper.GetServiceError(e);
			}
			if (!flag && serviceError == null)
			{
				serviceError = new ServiceError("An error occurred in SetEncryptionConfiguration.", ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2012);
			}
			if (serviceError != null)
			{
				return new ServiceResult<ServiceResultNone>(serviceError);
			}
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x04001098 RID: 4248
		private const string ErrorMessage = "An error occurred in SetEncryptionConfiguration.";
	}
}
