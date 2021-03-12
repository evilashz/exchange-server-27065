using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x02000099 RID: 153
	[XmlType(TypeName = "stringWithCharSetType", Namespace = "HMMAIL:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class stringWithCharSetType
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000198FF File Offset: 0x00017AFF
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00019907 File Offset: 0x00017B07
		[XmlIgnore]
		public string charset
		{
			get
			{
				return this.internalcharset;
			}
			set
			{
				this.internalcharset = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00019910 File Offset: 0x00017B10
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x00019918 File Offset: 0x00017B18
		[XmlIgnore]
		public string encoding
		{
			get
			{
				return this.internalencoding;
			}
			set
			{
				this.internalencoding = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00019921 File Offset: 0x00017B21
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00019929 File Offset: 0x00017B29
		[XmlIgnore]
		public string Value
		{
			get
			{
				return this.internalValue;
			}
			set
			{
				this.internalValue = value;
			}
		}

		// Token: 0x0400036E RID: 878
		[XmlAttribute(AttributeName = "charset", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalcharset;

		// Token: 0x0400036F RID: 879
		[XmlAttribute(AttributeName = "encoding", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalencoding;

		// Token: 0x04000370 RID: 880
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlText(DataType = "string")]
		public string internalValue;
	}
}
