using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes
{
	// Token: 0x020000B2 RID: 178
	[XmlType(TypeName = "StringWithVersionType", Namespace = "HMTYPES:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class StringWithVersionType
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00019F4E File Offset: 0x0001814E
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x00019F56 File Offset: 0x00018156
		[XmlIgnore]
		public int version
		{
			get
			{
				return this.internalversion;
			}
			set
			{
				this.internalversion = value;
				this.internalversionSpecified = true;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00019F66 File Offset: 0x00018166
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x00019F6E File Offset: 0x0001816E
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

		// Token: 0x04000393 RID: 915
		[XmlAttribute(AttributeName = "version", Form = XmlSchemaForm.Unqualified, DataType = "int", Namespace = "HMTYPES:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalversion;

		// Token: 0x04000394 RID: 916
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalversionSpecified;

		// Token: 0x04000395 RID: 917
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlText(DataType = "string")]
		public string internalValue;
	}
}
