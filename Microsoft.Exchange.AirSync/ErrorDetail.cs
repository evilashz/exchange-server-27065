using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000079 RID: 121
	public class ErrorDetail
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00025AE7 File Offset: 0x00023CE7
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x00025AEF File Offset: 0x00023CEF
		public string ErrorType { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00025AF8 File Offset: 0x00023CF8
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00025B00 File Offset: 0x00023D00
		public string ErrorMessage { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00025B09 File Offset: 0x00023D09
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x00025B11 File Offset: 0x00023D11
		public string StackTrace { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00025B1A File Offset: 0x00023D1A
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00025B22 File Offset: 0x00023D22
		public string UserEmail { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00025B2B File Offset: 0x00023D2B
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00025B33 File Offset: 0x00023D33
		public string DeviceID { get; set; }
	}
}
