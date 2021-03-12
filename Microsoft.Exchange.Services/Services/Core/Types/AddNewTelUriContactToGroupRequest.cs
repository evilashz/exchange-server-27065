using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F6 RID: 1014
	[XmlType("AddNewTelUriContactToGroupRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddNewTelUriContactToGroupRequest : BaseRequest
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0009E407 File Offset: 0x0009C607
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x0009E40F File Offset: 0x0009C60F
		[DataMember(Name = "TelUriAddress", IsRequired = true, Order = 1)]
		[XmlElement(ElementName = "TelUriAddress")]
		public string TelUriAddress { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0009E418 File Offset: 0x0009C618
		// (set) Token: 0x06001C78 RID: 7288 RVA: 0x0009E420 File Offset: 0x0009C620
		[XmlElement(ElementName = "ImContactSipUriAddress")]
		[DataMember(Name = "ImContactSipUriAddress", IsRequired = true, Order = 2)]
		public string ImContactSipUriAddress { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0009E429 File Offset: 0x0009C629
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0009E431 File Offset: 0x0009C631
		[XmlElement(ElementName = "ImTelephoneNumber")]
		[DataMember(Name = "ImTelephoneNumber", IsRequired = false, Order = 3)]
		public string ImTelephoneNumber { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0009E43A File Offset: 0x0009C63A
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x0009E442 File Offset: 0x0009C642
		[DataMember(Name = "GroupId", IsRequired = false, Order = 4)]
		[XmlElement(ElementName = "GroupId")]
		public ItemId GroupId { get; set; }

		// Token: 0x06001C7D RID: 7293 RVA: 0x0009E44B File Offset: 0x0009C64B
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new AddNewTelUriContactToGroupCommand(callContext, this);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0009E454 File Offset: 0x0009C654
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.GroupId != null)
			{
				return BaseRequest.GetServerInfoForItemId(callContext, this.GroupId);
			}
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0009E471 File Offset: 0x0009C671
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
