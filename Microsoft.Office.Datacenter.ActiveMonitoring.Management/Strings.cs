using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management
{
	// Token: 0x02000009 RID: 9
	internal static class Strings
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002A74 File Offset: 0x00000C74
		public static LocalizedString InvalidPropertyOverrideValue(string propertyOverride)
		{
			return new LocalizedString("InvalidPropertyOverrideValue", Strings.ResourceManager, new object[]
			{
				propertyOverride
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A9C File Offset: 0x00000C9C
		public static LocalizedString InvalidMonitorIdentity(string monitorIdentity)
		{
			return new LocalizedString("InvalidMonitorIdentity", Strings.ResourceManager, new object[]
			{
				monitorIdentity
			});
		}

		// Token: 0x04000017 RID: 23
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Office.Datacenter.ActiveMonitoring.Management.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200000A RID: 10
		private enum ParamIDs
		{
			// Token: 0x04000019 RID: 25
			InvalidPropertyOverrideValue,
			// Token: 0x0400001A RID: 26
			InvalidMonitorIdentity
		}
	}
}
