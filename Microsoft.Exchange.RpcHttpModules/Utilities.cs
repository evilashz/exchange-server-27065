using System;
using System.Text;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000018 RID: 24
	public static class Utilities
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003851 File Offset: 0x00001A51
		public static string EncodeToBase64(string input)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003863 File Offset: 0x00001A63
		public static string DecodeFromBase64(string input)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(input));
		}
	}
}
