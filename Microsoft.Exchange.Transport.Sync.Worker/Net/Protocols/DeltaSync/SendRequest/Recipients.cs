using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendRequest
{
	// Token: 0x020000D8 RID: 216
	[XmlType(TypeName = "Recipients", Namespace = "Send:")]
	[Serializable]
	public class Recipients
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001A89A File Offset: 0x00018A9A
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001A8B5 File Offset: 0x00018AB5
		[XmlIgnore]
		public RecipientsType To
		{
			get
			{
				if (this.internalTo == null)
				{
					this.internalTo = new RecipientsType();
				}
				return this.internalTo;
			}
			set
			{
				this.internalTo = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001A8BE File Offset: 0x00018ABE
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001A8D9 File Offset: 0x00018AD9
		[XmlIgnore]
		public RecipientsType Cc
		{
			get
			{
				if (this.internalCc == null)
				{
					this.internalCc = new RecipientsType();
				}
				return this.internalCc;
			}
			set
			{
				this.internalCc = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001A8E2 File Offset: 0x00018AE2
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001A8FD File Offset: 0x00018AFD
		[XmlIgnore]
		public RecipientsType Bcc
		{
			get
			{
				if (this.internalBcc == null)
				{
					this.internalBcc = new RecipientsType();
				}
				return this.internalBcc;
			}
			set
			{
				this.internalBcc = value;
			}
		}

		// Token: 0x040003D9 RID: 985
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(RecipientsType), ElementName = "To", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		public RecipientsType internalTo;

		// Token: 0x040003DA RID: 986
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(RecipientsType), ElementName = "Cc", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		public RecipientsType internalCc;

		// Token: 0x040003DB RID: 987
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(RecipientsType), ElementName = "Bcc", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		public RecipientsType internalBcc;
	}
}
