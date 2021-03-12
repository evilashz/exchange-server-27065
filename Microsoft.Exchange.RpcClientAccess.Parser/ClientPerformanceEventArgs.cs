using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001A7 RID: 423
	internal class ClientPerformanceEventArgs
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001D946 File Offset: 0x0001BB46
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0001D94E File Offset: 0x0001BB4E
		public ClientPerformanceEventType EventType { get; set; }

		// Token: 0x06000874 RID: 2164 RVA: 0x0001D957 File Offset: 0x0001BB57
		public ClientPerformanceEventArgs(ClientPerformanceEventType eventType)
		{
			this.EventType = eventType;
		}
	}
}
