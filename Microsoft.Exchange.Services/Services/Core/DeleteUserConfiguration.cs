using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002D1 RID: 721
	internal sealed class DeleteUserConfiguration : UserConfigurationCommandBase<DeleteUserConfigurationRequest, ServiceResultNone>
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x000643F8 File Offset: 0x000625F8
		public DeleteUserConfiguration(CallContext callContext, DeleteUserConfigurationRequest request) : base(callContext, request)
		{
			this.userConfigurationName = base.Request.UserConfigurationName;
			ServiceCommandBase.ThrowIfNull(this.userConfigurationName, "userConfigurationName", "DeleteUserConfiguration:Execute");
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00064428 File Offset: 0x00062628
		internal override IExchangeWebMethodResponse GetResponse()
		{
			DeleteUserConfigurationResponse deleteUserConfigurationResponse = new DeleteUserConfigurationResponse();
			deleteUserConfigurationResponse.ProcessServiceResult(base.Result);
			return deleteUserConfigurationResponse;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00064448 File Offset: 0x00062648
		private static void Delete(UserConfigurationCommandBase<DeleteUserConfigurationRequest, ServiceResultNone>.UserConfigurationName userConfigurationName)
		{
			OperationResult operationResult = userConfigurationName.MailboxSession.UserConfigurationManager.DeleteFolderConfigurations(userConfigurationName.FolderId, new string[]
			{
				userConfigurationName.Name
			});
			if (operationResult != OperationResult.Succeeded)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<OperationResult>(0L, "Delete UserConfiguration failed. OperationResult: {0}", operationResult);
				throw new DeleteItemsException((CoreResources.IDs)3912965805U);
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000644A4 File Offset: 0x000626A4
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			UserConfigurationCommandBase<DeleteUserConfigurationRequest, ServiceResultNone>.UserConfigurationName userConfigurationName = base.GetUserConfigurationName(this.userConfigurationName);
			UserConfiguration userConfiguration = UserConfigurationCommandBase<DeleteUserConfigurationRequest, ServiceResultNone>.Get(userConfigurationName);
			userConfiguration.Dispose();
			DeleteUserConfiguration.Delete(userConfigurationName);
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x04000D80 RID: 3456
		private UserConfigurationNameType userConfigurationName;
	}
}
