using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200010E RID: 270
	public class ProgressReportEventArgs : EventArgs
	{
		// Token: 0x06001FAF RID: 8111 RVA: 0x0005F89B File Offset: 0x0005DA9B
		public ProgressReportEventArgs(IList<ErrorRecord> errors, int percent, string status)
		{
			this.Errors = errors;
			this.Percent = percent;
			this.Status = status;
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0005F8B8 File Offset: 0x0005DAB8
		// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x0005F8C0 File Offset: 0x0005DAC0
		public IList<ErrorRecord> Errors { get; internal set; }

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0005F8C9 File Offset: 0x0005DAC9
		// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x0005F8D1 File Offset: 0x0005DAD1
		public int Percent { get; internal set; }

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0005F8DA File Offset: 0x0005DADA
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x0005F8E2 File Offset: 0x0005DAE2
		public string Status { get; private set; }

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0005F8EB File Offset: 0x0005DAEB
		public override string ToString()
		{
			return string.Format("Errors: {0}, Percent: {1}, Status: {2}", this.Errors.ToJsonString(DDIService.KnownTypes.Value), this.Percent, this.Status);
		}
	}
}
