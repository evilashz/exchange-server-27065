using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003CF RID: 975
	[Flags]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum GetGroupResultSet
	{
		// Token: 0x040011C7 RID: 4551
		GeneralInfo = 1,
		// Token: 0x040011C8 RID: 4552
		Members = 2
	}
}
