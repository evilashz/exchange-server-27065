using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003E3 RID: 995
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class LogDatapointRequest
	{
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x00078513 File Offset: 0x00076713
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x0007851B File Offset: 0x0007671B
		[DataMember(Name = "datapoints", IsRequired = true)]
		public Datapoint[] Datapoints { get; set; }
	}
}
