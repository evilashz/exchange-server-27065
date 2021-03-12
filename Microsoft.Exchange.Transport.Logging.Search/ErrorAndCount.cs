using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000039 RID: 57
	internal class ErrorAndCount : IComparable<ErrorAndCount>
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00007EF2 File Offset: 0x000060F2
		public int CompareTo(ErrorAndCount other)
		{
			return this.Count.CompareTo(other.Count);
		}

		// Token: 0x040000E4 RID: 228
		public string Error;

		// Token: 0x040000E5 RID: 229
		public int Count;

		// Token: 0x0200003A RID: 58
		internal enum Properties
		{
			// Token: 0x040000E7 RID: 231
			Error,
			// Token: 0x040000E8 RID: 232
			Count
		}
	}
}
