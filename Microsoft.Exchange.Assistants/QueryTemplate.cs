using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A2 RID: 162
	internal class QueryTemplate
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00019679 File Offset: 0x00017879
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00019681 File Offset: 0x00017881
		public SimpleProviderPropertyDefinition[] Parameters { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001968A File Offset: 0x0001788A
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00019692 File Offset: 0x00017892
		public Func<object[], QueryFilter, List<QueryableObject>> Executor { get; private set; }

		// Token: 0x060004DD RID: 1245 RVA: 0x0001969B File Offset: 0x0001789B
		public QueryTemplate(Func<object[], QueryFilter, List<QueryableObject>> executor, params SimpleProviderPropertyDefinition[] parameters)
		{
			this.Executor = executor;
			this.Parameters = parameters;
		}
	}
}
