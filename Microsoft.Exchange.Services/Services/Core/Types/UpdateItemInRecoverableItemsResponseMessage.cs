using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000577 RID: 1399
	[XmlType("UpdateItemInRecoverableItemsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateItemInRecoverableItemsResponseMessage : ItemInfoResponseMessage
	{
		// Token: 0x060026F9 RID: 9977 RVA: 0x000A6CBA File Offset: 0x000A4EBA
		public UpdateItemInRecoverableItemsResponseMessage()
		{
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000A6CC2 File Offset: 0x000A4EC2
		internal UpdateItemInRecoverableItemsResponseMessage(ServiceResultCode code, ServiceError error, ItemType item, AttachmentType[] attachments, ConflictResults conflictResults) : base(code, error, item)
		{
			this.Attachments = attachments;
			this.ConflictResults = conflictResults;
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x000A6CDD File Offset: 0x000A4EDD
		// (set) Token: 0x060026FC RID: 9980 RVA: 0x000A6CE5 File Offset: 0x000A4EE5
		[XmlArray("Attachments", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(EmitDefaultValue = false, Name = "Attachments")]
		[XmlArrayItem(ElementName = "FileAttachment", Type = typeof(FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "ItemAttachment", Type = typeof(ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "ReferenceAttachment", Type = typeof(ReferenceAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public AttachmentType[] Attachments { get; set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x000A6CEE File Offset: 0x000A4EEE
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x000A6CF6 File Offset: 0x000A4EF6
		[XmlElement("ConflictResults", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(EmitDefaultValue = false)]
		public ConflictResults ConflictResults { get; set; }
	}
}
