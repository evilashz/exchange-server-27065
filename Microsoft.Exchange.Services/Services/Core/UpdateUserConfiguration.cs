using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200039B RID: 923
	internal sealed class UpdateUserConfiguration : UserConfigurationCommandBase<UpdateUserConfigurationRequest, ServiceResultNone>
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x000957C8 File Offset: 0x000939C8
		public UpdateUserConfiguration(CallContext callContext, UpdateUserConfigurationRequest request) : base(callContext, request)
		{
			this.serviceUserConfiguration = base.Request.UserConfiguration;
			ServiceCommandBase.ThrowIfNull(this.serviceUserConfiguration, "serviceUserConfiguration", "UpdateUserConfiguration:PreExecute");
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000957FF File Offset: 0x000939FF
		public UpdateUserConfiguration(CallContext callContext, UpdateUserConfigurationOwaRequest request) : base(callContext, request)
		{
			this.replaceDictionary = false;
			this.serviceUserConfiguration = base.Request.UserConfiguration;
			ServiceCommandBase.ThrowIfNull(this.serviceUserConfiguration, "serviceUserConfiguration", "UpdateUserConfiguration:PreExecute");
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00095840 File Offset: 0x00093A40
		internal override IExchangeWebMethodResponse GetResponse()
		{
			UpdateUserConfigurationResponse updateUserConfigurationResponse = new UpdateUserConfigurationResponse();
			updateUserConfigurationResponse.ProcessServiceResult(base.Result);
			return updateUserConfigurationResponse;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00095860 File Offset: 0x00093A60
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			this.Update(this.serviceUserConfiguration);
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00095878 File Offset: 0x00093A78
		private void Update(ServiceUserConfiguration serviceUserConfiguration)
		{
			UserConfigurationCommandBase<UpdateUserConfigurationRequest, ServiceResultNone>.ValidatePropertiesForUpdate(serviceUserConfiguration);
			UserConfigurationCommandBase<UpdateUserConfigurationRequest, ServiceResultNone>.UserConfigurationName userConfigurationName = base.GetUserConfigurationName(serviceUserConfiguration.UserConfigurationName);
			using (UserConfiguration userConfiguration = UserConfigurationCommandBase<UpdateUserConfigurationRequest, ServiceResultNone>.Get(userConfigurationName))
			{
				if (this.replaceDictionary)
				{
					UserConfigurationCommandBase<UpdateUserConfigurationRequest, ServiceResultNone>.SetProperties(serviceUserConfiguration, userConfiguration);
				}
				else
				{
					UserConfigurationCommandBase<UpdateUserConfigurationRequest, ServiceResultNone>.UpdateProperties(serviceUserConfiguration, userConfiguration);
				}
				userConfiguration.Save();
			}
		}

		// Token: 0x04001147 RID: 4423
		private readonly bool replaceDictionary = true;

		// Token: 0x04001148 RID: 4424
		private ServiceUserConfiguration serviceUserConfiguration;
	}
}
