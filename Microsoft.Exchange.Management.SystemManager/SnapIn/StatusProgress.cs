using System;
using System.ComponentModel;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000270 RID: 624
	[Serializable]
	internal sealed class StatusProgress : IProgress
	{
		// Token: 0x06001ABF RID: 6847 RVA: 0x00075E97 File Offset: 0x00074097
		public StatusProgress(Status status, ISynchronizeInvoke owner)
		{
			this.status = status;
			this.owner = owner;
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x00075EB8 File Offset: 0x000740B8
		bool IProgress.Canceled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00075EBC File Offset: 0x000740BC
		void IProgress.ReportProgress(int workProcessed, int totalWork, string statusText)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<int, int, string>((long)this.GetHashCode(), "*--StatusProgress.ReportProgress: {0}/{1}: {2}", workProcessed, totalWork, statusText);
			if (!this.hasCompleted)
			{
				this.status.ReportProgress(workProcessed, totalWork, statusText);
				if (workProcessed == totalWork)
				{
					lock (this.statusLock)
					{
						if (!this.hasCompleted)
						{
							this.hasCompleted = true;
							this.owner.BeginInvoke(new StatusProgress.ReportCompletedDelegate(this.status.Complete), new object[]
							{
								statusText,
								totalWork > 0
							});
						}
					}
				}
			}
		}

		// Token: 0x040009F0 RID: 2544
		private Status status;

		// Token: 0x040009F1 RID: 2545
		private ISynchronizeInvoke owner;

		// Token: 0x040009F2 RID: 2546
		private bool hasCompleted;

		// Token: 0x040009F3 RID: 2547
		private object statusLock = new object();

		// Token: 0x02000271 RID: 625
		// (Invoke) Token: 0x06001AC3 RID: 6851
		private delegate void ReportCompletedDelegate(string statusText, bool succeeded);
	}
}
