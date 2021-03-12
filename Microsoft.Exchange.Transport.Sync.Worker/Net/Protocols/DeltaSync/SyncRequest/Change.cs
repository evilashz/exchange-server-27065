using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001AE RID: 430
	[XmlType(TypeName = "Change", Namespace = "AirSync:")]
	[Serializable]
	public class Change
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0001E65C File Offset: 0x0001C85C
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x0001E664 File Offset: 0x0001C864
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

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0001E66D File Offset: 0x0001C86D
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x0001E688 File Offset: 0x0001C888
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

		// Token: 0x040006E3 RID: 1763
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalServerId;

		// Token: 0x040006E4 RID: 1764
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ApplicationDataType), ElementName = "ApplicationData", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public ApplicationDataType internalApplicationData;
	}
}
