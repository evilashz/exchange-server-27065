using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D5 RID: 1493
	[XmlInclude(typeof(ModifiedEventType))]
	[KnownType(typeof(ModifiedEventType))]
	[XmlInclude(typeof(MovedCopiedEventType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(MovedCopiedEventType))]
	[DataContract(Name = "BaseObjectChangedEvent", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class BaseObjectChangedEventType : BaseNotificationEventType
	{
		// Token: 0x06002CEE RID: 11502 RVA: 0x000B1ACF File Offset: 0x000AFCCF
		public BaseObjectChangedEventType()
		{
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000B1AD7 File Offset: 0x000AFCD7
		public BaseObjectChangedEventType(NotificationTypeEnum notificationType) : base(notificationType)
		{
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000B1AE0 File Offset: 0x000AFCE0
		// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x000B1AE8 File Offset: 0x000AFCE8
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string TimeStamp { get; set; }

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000B1AF1 File Offset: 0x000AFCF1
		// (set) Token: 0x06002CF3 RID: 11507 RVA: 0x000B1AF9 File Offset: 0x000AFCF9
		[IgnoreDataMember]
		[XmlElement("ItemId", typeof(ItemId))]
		[XmlElement("FolderId", typeof(FolderId))]
		public object ChangedObject
		{
			get
			{
				return this.changedObject;
			}
			set
			{
				this.changedObject = value;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000B1B02 File Offset: 0x000AFD02
		// (set) Token: 0x06002CF5 RID: 11509 RVA: 0x000B1B0F File Offset: 0x000AFD0F
		[DataMember(Name = "FolderId", EmitDefaultValue = false, IsRequired = false, Order = 2)]
		[XmlIgnore]
		public FolderId FolderId
		{
			get
			{
				return this.changedObject as FolderId;
			}
			set
			{
				this.changedObject = value;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000B1B18 File Offset: 0x000AFD18
		// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x000B1B25 File Offset: 0x000AFD25
		[DataMember(Name = "ItemId", EmitDefaultValue = false, Order = 3)]
		[XmlIgnore]
		public ItemId ItemId
		{
			get
			{
				return this.changedObject as ItemId;
			}
			set
			{
				this.changedObject = value;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x000B1B2E File Offset: 0x000AFD2E
		// (set) Token: 0x06002CF9 RID: 11513 RVA: 0x000B1B36 File Offset: 0x000AFD36
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public FolderId ParentFolderId { get; set; }

		// Token: 0x04001B09 RID: 6921
		private object changedObject;
	}
}
