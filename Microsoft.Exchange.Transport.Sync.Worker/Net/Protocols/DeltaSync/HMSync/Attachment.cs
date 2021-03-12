using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000A8 RID: 168
	[XmlType(TypeName = "Attachment", Namespace = "HMSYNC:")]
	[Serializable]
	public class Attachment : AttachmentVirusInfoType
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00019D45 File Offset: 0x00017F45
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00019D60 File Offset: 0x00017F60
		[XmlIgnore]
		public Contents Contents
		{
			get
			{
				if (this.internalContents == null)
				{
					this.internalContents = new Contents();
				}
				return this.internalContents;
			}
			set
			{
				this.internalContents = value;
			}
		}

		// Token: 0x04000385 RID: 901
		[XmlElement(Type = typeof(Contents), ElementName = "Contents", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Contents internalContents;
	}
}
