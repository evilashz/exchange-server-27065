using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C2B RID: 3115
	[Cmdlet("New", "PswsVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewPswsVirtualDirectory : NewPowerShellCommonVirtualDirectory<ADPswsVirtualDirectory>
	{
		// Token: 0x1700243F RID: 9279
		// (get) Token: 0x060075CD RID: 30157 RVA: 0x001E136F File Offset: 0x001DF56F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewPswsVirtualDirectory(base.Name, base.Server.ToString());
			}
		}

		// Token: 0x17002440 RID: 9280
		// (get) Token: 0x060075CE RID: 30158 RVA: 0x001E1387 File Offset: 0x001DF587
		protected override string VirtualDirectoryPath
		{
			get
			{
				if (base.Role == VirtualDirectoryRole.ClientAccess)
				{
					return "FrontEnd\\HttpProxy\\Psws";
				}
				return "ClientAccess\\Psws";
			}
		}

		// Token: 0x17002441 RID: 9281
		// (get) Token: 0x060075CF RID: 30159 RVA: 0x001E139C File Offset: 0x001DF59C
		protected override string DefaultApplicationPoolId
		{
			get
			{
				if (base.Role == VirtualDirectoryRole.ClientAccess)
				{
					return "MSExchangePswsFrontEndAppPool";
				}
				return "MSExchangePswsAppPool";
			}
		}

		// Token: 0x060075D0 RID: 30160 RVA: 0x001E13B4 File Offset: 0x001DF5B4
		protected override IConfigurable PrepareDataObject()
		{
			ADPswsVirtualDirectory adpswsVirtualDirectory = (ADPswsVirtualDirectory)base.PrepareDataObject();
			adpswsVirtualDirectory.VirtualDirectoryType = PowerShellVirtualDirectoryType.Psws;
			return adpswsVirtualDirectory;
		}

		// Token: 0x060075D1 RID: 30161 RVA: 0x001E13D5 File Offset: 0x001DF5D5
		protected override void SetDefaultAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			virtualDirectory.BasicAuthentication = new bool?(false);
			virtualDirectory.WindowsAuthentication = new bool?(false);
		}

		// Token: 0x060075D2 RID: 30162 RVA: 0x001E13F0 File Offset: 0x001DF5F0
		protected override bool ShouldCreateVirtualDirectory()
		{
			bool flag = base.ShouldCreateVirtualDirectory();
			if (!flag && base.OwningServer.IsMailboxServer)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060075D3 RID: 30163 RVA: 0x001E1417 File Offset: 0x001DF617
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (ExchangeServiceVDirHelper.IsBackEndVirtualDirectory(this.DataObject))
			{
				ExchangeServiceVDirHelper.ForceAnonymous(this.DataObject.MetabasePath);
			}
		}

		// Token: 0x04003B99 RID: 15257
		private const string PswsFrontEndVDirPath = "FrontEnd\\HttpProxy\\Psws";

		// Token: 0x04003B9A RID: 15258
		private const string PswsBackEndVDirPath = "ClientAccess\\Psws";

		// Token: 0x04003B9B RID: 15259
		private const string PswsFrontEndDefaultAppPoolId = "MSExchangePswsFrontEndAppPool";

		// Token: 0x04003B9C RID: 15260
		private const string PswsBackEndDefaultAppPoolId = "MSExchangePswsAppPool";
	}
}
