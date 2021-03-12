using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000CB RID: 203
	[XmlType(TypeName = "ItemOperationsFault", Namespace = "ItemOperations:")]
	[Serializable]
	public class ItemOperationsFault
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001A430 File Offset: 0x00018630
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0001A438 File Offset: 0x00018638
		[XmlIgnore]
		public string Faultcode
		{
			get
			{
				return this.internalFaultcode;
			}
			set
			{
				this.internalFaultcode = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001A441 File Offset: 0x00018641
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0001A449 File Offset: 0x00018649
		[XmlIgnore]
		public string Faultstring
		{
			get
			{
				return this.internalFaultstring;
			}
			set
			{
				this.internalFaultstring = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001A452 File Offset: 0x00018652
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0001A45A File Offset: 0x0001865A
		[XmlIgnore]
		public string Detail
		{
			get
			{
				return this.internalDetail;
			}
			set
			{
				this.internalDetail = value;
			}
		}

		// Token: 0x040003B5 RID: 949
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Faultcode", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		public string internalFaultcode;

		// Token: 0x040003B6 RID: 950
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Faultstring", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		public string internalFaultstring;

		// Token: 0x040003B7 RID: 951
		[XmlElement(ElementName = "Detail", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalDetail;
	}
}
