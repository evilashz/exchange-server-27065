using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000393 RID: 915
	internal class SpeechRecognitionAsyncResult : AsyncResult
	{
		// Token: 0x06001D3E RID: 7486 RVA: 0x00074877 File Offset: 0x00072A77
		public SpeechRecognitionAsyncResult(AsyncCallback callback, object context) : base(callback, context)
		{
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00074881 File Offset: 0x00072A81
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x00074889 File Offset: 0x00072A89
		public int StatusCode { get; set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00074892 File Offset: 0x00072A92
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0007489A File Offset: 0x00072A9A
		public string StatusDescription { get; set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x000748A3 File Offset: 0x00072AA3
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x000748AB File Offset: 0x00072AAB
		public string ResponseText { get; set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x000748B4 File Offset: 0x00072AB4
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x000748BC File Offset: 0x00072ABC
		public double ThrottlingDelay { get; set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x000748C5 File Offset: 0x00072AC5
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x000748CD File Offset: 0x00072ACD
		public string ThrottlingNotEnforcedReason { get; set; }
	}
}
