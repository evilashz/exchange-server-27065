using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000BE RID: 190
	[XmlType(TypeName = "MessageType", Namespace = "ItemOperations:")]
	[Serializable]
	public class MessageType
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001A16A File Offset: 0x0001836A
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001A185 File Offset: 0x00018385
		[XmlIgnore]
		public Junk Junk
		{
			get
			{
				if (this.internalJunk == null)
				{
					this.internalJunk = new Junk();
				}
				return this.internalJunk;
			}
			set
			{
				this.internalJunk = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001A18E File Offset: 0x0001838E
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0001A1A9 File Offset: 0x000183A9
		[XmlIgnore]
		public Phishing Phishing
		{
			get
			{
				if (this.internalPhishing == null)
				{
					this.internalPhishing = new Phishing();
				}
				return this.internalPhishing;
			}
			set
			{
				this.internalPhishing = value;
			}
		}

		// Token: 0x040003A4 RID: 932
		[XmlElement(Type = typeof(Junk), ElementName = "Junk", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Junk internalJunk;

		// Token: 0x040003A5 RID: 933
		[XmlElement(Type = typeof(Phishing), ElementName = "Phishing", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Phishing internalPhishing;
	}
}
