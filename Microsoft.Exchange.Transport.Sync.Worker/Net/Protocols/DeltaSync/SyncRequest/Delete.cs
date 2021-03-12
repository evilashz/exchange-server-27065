using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001AF RID: 431
	[XmlType(TypeName = "Delete", Namespace = "AirSync:")]
	[Serializable]
	public class Delete
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0001E699 File Offset: 0x0001C899
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x0001E6A1 File Offset: 0x0001C8A1
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

		// Token: 0x040006E5 RID: 1765
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalServerId;
	}
}
