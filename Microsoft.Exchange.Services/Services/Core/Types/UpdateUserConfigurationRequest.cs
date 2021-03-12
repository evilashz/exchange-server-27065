using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200049C RID: 1180
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("UpdateUserConfigurationRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UpdateUserConfigurationRequest : BaseRequest
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x000A3E44 File Offset: 0x000A2044
		// (set) Token: 0x06002368 RID: 9064 RVA: 0x000A3E4C File Offset: 0x000A204C
		[DataMember(IsRequired = true)]
		[XmlElement]
		public ServiceUserConfiguration UserConfiguration { get; set; }

		// Token: 0x06002369 RID: 9065 RVA: 0x000A3E55 File Offset: 0x000A2055
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateUserConfiguration(callContext, this);
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000A3E60 File Offset: 0x000A2060
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.UserConfiguration == null || this.UserConfiguration.UserConfigurationName == null || this.UserConfiguration.UserConfigurationName.BaseFolderId == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderId(callContext, this.UserConfiguration.UserConfigurationName.BaseFolderId);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000A3EAC File Offset: 0x000A20AC
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
