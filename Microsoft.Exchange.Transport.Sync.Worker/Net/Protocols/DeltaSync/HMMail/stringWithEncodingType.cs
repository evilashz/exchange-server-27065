using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x02000098 RID: 152
	[XmlType(TypeName = "stringWithEncodingType", Namespace = "HMMAIL:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class stringWithEncodingType
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x000198D5 File Offset: 0x00017AD5
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x000198DD File Offset: 0x00017ADD
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

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x000198E6 File Offset: 0x00017AE6
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x000198EE File Offset: 0x00017AEE
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

		// Token: 0x0400036C RID: 876
		[XmlAttribute(AttributeName = "encoding", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalencoding;

		// Token: 0x0400036D RID: 877
		[XmlText(DataType = "string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalValue;
	}
}
