using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000317 RID: 791
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUMCallDataRecordsType : BaseRequestType
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00028A61 File Offset: 0x00026C61
		// (set) Token: 0x060019EB RID: 6635 RVA: 0x00028A69 File Offset: 0x00026C69
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTimeField;
			}
			set
			{
				this.startDateTimeField = value;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00028A72 File Offset: 0x00026C72
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x00028A7A File Offset: 0x00026C7A
		[XmlIgnore]
		public bool StartDateTimeSpecified
		{
			get
			{
				return this.startDateTimeFieldSpecified;
			}
			set
			{
				this.startDateTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x00028A83 File Offset: 0x00026C83
		// (set) Token: 0x060019EF RID: 6639 RVA: 0x00028A8B File Offset: 0x00026C8B
		public DateTime EndDateTime
		{
			get
			{
				return this.endDateTimeField;
			}
			set
			{
				this.endDateTimeField = value;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00028A94 File Offset: 0x00026C94
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x00028A9C File Offset: 0x00026C9C
		[XmlIgnore]
		public bool EndDateTimeSpecified
		{
			get
			{
				return this.endDateTimeFieldSpecified;
			}
			set
			{
				this.endDateTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00028AA5 File Offset: 0x00026CA5
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x00028AAD File Offset: 0x00026CAD
		public int Offset
		{
			get
			{
				return this.offsetField;
			}
			set
			{
				this.offsetField = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x00028AB6 File Offset: 0x00026CB6
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x00028ABE File Offset: 0x00026CBE
		[XmlIgnore]
		public bool OffsetSpecified
		{
			get
			{
				return this.offsetFieldSpecified;
			}
			set
			{
				this.offsetFieldSpecified = value;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x00028AC7 File Offset: 0x00026CC7
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x00028ACF File Offset: 0x00026CCF
		public int NumberOfRecords
		{
			get
			{
				return this.numberOfRecordsField;
			}
			set
			{
				this.numberOfRecordsField = value;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00028AD8 File Offset: 0x00026CD8
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x00028AE0 File Offset: 0x00026CE0
		[XmlIgnore]
		public bool NumberOfRecordsSpecified
		{
			get
			{
				return this.numberOfRecordsFieldSpecified;
			}
			set
			{
				this.numberOfRecordsFieldSpecified = value;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x00028AE9 File Offset: 0x00026CE9
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x00028AF1 File Offset: 0x00026CF1
		public string UserLegacyExchangeDN
		{
			get
			{
				return this.userLegacyExchangeDNField;
			}
			set
			{
				this.userLegacyExchangeDNField = value;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00028AFA File Offset: 0x00026CFA
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x00028B02 File Offset: 0x00026D02
		public UMCDRFilterByType FilterBy
		{
			get
			{
				return this.filterByField;
			}
			set
			{
				this.filterByField = value;
			}
		}

		// Token: 0x0400116B RID: 4459
		private DateTime startDateTimeField;

		// Token: 0x0400116C RID: 4460
		private bool startDateTimeFieldSpecified;

		// Token: 0x0400116D RID: 4461
		private DateTime endDateTimeField;

		// Token: 0x0400116E RID: 4462
		private bool endDateTimeFieldSpecified;

		// Token: 0x0400116F RID: 4463
		private int offsetField;

		// Token: 0x04001170 RID: 4464
		private bool offsetFieldSpecified;

		// Token: 0x04001171 RID: 4465
		private int numberOfRecordsField;

		// Token: 0x04001172 RID: 4466
		private bool numberOfRecordsFieldSpecified;

		// Token: 0x04001173 RID: 4467
		private string userLegacyExchangeDNField;

		// Token: 0x04001174 RID: 4468
		private UMCDRFilterByType filterByField;
	}
}
