using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001FB RID: 507
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoMetadata
	{
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0004DD65 File Offset: 0x0004BF65
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x0004DD6D File Offset: 0x0004BF6D
		public long Length { get; set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004DD76 File Offset: 0x0004BF76
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x0004DD7E File Offset: 0x0004BF7E
		public string ContentType { get; set; }
	}
}
