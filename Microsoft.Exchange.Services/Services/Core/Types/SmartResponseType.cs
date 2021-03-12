using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000652 RID: 1618
	[KnownType(typeof(CancelCalendarItemType))]
	[KnownType(typeof(ReplyAllToItemType))]
	[KnownType(typeof(ReplyToItemType))]
	[XmlInclude(typeof(CancelCalendarItemType))]
	[XmlInclude(typeof(ForwardItemType))]
	[XmlInclude(typeof(ReplyAllToItemType))]
	[XmlInclude(typeof(ReplyToItemType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(ForwardItemType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SmartResponseType : SmartResponseBaseType
	{
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000B76E8 File Offset: 0x000B58E8
		// (set) Token: 0x06003202 RID: 12802 RVA: 0x000B76F0 File Offset: 0x000B58F0
		[DataMember(EmitDefaultValue = false)]
		public BodyContentType NewBodyContent { get; set; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000B76F9 File Offset: 0x000B58F9
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000B7701 File Offset: 0x000B5901
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false)]
		public ItemId UpdateResponseItemId { get; set; }

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000B770A File Offset: 0x000B590A
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x000B7712 File Offset: 0x000B5912
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false)]
		public int ReferenceItemDocumentId { get; set; }
	}
}
