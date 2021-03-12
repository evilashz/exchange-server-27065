using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200047E RID: 1150
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SetImGroupRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetImGroupRequest : BaseRequest
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x000A2C90 File Offset: 0x000A0E90
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x000A2C98 File Offset: 0x000A0E98
		[DataMember(Name = "GroupId", IsRequired = true, Order = 1)]
		[XmlElement(ElementName = "GroupId")]
		public ItemId GroupId { get; set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x000A2CA1 File Offset: 0x000A0EA1
		// (set) Token: 0x06002225 RID: 8741 RVA: 0x000A2CA9 File Offset: 0x000A0EA9
		[DataMember(Name = "NewDisplayName", IsRequired = true, Order = 2)]
		[XmlElement(ElementName = "NewDisplayName")]
		public string NewDisplayName { get; set; }

		// Token: 0x06002226 RID: 8742 RVA: 0x000A2CB2 File Offset: 0x000A0EB2
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetImGroupCommand(callContext, this);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000A2CBB File Offset: 0x000A0EBB
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.GroupId);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000A2CC9 File Offset: 0x000A0EC9
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysForItemId(true, callContext, this.GroupId);
		}
	}
}
