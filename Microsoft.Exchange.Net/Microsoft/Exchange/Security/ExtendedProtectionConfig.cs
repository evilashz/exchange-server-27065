using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C17 RID: 3095
	internal class ExtendedProtectionConfig
	{
		// Token: 0x060043BD RID: 17341 RVA: 0x000B6045 File Offset: 0x000B4245
		public ExtendedProtectionConfig(int policySetting, HashSet<string> acceptedServiceSpns, bool extendedProtectionTlsTerminatedAtProxyScenario)
		{
			this.policySetting = (ExtendedProtectionPolicySetting)policySetting;
			if (this.policySetting != ExtendedProtectionPolicySetting.None && extendedProtectionTlsTerminatedAtProxyScenario && (acceptedServiceSpns == null || acceptedServiceSpns.Count == 0))
			{
				throw new ArgumentException("acceptedServiceSpns must not be empty if extendedProtectionTlsTerminatedAtProxyScenario is set to true");
			}
			this.acceptedServiceSpns = acceptedServiceSpns;
			this.extendedProtectionTlsTerminatedAtProxyScenario = extendedProtectionTlsTerminatedAtProxyScenario;
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x000B6083 File Offset: 0x000B4283
		public ExtendedProtectionPolicySetting PolicySetting
		{
			get
			{
				return this.policySetting;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x000B608B File Offset: 0x000B428B
		public bool ExtendedProtectionTlsTerminatedAtProxyScenario
		{
			get
			{
				return this.extendedProtectionTlsTerminatedAtProxyScenario;
			}
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000B6093 File Offset: 0x000B4293
		public bool IsValidTargetName(string targetName)
		{
			return this.acceptedServiceSpns != null && this.acceptedServiceSpns.Contains(targetName);
		}

		// Token: 0x040039B5 RID: 14773
		private readonly ExtendedProtectionPolicySetting policySetting;

		// Token: 0x040039B6 RID: 14774
		private readonly bool extendedProtectionTlsTerminatedAtProxyScenario;

		// Token: 0x040039B7 RID: 14775
		private readonly HashSet<string> acceptedServiceSpns;

		// Token: 0x040039B8 RID: 14776
		public static readonly ExtendedProtectionConfig NoExtendedProtection = new ExtendedProtectionConfig(0, null, false);
	}
}
