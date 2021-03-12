using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001CC RID: 460
	[DataContract(Name = "ErrorType", Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/")]
	public enum ErrorTypePersisted : short
	{
		// Token: 0x040006FC RID: 1788
		[EnumMember]
		Unknown,
		// Token: 0x040006FD RID: 1789
		[EnumMember]
		Success,
		// Token: 0x040006FE RID: 1790
		[EnumMember]
		Failure
	}
}
