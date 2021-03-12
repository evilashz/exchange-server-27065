using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001C3 RID: 451
	[XmlType(TypeName = "ResponsesDelete", Namespace = "AirSync:")]
	[Serializable]
	public class ResponsesDelete
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0001EF77 File Offset: 0x0001D177
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0001EF7F File Offset: 0x0001D17F
		[XmlIgnore]
		public string ServerId
		{
			get
			{
				return this.internalServerId;
			}
			set
			{
				this.internalServerId = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0001EF88 File Offset: 0x0001D188
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x0001EF90 File Offset: 0x0001D190
		[XmlIgnore]
		public int Status
		{
			get
			{
				return this.internalStatus;
			}
			set
			{
				this.internalStatus = value;
				this.internalStatusSpecified = true;
			}
		}

		// Token: 0x0400072E RID: 1838
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalServerId;

		// Token: 0x0400072F RID: 1839
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "AirSync:")]
		public int internalStatus;

		// Token: 0x04000730 RID: 1840
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalStatusSpecified;
	}
}
