using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Main
{
	// Token: 0x02000004 RID: 4
	internal static class Strings
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00003444 File Offset: 0x00001644
		public static LocalizedString AppConfigLoadFailure(string error)
		{
			return new LocalizedString("AppConfigLoadFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003474 File Offset: 0x00001674
		public static LocalizedString PrivilegeRemovalFailure(int win32Error)
		{
			return new LocalizedString("PrivilegeRemovalFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				win32Error
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000034A8 File Offset: 0x000016A8
		public static LocalizedString ADOperationFailure(string exception)
		{
			return new LocalizedString("ADOperationFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000034D8 File Offset: 0x000016D8
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", "Ex26AE70", false, true, Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x0400001D RID: 29
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Main.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000005 RID: 5
		private enum ParamIDs
		{
			// Token: 0x0400001F RID: 31
			AppConfigLoadFailure,
			// Token: 0x04000020 RID: 32
			PrivilegeRemovalFailure,
			// Token: 0x04000021 RID: 33
			ADOperationFailure,
			// Token: 0x04000022 RID: 34
			UsageText
		}
	}
}
