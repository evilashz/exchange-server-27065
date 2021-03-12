using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200006F RID: 111
	public class NullExecutionDiagnostics : IExecutionDiagnostics
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00010969 File Offset: 0x0000EB69
		public static NullExecutionDiagnostics Instance
		{
			get
			{
				return NullExecutionDiagnostics.instance;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00010970 File Offset: 0x0000EB70
		public string DiagnosticInformationForWatsonReport
		{
			get
			{
				return "NullExecutionDiagnosticsInformationForWatsonReport";
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00010977 File Offset: 0x0000EB77
		int IExecutionDiagnostics.MailboxNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001097A File Offset: 0x0000EB7A
		byte IExecutionDiagnostics.OperationId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001097D File Offset: 0x0000EB7D
		byte IExecutionDiagnostics.OperationType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00010980 File Offset: 0x0000EB80
		byte IExecutionDiagnostics.ClientType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00010983 File Offset: 0x0000EB83
		byte IExecutionDiagnostics.OperationFlags
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00010986 File Offset: 0x0000EB86
		int IExecutionDiagnostics.CorrelationId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00010989 File Offset: 0x0000EB89
		public void OnExceptionCatch(Exception exception)
		{
			this.OnExceptionCatch(exception, null);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00010993 File Offset: 0x0000EB93
		public void OnExceptionCatch(Exception exception, object diagnosticData)
		{
			ErrorHelper.OnExceptionCatch(0, 0, 0, 0, -1, exception, diagnosticData);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000109A1 File Offset: 0x0000EBA1
		public void OnUnhandledException(Exception exception)
		{
		}

		// Token: 0x04000604 RID: 1540
		private static readonly NullExecutionDiagnostics instance = new NullExecutionDiagnostics();
	}
}
