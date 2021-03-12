using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000242 RID: 578
	public class GetPublicFolderStatisticsCommand : SyntheticCommandWithPipelineInput<PublicFolderStatistics, PublicFolderStatistics>
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x000500F8 File Offset: 0x0004E2F8
		private GetPublicFolderStatisticsCommand() : base("Get-PublicFolderStatistics")
		{
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x00050105 File Offset: 0x0004E305
		public GetPublicFolderStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x00050114 File Offset: 0x0004E314
		public virtual GetPublicFolderStatisticsCommand SetParameters(GetPublicFolderStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0005011E File Offset: 0x0004E31E
		public virtual GetPublicFolderStatisticsCommand SetParameters(GetPublicFolderStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000243 RID: 579
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001296 RID: 4758
			// (set) Token: 0x06002B65 RID: 11109 RVA: 0x00050128 File Offset: 0x0004E328
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001297 RID: 4759
			// (set) Token: 0x06002B66 RID: 11110 RVA: 0x00050146 File Offset: 0x0004E346
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001298 RID: 4760
			// (set) Token: 0x06002B67 RID: 11111 RVA: 0x00050164 File Offset: 0x0004E364
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001299 RID: 4761
			// (set) Token: 0x06002B68 RID: 11112 RVA: 0x00050182 File Offset: 0x0004E382
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700129A RID: 4762
			// (set) Token: 0x06002B69 RID: 11113 RVA: 0x0005019A File Offset: 0x0004E39A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700129B RID: 4763
			// (set) Token: 0x06002B6A RID: 11114 RVA: 0x000501AD File Offset: 0x0004E3AD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700129C RID: 4764
			// (set) Token: 0x06002B6B RID: 11115 RVA: 0x000501C5 File Offset: 0x0004E3C5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700129D RID: 4765
			// (set) Token: 0x06002B6C RID: 11116 RVA: 0x000501DD File Offset: 0x0004E3DD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700129E RID: 4766
			// (set) Token: 0x06002B6D RID: 11117 RVA: 0x000501F5 File Offset: 0x0004E3F5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000244 RID: 580
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700129F RID: 4767
			// (set) Token: 0x06002B6F RID: 11119 RVA: 0x00050215 File Offset: 0x0004E415
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170012A0 RID: 4768
			// (set) Token: 0x06002B70 RID: 11120 RVA: 0x00050233 File Offset: 0x0004E433
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170012A1 RID: 4769
			// (set) Token: 0x06002B71 RID: 11121 RVA: 0x00050251 File Offset: 0x0004E451
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170012A2 RID: 4770
			// (set) Token: 0x06002B72 RID: 11122 RVA: 0x00050269 File Offset: 0x0004E469
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170012A3 RID: 4771
			// (set) Token: 0x06002B73 RID: 11123 RVA: 0x0005027C File Offset: 0x0004E47C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170012A4 RID: 4772
			// (set) Token: 0x06002B74 RID: 11124 RVA: 0x00050294 File Offset: 0x0004E494
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170012A5 RID: 4773
			// (set) Token: 0x06002B75 RID: 11125 RVA: 0x000502AC File Offset: 0x0004E4AC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170012A6 RID: 4774
			// (set) Token: 0x06002B76 RID: 11126 RVA: 0x000502C4 File Offset: 0x0004E4C4
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
