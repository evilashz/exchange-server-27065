using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x02000080 RID: 128
	internal abstract class BaseRequest : IRequest
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x0000BD15 File Offset: 0x00009F15
		protected BaseRequest()
		{
			this.executionFinishedEvent = new ManualResetEvent(false);
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000BD29 File Offset: 0x00009F29
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0000BD31 File Offset: 0x00009F31
		public virtual Exception Exception { get; private set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000BD3A File Offset: 0x00009F3A
		public virtual bool IsBlocked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000BD3D File Offset: 0x00009F3D
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000BD45 File Offset: 0x00009F45
		public IRequestQueue Queue { get; private set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000BD4E File Offset: 0x00009F4E
		public virtual IEnumerable<ResourceKey> Resources
		{
			get
			{
				return Array<ResourceKey>.Empty;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000BD55 File Offset: 0x00009F55
		public void Abort()
		{
			this.Exception = new OperationAbortedException();
			this.executionFinishedEvent.Set();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000BD6E File Offset: 0x00009F6E
		public void AssignQueue(IRequestQueue queue)
		{
			this.executionFinishedEvent.Reset();
			this.Queue = queue;
			this.queuedTimestamp = TimeProvider.UtcNow;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000BD90 File Offset: 0x00009F90
		public virtual RequestDiagnosticData GetDiagnosticData(bool verbose)
		{
			RequestDiagnosticData requestDiagnosticData = this.CreateDiagnosticData();
			requestDiagnosticData.RequestKind = base.GetType().Name;
			if (this.Queue != null)
			{
				requestDiagnosticData.Queue = this.Queue.Id;
				requestDiagnosticData.QueuedTimestamp = new DateTime?(this.queuedTimestamp);
			}
			requestDiagnosticData.ExecutionStartedTimestamp = this.executionStartedTimestamp;
			requestDiagnosticData.ExecutionFinishedTimestamp = this.executionFinishedTimestamp;
			requestDiagnosticData.Exception = this.Exception;
			return requestDiagnosticData;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000BE04 File Offset: 0x0000A004
		public void Process()
		{
			this.executionStartedTimestamp = new DateTime?(DateTime.UtcNow);
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.ProcessRequest();
			}
			catch (LocalizedException ex)
			{
				ex.PreserveExceptionStack();
				this.Exception = ex;
			}
			finally
			{
				this.executionFinishedTimestamp = new DateTime?(DateTime.UtcNow);
				this.executionFinishedEvent.Set();
				stopwatch.Stop();
				this.GetDiagnosticData(false).ExecutionDuration = stopwatch.Elapsed;
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000BE94 File Offset: 0x0000A094
		public virtual bool ShouldCancel(TimeSpan queueTimeout)
		{
			DateTime utcNow = TimeProvider.UtcNow;
			return this.queuedTimestamp + queueTimeout <= utcNow;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000BEB9 File Offset: 0x0000A0B9
		public bool WaitExecution()
		{
			return this.WaitExecution(Timeout.InfiniteTimeSpan);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000BEC6 File Offset: 0x0000A0C6
		public bool WaitExecution(TimeSpan timeout)
		{
			return this.executionFinishedEvent.WaitOne(timeout);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public bool WaitExecutionAndThrowOnFailure(TimeSpan timeout)
		{
			if (!this.WaitExecution(timeout))
			{
				return false;
			}
			if (this.Exception == null)
			{
				return true;
			}
			throw this.Exception;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000BEF1 File Offset: 0x0000A0F1
		protected virtual RequestDiagnosticData CreateDiagnosticData()
		{
			return new RequestDiagnosticData();
		}

		// Token: 0x0600048F RID: 1167
		protected abstract void ProcessRequest();

		// Token: 0x04000164 RID: 356
		private readonly ManualResetEvent executionFinishedEvent;

		// Token: 0x04000165 RID: 357
		private DateTime? executionFinishedTimestamp;

		// Token: 0x04000166 RID: 358
		private DateTime? executionStartedTimestamp;

		// Token: 0x04000167 RID: 359
		private DateTime queuedTimestamp;
	}
}
