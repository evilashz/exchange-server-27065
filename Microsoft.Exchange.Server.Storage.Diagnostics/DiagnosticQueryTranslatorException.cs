using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000030 RID: 48
	internal class DiagnosticQueryTranslatorException : DiagnosticQueryException
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000CA96 File Offset: 0x0000AC96
		public DiagnosticQueryTranslatorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		public DiagnosticQueryTranslatorException(string message) : base(message)
		{
		}
	}
}
