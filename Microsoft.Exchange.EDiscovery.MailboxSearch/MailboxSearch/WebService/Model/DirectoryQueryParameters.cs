using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200004A RID: 74
	internal class DirectoryQueryParameters
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00015B84 File Offset: 0x00013D84
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00015B8C File Offset: 0x00013D8C
		public QueryFilter Query { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00015B95 File Offset: 0x00013D95
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00015B9D File Offset: 0x00013D9D
		public PropertyDefinition[] Properties { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00015BA6 File Offset: 0x00013DA6
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00015BAE File Offset: 0x00013DAE
		public List<SearchSource> Sources { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00015BB7 File Offset: 0x00013DB7
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00015BBF File Offset: 0x00013DBF
		public int PageSize { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00015BC8 File Offset: 0x00013DC8
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00015BD0 File Offset: 0x00013DD0
		public bool MatchRecipientsToSources { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00015BD9 File Offset: 0x00013DD9
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00015BE1 File Offset: 0x00013DE1
		public bool ExpandGroups { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00015BEA File Offset: 0x00013DEA
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00015BF2 File Offset: 0x00013DF2
		public bool RequestGroups { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00015BFB File Offset: 0x00013DFB
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00015C03 File Offset: 0x00013E03
		public bool ExpandPublicFolders { get; set; }
	}
}
