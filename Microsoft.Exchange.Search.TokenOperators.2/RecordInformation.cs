using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200001D RID: 29
	internal struct RecordInformation
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005A35 File Offset: 0x00003C35
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00005A3D File Offset: 0x00003C3D
		internal Guid CorrelationId { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005A46 File Offset: 0x00003C46
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00005A4E File Offset: 0x00003C4E
		internal long DocumentId { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005A57 File Offset: 0x00003C57
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00005A5F File Offset: 0x00003C5F
		internal Guid TenantId { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005A68 File Offset: 0x00003C68
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005A70 File Offset: 0x00003C70
		internal IIdentity CompositeItemId { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005A79 File Offset: 0x00003C79
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005A81 File Offset: 0x00003C81
		internal int ErrorCode { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005A8A File Offset: 0x00003C8A
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005A92 File Offset: 0x00003C92
		internal int AttemptCount { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005A9B File Offset: 0x00003C9B
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005AA3 File Offset: 0x00003CA3
		internal string IndexSystemName { get; set; }
	}
}
