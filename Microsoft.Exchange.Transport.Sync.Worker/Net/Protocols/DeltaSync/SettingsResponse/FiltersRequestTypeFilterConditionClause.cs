using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200014F RID: 335
	[XmlType(TypeName = "FiltersRequestTypeFilterConditionClause", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterConditionClause
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001CA81 File Offset: 0x0001AC81
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0001CA89 File Offset: 0x0001AC89
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

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001CA99 File Offset: 0x0001AC99
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0001CAA1 File Offset: 0x0001ACA1
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

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001CAB1 File Offset: 0x0001ACB1
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0001CACC File Offset: 0x0001ACCC
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

		// Token: 0x0400055C RID: 1372
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Field", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FilterKeyType internalField;

		// Token: 0x0400055D RID: 1373
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalFieldSpecified;

		// Token: 0x0400055E RID: 1374
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Operator", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FilterOperatorType internalOperator;

		// Token: 0x0400055F RID: 1375
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalOperatorSpecified;

		// Token: 0x04000560 RID: 1376
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(StringWithCharSetType), ElementName = "Value", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public StringWithCharSetType internalValue;
	}
}
