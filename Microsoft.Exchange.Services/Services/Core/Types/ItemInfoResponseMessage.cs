using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200052C RID: 1324
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ItemInfoResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ItemInfoResponseMessage : ResponseMessage
	{
		// Token: 0x060025DA RID: 9690 RVA: 0x000A5FE7 File Offset: 0x000A41E7
		public ItemInfoResponseMessage()
		{
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000A5FF0 File Offset: 0x000A41F0
		internal ItemInfoResponseMessage(ServiceResultCode code, ServiceError error, ItemType item) : base(code, error)
		{
			this.Items = new ArrayOfRealItemsType
			{
				Items = new ItemType[]
				{
					item
				}
			};
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000A6024 File Offset: 0x000A4224
		internal ItemInfoResponseMessage(ServiceResultCode code, ServiceError error, ItemType[] items) : base(code, error)
		{
			this.Items = new ArrayOfRealItemsType
			{
				Items = items
			};
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x000A604D File Offset: 0x000A424D
		// (set) Token: 0x060025DE RID: 9694 RVA: 0x000A6055 File Offset: 0x000A4255
		[XmlElement("Items", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[IgnoreDataMember]
		public ArrayOfRealItemsType Items { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x000A605E File Offset: 0x000A425E
		// (set) Token: 0x060025E0 RID: 9696 RVA: 0x000A6078 File Offset: 0x000A4278
		[XmlIgnore]
		[DataMember(Name = "Items", IsRequired = true, Order = 1)]
		public ItemType[] ItemsArray
		{
			get
			{
				if (this.Items == null)
				{
					return null;
				}
				return this.Items.Items;
			}
			set
			{
				this.Items = new ArrayOfRealItemsType
				{
					Items = value
				};
			}
		}
	}
}
