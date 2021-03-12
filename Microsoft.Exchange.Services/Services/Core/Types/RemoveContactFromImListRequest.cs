using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200046E RID: 1134
	[XmlType("RemoveContactFromImListRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveContactFromImListRequest : BaseRequest
	{
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000A2602 File Offset: 0x000A0802
		// (set) Token: 0x06002177 RID: 8567 RVA: 0x000A260A File Offset: 0x000A080A
		[XmlElement(ElementName = "ContactId")]
		[DataMember(Name = "ContactId", IsRequired = true, Order = 1)]
		public ItemId ContactId { get; set; }

		// Token: 0x06002178 RID: 8568 RVA: 0x000A2613 File Offset: 0x000A0813
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new RemoveContactFromImListCommand(callContext, this);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000A261C File Offset: 0x000A081C
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.ContactId);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000A262A File Offset: 0x000A082A
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysForItemId(true, callContext, this.ContactId);
		}
	}
}
