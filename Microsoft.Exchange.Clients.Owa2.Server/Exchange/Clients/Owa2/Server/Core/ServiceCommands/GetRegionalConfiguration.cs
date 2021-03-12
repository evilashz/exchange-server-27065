using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000344 RID: 836
	internal class GetRegionalConfiguration : ServiceCommand<GetRegionalConfigurationResponse>
	{
		// Token: 0x06001B7D RID: 7037 RVA: 0x000695A0 File Offset: 0x000677A0
		public GetRegionalConfiguration(CallContext callContext, GetRegionalConfigurationRequest request) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(request, "GetRegionalConfigurationRequest", "GetRegionalConfiguration::GetRegionalConfiguration");
			request.Validate();
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000695C0 File Offset: 0x000677C0
		protected override GetRegionalConfigurationResponse InternalExecute()
		{
			GetRegionalConfigurationResponse getRegionalConfigurationResponse = new GetRegionalConfigurationResponse();
			List<CultureInfo> supportedCultureInfos = ClientCultures.SupportedCultureInfos;
			getRegionalConfigurationResponse.SupportedCultures = this.ToCultureInfoData(supportedCultureInfos);
			return getRegionalConfigurationResponse;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000695E8 File Offset: 0x000677E8
		private CultureInfoData[] ToCultureInfoData(List<CultureInfo> preferredCultureInfo)
		{
			CultureInfoData[] array = new CultureInfoData[preferredCultureInfo.Count];
			for (int i = 0; i < preferredCultureInfo.Count; i++)
			{
				array[i] = new CultureInfoData
				{
					Name = preferredCultureInfo[i].Name,
					NativeName = preferredCultureInfo[i].NativeName,
					LCID = preferredCultureInfo[i].LCID
				};
			}
			return array;
		}
	}
}
