using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000119 RID: 281
	[XmlType(TypeName = "FiltersRequestTypeFilterConditionClause", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterConditionClause
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001B85D File Offset: 0x00019A5D
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x0001B865 File Offset: 0x00019A65
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

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001B875 File Offset: 0x00019A75
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x0001B87D File Offset: 0x00019A7D
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

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001B88D File Offset: 0x00019A8D
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0001B8A8 File Offset: 0x00019AA8
		[XmlIgnore]
		public StringWithCharSetType Value
		{
			get
			{
				if (this.internalValue == null)
				{
					this.internalValue = new StringWithCharSetType();
				}
				return this.internalValue;
			}
			set
			{
				this.internalValue = value;
			}
		}

		// Token: 0x04000483 RID: 1155
		[XmlElement(ElementName = "Field", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FilterKeyType internalField;

		// Token: 0x04000484 RID: 1156
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalFieldSpecified;

		// Token: 0x04000485 RID: 1157
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Operator", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FilterOperatorType internalOperator;

		// Token: 0x04000486 RID: 1158
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalOperatorSpecified;

		// Token: 0x04000487 RID: 1159
		[XmlElement(Type = typeof(StringWithCharSetType), ElementName = "Value", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public StringWithCharSetType internalValue;
	}
}
