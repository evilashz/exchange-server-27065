using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C3 RID: 1987
	internal struct AuthenticationData
	{
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x00058143 File Offset: 0x00056343
		// (set) Token: 0x06002912 RID: 10514 RVA: 0x0005814B File Offset: 0x0005634B
		public bool UseDefaultCredentials { get; set; }

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x00058154 File Offset: 0x00056354
		// (set) Token: 0x06002914 RID: 10516 RVA: 0x0005815C File Offset: 0x0005635C
		public ICredentials Credentials { get; set; }
	}
}
