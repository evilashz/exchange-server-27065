using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x0200009E RID: 158
	[XmlRoot(ElementName = "FolderId", Namespace = "HMMAIL:", IsNullable = false)]
	[Serializable]
	public class FolderId
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x000199EA File Offset: 0x00017BEA
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x000199F2 File Offset: 0x00017BF2
		[XmlIgnore]
		public bitType isClientId
		{
			get
			{
				return this.internalisClientId;
			}
			set
			{
				this.internalisClientId = value;
				this.internalisClientIdSpecified = true;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00019A02 File Offset: 0x00017C02
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00019A0A File Offset: 0x00017C0A
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

		// Token: 0x04000375 RID: 885
		[XmlAttribute(AttributeName = "isClientId", Form = XmlSchemaForm.Unqualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bitType internalisClientId;

		// Token: 0x04000376 RID: 886
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalisClientIdSpecified;

		// Token: 0x04000377 RID: 887
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlText(DataType = "string")]
		public string internalValue;
	}
}
