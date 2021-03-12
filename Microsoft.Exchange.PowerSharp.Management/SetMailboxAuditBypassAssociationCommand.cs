using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000190 RID: 400
	public class SetMailboxAuditBypassAssociationCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxAuditBypassAssociation>
	{
		// Token: 0x0600235E RID: 9054 RVA: 0x00045628 File Offset: 0x00043828
		private SetMailboxAuditBypassAssociationCommand() : base("Set-MailboxAuditBypassAssociation")
		{
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00045635 File Offset: 0x00043835
		public SetMailboxAuditBypassAssociationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00045644 File Offset: 0x00043844
		public virtual SetMailboxAuditBypassAssociationCommand SetParameters(SetMailboxAuditBypassAssociationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0004564E File Offset: 0x0004384E
		public virtual SetMailboxAuditBypassAssociationCommand SetParameters(SetMailboxAuditBypassAssociationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000191 RID: 401
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000BF7 RID: 3063
			// (set) Token: 0x06002362 RID: 9058 RVA: 0x00045658 File Offset: 0x00043858
			public virtual bool AuditBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditBypassEnabled"] = value;
				}
			}

			// Token: 0x17000BF8 RID: 3064
			// (set) Token: 0x06002363 RID: 9059 RVA: 0x00045670 File Offset: 0x00043870
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BF9 RID: 3065
			// (set) Token: 0x06002364 RID: 9060 RVA: 0x00045683 File Offset: 0x00043883
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BFA RID: 3066
			// (set) Token: 0x06002365 RID: 9061 RVA: 0x0004569B File Offset: 0x0004389B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BFB RID: 3067
			// (set) Token: 0x06002366 RID: 9062 RVA: 0x000456B3 File Offset: 0x000438B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BFC RID: 3068
			// (set) Token: 0x06002367 RID: 9063 RVA: 0x000456CB File Offset: 0x000438CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000BFD RID: 3069
			// (set) Token: 0x06002368 RID: 9064 RVA: 0x000456E3 File Offset: 0x000438E3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000192 RID: 402
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000BFE RID: 3070
			// (set) Token: 0x0600236A RID: 9066 RVA: 0x00045703 File Offset: 0x00043903
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxAuditBypassAssociationIdParameter(value) : null);
				}
			}

			// Token: 0x17000BFF RID: 3071
			// (set) Token: 0x0600236B RID: 9067 RVA: 0x00045721 File Offset: 0x00043921
			public virtual bool AuditBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditBypassEnabled"] = value;
				}
			}

			// Token: 0x17000C00 RID: 3072
			// (set) Token: 0x0600236C RID: 9068 RVA: 0x00045739 File Offset: 0x00043939
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C01 RID: 3073
			// (set) Token: 0x0600236D RID: 9069 RVA: 0x0004574C File Offset: 0x0004394C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C02 RID: 3074
			// (set) Token: 0x0600236E RID: 9070 RVA: 0x00045764 File Offset: 0x00043964
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C03 RID: 3075
			// (set) Token: 0x0600236F RID: 9071 RVA: 0x0004577C File Offset: 0x0004397C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C04 RID: 3076
			// (set) Token: 0x06002370 RID: 9072 RVA: 0x00045794 File Offset: 0x00043994
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000C05 RID: 3077
			// (set) Token: 0x06002371 RID: 9073 RVA: 0x000457AC File Offset: 0x000439AC
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
