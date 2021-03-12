using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000CE RID: 206
	[XmlType(TypeName = "Fetch", Namespace = "ItemOperations:")]
	[Serializable]
	public class Fetch
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001A52D File Offset: 0x0001872D
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001A535 File Offset: 0x00018735
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001A53E File Offset: 0x0001873E
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001A546 File Offset: 0x00018746
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001A556 File Offset: 0x00018756
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001A571 File Offset: 0x00018771
		[XmlIgnore]
		public Message Message
		{
			get
			{
				if (this.internalMessage == null)
				{
					this.internalMessage = new Message();
				}
				return this.internalMessage;
			}
			set
			{
				this.internalMessage = value;
			}
		}

		// Token: 0x040003BE RID: 958
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		public string internalServerId;

		// Token: 0x040003BF RID: 959
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040003C0 RID: 960
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040003C1 RID: 961
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Message), ElementName = "Message", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public Message internalMessage;
	}
}
