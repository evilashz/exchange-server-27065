using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendRequest
{
	// Token: 0x020000D7 RID: 215
	[XmlType(TypeName = "SendItem", Namespace = "Send:")]
	[Serializable]
	public class SendItem
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001A839 File Offset: 0x00018A39
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001A841 File Offset: 0x00018A41
		[XmlIgnore]
		public string Class
		{
			get
			{
				return this.internalClass;
			}
			set
			{
				this.internalClass = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001A84A File Offset: 0x00018A4A
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001A865 File Offset: 0x00018A65
		[XmlIgnore]
		public Recipients Recipients
		{
			get
			{
				if (this.internalRecipients == null)
				{
					this.internalRecipients = new Recipients();
				}
				return this.internalRecipients;
			}
			set
			{
				this.internalRecipients = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001A86E File Offset: 0x00018A6E
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001A889 File Offset: 0x00018A89
		[XmlIgnore]
		public Item Item
		{
			get
			{
				if (this.internalItem == null)
				{
					this.internalItem = new Item();
				}
				return this.internalItem;
			}
			set
			{
				this.internalItem = value;
			}
		}

		// Token: 0x040003D6 RID: 982
		[XmlElement(ElementName = "Class", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "Send:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalClass;

		// Token: 0x040003D7 RID: 983
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Recipients), ElementName = "Recipients", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		public Recipients internalRecipients;

		// Token: 0x040003D8 RID: 984
		[XmlElement(Type = typeof(Item), ElementName = "Item", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Item internalItem;
	}
}
