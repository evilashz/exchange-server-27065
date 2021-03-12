using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000BC RID: 188
	[XmlType(TypeName = "FetchType", Namespace = "ItemOperations:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class FetchType : ItemOpsBaseType
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001A0E9 File Offset: 0x000182E9
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001A0F1 File Offset: 0x000182F1
		[XmlIgnore]
		public string Compression
		{
			get
			{
				return this.internalCompression;
			}
			set
			{
				this.internalCompression = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001A0FA File Offset: 0x000182FA
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0001A102 File Offset: 0x00018302
		[XmlIgnore]
		public ResponseContentTypeType ResponseContentType
		{
			get
			{
				return this.internalResponseContentType;
			}
			set
			{
				this.internalResponseContentType = value;
				this.internalResponseContentTypeSpecified = true;
			}
		}

		// Token: 0x0400039F RID: 927
		[XmlElement(ElementName = "Compression", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalCompression;

		// Token: 0x040003A0 RID: 928
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ResponseContentType", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		public ResponseContentTypeType internalResponseContentType;

		// Token: 0x040003A1 RID: 929
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalResponseContentTypeSpecified;
	}
}
