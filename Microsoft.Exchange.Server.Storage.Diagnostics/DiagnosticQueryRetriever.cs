using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000022 RID: 34
	public class DiagnosticQueryRetriever
	{
		// Token: 0x060000FC RID: 252 RVA: 0x0000967B File Offset: 0x0000787B
		protected DiagnosticQueryRetriever(DiagnosticQueryResults results)
		{
			this.results = (results ?? DiagnosticQueryRetriever.EmptyResults);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00009693 File Offset: 0x00007893
		public DiagnosticQueryResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000969B File Offset: 0x0000789B
		private static DiagnosticQueryResults EmptyResults
		{
			get
			{
				if (DiagnosticQueryRetriever.emptyResults == null)
				{
					DiagnosticQueryRetriever.emptyResults = DiagnosticQueryResults.Create(new string[0], new Type[0], new uint[0], new object[0][], false, false);
				}
				return DiagnosticQueryRetriever.emptyResults;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000096CD File Offset: 0x000078CD
		public static DiagnosticQueryRetriever Create(DiagnosticQueryParser parser, DiagnosableParameters parameters)
		{
			return new DiagnosticQueryRetriever(DiagnosticQueryRetriever.EmptyResults);
		}

		// Token: 0x040000DB RID: 219
		private static DiagnosticQueryResults emptyResults;

		// Token: 0x040000DC RID: 220
		private DiagnosticQueryResults results;
	}
}
