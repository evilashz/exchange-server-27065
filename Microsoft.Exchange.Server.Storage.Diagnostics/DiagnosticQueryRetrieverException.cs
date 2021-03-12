using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000023 RID: 35
	internal class DiagnosticQueryRetrieverException : DiagnosticQueryException
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000096D9 File Offset: 0x000078D9
		public DiagnosticQueryRetrieverException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000096E3 File Offset: 0x000078E3
		public DiagnosticQueryRetrieverException(string message) : base(message)
		{
		}
	}
}
