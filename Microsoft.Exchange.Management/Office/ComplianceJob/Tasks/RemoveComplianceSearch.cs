using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000758 RID: 1880
	[Cmdlet("Remove", "ComplianceSearch", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveComplianceSearch : RemoveComplianceJob<ComplianceSearch>
	{
		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x001138EA File Offset: 0x00111AEA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveComplianceSearchConfirmation(base.DataObject.Name);
			}
		}
	}
}
