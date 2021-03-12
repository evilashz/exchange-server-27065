using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000478 RID: 1144
	[KnownType(typeof(ItemId))]
	[KnownType(typeof(OccurrenceItemId))]
	[KnownType(typeof(RecurringMasterItemId))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SendItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SendItemRequest : BaseRequest
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000A2987 File Offset: 0x000A0B87
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x000A298F File Offset: 0x000A0B8F
		[DataMember(Name = "ItemIds", IsRequired = true, Order = 1)]
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("ItemIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public BaseItemId[] Ids { get; set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x000A2998 File Offset: 0x000A0B98
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x000A29A0 File Offset: 0x000A0BA0
		[XmlElement("SavedItemFolderId")]
		[DataMember(IsRequired = false, Order = 2)]
		public TargetFolderId SavedItemFolderId { get; set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000A29A9 File Offset: 0x000A0BA9
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x000A29B1 File Offset: 0x000A0BB1
		[DataMember(IsRequired = false, Order = 3)]
		[XmlAttribute(AttributeName = "SaveItemToFolder")]
		public bool SaveItemToFolder { get; set; }

		// Token: 0x060021D9 RID: 8665 RVA: 0x000A29BA File Offset: 0x000A0BBA
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SendItem(callContext, this);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000A29C3 File Offset: 0x000A0BC3
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.Ids == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForItemIdList(callContext, this.Ids);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000A29DC File Offset: 0x000A0BDC
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.targetFolderServerInfo == null && this.SavedItemFolderId != null)
			{
				this.targetFolderServerInfo = BaseRequest.GetServerInfoForFolderId(callContext, this.SavedItemFolderId.BaseFolderId);
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.Ids[taskStep]);
			if (this.targetFolderServerInfo != null)
			{
				return BaseRequest.ServerInfosToResourceKeys(true, new BaseServerIdInfo[]
				{
					this.targetFolderServerInfo,
					serverInfoForItemId
				});
			}
			return BaseRequest.ServerInfoToResourceKeys(true, serverInfoForItemId);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000A2A4A File Offset: 0x000A0C4A
		protected override List<ServiceObjectId> GetAllIds()
		{
			if (this.Ids != null)
			{
				return new List<ServiceObjectId>(this.Ids);
			}
			return null;
		}

		// Token: 0x040014B9 RID: 5305
		internal const string ItemIdsElementName = "ItemIds";

		// Token: 0x040014BA RID: 5306
		internal const string SavedItemFolderIdElementName = "SavedItemFolderId";

		// Token: 0x040014BB RID: 5307
		private BaseServerIdInfo targetFolderServerInfo;
	}
}
