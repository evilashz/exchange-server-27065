using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009D0 RID: 2512
	[Cmdlet("Set", "TransportService", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetTransportService : SetTransportServiceBase
	{
	}
}
