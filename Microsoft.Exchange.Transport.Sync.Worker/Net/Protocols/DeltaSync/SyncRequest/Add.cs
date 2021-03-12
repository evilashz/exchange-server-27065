using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001B0 RID: 432
	[XmlType(TypeName = "Add", Namespace = "AirSync:")]
	[Serializable]
	public class Add
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0001E6B2 File Offset: 0x0001C8B2
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x0001E6BA File Offset: 0x0001C8BA
		[XmlIgnore]
		public string ClientId
		{
			get
			{
				return this.internalClientId;
			}
			set
			{
				this.internalClientId = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0001E6C3 File Offset: 0x0001C8C3
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0001E6CB File Offset: 0x0001C8CB
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

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0001E6EF File Offset: 0x0001C8EF
		[XmlIgnore]
		public ApplicationDataType ApplicationData
		{
			get
			{
				if (this.internalApplicationData == null)
				{
					this.internalApplicationData = new ApplicationDataType();
				}
				return this.internalApplicationData;
			}
			set
			{
				this.internalApplicationData = value;
			}
		}

		// Token: 0x040006E6 RID: 1766
		[XmlElement(ElementName = "ClientId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalClientId;

		// Token: 0x040006E7 RID: 1767
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalServerId;

		// Token: 0x040006E8 RID: 1768
		[XmlElement(Type = typeof(ApplicationDataType), ElementName = "ApplicationData", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ApplicationDataType internalApplicationData;
	}
}
