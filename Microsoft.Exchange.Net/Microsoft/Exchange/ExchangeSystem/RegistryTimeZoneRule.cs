using System;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000064 RID: 100
	internal sealed class RegistryTimeZoneRule
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000EB34 File Offset: 0x0000CD34
		public RegistryTimeZoneRule(NativeMethods.SystemTime start, REG_TIMEZONE_INFO regTimeZoneInfo)
		{
			this.Start = new DateTime((int)start.Year, (int)start.Month, (int)start.Day, (int)start.Hour, (int)start.Minute, (int)start.Second, (int)start.Milliseconds);
			this.RegTimeZoneInfo = regTimeZoneInfo;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000EB8A File Offset: 0x0000CD8A
		public RegistryTimeZoneRule(DateTime start, REG_TIMEZONE_INFO regTimeZoneInfo)
		{
			this.Start = start;
			this.RegTimeZoneInfo = regTimeZoneInfo;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public RegistryTimeZoneRule(int year, REG_TIMEZONE_INFO regTimeZoneInfo) : this(new DateTime(year, 1, 1), regTimeZoneInfo)
		{
		}

		// Token: 0x040001B6 RID: 438
		public readonly DateTime Start;

		// Token: 0x040001B7 RID: 439
		public readonly REG_TIMEZONE_INFO RegTimeZoneInfo;
	}
}
