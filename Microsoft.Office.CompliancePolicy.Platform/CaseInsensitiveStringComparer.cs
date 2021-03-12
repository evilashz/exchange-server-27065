using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200002F RID: 47
	internal sealed class CaseInsensitiveStringComparer : IStringComparer
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000031E4 File Offset: 0x000013E4
		public static CaseInsensitiveStringComparer Instance
		{
			get
			{
				return CaseInsensitiveStringComparer.instance;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000031EB File Offset: 0x000013EB
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000053 RID: 83
		private static readonly CaseInsensitiveStringComparer instance = new CaseInsensitiveStringComparer();
	}
}
