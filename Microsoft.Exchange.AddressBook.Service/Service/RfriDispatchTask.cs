using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000020 RID: 32
	internal abstract class RfriDispatchTask : WorkloadManagerDispatchTask
	{
		// Token: 0x0600011F RID: 287 RVA: 0x0000616D File Offset: 0x0000436D
		public RfriDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriContext context) : base(asyncCallback, asyncState)
		{
			this.status = RfriStatus.GeneralFailure;
			this.protocolRequestInfo = protocolRequestInfo;
			this.clientBinding = clientBinding;
			this.context = context;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006199 File Offset: 0x00004399
		public override IBudget Budget
		{
			get
			{
				base.CheckDisposed();
				return this.context.Budget;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000061AC File Offset: 0x000043AC
		public int ContextHandle
		{
			get
			{
				base.CheckDisposed();
				return this.context.ContextHandle;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000061BF File Offset: 0x000043BF
		protected RfriStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000061C7 File Offset: 0x000043C7
		protected RfriContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000061CF File Offset: 0x000043CF
		protected ProtocolRequestInfo ProtocolRequestInfo
		{
			get
			{
				return this.protocolRequestInfo;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000061D7 File Offset: 0x000043D7
		protected ClientBinding ClientBinding
		{
			get
			{
				return this.clientBinding;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000061DF File Offset: 0x000043DF
		public override IActivityScope GetActivityScope()
		{
			base.CheckDisposed();
			return this.context.ActivityScope;
		}

		// Token: 0x06000127 RID: 295
		protected abstract void InternalDebugTrace();

		// Token: 0x06000128 RID: 296 RVA: 0x000061F4 File Offset: 0x000043F4
		protected override void InternalPreExecute()
		{
			if (RfriDispatchTask.ReferralTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.InternalDebugTrace();
			}
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.RfrRequests.Increment();
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.RfrRequestsTotal.Increment();
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.RfrRequestsRate.Increment();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006244 File Offset: 0x00004444
		protected virtual void InternalTaskExecute()
		{
			this.status = RfriStatus.Success;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000624D File Offset: 0x0000444D
		protected override void InternalExecute()
		{
			this.status = this.context.Initialize();
			if (this.status != RfriStatus.Success)
			{
				return;
			}
			this.InternalTaskExecute();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006270 File Offset: 0x00004470
		protected override void InternalPostExecute(TimeSpan queueAndDelayTime, TimeSpan totalTime, bool calledFromTimeout)
		{
			int num = (int)totalTime.TotalMilliseconds;
			int num2 = (int)queueAndDelayTime.TotalMilliseconds;
			AddressBookService.RfrRpcRequestsAverageLatency.AddSample((long)num);
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.RfrRequests.Decrement();
			if (this.status == RfriStatus.Success)
			{
				RfriDispatchTask.ReferralTracer.TraceDebug<string, int, int>((long)this.ContextHandle, "{0} succeeded (queued: {1}ms elapsed: {2}ms)\n", this.TaskName, num2, num);
			}
			else
			{
				RfriDispatchTask.ReferralTracer.TraceError((long)this.ContextHandle, "{0} failed: 0x{1:X} {1} (queued: {2}ms elapsed: {3}ms)\n", new object[]
				{
					this.TaskName,
					this.status,
					num2,
					num
				});
			}
			if (calledFromTimeout)
			{
				this.context.ProtocolLogSession[ProtocolLog.Field.Failures] = "Throttled";
			}
			this.context.ProtocolLogSession.Append(this.TaskName, this.status, num2, num);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006354 File Offset: 0x00004554
		protected override bool TryHandleException(Exception exception)
		{
			this.status = RfriStatus.GeneralFailure;
			bool flag = true;
			if (exception is FailRpcException)
			{
				RfriDispatchTask.ReferralTracer.TraceError<string>((long)this.ContextHandle, "{0}", exception.Message);
				this.status = (RfriStatus)((RpcException)exception).ErrorCode;
			}
			else if (exception is ADTransientException || exception is StorageTransientException || exception is ConnectionFailedPermanentException)
			{
				RfriDispatchTask.ReferralTracer.TraceError<string>((long)this.ContextHandle, "{0}", exception.Message);
			}
			else if (exception is RfriException)
			{
				RfriDispatchTask.ReferralTracer.TraceError<string>((long)this.ContextHandle, "{0}", exception.Message);
				this.status = ((RfriException)exception).Status;
			}
			else
			{
				RfriDispatchTask.ReferralTracer.TraceError<Exception>((long)this.ContextHandle, "{0}", exception);
				flag = false;
			}
			this.context.ProtocolLogSession[ProtocolLog.Field.Failures] = exception.LogMessage(!flag);
			return flag;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000644B File Offset: 0x0000464B
		protected void RfriContextCallWrapper(string contextCallName, Func<RfriStatus> contextCall)
		{
			this.status = contextCall();
			if (this.status != RfriStatus.Success)
			{
				RfriDispatchTask.ReferralTracer.TraceError<string, RfriStatus>((long)this.ContextHandle, "RfriContext.{0} failed; status={1}", contextCallName, this.status);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000647E File Offset: 0x0000467E
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.context);
			base.InternalDispose();
		}

		// Token: 0x040000AC RID: 172
		protected static readonly Trace ReferralTracer = ExTraceGlobals.ReferralTracer;

		// Token: 0x040000AD RID: 173
		private readonly ProtocolRequestInfo protocolRequestInfo;

		// Token: 0x040000AE RID: 174
		private readonly ClientBinding clientBinding;

		// Token: 0x040000AF RID: 175
		private readonly RfriContext context;

		// Token: 0x040000B0 RID: 176
		private RfriStatus status;
	}
}
