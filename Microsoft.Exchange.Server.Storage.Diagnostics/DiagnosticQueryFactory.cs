using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000013 RID: 19
	internal class DiagnosticQueryFactory
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00005009 File Offset: 0x00003209
		private static DiagnosticQueryFactory Instance
		{
			get
			{
				if (DiagnosticQueryFactory.instance == null)
				{
					DiagnosticQueryFactory.instance = new DiagnosticQueryFactory();
				}
				return DiagnosticQueryFactory.instance;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005021 File Offset: 0x00003221
		public static DiagnosticQueryFactory CreateFactory()
		{
			return DiagnosticQueryFactory.Instance;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005028 File Offset: 0x00003228
		public DiagnosticQueryParser CreateParser(string query)
		{
			return DiagnosticQueryParser.Create(query);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005030 File Offset: 0x00003230
		public virtual DiagnosticQueryRetriever CreateRetriever(DiagnosticQueryParser parser, DiagnosableParameters parameters)
		{
			return DiagnosticQueryRetriever.Create(parser, parameters);
		}

		// Token: 0x04000089 RID: 137
		private static DiagnosticQueryFactory instance;
	}
}
