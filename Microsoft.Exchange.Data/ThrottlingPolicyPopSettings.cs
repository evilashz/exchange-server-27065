using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B6 RID: 438
	internal class ThrottlingPolicyPopSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x0002EE94 File Offset: 0x0002D094
		public ThrottlingPolicyPopSettings()
		{
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002EE9C File Offset: 0x0002D09C
		private ThrottlingPolicyPopSettings(string value) : base(value)
		{
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002EEA5 File Offset: 0x0002D0A5
		public static ThrottlingPolicyPopSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyPopSettings(stateToParse);
		}
	}
}
