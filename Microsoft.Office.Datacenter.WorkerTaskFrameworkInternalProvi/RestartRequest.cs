using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200001A RID: 26
	public class RestartRequest
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000A1D2 File Offset: 0x000083D2
		private RestartRequest()
		{
			this.Timestamp = DateTime.UtcNow;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000A1E5 File Offset: 0x000083E5
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000A1ED File Offset: 0x000083ED
		public RestartRequestReason Reason { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000A1F6 File Offset: 0x000083F6
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000A1FE File Offset: 0x000083FE
		public Exception Exception { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000A207 File Offset: 0x00008407
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000A20F File Offset: 0x0000840F
		public string ResultName { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000A218 File Offset: 0x00008418
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000A220 File Offset: 0x00008420
		public int ResultId { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A229 File Offset: 0x00008429
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000A231 File Offset: 0x00008431
		public DateTime Timestamp { get; private set; }

		// Token: 0x0600029E RID: 670 RVA: 0x0000A23C File Offset: 0x0000843C
		public override string ToString()
		{
			string text = string.IsNullOrEmpty(this.ResultName) ? string.Empty : this.ResultName;
			if (this.Exception == null)
			{
				return string.Format("[RestartRequest at {0}]: ResultName={1}, ResultId={2}, RestartRequestReason={3}", new object[]
				{
					this.Timestamp,
					text,
					this.ResultId,
					this.Reason.ToString()
				});
			}
			return string.Format("[RestartRequest at {0}]: ResultName={1}, ResultId={2}, RestartRequestReason={3}, Exception = {4}", new object[]
			{
				this.Timestamp,
				text,
				this.ResultId,
				this.Reason.ToString(),
				this.Exception
			});
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A300 File Offset: 0x00008500
		internal static RestartRequest CreateDataAccessErrorRestartRequest(Exception exception)
		{
			return RestartRequest.CreateExceptionBasedRestartRequest(RestartRequestReason.DataAccessError, exception);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A309 File Offset: 0x00008509
		internal static RestartRequest CreateUnknownRestartRequest(Exception exception)
		{
			return RestartRequest.CreateExceptionBasedRestartRequest(RestartRequestReason.Unknown, exception);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A312 File Offset: 0x00008512
		internal static RestartRequest CreatePoisonResultRestartRequest(string resultName, int resultId)
		{
			return RestartRequest.CreateResultBasedRestartRequest(RestartRequestReason.PoisonResult, resultName, resultId);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A31C File Offset: 0x0000851C
		internal static RestartRequest CreateMaintenanceRestartRequest(string resultName, int resultId)
		{
			return RestartRequest.CreateResultBasedRestartRequest(RestartRequestReason.Maintenance, resultName, resultId);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A328 File Offset: 0x00008528
		internal static RestartRequest CreateSelfRecoveryBasedRestartRequest(string recoveryType, string metricName)
		{
			RestartRequest restartRequest = new RestartRequest();
			restartRequest.Reason = RestartRequestReason.SelfHealing;
			restartRequest.ResultName = string.Format("RecoveryType = {0}, MetricName = {1}", recoveryType, metricName);
			WTFDiagnostics.TraceError<DateTime, string, string>(WTFLog.WorkItem, TracingContext.Default, "[RestartRequest at {0}]: RestartRequestReason={1}, {2}", restartRequest.Timestamp, restartRequest.Reason.ToString(), restartRequest.ResultName, null, "CreateSelfRecoveryBasedRestartRequest", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\RestartRequest.cs", 137);
			return restartRequest;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A398 File Offset: 0x00008598
		private static RestartRequest CreateResultBasedRestartRequest(RestartRequestReason reason, string resultName, int resultId)
		{
			RestartRequest restartRequest = new RestartRequest();
			restartRequest.Reason = reason;
			restartRequest.ResultName = resultName;
			restartRequest.ResultId = resultId;
			WTFDiagnostics.TraceError<DateTime, string, int, string>(WTFLog.WorkItem, TracingContext.Default, "[RestartRequest at {0}]: ResultName={1}, ResultId={2}, RestartRequestReason={3}", restartRequest.Timestamp, resultName, resultId, reason.ToString(), null, "CreateResultBasedRestartRequest", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\RestartRequest.cs", 156);
			return restartRequest;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A3F8 File Offset: 0x000085F8
		private static RestartRequest CreateExceptionBasedRestartRequest(RestartRequestReason reason, Exception exception)
		{
			RestartRequest restartRequest = new RestartRequest();
			restartRequest.Reason = reason;
			restartRequest.Exception = exception;
			WTFDiagnostics.TraceError<DateTime, string, Exception>(WTFLog.WorkItem, TracingContext.Default, "[RestartRequest at {0}]: RestartRequestReason={1}, Exception={2}", restartRequest.Timestamp, reason.ToString(), exception, null, "CreateExceptionBasedRestartRequest", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\RestartRequest.cs", 173);
			return restartRequest;
		}
	}
}
