using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200000D RID: 13
	public class MockClientResult<T> : IClientResult<T>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000263F File Offset: 0x0000083F
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002647 File Offset: 0x00000847
		public T Value { get; set; }
	}
}
