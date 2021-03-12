using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x02000012 RID: 18
	internal static class Strings
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000050B0 File Offset: 0x000032B0
		public static LocalizedString InvalidPropertyOverrideValue(string propertyOverride)
		{
			return new LocalizedString("InvalidPropertyOverrideValue", Strings.ResourceManager, new object[]
			{
				propertyOverride
			});
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000050D8 File Offset: 0x000032D8
		public static LocalizedString InvalidMonitorIdentity(string monitorIdentity)
		{
			return new LocalizedString("InvalidMonitorIdentity", Strings.ResourceManager, new object[]
			{
				monitorIdentity
			});
		}

		// Token: 0x0400007C RID: 124
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000013 RID: 19
		private enum ParamIDs
		{
			// Token: 0x0400007E RID: 126
			InvalidPropertyOverrideValue,
			// Token: 0x0400007F RID: 127
			InvalidMonitorIdentity
		}
	}
}
