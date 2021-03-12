using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000438 RID: 1080
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetImItemsRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetImItemsRequest : BaseRequest
	{
		// Token: 0x06001FB1 RID: 8113 RVA: 0x000A1055 File Offset: 0x0009F255
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetImItemsCommand(callContext, this);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000A105E File Offset: 0x0009F25E
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.ContactIds != null)
			{
				return BaseRequest.GetServerInfoForItemIdList(callContext, this.ContactIds);
			}
			if (this.GroupIds != null)
			{
				return BaseRequest.GetServerInfoForItemIdList(callContext, this.GroupIds);
			}
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000A1090 File Offset: 0x0009F290
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x040013F4 RID: 5108
		[DataMember(Name = "ContactIds", IsRequired = false, Order = 1)]
		[XmlArray]
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemId[] ContactIds;

		// Token: 0x040013F5 RID: 5109
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray]
		[DataMember(Name = "GroupIds", IsRequired = false, Order = 2)]
		public ItemId[] GroupIds;

		// Token: 0x040013F6 RID: 5110
		[XmlArray]
		[DataMember(Name = "ExtendedProperties", IsRequired = false, Order = 3)]
		[XmlArrayItem("ExtendedProperty", typeof(ExtendedPropertyUri), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ExtendedPropertyUri[] ExtendedProperties;
	}
}
