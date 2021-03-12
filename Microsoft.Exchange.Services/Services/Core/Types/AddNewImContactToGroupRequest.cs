using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F5 RID: 1013
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("AddNewImContactToGroupRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddNewImContactToGroupRequest : BaseRequest
	{
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0009E39C File Offset: 0x0009C59C
		// (set) Token: 0x06001C6C RID: 7276 RVA: 0x0009E3A4 File Offset: 0x0009C5A4
		[XmlElement(ElementName = "ImAddress")]
		[DataMember(Name = "ImAddress", IsRequired = true, Order = 1)]
		public string ImAddress { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0009E3AD File Offset: 0x0009C5AD
		// (set) Token: 0x06001C6E RID: 7278 RVA: 0x0009E3B5 File Offset: 0x0009C5B5
		[XmlElement(ElementName = "DisplayName")]
		[DataMember(Name = "DisplayName", IsRequired = false, Order = 2)]
		public string DisplayName { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x0009E3BE File Offset: 0x0009C5BE
		// (set) Token: 0x06001C70 RID: 7280 RVA: 0x0009E3C6 File Offset: 0x0009C5C6
		[XmlElement(ElementName = "GroupId")]
		[DataMember(Name = "GroupId", IsRequired = false, Order = 3)]
		public ItemId GroupId { get; set; }

		// Token: 0x06001C71 RID: 7281 RVA: 0x0009E3CF File Offset: 0x0009C5CF
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new AddNewImContactToGroupCommand(callContext, this);
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0009E3D8 File Offset: 0x0009C5D8
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.GroupId != null)
			{
				return BaseRequest.GetServerInfoForItemId(callContext, this.GroupId);
			}
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0009E3F5 File Offset: 0x0009C5F5
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
