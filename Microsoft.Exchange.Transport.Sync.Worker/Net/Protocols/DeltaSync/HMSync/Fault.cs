using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000AE RID: 174
	[XmlRoot(ElementName = "Fault", Namespace = "HMSYNC:", IsNullable = false)]
	[Serializable]
	public class Fault
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00019EE9 File Offset: 0x000180E9
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00019EF1 File Offset: 0x000180F1
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

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00019EFA File Offset: 0x000180FA
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x00019F02 File Offset: 0x00018102
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

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00019F0B File Offset: 0x0001810B
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00019F13 File Offset: 0x00018113
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

		// Token: 0x0400038A RID: 906
		[XmlElement(ElementName = "Faultcode", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalFaultcode;

		// Token: 0x0400038B RID: 907
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Faultstring", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSYNC:")]
		public string internalFaultstring;

		// Token: 0x0400038C RID: 908
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Detail", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSYNC:")]
		public string internalDetail;
	}
}
