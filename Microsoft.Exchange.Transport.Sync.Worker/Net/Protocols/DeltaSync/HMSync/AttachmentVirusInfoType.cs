using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000A4 RID: 164
	[XmlType(TypeName = "AttachmentVirusInfoType", Namespace = "HMSYNC:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AttachmentVirusInfoType
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00019B1B File Offset: 0x00017D1B
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00019B23 File Offset: 0x00017D23
		[XmlIgnore]
		public int Index
		{
			get
			{
				return this.internalIndex;
			}
			set
			{
				this.internalIndex = value;
				this.internalIndexSpecified = true;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00019B33 File Offset: 0x00017D33
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00019B3B File Offset: 0x00017D3B
		[XmlIgnore]
		public string ContentId
		{
			get
			{
				return this.internalContentId;
			}
			set
			{
				this.internalContentId = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00019B44 File Offset: 0x00017D44
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00019B5F File Offset: 0x00017D5F
		[XmlIgnore]
		public VirusesFound VirusesFound
		{
			get
			{
				if (this.internalVirusesFound == null)
				{
					this.internalVirusesFound = new VirusesFound();
				}
				return this.internalVirusesFound;
			}
			set
			{
				this.internalVirusesFound = value;
			}
		}

		// Token: 0x0400037A RID: 890
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Index", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSYNC:")]
		public int internalIndex;

		// Token: 0x0400037B RID: 891
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalIndexSpecified;

		// Token: 0x0400037C RID: 892
		[XmlElement(ElementName = "ContentId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalContentId;

		// Token: 0x0400037D RID: 893
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(VirusesFound), ElementName = "VirusesFound", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public VirusesFound internalVirusesFound;
	}
}
