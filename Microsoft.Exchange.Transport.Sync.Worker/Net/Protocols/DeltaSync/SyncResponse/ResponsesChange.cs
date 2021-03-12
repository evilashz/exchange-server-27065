using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001C2 RID: 450
	[XmlType(TypeName = "ResponsesChange", Namespace = "AirSync:")]
	[Serializable]
	public class ResponsesChange
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0001EF46 File Offset: 0x0001D146
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0001EF4E File Offset: 0x0001D14E
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

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0001EF57 File Offset: 0x0001D157
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0001EF5F File Offset: 0x0001D15F
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

		// Token: 0x0400072B RID: 1835
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalServerId;

		// Token: 0x0400072C RID: 1836
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "AirSync:")]
		public int internalStatus;

		// Token: 0x0400072D RID: 1837
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalStatusSpecified;
	}
}
