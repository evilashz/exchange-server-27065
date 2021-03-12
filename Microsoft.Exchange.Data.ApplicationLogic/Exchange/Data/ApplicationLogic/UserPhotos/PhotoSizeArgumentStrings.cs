using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x0200020C RID: 524
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PhotoSizeArgumentStrings
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x0004EF08 File Offset: 0x0004D108
		public static string Get(UserPhotoSize size)
		{
			switch (size)
			{
			case UserPhotoSize.HR48x48:
				return "size=HR48X48";
			case UserPhotoSize.HR64x64:
				return "size=HR64X64";
			case UserPhotoSize.HR96x96:
				return "size=HR96X96";
			case UserPhotoSize.HR120x120:
				return "size=HR120X120";
			case UserPhotoSize.HR240x240:
				return "size=HR240X240";
			case UserPhotoSize.HR360x360:
				return "size=HR360X360";
			case UserPhotoSize.HR432x432:
				return "size=HR432X432";
			case UserPhotoSize.HR504x504:
				return "size=HR504X504";
			case UserPhotoSize.HR648x648:
				return "size=HR648X648";
			default:
				throw new ArgumentOutOfRangeException("size");
			}
		}

		// Token: 0x04000A89 RID: 2697
		private const string HR48x48 = "size=HR48X48";

		// Token: 0x04000A8A RID: 2698
		private const string HR64x64 = "size=HR64X64";

		// Token: 0x04000A8B RID: 2699
		private const string HR96x96 = "size=HR96X96";

		// Token: 0x04000A8C RID: 2700
		private const string HR120x120 = "size=HR120X120";

		// Token: 0x04000A8D RID: 2701
		private const string HR240x240 = "size=HR240X240";

		// Token: 0x04000A8E RID: 2702
		private const string HR360x360 = "size=HR360X360";

		// Token: 0x04000A8F RID: 2703
		private const string HR432x432 = "size=HR432X432";

		// Token: 0x04000A90 RID: 2704
		private const string HR504x504 = "size=HR504X504";

		// Token: 0x04000A91 RID: 2705
		private const string HR648x648 = "size=HR648X648";
	}
}
