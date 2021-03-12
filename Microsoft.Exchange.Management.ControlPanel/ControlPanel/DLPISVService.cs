using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001BE RID: 446
	public class DLPISVService : DataSourceService
	{
		// Token: 0x0600240B RID: 9227 RVA: 0x0006E68D File Offset: 0x0006C88D
		public PowerShellResults ProcessUpload(DLPPolicyUploadParameters parameters)
		{
			parameters.FaultIfNull();
			if (parameters is DLPNewPolicyUploadParameters)
			{
				return base.Invoke(new PSCommand().AddCommand("New-DLPPolicy"), Identity.FromExecutingUserId(), parameters);
			}
			return null;
		}
	}
}
