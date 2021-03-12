using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000054 RID: 84
	internal class StoreQueryFactory : DiagnosticQueryFactory
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00010A1C File Offset: 0x0000EC1C
		private static DiagnosticQueryFactory Instance
		{
			get
			{
				if (StoreQueryFactory.instance == null)
				{
					StoreQueryFactory.instance = new StoreQueryFactory();
				}
				return StoreQueryFactory.instance;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010A34 File Offset: 0x0000EC34
		public new static DiagnosticQueryFactory CreateFactory()
		{
			return StoreQueryFactory.Instance;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00010A3B File Offset: 0x0000EC3B
		public override DiagnosticQueryRetriever CreateRetriever(DiagnosticQueryParser parser, DiagnosableParameters parameters)
		{
			return StoreQueryRetriever.Create(parser, parameters);
		}

		// Token: 0x04000193 RID: 403
		private static DiagnosticQueryFactory instance;
	}
}
