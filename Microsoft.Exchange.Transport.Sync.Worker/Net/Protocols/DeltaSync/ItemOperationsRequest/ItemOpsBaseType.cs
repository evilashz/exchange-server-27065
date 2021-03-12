using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000B7 RID: 183
	[XmlType(TypeName = "ItemOpsBaseType", Namespace = "ItemOperations:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ItemOpsBaseType
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001A03F File Offset: 0x0001823F
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x0001A047 File Offset: 0x00018247
		[XmlIgnore]
		public string Class
		{
			get
			{
				return this.internalClass;
			}
			set
			{
				this.internalClass = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001A050 File Offset: 0x00018250
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001A058 File Offset: 0x00018258
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

		// Token: 0x04000399 RID: 921
		[XmlElement(ElementName = "Class", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalClass;

		// Token: 0x0400039A RID: 922
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMMAIL:")]
		public string internalServerId;
	}
}
