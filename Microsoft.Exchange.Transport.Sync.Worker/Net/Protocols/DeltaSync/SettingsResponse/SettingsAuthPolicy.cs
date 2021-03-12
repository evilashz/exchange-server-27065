using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200015F RID: 351
	[XmlType(TypeName = "SettingsAuthPolicy", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class SettingsAuthPolicy
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001D344 File Offset: 0x0001B544
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0001D34C File Offset: 0x0001B54C
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

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001D355 File Offset: 0x0001B555
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0001D35D File Offset: 0x0001B55D
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

		// Token: 0x040005BA RID: 1466
		[XmlElement(ElementName = "SAP", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalSAP;

		// Token: 0x040005BB RID: 1467
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Version", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalVersion;
	}
}
