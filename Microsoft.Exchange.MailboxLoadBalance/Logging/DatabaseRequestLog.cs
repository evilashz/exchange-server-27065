using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging
{
	// Token: 0x020000A1 RID: 161
	internal class DatabaseRequestLog : ObjectLog<RequestDiagnosticData>
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x0000F1AE File Offset: 0x0000D3AE
		private DatabaseRequestLog() : base(new DatabaseRequestLog.RequestLogSchema(), new LoadBalanceLoggingConfig("Requests"))
		{
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000F1C5 File Offset: 0x0000D3C5
		public static void Write(IRequest request)
		{
			DatabaseRequestLog.Instance.LogObject(request.GetDiagnosticData(false));
		}

		// Token: 0x040001DA RID: 474
		private static readonly DatabaseRequestLog Instance = new DatabaseRequestLog();

		// Token: 0x020000A2 RID: 162
		private class RequestLogData : ConfigurableObject
		{
			// Token: 0x060005B9 RID: 1465 RVA: 0x0000F1E4 File Offset: 0x0000D3E4
			public RequestLogData(PropertyBag propertyBag) : base(propertyBag)
			{
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000F1ED File Offset: 0x0000D3ED
			internal override ObjectSchema ObjectSchema
			{
				get
				{
					return new DummyObjectSchema();
				}
			}
		}

		// Token: 0x020000A3 RID: 163
		private class RequestLogSchema : ConfigurableObjectLogSchema<DatabaseRequestLog.RequestLogData, DummyObjectSchema>
		{
			// Token: 0x170001EA RID: 490
			// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
			public override string LogType
			{
				get
				{
					return "Database Requests";
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000F1FB File Offset: 0x0000D3FB
			public override string Software
			{
				get
				{
					return "Mailbox Load Balancing";
				}
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0000F204 File Offset: 0x0000D404
			private static string ComputeFailureHash(Exception failureException)
			{
				string result;
				WatsonExceptionReport.TryStringHashFromStackTrace(failureException, false, out result);
				return result;
			}

			// Token: 0x040001DB RID: 475
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> BatchName = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("BatchName", (RequestDiagnosticData r) => r.BatchName ?? string.Empty);

			// Token: 0x040001DC RID: 476
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> Exception = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("Exception", delegate(RequestDiagnosticData r)
			{
				if (r.Exception != null)
				{
					return r.Exception.ToString();
				}
				return string.Empty;
			});

			// Token: 0x040001DD RID: 477
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> ExceptionType = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("ExceptionType", delegate(RequestDiagnosticData r)
			{
				if (r.Exception != null)
				{
					return r.Exception.GetType().Name;
				}
				return string.Empty;
			});

			// Token: 0x040001DE RID: 478
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> ExecutionDuration = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("ExecutionDurationMs", (RequestDiagnosticData r) => r.ExecutionDuration.TotalMilliseconds);

			// Token: 0x040001DF RID: 479
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> ExecutionFinishedTimestamp = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("FinishTS", (RequestDiagnosticData r) => r.ExecutionFinishedTimestamp);

			// Token: 0x040001E0 RID: 480
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> ExecutionStartTimestamp = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("StartTS", (RequestDiagnosticData r) => r.ExecutionStartedTimestamp);

			// Token: 0x040001E1 RID: 481
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> MailboxGuid = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("MailboxGuid", (RequestDiagnosticData r) => r.MovedMailboxGuid);

			// Token: 0x040001E2 RID: 482
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> QueueDuration = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("QueueDurationMs", (RequestDiagnosticData r) => r.QueueDuration.TotalMilliseconds);

			// Token: 0x040001E3 RID: 483
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> QueuedTimestamp = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("QueueTS", (RequestDiagnosticData r) => r.QueuedTimestamp);

			// Token: 0x040001E4 RID: 484
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> RealApplicationVersion = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("RealApplicationVersion", (RequestDiagnosticData r) => ExWatson.RealApplicationVersion.ToString());

			// Token: 0x040001E5 RID: 485
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> RequestQueue = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("Queue", (RequestDiagnosticData r) => r.Queue);

			// Token: 0x040001E6 RID: 486
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> RequestType = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("Type", (RequestDiagnosticData r) => r.RequestKind);

			// Token: 0x040001E7 RID: 487
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> Result = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("Result", (RequestDiagnosticData r) => r.Result);

			// Token: 0x040001E8 RID: 488
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> SourceDatabaseName = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("SourceDatabaseName", (RequestDiagnosticData r) => r.SourceDatabaseName);

			// Token: 0x040001E9 RID: 489
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> TargetDatabaseName = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("TargetDatabaseName", (RequestDiagnosticData r) => r.TargetDatabaseName);

			// Token: 0x040001EA RID: 490
			public static readonly ObjectLogSimplePropertyDefinition<RequestDiagnosticData> WatsonHash = new ObjectLogSimplePropertyDefinition<RequestDiagnosticData>("WatsonHash", delegate(RequestDiagnosticData r)
			{
				if (r.Exception != null)
				{
					return DatabaseRequestLog.RequestLogSchema.ComputeFailureHash(r.Exception);
				}
				return string.Empty;
			});
		}
	}
}
