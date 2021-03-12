using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000400 RID: 1024
	public class NewMailboxSearchCommand : SyntheticCommandWithPipelineInput<MailboxDiscoverySearch, MailboxDiscoverySearch>
	{
		// Token: 0x06003C9F RID: 15519 RVA: 0x00066770 File Offset: 0x00064970
		private NewMailboxSearchCommand() : base("New-MailboxSearch")
		{
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x0006677D File Offset: 0x0006497D
		public NewMailboxSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x0006678C File Offset: 0x0006498C
		public virtual NewMailboxSearchCommand SetParameters(NewMailboxSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000401 RID: 1025
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002058 RID: 8280
			// (set) Token: 0x06003CA2 RID: 15522 RVA: 0x00066796 File Offset: 0x00064996
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002059 RID: 8281
			// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x000667A9 File Offset: 0x000649A9
			public virtual RecipientIdParameter SourceMailboxes
			{
				set
				{
					base.PowerSharpParameters["SourceMailboxes"] = value;
				}
			}

			// Token: 0x1700205A RID: 8282
			// (set) Token: 0x06003CA4 RID: 15524 RVA: 0x000667BC File Offset: 0x000649BC
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700205B RID: 8283
			// (set) Token: 0x06003CA5 RID: 15525 RVA: 0x000667DA File Offset: 0x000649DA
			public virtual string SearchQuery
			{
				set
				{
					base.PowerSharpParameters["SearchQuery"] = value;
				}
			}

			// Token: 0x1700205C RID: 8284
			// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x000667ED File Offset: 0x000649ED
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x1700205D RID: 8285
			// (set) Token: 0x06003CA7 RID: 15527 RVA: 0x00066800 File Offset: 0x00064A00
			public virtual PublicFolderIdParameter PublicFolderSources
			{
				set
				{
					base.PowerSharpParameters["PublicFolderSources"] = value;
				}
			}

			// Token: 0x1700205E RID: 8286
			// (set) Token: 0x06003CA8 RID: 15528 RVA: 0x00066813 File Offset: 0x00064A13
			public virtual bool AllPublicFolderSources
			{
				set
				{
					base.PowerSharpParameters["AllPublicFolderSources"] = value;
				}
			}

			// Token: 0x1700205F RID: 8287
			// (set) Token: 0x06003CA9 RID: 15529 RVA: 0x0006682B File Offset: 0x00064A2B
			public virtual bool AllSourceMailboxes
			{
				set
				{
					base.PowerSharpParameters["AllSourceMailboxes"] = value;
				}
			}

			// Token: 0x17002060 RID: 8288
			// (set) Token: 0x06003CAA RID: 15530 RVA: 0x00066843 File Offset: 0x00064A43
			public virtual string Senders
			{
				set
				{
					base.PowerSharpParameters["Senders"] = value;
				}
			}

			// Token: 0x17002061 RID: 8289
			// (set) Token: 0x06003CAB RID: 15531 RVA: 0x00066856 File Offset: 0x00064A56
			public virtual string Recipients
			{
				set
				{
					base.PowerSharpParameters["Recipients"] = value;
				}
			}

			// Token: 0x17002062 RID: 8290
			// (set) Token: 0x06003CAC RID: 15532 RVA: 0x00066869 File Offset: 0x00064A69
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17002063 RID: 8291
			// (set) Token: 0x06003CAD RID: 15533 RVA: 0x00066881 File Offset: 0x00064A81
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17002064 RID: 8292
			// (set) Token: 0x06003CAE RID: 15534 RVA: 0x00066899 File Offset: 0x00064A99
			public virtual KindKeyword MessageTypes
			{
				set
				{
					base.PowerSharpParameters["MessageTypes"] = value;
				}
			}

			// Token: 0x17002065 RID: 8293
			// (set) Token: 0x06003CAF RID: 15535 RVA: 0x000668B1 File Offset: 0x00064AB1
			public virtual LoggingLevel LogLevel
			{
				set
				{
					base.PowerSharpParameters["LogLevel"] = value;
				}
			}

			// Token: 0x17002066 RID: 8294
			// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x000668C9 File Offset: 0x00064AC9
			public virtual RecipientIdParameter StatusMailRecipients
			{
				set
				{
					base.PowerSharpParameters["StatusMailRecipients"] = value;
				}
			}

			// Token: 0x17002067 RID: 8295
			// (set) Token: 0x06003CB1 RID: 15537 RVA: 0x000668DC File Offset: 0x00064ADC
			public virtual SwitchParameter IncludeUnsearchableItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnsearchableItems"] = value;
				}
			}

			// Token: 0x17002068 RID: 8296
			// (set) Token: 0x06003CB2 RID: 15538 RVA: 0x000668F4 File Offset: 0x00064AF4
			public virtual SwitchParameter EstimateOnly
			{
				set
				{
					base.PowerSharpParameters["EstimateOnly"] = value;
				}
			}

			// Token: 0x17002069 RID: 8297
			// (set) Token: 0x06003CB3 RID: 15539 RVA: 0x0006690C File Offset: 0x00064B0C
			public virtual bool ExcludeDuplicateMessages
			{
				set
				{
					base.PowerSharpParameters["ExcludeDuplicateMessages"] = value;
				}
			}

			// Token: 0x1700206A RID: 8298
			// (set) Token: 0x06003CB4 RID: 15540 RVA: 0x00066924 File Offset: 0x00064B24
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700206B RID: 8299
			// (set) Token: 0x06003CB5 RID: 15541 RVA: 0x0006693C File Offset: 0x00064B3C
			public virtual SwitchParameter IncludeKeywordStatistics
			{
				set
				{
					base.PowerSharpParameters["IncludeKeywordStatistics"] = value;
				}
			}

			// Token: 0x1700206C RID: 8300
			// (set) Token: 0x06003CB6 RID: 15542 RVA: 0x00066954 File Offset: 0x00064B54
			public virtual bool InPlaceHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldEnabled"] = value;
				}
			}

			// Token: 0x1700206D RID: 8301
			// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x0006696C File Offset: 0x00064B6C
			public virtual Unlimited<EnhancedTimeSpan> ItemHoldPeriod
			{
				set
				{
					base.PowerSharpParameters["ItemHoldPeriod"] = value;
				}
			}

			// Token: 0x1700206E RID: 8302
			// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x00066984 File Offset: 0x00064B84
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x1700206F RID: 8303
			// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x00066997 File Offset: 0x00064B97
			public virtual string InPlaceHoldIdentity
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldIdentity"] = value;
				}
			}

			// Token: 0x17002070 RID: 8304
			// (set) Token: 0x06003CBA RID: 15546 RVA: 0x000669AA File Offset: 0x00064BAA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002071 RID: 8305
			// (set) Token: 0x06003CBB RID: 15547 RVA: 0x000669BD File Offset: 0x00064BBD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002072 RID: 8306
			// (set) Token: 0x06003CBC RID: 15548 RVA: 0x000669D5 File Offset: 0x00064BD5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002073 RID: 8307
			// (set) Token: 0x06003CBD RID: 15549 RVA: 0x000669ED File Offset: 0x00064BED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002074 RID: 8308
			// (set) Token: 0x06003CBE RID: 15550 RVA: 0x00066A05 File Offset: 0x00064C05
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002075 RID: 8309
			// (set) Token: 0x06003CBF RID: 15551 RVA: 0x00066A1D File Offset: 0x00064C1D
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
