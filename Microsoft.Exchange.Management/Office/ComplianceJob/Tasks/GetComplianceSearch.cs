using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000759 RID: 1881
	[Cmdlet("Get", "ComplianceSearch", DefaultParameterSetName = "Identity")]
	public sealed class GetComplianceSearch : GetComplianceJob<ComplianceSearch>
	{
	}
}
