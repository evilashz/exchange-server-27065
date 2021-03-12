using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A5C RID: 2652
	public class SetMailboxRestoreRequestCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxRestoreRequestIdParameter>
	{
		// Token: 0x060083EE RID: 33774 RVA: 0x000C3096 File Offset: 0x000C1296
		private SetMailboxRestoreRequestCommand() : base("Set-MailboxRestoreRequest")
		{
		}

		// Token: 0x060083EF RID: 33775 RVA: 0x000C30A3 File Offset: 0x000C12A3
		public SetMailboxRestoreRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060083F0 RID: 33776 RVA: 0x000C30B2 File Offset: 0x000C12B2
		public virtual SetMailboxRestoreRequestCommand SetParameters(SetMailboxRestoreRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060083F1 RID: 33777 RVA: 0x000C30BC File Offset: 0x000C12BC
		public virtual SetMailboxRestoreRequestCommand SetParameters(SetMailboxRestoreRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060083F2 RID: 33778 RVA: 0x000C30C6 File Offset: 0x000C12C6
		public virtual SetMailboxRestoreRequestCommand SetParameters(SetMailboxRestoreRequestCommand.RehomeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A5D RID: 2653
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005AEF RID: 23279
			// (set) Token: 0x060083F3 RID: 33779 RVA: 0x000C30D0 File Offset: 0x000C12D0
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005AF0 RID: 23280
			// (set) Token: 0x060083F4 RID: 33780 RVA: 0x000C30E3 File Offset: 0x000C12E3
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005AF1 RID: 23281
			// (set) Token: 0x060083F5 RID: 33781 RVA: 0x000C30F6 File Offset: 0x000C12F6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRestoreRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005AF2 RID: 23282
			// (set) Token: 0x060083F6 RID: 33782 RVA: 0x000C3114 File Offset: 0x000C1314
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005AF3 RID: 23283
			// (set) Token: 0x060083F7 RID: 33783 RVA: 0x000C3127 File Offset: 0x000C1327
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005AF4 RID: 23284
			// (set) Token: 0x060083F8 RID: 33784 RVA: 0x000C313F File Offset: 0x000C133F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005AF5 RID: 23285
			// (set) Token: 0x060083F9 RID: 33785 RVA: 0x000C3157 File Offset: 0x000C1357
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005AF6 RID: 23286
			// (set) Token: 0x060083FA RID: 33786 RVA: 0x000C316F File Offset: 0x000C136F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005AF7 RID: 23287
			// (set) Token: 0x060083FB RID: 33787 RVA: 0x000C3187 File Offset: 0x000C1387
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A5E RID: 2654
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005AF8 RID: 23288
			// (set) Token: 0x060083FD RID: 33789 RVA: 0x000C31A7 File Offset: 0x000C13A7
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005AF9 RID: 23289
			// (set) Token: 0x060083FE RID: 33790 RVA: 0x000C31BF File Offset: 0x000C13BF
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005AFA RID: 23290
			// (set) Token: 0x060083FF RID: 33791 RVA: 0x000C31D7 File Offset: 0x000C13D7
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005AFB RID: 23291
			// (set) Token: 0x06008400 RID: 33792 RVA: 0x000C31EF File Offset: 0x000C13EF
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005AFC RID: 23292
			// (set) Token: 0x06008401 RID: 33793 RVA: 0x000C3202 File Offset: 0x000C1402
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005AFD RID: 23293
			// (set) Token: 0x06008402 RID: 33794 RVA: 0x000C321A File Offset: 0x000C141A
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005AFE RID: 23294
			// (set) Token: 0x06008403 RID: 33795 RVA: 0x000C3232 File Offset: 0x000C1432
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005AFF RID: 23295
			// (set) Token: 0x06008404 RID: 33796 RVA: 0x000C324A File Offset: 0x000C144A
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005B00 RID: 23296
			// (set) Token: 0x06008405 RID: 33797 RVA: 0x000C3262 File Offset: 0x000C1462
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005B01 RID: 23297
			// (set) Token: 0x06008406 RID: 33798 RVA: 0x000C3275 File Offset: 0x000C1475
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005B02 RID: 23298
			// (set) Token: 0x06008407 RID: 33799 RVA: 0x000C3288 File Offset: 0x000C1488
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRestoreRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005B03 RID: 23299
			// (set) Token: 0x06008408 RID: 33800 RVA: 0x000C32A6 File Offset: 0x000C14A6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B04 RID: 23300
			// (set) Token: 0x06008409 RID: 33801 RVA: 0x000C32B9 File Offset: 0x000C14B9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B05 RID: 23301
			// (set) Token: 0x0600840A RID: 33802 RVA: 0x000C32D1 File Offset: 0x000C14D1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B06 RID: 23302
			// (set) Token: 0x0600840B RID: 33803 RVA: 0x000C32E9 File Offset: 0x000C14E9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B07 RID: 23303
			// (set) Token: 0x0600840C RID: 33804 RVA: 0x000C3301 File Offset: 0x000C1501
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B08 RID: 23304
			// (set) Token: 0x0600840D RID: 33805 RVA: 0x000C3319 File Offset: 0x000C1519
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A5F RID: 2655
		public class RehomeParameters : ParametersBase
		{
			// Token: 0x17005B09 RID: 23305
			// (set) Token: 0x0600840F RID: 33807 RVA: 0x000C3339 File Offset: 0x000C1539
			public virtual SwitchParameter RehomeRequest
			{
				set
				{
					base.PowerSharpParameters["RehomeRequest"] = value;
				}
			}

			// Token: 0x17005B0A RID: 23306
			// (set) Token: 0x06008410 RID: 33808 RVA: 0x000C3351 File Offset: 0x000C1551
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005B0B RID: 23307
			// (set) Token: 0x06008411 RID: 33809 RVA: 0x000C3364 File Offset: 0x000C1564
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005B0C RID: 23308
			// (set) Token: 0x06008412 RID: 33810 RVA: 0x000C3377 File Offset: 0x000C1577
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRestoreRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005B0D RID: 23309
			// (set) Token: 0x06008413 RID: 33811 RVA: 0x000C3395 File Offset: 0x000C1595
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B0E RID: 23310
			// (set) Token: 0x06008414 RID: 33812 RVA: 0x000C33A8 File Offset: 0x000C15A8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B0F RID: 23311
			// (set) Token: 0x06008415 RID: 33813 RVA: 0x000C33C0 File Offset: 0x000C15C0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B10 RID: 23312
			// (set) Token: 0x06008416 RID: 33814 RVA: 0x000C33D8 File Offset: 0x000C15D8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B11 RID: 23313
			// (set) Token: 0x06008417 RID: 33815 RVA: 0x000C33F0 File Offset: 0x000C15F0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B12 RID: 23314
			// (set) Token: 0x06008418 RID: 33816 RVA: 0x000C3408 File Offset: 0x000C1608
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
