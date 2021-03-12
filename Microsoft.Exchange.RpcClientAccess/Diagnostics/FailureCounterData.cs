using System;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200002A RID: 42
	internal class FailureCounterData : IRpcCounterData
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005C30 File Offset: 0x00003E30
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00005C38 File Offset: 0x00003E38
		public uint FailureCode { get; set; }
	}
}
