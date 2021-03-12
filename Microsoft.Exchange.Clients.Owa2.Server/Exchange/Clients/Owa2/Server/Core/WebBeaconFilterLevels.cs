using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200041D RID: 1053
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum WebBeaconFilterLevels
	{
		// Token: 0x04001363 RID: 4963
		UserFilterChoice,
		// Token: 0x04001364 RID: 4964
		ForceFilter,
		// Token: 0x04001365 RID: 4965
		DisableFilter
	}
}
