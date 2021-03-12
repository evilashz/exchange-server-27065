using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x02000461 RID: 1121
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Helper
	{
		// Token: 0x060031F7 RID: 12791 RVA: 0x000CCE17 File Offset: 0x000CB017
		public static string FromBytes(byte[] bytes)
		{
			if (bytes != null && bytes.Length > 0)
			{
				return Encoding.UTF8.GetString(bytes);
			}
			return null;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000CCE2F File Offset: 0x000CB02F
		public static byte[] ToBytes(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return Encoding.UTF8.GetBytes(s);
			}
			return null;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000CCE46 File Offset: 0x000CB046
		public static string ToDefaultString(string input, string defaultValue = null)
		{
			if (string.IsNullOrEmpty(input))
			{
				return defaultValue;
			}
			return input;
		}
	}
}
