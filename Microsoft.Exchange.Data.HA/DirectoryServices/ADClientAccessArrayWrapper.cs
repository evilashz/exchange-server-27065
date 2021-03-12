using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADClientAccessArrayWrapper : ADObjectWrapperBase, IADClientAccessArray, IADObjectCommon
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00004173 File Offset: 0x00002373
		private ADClientAccessArrayWrapper(ClientAccessArray caArray) : base(caArray)
		{
			this.ExchangeLegacyDN = (string)caArray[ClientAccessArraySchema.ExchangeLegacyDN];
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004192 File Offset: 0x00002392
		public static ADClientAccessArrayWrapper CreateWrapper(ClientAccessArray caArray)
		{
			if (caArray == null)
			{
				return null;
			}
			return new ADClientAccessArrayWrapper(caArray);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000419F File Offset: 0x0000239F
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000041A7 File Offset: 0x000023A7
		public string ExchangeLegacyDN { get; private set; }
	}
}
