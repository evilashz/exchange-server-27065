using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200040A RID: 1034
	[DataContract]
	public class JournalReportNdrTo : BaseRow
	{
		// Token: 0x060034E3 RID: 13539 RVA: 0x000A4FBA File Offset: 0x000A31BA
		public JournalReportNdrTo(TransportConfigContainer container) : base(null, container)
		{
			this.MyContainer = container;
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000A4FCC File Offset: 0x000A31CC
		// (set) Token: 0x060034E5 RID: 13541 RVA: 0x000A5005 File Offset: 0x000A3205
		[DataMember]
		public string JournalingReportNdrTo
		{
			get
			{
				SmtpAddress journalingReportNdrTo = this.MyContainer.JournalingReportNdrTo;
				if (journalingReportNdrTo == SmtpAddress.NullReversePath)
				{
					return string.Empty;
				}
				return journalingReportNdrTo.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000A500C File Offset: 0x000A320C
		// (set) Token: 0x060034E7 RID: 13543 RVA: 0x000A5034 File Offset: 0x000A3234
		[DataMember]
		public string JournalingReportNdrToOrSelectString
		{
			get
			{
				string journalingReportNdrTo = this.JournalingReportNdrTo;
				if (!string.IsNullOrEmpty(journalingReportNdrTo))
				{
					return journalingReportNdrTo;
				}
				return Strings.SelectJournalingReportNdrToText;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400254B RID: 9547
		private readonly TransportConfigContainer MyContainer;
	}
}
