using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000855 RID: 2133
	public class NewRemoteAccountPolicyCommand : SyntheticCommandWithPipelineInput<RemoteAccountPolicy, RemoteAccountPolicy>
	{
		// Token: 0x06006A11 RID: 27153 RVA: 0x000A10EA File Offset: 0x0009F2EA
		private NewRemoteAccountPolicyCommand() : base("New-RemoteAccountPolicy")
		{
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x000A10F7 File Offset: 0x0009F2F7
		public NewRemoteAccountPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x000A1106 File Offset: 0x0009F306
		public virtual NewRemoteAccountPolicyCommand SetParameters(NewRemoteAccountPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000856 RID: 2134
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004520 RID: 17696
			// (set) Token: 0x06006A14 RID: 27156 RVA: 0x000A1110 File Offset: 0x0009F310
			public virtual EnhancedTimeSpan PollingInterval
			{
				set
				{
					base.PowerSharpParameters["PollingInterval"] = value;
				}
			}

			// Token: 0x17004521 RID: 17697
			// (set) Token: 0x06006A15 RID: 27157 RVA: 0x000A1128 File Offset: 0x0009F328
			public virtual EnhancedTimeSpan TimeBeforeInactive
			{
				set
				{
					base.PowerSharpParameters["TimeBeforeInactive"] = value;
				}
			}

			// Token: 0x17004522 RID: 17698
			// (set) Token: 0x06006A16 RID: 27158 RVA: 0x000A1140 File Offset: 0x0009F340
			public virtual EnhancedTimeSpan TimeBeforeDormant
			{
				set
				{
					base.PowerSharpParameters["TimeBeforeDormant"] = value;
				}
			}

			// Token: 0x17004523 RID: 17699
			// (set) Token: 0x06006A17 RID: 27159 RVA: 0x000A1158 File Offset: 0x0009F358
			public virtual int MaxSyncAccounts
			{
				set
				{
					base.PowerSharpParameters["MaxSyncAccounts"] = value;
				}
			}

			// Token: 0x17004524 RID: 17700
			// (set) Token: 0x06006A18 RID: 27160 RVA: 0x000A1170 File Offset: 0x0009F370
			public virtual bool SyncNowAllowed
			{
				set
				{
					base.PowerSharpParameters["SyncNowAllowed"] = value;
				}
			}

			// Token: 0x17004525 RID: 17701
			// (set) Token: 0x06006A19 RID: 27161 RVA: 0x000A1188 File Offset: 0x0009F388
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004526 RID: 17702
			// (set) Token: 0x06006A1A RID: 27162 RVA: 0x000A11A6 File Offset: 0x0009F3A6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004527 RID: 17703
			// (set) Token: 0x06006A1B RID: 27163 RVA: 0x000A11B9 File Offset: 0x0009F3B9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004528 RID: 17704
			// (set) Token: 0x06006A1C RID: 27164 RVA: 0x000A11CC File Offset: 0x0009F3CC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004529 RID: 17705
			// (set) Token: 0x06006A1D RID: 27165 RVA: 0x000A11E4 File Offset: 0x0009F3E4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700452A RID: 17706
			// (set) Token: 0x06006A1E RID: 27166 RVA: 0x000A11FC File Offset: 0x0009F3FC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700452B RID: 17707
			// (set) Token: 0x06006A1F RID: 27167 RVA: 0x000A1214 File Offset: 0x0009F414
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700452C RID: 17708
			// (set) Token: 0x06006A20 RID: 27168 RVA: 0x000A122C File Offset: 0x0009F42C
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
