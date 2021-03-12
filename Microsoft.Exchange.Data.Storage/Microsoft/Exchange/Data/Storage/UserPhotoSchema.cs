using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CE3 RID: 3299
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserPhotoSchema : StoreObjectSchema
	{
		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x06007217 RID: 29207 RVA: 0x001F91C4 File Offset: 0x001F73C4
		public new static UserPhotoSchema Instance
		{
			get
			{
				if (UserPhotoSchema.instance == null)
				{
					UserPhotoSchema.instance = new UserPhotoSchema();
				}
				return UserPhotoSchema.instance;
			}
		}

		// Token: 0x04004F52 RID: 20306
		public static readonly PropertyDefinition UserPhotoHR648x648 = InternalSchema.UserPhotoHR648x648;

		// Token: 0x04004F53 RID: 20307
		public static readonly PropertyDefinition UserPhotoHR504x504 = InternalSchema.UserPhotoHR504x504;

		// Token: 0x04004F54 RID: 20308
		public static readonly PropertyDefinition UserPhotoHR432x432 = InternalSchema.UserPhotoHR432x432;

		// Token: 0x04004F55 RID: 20309
		public static readonly PropertyDefinition UserPhotoHR360x360 = InternalSchema.UserPhotoHR360x360;

		// Token: 0x04004F56 RID: 20310
		public static readonly PropertyDefinition UserPhotoHR240x240 = InternalSchema.UserPhotoHR240x240;

		// Token: 0x04004F57 RID: 20311
		public static readonly PropertyDefinition UserPhotoHR120x120 = InternalSchema.UserPhotoHR120x120;

		// Token: 0x04004F58 RID: 20312
		public static readonly PropertyDefinition UserPhotoHR96x96 = InternalSchema.UserPhotoHR96x96;

		// Token: 0x04004F59 RID: 20313
		public static readonly PropertyDefinition UserPhotoHR64x64 = InternalSchema.UserPhotoHR64x64;

		// Token: 0x04004F5A RID: 20314
		public static readonly PropertyDefinition UserPhotoHR48x48 = InternalSchema.UserPhotoHR48x48;

		// Token: 0x04004F5B RID: 20315
		private static UserPhotoSchema instance;
	}
}
