using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000188 RID: 392
	public class SetSyncConfigCommand : SyntheticCommandWithPipelineInputNoOutput<SyncOrganization>
	{
		// Token: 0x0600230E RID: 8974 RVA: 0x00044FBC File Offset: 0x000431BC
		private SetSyncConfigCommand() : base("Set-SyncConfig")
		{
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00044FC9 File Offset: 0x000431C9
		public SetSyncConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x00044FD8 File Offset: 0x000431D8
		public virtual SetSyncConfigCommand SetParameters(SetSyncConfigCommand.FederatedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00044FE2 File Offset: 0x000431E2
		public virtual SetSyncConfigCommand SetParameters(SetSyncConfigCommand.ManagedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x00044FEC File Offset: 0x000431EC
		public virtual SetSyncConfigCommand SetParameters(SetSyncConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000189 RID: 393
		public class FederatedParameters : ParametersBase
		{
			// Token: 0x17000BB7 RID: 2999
			// (set) Token: 0x06002313 RID: 8979 RVA: 0x00044FF6 File Offset: 0x000431F6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000BB8 RID: 3000
			// (set) Token: 0x06002314 RID: 8980 RVA: 0x00045014 File Offset: 0x00043214
			public virtual string FederatedIdentitySourceADAttribute
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentitySourceADAttribute"] = value;
				}
			}

			// Token: 0x17000BB9 RID: 3001
			// (set) Token: 0x06002315 RID: 8981 RVA: 0x00045027 File Offset: 0x00043227
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BBA RID: 3002
			// (set) Token: 0x06002316 RID: 8982 RVA: 0x0004503A File Offset: 0x0004323A
			public virtual bool WlidUseSMTPPrimary
			{
				set
				{
					base.PowerSharpParameters["WlidUseSMTPPrimary"] = value;
				}
			}

			// Token: 0x17000BBB RID: 3003
			// (set) Token: 0x06002317 RID: 8983 RVA: 0x00045052 File Offset: 0x00043252
			public virtual SmtpDomainWithSubdomains ProvisioningDomain
			{
				set
				{
					base.PowerSharpParameters["ProvisioningDomain"] = value;
				}
			}

			// Token: 0x17000BBC RID: 3004
			// (set) Token: 0x06002318 RID: 8984 RVA: 0x00045065 File Offset: 0x00043265
			public virtual EnterpriseExchangeVersionEnum EnterpriseExchangeVersion
			{
				set
				{
					base.PowerSharpParameters["EnterpriseExchangeVersion"] = value;
				}
			}

			// Token: 0x17000BBD RID: 3005
			// (set) Token: 0x06002319 RID: 8985 RVA: 0x0004507D File Offset: 0x0004327D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BBE RID: 3006
			// (set) Token: 0x0600231A RID: 8986 RVA: 0x00045095 File Offset: 0x00043295
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BBF RID: 3007
			// (set) Token: 0x0600231B RID: 8987 RVA: 0x000450AD File Offset: 0x000432AD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BC0 RID: 3008
			// (set) Token: 0x0600231C RID: 8988 RVA: 0x000450C5 File Offset: 0x000432C5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000BC1 RID: 3009
			// (set) Token: 0x0600231D RID: 8989 RVA: 0x000450DD File Offset: 0x000432DD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200018A RID: 394
		public class ManagedParameters : ParametersBase
		{
			// Token: 0x17000BC2 RID: 3010
			// (set) Token: 0x0600231F RID: 8991 RVA: 0x000450FD File Offset: 0x000432FD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000BC3 RID: 3011
			// (set) Token: 0x06002320 RID: 8992 RVA: 0x0004511B File Offset: 0x0004331B
			public virtual bool DisableWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["DisableWindowsLiveID"] = value;
				}
			}

			// Token: 0x17000BC4 RID: 3012
			// (set) Token: 0x06002321 RID: 8993 RVA: 0x00045133 File Offset: 0x00043333
			public virtual string PasswordFilePath
			{
				set
				{
					base.PowerSharpParameters["PasswordFilePath"] = value;
				}
			}

			// Token: 0x17000BC5 RID: 3013
			// (set) Token: 0x06002322 RID: 8994 RVA: 0x00045146 File Offset: 0x00043346
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17000BC6 RID: 3014
			// (set) Token: 0x06002323 RID: 8995 RVA: 0x0004515E File Offset: 0x0004335E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BC7 RID: 3015
			// (set) Token: 0x06002324 RID: 8996 RVA: 0x00045171 File Offset: 0x00043371
			public virtual bool WlidUseSMTPPrimary
			{
				set
				{
					base.PowerSharpParameters["WlidUseSMTPPrimary"] = value;
				}
			}

			// Token: 0x17000BC8 RID: 3016
			// (set) Token: 0x06002325 RID: 8997 RVA: 0x00045189 File Offset: 0x00043389
			public virtual SmtpDomainWithSubdomains ProvisioningDomain
			{
				set
				{
					base.PowerSharpParameters["ProvisioningDomain"] = value;
				}
			}

			// Token: 0x17000BC9 RID: 3017
			// (set) Token: 0x06002326 RID: 8998 RVA: 0x0004519C File Offset: 0x0004339C
			public virtual EnterpriseExchangeVersionEnum EnterpriseExchangeVersion
			{
				set
				{
					base.PowerSharpParameters["EnterpriseExchangeVersion"] = value;
				}
			}

			// Token: 0x17000BCA RID: 3018
			// (set) Token: 0x06002327 RID: 8999 RVA: 0x000451B4 File Offset: 0x000433B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BCB RID: 3019
			// (set) Token: 0x06002328 RID: 9000 RVA: 0x000451CC File Offset: 0x000433CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BCC RID: 3020
			// (set) Token: 0x06002329 RID: 9001 RVA: 0x000451E4 File Offset: 0x000433E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BCD RID: 3021
			// (set) Token: 0x0600232A RID: 9002 RVA: 0x000451FC File Offset: 0x000433FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000BCE RID: 3022
			// (set) Token: 0x0600232B RID: 9003 RVA: 0x00045214 File Offset: 0x00043414
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200018B RID: 395
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000BCF RID: 3023
			// (set) Token: 0x0600232D RID: 9005 RVA: 0x00045234 File Offset: 0x00043434
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BD0 RID: 3024
			// (set) Token: 0x0600232E RID: 9006 RVA: 0x00045247 File Offset: 0x00043447
			public virtual bool WlidUseSMTPPrimary
			{
				set
				{
					base.PowerSharpParameters["WlidUseSMTPPrimary"] = value;
				}
			}

			// Token: 0x17000BD1 RID: 3025
			// (set) Token: 0x0600232F RID: 9007 RVA: 0x0004525F File Offset: 0x0004345F
			public virtual SmtpDomainWithSubdomains ProvisioningDomain
			{
				set
				{
					base.PowerSharpParameters["ProvisioningDomain"] = value;
				}
			}

			// Token: 0x17000BD2 RID: 3026
			// (set) Token: 0x06002330 RID: 9008 RVA: 0x00045272 File Offset: 0x00043472
			public virtual EnterpriseExchangeVersionEnum EnterpriseExchangeVersion
			{
				set
				{
					base.PowerSharpParameters["EnterpriseExchangeVersion"] = value;
				}
			}

			// Token: 0x17000BD3 RID: 3027
			// (set) Token: 0x06002331 RID: 9009 RVA: 0x0004528A File Offset: 0x0004348A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BD4 RID: 3028
			// (set) Token: 0x06002332 RID: 9010 RVA: 0x000452A2 File Offset: 0x000434A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BD5 RID: 3029
			// (set) Token: 0x06002333 RID: 9011 RVA: 0x000452BA File Offset: 0x000434BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BD6 RID: 3030
			// (set) Token: 0x06002334 RID: 9012 RVA: 0x000452D2 File Offset: 0x000434D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000BD7 RID: 3031
			// (set) Token: 0x06002335 RID: 9013 RVA: 0x000452EA File Offset: 0x000434EA
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
