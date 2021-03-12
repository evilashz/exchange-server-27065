using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000509 RID: 1289
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OscNetworkProperties
	{
		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000E0D9A File Offset: 0x000DEF9A
		// (set) Token: 0x060037BB RID: 14267 RVA: 0x000E0DA2 File Offset: 0x000DEFA2
		public string NetworkId { get; internal set; }

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000E0DAB File Offset: 0x000DEFAB
		// (set) Token: 0x060037BD RID: 14269 RVA: 0x000E0DB3 File Offset: 0x000DEFB3
		public string NetworkUserId { get; internal set; }
	}
}
