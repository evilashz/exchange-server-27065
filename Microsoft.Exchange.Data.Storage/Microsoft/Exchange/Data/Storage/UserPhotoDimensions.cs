using System;
using System.Drawing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E9A RID: 3738
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class UserPhotoDimensions
	{
		// Token: 0x060081F8 RID: 33272 RVA: 0x00238274 File Offset: 0x00236474
		public static Size GetImageSize(UserPhotoSize size)
		{
			switch (size)
			{
			case UserPhotoSize.HR48x48:
				return UserPhotoDimensions.HR48x48ImageSize;
			case UserPhotoSize.HR64x64:
				return UserPhotoDimensions.HR64x64ImageSize;
			case UserPhotoSize.HR96x96:
				return UserPhotoDimensions.HR96x96ImageSize;
			case UserPhotoSize.HR120x120:
				return UserPhotoDimensions.HR120x120ImageSize;
			case UserPhotoSize.HR240x240:
				return UserPhotoDimensions.HR240x240ImageSize;
			case UserPhotoSize.HR360x360:
				return UserPhotoDimensions.HR360x360ImageSize;
			case UserPhotoSize.HR432x432:
				return UserPhotoDimensions.HR432x432ImageSize;
			case UserPhotoSize.HR504x504:
				return UserPhotoDimensions.HR504x504ImageSize;
			case UserPhotoSize.HR648x648:
				return UserPhotoDimensions.HR648x648ImageSize;
			default:
				throw new EnumOutOfRangeException("size", size, "UserPhotoSize dimensions are unaccounted for.");
			}
		}

		// Token: 0x04005735 RID: 22325
		public static readonly Size HR648x648ImageSize = new Size(648, 648);

		// Token: 0x04005736 RID: 22326
		public static readonly Size HR504x504ImageSize = new Size(504, 504);

		// Token: 0x04005737 RID: 22327
		public static readonly Size HR432x432ImageSize = new Size(432, 432);

		// Token: 0x04005738 RID: 22328
		public static readonly Size HR360x360ImageSize = new Size(360, 360);

		// Token: 0x04005739 RID: 22329
		public static readonly Size HR240x240ImageSize = new Size(240, 240);

		// Token: 0x0400573A RID: 22330
		public static readonly Size HR120x120ImageSize = new Size(120, 120);

		// Token: 0x0400573B RID: 22331
		public static readonly Size HR96x96ImageSize = new Size(96, 96);

		// Token: 0x0400573C RID: 22332
		public static readonly Size HR64x64ImageSize = new Size(64, 64);

		// Token: 0x0400573D RID: 22333
		public static readonly Size HR48x48ImageSize = new Size(48, 48);
	}
}
