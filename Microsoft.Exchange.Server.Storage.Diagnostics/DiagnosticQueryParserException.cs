using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000020 RID: 32
	internal class DiagnosticQueryParserException : DiagnosticQueryException
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x000095D5 File Offset: 0x000077D5
		public DiagnosticQueryParserException(string message) : base(message)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000095DE File Offset: 0x000077DE
		public DiagnosticQueryParserException(string message, string query) : base(message)
		{
			this.query = query;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000095EE File Offset: 0x000077EE
		public DiagnosticQueryParserException(string message, string query, Exception innerException) : base(message, innerException)
		{
			this.query = query;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000095FF File Offset: 0x000077FF
		public string Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x040000D4 RID: 212
		private string query;
	}
}
