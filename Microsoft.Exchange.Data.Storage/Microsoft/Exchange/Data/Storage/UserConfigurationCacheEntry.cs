using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200028C RID: 652
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class UserConfigurationCacheEntry
	{
		// Token: 0x06001B36 RID: 6966 RVA: 0x0007DD4B File Offset: 0x0007BF4B
		public UserConfigurationCacheEntry(string configName, StoreObjectId folderId, StoreObjectId itemId)
		{
			Util.ThrowOnNullArgument(configName, "configName");
			this.configName = configName;
			this.folderId = folderId;
			this.itemId = itemId;
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0007DD73 File Offset: 0x0007BF73
		public bool CheckMatch(string configName, StoreObjectId folderId)
		{
			return string.Equals(configName, this.configName, StringComparison.OrdinalIgnoreCase) && folderId.Equals(this.folderId);
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0007DD92 File Offset: 0x0007BF92
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0007DD9A File Offset: 0x0007BF9A
		public string ConfigurationName
		{
			get
			{
				return this.configName;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0007DDA2 File Offset: 0x0007BFA2
		public StoreObjectId ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x040012EC RID: 4844
		private readonly string configName;

		// Token: 0x040012ED RID: 4845
		private readonly StoreObjectId folderId;

		// Token: 0x040012EE RID: 4846
		private readonly StoreObjectId itemId;
	}
}
