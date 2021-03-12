using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001BE RID: 446
	[XmlType(TypeName = "Change", Namespace = "AirSync:")]
	[Serializable]
	public class Change
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0001EB78 File Offset: 0x0001CD78
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x0001EB80 File Offset: 0x0001CD80
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

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0001EB89 File Offset: 0x0001CD89
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
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

		// Token: 0x040006FB RID: 1787
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalServerId;

		// Token: 0x040006FC RID: 1788
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ApplicationData), ElementName = "ApplicationData", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public ApplicationData internalApplicationData;
	}
}
