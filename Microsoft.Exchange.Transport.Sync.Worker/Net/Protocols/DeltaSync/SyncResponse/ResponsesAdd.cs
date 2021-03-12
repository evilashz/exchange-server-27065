using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001C4 RID: 452
	[XmlType(TypeName = "ResponsesAdd", Namespace = "AirSync:")]
	[Serializable]
	public class ResponsesAdd
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0001EFA8 File Offset: 0x0001D1A8
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
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

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0001EFB9 File Offset: 0x0001D1B9
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0001EFC1 File Offset: 0x0001D1C1
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

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0001EFCA File Offset: 0x0001D1CA
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0001EFD2 File Offset: 0x0001D1D2
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

		// Token: 0x04000731 RID: 1841
		[XmlElement(ElementName = "ClientId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalClientId;

		// Token: 0x04000732 RID: 1842
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalServerId;

		// Token: 0x04000733 RID: 1843
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "AirSync:")]
		public int internalStatus;

		// Token: 0x04000734 RID: 1844
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;
	}
}
