using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000009 RID: 9
	internal abstract class NspiDispatchTask : WorkloadManagerDispatchTask
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000040D0 File Offset: 0x000022D0
		public NspiDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context) : base(asyncCallback, asyncState)
		{
			this.status = NspiStatus.GeneralFailure;
			this.protocolRequestInfo = protocolRequestInfo;
			this.context = context;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000040F4 File Offset: 0x000022F4
		public override IActivityScope GetActivityScope()
		{
			base.CheckDisposed();
			return this.context.ActivityScope;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004107 File Offset: 0x00002307
		public override IBudget Budget
		{
			get
			{
				base.CheckDisposed();
				return this.context.Budget;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000411A File Offset: 0x0000231A
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00004122 File Offset: 0x00002322
		public bool IsContextRundown { get; protected set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000412B File Offset: 0x0000232B
		public int ContextHandle
		{
			get
			{
				base.CheckDisposed();
				return this.context.ContextHandle;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000413E File Offset: 0x0000233E
		protected NspiStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004146 File Offset: 0x00002346
		protected NspiContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000414E File Offset: 0x0000234E
		protected ProtocolRequestInfo ProtocolRequestInfo
		{
			get
			{
				return this.protocolRequestInfo;
			}
		}

		// Token: 0x0600008F RID: 143
		protected abstract void InternalDebugTrace();

		// Token: 0x06000090 RID: 144 RVA: 0x00004158 File Offset: 0x00002358
		protected override void InternalPreExecute()
		{
			if (NspiDispatchTask.NspiTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.InternalDebugTrace();
			}
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.NspiRequests.Increment();
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.NspiRequestsRate.Increment();
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.NspiRequestsTotal.Increment();
			if (this.context != null)
			{
				if (this.context.TraceUser)
				{
					BaseTrace.CurrentThreadSettings.EnableTracing();
				}
				this.context.PurgeExpiredLogs();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000041D2 File Offset: 0x000023D2
		protected virtual void InternalTaskExecute()
		{
			this.status = NspiStatus.Success;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000041E3 File Offset: 0x000023E3
		protected override void InternalExecute()
		{
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				this.InternalTaskExecute();
			});
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000041F8 File Offset: 0x000023F8
		protected override void InternalPostExecute(TimeSpan queueAndDelayTime, TimeSpan totalTime, bool calledFromTimeout)
		{
			int num = (int)totalTime.TotalMilliseconds;
			int num2 = (int)queueAndDelayTime.TotalMilliseconds;
			AddressBookService.NspiRpcRequestsAverageLatency.AddSample((long)num);
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.NspiRequests.Decrement();
			if (this.status == NspiStatus.Success)
			{
				NspiDispatchTask.NspiTracer.TraceDebug<string, int, int>((long)this.ContextHandle, "{0} succeeded (queued: {1}ms elapsed: {2}ms)\n", this.TaskName, num2, num);
			}
			else
			{
				NspiDispatchTask.NspiTracer.TraceError((long)this.ContextHandle, "{0} failed: 0x{1:X} {1} (queued: {2}ms elapsed: {3}ms)\n", new object[]
				{
					this.TaskName,
					this.status,
					num2,
					num
				});
			}
			ProtocolLogSession protocolLogSession;
			if (this.context != null)
			{
				protocolLogSession = this.context.ProtocolLogSession;
			}
			else
			{
				protocolLogSession = ProtocolLog.CreateSession(this.ContextHandle, null, null, null);
				protocolLogSession[ProtocolLog.Field.Failures] = "NoContext";
			}
			if (calledFromTimeout)
			{
				protocolLogSession[ProtocolLog.Field.Failures] = "Throttled";
			}
			protocolLogSession.Append(this.TaskName, this.status, num2, num);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000042F8 File Offset: 0x000024F8
		protected override bool TryHandleException(Exception exception)
		{
			this.status = NspiStatus.GeneralFailure;
			bool flag = true;
			if (exception is FailRpcException)
			{
				NspiDispatchTask.NspiTracer.TraceError<string>((long)this.ContextHandle, "{0}", exception.Message);
				this.status = (NspiStatus)((RpcException)exception).ErrorCode;
			}
			else if (exception is RpcException)
			{
				NspiDispatchTask.NspiTracer.TraceError<string>((long)this.ContextHandle, "{0}", exception.Message);
			}
			else if (exception is ADTransientException)
			{
				NspiDispatchTask.NspiTracer.TraceError<Exception>((long)this.ContextHandle, "{0}", exception);
			}
			else if (exception is ADOperationException)
			{
				NspiDispatchTask.NspiTracer.TraceError<Exception>((long)this.ContextHandle, "{0}", exception);
			}
			else if (exception is DataValidationException)
			{
				NspiDispatchTask.NspiTracer.TraceError<Exception>((long)this.ContextHandle, "{0}", exception);
			}
			else if (exception is StorageTransientException)
			{
				NspiDispatchTask.NspiTracer.TraceError<Exception>((long)this.ContextHandle, "{0}", exception);
			}
			else if (exception is StoragePermanentException)
			{
				NspiDispatchTask.NspiTracer.TraceError<Exception>((long)this.ContextHandle, "{0}", exception);
			}
			else if (exception is NspiException)
			{
				NspiDispatchTask.NspiTracer.TraceError<string>((long)this.ContextHandle, "{0}", exception.Message);
				this.status = ((NspiException)exception).Status;
			}
			else
			{
				NspiDispatchTask.NspiTracer.TraceError<Exception>((long)this.ContextHandle, "{0}", exception);
				flag = false;
			}
			this.context.ProtocolLogSession[ProtocolLog.Field.Failures] = exception.LogMessage(!flag);
			return flag;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004490 File Offset: 0x00002690
		protected void NspiContextCallWrapper(string contextCallName, Func<NspiStatus> contextCall)
		{
			this.status = contextCall();
			if (this.status != NspiStatus.Success)
			{
				NspiDispatchTask.NspiTracer.TraceError<string, NspiStatus>((long)this.ContextHandle, "NspiContext.{0} failed; status={1}", contextCallName, this.status);
			}
		}

		// Token: 0x04000035 RID: 53
		protected static readonly Trace NspiTracer = ExTraceGlobals.NspiTracer;

		// Token: 0x04000036 RID: 54
		private readonly ProtocolRequestInfo protocolRequestInfo;

		// Token: 0x04000037 RID: 55
		private readonly NspiContext context;

		// Token: 0x04000038 RID: 56
		private NspiStatus status;
	}
}
