using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F3 RID: 1011
	[XmlType("AddImContactToGroupRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddImContactToGroupRequest : BaseRequest
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0009E317 File Offset: 0x0009C517
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x0009E31F File Offset: 0x0009C51F
		[XmlElement(ElementName = "ContactId")]
		[DataMember(Name = "ContactId", IsRequired = true, Order = 1)]
		public ItemId ContactId { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0009E328 File Offset: 0x0009C528
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x0009E330 File Offset: 0x0009C530
		[XmlElement(ElementName = "GroupId")]
		[DataMember(Name = "GroupId", IsRequired = false, Order = 2)]
		public ItemId GroupId { get; set; }

		// Token: 0x06001C61 RID: 7265 RVA: 0x0009E339 File Offset: 0x0009C539
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new AddImContactToGroupCommand(callContext, this);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0009E342 File Offset: 0x0009C542
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.ContactId);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0009E350 File Offset: 0x0009C550
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysForItemId(true, callContext, this.ContactId);
		}
	}
}
