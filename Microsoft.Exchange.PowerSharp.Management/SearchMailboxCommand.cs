using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003FB RID: 1019
	public class SearchMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06003C5A RID: 15450 RVA: 0x00066193 File Offset: 0x00064393
		private SearchMailboxCommand() : base("Search-Mailbox")
		{
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x000661A0 File Offset: 0x000643A0
		public SearchMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x000661AF File Offset: 0x000643AF
		public virtual SearchMailboxCommand SetParameters(SearchMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x000661B9 File Offset: 0x000643B9
		public virtual SearchMailboxCommand SetParameters(SearchMailboxCommand.MailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x000661C3 File Offset: 0x000643C3
		public virtual SearchMailboxCommand SetParameters(SearchMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x000661CD File Offset: 0x000643CD
		public virtual SearchMailboxCommand SetParameters(SearchMailboxCommand.EstimateResultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003FC RID: 1020
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700201D RID: 8221
			// (set) Token: 0x06003C60 RID: 15456 RVA: 0x000661D7 File Offset: 0x000643D7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700201E RID: 8222
			// (set) Token: 0x06003C61 RID: 15457 RVA: 0x000661F5 File Offset: 0x000643F5
			public virtual string SearchQuery
			{
				set
				{
					base.PowerSharpParameters["SearchQuery"] = value;
				}
			}

			// Token: 0x1700201F RID: 8223
			// (set) Token: 0x06003C62 RID: 15458 RVA: 0x00066208 File Offset: 0x00064408
			public virtual SwitchParameter SearchDumpster
			{
				set
				{
					base.PowerSharpParameters["SearchDumpster"] = value;
				}
			}

			// Token: 0x17002020 RID: 8224
			// (set) Token: 0x06003C63 RID: 15459 RVA: 0x00066220 File Offset: 0x00064420
			public virtual SwitchParameter SearchDumpsterOnly
			{
				set
				{
					base.PowerSharpParameters["SearchDumpsterOnly"] = value;
				}
			}

			// Token: 0x17002021 RID: 8225
			// (set) Token: 0x06003C64 RID: 15460 RVA: 0x00066238 File Offset: 0x00064438
			public virtual SwitchParameter IncludeUnsearchableItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnsearchableItems"] = value;
				}
			}

			// Token: 0x17002022 RID: 8226
			// (set) Token: 0x06003C65 RID: 15461 RVA: 0x00066250 File Offset: 0x00064450
			public virtual SwitchParameter DoNotIncludeArchive
			{
				set
				{
					base.PowerSharpParameters["DoNotIncludeArchive"] = value;
				}
			}

			// Token: 0x17002023 RID: 8227
			// (set) Token: 0x06003C66 RID: 15462 RVA: 0x00066268 File Offset: 0x00064468
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002024 RID: 8228
			// (set) Token: 0x06003C67 RID: 15463 RVA: 0x00066280 File Offset: 0x00064480
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002025 RID: 8229
			// (set) Token: 0x06003C68 RID: 15464 RVA: 0x00066293 File Offset: 0x00064493
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002026 RID: 8230
			// (set) Token: 0x06003C69 RID: 15465 RVA: 0x000662AB File Offset: 0x000644AB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002027 RID: 8231
			// (set) Token: 0x06003C6A RID: 15466 RVA: 0x000662C3 File Offset: 0x000644C3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002028 RID: 8232
			// (set) Token: 0x06003C6B RID: 15467 RVA: 0x000662DB File Offset: 0x000644DB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002029 RID: 8233
			// (set) Token: 0x06003C6C RID: 15468 RVA: 0x000662F3 File Offset: 0x000644F3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003FD RID: 1021
		public class MailboxParameters : ParametersBase
		{
			// Token: 0x1700202A RID: 8234
			// (set) Token: 0x06003C6E RID: 15470 RVA: 0x00066313 File Offset: 0x00064513
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700202B RID: 8235
			// (set) Token: 0x06003C6F RID: 15471 RVA: 0x00066331 File Offset: 0x00064531
			public virtual string TargetFolder
			{
				set
				{
					base.PowerSharpParameters["TargetFolder"] = value;
				}
			}

			// Token: 0x1700202C RID: 8236
			// (set) Token: 0x06003C70 RID: 15472 RVA: 0x00066344 File Offset: 0x00064544
			public virtual SwitchParameter DeleteContent
			{
				set
				{
					base.PowerSharpParameters["DeleteContent"] = value;
				}
			}

			// Token: 0x1700202D RID: 8237
			// (set) Token: 0x06003C71 RID: 15473 RVA: 0x0006635C File Offset: 0x0006455C
			public virtual LoggingLevel LogLevel
			{
				set
				{
					base.PowerSharpParameters["LogLevel"] = value;
				}
			}

			// Token: 0x1700202E RID: 8238
			// (set) Token: 0x06003C72 RID: 15474 RVA: 0x00066374 File Offset: 0x00064574
			public virtual SwitchParameter LogOnly
			{
				set
				{
					base.PowerSharpParameters["LogOnly"] = value;
				}
			}

			// Token: 0x1700202F RID: 8239
			// (set) Token: 0x06003C73 RID: 15475 RVA: 0x0006638C File Offset: 0x0006458C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17002030 RID: 8240
			// (set) Token: 0x06003C74 RID: 15476 RVA: 0x000663AA File Offset: 0x000645AA
			public virtual string SearchQuery
			{
				set
				{
					base.PowerSharpParameters["SearchQuery"] = value;
				}
			}

			// Token: 0x17002031 RID: 8241
			// (set) Token: 0x06003C75 RID: 15477 RVA: 0x000663BD File Offset: 0x000645BD
			public virtual SwitchParameter SearchDumpster
			{
				set
				{
					base.PowerSharpParameters["SearchDumpster"] = value;
				}
			}

			// Token: 0x17002032 RID: 8242
			// (set) Token: 0x06003C76 RID: 15478 RVA: 0x000663D5 File Offset: 0x000645D5
			public virtual SwitchParameter SearchDumpsterOnly
			{
				set
				{
					base.PowerSharpParameters["SearchDumpsterOnly"] = value;
				}
			}

			// Token: 0x17002033 RID: 8243
			// (set) Token: 0x06003C77 RID: 15479 RVA: 0x000663ED File Offset: 0x000645ED
			public virtual SwitchParameter IncludeUnsearchableItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnsearchableItems"] = value;
				}
			}

			// Token: 0x17002034 RID: 8244
			// (set) Token: 0x06003C78 RID: 15480 RVA: 0x00066405 File Offset: 0x00064605
			public virtual SwitchParameter DoNotIncludeArchive
			{
				set
				{
					base.PowerSharpParameters["DoNotIncludeArchive"] = value;
				}
			}

			// Token: 0x17002035 RID: 8245
			// (set) Token: 0x06003C79 RID: 15481 RVA: 0x0006641D File Offset: 0x0006461D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002036 RID: 8246
			// (set) Token: 0x06003C7A RID: 15482 RVA: 0x00066435 File Offset: 0x00064635
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002037 RID: 8247
			// (set) Token: 0x06003C7B RID: 15483 RVA: 0x00066448 File Offset: 0x00064648
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002038 RID: 8248
			// (set) Token: 0x06003C7C RID: 15484 RVA: 0x00066460 File Offset: 0x00064660
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002039 RID: 8249
			// (set) Token: 0x06003C7D RID: 15485 RVA: 0x00066478 File Offset: 0x00064678
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700203A RID: 8250
			// (set) Token: 0x06003C7E RID: 15486 RVA: 0x00066490 File Offset: 0x00064690
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700203B RID: 8251
			// (set) Token: 0x06003C7F RID: 15487 RVA: 0x000664A8 File Offset: 0x000646A8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003FE RID: 1022
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700203C RID: 8252
			// (set) Token: 0x06003C81 RID: 15489 RVA: 0x000664C8 File Offset: 0x000646C8
			public virtual SwitchParameter DeleteContent
			{
				set
				{
					base.PowerSharpParameters["DeleteContent"] = value;
				}
			}

			// Token: 0x1700203D RID: 8253
			// (set) Token: 0x06003C82 RID: 15490 RVA: 0x000664E0 File Offset: 0x000646E0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700203E RID: 8254
			// (set) Token: 0x06003C83 RID: 15491 RVA: 0x000664FE File Offset: 0x000646FE
			public virtual string SearchQuery
			{
				set
				{
					base.PowerSharpParameters["SearchQuery"] = value;
				}
			}

			// Token: 0x1700203F RID: 8255
			// (set) Token: 0x06003C84 RID: 15492 RVA: 0x00066511 File Offset: 0x00064711
			public virtual SwitchParameter SearchDumpster
			{
				set
				{
					base.PowerSharpParameters["SearchDumpster"] = value;
				}
			}

			// Token: 0x17002040 RID: 8256
			// (set) Token: 0x06003C85 RID: 15493 RVA: 0x00066529 File Offset: 0x00064729
			public virtual SwitchParameter SearchDumpsterOnly
			{
				set
				{
					base.PowerSharpParameters["SearchDumpsterOnly"] = value;
				}
			}

			// Token: 0x17002041 RID: 8257
			// (set) Token: 0x06003C86 RID: 15494 RVA: 0x00066541 File Offset: 0x00064741
			public virtual SwitchParameter IncludeUnsearchableItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnsearchableItems"] = value;
				}
			}

			// Token: 0x17002042 RID: 8258
			// (set) Token: 0x06003C87 RID: 15495 RVA: 0x00066559 File Offset: 0x00064759
			public virtual SwitchParameter DoNotIncludeArchive
			{
				set
				{
					base.PowerSharpParameters["DoNotIncludeArchive"] = value;
				}
			}

			// Token: 0x17002043 RID: 8259
			// (set) Token: 0x06003C88 RID: 15496 RVA: 0x00066571 File Offset: 0x00064771
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002044 RID: 8260
			// (set) Token: 0x06003C89 RID: 15497 RVA: 0x00066589 File Offset: 0x00064789
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002045 RID: 8261
			// (set) Token: 0x06003C8A RID: 15498 RVA: 0x0006659C File Offset: 0x0006479C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002046 RID: 8262
			// (set) Token: 0x06003C8B RID: 15499 RVA: 0x000665B4 File Offset: 0x000647B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002047 RID: 8263
			// (set) Token: 0x06003C8C RID: 15500 RVA: 0x000665CC File Offset: 0x000647CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002048 RID: 8264
			// (set) Token: 0x06003C8D RID: 15501 RVA: 0x000665E4 File Offset: 0x000647E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002049 RID: 8265
			// (set) Token: 0x06003C8E RID: 15502 RVA: 0x000665FC File Offset: 0x000647FC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003FF RID: 1023
		public class EstimateResultParameters : ParametersBase
		{
			// Token: 0x1700204A RID: 8266
			// (set) Token: 0x06003C90 RID: 15504 RVA: 0x0006661C File Offset: 0x0006481C
			public virtual SwitchParameter EstimateResultOnly
			{
				set
				{
					base.PowerSharpParameters["EstimateResultOnly"] = value;
				}
			}

			// Token: 0x1700204B RID: 8267
			// (set) Token: 0x06003C91 RID: 15505 RVA: 0x00066634 File Offset: 0x00064834
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700204C RID: 8268
			// (set) Token: 0x06003C92 RID: 15506 RVA: 0x00066652 File Offset: 0x00064852
			public virtual string SearchQuery
			{
				set
				{
					base.PowerSharpParameters["SearchQuery"] = value;
				}
			}

			// Token: 0x1700204D RID: 8269
			// (set) Token: 0x06003C93 RID: 15507 RVA: 0x00066665 File Offset: 0x00064865
			public virtual SwitchParameter SearchDumpster
			{
				set
				{
					base.PowerSharpParameters["SearchDumpster"] = value;
				}
			}

			// Token: 0x1700204E RID: 8270
			// (set) Token: 0x06003C94 RID: 15508 RVA: 0x0006667D File Offset: 0x0006487D
			public virtual SwitchParameter SearchDumpsterOnly
			{
				set
				{
					base.PowerSharpParameters["SearchDumpsterOnly"] = value;
				}
			}

			// Token: 0x1700204F RID: 8271
			// (set) Token: 0x06003C95 RID: 15509 RVA: 0x00066695 File Offset: 0x00064895
			public virtual SwitchParameter IncludeUnsearchableItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnsearchableItems"] = value;
				}
			}

			// Token: 0x17002050 RID: 8272
			// (set) Token: 0x06003C96 RID: 15510 RVA: 0x000666AD File Offset: 0x000648AD
			public virtual SwitchParameter DoNotIncludeArchive
			{
				set
				{
					base.PowerSharpParameters["DoNotIncludeArchive"] = value;
				}
			}

			// Token: 0x17002051 RID: 8273
			// (set) Token: 0x06003C97 RID: 15511 RVA: 0x000666C5 File Offset: 0x000648C5
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002052 RID: 8274
			// (set) Token: 0x06003C98 RID: 15512 RVA: 0x000666DD File Offset: 0x000648DD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002053 RID: 8275
			// (set) Token: 0x06003C99 RID: 15513 RVA: 0x000666F0 File Offset: 0x000648F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002054 RID: 8276
			// (set) Token: 0x06003C9A RID: 15514 RVA: 0x00066708 File Offset: 0x00064908
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002055 RID: 8277
			// (set) Token: 0x06003C9B RID: 15515 RVA: 0x00066720 File Offset: 0x00064920
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002056 RID: 8278
			// (set) Token: 0x06003C9C RID: 15516 RVA: 0x00066738 File Offset: 0x00064938
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002057 RID: 8279
			// (set) Token: 0x06003C9D RID: 15517 RVA: 0x00066750 File Offset: 0x00064950
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
