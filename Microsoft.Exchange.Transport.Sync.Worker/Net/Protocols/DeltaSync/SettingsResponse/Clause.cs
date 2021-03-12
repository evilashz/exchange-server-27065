using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000145 RID: 325
	[XmlType(TypeName = "Clause", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Clause
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001C615 File Offset: 0x0001A815
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0001C61D File Offset: 0x0001A81D
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

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001C62D File Offset: 0x0001A82D
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0001C635 File Offset: 0x0001A835
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0001C645 File Offset: 0x0001A845
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0001C660 File Offset: 0x0001A860
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

		// Token: 0x04000529 RID: 1321
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Field", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FilterKeyType internalField;

		// Token: 0x0400052A RID: 1322
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalFieldSpecified;

		// Token: 0x0400052B RID: 1323
		[XmlElement(ElementName = "Operator", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FilterOperatorType internalOperator;

		// Token: 0x0400052C RID: 1324
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalOperatorSpecified;

		// Token: 0x0400052D RID: 1325
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(StringWithVersionType), ElementName = "Value", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public StringWithVersionType internalValue;
	}
}
