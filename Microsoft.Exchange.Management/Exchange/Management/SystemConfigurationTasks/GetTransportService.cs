using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B8 RID: 2488
	[Cmdlet("Get", "TransportService", DefaultParameterSetName = "Identity")]
	public sealed class GetTransportService : GetTransportServiceBase
	{
	}
}
