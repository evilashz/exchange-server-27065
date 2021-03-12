using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000996 RID: 2454
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UriComparer : IEqualityComparer<Uri>
	{
		// Token: 0x06005A86 RID: 23174 RVA: 0x00178D0D File Offset: 0x00176F0D
		private UriComparer()
		{
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x00178D15 File Offset: 0x00176F15
		public static bool IsEqual(Uri x, Uri y)
		{
			return UriComparer.Default.Equals(x, y);
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x00178D23 File Offset: 0x00176F23
		public bool Equals(Uri x, Uri y)
		{
			return Uri.Compare(x, y, UriComponents.AbsoluteUri, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x00178D33 File Offset: 0x00176F33
		public int GetHashCode(Uri x)
		{
			return x.GetHashCode();
		}

		// Token: 0x040031F9 RID: 12793
		public static UriComparer Default = new UriComparer();
	}
}
