using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200003E RID: 62
	internal class GetSearchableMailboxesInputs
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000144EF File Offset: 0x000126EF
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x000144F7 File Offset: 0x000126F7
		public bool ExpandGroups { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00014500 File Offset: 0x00012700
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00014508 File Offset: 0x00012708
		public string Filter { get; set; }
	}
}
