using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C4E RID: 3150
	public class NewInboxRuleCommand : SyntheticCommandWithPipelineInput<InboxRule, InboxRule>
	{
		// Token: 0x060099E4 RID: 39396 RVA: 0x000DF7F1 File Offset: 0x000DD9F1
		private NewInboxRuleCommand() : base("New-InboxRule")
		{
		}

		// Token: 0x060099E5 RID: 39397 RVA: 0x000DF7FE File Offset: 0x000DD9FE
		public NewInboxRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060099E6 RID: 39398 RVA: 0x000DF80D File Offset: 0x000DDA0D
		public virtual NewInboxRuleCommand SetParameters(NewInboxRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060099E7 RID: 39399 RVA: 0x000DF817 File Offset: 0x000DDA17
		public virtual NewInboxRuleCommand SetParameters(NewInboxRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060099E8 RID: 39400 RVA: 0x000DF821 File Offset: 0x000DDA21
		public virtual NewInboxRuleCommand SetParameters(NewInboxRuleCommand.FromMessageParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C4F RID: 3151
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006D01 RID: 27905
			// (set) Token: 0x060099E9 RID: 39401 RVA: 0x000DF82B File Offset: 0x000DDA2B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006D02 RID: 27906
			// (set) Token: 0x060099EA RID: 39402 RVA: 0x000DF843 File Offset: 0x000DDA43
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006D03 RID: 27907
			// (set) Token: 0x060099EB RID: 39403 RVA: 0x000DF85B File Offset: 0x000DDA5B
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006D04 RID: 27908
			// (set) Token: 0x060099EC RID: 39404 RVA: 0x000DF879 File Offset: 0x000DDA79
			public virtual AggregationSubscriptionIdentity FromSubscription
			{
				set
				{
					base.PowerSharpParameters["FromSubscription"] = value;
				}
			}

			// Token: 0x17006D05 RID: 27909
			// (set) Token: 0x060099ED RID: 39405 RVA: 0x000DF88C File Offset: 0x000DDA8C
			public virtual AggregationSubscriptionIdentity ExceptIfFromSubscription
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromSubscription"] = value;
				}
			}

			// Token: 0x17006D06 RID: 27910
			// (set) Token: 0x060099EE RID: 39406 RVA: 0x000DF89F File Offset: 0x000DDA9F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006D07 RID: 27911
			// (set) Token: 0x060099EF RID: 39407 RVA: 0x000DF8BD File Offset: 0x000DDABD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006D08 RID: 27912
			// (set) Token: 0x060099F0 RID: 39408 RVA: 0x000DF8D0 File Offset: 0x000DDAD0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006D09 RID: 27913
			// (set) Token: 0x060099F1 RID: 39409 RVA: 0x000DF8E8 File Offset: 0x000DDAE8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006D0A RID: 27914
			// (set) Token: 0x060099F2 RID: 39410 RVA: 0x000DF900 File Offset: 0x000DDB00
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006D0B RID: 27915
			// (set) Token: 0x060099F3 RID: 39411 RVA: 0x000DF918 File Offset: 0x000DDB18
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006D0C RID: 27916
			// (set) Token: 0x060099F4 RID: 39412 RVA: 0x000DF930 File Offset: 0x000DDB30
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C50 RID: 3152
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006D0D RID: 27917
			// (set) Token: 0x060099F6 RID: 39414 RVA: 0x000DF950 File Offset: 0x000DDB50
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006D0E RID: 27918
			// (set) Token: 0x060099F7 RID: 39415 RVA: 0x000DF963 File Offset: 0x000DDB63
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17006D0F RID: 27919
			// (set) Token: 0x060099F8 RID: 39416 RVA: 0x000DF97B File Offset: 0x000DDB7B
			public virtual MultiValuedProperty<string> BodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["BodyContainsWords"] = value;
				}
			}

			// Token: 0x17006D10 RID: 27920
			// (set) Token: 0x060099F9 RID: 39417 RVA: 0x000DF98E File Offset: 0x000DDB8E
			public virtual MultiValuedProperty<string> ExceptIfBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006D11 RID: 27921
			// (set) Token: 0x060099FA RID: 39418 RVA: 0x000DF9A1 File Offset: 0x000DDBA1
			public virtual string FlaggedForAction
			{
				set
				{
					base.PowerSharpParameters["FlaggedForAction"] = value;
				}
			}

			// Token: 0x17006D12 RID: 27922
			// (set) Token: 0x060099FB RID: 39419 RVA: 0x000DF9B4 File Offset: 0x000DDBB4
			public virtual string ExceptIfFlaggedForAction
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFlaggedForAction"] = value;
				}
			}

			// Token: 0x17006D13 RID: 27923
			// (set) Token: 0x060099FC RID: 39420 RVA: 0x000DF9C7 File Offset: 0x000DDBC7
			public virtual RecipientIdParameter From
			{
				set
				{
					base.PowerSharpParameters["From"] = value;
				}
			}

			// Token: 0x17006D14 RID: 27924
			// (set) Token: 0x060099FD RID: 39421 RVA: 0x000DF9DA File Offset: 0x000DDBDA
			public virtual RecipientIdParameter ExceptIfFrom
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFrom"] = value;
				}
			}

			// Token: 0x17006D15 RID: 27925
			// (set) Token: 0x060099FE RID: 39422 RVA: 0x000DF9ED File Offset: 0x000DDBED
			public virtual MultiValuedProperty<string> FromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["FromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006D16 RID: 27926
			// (set) Token: 0x060099FF RID: 39423 RVA: 0x000DFA00 File Offset: 0x000DDC00
			public virtual MultiValuedProperty<string> ExceptIfFromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006D17 RID: 27927
			// (set) Token: 0x06009A00 RID: 39424 RVA: 0x000DFA13 File Offset: 0x000DDC13
			public virtual bool HasAttachment
			{
				set
				{
					base.PowerSharpParameters["HasAttachment"] = value;
				}
			}

			// Token: 0x17006D18 RID: 27928
			// (set) Token: 0x06009A01 RID: 39425 RVA: 0x000DFA2B File Offset: 0x000DDC2B
			public virtual bool ExceptIfHasAttachment
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasAttachment"] = value;
				}
			}

			// Token: 0x17006D19 RID: 27929
			// (set) Token: 0x06009A02 RID: 39426 RVA: 0x000DFA43 File Offset: 0x000DDC43
			public virtual MessageClassificationIdParameter HasClassification
			{
				set
				{
					base.PowerSharpParameters["HasClassification"] = value;
				}
			}

			// Token: 0x17006D1A RID: 27930
			// (set) Token: 0x06009A03 RID: 39427 RVA: 0x000DFA56 File Offset: 0x000DDC56
			public virtual MessageClassificationIdParameter ExceptIfHasClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasClassification"] = value;
				}
			}

			// Token: 0x17006D1B RID: 27931
			// (set) Token: 0x06009A04 RID: 39428 RVA: 0x000DFA69 File Offset: 0x000DDC69
			public virtual MultiValuedProperty<string> HeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsWords"] = value;
				}
			}

			// Token: 0x17006D1C RID: 27932
			// (set) Token: 0x06009A05 RID: 39429 RVA: 0x000DFA7C File Offset: 0x000DDC7C
			public virtual MultiValuedProperty<string> ExceptIfHeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsWords"] = value;
				}
			}

			// Token: 0x17006D1D RID: 27933
			// (set) Token: 0x06009A06 RID: 39430 RVA: 0x000DFA8F File Offset: 0x000DDC8F
			public virtual InboxRuleMessageType MessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["MessageTypeMatches"] = value;
				}
			}

			// Token: 0x17006D1E RID: 27934
			// (set) Token: 0x06009A07 RID: 39431 RVA: 0x000DFAA7 File Offset: 0x000DDCA7
			public virtual InboxRuleMessageType ExceptIfMessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageTypeMatches"] = value;
				}
			}

			// Token: 0x17006D1F RID: 27935
			// (set) Token: 0x06009A08 RID: 39432 RVA: 0x000DFABF File Offset: 0x000DDCBF
			public virtual bool MyNameInCcBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInCcBox"] = value;
				}
			}

			// Token: 0x17006D20 RID: 27936
			// (set) Token: 0x06009A09 RID: 39433 RVA: 0x000DFAD7 File Offset: 0x000DDCD7
			public virtual bool ExceptIfMyNameInCcBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInCcBox"] = value;
				}
			}

			// Token: 0x17006D21 RID: 27937
			// (set) Token: 0x06009A0A RID: 39434 RVA: 0x000DFAEF File Offset: 0x000DDCEF
			public virtual bool MyNameInToBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInToBox"] = value;
				}
			}

			// Token: 0x17006D22 RID: 27938
			// (set) Token: 0x06009A0B RID: 39435 RVA: 0x000DFB07 File Offset: 0x000DDD07
			public virtual bool ExceptIfMyNameInToBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInToBox"] = value;
				}
			}

			// Token: 0x17006D23 RID: 27939
			// (set) Token: 0x06009A0C RID: 39436 RVA: 0x000DFB1F File Offset: 0x000DDD1F
			public virtual bool MyNameInToOrCcBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInToOrCcBox"] = value;
				}
			}

			// Token: 0x17006D24 RID: 27940
			// (set) Token: 0x06009A0D RID: 39437 RVA: 0x000DFB37 File Offset: 0x000DDD37
			public virtual bool ExceptIfMyNameInToOrCcBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInToOrCcBox"] = value;
				}
			}

			// Token: 0x17006D25 RID: 27941
			// (set) Token: 0x06009A0E RID: 39438 RVA: 0x000DFB4F File Offset: 0x000DDD4F
			public virtual bool MyNameNotInToBox
			{
				set
				{
					base.PowerSharpParameters["MyNameNotInToBox"] = value;
				}
			}

			// Token: 0x17006D26 RID: 27942
			// (set) Token: 0x06009A0F RID: 39439 RVA: 0x000DFB67 File Offset: 0x000DDD67
			public virtual bool ExceptIfMyNameNotInToBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameNotInToBox"] = value;
				}
			}

			// Token: 0x17006D27 RID: 27943
			// (set) Token: 0x06009A10 RID: 39440 RVA: 0x000DFB7F File Offset: 0x000DDD7F
			public virtual ExDateTime ReceivedAfterDate
			{
				set
				{
					base.PowerSharpParameters["ReceivedAfterDate"] = value;
				}
			}

			// Token: 0x17006D28 RID: 27944
			// (set) Token: 0x06009A11 RID: 39441 RVA: 0x000DFB97 File Offset: 0x000DDD97
			public virtual ExDateTime ExceptIfReceivedAfterDate
			{
				set
				{
					base.PowerSharpParameters["ExceptIfReceivedAfterDate"] = value;
				}
			}

			// Token: 0x17006D29 RID: 27945
			// (set) Token: 0x06009A12 RID: 39442 RVA: 0x000DFBAF File Offset: 0x000DDDAF
			public virtual ExDateTime ReceivedBeforeDate
			{
				set
				{
					base.PowerSharpParameters["ReceivedBeforeDate"] = value;
				}
			}

			// Token: 0x17006D2A RID: 27946
			// (set) Token: 0x06009A13 RID: 39443 RVA: 0x000DFBC7 File Offset: 0x000DDDC7
			public virtual ExDateTime ExceptIfReceivedBeforeDate
			{
				set
				{
					base.PowerSharpParameters["ExceptIfReceivedBeforeDate"] = value;
				}
			}

			// Token: 0x17006D2B RID: 27947
			// (set) Token: 0x06009A14 RID: 39444 RVA: 0x000DFBDF File Offset: 0x000DDDDF
			public virtual MultiValuedProperty<string> RecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006D2C RID: 27948
			// (set) Token: 0x06009A15 RID: 39445 RVA: 0x000DFBF2 File Offset: 0x000DDDF2
			public virtual MultiValuedProperty<string> ExceptIfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006D2D RID: 27949
			// (set) Token: 0x06009A16 RID: 39446 RVA: 0x000DFC05 File Offset: 0x000DDE05
			public virtual bool SentOnlyToMe
			{
				set
				{
					base.PowerSharpParameters["SentOnlyToMe"] = value;
				}
			}

			// Token: 0x17006D2E RID: 27950
			// (set) Token: 0x06009A17 RID: 39447 RVA: 0x000DFC1D File Offset: 0x000DDE1D
			public virtual bool ExceptIfSentOnlyToMe
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentOnlyToMe"] = value;
				}
			}

			// Token: 0x17006D2F RID: 27951
			// (set) Token: 0x06009A18 RID: 39448 RVA: 0x000DFC35 File Offset: 0x000DDE35
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17006D30 RID: 27952
			// (set) Token: 0x06009A19 RID: 39449 RVA: 0x000DFC48 File Offset: 0x000DDE48
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17006D31 RID: 27953
			// (set) Token: 0x06009A1A RID: 39450 RVA: 0x000DFC5B File Offset: 0x000DDE5B
			public virtual MultiValuedProperty<string> SubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectContainsWords"] = value;
				}
			}

			// Token: 0x17006D32 RID: 27954
			// (set) Token: 0x06009A1B RID: 39451 RVA: 0x000DFC6E File Offset: 0x000DDE6E
			public virtual MultiValuedProperty<string> ExceptIfSubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectContainsWords"] = value;
				}
			}

			// Token: 0x17006D33 RID: 27955
			// (set) Token: 0x06009A1C RID: 39452 RVA: 0x000DFC81 File Offset: 0x000DDE81
			public virtual MultiValuedProperty<string> SubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006D34 RID: 27956
			// (set) Token: 0x06009A1D RID: 39453 RVA: 0x000DFC94 File Offset: 0x000DDE94
			public virtual MultiValuedProperty<string> ExceptIfSubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006D35 RID: 27957
			// (set) Token: 0x06009A1E RID: 39454 RVA: 0x000DFCA7 File Offset: 0x000DDEA7
			public virtual Importance WithImportance
			{
				set
				{
					base.PowerSharpParameters["WithImportance"] = value;
				}
			}

			// Token: 0x17006D36 RID: 27958
			// (set) Token: 0x06009A1F RID: 39455 RVA: 0x000DFCBF File Offset: 0x000DDEBF
			public virtual Importance ExceptIfWithImportance
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithImportance"] = value;
				}
			}

			// Token: 0x17006D37 RID: 27959
			// (set) Token: 0x06009A20 RID: 39456 RVA: 0x000DFCD7 File Offset: 0x000DDED7
			public virtual ByteQuantifiedSize? WithinSizeRangeMaximum
			{
				set
				{
					base.PowerSharpParameters["WithinSizeRangeMaximum"] = value;
				}
			}

			// Token: 0x17006D38 RID: 27960
			// (set) Token: 0x06009A21 RID: 39457 RVA: 0x000DFCEF File Offset: 0x000DDEEF
			public virtual ByteQuantifiedSize? ExceptIfWithinSizeRangeMaximum
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithinSizeRangeMaximum"] = value;
				}
			}

			// Token: 0x17006D39 RID: 27961
			// (set) Token: 0x06009A22 RID: 39458 RVA: 0x000DFD07 File Offset: 0x000DDF07
			public virtual ByteQuantifiedSize? WithinSizeRangeMinimum
			{
				set
				{
					base.PowerSharpParameters["WithinSizeRangeMinimum"] = value;
				}
			}

			// Token: 0x17006D3A RID: 27962
			// (set) Token: 0x06009A23 RID: 39459 RVA: 0x000DFD1F File Offset: 0x000DDF1F
			public virtual ByteQuantifiedSize? ExceptIfWithinSizeRangeMinimum
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithinSizeRangeMinimum"] = value;
				}
			}

			// Token: 0x17006D3B RID: 27963
			// (set) Token: 0x06009A24 RID: 39460 RVA: 0x000DFD37 File Offset: 0x000DDF37
			public virtual Sensitivity WithSensitivity
			{
				set
				{
					base.PowerSharpParameters["WithSensitivity"] = value;
				}
			}

			// Token: 0x17006D3C RID: 27964
			// (set) Token: 0x06009A25 RID: 39461 RVA: 0x000DFD4F File Offset: 0x000DDF4F
			public virtual Sensitivity ExceptIfWithSensitivity
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithSensitivity"] = value;
				}
			}

			// Token: 0x17006D3D RID: 27965
			// (set) Token: 0x06009A26 RID: 39462 RVA: 0x000DFD67 File Offset: 0x000DDF67
			public virtual MultiValuedProperty<string> ApplyCategory
			{
				set
				{
					base.PowerSharpParameters["ApplyCategory"] = value;
				}
			}

			// Token: 0x17006D3E RID: 27966
			// (set) Token: 0x06009A27 RID: 39463 RVA: 0x000DFD7A File Offset: 0x000DDF7A
			public virtual string CopyToFolder
			{
				set
				{
					base.PowerSharpParameters["CopyToFolder"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17006D3F RID: 27967
			// (set) Token: 0x06009A28 RID: 39464 RVA: 0x000DFD98 File Offset: 0x000DDF98
			public virtual bool DeleteMessage
			{
				set
				{
					base.PowerSharpParameters["DeleteMessage"] = value;
				}
			}

			// Token: 0x17006D40 RID: 27968
			// (set) Token: 0x06009A29 RID: 39465 RVA: 0x000DFDB0 File Offset: 0x000DDFB0
			public virtual RecipientIdParameter ForwardAsAttachmentTo
			{
				set
				{
					base.PowerSharpParameters["ForwardAsAttachmentTo"] = value;
				}
			}

			// Token: 0x17006D41 RID: 27969
			// (set) Token: 0x06009A2A RID: 39466 RVA: 0x000DFDC3 File Offset: 0x000DDFC3
			public virtual RecipientIdParameter ForwardTo
			{
				set
				{
					base.PowerSharpParameters["ForwardTo"] = value;
				}
			}

			// Token: 0x17006D42 RID: 27970
			// (set) Token: 0x06009A2B RID: 39467 RVA: 0x000DFDD6 File Offset: 0x000DDFD6
			public virtual bool MarkAsRead
			{
				set
				{
					base.PowerSharpParameters["MarkAsRead"] = value;
				}
			}

			// Token: 0x17006D43 RID: 27971
			// (set) Token: 0x06009A2C RID: 39468 RVA: 0x000DFDEE File Offset: 0x000DDFEE
			public virtual Importance MarkImportance
			{
				set
				{
					base.PowerSharpParameters["MarkImportance"] = value;
				}
			}

			// Token: 0x17006D44 RID: 27972
			// (set) Token: 0x06009A2D RID: 39469 RVA: 0x000DFE06 File Offset: 0x000DE006
			public virtual string MoveToFolder
			{
				set
				{
					base.PowerSharpParameters["MoveToFolder"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17006D45 RID: 27973
			// (set) Token: 0x06009A2E RID: 39470 RVA: 0x000DFE24 File Offset: 0x000DE024
			public virtual RecipientIdParameter RedirectTo
			{
				set
				{
					base.PowerSharpParameters["RedirectTo"] = value;
				}
			}

			// Token: 0x17006D46 RID: 27974
			// (set) Token: 0x06009A2F RID: 39471 RVA: 0x000DFE37 File Offset: 0x000DE037
			public virtual MultiValuedProperty<E164Number> SendTextMessageNotificationTo
			{
				set
				{
					base.PowerSharpParameters["SendTextMessageNotificationTo"] = value;
				}
			}

			// Token: 0x17006D47 RID: 27975
			// (set) Token: 0x06009A30 RID: 39472 RVA: 0x000DFE4A File Offset: 0x000DE04A
			public virtual bool StopProcessingRules
			{
				set
				{
					base.PowerSharpParameters["StopProcessingRules"] = value;
				}
			}

			// Token: 0x17006D48 RID: 27976
			// (set) Token: 0x06009A31 RID: 39473 RVA: 0x000DFE62 File Offset: 0x000DE062
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006D49 RID: 27977
			// (set) Token: 0x06009A32 RID: 39474 RVA: 0x000DFE7A File Offset: 0x000DE07A
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006D4A RID: 27978
			// (set) Token: 0x06009A33 RID: 39475 RVA: 0x000DFE92 File Offset: 0x000DE092
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006D4B RID: 27979
			// (set) Token: 0x06009A34 RID: 39476 RVA: 0x000DFEB0 File Offset: 0x000DE0B0
			public virtual AggregationSubscriptionIdentity FromSubscription
			{
				set
				{
					base.PowerSharpParameters["FromSubscription"] = value;
				}
			}

			// Token: 0x17006D4C RID: 27980
			// (set) Token: 0x06009A35 RID: 39477 RVA: 0x000DFEC3 File Offset: 0x000DE0C3
			public virtual AggregationSubscriptionIdentity ExceptIfFromSubscription
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromSubscription"] = value;
				}
			}

			// Token: 0x17006D4D RID: 27981
			// (set) Token: 0x06009A36 RID: 39478 RVA: 0x000DFED6 File Offset: 0x000DE0D6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006D4E RID: 27982
			// (set) Token: 0x06009A37 RID: 39479 RVA: 0x000DFEF4 File Offset: 0x000DE0F4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006D4F RID: 27983
			// (set) Token: 0x06009A38 RID: 39480 RVA: 0x000DFF07 File Offset: 0x000DE107
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006D50 RID: 27984
			// (set) Token: 0x06009A39 RID: 39481 RVA: 0x000DFF1F File Offset: 0x000DE11F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006D51 RID: 27985
			// (set) Token: 0x06009A3A RID: 39482 RVA: 0x000DFF37 File Offset: 0x000DE137
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006D52 RID: 27986
			// (set) Token: 0x06009A3B RID: 39483 RVA: 0x000DFF4F File Offset: 0x000DE14F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006D53 RID: 27987
			// (set) Token: 0x06009A3C RID: 39484 RVA: 0x000DFF67 File Offset: 0x000DE167
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C51 RID: 3153
		public class FromMessageParameters : ParametersBase
		{
			// Token: 0x17006D54 RID: 27988
			// (set) Token: 0x06009A3E RID: 39486 RVA: 0x000DFF87 File Offset: 0x000DE187
			public virtual string FromMessageId
			{
				set
				{
					base.PowerSharpParameters["FromMessageId"] = ((value != null) ? new MailboxStoreObjectIdParameter(value) : null);
				}
			}

			// Token: 0x17006D55 RID: 27989
			// (set) Token: 0x06009A3F RID: 39487 RVA: 0x000DFFA5 File Offset: 0x000DE1A5
			public virtual SwitchParameter ValidateOnly
			{
				set
				{
					base.PowerSharpParameters["ValidateOnly"] = value;
				}
			}

			// Token: 0x17006D56 RID: 27990
			// (set) Token: 0x06009A40 RID: 39488 RVA: 0x000DFFBD File Offset: 0x000DE1BD
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006D57 RID: 27991
			// (set) Token: 0x06009A41 RID: 39489 RVA: 0x000DFFD5 File Offset: 0x000DE1D5
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006D58 RID: 27992
			// (set) Token: 0x06009A42 RID: 39490 RVA: 0x000DFFED File Offset: 0x000DE1ED
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006D59 RID: 27993
			// (set) Token: 0x06009A43 RID: 39491 RVA: 0x000E000B File Offset: 0x000DE20B
			public virtual AggregationSubscriptionIdentity FromSubscription
			{
				set
				{
					base.PowerSharpParameters["FromSubscription"] = value;
				}
			}

			// Token: 0x17006D5A RID: 27994
			// (set) Token: 0x06009A44 RID: 39492 RVA: 0x000E001E File Offset: 0x000DE21E
			public virtual AggregationSubscriptionIdentity ExceptIfFromSubscription
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromSubscription"] = value;
				}
			}

			// Token: 0x17006D5B RID: 27995
			// (set) Token: 0x06009A45 RID: 39493 RVA: 0x000E0031 File Offset: 0x000DE231
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006D5C RID: 27996
			// (set) Token: 0x06009A46 RID: 39494 RVA: 0x000E004F File Offset: 0x000DE24F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006D5D RID: 27997
			// (set) Token: 0x06009A47 RID: 39495 RVA: 0x000E0062 File Offset: 0x000DE262
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006D5E RID: 27998
			// (set) Token: 0x06009A48 RID: 39496 RVA: 0x000E007A File Offset: 0x000DE27A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006D5F RID: 27999
			// (set) Token: 0x06009A49 RID: 39497 RVA: 0x000E0092 File Offset: 0x000DE292
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006D60 RID: 28000
			// (set) Token: 0x06009A4A RID: 39498 RVA: 0x000E00AA File Offset: 0x000DE2AA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006D61 RID: 28001
			// (set) Token: 0x06009A4B RID: 39499 RVA: 0x000E00C2 File Offset: 0x000DE2C2
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
