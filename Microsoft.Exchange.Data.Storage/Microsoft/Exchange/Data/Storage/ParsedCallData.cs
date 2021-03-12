using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E2E RID: 3630
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ParsedCallData
	{
		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x06007D90 RID: 32144 RVA: 0x0022A17E File Offset: 0x0022837E
		// (set) Token: 0x06007D91 RID: 32145 RVA: 0x0022A186 File Offset: 0x00228386
		public SmtpAddress Mailbox { get; set; }

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x06007D92 RID: 32146 RVA: 0x0022A18F File Offset: 0x0022838F
		// (set) Token: 0x06007D93 RID: 32147 RVA: 0x0022A197 File Offset: 0x00228397
		public string DeviceId { get; set; }

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x06007D94 RID: 32148 RVA: 0x0022A1A0 File Offset: 0x002283A0
		// (set) Token: 0x06007D95 RID: 32149 RVA: 0x0022A1A8 File Offset: 0x002283A8
		public string DeviceType { get; set; }

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x06007D96 RID: 32150 RVA: 0x0022A1B1 File Offset: 0x002283B1
		// (set) Token: 0x06007D97 RID: 32151 RVA: 0x0022A1B9 File Offset: 0x002283B9
		public string SyncStateName { get; set; }

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x06007D98 RID: 32152 RVA: 0x0022A1C2 File Offset: 0x002283C2
		// (set) Token: 0x06007D99 RID: 32153 RVA: 0x0022A1CA File Offset: 0x002283CA
		public bool Metadata { get; set; }

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x06007D9A RID: 32154 RVA: 0x0022A1D3 File Offset: 0x002283D3
		// (set) Token: 0x06007D9B RID: 32155 RVA: 0x0022A1DB File Offset: 0x002283DB
		public bool FidMapping { get; set; }
	}
}
