using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x0200092A RID: 2346
	internal class RpcHttpQueryString
	{
		// Token: 0x0600324A RID: 12874 RVA: 0x0007BC28 File Offset: 0x00079E28
		public RpcHttpQueryString(string queryString)
		{
			this.RcaServer = string.Empty;
			this.RcaServerPort = string.Empty;
			this.AdditionalParameters = string.Empty;
			if (string.IsNullOrEmpty(queryString))
			{
				return;
			}
			int num = queryString.IndexOf(':', 1);
			if (num == -1)
			{
				num = queryString.Length;
			}
			this.RcaServer = queryString.Substring(1, num - 1);
			int num2 = queryString.IndexOf('&', num);
			if (num2 == -1)
			{
				num2 = queryString.Length;
			}
			int num3 = num2 - (num + 1);
			if (num3 > 0)
			{
				this.RcaServerPort = queryString.Substring(num + 1, num3);
			}
			if (num2 < queryString.Length)
			{
				this.AdditionalParameters = queryString.Substring(num2);
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x0007BCCF File Offset: 0x00079ECF
		// (set) Token: 0x0600324C RID: 12876 RVA: 0x0007BCD7 File Offset: 0x00079ED7
		public string RcaServer { get; private set; }

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x0007BCE0 File Offset: 0x00079EE0
		// (set) Token: 0x0600324E RID: 12878 RVA: 0x0007BCE8 File Offset: 0x00079EE8
		public string RcaServerPort { get; private set; }

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x0007BCF1 File Offset: 0x00079EF1
		// (set) Token: 0x06003250 RID: 12880 RVA: 0x0007BCF9 File Offset: 0x00079EF9
		public string AdditionalParameters { get; private set; }
	}
}
