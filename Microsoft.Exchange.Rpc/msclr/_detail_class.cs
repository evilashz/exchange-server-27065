using System;

namespace msclr
{
	// Token: 0x020001D7 RID: 471
	internal struct _detail_class
	{
		// Token: 0x04000B83 RID: 2947
		public static string _safe_true = _detail_class.dummy_struct.dummy_string;

		// Token: 0x04000B84 RID: 2948
		public static string _safe_false = null;

		// Token: 0x020001D8 RID: 472
		public struct dummy_struct
		{
			// Token: 0x04000B85 RID: 2949
			public static readonly string dummy_string = "";
		}
	}
}
