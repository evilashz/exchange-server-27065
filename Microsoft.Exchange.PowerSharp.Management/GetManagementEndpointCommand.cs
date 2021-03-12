using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ManagementEndpoint;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000206 RID: 518
	public class GetManagementEndpointCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x0600292E RID: 10542 RVA: 0x0004D37A File Offset: 0x0004B57A
		private GetManagementEndpointCommand() : base("Get-ManagementEndpoint")
		{
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0004D387 File Offset: 0x0004B587
		public GetManagementEndpointCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x0004D396 File Offset: 0x0004B596
		public virtual GetManagementEndpointCommand SetParameters(GetManagementEndpointCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000207 RID: 519
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170010DB RID: 4315
			// (set) Token: 0x06002931 RID: 10545 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170010DC RID: 4316
			// (set) Token: 0x06002932 RID: 10546 RVA: 0x0004D3B3 File Offset: 0x0004B5B3
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x170010DD RID: 4317
			// (set) Token: 0x06002933 RID: 10547 RVA: 0x0004D3CB File Offset: 0x0004B5CB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010DE RID: 4318
			// (set) Token: 0x06002934 RID: 10548 RVA: 0x0004D3E3 File Offset: 0x0004B5E3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010DF RID: 4319
			// (set) Token: 0x06002935 RID: 10549 RVA: 0x0004D3FB File Offset: 0x0004B5FB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010E0 RID: 4320
			// (set) Token: 0x06002936 RID: 10550 RVA: 0x0004D413 File Offset: 0x0004B613
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
