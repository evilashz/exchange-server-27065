using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200015E RID: 350
	[XmlType(TypeName = "SettingsFault", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class SettingsFault
	{
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0001D309 File Offset: 0x0001B509
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x0001D311 File Offset: 0x0001B511
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

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0001D31A File Offset: 0x0001B51A
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0001D322 File Offset: 0x0001B522
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

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0001D32B File Offset: 0x0001B52B
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0001D333 File Offset: 0x0001B533
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

		// Token: 0x040005B7 RID: 1463
		[XmlElement(ElementName = "Faultcode", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalFaultcode;

		// Token: 0x040005B8 RID: 1464
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Faultstring", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalFaultstring;

		// Token: 0x040005B9 RID: 1465
		[XmlElement(ElementName = "Detail", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalDetail;
	}
}
