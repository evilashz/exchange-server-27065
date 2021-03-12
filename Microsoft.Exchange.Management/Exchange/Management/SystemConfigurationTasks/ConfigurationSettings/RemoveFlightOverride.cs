using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000948 RID: 2376
	[Cmdlet("Remove", "FlightOverride", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveFlightOverride : RemoveOverrideBase
	{
		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x060054E2 RID: 21730 RVA: 0x0015E8BD File Offset: 0x0015CABD
		protected override bool IsFlight
		{
			get
			{
				return true;
			}
		}
	}
}
