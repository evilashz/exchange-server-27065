using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder
{
	// Token: 0x02000090 RID: 144
	[XmlRoot(ElementName = "ParentId", Namespace = "HMFOLDER:", IsNullable = false)]
	[Serializable]
	public class ParentId
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00019765 File Offset: 0x00017965
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0001976D File Offset: 0x0001796D
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

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001977D File Offset: 0x0001797D
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x00019785 File Offset: 0x00017985
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

		// Token: 0x04000363 RID: 867
		[XmlAttribute(AttributeName = "isClientId", Form = XmlSchemaForm.Unqualified, Namespace = "HMFOLDER:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bitType internalisClientId;

		// Token: 0x04000364 RID: 868
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalisClientIdSpecified;

		// Token: 0x04000365 RID: 869
		[XmlText(DataType = "string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalValue;
	}
}
