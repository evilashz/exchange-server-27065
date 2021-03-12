using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200070F RID: 1807
	public class GetHostedContentFilterPolicyCommand : SyntheticCommandWithPipelineInput<HostedContentFilterPolicy, HostedContentFilterPolicy>
	{
		// Token: 0x06005D39 RID: 23865 RVA: 0x00090931 File Offset: 0x0008EB31
		private GetHostedContentFilterPolicyCommand() : base("Get-HostedContentFilterPolicy")
		{
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x0009093E File Offset: 0x0008EB3E
		public GetHostedContentFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x0009094D File Offset: 0x0008EB4D
		public virtual GetHostedContentFilterPolicyCommand SetParameters(GetHostedContentFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x00090957 File Offset: 0x0008EB57
		public virtual GetHostedContentFilterPolicyCommand SetParameters(GetHostedContentFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000710 RID: 1808
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003AD4 RID: 15060
			// (set) Token: 0x06005D3D RID: 23869 RVA: 0x00090961 File Offset: 0x0008EB61
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003AD5 RID: 15061
			// (set) Token: 0x06005D3E RID: 23870 RVA: 0x00090979 File Offset: 0x0008EB79
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003AD6 RID: 15062
			// (set) Token: 0x06005D3F RID: 23871 RVA: 0x00090997 File Offset: 0x0008EB97
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003AD7 RID: 15063
			// (set) Token: 0x06005D40 RID: 23872 RVA: 0x000909AA File Offset: 0x0008EBAA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003AD8 RID: 15064
			// (set) Token: 0x06005D41 RID: 23873 RVA: 0x000909C2 File Offset: 0x0008EBC2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003AD9 RID: 15065
			// (set) Token: 0x06005D42 RID: 23874 RVA: 0x000909DA File Offset: 0x0008EBDA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003ADA RID: 15066
			// (set) Token: 0x06005D43 RID: 23875 RVA: 0x000909F2 File Offset: 0x0008EBF2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000711 RID: 1809
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003ADB RID: 15067
			// (set) Token: 0x06005D45 RID: 23877 RVA: 0x00090A12 File Offset: 0x0008EC12
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedContentFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003ADC RID: 15068
			// (set) Token: 0x06005D46 RID: 23878 RVA: 0x00090A30 File Offset: 0x0008EC30
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003ADD RID: 15069
			// (set) Token: 0x06005D47 RID: 23879 RVA: 0x00090A48 File Offset: 0x0008EC48
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003ADE RID: 15070
			// (set) Token: 0x06005D48 RID: 23880 RVA: 0x00090A66 File Offset: 0x0008EC66
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003ADF RID: 15071
			// (set) Token: 0x06005D49 RID: 23881 RVA: 0x00090A79 File Offset: 0x0008EC79
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003AE0 RID: 15072
			// (set) Token: 0x06005D4A RID: 23882 RVA: 0x00090A91 File Offset: 0x0008EC91
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003AE1 RID: 15073
			// (set) Token: 0x06005D4B RID: 23883 RVA: 0x00090AA9 File Offset: 0x0008ECA9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003AE2 RID: 15074
			// (set) Token: 0x06005D4C RID: 23884 RVA: 0x00090AC1 File Offset: 0x0008ECC1
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
