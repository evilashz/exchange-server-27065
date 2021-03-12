using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200002C RID: 44
	internal sealed class ScopedQueue : ISchedulerQueue
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00004D28 File Offset: 0x00002F28
		public ScopedQueue(IMessageScope scope, ISchedulerQueue queueInstance, Func<DateTime> timeProvider = null)
		{
			ArgumentValidator.ThrowIfNull("scope", scope);
			ArgumentValidator.ThrowIfNull("queueInstance", queueInstance);
			this.scope = scope;
			this.queueInstance = queueInstance;
			this.timeProvider = timeProvider;
			this.Locked = true;
			DateTime currentTime = this.GetCurrentTime();
			this.LastActivity = currentTime;
			this.LockDateTime = currentTime;
			this.createDateTime = currentTime;
			this.queueLog = new ScopedQueueLog(currentTime);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004D95 File Offset: 0x00002F95
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004D9D File Offset: 0x00002F9D
		public DateTime LastActivity { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004DA6 File Offset: 0x00002FA6
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004DAE File Offset: 0x00002FAE
		public bool Locked { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004DB7 File Offset: 0x00002FB7
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004DBF File Offset: 0x00002FBF
		public DateTime LockDateTime { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public IMessageScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public DateTime CreateDateTime
		{
			get
			{
				return this.createDateTime;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004DD8 File Offset: 0x00002FD8
		public bool IsEmpty
		{
			get
			{
				return this.queueInstance.IsEmpty;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004DE5 File Offset: 0x00002FE5
		public long Count
		{
			get
			{
				return this.queueInstance.Count;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004DF2 File Offset: 0x00002FF2
		public void Enqueue(ISchedulableMessage message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			this.LastActivity = this.GetCurrentTime();
			this.queueInstance.Enqueue(message);
			this.queueLog.RecordEnqueue();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004E22 File Offset: 0x00003022
		public bool TryDequeue(out ISchedulableMessage message)
		{
			if (this.queueInstance.TryDequeue(out message))
			{
				this.LastActivity = this.GetCurrentTime();
				this.queueLog.RecordDequeue();
				return true;
			}
			return false;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004E4C File Offset: 0x0000304C
		public bool TryPeek(out ISchedulableMessage message)
		{
			return this.queueInstance.TryPeek(out message);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004E5A File Offset: 0x0000305A
		public void Lock()
		{
			this.Locked = true;
			this.LockDateTime = this.GetCurrentTime();
			this.queueLog.Lock(this.LockDateTime);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004E80 File Offset: 0x00003080
		public void Unlock()
		{
			this.Locked = false;
			this.queueLog.Unlock(this.GetCurrentTime());
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004E9A File Offset: 0x0000309A
		public void Flush(DateTime timestamp, QueueLogInfo info)
		{
			info.Count = this.Count;
			this.queueLog.Flush(timestamp, info);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004EB5 File Offset: 0x000030B5
		private DateTime GetCurrentTime()
		{
			if (this.timeProvider == null)
			{
				return DateTime.UtcNow;
			}
			return this.timeProvider();
		}

		// Token: 0x04000091 RID: 145
		private readonly ISchedulerQueue queueInstance;

		// Token: 0x04000092 RID: 146
		private readonly IMessageScope scope;

		// Token: 0x04000093 RID: 147
		private readonly Func<DateTime> timeProvider;

		// Token: 0x04000094 RID: 148
		private readonly DateTime createDateTime;

		// Token: 0x04000095 RID: 149
		private readonly ScopedQueueLog queueLog;
	}
}
