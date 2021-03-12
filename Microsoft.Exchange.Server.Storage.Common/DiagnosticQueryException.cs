using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200002F RID: 47
	public class DiagnosticQueryException : Exception
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x0000B55E File Offset: 0x0000975E
		public DiagnosticQueryException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000B568 File Offset: 0x00009768
		public DiagnosticQueryException(string message) : base(message)
		{
		}
	}
}
