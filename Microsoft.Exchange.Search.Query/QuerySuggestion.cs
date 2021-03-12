using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class QuerySuggestion
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00006EE8 File Offset: 0x000050E8
		internal QuerySuggestion(string suggestedQuery, double weight, QuerySuggestionSources source)
		{
			this.SuggestedQuery = suggestedQuery;
			this.Weight = weight;
			this.Source = source;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006F05 File Offset: 0x00005105
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00006F0D File Offset: 0x0000510D
		public string SuggestedQuery { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006F16 File Offset: 0x00005116
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00006F1E File Offset: 0x0000511E
		public double Weight { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006F27 File Offset: 0x00005127
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00006F2F File Offset: 0x0000512F
		public QuerySuggestionSources Source { get; private set; }
	}
}
