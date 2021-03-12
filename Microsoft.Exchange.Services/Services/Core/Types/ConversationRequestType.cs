using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B4 RID: 1460
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ConversationRequestType")]
	[Serializable]
	public class ConversationRequestType
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x000AFF1C File Offset: 0x000AE11C
		// (set) Token: 0x06002BD2 RID: 11218 RVA: 0x000AFF24 File Offset: 0x000AE124
		[XmlElement("ConversationId", IsNullable = false)]
		[DataMember(Name = "ConversationId", IsRequired = true, Order = 1)]
		public ItemId ConversationId { get; set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000AFF2D File Offset: 0x000AE12D
		// (set) Token: 0x06002BD4 RID: 11220 RVA: 0x000AFF35 File Offset: 0x000AE135
		[XmlElement(DataType = "base64Binary")]
		[IgnoreDataMember]
		public byte[] SyncState { get; set; }

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000AFF40 File Offset: 0x000AE140
		// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x000AFF5F File Offset: 0x000AE15F
		[DataMember(Name = "SyncState", EmitDefaultValue = false, Order = 2)]
		[XmlIgnore]
		public string SyncStateString
		{
			get
			{
				byte[] syncState = this.SyncState;
				if (syncState == null)
				{
					return null;
				}
				return Convert.ToBase64String(syncState);
			}
			set
			{
				this.SyncState = (string.IsNullOrEmpty(value) ? null : Convert.FromBase64String(value));
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x000AFF78 File Offset: 0x000AE178
		// (set) Token: 0x06002BD8 RID: 11224 RVA: 0x000AFF80 File Offset: 0x000AE180
		[DataMember(Name = "ConversationFamilyId", IsRequired = false, Order = 3)]
		public ItemId ConversationFamilyId { get; set; }
	}
}
