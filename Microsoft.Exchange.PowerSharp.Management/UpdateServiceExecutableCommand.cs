using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000D3 RID: 211
	public class UpdateServiceExecutableCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06001D08 RID: 7432 RVA: 0x0003D691 File Offset: 0x0003B891
		private UpdateServiceExecutableCommand() : base("Update-ServiceExecutable")
		{
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0003D69E File Offset: 0x0003B89E
		public UpdateServiceExecutableCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0003D6AD File Offset: 0x0003B8AD
		public virtual UpdateServiceExecutableCommand SetParameters(UpdateServiceExecutableCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000D4 RID: 212
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700071B RID: 1819
			// (set) Token: 0x06001D0B RID: 7435 RVA: 0x0003D6B7 File Offset: 0x0003B8B7
			public virtual string ServiceName
			{
				set
				{
					base.PowerSharpParameters["ServiceName"] = value;
				}
			}

			// Token: 0x1700071C RID: 1820
			// (set) Token: 0x06001D0C RID: 7436 RVA: 0x0003D6CA File Offset: 0x0003B8CA
			public virtual string Executable
			{
				set
				{
					base.PowerSharpParameters["Executable"] = value;
				}
			}

			// Token: 0x1700071D RID: 1821
			// (set) Token: 0x06001D0D RID: 7437 RVA: 0x0003D6DD File Offset: 0x0003B8DD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700071E RID: 1822
			// (set) Token: 0x06001D0E RID: 7438 RVA: 0x0003D6F5 File Offset: 0x0003B8F5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700071F RID: 1823
			// (set) Token: 0x06001D0F RID: 7439 RVA: 0x0003D70D File Offset: 0x0003B90D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000720 RID: 1824
			// (set) Token: 0x06001D10 RID: 7440 RVA: 0x0003D725 File Offset: 0x0003B925
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
