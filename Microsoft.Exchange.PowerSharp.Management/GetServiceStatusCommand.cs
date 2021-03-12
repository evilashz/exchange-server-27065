using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002C5 RID: 709
	public class GetServiceStatusCommand : SyntheticCommandWithPipelineInput<OrganizationIdParameter, OrganizationIdParameter>
	{
		// Token: 0x06003154 RID: 12628 RVA: 0x00057F23 File Offset: 0x00056123
		private GetServiceStatusCommand() : base("Get-ServiceStatus")
		{
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x00057F30 File Offset: 0x00056130
		public GetServiceStatusCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00057F3F File Offset: 0x0005613F
		public virtual GetServiceStatusCommand SetParameters(GetServiceStatusCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002C6 RID: 710
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001783 RID: 6019
			// (set) Token: 0x06003157 RID: 12631 RVA: 0x00057F49 File Offset: 0x00056149
			public virtual uint MaintenanceWindowDays
			{
				set
				{
					base.PowerSharpParameters["MaintenanceWindowDays"] = value;
				}
			}

			// Token: 0x17001784 RID: 6020
			// (set) Token: 0x06003158 RID: 12632 RVA: 0x00057F61 File Offset: 0x00056161
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001785 RID: 6021
			// (set) Token: 0x06003159 RID: 12633 RVA: 0x00057F7F File Offset: 0x0005617F
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001786 RID: 6022
			// (set) Token: 0x0600315A RID: 12634 RVA: 0x00057F92 File Offset: 0x00056192
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x17001787 RID: 6023
			// (set) Token: 0x0600315B RID: 12635 RVA: 0x00057FA5 File Offset: 0x000561A5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001788 RID: 6024
			// (set) Token: 0x0600315C RID: 12636 RVA: 0x00057FB8 File Offset: 0x000561B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001789 RID: 6025
			// (set) Token: 0x0600315D RID: 12637 RVA: 0x00057FD0 File Offset: 0x000561D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700178A RID: 6026
			// (set) Token: 0x0600315E RID: 12638 RVA: 0x00057FE8 File Offset: 0x000561E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700178B RID: 6027
			// (set) Token: 0x0600315F RID: 12639 RVA: 0x00058000 File Offset: 0x00056200
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
