using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006AB RID: 1707
	public class SetSharingPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<SharingPolicy>
	{
		// Token: 0x06005A2F RID: 23087 RVA: 0x0008CC1F File Offset: 0x0008AE1F
		private SetSharingPolicyCommand() : base("Set-SharingPolicy")
		{
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x0008CC2C File Offset: 0x0008AE2C
		public SetSharingPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x0008CC3B File Offset: 0x0008AE3B
		public virtual SetSharingPolicyCommand SetParameters(SetSharingPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x0008CC45 File Offset: 0x0008AE45
		public virtual SetSharingPolicyCommand SetParameters(SetSharingPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006AC RID: 1708
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003892 RID: 14482
			// (set) Token: 0x06005A33 RID: 23091 RVA: 0x0008CC4F File Offset: 0x0008AE4F
			public virtual MultiValuedProperty<SharingPolicyDomain> Domains
			{
				set
				{
					base.PowerSharpParameters["Domains"] = value;
				}
			}

			// Token: 0x17003893 RID: 14483
			// (set) Token: 0x06005A34 RID: 23092 RVA: 0x0008CC62 File Offset: 0x0008AE62
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003894 RID: 14484
			// (set) Token: 0x06005A35 RID: 23093 RVA: 0x0008CC7A File Offset: 0x0008AE7A
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17003895 RID: 14485
			// (set) Token: 0x06005A36 RID: 23094 RVA: 0x0008CC92 File Offset: 0x0008AE92
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003896 RID: 14486
			// (set) Token: 0x06005A37 RID: 23095 RVA: 0x0008CCA5 File Offset: 0x0008AEA5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003897 RID: 14487
			// (set) Token: 0x06005A38 RID: 23096 RVA: 0x0008CCB8 File Offset: 0x0008AEB8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003898 RID: 14488
			// (set) Token: 0x06005A39 RID: 23097 RVA: 0x0008CCD0 File Offset: 0x0008AED0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003899 RID: 14489
			// (set) Token: 0x06005A3A RID: 23098 RVA: 0x0008CCE8 File Offset: 0x0008AEE8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700389A RID: 14490
			// (set) Token: 0x06005A3B RID: 23099 RVA: 0x0008CD00 File Offset: 0x0008AF00
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700389B RID: 14491
			// (set) Token: 0x06005A3C RID: 23100 RVA: 0x0008CD18 File Offset: 0x0008AF18
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006AD RID: 1709
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700389C RID: 14492
			// (set) Token: 0x06005A3E RID: 23102 RVA: 0x0008CD38 File Offset: 0x0008AF38
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700389D RID: 14493
			// (set) Token: 0x06005A3F RID: 23103 RVA: 0x0008CD56 File Offset: 0x0008AF56
			public virtual MultiValuedProperty<SharingPolicyDomain> Domains
			{
				set
				{
					base.PowerSharpParameters["Domains"] = value;
				}
			}

			// Token: 0x1700389E RID: 14494
			// (set) Token: 0x06005A40 RID: 23104 RVA: 0x0008CD69 File Offset: 0x0008AF69
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700389F RID: 14495
			// (set) Token: 0x06005A41 RID: 23105 RVA: 0x0008CD81 File Offset: 0x0008AF81
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x170038A0 RID: 14496
			// (set) Token: 0x06005A42 RID: 23106 RVA: 0x0008CD99 File Offset: 0x0008AF99
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170038A1 RID: 14497
			// (set) Token: 0x06005A43 RID: 23107 RVA: 0x0008CDAC File Offset: 0x0008AFAC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170038A2 RID: 14498
			// (set) Token: 0x06005A44 RID: 23108 RVA: 0x0008CDBF File Offset: 0x0008AFBF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038A3 RID: 14499
			// (set) Token: 0x06005A45 RID: 23109 RVA: 0x0008CDD7 File Offset: 0x0008AFD7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038A4 RID: 14500
			// (set) Token: 0x06005A46 RID: 23110 RVA: 0x0008CDEF File Offset: 0x0008AFEF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038A5 RID: 14501
			// (set) Token: 0x06005A47 RID: 23111 RVA: 0x0008CE07 File Offset: 0x0008B007
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170038A6 RID: 14502
			// (set) Token: 0x06005A48 RID: 23112 RVA: 0x0008CE1F File Offset: 0x0008B01F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
