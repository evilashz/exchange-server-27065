using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder
{
	// Token: 0x0200008F RID: 143
	[XmlRoot(ElementName = "DisplayName", Namespace = "HMFOLDER:", IsNullable = false)]
	[Serializable]
	public class DisplayName
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001972A File Offset: 0x0001792A
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00019732 File Offset: 0x00017932
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

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001973B File Offset: 0x0001793B
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00019743 File Offset: 0x00017943
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

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001974C File Offset: 0x0001794C
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00019754 File Offset: 0x00017954
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

		// Token: 0x04000360 RID: 864
		[XmlAttribute(AttributeName = "charset", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMFOLDER:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalcharset;

		// Token: 0x04000361 RID: 865
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlAttribute(AttributeName = "encoding", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMFOLDER:")]
		public string internalencoding;

		// Token: 0x04000362 RID: 866
		[XmlText(DataType = "string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalValue;
	}
}
