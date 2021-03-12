using System;
using Microsoft.Exchange.Diagnostics.CmdletInfra;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200027F RID: 639
	internal class LatencyTrackingModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x00051EE8 File Offset: 0x000500E8
		public LatencyTrackingModule(TaskContext context)
		{
			this.context = context;
			CmdletLatencyTracker.StartInternalTracking(context.UniqueId, "ParameterBinding");
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00051F08 File Offset: 0x00050108
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00051F0C File Offset: 0x0005010C
		void ITaskModule.Init(ITaskEvent task)
		{
			task.PreInit += this.OnPreInit;
			task.InitCompleted += this.OnInitCompleted;
			task.PreIterate += this.OnPreIterate;
			task.IterateCompleted += this.OnIterateCompleted;
			task.PreRelease += this.OnPreRelease;
			task.Release += this.OnRelease;
			task.PreStop += this.OnPreStop;
			task.Stop += this.OnStop;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00051FA9 File Offset: 0x000501A9
		void ITaskModule.Dispose()
		{
			CmdletLatencyTracker.DisposeLatencyTracker(this.context.UniqueId);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00051FBC File Offset: 0x000501BC
		private void OnPreInit(object sender, EventArgs eventArgs)
		{
			Guid uniqueId = this.context.UniqueId;
			CmdletLatencyTracker.EndInternalTracking(uniqueId, "ParameterBinding");
			CmdletLatencyTracker.StartInternalTracking(this.context.UniqueId, "BeginProcessing");
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00051FF6 File Offset: 0x000501F6
		private void OnInitCompleted(object sender, EventArgs eventArgs)
		{
			CmdletLatencyTracker.EndInternalTracking(this.context.UniqueId, "BeginProcessing");
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0005200D File Offset: 0x0005020D
		private void OnPreIterate(object sender, EventArgs eventArgs)
		{
			CmdletLatencyTracker.StartInternalTracking(this.context.UniqueId, "ProcessRecord");
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00052025 File Offset: 0x00050225
		private void OnIterateCompleted(object sender, EventArgs eventArgs)
		{
			CmdletLatencyTracker.EndInternalTracking(this.context.UniqueId, "ProcessRecord");
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0005203C File Offset: 0x0005023C
		private void OnPreRelease(object sender, EventArgs eventArgs)
		{
			CmdletLatencyTracker.StartInternalTracking(this.context.UniqueId, "EndProcessing");
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00052054 File Offset: 0x00050254
		private void OnRelease(object sender, EventArgs eventArgs)
		{
			CmdletLatencyTracker.EndInternalTracking(this.context.UniqueId, "EndProcessing");
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0005206B File Offset: 0x0005026B
		private void OnPreStop(object sender, EventArgs eventArgs)
		{
			CmdletLatencyTracker.StartInternalTracking(this.context.UniqueId, "StopProcessing");
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00052083 File Offset: 0x00050283
		private void OnStop(object sender, EventArgs eventArgs)
		{
			CmdletLatencyTracker.EndInternalTracking(this.context.UniqueId, "StopProcessing");
		}

		// Token: 0x040006B2 RID: 1714
		private readonly TaskContext context;
	}
}
