using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CF0 RID: 3312
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Token
	{
		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x06007238 RID: 29240 RVA: 0x001F9440 File Offset: 0x001F7640
		public int Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x06007239 RID: 29241 RVA: 0x001F9448 File Offset: 0x001F7648
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x001F9450 File Offset: 0x001F7650
		public Token(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		// Token: 0x04004F8F RID: 20367
		private int start;

		// Token: 0x04004F90 RID: 20368
		private int length;
	}
}
