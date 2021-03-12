using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200011D RID: 285
	[XmlType(TypeName = "ListsGetResponseTypeList", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ListsGetResponseTypeList
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001B9A5 File Offset: 0x00019BA5
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0001B9AD File Offset: 0x00019BAD
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

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001B9B6 File Offset: 0x00019BB6
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x0001B9D1 File Offset: 0x00019BD1
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

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001B9DA File Offset: 0x00019BDA
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0001B9F5 File Offset: 0x00019BF5
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

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001B9FE File Offset: 0x00019BFE
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0001BA19 File Offset: 0x00019C19
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

		// Token: 0x0400048B RID: 1163
		[XmlAttribute(AttributeName = "name", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalname;

		// Token: 0x0400048C RID: 1164
		[XmlElement(Type = typeof(AddressesType), ElementName = "Addresses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AddressesType internalAddresses;

		// Token: 0x0400048D RID: 1165
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(DomainsType), ElementName = "Domains", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public DomainsType internalDomains;

		// Token: 0x0400048E RID: 1166
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(LocalPartsType), ElementName = "LocalParts", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public LocalPartsType internalLocalParts;
	}
}
