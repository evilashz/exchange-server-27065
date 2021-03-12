using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000039 RID: 57
	internal sealed class CaseInsensitiveStringComparer : IStringComparer
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006CE1 File Offset: 0x00004EE1
		public static CaseInsensitiveStringComparer Instance
		{
			get
			{
				return CaseInsensitiveStringComparer.instance;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040000B0 RID: 176
		private static readonly CaseInsensitiveStringComparer instance = new CaseInsensitiveStringComparer();
	}
}
