using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000413 RID: 1043
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("DeleteUserConfigurationRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class DeleteUserConfigurationRequest : BaseRequest
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0009F985 File Offset: 0x0009DB85
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x0009F98D File Offset: 0x0009DB8D
		[XmlElement("UserConfigurationName")]
		[DataMember]
		public UserConfigurationNameType UserConfigurationName { get; set; }

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0009F996 File Offset: 0x0009DB96
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new DeleteUserConfiguration(callContext, this);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0009F99F File Offset: 0x0009DB9F
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.UserConfigurationName == null || this.UserConfigurationName.BaseFolderId == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderId(callContext, this.UserConfigurationName.BaseFolderId);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0009F9C9 File Offset: 0x0009DBC9
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
