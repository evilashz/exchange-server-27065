using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000470 RID: 1136
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("RemoveDistributionGroupFromImListRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveDistributionGroupFromImListRequest : BaseRequest
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x000A2665 File Offset: 0x000A0865
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x000A266D File Offset: 0x000A086D
		[DataMember(Name = "GroupId", IsRequired = true, Order = 1)]
		[XmlElement(ElementName = "GroupId")]
		public ItemId GroupId { get; set; }

		// Token: 0x06002182 RID: 8578 RVA: 0x000A2676 File Offset: 0x000A0876
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new RemoveDistributionGroupFromImListCommand(callContext, this);
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000A267F File Offset: 0x000A087F
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.GroupId);
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000A268D File Offset: 0x000A088D
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysForItemId(true, callContext, this.GroupId);
		}
	}
}
