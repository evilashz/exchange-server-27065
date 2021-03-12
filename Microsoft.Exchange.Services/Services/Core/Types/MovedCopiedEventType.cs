using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D6 RID: 1494
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "MovedCopiedEvent", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class MovedCopiedEventType : BaseObjectChangedEventType
	{
		// Token: 0x06002CFA RID: 11514 RVA: 0x000B1B3F File Offset: 0x000AFD3F
		public MovedCopiedEventType()
		{
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000B1B47 File Offset: 0x000AFD47
		public MovedCopiedEventType(NotificationTypeEnum notificationType) : base(notificationType)
		{
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x000B1B50 File Offset: 0x000AFD50
		// (set) Token: 0x06002CFD RID: 11517 RVA: 0x000B1B58 File Offset: 0x000AFD58
		[XmlElement("OldFolderId", typeof(FolderId))]
		[XmlElement("OldItemId", typeof(ItemId))]
		[IgnoreDataMember]
		public object OldObject
		{
			get
			{
				return this.oldObject;
			}
			set
			{
				this.oldObject = value;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000B1B61 File Offset: 0x000AFD61
		// (set) Token: 0x06002CFF RID: 11519 RVA: 0x000B1B6E File Offset: 0x000AFD6E
		[DataMember(Name = "OldFolderId", EmitDefaultValue = false, IsRequired = false, Order = 1)]
		[XmlIgnore]
		public FolderId OldFolderId
		{
			get
			{
				return this.oldObject as FolderId;
			}
			set
			{
				this.oldObject = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x000B1B77 File Offset: 0x000AFD77
		// (set) Token: 0x06002D01 RID: 11521 RVA: 0x000B1B84 File Offset: 0x000AFD84
		[DataMember(Name = "OldItemId", EmitDefaultValue = false, Order = 2)]
		[XmlIgnore]
		public ItemId OldItemId
		{
			get
			{
				return this.oldObject as ItemId;
			}
			set
			{
				this.oldObject = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x000B1B8D File Offset: 0x000AFD8D
		// (set) Token: 0x06002D03 RID: 11523 RVA: 0x000B1B95 File Offset: 0x000AFD95
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public FolderId OldParentFolderId { get; set; }

		// Token: 0x04001B0C RID: 6924
		private object oldObject;
	}
}
