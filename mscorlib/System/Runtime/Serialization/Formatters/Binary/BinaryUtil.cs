using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000752 RID: 1874
	internal static class BinaryUtil
	{
		// Token: 0x0600529F RID: 21151 RVA: 0x00122345 File Offset: 0x00120545
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, string value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x00122352 File Offset: 0x00120552
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, object value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}
	}
}
