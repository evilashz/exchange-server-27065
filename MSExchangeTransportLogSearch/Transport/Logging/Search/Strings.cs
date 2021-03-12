using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000063 RID: 99
	public static class Strings
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000CF9C File Offset: 0x0000B19C
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", "ExAE709D", false, true, Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		public static LocalizedString SyncHealthLogInvalidData(bool timestampInvalid, bool eventIdInvalid, bool eventDataInvalid)
		{
			return new LocalizedString("SyncHealthLogInvalidData", "ExA85D76", false, true, Strings.ResourceManager, new object[]
			{
				timestampInvalid,
				eventIdInvalid,
				eventDataInvalid
			});
		}

		// Token: 0x04000185 RID: 389
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Logging.Search.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000064 RID: 100
		private enum ParamIDs
		{
			// Token: 0x04000187 RID: 391
			UsageText,
			// Token: 0x04000188 RID: 392
			SyncHealthLogInvalidData
		}
	}
}
