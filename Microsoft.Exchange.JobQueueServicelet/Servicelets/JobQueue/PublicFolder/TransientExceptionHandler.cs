using System;
using System.Threading;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransientExceptionHandler
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00006E9C File Offset: 0x0000509C
		public TransientExceptionHandler(Trace tracer, int maximumNumberOfTransientExceptions, FilterDelegate transientExceptionFilter, Guid correlationId)
		{
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			ArgumentValidator.ThrowIfOutOfRange<int>("maximumNumberOfTransientExceptions", maximumNumberOfTransientExceptions, 1, 30);
			this.tracer = tracer;
			this.maximumNumberOfTransientExceptions = maximumNumberOfTransientExceptions;
			this.transientExceptionFilter = (transientExceptionFilter ?? new FilterDelegate(null, (UIntPtr)ldftn(IsTransientException)));
			this.correlationId = correlationId;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006EF5 File Offset: 0x000050F5
		public TransientExceptionHandler(Trace tracer, int maximumNumberOfTransientExceptions, FilterDelegate transientExceptionFilter, Guid correlationId, Action<Exception> recoveryAction) : this(tracer, maximumNumberOfTransientExceptions, transientExceptionFilter, correlationId)
		{
			this.recoveryAction = recoveryAction;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006F0A File Offset: 0x0000510A
		public TransientExceptionHandler(Trace tracer, int maximumNumberOfTransientExceptions, TimeSpan delayBetweenAttempts, FilterDelegate transientExceptionFilter, Guid correlationId, Action<Exception> recoveryAction, IBudget budget, string callerInfo) : this(tracer, maximumNumberOfTransientExceptions, transientExceptionFilter, correlationId, recoveryAction)
		{
			this.budget = budget;
			this.callerInfo = callerInfo;
			this.delayBetweenAttempts = delayBetweenAttempts;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00006F31 File Offset: 0x00005131
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00006F39 File Offset: 0x00005139
		public int TransientExceptionCount { get; private set; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00006F42 File Offset: 0x00005142
		public static bool IsTransientException(object e)
		{
			return TransientExceptionHandler.IsTransientException(e as Exception);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006F50 File Offset: 0x00005150
		public static bool IsTransientException(Exception exception)
		{
			return CommonUtils.ExceptionIsAny(exception, new WellKnownException[]
			{
				WellKnownException.Transient,
				WellKnownException.DataProviderTransient,
				WellKnownException.MRSTransient
			});
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006F7C File Offset: 0x0000517C
		public static bool IsConnectionFailure(Exception exception)
		{
			return exception is CommunicationWithRemoteServiceFailedTransientException || CommonUtils.ExceptionIsAny(exception, new WellKnownException[]
			{
				WellKnownException.MapiNetworkError,
				WellKnownException.MapiMailboxInTransit,
				WellKnownException.ConnectionFailedTransient
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006FB0 File Offset: 0x000051B0
		public void ExecuteWithRetry(TryDelegate task)
		{
			ArgumentValidator.ThrowIfNull("task", task);
			while (!this.TryExecute(task))
			{
				if (this.budget != null)
				{
					this.budget.EndLocal();
				}
				if (this.delayBetweenAttempts > TimeSpan.Zero)
				{
					this.tracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "TransientExceptionHandler.ExecuteWithRetry: Sleeping thread for {0}.", this.delayBetweenAttempts);
					Thread.Sleep(this.delayBetweenAttempts);
				}
				if (this.budget != null)
				{
					this.budget.StartLocal(this.callerInfo, default(TimeSpan));
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007174 File Offset: 0x00005374
		public bool TryExecute(TryDelegate task)
		{
			TransientExceptionHandler.<>c__DisplayClass1 CS$<>8__locals1 = new TransientExceptionHandler.<>c__DisplayClass1();
			CS$<>8__locals1.<>4__this = this;
			ArgumentValidator.ThrowIfNull("task", task);
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.needToRetry = false;
			ILUtil.DoTryFilterCatch(task, this.transientExceptionFilter, new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<TryExecute>b__0)));
			if (!CS$<>8__locals1.needToRetry)
			{
				this.TransientExceptionCount = 0;
				this.tracer.TraceDebug((long)this.GetHashCode(), "TransientExceptionHandler.TryExecute: Task completed successfully and the retry count has beeen restarted.");
			}
			else if (this.recoveryAction != null)
			{
				this.recoveryAction(CS$<>8__locals1.exception);
			}
			return !CS$<>8__locals1.needToRetry;
		}

		// Token: 0x040000A9 RID: 169
		private const int AbsoluteMaximumNumberOfRetries = 30;

		// Token: 0x040000AA RID: 170
		private readonly Trace tracer;

		// Token: 0x040000AB RID: 171
		private readonly int maximumNumberOfTransientExceptions;

		// Token: 0x040000AC RID: 172
		private readonly TimeSpan delayBetweenAttempts;

		// Token: 0x040000AD RID: 173
		private readonly FilterDelegate transientExceptionFilter;

		// Token: 0x040000AE RID: 174
		private readonly Action<Exception> recoveryAction;

		// Token: 0x040000AF RID: 175
		private readonly Guid correlationId;

		// Token: 0x040000B0 RID: 176
		private readonly IBudget budget;

		// Token: 0x040000B1 RID: 177
		private readonly string callerInfo;
	}
}
