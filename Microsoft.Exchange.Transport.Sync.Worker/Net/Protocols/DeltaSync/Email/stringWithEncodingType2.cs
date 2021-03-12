using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.Email
{
	// Token: 0x0200008A RID: 138
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "stringWithEncodingType2", Namespace = "EMAIL:")]
	[Serializable]
	public class stringWithEncodingType2
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x000196F0 File Offset: 0x000178F0
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x000196F8 File Offset: 0x000178F8
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00019701 File Offset: 0x00017901
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x00019709 File Offset: 0x00017909
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

		// Token: 0x0400035A RID: 858
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlAttribute(AttributeName = "encoding", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "EMAIL:")]
		public string internalencoding;

		// Token: 0x0400035B RID: 859
		[XmlText(DataType = "string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalValue;
	}
}
