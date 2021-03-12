using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF8 RID: 3576
	internal static class UrlUtilities
	{
		// Token: 0x06005C90 RID: 23696 RVA: 0x00120950 File Offset: 0x0011EB50
		public static string TrimKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return key;
			}
			return key.Trim(new char[]
			{
				'\'',
				'"',
				' '
			});
		}
	}
}
