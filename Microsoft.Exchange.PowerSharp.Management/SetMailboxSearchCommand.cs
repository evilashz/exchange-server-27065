using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000409 RID: 1033
	public class SetMailboxSearchCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxDiscoverySearch>
	{
		// Token: 0x06003CF8 RID: 15608 RVA: 0x00066EA7 File Offset: 0x000650A7
		private SetMailboxSearchCommand() : base("Set-MailboxSearch")
		{
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x00066EB4 File Offset: 0x000650B4
		public SetMailboxSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x00066EC3 File Offset: 0x000650C3
		public virtual SetMailboxSearchCommand SetParameters(SetMailboxSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x00066ECD File Offset: 0x000650CD
		public virtual SetMailboxSearchCommand SetParameters(SetMailboxSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200040A RID: 1034
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700209F RID: 8351
			// (set) Token: 0x06003CFC RID: 15612 RVA: 0x00066ED7 File Offset: 0x000650D7
			public virtual string SearchQuery
			{
				set
				{
					base.PowerSharpParameters["SearchQuery"] = value;
				}
			}

			// Token: 0x170020A0 RID: 8352
			// (set) Token: 0x06003CFD RID: 15613 RVA: 0x00066EEA File Offset: 0x000650EA
			public virtual RecipientIdParameter SourceMailboxes
			{
				set
				{
					base.PowerSharpParameters["SourceMailboxes"] = value;
				}
			}

			// Token: 0x170020A1 RID: 8353
			// (set) Token: 0x06003CFE RID: 15614 RVA: 0x00066EFD File Offset: 0x000650FD
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170020A2 RID: 8354
			// (set) Token: 0x06003CFF RID: 15615 RVA: 0x00066F1B File Offset: 0x0006511B
			public virtual PublicFolderIdParameter PublicFolderSources
			{
				set
				{
					base.PowerSharpParameters["PublicFolderSources"] = value;
				}
			}

			// Token: 0x170020A3 RID: 8355
			// (set) Token: 0x06003D00 RID: 15616 RVA: 0x00066F2E File Offset: 0x0006512E
			public virtual bool AllPublicFolderSources
			{
				set
				{
					base.PowerSharpParameters["AllPublicFolderSources"] = value;
				}
			}

			// Token: 0x170020A4 RID: 8356
			// (set) Token: 0x06003D01 RID: 15617 RVA: 0x00066F46 File Offset: 0x00065146
			public virtual bool AllSourceMailboxes
			{
				set
				{
					base.PowerSharpParameters["AllSourceMailboxes"] = value;
				}
			}

			// Token: 0x170020A5 RID: 8357
			// (set) Token: 0x06003D02 RID: 15618 RVA: 0x00066F5E File Offset: 0x0006515E
			public virtual string Senders
			{
				set
				{
					base.PowerSharpParameters["Senders"] = value;
				}
			}

			// Token: 0x170020A6 RID: 8358
			// (set) Token: 0x06003D03 RID: 15619 RVA: 0x00066F71 File Offset: 0x00065171
			public virtual string Recipients
			{
				set
				{
					base.PowerSharpParameters["Recipients"] = value;
				}
			}

			// Token: 0x170020A7 RID: 8359
			// (set) Token: 0x06003D04 RID: 15620 RVA: 0x00066F84 File Offset: 0x00065184
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170020A8 RID: 8360
			// (set) Token: 0x06003D05 RID: 15621 RVA: 0x00066F9C File Offset: 0x0006519C
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x170020A9 RID: 8361
			// (set) Token: 0x06003D06 RID: 15622 RVA: 0x00066FB4 File Offset: 0x000651B4
			public virtual KindKeyword MessageTypes
			{
				set
				{
					base.PowerSharpParameters["MessageTypes"] = value;
				}
			}

			// Token: 0x170020AA RID: 8362
			// (set) Token: 0x06003D07 RID: 15623 RVA: 0x00066FCC File Offset: 0x000651CC
			public virtual RecipientIdParameter StatusMailRecipients
			{
				set
				{
					base.PowerSharpParameters["StatusMailRecipients"] = value;
				}
			}

			// Token: 0x170020AB RID: 8363
			// (set) Token: 0x06003D08 RID: 15624 RVA: 0x00066FDF File Offset: 0x000651DF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170020AC RID: 8364
			// (set) Token: 0x06003D09 RID: 15625 RVA: 0x00066FF7 File Offset: 0x000651F7
			public virtual bool EstimateOnly
			{
				set
				{
					base.PowerSharpParameters["EstimateOnly"] = value;
				}
			}

			// Token: 0x170020AD RID: 8365
			// (set) Token: 0x06003D0A RID: 15626 RVA: 0x0006700F File Offset: 0x0006520F
			public virtual SwitchParameter IncludeKeywordStatistics
			{
				set
				{
					base.PowerSharpParameters["IncludeKeywordStatistics"] = value;
				}
			}

			// Token: 0x170020AE RID: 8366
			// (set) Token: 0x06003D0B RID: 15627 RVA: 0x00067027 File Offset: 0x00065227
			public virtual int StatisticsStartIndex
			{
				set
				{
					base.PowerSharpParameters["StatisticsStartIndex"] = value;
				}
			}

			// Token: 0x170020AF RID: 8367
			// (set) Token: 0x06003D0C RID: 15628 RVA: 0x0006703F File Offset: 0x0006523F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170020B0 RID: 8368
			// (set) Token: 0x06003D0D RID: 15629 RVA: 0x00067052 File Offset: 0x00065252
			public virtual bool IncludeUnsearchableItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnsearchableItems"] = value;
				}
			}

			// Token: 0x170020B1 RID: 8369
			// (set) Token: 0x06003D0E RID: 15630 RVA: 0x0006706A File Offset: 0x0006526A
			public virtual LoggingLevel LogLevel
			{
				set
				{
					base.PowerSharpParameters["LogLevel"] = value;
				}
			}

			// Token: 0x170020B2 RID: 8370
			// (set) Token: 0x06003D0F RID: 15631 RVA: 0x00067082 File Offset: 0x00065282
			public virtual string Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x170020B3 RID: 8371
			// (set) Token: 0x06003D10 RID: 15632 RVA: 0x00067095 File Offset: 0x00065295
			public virtual bool ExcludeDuplicateMessages
			{
				set
				{
					base.PowerSharpParameters["ExcludeDuplicateMessages"] = value;
				}
			}

			// Token: 0x170020B4 RID: 8372
			// (set) Token: 0x06003D11 RID: 15633 RVA: 0x000670AD File Offset: 0x000652AD
			public virtual bool InPlaceHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldEnabled"] = value;
				}
			}

			// Token: 0x170020B5 RID: 8373
			// (set) Token: 0x06003D12 RID: 15634 RVA: 0x000670C5 File Offset: 0x000652C5
			public virtual Unlimited<EnhancedTimeSpan> ItemHoldPeriod
			{
				set
				{
					base.PowerSharpParameters["ItemHoldPeriod"] = value;
				}
			}

			// Token: 0x170020B6 RID: 8374
			// (set) Token: 0x06003D13 RID: 15635 RVA: 0x000670DD File Offset: 0x000652DD
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x170020B7 RID: 8375
			// (set) Token: 0x06003D14 RID: 15636 RVA: 0x000670F0 File Offset: 0x000652F0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170020B8 RID: 8376
			// (set) Token: 0x06003D15 RID: 15637 RVA: 0x00067103 File Offset: 0x00065303
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170020B9 RID: 8377
			// (set) Token: 0x06003D16 RID: 15638 RVA: 0x0006711B File Offset: 0x0006531B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170020BA RID: 8378
			// (set) Token: 0x06003D17 RID: 15639 RVA: 0x00067133 File Offset: 0x00065333
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170020BB RID: 8379
			// (set) Token: 0x06003D18 RID: 15640 RVA: 0x0006714B File Offset: 0x0006534B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170020BC RID: 8380
			// (set) Token: 0x06003D19 RID: 15641 RVA: 0x00067163 File Offset: 0x00065363
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200040B RID: 1035
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170020BD RID: 8381
			// (set) Token: 0x06003D1B RID: 15643 RVA: 0x00067183 File Offset: 0x00065383
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new EwsStoreObjectIdParameter(value) : null);
				}
			}

			// Token: 0x170020BE RID: 8382
			// (set) Token: 0x06003D1C RID: 15644 RVA: 0x000671A1 File Offset: 0x000653A1
			public virtual string SearchQuery
			{
				set
				{
					base.PowerSharpParameters["SearchQuery"] = value;
				}
			}

			// Token: 0x170020BF RID: 8383
			// (set) Token: 0x06003D1D RID: 15645 RVA: 0x000671B4 File Offset: 0x000653B4
			public virtual RecipientIdParameter SourceMailboxes
			{
				set
				{
					base.PowerSharpParameters["SourceMailboxes"] = value;
				}
			}

			// Token: 0x170020C0 RID: 8384
			// (set) Token: 0x06003D1E RID: 15646 RVA: 0x000671C7 File Offset: 0x000653C7
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170020C1 RID: 8385
			// (set) Token: 0x06003D1F RID: 15647 RVA: 0x000671E5 File Offset: 0x000653E5
			public virtual PublicFolderIdParameter PublicFolderSources
			{
				set
				{
					base.PowerSharpParameters["PublicFolderSources"] = value;
				}
			}

			// Token: 0x170020C2 RID: 8386
			// (set) Token: 0x06003D20 RID: 15648 RVA: 0x000671F8 File Offset: 0x000653F8
			public virtual bool AllPublicFolderSources
			{
				set
				{
					base.PowerSharpParameters["AllPublicFolderSources"] = value;
				}
			}

			// Token: 0x170020C3 RID: 8387
			// (set) Token: 0x06003D21 RID: 15649 RVA: 0x00067210 File Offset: 0x00065410
			public virtual bool AllSourceMailboxes
			{
				set
				{
					base.PowerSharpParameters["AllSourceMailboxes"] = value;
				}
			}

			// Token: 0x170020C4 RID: 8388
			// (set) Token: 0x06003D22 RID: 15650 RVA: 0x00067228 File Offset: 0x00065428
			public virtual string Senders
			{
				set
				{
					base.PowerSharpParameters["Senders"] = value;
				}
			}

			// Token: 0x170020C5 RID: 8389
			// (set) Token: 0x06003D23 RID: 15651 RVA: 0x0006723B File Offset: 0x0006543B
			public virtual string Recipients
			{
				set
				{
					base.PowerSharpParameters["Recipients"] = value;
				}
			}

			// Token: 0x170020C6 RID: 8390
			// (set) Token: 0x06003D24 RID: 15652 RVA: 0x0006724E File Offset: 0x0006544E
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170020C7 RID: 8391
			// (set) Token: 0x06003D25 RID: 15653 RVA: 0x00067266 File Offset: 0x00065466
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x170020C8 RID: 8392
			// (set) Token: 0x06003D26 RID: 15654 RVA: 0x0006727E File Offset: 0x0006547E
			public virtual KindKeyword MessageTypes
			{
				set
				{
					base.PowerSharpParameters["MessageTypes"] = value;
				}
			}

			// Token: 0x170020C9 RID: 8393
			// (set) Token: 0x06003D27 RID: 15655 RVA: 0x00067296 File Offset: 0x00065496
			public virtual RecipientIdParameter StatusMailRecipients
			{
				set
				{
					base.PowerSharpParameters["StatusMailRecipients"] = value;
				}
			}

			// Token: 0x170020CA RID: 8394
			// (set) Token: 0x06003D28 RID: 15656 RVA: 0x000672A9 File Offset: 0x000654A9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170020CB RID: 8395
			// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000672C1 File Offset: 0x000654C1
			public virtual bool EstimateOnly
			{
				set
				{
					base.PowerSharpParameters["EstimateOnly"] = value;
				}
			}

			// Token: 0x170020CC RID: 8396
			// (set) Token: 0x06003D2A RID: 15658 RVA: 0x000672D9 File Offset: 0x000654D9
			public virtual SwitchParameter IncludeKeywordStatistics
			{
				set
				{
					base.PowerSharpParameters["IncludeKeywordStatistics"] = value;
				}
			}

			// Token: 0x170020CD RID: 8397
			// (set) Token: 0x06003D2B RID: 15659 RVA: 0x000672F1 File Offset: 0x000654F1
			public virtual int StatisticsStartIndex
			{
				set
				{
					base.PowerSharpParameters["StatisticsStartIndex"] = value;
				}
			}

			// Token: 0x170020CE RID: 8398
			// (set) Token: 0x06003D2C RID: 15660 RVA: 0x00067309 File Offset: 0x00065509
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170020CF RID: 8399
			// (set) Token: 0x06003D2D RID: 15661 RVA: 0x0006731C File Offset: 0x0006551C
			public virtual bool IncludeUnsearchableItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnsearchableItems"] = value;
				}
			}

			// Token: 0x170020D0 RID: 8400
			// (set) Token: 0x06003D2E RID: 15662 RVA: 0x00067334 File Offset: 0x00065534
			public virtual LoggingLevel LogLevel
			{
				set
				{
					base.PowerSharpParameters["LogLevel"] = value;
				}
			}

			// Token: 0x170020D1 RID: 8401
			// (set) Token: 0x06003D2F RID: 15663 RVA: 0x0006734C File Offset: 0x0006554C
			public virtual string Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x170020D2 RID: 8402
			// (set) Token: 0x06003D30 RID: 15664 RVA: 0x0006735F File Offset: 0x0006555F
			public virtual bool ExcludeDuplicateMessages
			{
				set
				{
					base.PowerSharpParameters["ExcludeDuplicateMessages"] = value;
				}
			}

			// Token: 0x170020D3 RID: 8403
			// (set) Token: 0x06003D31 RID: 15665 RVA: 0x00067377 File Offset: 0x00065577
			public virtual bool InPlaceHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldEnabled"] = value;
				}
			}

			// Token: 0x170020D4 RID: 8404
			// (set) Token: 0x06003D32 RID: 15666 RVA: 0x0006738F File Offset: 0x0006558F
			public virtual Unlimited<EnhancedTimeSpan> ItemHoldPeriod
			{
				set
				{
					base.PowerSharpParameters["ItemHoldPeriod"] = value;
				}
			}

			// Token: 0x170020D5 RID: 8405
			// (set) Token: 0x06003D33 RID: 15667 RVA: 0x000673A7 File Offset: 0x000655A7
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x170020D6 RID: 8406
			// (set) Token: 0x06003D34 RID: 15668 RVA: 0x000673BA File Offset: 0x000655BA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170020D7 RID: 8407
			// (set) Token: 0x06003D35 RID: 15669 RVA: 0x000673CD File Offset: 0x000655CD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170020D8 RID: 8408
			// (set) Token: 0x06003D36 RID: 15670 RVA: 0x000673E5 File Offset: 0x000655E5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170020D9 RID: 8409
			// (set) Token: 0x06003D37 RID: 15671 RVA: 0x000673FD File Offset: 0x000655FD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170020DA RID: 8410
			// (set) Token: 0x06003D38 RID: 15672 RVA: 0x00067415 File Offset: 0x00065615
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170020DB RID: 8411
			// (set) Token: 0x06003D39 RID: 15673 RVA: 0x0006742D File Offset: 0x0006562D
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
