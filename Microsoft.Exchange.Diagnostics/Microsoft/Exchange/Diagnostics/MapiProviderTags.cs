using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200023E RID: 574
	public struct MapiProviderTags
	{
		// Token: 0x04000EFB RID: 3835
		public const int MapiSession = 0;

		// Token: 0x04000EFC RID: 3836
		public const int MapiObject = 1;

		// Token: 0x04000EFD RID: 3837
		public const int PropertyBag = 2;

		// Token: 0x04000EFE RID: 3838
		public const int MessageStore = 3;

		// Token: 0x04000EFF RID: 3839
		public const int Folder = 4;

		// Token: 0x04000F00 RID: 3840
		public const int LogonStatistics = 5;

		// Token: 0x04000F01 RID: 3841
		public const int Convertor = 6;

		// Token: 0x04000F02 RID: 3842
		public static Guid guid = new Guid("C9AAFFBB-C5D9-4e08-B398-7733BC04D45E");
	}
}
