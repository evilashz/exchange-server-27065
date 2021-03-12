using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003EF RID: 1007
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum JsMvvmPerfTraceLevel
	{
		// Token: 0x0400125A RID: 4698
		Off,
		// Token: 0x0400125B RID: 4699
		Essential,
		// Token: 0x0400125C RID: 4700
		Info,
		// Token: 0x0400125D RID: 4701
		Verbose,
		// Token: 0x0400125E RID: 4702
		Debug
	}
}
