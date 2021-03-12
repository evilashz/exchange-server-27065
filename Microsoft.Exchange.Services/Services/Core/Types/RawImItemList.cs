using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000619 RID: 1561
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RawImItemList
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000B6CF8 File Offset: 0x000B4EF8
		// (set) Token: 0x0600310A RID: 12554 RVA: 0x000B6D00 File Offset: 0x000B4F00
		public RawImGroup[] Groups { get; set; }

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000B6D09 File Offset: 0x000B4F09
		// (set) Token: 0x0600310C RID: 12556 RVA: 0x000B6D11 File Offset: 0x000B4F11
		public PersonId[] Personas { get; set; }
	}
}
