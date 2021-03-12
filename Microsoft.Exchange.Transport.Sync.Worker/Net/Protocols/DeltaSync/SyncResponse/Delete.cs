using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001BD RID: 445
	[XmlType(TypeName = "Delete", Namespace = "AirSync:")]
	[Serializable]
	public class Delete
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0001EB5F File Offset: 0x0001CD5F
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0001EB67 File Offset: 0x0001CD67
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

		// Token: 0x040006FA RID: 1786
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalServerId;
	}
}
