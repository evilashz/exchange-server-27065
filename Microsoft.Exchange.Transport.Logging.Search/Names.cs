using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000036 RID: 54
	internal class Names<T> where T : struct
	{
		// Token: 0x040000DD RID: 221
		public static string[] Map = Enum.GetNames(typeof(T));
	}
}
