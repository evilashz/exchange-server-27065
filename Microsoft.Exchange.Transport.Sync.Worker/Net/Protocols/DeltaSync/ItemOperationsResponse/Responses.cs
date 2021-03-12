using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000CD RID: 205
	[XmlType(TypeName = "Responses", Namespace = "ItemOperations:")]
	[Serializable]
	public class Responses
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001A495 File Offset: 0x00018695
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001A4B0 File Offset: 0x000186B0
		[XmlIgnore]
		public Fetch Fetch
		{
			get
			{
				if (this.internalFetch == null)
				{
					this.internalFetch = new Fetch();
				}
				return this.internalFetch;
			}
			set
			{
				this.internalFetch = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001A4B9 File Offset: 0x000186B9
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0001A4D4 File Offset: 0x000186D4
		[XmlIgnore]
		public Scan Scan
		{
			get
			{
				if (this.internalScan == null)
				{
					this.internalScan = new Scan();
				}
				return this.internalScan;
			}
			set
			{
				this.internalScan = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001A4DD File Offset: 0x000186DD
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0001A4F8 File Offset: 0x000186F8
		[XmlIgnore]
		public ReportAsSpamCollection ReportAsSpamCollection
		{
			get
			{
				if (this.internalReportAsSpamCollection == null)
				{
					this.internalReportAsSpamCollection = new ReportAsSpamCollection();
				}
				return this.internalReportAsSpamCollection;
			}
			set
			{
				this.internalReportAsSpamCollection = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001A501 File Offset: 0x00018701
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001A51C File Offset: 0x0001871C
		[XmlIgnore]
		public ReportAsNotSpamCollection ReportAsNotSpamCollection
		{
			get
			{
				if (this.internalReportAsNotSpamCollection == null)
				{
					this.internalReportAsNotSpamCollection = new ReportAsNotSpamCollection();
				}
				return this.internalReportAsNotSpamCollection;
			}
			set
			{
				this.internalReportAsNotSpamCollection = value;
			}
		}

		// Token: 0x040003BA RID: 954
		[XmlElement(Type = typeof(Fetch), ElementName = "Fetch", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Fetch internalFetch;

		// Token: 0x040003BB RID: 955
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Scan), ElementName = "Scan", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public Scan internalScan;

		// Token: 0x040003BC RID: 956
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ReportAsSpam), ElementName = "ReportAsSpam", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public ReportAsSpamCollection internalReportAsSpamCollection;

		// Token: 0x040003BD RID: 957
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ReportAsNotSpam), ElementName = "ReportAsNotSpam", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public ReportAsNotSpamCollection internalReportAsNotSpamCollection;
	}
}
