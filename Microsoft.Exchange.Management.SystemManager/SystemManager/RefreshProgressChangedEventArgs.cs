using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200004A RID: 74
	public class RefreshProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000AA64 File Offset: 0x00008C64
		public RefreshProgressChangedEventArgs(RefreshRequestEventArgs request, int workProcessed, int totalWork, string statusText, object userState) : base(RefreshProgressChangedEventArgs.GetPercentage(workProcessed, totalWork), userState)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.request = request;
			this.workProcessed = workProcessed;
			this.totalWork = totalWork;
			this.statusText = statusText;
			this.isFirstProgressReport = !request.ReportedProgress;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		private static int GetPercentage(int workProcessed, int totalWork)
		{
			if (totalWork <= 0)
			{
				throw new ArgumentOutOfRangeException("totalWork", totalWork, "totalWork <= 0");
			}
			if (workProcessed < 0)
			{
				throw new ArgumentOutOfRangeException("workProcessed", workProcessed, "workProcessed < 0");
			}
			if (workProcessed > totalWork)
			{
				throw new ArgumentOutOfRangeException("workProcessed", workProcessed, "workProcessed > totalWork");
			}
			return checked(workProcessed * 100) / totalWork;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000AB31 File Offset: 0x00008D31
		public object RequestArgument
		{
			get
			{
				return this.request.Argument;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000AB3E File Offset: 0x00008D3E
		public RefreshRequestEventArgs Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000AB46 File Offset: 0x00008D46
		public bool CancellationPending
		{
			get
			{
				return this.request.CancellationPending;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000AB53 File Offset: 0x00008D53
		public bool IsFullRefresh
		{
			get
			{
				return this.request.IsFullRefresh;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000AB60 File Offset: 0x00008D60
		public int WorkProcessed
		{
			get
			{
				return this.workProcessed;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000AB68 File Offset: 0x00008D68
		public int TotalWork
		{
			get
			{
				return this.totalWork;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000AB70 File Offset: 0x00008D70
		public string StatusText
		{
			get
			{
				return this.statusText;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000AB78 File Offset: 0x00008D78
		public bool IsFirstProgressReport
		{
			get
			{
				return this.isFirstProgressReport;
			}
		}

		// Token: 0x040000C4 RID: 196
		private RefreshRequestEventArgs request;

		// Token: 0x040000C5 RID: 197
		private int workProcessed;

		// Token: 0x040000C6 RID: 198
		private int totalWork = 100;

		// Token: 0x040000C7 RID: 199
		private string statusText = "";

		// Token: 0x040000C8 RID: 200
		private bool isFirstProgressReport;
	}
}
