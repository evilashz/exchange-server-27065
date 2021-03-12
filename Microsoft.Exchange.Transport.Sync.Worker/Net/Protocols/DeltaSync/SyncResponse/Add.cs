using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001C0 RID: 448
	[XmlType(TypeName = "Add", Namespace = "AirSync:")]
	[Serializable]
	public class Add
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0001EE95 File Offset: 0x0001D095
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0001EE9D File Offset: 0x0001D09D
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0001EEA6 File Offset: 0x0001D0A6
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0001EEC1 File Offset: 0x0001D0C1
		[XmlIgnore]
		public ApplicationData ApplicationData
		{
			get
			{
				if (this.internalApplicationData == null)
				{
					this.internalApplicationData = new ApplicationData();
				}
				return this.internalApplicationData;
			}
			set
			{
				this.internalApplicationData = value;
			}
		}

		// Token: 0x04000726 RID: 1830
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalServerId;

		// Token: 0x04000727 RID: 1831
		[XmlElement(Type = typeof(ApplicationData), ElementName = "ApplicationData", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ApplicationData internalApplicationData;
	}
}
