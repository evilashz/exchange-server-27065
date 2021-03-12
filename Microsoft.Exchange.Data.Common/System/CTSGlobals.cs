using System;
using System.Text;

namespace System
{
	// Token: 0x02000002 RID: 2
	public static class CTSGlobals
	{
		// Token: 0x04000001 RID: 1
		public const int ReadBufferSize = 16384;

		// Token: 0x04000002 RID: 2
		public static Encoding AsciiEncoding = Encoding.GetEncoding("us-ascii");

		// Token: 0x04000003 RID: 3
		public static Encoding UnicodeEncoding = Encoding.GetEncoding("utf-16");

		// Token: 0x04000004 RID: 4
		public static Encoding Utf8Encoding = Encoding.GetEncoding("utf-8");
	}
}
