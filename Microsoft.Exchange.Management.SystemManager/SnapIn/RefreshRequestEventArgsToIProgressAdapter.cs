using System;
using System.ComponentModel;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Management.SystemManager;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000278 RID: 632
	internal class RefreshRequestEventArgsToIProgressAdapter : IReportProgress
	{
		// Token: 0x06001AF6 RID: 6902 RVA: 0x00076C13 File Offset: 0x00074E13
		public RefreshRequestEventArgsToIProgressAdapter(RefreshRequestEventArgs eventArgs)
		{
			this.eventArgs = eventArgs;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00076C22 File Offset: 0x00074E22
		public void ReportProgress(int workProcessed, int totalWork, string statusText, string errorHeader)
		{
			this.eventArgs.ReportProgress(workProcessed, totalWork, statusText, null);
			this.workProcessed = workProcessed;
			this.statusText = statusText;
			if (!string.IsNullOrEmpty(errorHeader))
			{
				this.errorHeader = errorHeader;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x00076C52 File Offset: 0x00074E52
		internal string StepDescription
		{
			get
			{
				return this.statusText;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00076C5A File Offset: 0x00074E5A
		internal string ErrorHeader
		{
			get
			{
				return this.errorHeader;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x00076C62 File Offset: 0x00074E62
		[DefaultValue(0)]
		internal int Value
		{
			get
			{
				return this.workProcessed;
			}
		}

		// Token: 0x04000A08 RID: 2568
		private int workProcessed;

		// Token: 0x04000A09 RID: 2569
		private string statusText;

		// Token: 0x04000A0A RID: 2570
		private string errorHeader;

		// Token: 0x04000A0B RID: 2571
		private RefreshRequestEventArgs eventArgs;
	}
}
