using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000112 RID: 274
	internal abstract class BaseAsyncCommand
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x0002F044 File Offset: 0x0002D244
		public BaseAsyncCommand(string scenario)
		{
			this.scenario = scenario;
			this.ResetRequestID();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002F05C File Offset: 0x0002D25C
		protected void ExecuteWithExceptionHandler(GrayException.UserCodeDelegate callback)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(callback);
			}
			catch (GrayException exception)
			{
				this.InternalFailureCallback(exception, null);
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002F08C File Offset: 0x0002D28C
		protected virtual void InternalFailureCallback(Exception exception = null, string traceMessage = null)
		{
			if (exception == null && traceMessage == null)
			{
				throw new ArgumentNullException("exception", "exception or traceMessage must be non-null");
			}
			if (exception != null)
			{
				BaseAsyncCommand.Tracer.TraceError<Uri, Exception>(0L, "BaseAsyncOmexCommand.InternalFailureCallback: Request: {0} Exception: {1}", this.uri, exception);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_RequestFailed, this.periodicKey, new object[]
				{
					this.scenario,
					this.requestId,
					this.GetLoggedMailboxIdentifier(),
					this.uri,
					ExtensionDiagnostics.GetLoggedExceptionString(exception)
				});
			}
			else
			{
				BaseAsyncCommand.Tracer.TraceError<Uri, string>(0L, "BaseAsyncOmexCommand.InternalFailureCallback: Request: {0} Message: {1}", this.uri, traceMessage);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_RequestFailed, this.periodicKey, new object[]
				{
					this.scenario,
					this.requestId,
					this.GetLoggedMailboxIdentifier(),
					this.uri,
					traceMessage
				});
			}
			this.failureCallback(exception);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002F184 File Offset: 0x0002D384
		protected virtual void LogResponseParseFailureEvent(ExEventLog.EventTuple eventTuple, string periodicKey, object messageArg)
		{
			ExtensionDiagnostics.Logger.LogEvent(eventTuple, this.periodicKey, new object[]
			{
				this.scenario,
				this,
				this.requestId,
				this.GetLoggedMailboxIdentifier(),
				this.uri,
				messageArg
			});
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002F1D8 File Offset: 0x0002D3D8
		protected string GetLoggedMailboxIdentifier()
		{
			string result = null;
			if (this.getLoggedMailboxIdentifierCallback != null)
			{
				result = this.getLoggedMailboxIdentifierCallback();
			}
			return result;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002F1FC File Offset: 0x0002D3FC
		public void ResetRequestID()
		{
			this.requestId = Guid.NewGuid().ToString();
		}

		// Token: 0x040005D4 RID: 1492
		protected static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x040005D5 RID: 1493
		protected string periodicKey;

		// Token: 0x040005D6 RID: 1494
		protected Uri uri;

		// Token: 0x040005D7 RID: 1495
		protected string scenario;

		// Token: 0x040005D8 RID: 1496
		protected string requestId;

		// Token: 0x040005D9 RID: 1497
		protected BaseAsyncCommand.FailureCallback failureCallback;

		// Token: 0x040005DA RID: 1498
		protected BaseAsyncCommand.GetLoggedMailboxIdentifierCallback getLoggedMailboxIdentifierCallback;

		// Token: 0x02000113 RID: 275
		// (Invoke) Token: 0x06000B8A RID: 2954
		internal delegate void LogResponseParseFailureEventCallback(ExEventLog.EventTuple eventTuple, string periodicKey, object messageArg);

		// Token: 0x02000114 RID: 276
		// (Invoke) Token: 0x06000B8E RID: 2958
		internal delegate string GetLoggedMailboxIdentifierCallback();

		// Token: 0x02000115 RID: 277
		// (Invoke) Token: 0x06000B92 RID: 2962
		internal delegate void FailureCallback(Exception exception);
	}
}
