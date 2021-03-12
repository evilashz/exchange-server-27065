using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200022A RID: 554
	[DataContract]
	public sealed class MigrationReportEntry
	{
		// Token: 0x060027A9 RID: 10153 RVA: 0x0007CCC0 File Offset: 0x0007AEC0
		public MigrationReportEntry(string creationTime, string reportUrl, string result)
		{
			this.CreationTime = creationTime;
			this.ReportUrl = reportUrl;
			this.Result = result;
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x0007CCDD File Offset: 0x0007AEDD
		// (set) Token: 0x060027AB RID: 10155 RVA: 0x0007CCE5 File Offset: 0x0007AEE5
		[DataMember]
		public string CreationTime { get; private set; }

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x0007CCEE File Offset: 0x0007AEEE
		// (set) Token: 0x060027AD RID: 10157 RVA: 0x0007CCF6 File Offset: 0x0007AEF6
		[DataMember]
		public string Result { get; private set; }

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x0007CCFF File Offset: 0x0007AEFF
		// (set) Token: 0x060027AF RID: 10159 RVA: 0x0007CD07 File Offset: 0x0007AF07
		[DataMember]
		public string ReportUrl { get; private set; }

		// Token: 0x060027B0 RID: 10160 RVA: 0x0007CD10 File Offset: 0x0007AF10
		public override int GetHashCode()
		{
			return this.CreationTime.GetHashCode();
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x0007CD20 File Offset: 0x0007AF20
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			MigrationReportEntry migrationReportEntry = (MigrationReportEntry)obj;
			return this == migrationReportEntry || (string.Equals(this.CreationTime, migrationReportEntry.CreationTime) && string.Equals(this.Result, migrationReportEntry.Result) && string.Equals(this.ReportUrl, migrationReportEntry.ReportUrl));
		}
	}
}
