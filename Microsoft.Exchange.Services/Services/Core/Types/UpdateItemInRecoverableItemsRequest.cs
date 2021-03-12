using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000498 RID: 1176
	[XmlType("UpdateItemInRecoverableItemsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateItemInRecoverableItemsRequest : BaseRequest
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000A390E File Offset: 0x000A1B0E
		// (set) Token: 0x06002321 RID: 8993 RVA: 0x000A3916 File Offset: 0x000A1B16
		[DataMember(Name = "ItemId", IsRequired = true)]
		[XmlElement("ItemId", typeof(ItemId))]
		public ItemId ItemId { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x000A391F File Offset: 0x000A1B1F
		// (set) Token: 0x06002323 RID: 8995 RVA: 0x000A3927 File Offset: 0x000A1B27
		[XmlArrayItem("SetItemField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(SetItemPropertyUpdate))]
		[XmlArrayItem("DeleteItemField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(DeleteItemPropertyUpdate))]
		[XmlArrayItem("AppendToItemField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(AppendItemPropertyUpdate))]
		[DataMember(Name = "Updates", IsRequired = true)]
		[XmlArray("Updates")]
		public PropertyUpdate[] PropertyUpdates { get; set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x000A3930 File Offset: 0x000A1B30
		// (set) Token: 0x06002325 RID: 8997 RVA: 0x000A3938 File Offset: 0x000A1B38
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ReferenceAttachment", typeof(ReferenceAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("Attachments")]
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(EmitDefaultValue = false)]
		public AttachmentType[] Attachments { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000A3941 File Offset: 0x000A1B41
		// (set) Token: 0x06002327 RID: 8999 RVA: 0x000A3949 File Offset: 0x000A1B49
		[DataMember(Name = "MakeItemImmutable", IsRequired = false)]
		[XmlElement("MakeItemImmutable", typeof(bool))]
		public bool MakeItemImmutable { get; set; }

		// Token: 0x06002328 RID: 9000 RVA: 0x000A3952 File Offset: 0x000A1B52
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateItemInRecoverableItems(callContext, this);
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000A395B File Offset: 0x000A1B5B
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.ItemId);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000A396C File Offset: 0x000A1B6C
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.resourceKeys == null)
			{
				BaseServerIdInfo serverInfoForFolderId = BaseRequest.GetServerInfoForFolderId(callContext, new DistinguishedFolderId
				{
					Id = DistinguishedFolderIdName.recoverableitemspurges
				});
				BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.ItemId);
				this.resourceKeys = BaseRequest.ServerInfosToResourceKeys(true, new BaseServerIdInfo[]
				{
					serverInfoForFolderId,
					serverInfoForItemId
				});
			}
			return this.resourceKeys;
		}

		// Token: 0x0400152E RID: 5422
		private ResourceKey[] resourceKeys;
	}
}
