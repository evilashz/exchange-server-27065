using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009F3 RID: 2547
	public class SetMergeRequestCommand : SyntheticCommandWithPipelineInputNoOutput<MergeRequestIdParameter>
	{
		// Token: 0x06007FE3 RID: 32739 RVA: 0x000BDD57 File Offset: 0x000BBF57
		private SetMergeRequestCommand() : base("Set-MergeRequest")
		{
		}

		// Token: 0x06007FE4 RID: 32740 RVA: 0x000BDD64 File Offset: 0x000BBF64
		public SetMergeRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x000BDD73 File Offset: 0x000BBF73
		public virtual SetMergeRequestCommand SetParameters(SetMergeRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x000BDD7D File Offset: 0x000BBF7D
		public virtual SetMergeRequestCommand SetParameters(SetMergeRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007FE7 RID: 32743 RVA: 0x000BDD87 File Offset: 0x000BBF87
		public virtual SetMergeRequestCommand SetParameters(SetMergeRequestCommand.RehomeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009F4 RID: 2548
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170057B6 RID: 22454
			// (set) Token: 0x06007FE8 RID: 32744 RVA: 0x000BDD91 File Offset: 0x000BBF91
			public virtual string RemoteSourceMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteSourceMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x170057B7 RID: 22455
			// (set) Token: 0x06007FE9 RID: 32745 RVA: 0x000BDDA4 File Offset: 0x000BBFA4
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x170057B8 RID: 22456
			// (set) Token: 0x06007FEA RID: 32746 RVA: 0x000BDDB7 File Offset: 0x000BBFB7
			public virtual bool IsAdministrativeCredential
			{
				set
				{
					base.PowerSharpParameters["IsAdministrativeCredential"] = value;
				}
			}

			// Token: 0x170057B9 RID: 22457
			// (set) Token: 0x06007FEB RID: 32747 RVA: 0x000BDDCF File Offset: 0x000BBFCF
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x170057BA RID: 22458
			// (set) Token: 0x06007FEC RID: 32748 RVA: 0x000BDDE7 File Offset: 0x000BBFE7
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x170057BB RID: 22459
			// (set) Token: 0x06007FED RID: 32749 RVA: 0x000BDDFA File Offset: 0x000BBFFA
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170057BC RID: 22460
			// (set) Token: 0x06007FEE RID: 32750 RVA: 0x000BDE12 File Offset: 0x000BC012
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x170057BD RID: 22461
			// (set) Token: 0x06007FEF RID: 32751 RVA: 0x000BDE2A File Offset: 0x000BC02A
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170057BE RID: 22462
			// (set) Token: 0x06007FF0 RID: 32752 RVA: 0x000BDE42 File Offset: 0x000BC042
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170057BF RID: 22463
			// (set) Token: 0x06007FF1 RID: 32753 RVA: 0x000BDE55 File Offset: 0x000BC055
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170057C0 RID: 22464
			// (set) Token: 0x06007FF2 RID: 32754 RVA: 0x000BDE6D File Offset: 0x000BC06D
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170057C1 RID: 22465
			// (set) Token: 0x06007FF3 RID: 32755 RVA: 0x000BDE85 File Offset: 0x000BC085
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x170057C2 RID: 22466
			// (set) Token: 0x06007FF4 RID: 32756 RVA: 0x000BDE9D File Offset: 0x000BC09D
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170057C3 RID: 22467
			// (set) Token: 0x06007FF5 RID: 32757 RVA: 0x000BDEB5 File Offset: 0x000BC0B5
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x170057C4 RID: 22468
			// (set) Token: 0x06007FF6 RID: 32758 RVA: 0x000BDECD File Offset: 0x000BC0CD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MergeRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170057C5 RID: 22469
			// (set) Token: 0x06007FF7 RID: 32759 RVA: 0x000BDEEB File Offset: 0x000BC0EB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057C6 RID: 22470
			// (set) Token: 0x06007FF8 RID: 32760 RVA: 0x000BDEFE File Offset: 0x000BC0FE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057C7 RID: 22471
			// (set) Token: 0x06007FF9 RID: 32761 RVA: 0x000BDF16 File Offset: 0x000BC116
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057C8 RID: 22472
			// (set) Token: 0x06007FFA RID: 32762 RVA: 0x000BDF2E File Offset: 0x000BC12E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057C9 RID: 22473
			// (set) Token: 0x06007FFB RID: 32763 RVA: 0x000BDF46 File Offset: 0x000BC146
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057CA RID: 22474
			// (set) Token: 0x06007FFC RID: 32764 RVA: 0x000BDF5E File Offset: 0x000BC15E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009F5 RID: 2549
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170057CB RID: 22475
			// (set) Token: 0x06007FFE RID: 32766 RVA: 0x000BDF7E File Offset: 0x000BC17E
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x170057CC RID: 22476
			// (set) Token: 0x06007FFF RID: 32767 RVA: 0x000BDF96 File Offset: 0x000BC196
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MergeRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170057CD RID: 22477
			// (set) Token: 0x06008000 RID: 32768 RVA: 0x000BDFB4 File Offset: 0x000BC1B4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057CE RID: 22478
			// (set) Token: 0x06008001 RID: 32769 RVA: 0x000BDFC7 File Offset: 0x000BC1C7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057CF RID: 22479
			// (set) Token: 0x06008002 RID: 32770 RVA: 0x000BDFDF File Offset: 0x000BC1DF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057D0 RID: 22480
			// (set) Token: 0x06008003 RID: 32771 RVA: 0x000BDFF7 File Offset: 0x000BC1F7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057D1 RID: 22481
			// (set) Token: 0x06008004 RID: 32772 RVA: 0x000BE00F File Offset: 0x000BC20F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057D2 RID: 22482
			// (set) Token: 0x06008005 RID: 32773 RVA: 0x000BE027 File Offset: 0x000BC227
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009F6 RID: 2550
		public class RehomeParameters : ParametersBase
		{
			// Token: 0x170057D3 RID: 22483
			// (set) Token: 0x06008007 RID: 32775 RVA: 0x000BE047 File Offset: 0x000BC247
			public virtual SwitchParameter RehomeRequest
			{
				set
				{
					base.PowerSharpParameters["RehomeRequest"] = value;
				}
			}

			// Token: 0x170057D4 RID: 22484
			// (set) Token: 0x06008008 RID: 32776 RVA: 0x000BE05F File Offset: 0x000BC25F
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x170057D5 RID: 22485
			// (set) Token: 0x06008009 RID: 32777 RVA: 0x000BE077 File Offset: 0x000BC277
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MergeRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170057D6 RID: 22486
			// (set) Token: 0x0600800A RID: 32778 RVA: 0x000BE095 File Offset: 0x000BC295
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057D7 RID: 22487
			// (set) Token: 0x0600800B RID: 32779 RVA: 0x000BE0A8 File Offset: 0x000BC2A8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057D8 RID: 22488
			// (set) Token: 0x0600800C RID: 32780 RVA: 0x000BE0C0 File Offset: 0x000BC2C0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057D9 RID: 22489
			// (set) Token: 0x0600800D RID: 32781 RVA: 0x000BE0D8 File Offset: 0x000BC2D8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057DA RID: 22490
			// (set) Token: 0x0600800E RID: 32782 RVA: 0x000BE0F0 File Offset: 0x000BC2F0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057DB RID: 22491
			// (set) Token: 0x0600800F RID: 32783 RVA: 0x000BE108 File Offset: 0x000BC308
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
