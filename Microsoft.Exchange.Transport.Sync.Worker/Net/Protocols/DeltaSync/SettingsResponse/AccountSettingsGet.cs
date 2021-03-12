using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000165 RID: 357
	[XmlType(TypeName = "AccountSettingsGet", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class AccountSettingsGet
	{
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0001D53A File Offset: 0x0001B73A
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0001D555 File Offset: 0x0001B755
		[XmlIgnore]
		public FiltersResponseType Filters
		{
			get
			{
				if (this.internalFilters == null)
				{
					this.internalFilters = new FiltersResponseType();
				}
				return this.internalFilters;
			}
			set
			{
				this.internalFilters = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0001D55E File Offset: 0x0001B75E
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0001D579 File Offset: 0x0001B779
		[XmlIgnore]
		public ListsGetResponseType Lists
		{
			get
			{
				if (this.internalLists == null)
				{
					this.internalLists = new ListsGetResponseType();
				}
				return this.internalLists;
			}
			set
			{
				this.internalLists = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0001D582 File Offset: 0x0001B782
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0001D59D File Offset: 0x0001B79D
		[XmlIgnore]
		public OptionsType Options
		{
			get
			{
				if (this.internalOptions == null)
				{
					this.internalOptions = new OptionsType();
				}
				return this.internalOptions;
			}
			set
			{
				this.internalOptions = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001D5A6 File Offset: 0x0001B7A6
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x0001D5C1 File Offset: 0x0001B7C1
		[XmlIgnore]
		public PropertiesType Properties
		{
			get
			{
				if (this.internalProperties == null)
				{
					this.internalProperties = new PropertiesType();
				}
				return this.internalProperties;
			}
			set
			{
				this.internalProperties = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0001D5CA File Offset: 0x0001B7CA
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0001D5E5 File Offset: 0x0001B7E5
		[XmlIgnore]
		public StringWithVersionType UserSignature
		{
			get
			{
				if (this.internalUserSignature == null)
				{
					this.internalUserSignature = new StringWithVersionType();
				}
				return this.internalUserSignature;
			}
			set
			{
				this.internalUserSignature = value;
			}
		}

		// Token: 0x040005CD RID: 1485
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FiltersResponseType), ElementName = "Filters", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FiltersResponseType internalFilters;

		// Token: 0x040005CE RID: 1486
		[XmlElement(Type = typeof(ListsGetResponseType), ElementName = "Lists", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsGetResponseType internalLists;

		// Token: 0x040005CF RID: 1487
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(OptionsType), ElementName = "Options", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public OptionsType internalOptions;

		// Token: 0x040005D0 RID: 1488
		[XmlElement(Type = typeof(PropertiesType), ElementName = "Properties", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PropertiesType internalProperties;

		// Token: 0x040005D1 RID: 1489
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(StringWithVersionType), ElementName = "UserSignature", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public StringWithVersionType internalUserSignature;
	}
}
