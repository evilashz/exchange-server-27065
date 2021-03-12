using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000113 RID: 275
	[XmlType(TypeName = "Set", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Set
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001B456 File Offset: 0x00019656
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0001B471 File Offset: 0x00019671
		[XmlIgnore]
		public FiltersRequestType Filters
		{
			get
			{
				if (this.internalFilters == null)
				{
					this.internalFilters = new FiltersRequestType();
				}
				return this.internalFilters;
			}
			set
			{
				this.internalFilters = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0001B47A File Offset: 0x0001967A
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0001B495 File Offset: 0x00019695
		[XmlIgnore]
		public ListsRequestType Lists
		{
			get
			{
				if (this.internalLists == null)
				{
					this.internalLists = new ListsRequestType();
				}
				return this.internalLists;
			}
			set
			{
				this.internalLists = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001B49E File Offset: 0x0001969E
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0001B4B9 File Offset: 0x000196B9
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

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0001B4C2 File Offset: 0x000196C2
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0001B4DD File Offset: 0x000196DD
		[XmlIgnore]
		public StringWithCharSetType UserSignature
		{
			get
			{
				if (this.internalUserSignature == null)
				{
					this.internalUserSignature = new StringWithCharSetType();
				}
				return this.internalUserSignature;
			}
			set
			{
				this.internalUserSignature = value;
			}
		}

		// Token: 0x04000458 RID: 1112
		[XmlElement(Type = typeof(FiltersRequestType), ElementName = "Filters", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FiltersRequestType internalFilters;

		// Token: 0x04000459 RID: 1113
		[XmlElement(Type = typeof(ListsRequestType), ElementName = "Lists", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsRequestType internalLists;

		// Token: 0x0400045A RID: 1114
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(OptionsType), ElementName = "Options", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public OptionsType internalOptions;

		// Token: 0x0400045B RID: 1115
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(StringWithCharSetType), ElementName = "UserSignature", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public StringWithCharSetType internalUserSignature;
	}
}
