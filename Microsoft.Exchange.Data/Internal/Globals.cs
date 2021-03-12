using System;
using System.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000009 RID: 9
	internal class Globals
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002C38 File Offset: 0x00000E38
		public static Guid ComponentGuid
		{
			get
			{
				return Globals.componentGuid;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C3F File Offset: 0x00000E3F
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string formatString, params object[] parameters)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C41 File Offset: 0x00000E41
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x0400001A RID: 26
		internal const string ComponentGuidString = "{82956720-170a-4dc6-8984-fb1816647d4e}";

		// Token: 0x0400001B RID: 27
		public static readonly StringPool StringPool = new StringPool(StringComparer.Ordinal);

		// Token: 0x0400001C RID: 28
		private static readonly Guid componentGuid = new Guid("{82956720-170a-4dc6-8984-fb1816647d4e}");
	}
}
