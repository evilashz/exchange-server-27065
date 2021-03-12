using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendResponse
{
	// Token: 0x020000DF RID: 223
	[XmlType(TypeName = "SaveItem", Namespace = "Send:")]
	[Serializable]
	public class SaveItem
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001AA86 File Offset: 0x00018C86
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0001AA8E File Offset: 0x00018C8E
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001AA9E File Offset: 0x00018C9E
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001AAB9 File Offset: 0x00018CB9
		[XmlIgnore]
		public Fault Fault
		{
			get
			{
				if (this.internalFault == null)
				{
					this.internalFault = new Fault();
				}
				return this.internalFault;
			}
			set
			{
				this.internalFault = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001AAC2 File Offset: 0x00018CC2
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001AACA File Offset: 0x00018CCA
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

		// Token: 0x040003E9 RID: 1001
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "Send:")]
		public int internalStatus;

		// Token: 0x040003EA RID: 1002
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalStatusSpecified;

		// Token: 0x040003EB RID: 1003
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Fault), ElementName = "Fault", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public Fault internalFault;

		// Token: 0x040003EC RID: 1004
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "Send:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalServerId;
	}
}
