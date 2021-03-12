using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001AE RID: 430
	internal class ThrottlingPolicyAnonymousSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x0002D9E1 File Offset: 0x0002BBE1
		public ThrottlingPolicyAnonymousSettings()
		{
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0002D9E9 File Offset: 0x0002BBE9
		private ThrottlingPolicyAnonymousSettings(string value) : base(value)
		{
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0002D9F2 File Offset: 0x0002BBF2
		public static ThrottlingPolicyAnonymousSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyAnonymousSettings(stateToParse);
		}
	}
}
