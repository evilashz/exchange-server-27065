using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000CC RID: 204
	[XmlType(TypeName = "ItemOperationsAuthPolicy", Namespace = "ItemOperations:")]
	[Serializable]
	public class ItemOperationsAuthPolicy
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001A46B File Offset: 0x0001866B
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0001A473 File Offset: 0x00018673
		[XmlIgnore]
		public string SAP
		{
			get
			{
				return this.internalSAP;
			}
			set
			{
				this.internalSAP = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001A47C File Offset: 0x0001867C
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0001A484 File Offset: 0x00018684
		[XmlIgnore]
		public string Version
		{
			get
			{
				return this.internalVersion;
			}
			set
			{
				this.internalVersion = value;
			}
		}

		// Token: 0x040003B8 RID: 952
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "SAP", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		public string internalSAP;

		// Token: 0x040003B9 RID: 953
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Version", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		public string internalVersion;
	}
}
