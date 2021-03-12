using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000DF RID: 223
	public class RegisterConditionResult
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x00025417 File Offset: 0x00023617
		public RegisterConditionResult()
		{
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00025420 File Offset: 0x00023620
		internal RegisterConditionResult(ConditionalRegistration registration)
		{
			this.Cookie = registration.Cookie;
			this.ParsedFilter = registration.QueryFilter.ToString();
			this.MaxHits = registration.MaxHits;
			this.TimeToLive = registration.TimeToLive.ToString();
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00025476 File Offset: 0x00023676
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0002547E File Offset: 0x0002367E
		public string Cookie { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00025487 File Offset: 0x00023687
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0002548F File Offset: 0x0002368F
		public string ParsedFilter { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00025498 File Offset: 0x00023698
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x000254A0 File Offset: 0x000236A0
		public int MaxHits { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000254A9 File Offset: 0x000236A9
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x000254B1 File Offset: 0x000236B1
		public string TimeToLive { get; set; }
	}
}
