using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendResponse
{
	// Token: 0x020000DD RID: 221
	[XmlType(TypeName = "Responses", Namespace = "Send:")]
	[Serializable]
	public class Responses
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001A9CE File Offset: 0x00018BCE
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0001A9E9 File Offset: 0x00018BE9
		[XmlIgnore]
		public SendItem SendItem
		{
			get
			{
				if (this.internalSendItem == null)
				{
					this.internalSendItem = new SendItem();
				}
				return this.internalSendItem;
			}
			set
			{
				this.internalSendItem = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001A9F2 File Offset: 0x00018BF2
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0001AA0D File Offset: 0x00018C0D
		[XmlIgnore]
		public SaveItem SaveItem
		{
			get
			{
				if (this.internalSaveItem == null)
				{
					this.internalSaveItem = new SaveItem();
				}
				return this.internalSaveItem;
			}
			set
			{
				this.internalSaveItem = value;
			}
		}

		// Token: 0x040003E3 RID: 995
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(SendItem), ElementName = "SendItem", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		public SendItem internalSendItem;

		// Token: 0x040003E4 RID: 996
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(SaveItem), ElementName = "SaveItem", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		public SaveItem internalSaveItem;
	}
}
