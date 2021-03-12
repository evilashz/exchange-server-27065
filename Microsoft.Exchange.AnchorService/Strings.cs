using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x0200003D RID: 61
	internal static class Strings
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00009454 File Offset: 0x00007654
		static Strings()
		{
			Strings.stringIDs.Add(1191245078U, "AnchorMailboxNotFound");
			Strings.stringIDs.Add(3166985769U, "MultipleAnchorMailboxesFound");
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000094B7 File Offset: 0x000076B7
		public static LocalizedString AnchorMailboxNotFound
		{
			get
			{
				return new LocalizedString("AnchorMailboxNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000094CE File Offset: 0x000076CE
		public static LocalizedString MultipleAnchorMailboxesFound
		{
			get
			{
				return new LocalizedString("MultipleAnchorMailboxesFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000094E5 File Offset: 0x000076E5
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040000AB RID: 171
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x040000AC RID: 172
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.AnchorService.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200003E RID: 62
		public enum IDs : uint
		{
			// Token: 0x040000AE RID: 174
			AnchorMailboxNotFound = 1191245078U,
			// Token: 0x040000AF RID: 175
			MultipleAnchorMailboxesFound = 3166985769U
		}
	}
}
