using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C2A RID: 3114
	[Cmdlet("New", "PowerShellVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewPowerShellVirtualDirectory : NewPowerShellCommonVirtualDirectory<ADPowerShellVirtualDirectory>
	{
		// Token: 0x1700243B RID: 9275
		// (get) Token: 0x060075C2 RID: 30146 RVA: 0x001E112F File Offset: 0x001DF32F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewPowerShellVirtualDirectory(base.Name, base.Server.ToString());
			}
		}

		// Token: 0x1700243C RID: 9276
		// (get) Token: 0x060075C3 RID: 30147 RVA: 0x001E1147 File Offset: 0x001DF347
		protected override string VirtualDirectoryPath
		{
			get
			{
				if (base.Role == VirtualDirectoryRole.ClientAccess)
				{
					return "FrontEnd\\HttpProxy\\PowerShell";
				}
				return "ClientAccess\\PowerShell";
			}
		}

		// Token: 0x1700243D RID: 9277
		// (get) Token: 0x060075C4 RID: 30148 RVA: 0x001E115C File Offset: 0x001DF35C
		protected override string DefaultApplicationPoolId
		{
			get
			{
				if (base.Role == VirtualDirectoryRole.ClientAccess)
				{
					return "MSExchangePowerShellFrontEndAppPool";
				}
				return "MSExchangePowerShellAppPool";
			}
		}

		// Token: 0x1700243E RID: 9278
		// (get) Token: 0x060075C5 RID: 30149 RVA: 0x001E1171 File Offset: 0x001DF371
		// (set) Token: 0x060075C6 RID: 30150 RVA: 0x001E119C File Offset: 0x001DF39C
		[Parameter(Mandatory = false)]
		public bool RequireSSL
		{
			get
			{
				return base.Fields["RequireSSL"] == null || (bool)base.Fields["RequireSSL"];
			}
			set
			{
				base.Fields["RequireSSL"] = value;
			}
		}

		// Token: 0x060075C7 RID: 30151 RVA: 0x001E11B4 File Offset: 0x001DF3B4
		protected override IConfigurable PrepareDataObject()
		{
			ADPowerShellVirtualDirectory adpowerShellVirtualDirectory = (ADPowerShellVirtualDirectory)base.PrepareDataObject();
			adpowerShellVirtualDirectory.VirtualDirectoryType = PowerShellVirtualDirectoryType.PowerShell;
			return adpowerShellVirtualDirectory;
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x001E11D8 File Offset: 0x001DF3D8
		protected override void SetDefaultAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			virtualDirectory.BasicAuthentication = new bool?(true);
			virtualDirectory.WindowsAuthentication = new bool?(false);
			virtualDirectory.DigestAuthentication = new bool?(false);
			virtualDirectory.LiveIdBasicAuthentication = new bool?(false);
			virtualDirectory.LiveIdNegotiateAuthentication = new bool?(false);
			virtualDirectory.WSSecurityAuthentication = new bool?(false);
		}

		// Token: 0x060075C9 RID: 30153 RVA: 0x001E1230 File Offset: 0x001DF430
		protected override bool VerifyRoleConsistency()
		{
			if (base.Role == VirtualDirectoryRole.ClientAccess && !base.OwningServer.IsCafeServer)
			{
				base.WriteError(new ArgumentException("Argument: -Role ClientAccess"), ErrorCategory.InvalidArgument, null);
				return false;
			}
			if (base.Role == VirtualDirectoryRole.Mailbox && !base.OwningServer.IsHubTransportServer && !base.OwningServer.IsMailboxServer && !base.OwningServer.IsUnifiedMessagingServer && !base.OwningServer.IsFrontendTransportServer && !base.OwningServer.IsFfoWebServiceRole && !base.OwningServer.IsOSPRole && !base.OwningServer.IsClientAccessServer)
			{
				base.WriteError(new ArgumentException("Argument: -Role Mailbox"), ErrorCategory.InvalidArgument, null);
				return false;
			}
			return true;
		}

		// Token: 0x060075CA RID: 30154 RVA: 0x001E12DF File Offset: 0x001DF4DF
		protected override bool ShouldCreateVirtualDirectory()
		{
			base.ShouldCreateVirtualDirectory();
			return this.VerifyRoleConsistency();
		}

		// Token: 0x060075CB RID: 30155 RVA: 0x001E12F0 File Offset: 0x001DF4F0
		protected override void InternalProcessComplete()
		{
			base.InternalProcessComplete();
			this.DataObject.RequireSSL = new bool?(true);
			if (base.Fields["RequireSSL"] != null)
			{
				ExchangeServiceVDirHelper.SetSSLRequired(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError), Strings.ErrorUpdatingVDir(this.DataObject.MetabasePath, string.Empty), (bool)base.Fields["RequireSSL"]);
			}
		}

		// Token: 0x04003B95 RID: 15253
		private const string PowerShellFrontEndVDirPath = "FrontEnd\\HttpProxy\\PowerShell";

		// Token: 0x04003B96 RID: 15254
		private const string PowerShellBackEndVDirPath = "ClientAccess\\PowerShell";

		// Token: 0x04003B97 RID: 15255
		private const string PowerShellFrontEndDefaultAppPoolId = "MSExchangePowerShellFrontEndAppPool";

		// Token: 0x04003B98 RID: 15256
		private const string PowerShellBackEndDefaultAppPoolId = "MSExchangePowerShellAppPool";
	}
}
