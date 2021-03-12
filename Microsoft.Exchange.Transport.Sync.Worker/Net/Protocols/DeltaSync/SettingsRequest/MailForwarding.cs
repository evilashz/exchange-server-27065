using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000124 RID: 292
	[XmlType(TypeName = "MailForwarding", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class MailForwarding
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001BF09 File Offset: 0x0001A109
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0001BF11 File Offset: 0x0001A111
		[XmlIgnore]
		public ForwardingMode Mode
		{
			get
			{
				return this.internalMode;
			}
			set
			{
				this.internalMode = value;
				this.internalModeSpecified = true;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001BF21 File Offset: 0x0001A121
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x0001BF3C File Offset: 0x0001A13C
		[XmlIgnore]
		public Addresses Addresses
		{
			get
			{
				if (this.internalAddresses == null)
				{
					this.internalAddresses = new Addresses();
				}
				return this.internalAddresses;
			}
			set
			{
				this.internalAddresses = value;
			}
		}

		// Token: 0x040004D1 RID: 1233
		[XmlElement(ElementName = "Mode", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ForwardingMode internalMode;

		// Token: 0x040004D2 RID: 1234
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalModeSpecified;

		// Token: 0x040004D3 RID: 1235
		[XmlElement(Type = typeof(Addresses), ElementName = "Addresses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Addresses internalAddresses;
	}
}
