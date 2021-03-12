using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000109 RID: 265
	[XmlType(TypeName = "Clause", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Clause
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001B281 File Offset: 0x00019481
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0001B289 File Offset: 0x00019489
		[XmlIgnore]
		public FilterKeyType Field
		{
			get
			{
				return this.internalField;
			}
			set
			{
				this.internalField = value;
				this.internalFieldSpecified = true;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001B299 File Offset: 0x00019499
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0001B2A1 File Offset: 0x000194A1
		[XmlIgnore]
		public FilterOperatorType Operator
		{
			get
			{
				return this.internalOperator;
			}
			set
			{
				this.internalOperator = value;
				this.internalOperatorSpecified = true;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001B2B1 File Offset: 0x000194B1
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001B2CC File Offset: 0x000194CC
		[XmlIgnore]
		public StringWithVersionType Value
		{
			get
			{
				if (this.internalValue == null)
				{
					this.internalValue = new StringWithVersionType();
				}
				return this.internalValue;
			}
			set
			{
				this.internalValue = value;
			}
		}

		// Token: 0x0400044A RID: 1098
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Field", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FilterKeyType internalField;

		// Token: 0x0400044B RID: 1099
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalFieldSpecified;

		// Token: 0x0400044C RID: 1100
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Operator", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FilterOperatorType internalOperator;

		// Token: 0x0400044D RID: 1101
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalOperatorSpecified;

		// Token: 0x0400044E RID: 1102
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(StringWithVersionType), ElementName = "Value", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public StringWithVersionType internalValue;
	}
}
