using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200008C RID: 140
	public sealed class TaskExecutionDiagnosticsProxy : IExecutionDiagnostics
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001471F File Offset: 0x0001291F
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x00014727 File Offset: 0x00012927
		public IExecutionDiagnostics ExecutionDiagnostics
		{
			get
			{
				return this.executionDiagnostics;
			}
			set
			{
				this.executionDiagnostics = value;
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00014730 File Offset: 0x00012930
		internal static IDisposable SetUnhandledExceptionTestHook(Action action)
		{
			return TaskExecutionDiagnosticsProxy.unhandledExceptionTestHook.SetTestHook(action);
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001473D File Offset: 0x0001293D
		public string DiagnosticInformationForWatsonReport
		{
			get
			{
				if (this.ExecutionDiagnostics == null)
				{
					return string.Empty;
				}
				return this.ExecutionDiagnostics.DiagnosticInformationForWatsonReport;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00014758 File Offset: 0x00012958
		int IExecutionDiagnostics.MailboxNumber
		{
			get
			{
				if (this.ExecutionDiagnostics == null)
				{
					return 0;
				}
				return this.ExecutionDiagnostics.MailboxNumber;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001476F File Offset: 0x0001296F
		byte IExecutionDiagnostics.OperationId
		{
			get
			{
				if (this.ExecutionDiagnostics == null)
				{
					return 0;
				}
				return this.ExecutionDiagnostics.OperationId;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00014786 File Offset: 0x00012986
		byte IExecutionDiagnostics.OperationType
		{
			get
			{
				if (this.ExecutionDiagnostics == null)
				{
					return 0;
				}
				return this.ExecutionDiagnostics.OperationType;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001479D File Offset: 0x0001299D
		byte IExecutionDiagnostics.ClientType
		{
			get
			{
				if (this.ExecutionDiagnostics == null)
				{
					return 0;
				}
				return this.ExecutionDiagnostics.ClientType;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x000147B4 File Offset: 0x000129B4
		byte IExecutionDiagnostics.OperationFlags
		{
			get
			{
				if (this.ExecutionDiagnostics == null)
				{
					return 0;
				}
				return this.ExecutionDiagnostics.OperationFlags;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000147CB File Offset: 0x000129CB
		int IExecutionDiagnostics.CorrelationId
		{
			get
			{
				if (this.ExecutionDiagnostics == null)
				{
					return 0;
				}
				return this.ExecutionDiagnostics.CorrelationId;
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000147E2 File Offset: 0x000129E2
		public void OnExceptionCatch(Exception exception)
		{
			this.OnExceptionCatch(exception, null);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000147EC File Offset: 0x000129EC
		public void OnExceptionCatch(Exception exception, object diagnosticData)
		{
			if (this.ExecutionDiagnostics != null)
			{
				this.ExecutionDiagnostics.OnExceptionCatch(exception, diagnosticData);
				return;
			}
			ErrorHelper.OnExceptionCatch(0, 0, 0, 0, -1, exception, diagnosticData);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00014810 File Offset: 0x00012A10
		public void OnUnhandledException(Exception exception)
		{
			FaultInjection.InjectFault(TaskExecutionDiagnosticsProxy.unhandledExceptionTestHook);
			if (this.ExecutionDiagnostics != null)
			{
				this.ExecutionDiagnostics.OnUnhandledException(exception);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00014830 File Offset: 0x00012A30
		public TaskExecutionDiagnosticsProxy.TaskExecutionDiagnosticsScope NewDiagnosticsScope()
		{
			return new TaskExecutionDiagnosticsProxy.TaskExecutionDiagnosticsScope(this);
		}

		// Token: 0x0400068E RID: 1678
		private static Hookable<Action> unhandledExceptionTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x0400068F RID: 1679
		private IExecutionDiagnostics executionDiagnostics;

		// Token: 0x0200008D RID: 141
		public struct TaskExecutionDiagnosticsScope : IDisposable
		{
			// Token: 0x0600076D RID: 1901 RVA: 0x0001484E File Offset: 0x00012A4E
			internal TaskExecutionDiagnosticsScope(TaskExecutionDiagnosticsProxy proxyExecutionContext)
			{
				this.proxyContext = proxyExecutionContext;
				this.savedExecutionDiagnostics = proxyExecutionContext.ExecutionDiagnostics;
			}

			// Token: 0x0600076E RID: 1902 RVA: 0x00014863 File Offset: 0x00012A63
			public void Dispose()
			{
				if (this.proxyContext != null)
				{
					this.proxyContext.ExecutionDiagnostics = this.savedExecutionDiagnostics;
				}
			}

			// Token: 0x04000690 RID: 1680
			private TaskExecutionDiagnosticsProxy proxyContext;

			// Token: 0x04000691 RID: 1681
			private IExecutionDiagnostics savedExecutionDiagnostics;
		}
	}
}
