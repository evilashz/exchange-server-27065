using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x02000029 RID: 41
	internal class AnonymousAutodiscoverResult
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00005D50 File Offset: 0x00003F50
		internal AnonymousAutodiscoverResult()
		{
			this.Redirects = new List<string>();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005D63 File Offset: 0x00003F63
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00005D6B File Offset: 0x00003F6B
		public string AuthenticatedServerUri { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005D74 File Offset: 0x00003F74
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00005D7C File Offset: 0x00003F7C
		public List<string> Redirects { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005D85 File Offset: 0x00003F85
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00005D8D File Offset: 0x00003F8D
		public Exception Exception { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005D96 File Offset: 0x00003F96
		public bool HasException
		{
			get
			{
				return this.Exception != null;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005DA4 File Offset: 0x00003FA4
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00005DAC File Offset: 0x00003FAC
		public string RequestHeaders { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005DB5 File Offset: 0x00003FB5
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00005DBD File Offset: 0x00003FBD
		public string ResponseHeaders { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005DC6 File Offset: 0x00003FC6
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00005DCE File Offset: 0x00003FCE
		public string ResponseBody { get; set; }
	}
}
