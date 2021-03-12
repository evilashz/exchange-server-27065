using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C33 RID: 3123
	[Cmdlet("Remove", "EcpVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveEcpVirtualDirectory : RemoveExchangeVirtualDirectory<ADEcpVirtualDirectory>
	{
		// Token: 0x17002475 RID: 9333
		// (get) Token: 0x0600765B RID: 30299 RVA: 0x001E3477 File Offset: 0x001E1677
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveEcpVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x17002476 RID: 9334
		// (get) Token: 0x0600765C RID: 30300 RVA: 0x001E3489 File Offset: 0x001E1689
		protected override ICollection ChildVirtualDirectoryNames
		{
			get
			{
				return RemoveEcpVirtualDirectory.ChildVirtualDirectory;
			}
		}

		// Token: 0x04003BC1 RID: 15297
		private const string ReportingWebService = "ReportingWebService";

		// Token: 0x04003BC2 RID: 15298
		private static readonly string[] ChildVirtualDirectory = new string[]
		{
			"ReportingWebService"
		};
	}
}
