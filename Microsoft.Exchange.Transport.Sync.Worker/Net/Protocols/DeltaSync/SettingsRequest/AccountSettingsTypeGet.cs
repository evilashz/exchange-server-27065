using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200010D RID: 269
	[XmlType(TypeName = "AccountSettingsTypeGet", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class AccountSettingsTypeGet
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001B372 File Offset: 0x00019572
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0001B38D File Offset: 0x0001958D
		[XmlIgnore]
		public Filters Filters
		{
			get
			{
				if (this.internalFilters == null)
				{
					this.internalFilters = new Filters();
				}
				return this.internalFilters;
			}
			set
			{
				this.internalFilters = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0001B396 File Offset: 0x00019596
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0001B3B1 File Offset: 0x000195B1
		[XmlIgnore]
		public AccountSettingsTypeGetLists Lists
		{
			get
			{
				if (this.internalLists == null)
				{
					this.internalLists = new AccountSettingsTypeGetLists();
				}
				return this.internalLists;
			}
			set
			{
				this.internalLists = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001B3BA File Offset: 0x000195BA
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0001B3D5 File Offset: 0x000195D5
		[XmlIgnore]
		public AccountSettingsTypeGetOptions Options
		{
			get
			{
				if (this.internalOptions == null)
				{
					this.internalOptions = new AccountSettingsTypeGetOptions();
				}
				return this.internalOptions;
			}
			set
			{
				this.internalOptions = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001B3DE File Offset: 0x000195DE
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0001B3F9 File Offset: 0x000195F9
		[XmlIgnore]
		public AccountSettingsTypeGetProperties Properties
		{
			get
			{
				if (this.internalProperties == null)
				{
					this.internalProperties = new AccountSettingsTypeGetProperties();
				}
				return this.internalProperties;
			}
			set
			{
				this.internalProperties = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001B402 File Offset: 0x00019602
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0001B41D File Offset: 0x0001961D
		[XmlIgnore]
		public UserSignature UserSignature
		{
			get
			{
				if (this.internalUserSignature == null)
				{
					this.internalUserSignature = new UserSignature();
				}
				return this.internalUserSignature;
			}
			set
			{
				this.internalUserSignature = value;
			}
		}

		// Token: 0x04000453 RID: 1107
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Filters), ElementName = "Filters", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Filters internalFilters;

		// Token: 0x04000454 RID: 1108
		[XmlElement(Type = typeof(AccountSettingsTypeGetLists), ElementName = "Lists", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AccountSettingsTypeGetLists internalLists;

		// Token: 0x04000455 RID: 1109
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AccountSettingsTypeGetOptions), ElementName = "Options", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AccountSettingsTypeGetOptions internalOptions;

		// Token: 0x04000456 RID: 1110
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AccountSettingsTypeGetProperties), ElementName = "Properties", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AccountSettingsTypeGetProperties internalProperties;

		// Token: 0x04000457 RID: 1111
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(UserSignature), ElementName = "UserSignature", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public UserSignature internalUserSignature;
	}
}
