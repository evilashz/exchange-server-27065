using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003EE RID: 1006
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum PerfTraceLevel
	{
		// Token: 0x04001251 RID: 4689
		Off,
		// Token: 0x04001252 RID: 4690
		Scenario,
		// Token: 0x04001253 RID: 4691
		Ctq,
		// Token: 0x04001254 RID: 4692
		Request,
		// Token: 0x04001255 RID: 4693
		Execution,
		// Token: 0x04001256 RID: 4694
		Detailed,
		// Token: 0x04001257 RID: 4695
		Component,
		// Token: 0x04001258 RID: 4696
		Logging
	}
}
