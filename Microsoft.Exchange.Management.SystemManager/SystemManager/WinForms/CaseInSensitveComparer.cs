using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000AF RID: 175
	internal class CaseInSensitveComparer : IEqualityComparer<string>
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x000150CD File Offset: 0x000132CD
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000150D7 File Offset: 0x000132D7
		public int GetHashCode(string obj)
		{
			return obj.ToLowerInvariant().GetHashCode();
		}
	}
}
