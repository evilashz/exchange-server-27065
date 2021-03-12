using System;
using FUSE.Paxos;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x02000065 RID: 101
	public class PeriodicPaxosTrancator
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public PeriodicPaxosTrancator(DxStoreInstance instance)
		{
			this.instance = instance;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000C5FA File Offset: 0x0000A7FA
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.TruncatorTracer;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000C604 File Offset: 0x0000A804
		public bool IsStarted
		{
			get
			{
				bool result;
				lock (this.locker)
				{
					result = (this.timer != null);
				}
				return result;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C654 File Offset: 0x0000A854
		public void Start()
		{
			if (this.instance.IsStopping)
			{
				return;
			}
			lock (this.locker)
			{
				if (this.timer != null)
				{
					this.timer.Dispose(true);
					this.timer = null;
				}
				PeriodicPaxosTrancator.Tracer.TraceDebug<string>((long)this.instance.IdentityHash, "{0}: Starting truncator timer", this.instance.GroupConfig.Identity);
				this.timer = new GuardedTimer(delegate(object unused)
				{
					this.TruncateCallback();
				}, null, TimeSpan.Zero, this.instance.GroupConfig.Settings.TruncationPeriodicCheckInterval);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C71C File Offset: 0x0000A91C
		public void TruncateIfRequired()
		{
			lock (this.locker)
			{
				if (this.instance.StateMachine != null)
				{
					IStorage<string, DxStoreCommand> storage = this.instance.StateMachine.Paxos.Storage;
					if (storage != null)
					{
						int countExecuted = this.instance.StateMachine.CountExecuted;
						int countTruncated = storage.CountTruncated;
						int num = countExecuted - countTruncated;
						PeriodicPaxosTrancator.Tracer.TraceDebug((long)this.instance.IdentityHash, "{0}: CountExecuted: {1} CountTruncated: {2} Limit: {3} PaddingLength: {4}", new object[]
						{
							this.instance.GroupConfig.Identity,
							countExecuted,
							countTruncated,
							this.instance.GroupConfig.Settings.TruncationLimit,
							this.instance.GroupConfig.Settings.TruncationPaddingLength
						});
						if (num > this.instance.GroupConfig.Settings.TruncationLimit + this.instance.GroupConfig.Settings.TruncationPaddingLength)
						{
							int num2 = countExecuted - this.instance.GroupConfig.Settings.TruncationLimit;
							PeriodicPaxosTrancator.Tracer.TraceInformation<string, int, int>(0, (long)this.instance.IdentityHash, "{0}: Starting to truncate upto {1} (Diff: {2})", this.instance.GroupConfig.Identity, num2, num);
							storage.Truncate(num2);
							PeriodicPaxosTrancator.Tracer.TraceInformation<string, int>(0, (long)this.instance.IdentityHash, "{0}: Finished truncating upto {1}", this.instance.GroupConfig.Identity, num2);
						}
					}
					else
					{
						PeriodicPaxosTrancator.Tracer.TraceWarning<string>((long)this.instance.IdentityHash, "{0}: Skipped truncation checks since storage is not initialized yet", this.instance.GroupConfig.Identity);
					}
				}
				else
				{
					PeriodicPaxosTrancator.Tracer.TraceWarning<string>((long)this.instance.IdentityHash, "{0}: Skipped truncation checks since state machine is not initialized yet", this.instance.GroupConfig.Identity);
				}
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000C940 File Offset: 0x0000AB40
		public void Stop()
		{
			lock (this.locker)
			{
				if (this.timer != null)
				{
					PeriodicPaxosTrancator.Tracer.TraceDebug<string>((long)this.instance.IdentityHash, "{0}: Stopping truncator timer (disposing)", this.instance.GroupConfig.Identity);
					this.timer.Dispose(true);
					this.timer = null;
				}
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		private void TruncateCallback()
		{
			if (!this.instance.IsStopping)
			{
				this.instance.RunBestEffortOperation("TruncateIfRequired", delegate
				{
					this.TruncateIfRequired();
				}, LogOptions.LogException, null, null, null, null);
			}
		}

		// Token: 0x04000208 RID: 520
		private readonly object locker = new object();

		// Token: 0x04000209 RID: 521
		private readonly DxStoreInstance instance;

		// Token: 0x0400020A RID: 522
		private GuardedTimer timer;
	}
}
