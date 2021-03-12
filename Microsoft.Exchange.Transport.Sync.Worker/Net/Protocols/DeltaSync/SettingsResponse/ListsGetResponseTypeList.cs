using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000153 RID: 339
	[XmlType(TypeName = "ListsGetResponseTypeList", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ListsGetResponseTypeList
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001CBC9 File Offset: 0x0001ADC9
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0001CBD1 File Offset: 0x0001ADD1
		[XmlIgnore]
		public string name
		{
			get
			{
				return this.internalname;
			}
			set
			{
				this.internalname = value;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0001CBDA File Offset: 0x0001ADDA
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0001CBF5 File Offset: 0x0001ADF5
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

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001CBFE File Offset: 0x0001ADFE
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x0001CC19 File Offset: 0x0001AE19
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

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0001CC22 File Offset: 0x0001AE22
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x0001CC3D File Offset: 0x0001AE3D
		[XmlIgnore]
		public LocalPartsType LocalParts
		{
			get
			{
				if (this.internalLocalParts == null)
				{
					this.internalLocalParts = new LocalPartsType();
				}
				return this.internalLocalParts;
			}
			set
			{
				this.internalLocalParts = value;
			}
		}

		// Token: 0x04000564 RID: 1380
		[XmlAttribute(AttributeName = "name", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalname;

		// Token: 0x04000565 RID: 1381
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AddressesType), ElementName = "Addresses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AddressesType internalAddresses;

		// Token: 0x04000566 RID: 1382
		[XmlElement(Type = typeof(DomainsType), ElementName = "Domains", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DomainsType internalDomains;

		// Token: 0x04000567 RID: 1383
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(LocalPartsType), ElementName = "LocalParts", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public LocalPartsType internalLocalParts;
	}
}
