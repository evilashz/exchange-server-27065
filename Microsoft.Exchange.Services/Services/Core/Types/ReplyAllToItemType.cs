using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000658 RID: 1624
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "ReplyAllToItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ReplyAllToItemType : SmartResponseType
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000B774B File Offset: 0x000B594B
		// (set) Token: 0x0600320E RID: 12814 RVA: 0x000B775D File Offset: 0x000B595D
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlIgnore]
		public bool? IsSpecificMessageReply
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(MessageSchema.IsSpecificMessageReply);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(MessageSchema.IsSpecificMessageReply, value);
				this.IsSpecificMessageReplyStamped = new bool?(true);
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000B777C File Offset: 0x000B597C
		// (set) Token: 0x06003210 RID: 12816 RVA: 0x000B778E File Offset: 0x000B598E
		[IgnoreDataMember]
		[XmlIgnore]
		public bool? IsSpecificMessageReplyStamped
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(MessageSchema.IsSpecificMessageReplyStamped);
			}
			private set
			{
				base.PropertyBag.SetNullableValue<bool>(MessageSchema.IsSpecificMessageReplyStamped, value);
			}
		}
	}
}
