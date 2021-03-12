using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000471 RID: 1137
	[XmlType("RemoveImContactFromGroupRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveImContactFromGroupRequest : BaseRequest
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000A26A5 File Offset: 0x000A08A5
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x000A26AD File Offset: 0x000A08AD
		[DataMember(Name = "ContactId", IsRequired = true, Order = 1)]
		[XmlElement(ElementName = "ContactId")]
		public ItemId ContactId { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000A26B6 File Offset: 0x000A08B6
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x000A26BE File Offset: 0x000A08BE
		[DataMember(Name = "GroupId", IsRequired = true, Order = 2)]
		[XmlElement(ElementName = "GroupId")]
		public ItemId GroupId { get; set; }

		// Token: 0x0600218A RID: 8586 RVA: 0x000A26C7 File Offset: 0x000A08C7
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new RemoveImContactFromGroupCommand(callContext, this);
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000A26D0 File Offset: 0x000A08D0
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.ContactId);
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000A26DE File Offset: 0x000A08DE
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysForItemId(true, callContext, this.ContactId);
		}
	}
}
