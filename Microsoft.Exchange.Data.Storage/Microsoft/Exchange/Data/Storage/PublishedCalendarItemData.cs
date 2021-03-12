using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DAE RID: 3502
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct PublishedCalendarItemData
	{
		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x06007865 RID: 30821 RVA: 0x002139C0 File Offset: 0x00211BC0
		// (set) Token: 0x06007866 RID: 30822 RVA: 0x002139C8 File Offset: 0x00211BC8
		public string Subject { get; internal set; }

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x06007867 RID: 30823 RVA: 0x002139D1 File Offset: 0x00211BD1
		// (set) Token: 0x06007868 RID: 30824 RVA: 0x002139D9 File Offset: 0x00211BD9
		public string Location { get; internal set; }

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x06007869 RID: 30825 RVA: 0x002139E2 File Offset: 0x00211BE2
		// (set) Token: 0x0600786A RID: 30826 RVA: 0x002139EA File Offset: 0x00211BEA
		public string When { get; internal set; }

		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x0600786B RID: 30827 RVA: 0x002139F3 File Offset: 0x00211BF3
		// (set) Token: 0x0600786C RID: 30828 RVA: 0x002139FB File Offset: 0x00211BFB
		public string BodyText { get; internal set; }
	}
}
