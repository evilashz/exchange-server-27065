using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000C5 RID: 197
	internal class MobileRecoRPCAsyncCompletedArgs
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00019B5B File Offset: 0x00017D5B
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00019B63 File Offset: 0x00017D63
		public string Result { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00019B6C File Offset: 0x00017D6C
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00019B74 File Offset: 0x00017D74
		public int ErrorCode { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00019B7D File Offset: 0x00017D7D
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00019B85 File Offset: 0x00017D85
		public string ErrorMessage { get; private set; }

		// Token: 0x060006A1 RID: 1697 RVA: 0x00019B8E File Offset: 0x00017D8E
		public MobileRecoRPCAsyncCompletedArgs(string result, int errorCode, string errorMessage)
		{
			this.Result = result;
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
		}
	}
}
