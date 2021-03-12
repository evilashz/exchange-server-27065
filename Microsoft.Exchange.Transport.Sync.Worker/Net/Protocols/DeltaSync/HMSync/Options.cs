using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000AD RID: 173
	[XmlRoot(ElementName = "Options", Namespace = "HMSYNC:", IsNullable = false)]
	[Serializable]
	public class Options
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00019EC9 File Offset: 0x000180C9
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00019ED1 File Offset: 0x000180D1
		[XmlIgnore]
		public byte Conflict
		{
			get
			{
				return this.internalConflict;
			}
			set
			{
				this.internalConflict = value;
				this.internalConflictSpecified = true;
			}
		}

		// Token: 0x04000388 RID: 904
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Conflict", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMSYNC:")]
		public byte internalConflict;

		// Token: 0x04000389 RID: 905
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalConflictSpecified;
	}
}
