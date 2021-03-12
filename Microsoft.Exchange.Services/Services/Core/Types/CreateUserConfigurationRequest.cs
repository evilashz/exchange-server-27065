using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200040E RID: 1038
	[XmlType("CreateUserConfigurationRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateUserConfigurationRequest : BaseRequest
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0009F53B File Offset: 0x0009D73B
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x0009F543 File Offset: 0x0009D743
		[DataMember(IsRequired = true)]
		[XmlElement]
		public ServiceUserConfiguration UserConfiguration { get; set; }

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0009F54C File Offset: 0x0009D74C
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateUserConfiguration(callContext, this);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0009F558 File Offset: 0x0009D758
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.UserConfiguration == null || this.UserConfiguration.UserConfigurationName == null || this.UserConfiguration.UserConfigurationName.BaseFolderId == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderId(callContext, this.UserConfiguration.UserConfigurationName.BaseFolderId);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0009F5A4 File Offset: 0x0009D7A4
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
