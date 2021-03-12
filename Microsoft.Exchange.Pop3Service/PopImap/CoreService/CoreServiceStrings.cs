using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.PopImap.CoreService
{
	// Token: 0x02000004 RID: 4
	internal static class CoreServiceStrings
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000271C File Offset: 0x0000091C
		static CoreServiceStrings()
		{
			CoreServiceStrings.stringIDs.Add(1139742230U, "UsageText");
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000276B File Offset: 0x0000096B
		public static string UsageText
		{
			get
			{
				return CoreServiceStrings.ResourceManager.GetString("UsageText");
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000277C File Offset: 0x0000097C
		public static string GetLocalizedString(CoreServiceStrings.IDs key)
		{
			return CoreServiceStrings.ResourceManager.GetString(CoreServiceStrings.stringIDs[(uint)key]);
		}

		// Token: 0x04000012 RID: 18
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000013 RID: 19
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.PopImap.CoreService.CoreServiceStrings", typeof(CoreServiceStrings).GetTypeInfo().Assembly);

		// Token: 0x02000005 RID: 5
		public enum IDs : uint
		{
			// Token: 0x04000015 RID: 21
			UsageText = 1139742230U
		}
	}
}
