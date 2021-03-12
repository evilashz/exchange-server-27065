using System;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x0200000B RID: 11
	public class AutoDiscoverV2Response
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002C37 File Offset: 0x00000E37
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002C3F File Offset: 0x00000E3F
		public string RedirectUrl { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002C48 File Offset: 0x00000E48
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002C50 File Offset: 0x00000E50
		public string Url { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C59 File Offset: 0x00000E59
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002C61 File Offset: 0x00000E61
		public string ProtocolName { get; set; }
	}
}
