using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C4E RID: 3150
	[Cmdlet("Set", "PowerShellVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetPowerShellVirtualDirectory : SetPowerShellCommonVirtualDirectory<ADPowerShellVirtualDirectory>
	{
		// Token: 0x170024E6 RID: 9446
		// (get) Token: 0x06007782 RID: 30594 RVA: 0x001E733B File Offset: 0x001E553B
		// (set) Token: 0x06007783 RID: 30595 RVA: 0x001E7343 File Offset: 0x001E5543
		internal new bool DigestAuthentication
		{
			get
			{
				return base.DigestAuthentication;
			}
			set
			{
				base.DigestAuthentication = value;
			}
		}

		// Token: 0x170024E7 RID: 9447
		// (get) Token: 0x06007784 RID: 30596 RVA: 0x001E734C File Offset: 0x001E554C
		// (set) Token: 0x06007785 RID: 30597 RVA: 0x001E7377 File Offset: 0x001E5577
		[Parameter(Mandatory = false)]
		public bool EnableSessionKeyRedirectionModule
		{
			get
			{
				return base.Fields["EnableSessionKeyRedirectionModule"] != null && (bool)base.Fields["EnableSessionKeyRedirectionModule"];
			}
			set
			{
				base.Fields["EnableSessionKeyRedirectionModule"] = value;
			}
		}

		// Token: 0x170024E8 RID: 9448
		// (get) Token: 0x06007786 RID: 30598 RVA: 0x001E738F File Offset: 0x001E558F
		// (set) Token: 0x06007787 RID: 30599 RVA: 0x001E73BA File Offset: 0x001E55BA
		[Parameter(Mandatory = false)]
		public bool EnableDelegatedAuthModule
		{
			get
			{
				return base.Fields["EnableDelegatedAuthModule"] != null && (bool)base.Fields["EnableDelegatedAuthModule"];
			}
			set
			{
				base.Fields["EnableDelegatedAuthModule"] = value;
			}
		}

		// Token: 0x170024E9 RID: 9449
		// (get) Token: 0x06007788 RID: 30600 RVA: 0x001E73D2 File Offset: 0x001E55D2
		// (set) Token: 0x06007789 RID: 30601 RVA: 0x001E73FD File Offset: 0x001E55FD
		[Parameter(Mandatory = false)]
		public bool RequireSSL
		{
			get
			{
				return base.Fields["RequireSSL"] != null && (bool)base.Fields["RequireSSL"];
			}
			set
			{
				base.Fields["RequireSSL"] = value;
			}
		}

		// Token: 0x170024EA RID: 9450
		// (get) Token: 0x0600778A RID: 30602 RVA: 0x001E7415 File Offset: 0x001E5615
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetPowerShellVirtualDirectory(this.DataObject.Name, this.DataObject.Server.ToString());
			}
		}

		// Token: 0x170024EB RID: 9451
		// (get) Token: 0x0600778B RID: 30603 RVA: 0x001E7437 File Offset: 0x001E5637
		protected override PowerShellVirtualDirectoryType AllowedVirtualDirectoryType
		{
			get
			{
				return PowerShellVirtualDirectoryType.PowerShell;
			}
		}

		// Token: 0x0600778C RID: 30604 RVA: 0x001E743C File Offset: 0x001E563C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			base.InternalEnableLiveIdNegotiateAuxiliaryModule();
			if (base.Fields["EnableSessionKeyRedirectionModule"] != null)
			{
				base.SetSessionKeyRedirectionModule(this.EnableSessionKeyRedirectionModule, false);
			}
			if (base.Fields["EnableDelegatedAuthModule"] != null)
			{
				base.SetDelegatedAuthenticationModule(this.EnableDelegatedAuthModule, false);
				base.SetPowerShellRequestFilterModule(this.EnableDelegatedAuthModule, false);
			}
			if (base.Fields["RequireSSL"] != null)
			{
				ExchangeServiceVDirHelper.SetSSLRequired(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError), Strings.ErrorUpdatingVDir(this.DataObject.MetabasePath, string.Empty), (bool)base.Fields["RequireSSL"]);
			}
		}
	}
}
