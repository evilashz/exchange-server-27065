using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000166 RID: 358
	[XmlType(TypeName = "Set", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Set
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0001D5F6 File Offset: 0x0001B7F6
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0001D611 File Offset: 0x0001B811
		[XmlIgnore]
		public SettingsCategoryResponseType Filters
		{
			get
			{
				if (this.internalFilters == null)
				{
					this.internalFilters = new SettingsCategoryResponseType();
				}
				return this.internalFilters;
			}
			set
			{
				this.internalFilters = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001D61A File Offset: 0x0001B81A
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0001D635 File Offset: 0x0001B835
		[XmlIgnore]
		public ListsSetResponseType Lists
		{
			get
			{
				if (this.internalLists == null)
				{
					this.internalLists = new ListsSetResponseType();
				}
				return this.internalLists;
			}
			set
			{
				this.internalLists = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001D63E File Offset: 0x0001B83E
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0001D659 File Offset: 0x0001B859
		[XmlIgnore]
		public SettingsCategoryResponseType Options
		{
			get
			{
				if (this.internalOptions == null)
				{
					this.internalOptions = new SettingsCategoryResponseType();
				}
				return this.internalOptions;
			}
			set
			{
				this.internalOptions = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001D662 File Offset: 0x0001B862
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x0001D67D File Offset: 0x0001B87D
		[XmlIgnore]
		public SettingsCategoryResponseType UserSignature
		{
			get
			{
				if (this.internalUserSignature == null)
				{
					this.internalUserSignature = new SettingsCategoryResponseType();
				}
				return this.internalUserSignature;
			}
			set
			{
				this.internalUserSignature = value;
			}
		}

		// Token: 0x040005D2 RID: 1490
		[XmlElement(Type = typeof(SettingsCategoryResponseType), ElementName = "Filters", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SettingsCategoryResponseType internalFilters;

		// Token: 0x040005D3 RID: 1491
		[XmlElement(Type = typeof(ListsSetResponseType), ElementName = "Lists", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsSetResponseType internalLists;

		// Token: 0x040005D4 RID: 1492
		[XmlElement(Type = typeof(SettingsCategoryResponseType), ElementName = "Options", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SettingsCategoryResponseType internalOptions;

		// Token: 0x040005D5 RID: 1493
		[XmlElement(Type = typeof(SettingsCategoryResponseType), ElementName = "UserSignature", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SettingsCategoryResponseType internalUserSignature;
	}
}
