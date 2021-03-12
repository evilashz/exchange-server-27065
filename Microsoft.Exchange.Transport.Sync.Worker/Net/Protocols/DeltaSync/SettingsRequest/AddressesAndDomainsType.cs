using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000120 RID: 288
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "AddressesAndDomainsType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class AddressesAndDomainsType
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0001BB75 File Offset: 0x00019D75
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0001BB90 File Offset: 0x00019D90
		[XmlIgnore]
		public AddressesType Addresses
		{
			get
			{
				if (this.internalAddresses == null)
				{
					this.internalAddresses = new AddressesType();
				}
				return this.internalAddresses;
			}
			set
			{
				this.internalAddresses = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0001BB99 File Offset: 0x00019D99
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		[XmlIgnore]
		public DomainsType Domains
		{
			get
			{
				if (this.internalDomains == null)
				{
					this.internalDomains = new DomainsType();
				}
				return this.internalDomains;
			}
			set
			{
				this.internalDomains = value;
			}
		}

		// Token: 0x04000491 RID: 1169
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AddressesType), ElementName = "Addresses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AddressesType internalAddresses;

		// Token: 0x04000492 RID: 1170
		[XmlElement(Type = typeof(DomainsType), ElementName = "Domains", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DomainsType internalDomains;
	}
}
