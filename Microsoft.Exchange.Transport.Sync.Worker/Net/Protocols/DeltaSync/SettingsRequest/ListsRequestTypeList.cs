using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000105 RID: 261
	[XmlType(TypeName = "ListsRequestTypeList", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ListsRequestTypeList
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001B091 File Offset: 0x00019291
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0001B099 File Offset: 0x00019299
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

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001B0A2 File Offset: 0x000192A2
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0001B0BD File Offset: 0x000192BD
		[XmlIgnore]
		public AddressesAndDomainsType Set
		{
			get
			{
				if (this.internalSet == null)
				{
					this.internalSet = new AddressesAndDomainsType();
				}
				return this.internalSet;
			}
			set
			{
				this.internalSet = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001B0C6 File Offset: 0x000192C6
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0001B0E1 File Offset: 0x000192E1
		[XmlIgnore]
		public AddressesAndDomainsType Add
		{
			get
			{
				if (this.internalAdd == null)
				{
					this.internalAdd = new AddressesAndDomainsType();
				}
				return this.internalAdd;
			}
			set
			{
				this.internalAdd = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0001B0EA File Offset: 0x000192EA
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0001B105 File Offset: 0x00019305
		[XmlIgnore]
		public AddressesAndDomainsType Delete
		{
			get
			{
				if (this.internalDelete == null)
				{
					this.internalDelete = new AddressesAndDomainsType();
				}
				return this.internalDelete;
			}
			set
			{
				this.internalDelete = value;
			}
		}

		// Token: 0x0400043C RID: 1084
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlAttribute(AttributeName = "name", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalname;

		// Token: 0x0400043D RID: 1085
		[XmlElement(Type = typeof(AddressesAndDomainsType), ElementName = "Set", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AddressesAndDomainsType internalSet;

		// Token: 0x0400043E RID: 1086
		[XmlElement(Type = typeof(AddressesAndDomainsType), ElementName = "Add", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AddressesAndDomainsType internalAdd;

		// Token: 0x0400043F RID: 1087
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AddressesAndDomainsType), ElementName = "Delete", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AddressesAndDomainsType internalDelete;
	}
}
