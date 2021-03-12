using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200016A RID: 362
	internal class LegacyThrottlingPolicySettings
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x0002557A File Offset: 0x0002377A
		private LegacyThrottlingPolicySettings(string value)
		{
			this.toString = value;
			this.settings = new Dictionary<string, string>();
			ThrottlingPolicyBaseSettings.InternalParse(value, this.settings);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000255A0 File Offset: 0x000237A0
		internal bool TryGetValue(string key, out string value)
		{
			value = null;
			if (!this.settings.ContainsKey(key))
			{
				return false;
			}
			value = this.settings[key];
			return true;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000255C4 File Offset: 0x000237C4
		public override string ToString()
		{
			return this.toString;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000255CC File Offset: 0x000237CC
		public static LegacyThrottlingPolicySettings Parse(string stateToParse)
		{
			return new LegacyThrottlingPolicySettings(stateToParse);
		}

		// Token: 0x04000743 RID: 1859
		private Dictionary<string, string> settings;

		// Token: 0x04000744 RID: 1860
		private readonly string toString;
	}
}
