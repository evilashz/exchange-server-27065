using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000141 RID: 321
	[XmlType(TypeName = "ListsRequestTypeList", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ListsRequestTypeList
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001C425 File Offset: 0x0001A625
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0001C42D File Offset: 0x0001A62D
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

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001C436 File Offset: 0x0001A636
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0001C451 File Offset: 0x0001A651
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

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001C45A File Offset: 0x0001A65A
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0001C475 File Offset: 0x0001A675
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

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001C47E File Offset: 0x0001A67E
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0001C499 File Offset: 0x0001A699
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

		// Token: 0x0400051B RID: 1307
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlAttribute(AttributeName = "name", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalname;

		// Token: 0x0400051C RID: 1308
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AddressesAndDomainsType), ElementName = "Set", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AddressesAndDomainsType internalSet;

		// Token: 0x0400051D RID: 1309
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AddressesAndDomainsType), ElementName = "Add", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AddressesAndDomainsType internalAdd;

		// Token: 0x0400051E RID: 1310
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AddressesAndDomainsType), ElementName = "Delete", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AddressesAndDomainsType internalDelete;
	}
}
