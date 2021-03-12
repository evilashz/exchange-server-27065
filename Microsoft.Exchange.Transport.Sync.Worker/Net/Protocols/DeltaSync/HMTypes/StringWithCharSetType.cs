using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes
{
	// Token: 0x020000B3 RID: 179
	[XmlType(TypeName = "StringWithCharSetType", Namespace = "HMTYPES:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class StringWithCharSetType
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00019F7F File Offset: 0x0001817F
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00019F87 File Offset: 0x00018187
		[XmlIgnore]
		public string charset
		{
			get
			{
				return this.internalcharset;
			}
			set
			{
				this.internalcharset = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00019F90 File Offset: 0x00018190
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00019F98 File Offset: 0x00018198
		[XmlIgnore]
		public string Value
		{
			get
			{
				return this.internalValue;
			}
			set
			{
				this.internalValue = value;
			}
		}

		// Token: 0x04000396 RID: 918
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlAttribute(AttributeName = "charset", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMTYPES:")]
		public string internalcharset;

		// Token: 0x04000397 RID: 919
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlText(DataType = "string")]
		public string internalValue;
	}
}
