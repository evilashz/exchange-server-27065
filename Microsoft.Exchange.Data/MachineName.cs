using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200022B RID: 555
	internal static class MachineName
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0003B180 File Offset: 0x00039380
		internal static StringComparer Comparer
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0003B187 File Offset: 0x00039387
		internal static StringComparison Comparison
		{
			get
			{
				return StringComparison.OrdinalIgnoreCase;
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0003B18C File Offset: 0x0003938C
		internal static string GetNodeNameFromFqdn(string fqdn)
		{
			int num = fqdn.IndexOf(".");
			if (num != -1)
			{
				fqdn = fqdn.Substring(0, num);
			}
			return fqdn;
		}

		// Token: 0x04000B49 RID: 2889
		public static readonly string Local = Environment.MachineName;
	}
}
