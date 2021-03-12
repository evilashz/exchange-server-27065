using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CC6 RID: 3270
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointBindingSchema : Schema
	{
		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x0600718B RID: 29067 RVA: 0x001F7794 File Offset: 0x001F5994
		public new static SharepointBindingSchema Instance
		{
			get
			{
				if (SharepointBindingSchema.instance == null)
				{
					SharepointBindingSchema.instance = new SharepointBindingSchema();
				}
				return SharepointBindingSchema.instance;
			}
		}

		// Token: 0x04004EBD RID: 20157
		public static readonly StorePropertyDefinition ProviderGuid = InternalSchema.SharingProviderGuid;

		// Token: 0x04004EBE RID: 20158
		public static readonly StorePropertyDefinition SharepointFolder = InternalSchema.SharingRemotePath;

		// Token: 0x04004EBF RID: 20159
		public static readonly StorePropertyDefinition SharepointFolderDisplayName = InternalSchema.SharingRemoteName;

		// Token: 0x04004EC0 RID: 20160
		private static SharepointBindingSchema instance = null;
	}
}
