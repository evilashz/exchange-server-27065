using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007F5 RID: 2037
	internal class TestEventArgs : EventArgs
	{
		// Token: 0x06002ABF RID: 10943 RVA: 0x0005D0F3 File Offset: 0x0005B2F3
		public TestEventArgs(TestId testId, object eventState)
		{
			this.TestId = testId;
			this.EventState = eventState;
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x0005D109 File Offset: 0x0005B309
		// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x0005D111 File Offset: 0x0005B311
		public TestId TestId { get; set; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x0005D11A File Offset: 0x0005B31A
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x0005D122 File Offset: 0x0005B322
		public object EventState { get; set; }
	}
}
