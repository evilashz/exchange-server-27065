using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200003E RID: 62
	public interface IExecutionDiagnostics
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600046E RID: 1134
		int MailboxNumber { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600046F RID: 1135
		byte OperationId { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000470 RID: 1136
		byte OperationType { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000471 RID: 1137
		byte ClientType { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000472 RID: 1138
		byte OperationFlags { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000473 RID: 1139
		int CorrelationId { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000474 RID: 1140
		string DiagnosticInformationForWatsonReport { get; }

		// Token: 0x06000475 RID: 1141
		void OnExceptionCatch(Exception exception);

		// Token: 0x06000476 RID: 1142
		void OnExceptionCatch(Exception exception, object diagnosticData);

		// Token: 0x06000477 RID: 1143
		void OnUnhandledException(Exception exception);
	}
}
