using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000C5 RID: 197
	[XmlRoot(ElementName = "ItemOperations", Namespace = "ItemOperations:", IsNullable = false)]
	[Serializable]
	public class ItemOperations
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001A256 File Offset: 0x00018456
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0001A271 File Offset: 0x00018471
		[XmlIgnore]
		public FetchType Fetch
		{
			get
			{
				if (this.internalFetch == null)
				{
					this.internalFetch = new FetchType();
				}
				return this.internalFetch;
			}
			set
			{
				this.internalFetch = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001A27A File Offset: 0x0001847A
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0001A295 File Offset: 0x00018495
		[XmlIgnore]
		public ScanType Scan
		{
			get
			{
				if (this.internalScan == null)
				{
					this.internalScan = new ScanType();
				}
				return this.internalScan;
			}
			set
			{
				this.internalScan = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001A29E File Offset: 0x0001849E
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0001A2B9 File Offset: 0x000184B9
		[XmlIgnore]
		public ReportAsSpamTypeCollection ReportAsSpamCollection
		{
			get
			{
				if (this.internalReportAsSpamCollection == null)
				{
					this.internalReportAsSpamCollection = new ReportAsSpamTypeCollection();
				}
				return this.internalReportAsSpamCollection;
			}
			set
			{
				this.internalReportAsSpamCollection = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001A2C2 File Offset: 0x000184C2
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0001A2DD File Offset: 0x000184DD
		[XmlIgnore]
		public ReportAsNotSpamTypeCollection ReportAsNotSpamCollection
		{
			get
			{
				if (this.internalReportAsNotSpamCollection == null)
				{
					this.internalReportAsNotSpamCollection = new ReportAsNotSpamTypeCollection();
				}
				return this.internalReportAsNotSpamCollection;
			}
			set
			{
				this.internalReportAsNotSpamCollection = value;
			}
		}

		// Token: 0x040003A9 RID: 937
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FetchType), ElementName = "Fetch", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public FetchType internalFetch;

		// Token: 0x040003AA RID: 938
		[XmlElement(Type = typeof(ScanType), ElementName = "Scan", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ScanType internalScan;

		// Token: 0x040003AB RID: 939
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ReportAsSpamType), ElementName = "ReportAsSpam", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public ReportAsSpamTypeCollection internalReportAsSpamCollection;

		// Token: 0x040003AC RID: 940
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ReportAsNotSpamType), ElementName = "ReportAsNotSpam", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public ReportAsNotSpamTypeCollection internalReportAsNotSpamCollection;
	}
}
