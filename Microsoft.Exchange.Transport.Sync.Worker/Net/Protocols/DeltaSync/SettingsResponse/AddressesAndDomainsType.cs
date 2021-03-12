using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000156 RID: 342
	[XmlType(TypeName = "AddressesAndDomainsType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AddressesAndDomainsType
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0001CD99 File Offset: 0x0001AF99
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
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

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0001CDBD File Offset: 0x0001AFBD
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
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

		// Token: 0x0400056A RID: 1386
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AddressesType), ElementName = "Addresses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AddressesType internalAddresses;

		// Token: 0x0400056B RID: 1387
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(DomainsType), ElementName = "Domains", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public DomainsType internalDomains;
	}
}
