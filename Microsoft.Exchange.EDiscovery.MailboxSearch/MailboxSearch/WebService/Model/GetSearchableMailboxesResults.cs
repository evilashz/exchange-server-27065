using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200003F RID: 63
	internal class GetSearchableMailboxesResults
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x00014519 File Offset: 0x00012719
		public GetSearchableMailboxesResults()
		{
			this.Sources = new List<SearchSource>();
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0001452C File Offset: 0x0001272C
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00014534 File Offset: 0x00012734
		public List<SearchSource> Sources { get; set; }
	}
}
