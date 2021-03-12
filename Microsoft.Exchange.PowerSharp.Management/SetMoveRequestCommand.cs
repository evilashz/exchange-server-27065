using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009E1 RID: 2529
	public class SetMoveRequestCommand : SyntheticCommandWithPipelineInputNoOutput<MoveRequestIdParameter>
	{
		// Token: 0x06007EEA RID: 32490 RVA: 0x000BC8B9 File Offset: 0x000BAAB9
		private SetMoveRequestCommand() : base("Set-MoveRequest")
		{
		}

		// Token: 0x06007EEB RID: 32491 RVA: 0x000BC8C6 File Offset: 0x000BAAC6
		public SetMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x000BC8D5 File Offset: 0x000BAAD5
		public virtual SetMoveRequestCommand SetParameters(SetMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x000BC8DF File Offset: 0x000BAADF
		public virtual SetMoveRequestCommand SetParameters(SetMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009E2 RID: 2530
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170056E1 RID: 22241
			// (set) Token: 0x06007EEE RID: 32494 RVA: 0x000BC8E9 File Offset: 0x000BAAE9
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x170056E2 RID: 22242
			// (set) Token: 0x06007EEF RID: 32495 RVA: 0x000BC901 File Offset: 0x000BAB01
			public virtual Fqdn RemoteGlobalCatalog
			{
				set
				{
					base.PowerSharpParameters["RemoteGlobalCatalog"] = value;
				}
			}

			// Token: 0x170056E3 RID: 22243
			// (set) Token: 0x06007EF0 RID: 32496 RVA: 0x000BC914 File Offset: 0x000BAB14
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170056E4 RID: 22244
			// (set) Token: 0x06007EF1 RID: 32497 RVA: 0x000BC92C File Offset: 0x000BAB2C
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x170056E5 RID: 22245
			// (set) Token: 0x06007EF2 RID: 32498 RVA: 0x000BC944 File Offset: 0x000BAB44
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170056E6 RID: 22246
			// (set) Token: 0x06007EF3 RID: 32499 RVA: 0x000BC95C File Offset: 0x000BAB5C
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x170056E7 RID: 22247
			// (set) Token: 0x06007EF4 RID: 32500 RVA: 0x000BC96F File Offset: 0x000BAB6F
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x170056E8 RID: 22248
			// (set) Token: 0x06007EF5 RID: 32501 RVA: 0x000BC982 File Offset: 0x000BAB82
			public virtual bool Protect
			{
				set
				{
					base.PowerSharpParameters["Protect"] = value;
				}
			}

			// Token: 0x170056E9 RID: 22249
			// (set) Token: 0x06007EF6 RID: 32502 RVA: 0x000BC99A File Offset: 0x000BAB9A
			public virtual bool IgnoreRuleLimitErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreRuleLimitErrors"] = value;
				}
			}

			// Token: 0x170056EA RID: 22250
			// (set) Token: 0x06007EF7 RID: 32503 RVA: 0x000BC9B2 File Offset: 0x000BABB2
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170056EB RID: 22251
			// (set) Token: 0x06007EF8 RID: 32504 RVA: 0x000BC9C5 File Offset: 0x000BABC5
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170056EC RID: 22252
			// (set) Token: 0x06007EF9 RID: 32505 RVA: 0x000BC9DD File Offset: 0x000BABDD
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170056ED RID: 22253
			// (set) Token: 0x06007EFA RID: 32506 RVA: 0x000BC9F5 File Offset: 0x000BABF5
			public virtual bool PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x170056EE RID: 22254
			// (set) Token: 0x06007EFB RID: 32507 RVA: 0x000BCA0D File Offset: 0x000BAC0D
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x170056EF RID: 22255
			// (set) Token: 0x06007EFC RID: 32508 RVA: 0x000BCA25 File Offset: 0x000BAC25
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170056F0 RID: 22256
			// (set) Token: 0x06007EFD RID: 32509 RVA: 0x000BCA3D File Offset: 0x000BAC3D
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x170056F1 RID: 22257
			// (set) Token: 0x06007EFE RID: 32510 RVA: 0x000BCA55 File Offset: 0x000BAC55
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x170056F2 RID: 22258
			// (set) Token: 0x06007EFF RID: 32511 RVA: 0x000BCA6D File Offset: 0x000BAC6D
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x170056F3 RID: 22259
			// (set) Token: 0x06007F00 RID: 32512 RVA: 0x000BCA85 File Offset: 0x000BAC85
			public virtual DatabaseIdParameter TargetDatabase
			{
				set
				{
					base.PowerSharpParameters["TargetDatabase"] = value;
				}
			}

			// Token: 0x170056F4 RID: 22260
			// (set) Token: 0x06007F01 RID: 32513 RVA: 0x000BCA98 File Offset: 0x000BAC98
			public virtual DatabaseIdParameter ArchiveTargetDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveTargetDatabase"] = value;
				}
			}

			// Token: 0x170056F5 RID: 22261
			// (set) Token: 0x06007F02 RID: 32514 RVA: 0x000BCAAB File Offset: 0x000BACAB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170056F6 RID: 22262
			// (set) Token: 0x06007F03 RID: 32515 RVA: 0x000BCABE File Offset: 0x000BACBE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170056F7 RID: 22263
			// (set) Token: 0x06007F04 RID: 32516 RVA: 0x000BCAD6 File Offset: 0x000BACD6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170056F8 RID: 22264
			// (set) Token: 0x06007F05 RID: 32517 RVA: 0x000BCAEE File Offset: 0x000BACEE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170056F9 RID: 22265
			// (set) Token: 0x06007F06 RID: 32518 RVA: 0x000BCB06 File Offset: 0x000BAD06
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170056FA RID: 22266
			// (set) Token: 0x06007F07 RID: 32519 RVA: 0x000BCB1E File Offset: 0x000BAD1E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009E3 RID: 2531
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170056FB RID: 22267
			// (set) Token: 0x06007F09 RID: 32521 RVA: 0x000BCB3E File Offset: 0x000BAD3E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170056FC RID: 22268
			// (set) Token: 0x06007F0A RID: 32522 RVA: 0x000BCB5C File Offset: 0x000BAD5C
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x170056FD RID: 22269
			// (set) Token: 0x06007F0B RID: 32523 RVA: 0x000BCB74 File Offset: 0x000BAD74
			public virtual Fqdn RemoteGlobalCatalog
			{
				set
				{
					base.PowerSharpParameters["RemoteGlobalCatalog"] = value;
				}
			}

			// Token: 0x170056FE RID: 22270
			// (set) Token: 0x06007F0C RID: 32524 RVA: 0x000BCB87 File Offset: 0x000BAD87
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170056FF RID: 22271
			// (set) Token: 0x06007F0D RID: 32525 RVA: 0x000BCB9F File Offset: 0x000BAD9F
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005700 RID: 22272
			// (set) Token: 0x06007F0E RID: 32526 RVA: 0x000BCBB7 File Offset: 0x000BADB7
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005701 RID: 22273
			// (set) Token: 0x06007F0F RID: 32527 RVA: 0x000BCBCF File Offset: 0x000BADCF
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005702 RID: 22274
			// (set) Token: 0x06007F10 RID: 32528 RVA: 0x000BCBE2 File Offset: 0x000BADE2
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005703 RID: 22275
			// (set) Token: 0x06007F11 RID: 32529 RVA: 0x000BCBF5 File Offset: 0x000BADF5
			public virtual bool Protect
			{
				set
				{
					base.PowerSharpParameters["Protect"] = value;
				}
			}

			// Token: 0x17005704 RID: 22276
			// (set) Token: 0x06007F12 RID: 32530 RVA: 0x000BCC0D File Offset: 0x000BAE0D
			public virtual bool IgnoreRuleLimitErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreRuleLimitErrors"] = value;
				}
			}

			// Token: 0x17005705 RID: 22277
			// (set) Token: 0x06007F13 RID: 32531 RVA: 0x000BCC25 File Offset: 0x000BAE25
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005706 RID: 22278
			// (set) Token: 0x06007F14 RID: 32532 RVA: 0x000BCC38 File Offset: 0x000BAE38
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005707 RID: 22279
			// (set) Token: 0x06007F15 RID: 32533 RVA: 0x000BCC50 File Offset: 0x000BAE50
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005708 RID: 22280
			// (set) Token: 0x06007F16 RID: 32534 RVA: 0x000BCC68 File Offset: 0x000BAE68
			public virtual bool PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x17005709 RID: 22281
			// (set) Token: 0x06007F17 RID: 32535 RVA: 0x000BCC80 File Offset: 0x000BAE80
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x1700570A RID: 22282
			// (set) Token: 0x06007F18 RID: 32536 RVA: 0x000BCC98 File Offset: 0x000BAE98
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x1700570B RID: 22283
			// (set) Token: 0x06007F19 RID: 32537 RVA: 0x000BCCB0 File Offset: 0x000BAEB0
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x1700570C RID: 22284
			// (set) Token: 0x06007F1A RID: 32538 RVA: 0x000BCCC8 File Offset: 0x000BAEC8
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700570D RID: 22285
			// (set) Token: 0x06007F1B RID: 32539 RVA: 0x000BCCE0 File Offset: 0x000BAEE0
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x1700570E RID: 22286
			// (set) Token: 0x06007F1C RID: 32540 RVA: 0x000BCCF8 File Offset: 0x000BAEF8
			public virtual DatabaseIdParameter TargetDatabase
			{
				set
				{
					base.PowerSharpParameters["TargetDatabase"] = value;
				}
			}

			// Token: 0x1700570F RID: 22287
			// (set) Token: 0x06007F1D RID: 32541 RVA: 0x000BCD0B File Offset: 0x000BAF0B
			public virtual DatabaseIdParameter ArchiveTargetDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveTargetDatabase"] = value;
				}
			}

			// Token: 0x17005710 RID: 22288
			// (set) Token: 0x06007F1E RID: 32542 RVA: 0x000BCD1E File Offset: 0x000BAF1E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005711 RID: 22289
			// (set) Token: 0x06007F1F RID: 32543 RVA: 0x000BCD31 File Offset: 0x000BAF31
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005712 RID: 22290
			// (set) Token: 0x06007F20 RID: 32544 RVA: 0x000BCD49 File Offset: 0x000BAF49
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005713 RID: 22291
			// (set) Token: 0x06007F21 RID: 32545 RVA: 0x000BCD61 File Offset: 0x000BAF61
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005714 RID: 22292
			// (set) Token: 0x06007F22 RID: 32546 RVA: 0x000BCD79 File Offset: 0x000BAF79
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005715 RID: 22293
			// (set) Token: 0x06007F23 RID: 32547 RVA: 0x000BCD91 File Offset: 0x000BAF91
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
