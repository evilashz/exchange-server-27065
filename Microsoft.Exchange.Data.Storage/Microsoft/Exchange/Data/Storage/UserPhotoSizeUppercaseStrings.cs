using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E9C RID: 3740
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UserPhotoSizeUppercaseStrings
	{
		// Token: 0x060081FA RID: 33274 RVA: 0x002383A8 File Offset: 0x002365A8
		public static bool TryMapStringToSize(string sizeString, out UserPhotoSize size)
		{
			if ("HR48X48".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR48x48;
				return true;
			}
			if ("HR64X64".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR64x64;
				return true;
			}
			if ("HR96X96".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR96x96;
				return true;
			}
			if ("HR120X120".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR120x120;
				return true;
			}
			if ("HR240X240".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR240x240;
				return true;
			}
			if ("HR360X360".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR360x360;
				return true;
			}
			if ("HR432X432".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR432x432;
				return true;
			}
			if ("HR504X504".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR504x504;
				return true;
			}
			if ("HR648X648".Equals(sizeString, StringComparison.OrdinalIgnoreCase))
			{
				size = UserPhotoSize.HR648x648;
				return true;
			}
			size = UserPhotoSize.HR48x48;
			return false;
		}

		// Token: 0x04005748 RID: 22344
		public const string HR48x48 = "HR48X48";

		// Token: 0x04005749 RID: 22345
		public const string HR64x64 = "HR64X64";

		// Token: 0x0400574A RID: 22346
		public const string HR96x96 = "HR96X96";

		// Token: 0x0400574B RID: 22347
		public const string HR120x120 = "HR120X120";

		// Token: 0x0400574C RID: 22348
		public const string HR240x240 = "HR240X240";

		// Token: 0x0400574D RID: 22349
		public const string HR360x360 = "HR360X360";

		// Token: 0x0400574E RID: 22350
		public const string HR432x432 = "HR432X432";

		// Token: 0x0400574F RID: 22351
		public const string HR504x504 = "HR504X504";

		// Token: 0x04005750 RID: 22352
		public const string HR648x648 = "HR648X648";
	}
}
