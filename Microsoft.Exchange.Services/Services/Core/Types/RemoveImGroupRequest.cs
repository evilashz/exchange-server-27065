using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000472 RID: 1138
	[XmlType("RemoveImGroupRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveImGroupRequest : BaseRequest
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x000A26F6 File Offset: 0x000A08F6
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x000A26FE File Offset: 0x000A08FE
		[DataMember(Name = "GroupId", IsRequired = true, Order = 1)]
		[XmlElement(ElementName = "GroupId")]
		public ItemId GroupId { get; set; }

		// Token: 0x06002190 RID: 8592 RVA: 0x000A2707 File Offset: 0x000A0907
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new RemoveImGroupCommand(callContext, this);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000A2710 File Offset: 0x000A0910
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.GroupId);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000A271E File Offset: 0x000A091E
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysForItemId(true, callContext, this.GroupId);
		}
	}
}
