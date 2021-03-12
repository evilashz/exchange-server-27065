using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000D0 RID: 208
	[XmlType(TypeName = "Scan", Namespace = "ItemOperations:")]
	[Serializable]
	public class Scan
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001A5BF File Offset: 0x000187BF
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001A5C7 File Offset: 0x000187C7
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

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001A5D0 File Offset: 0x000187D0
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0001A5D8 File Offset: 0x000187D8
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

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001A5E8 File Offset: 0x000187E8
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0001A603 File Offset: 0x00018803
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

		// Token: 0x040003C4 RID: 964
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		public string internalServerId;

		// Token: 0x040003C5 RID: 965
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "ItemOperations:")]
		public int internalStatus;

		// Token: 0x040003C6 RID: 966
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040003C7 RID: 967
		[XmlElement(Type = typeof(ScanInfoType), ElementName = "ScanInfo", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ScanInfoType internalScanInfo;
	}
}
