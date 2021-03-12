using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000063 RID: 99
	internal sealed class RegistryTimeZoneInformation
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000EA98 File Offset: 0x0000CC98
		public RegistryTimeZoneInformation(string keyName, string displayName, string standardName, string daylightName, string muiStandardName, REG_TIMEZONE_INFO regInfo)
		{
			this.KeyName = keyName;
			this.DisplayName = displayName;
			this.StandardName = standardName;
			this.DaylightName = daylightName;
			this.MuiStandardName = muiStandardName;
			this.RegInfo = regInfo;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000EB0F File Offset: 0x0000CD0F
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000EB2B File Offset: 0x0000CD2B
		public IList<RegistryTimeZoneRule> Rules
		{
			get
			{
				if (this.rules == null)
				{
					this.rules = new List<RegistryTimeZoneRule>(2);
				}
				return this.rules;
			}
			internal set
			{
				this.rules = value;
			}
		}

		// Token: 0x040001AF RID: 431
		public readonly string KeyName = string.Empty;

		// Token: 0x040001B0 RID: 432
		public readonly string DisplayName = string.Empty;

		// Token: 0x040001B1 RID: 433
		public readonly string StandardName = string.Empty;

		// Token: 0x040001B2 RID: 434
		public readonly string DaylightName = string.Empty;

		// Token: 0x040001B3 RID: 435
		public readonly string MuiStandardName = string.Empty;

		// Token: 0x040001B4 RID: 436
		public readonly REG_TIMEZONE_INFO RegInfo;

		// Token: 0x040001B5 RID: 437
		private IList<RegistryTimeZoneRule> rules;
	}
}
