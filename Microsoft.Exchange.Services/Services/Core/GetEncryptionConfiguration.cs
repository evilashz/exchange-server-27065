using System;
using Microsoft.Exchange.Data.ApplicationLogic.E4E;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000307 RID: 775
	internal sealed class GetEncryptionConfiguration : SingleStepServiceCommand<GetEncryptionConfigurationRequest, GetEncryptionConfigurationResponse>
	{
		// Token: 0x060015FB RID: 5627 RVA: 0x00071FC4 File Offset: 0x000701C4
		public GetEncryptionConfiguration(CallContext callContext, GetEncryptionConfigurationRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00071FCE File Offset: 0x000701CE
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetEncryptionConfigurationResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00071FF8 File Offset: 0x000701F8
		internal override ServiceResult<GetEncryptionConfigurationResponse> Execute()
		{
			ServiceError serviceError = null;
			GetEncryptionConfigurationResponse getEncryptionConfigurationResponse = null;
			try
			{
				MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
				EncryptionConfigurationData encryptionConfigurationData = EncryptionConfigurationHelper.GetEncryptionConfigurationData(mailboxIdentityMailboxSession);
				getEncryptionConfigurationResponse = new GetEncryptionConfigurationResponse();
				getEncryptionConfigurationResponse.ImageBase64 = encryptionConfigurationData.ImageBase64;
				getEncryptionConfigurationResponse.EmailText = encryptionConfigurationData.EmailText;
				getEncryptionConfigurationResponse.PortalText = encryptionConfigurationData.PortalText;
				getEncryptionConfigurationResponse.DisclaimerText = encryptionConfigurationData.DisclaimerText;
				getEncryptionConfigurationResponse.OTPEnabled = encryptionConfigurationData.OTPEnabled;
			}
			catch (Exception e)
			{
				serviceError = EncryptionConfigurationHelper.GetServiceError(e);
			}
			if (serviceError != null)
			{
				return new ServiceResult<GetEncryptionConfigurationResponse>(serviceError);
			}
			return new ServiceResult<GetEncryptionConfigurationResponse>(getEncryptionConfigurationResponse);
		}
	}
}
