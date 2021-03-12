using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000005 RID: 5
	public class ExceptionDetails
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002582 File Offset: 0x00000782
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000258A File Offset: 0x0000078A
		public DateTime ExceptionTimeUtc { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002593 File Offset: 0x00000793
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000259B File Offset: 0x0000079B
		public string ExceptionType { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000025AC File Offset: 0x000007AC
		public string Details { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025B5 File Offset: 0x000007B5
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000025BD File Offset: 0x000007BD
		public string DiagnosticContext { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x000025C8 File Offset: 0x000007C8
		internal static ExceptionDetails FromException(Exception exception)
		{
			if (exception != null)
			{
				return new ExceptionDetails
				{
					ExceptionTimeUtc = DateTime.UtcNow,
					ExceptionType = exception.GetType().ToString(),
					Details = exception.ToString(),
					DiagnosticContext = (Microsoft.Exchange.Diagnostics.DiagnosticContext.HasData ? AuditingOpticsLogger.GetDiagnosticContextFromThread() : null)
				};
			}
			return null;
		}
	}
}
