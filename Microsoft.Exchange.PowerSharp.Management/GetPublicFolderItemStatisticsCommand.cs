using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200023C RID: 572
	public class GetPublicFolderItemStatisticsCommand : SyntheticCommandWithPipelineInput<PublicFolderItemStatistics, PublicFolderItemStatistics>
	{
		// Token: 0x06002B37 RID: 11063 RVA: 0x0004FDA4 File Offset: 0x0004DFA4
		private GetPublicFolderItemStatisticsCommand() : base("Get-PublicFolderItemStatistics")
		{
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x0004FDB1 File Offset: 0x0004DFB1
		public GetPublicFolderItemStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x0004FDC0 File Offset: 0x0004DFC0
		public virtual GetPublicFolderItemStatisticsCommand SetParameters(GetPublicFolderItemStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x0004FDCA File Offset: 0x0004DFCA
		public virtual GetPublicFolderItemStatisticsCommand SetParameters(GetPublicFolderItemStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200023D RID: 573
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001278 RID: 4728
			// (set) Token: 0x06002B3B RID: 11067 RVA: 0x0004FDD4 File Offset: 0x0004DFD4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001279 RID: 4729
			// (set) Token: 0x06002B3C RID: 11068 RVA: 0x0004FDF2 File Offset: 0x0004DFF2
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700127A RID: 4730
			// (set) Token: 0x06002B3D RID: 11069 RVA: 0x0004FE10 File Offset: 0x0004E010
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700127B RID: 4731
			// (set) Token: 0x06002B3E RID: 11070 RVA: 0x0004FE23 File Offset: 0x0004E023
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700127C RID: 4732
			// (set) Token: 0x06002B3F RID: 11071 RVA: 0x0004FE3B File Offset: 0x0004E03B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700127D RID: 4733
			// (set) Token: 0x06002B40 RID: 11072 RVA: 0x0004FE53 File Offset: 0x0004E053
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700127E RID: 4734
			// (set) Token: 0x06002B41 RID: 11073 RVA: 0x0004FE6B File Offset: 0x0004E06B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200023E RID: 574
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700127F RID: 4735
			// (set) Token: 0x06002B43 RID: 11075 RVA: 0x0004FE8B File Offset: 0x0004E08B
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001280 RID: 4736
			// (set) Token: 0x06002B44 RID: 11076 RVA: 0x0004FEA9 File Offset: 0x0004E0A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001281 RID: 4737
			// (set) Token: 0x06002B45 RID: 11077 RVA: 0x0004FEBC File Offset: 0x0004E0BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001282 RID: 4738
			// (set) Token: 0x06002B46 RID: 11078 RVA: 0x0004FED4 File Offset: 0x0004E0D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001283 RID: 4739
			// (set) Token: 0x06002B47 RID: 11079 RVA: 0x0004FEEC File Offset: 0x0004E0EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001284 RID: 4740
			// (set) Token: 0x06002B48 RID: 11080 RVA: 0x0004FF04 File Offset: 0x0004E104
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
