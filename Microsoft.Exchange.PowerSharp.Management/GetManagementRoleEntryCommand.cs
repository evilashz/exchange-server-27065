using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200035F RID: 863
	public class GetManagementRoleEntryCommand : SyntheticCommandWithPipelineInput<ExchangeRoleEntryPresentation, ExchangeRoleEntryPresentation>
	{
		// Token: 0x0600373B RID: 14139 RVA: 0x0005F871 File Offset: 0x0005DA71
		private GetManagementRoleEntryCommand() : base("Get-ManagementRoleEntry")
		{
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x0005F87E File Offset: 0x0005DA7E
		public GetManagementRoleEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x0005F88D File Offset: 0x0005DA8D
		public virtual GetManagementRoleEntryCommand SetParameters(GetManagementRoleEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000360 RID: 864
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001C36 RID: 7222
			// (set) Token: 0x0600373E RID: 14142 RVA: 0x0005F897 File Offset: 0x0005DA97
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleEntryIdParameter(value) : null);
				}
			}

			// Token: 0x17001C37 RID: 7223
			// (set) Token: 0x0600373F RID: 14143 RVA: 0x0005F8B5 File Offset: 0x0005DAB5
			public virtual string Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17001C38 RID: 7224
			// (set) Token: 0x06003740 RID: 14144 RVA: 0x0005F8C8 File Offset: 0x0005DAC8
			public virtual string PSSnapinName
			{
				set
				{
					base.PowerSharpParameters["PSSnapinName"] = value;
				}
			}

			// Token: 0x17001C39 RID: 7225
			// (set) Token: 0x06003741 RID: 14145 RVA: 0x0005F8DB File Offset: 0x0005DADB
			public virtual ManagementRoleEntryType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17001C3A RID: 7226
			// (set) Token: 0x06003742 RID: 14146 RVA: 0x0005F8F3 File Offset: 0x0005DAF3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C3B RID: 7227
			// (set) Token: 0x06003743 RID: 14147 RVA: 0x0005F906 File Offset: 0x0005DB06
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C3C RID: 7228
			// (set) Token: 0x06003744 RID: 14148 RVA: 0x0005F91E File Offset: 0x0005DB1E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C3D RID: 7229
			// (set) Token: 0x06003745 RID: 14149 RVA: 0x0005F936 File Offset: 0x0005DB36
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C3E RID: 7230
			// (set) Token: 0x06003746 RID: 14150 RVA: 0x0005F94E File Offset: 0x0005DB4E
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
