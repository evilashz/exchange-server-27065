using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000946 RID: 2374
	[Cmdlet("Get", "FlightOverride", DefaultParameterSetName = "Identity")]
	public sealed class GetFlightOverride : GetOverrideBase
	{
		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x060054D5 RID: 21717 RVA: 0x0015E7E7 File Offset: 0x0015C9E7
		protected override bool IsFlight
		{
			get
			{
				return true;
			}
		}
	}
}
