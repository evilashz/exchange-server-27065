using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x0200010D RID: 269
	public static class ToStringHelpers
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x0001F22C File Offset: 0x0001D42C
		public static string SafeToString(object item)
		{
			if (item == null)
			{
				return string.Empty;
			}
			return item.ToString();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001F25C File Offset: 0x0001D45C
		public static string SafeEnumerableToString<T>(IEnumerable<T> item, string separator)
		{
			if (string.IsNullOrEmpty(separator))
			{
				throw new ArgumentException("separator");
			}
			if (item == null)
			{
				return string.Empty;
			}
			return string.Join(separator, (from n in item
			where n != null
			select n.ToString()).ToArray<string>());
		}
	}
}
