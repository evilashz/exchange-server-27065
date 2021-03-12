using System;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002B8 RID: 696
	internal static class UrlTokenConverter
	{
		// Token: 0x06001915 RID: 6421 RVA: 0x0004EFF4 File Offset: 0x0004D1F4
		internal static string UrlTokenEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			return HttpServerUtility.UrlTokenEncode(bytes);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0004F020 File Offset: 0x0004D220
		internal static bool TryUrlTokenDecode(string value, out string decodedValue)
		{
			decodedValue = null;
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			bool result;
			try
			{
				byte[] array = HttpServerUtility.UrlTokenDecode(value);
				if (array == null || array.Length == 0)
				{
					result = false;
				}
				else
				{
					decodedValue = Encoding.UTF8.GetString(array, 0, array.Length);
					result = true;
				}
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}
	}
}
