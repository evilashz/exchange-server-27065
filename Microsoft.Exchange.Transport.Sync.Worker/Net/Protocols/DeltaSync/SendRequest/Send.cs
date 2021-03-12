using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendRequest
{
	// Token: 0x020000D6 RID: 214
	[XmlRoot(ElementName = "Send", Namespace = "Send:", IsNullable = false)]
	[Serializable]
	public class Send
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001A7E9 File Offset: 0x000189E9
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001A804 File Offset: 0x00018A04
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001A80D File Offset: 0x00018A0D
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001A828 File Offset: 0x00018A28
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

		// Token: 0x040003D4 RID: 980
		[XmlElement(Type = typeof(SendItem), ElementName = "SendItem", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SendItem internalSendItem;

		// Token: 0x040003D5 RID: 981
		[XmlElement(Type = typeof(SaveItem), ElementName = "SaveItem", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SaveItem internalSaveItem;
	}
}
