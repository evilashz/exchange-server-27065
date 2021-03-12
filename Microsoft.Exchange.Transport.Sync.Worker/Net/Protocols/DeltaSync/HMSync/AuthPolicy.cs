using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000AF RID: 175
	[XmlRoot(ElementName = "AuthPolicy", Namespace = "HMSYNC:", IsNullable = false)]
	[Serializable]
	public class AuthPolicy
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00019F24 File Offset: 0x00018124
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00019F2C File Offset: 0x0001812C
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

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00019F35 File Offset: 0x00018135
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00019F3D File Offset: 0x0001813D
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

		// Token: 0x0400038D RID: 909
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "SAP", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSYNC:")]
		public string internalSAP;

		// Token: 0x0400038E RID: 910
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Version", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSYNC:")]
		public string internalVersion;
	}
}
