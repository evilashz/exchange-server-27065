using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x0200075C RID: 1884
	[Cmdlet("Stop", "ComplianceSearch", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class StopComplianceSearch : StopComplianceJob<ComplianceSearch>
	{
		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x00113BF3 File Offset: 0x00111DF3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStopComplianceSearch(this.Identity.ToString());
			}
		}
	}
}
