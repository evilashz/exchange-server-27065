using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C40 RID: 3136
	[Cmdlet("Remove", "WebServicesVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveWebServicesVirtualDirectory : RemoveExchangeVirtualDirectory<ADWebServicesVirtualDirectory>
	{
		// Token: 0x17002483 RID: 9347
		// (get) Token: 0x06007682 RID: 30338 RVA: 0x001E3B9F File Offset: 0x001E1D9F
		// (set) Token: 0x06007683 RID: 30339 RVA: 0x001E3BA7 File Offset: 0x001E1DA7
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x17002484 RID: 9348
		// (get) Token: 0x06007684 RID: 30340 RVA: 0x001E3BB0 File Offset: 0x001E1DB0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveWebServicesVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x001E3BC2 File Offset: 0x001E1DC2
		protected override void InternalProcessRecord()
		{
			this.WriteWarning(Strings.WarningMessageRemoveWebServicesVirtualDirectory(this.Identity.ToString()));
			if (!this.Force && !base.ShouldContinue(Strings.ConfirmationMessageWebServicesVirtualDirectoryContinue))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
		}
	}
}
