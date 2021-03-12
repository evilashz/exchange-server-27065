using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001E1 RID: 481
	internal static class SystemProbeConstants
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00038E62 File Offset: 0x00037062
		public static Guid TenantID
		{
			get
			{
				return new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
			}
		}

		// Token: 0x040009FA RID: 2554
		public const string SysProbeRecipientDomain = "contoso.com";

		// Token: 0x040009FB RID: 2555
		public const string SysProbeRecipientPrefix = "sysprb";
	}
}
