using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.QuickLogStrings
{
	// Token: 0x020001A7 RID: 423
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Strings
	{
		// Token: 0x06001797 RID: 6039 RVA: 0x00070EE4 File Offset: 0x0006F0E4
		public static LocalizedString FailedToGetItem(string messageClass, string folder)
		{
			return new LocalizedString("FailedToGetItem", "", false, false, Strings.ResourceManager, new object[]
			{
				messageClass,
				folder
			});
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00070F18 File Offset: 0x0006F118
		public static LocalizedString descMissingProperty(string propertyName, string unexpectedObject)
		{
			return new LocalizedString("descMissingProperty", "Ex28B9D6", false, true, Strings.ResourceManager, new object[]
			{
				propertyName,
				unexpectedObject
			});
		}

		// Token: 0x04000B22 RID: 2850
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.QuickLog.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x020001A8 RID: 424
		private enum ParamIDs
		{
			// Token: 0x04000B24 RID: 2852
			FailedToGetItem,
			// Token: 0x04000B25 RID: 2853
			descMissingProperty
		}
	}
}
