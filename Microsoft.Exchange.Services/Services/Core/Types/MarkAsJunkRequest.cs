using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000465 RID: 1125
	[XmlType("MarkAsJunkType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MarkAsJunkRequest : BaseRequest
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x000A22DD File Offset: 0x000A04DD
		// (set) Token: 0x06002124 RID: 8484 RVA: 0x000A22E5 File Offset: 0x000A04E5
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "ItemIds", IsRequired = true, Order = 1)]
		[XmlArray("ItemIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ItemId[] ItemIds { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x000A22EE File Offset: 0x000A04EE
		// (set) Token: 0x06002126 RID: 8486 RVA: 0x000A22F6 File Offset: 0x000A04F6
		[XmlAttribute("IsJunk", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "IsJunk", IsRequired = true, Order = 2)]
		public bool IsJunk { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x000A22FF File Offset: 0x000A04FF
		// (set) Token: 0x06002128 RID: 8488 RVA: 0x000A2307 File Offset: 0x000A0507
		[XmlAttribute("MoveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "MoveItem", IsRequired = true, Order = 3)]
		public bool MoveItem { get; set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x000A2310 File Offset: 0x000A0510
		// (set) Token: 0x0600212A RID: 8490 RVA: 0x000A2318 File Offset: 0x000A0518
		[DataMember(Name = "SendCopy", IsRequired = false, Order = 4)]
		[XmlIgnore]
		public bool SendCopy { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x000A2321 File Offset: 0x000A0521
		// (set) Token: 0x0600212C RID: 8492 RVA: 0x000A2329 File Offset: 0x000A0529
		[DataMember(Name = "JunkMessageHeader", IsRequired = false, Order = 5)]
		[XmlIgnore]
		public string JunkMessageHeader { get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x000A2332 File Offset: 0x000A0532
		// (set) Token: 0x0600212E RID: 8494 RVA: 0x000A233A File Offset: 0x000A053A
		[DataMember(Name = "JunkMessageBody", IsRequired = false, Order = 6)]
		[XmlIgnore]
		public string JunkMessageBody { get; set; }

		// Token: 0x0600212F RID: 8495 RVA: 0x000A2343 File Offset: 0x000A0543
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new MarkAsJunk(callContext, this);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000A234C File Offset: 0x000A054C
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.ItemIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForItemIdList(callContext, this.ItemIds);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000A2364 File Offset: 0x000A0564
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.ItemIds == null || this.ItemIds.Length < taskStep)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.ItemIds[taskStep]);
			return BaseRequest.ServerInfoToResourceKeys(true, serverInfoForItemId);
		}
	}
}
