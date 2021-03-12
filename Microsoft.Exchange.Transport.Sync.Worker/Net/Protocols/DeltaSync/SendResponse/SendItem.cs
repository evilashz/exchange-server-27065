using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendResponse
{
	// Token: 0x020000DE RID: 222
	[XmlType(TypeName = "SendItem", Namespace = "Send:")]
	[Serializable]
	public class SendItem
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001AA1E File Offset: 0x00018C1E
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001AA26 File Offset: 0x00018C26
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001AA36 File Offset: 0x00018C36
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001AA51 File Offset: 0x00018C51
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001AA5A File Offset: 0x00018C5A
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001AA75 File Offset: 0x00018C75
		[XmlIgnore]
		public ScanInfoType ScanInfo
		{
			get
			{
				if (this.internalScanInfo == null)
				{
					this.internalScanInfo = new ScanInfoType();
				}
				return this.internalScanInfo;
			}
			set
			{
				this.internalScanInfo = value;
			}
		}

		// Token: 0x040003E5 RID: 997
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "Send:")]
		public int internalStatus;

		// Token: 0x040003E6 RID: 998
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalStatusSpecified;

		// Token: 0x040003E7 RID: 999
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Fault), ElementName = "Fault", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public Fault internalFault;

		// Token: 0x040003E8 RID: 1000
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ScanInfoType), ElementName = "ScanInfo", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		public ScanInfoType internalScanInfo;
	}
}
