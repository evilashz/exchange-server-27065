using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200015A RID: 346
	[XmlType(TypeName = "MailForwarding", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class MailForwarding
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0001D12D File Offset: 0x0001B32D
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0001D135 File Offset: 0x0001B335
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0001D145 File Offset: 0x0001B345
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0001D160 File Offset: 0x0001B360
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

		// Token: 0x040005AA RID: 1450
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Mode", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public ForwardingMode internalMode;

		// Token: 0x040005AB RID: 1451
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalModeSpecified;

		// Token: 0x040005AC RID: 1452
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Addresses), ElementName = "Addresses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Addresses internalAddresses;
	}
}
