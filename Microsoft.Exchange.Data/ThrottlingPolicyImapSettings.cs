using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B4 RID: 436
	internal class ThrottlingPolicyImapSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x0002EDDA File Offset: 0x0002CFDA
		public ThrottlingPolicyImapSettings()
		{
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0002EDE2 File Offset: 0x0002CFE2
		private ThrottlingPolicyImapSettings(string value) : base(value)
		{
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0002EDEB File Offset: 0x0002CFEB
		public static ThrottlingPolicyImapSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyImapSettings(stateToParse);
		}
	}
}
