using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200002D RID: 45
	internal static class ADGlobalConfigSettings
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000FC44 File Offset: 0x0000DE44
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000FC4B File Offset: 0x0000DE4B
		internal static bool WriteOriginatingChangeTimestamp
		{
			get
			{
				return ADGlobalConfigSettings.writeOriginatingChangeTimestamp;
			}
			set
			{
				ADGlobalConfigSettings.writeOriginatingChangeTimestamp = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000FC53 File Offset: 0x0000DE53
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000FC5A File Offset: 0x0000DE5A
		internal static bool WriteShadowProperties
		{
			get
			{
				return ADGlobalConfigSettings.writeShadowProperties;
			}
			set
			{
				ADGlobalConfigSettings.writeShadowProperties = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000FC62 File Offset: 0x0000DE62
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000FC69 File Offset: 0x0000DE69
		internal static bool SoftLinkEnabled
		{
			get
			{
				return ADGlobalConfigSettings.softLinkEnabled;
			}
			set
			{
				ADGlobalConfigSettings.softLinkEnabled = value;
			}
		}

		// Token: 0x040000C4 RID: 196
		private static bool writeOriginatingChangeTimestamp = Datacenter.IsMicrosoftHostedOnly(true);

		// Token: 0x040000C5 RID: 197
		private static bool writeShadowProperties = Datacenter.IsMicrosoftHostedOnly(true);

		// Token: 0x040000C6 RID: 198
		private static bool softLinkEnabled = Datacenter.IsMicrosoftHostedOnly(true);
	}
}
