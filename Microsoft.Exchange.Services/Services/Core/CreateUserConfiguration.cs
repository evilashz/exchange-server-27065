using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002CA RID: 714
	internal sealed class CreateUserConfiguration : UserConfigurationCommandBase<CreateUserConfigurationRequest, ServiceResultNone>
	{
		// Token: 0x060013D3 RID: 5075 RVA: 0x0006336D File Offset: 0x0006156D
		public CreateUserConfiguration(CallContext callContext, CreateUserConfigurationRequest request) : base(callContext, request)
		{
			this.serviceUserConfiguration = request.UserConfiguration;
			ServiceCommandBase.ThrowIfNull(this.serviceUserConfiguration, "serviceUserConfiguration", "CreateUserConfiguration::ctor");
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00063398 File Offset: 0x00061598
		internal override IExchangeWebMethodResponse GetResponse()
		{
			CreateUserConfigurationResponse createUserConfigurationResponse = new CreateUserConfigurationResponse();
			createUserConfigurationResponse.ProcessServiceResult(base.Result);
			return createUserConfigurationResponse;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000633B8 File Offset: 0x000615B8
		private static UserConfiguration CreateInstance(UserConfigurationCommandBase<CreateUserConfigurationRequest, ServiceResultNone>.UserConfigurationName userConfigurationName)
		{
			UserConfiguration result;
			try
			{
				result = userConfigurationName.MailboxSession.UserConfigurationManager.CreateFolderConfiguration(userConfigurationName.Name, UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary, userConfigurationName.FolderId);
			}
			catch (ObjectExistedException ex)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<ObjectExistedException, string, StoreId>(0L, "ObjectExistedException during UserConfiguration creation: {0} Name {1} FolderId: {2}", ex, userConfigurationName.Name, userConfigurationName.FolderId);
				throw new ObjectSaveException(CoreResources.IDs.ErrorItemSaveUserConfigurationExists, ex);
			}
			return result;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00063428 File Offset: 0x00061628
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			UserConfigurationCommandBase<CreateUserConfigurationRequest, ServiceResultNone>.ValidatePropertiesForUpdate(this.serviceUserConfiguration);
			UserConfigurationCommandBase<CreateUserConfigurationRequest, ServiceResultNone>.UserConfigurationName userConfigurationName = base.GetUserConfigurationName(this.serviceUserConfiguration.UserConfigurationName);
			using (UserConfiguration userConfiguration = CreateUserConfiguration.CreateInstance(userConfigurationName))
			{
				UserConfigurationCommandBase<CreateUserConfigurationRequest, ServiceResultNone>.SetProperties(this.serviceUserConfiguration, userConfiguration);
				userConfiguration.Save();
			}
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x04000D73 RID: 3443
		private ServiceUserConfiguration serviceUserConfiguration;
	}
}
