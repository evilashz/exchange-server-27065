using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200003B RID: 59
	internal class AutoDiscoverResult
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00007EBA File Offset: 0x000060BA
		public AutoDiscoverResult(AutoDiscoverResultCode resultCode, string resultValue)
		{
			this.ResultCode = resultCode;
			this.ResultValue = resultValue;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00007ED0 File Offset: 0x000060D0
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00007ED8 File Offset: 0x000060D8
		public AutoDiscoverResultCode ResultCode { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00007EE1 File Offset: 0x000060E1
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00007EE9 File Offset: 0x000060E9
		public string ResultValue { get; private set; }

		// Token: 0x06000240 RID: 576 RVA: 0x00007EF4 File Offset: 0x000060F4
		public Uri GetEwsEndpoint()
		{
			Uri result = null;
			if (this.ResultCode == AutoDiscoverResultCode.Success)
			{
				result = new Uri(this.ResultValue);
			}
			return result;
		}
	}
}
