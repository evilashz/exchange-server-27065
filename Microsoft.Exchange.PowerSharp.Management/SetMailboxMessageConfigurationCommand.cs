using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000492 RID: 1170
	public class SetMailboxMessageConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxMessageConfiguration>
	{
		// Token: 0x060041D4 RID: 16852 RVA: 0x0006D280 File Offset: 0x0006B480
		private SetMailboxMessageConfigurationCommand() : base("Set-MailboxMessageConfiguration")
		{
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x0006D28D File Offset: 0x0006B48D
		public SetMailboxMessageConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x0006D29C File Offset: 0x0006B49C
		public virtual SetMailboxMessageConfigurationCommand SetParameters(SetMailboxMessageConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x0006D2A6 File Offset: 0x0006B4A6
		public virtual SetMailboxMessageConfigurationCommand SetParameters(SetMailboxMessageConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000493 RID: 1171
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002469 RID: 9321
			// (set) Token: 0x060041D8 RID: 16856 RVA: 0x0006D2B0 File Offset: 0x0006B4B0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700246A RID: 9322
			// (set) Token: 0x060041D9 RID: 16857 RVA: 0x0006D2CE File Offset: 0x0006B4CE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700246B RID: 9323
			// (set) Token: 0x060041DA RID: 16858 RVA: 0x0006D2E6 File Offset: 0x0006B4E6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700246C RID: 9324
			// (set) Token: 0x060041DB RID: 16859 RVA: 0x0006D2F9 File Offset: 0x0006B4F9
			public virtual AfterMoveOrDeleteBehavior AfterMoveOrDeleteBehavior
			{
				set
				{
					base.PowerSharpParameters["AfterMoveOrDeleteBehavior"] = value;
				}
			}

			// Token: 0x1700246D RID: 9325
			// (set) Token: 0x060041DC RID: 16860 RVA: 0x0006D311 File Offset: 0x0006B511
			public virtual NewItemNotification NewItemNotification
			{
				set
				{
					base.PowerSharpParameters["NewItemNotification"] = value;
				}
			}

			// Token: 0x1700246E RID: 9326
			// (set) Token: 0x060041DD RID: 16861 RVA: 0x0006D329 File Offset: 0x0006B529
			public virtual bool EmptyDeletedItemsOnLogoff
			{
				set
				{
					base.PowerSharpParameters["EmptyDeletedItemsOnLogoff"] = value;
				}
			}

			// Token: 0x1700246F RID: 9327
			// (set) Token: 0x060041DE RID: 16862 RVA: 0x0006D341 File Offset: 0x0006B541
			public virtual bool AutoAddSignature
			{
				set
				{
					base.PowerSharpParameters["AutoAddSignature"] = value;
				}
			}

			// Token: 0x17002470 RID: 9328
			// (set) Token: 0x060041DF RID: 16863 RVA: 0x0006D359 File Offset: 0x0006B559
			public virtual string SignatureText
			{
				set
				{
					base.PowerSharpParameters["SignatureText"] = value;
				}
			}

			// Token: 0x17002471 RID: 9329
			// (set) Token: 0x060041E0 RID: 16864 RVA: 0x0006D36C File Offset: 0x0006B56C
			public virtual string SignatureHtml
			{
				set
				{
					base.PowerSharpParameters["SignatureHtml"] = value;
				}
			}

			// Token: 0x17002472 RID: 9330
			// (set) Token: 0x060041E1 RID: 16865 RVA: 0x0006D37F File Offset: 0x0006B57F
			public virtual bool AutoAddSignatureOnMobile
			{
				set
				{
					base.PowerSharpParameters["AutoAddSignatureOnMobile"] = value;
				}
			}

			// Token: 0x17002473 RID: 9331
			// (set) Token: 0x060041E2 RID: 16866 RVA: 0x0006D397 File Offset: 0x0006B597
			public virtual string SignatureTextOnMobile
			{
				set
				{
					base.PowerSharpParameters["SignatureTextOnMobile"] = value;
				}
			}

			// Token: 0x17002474 RID: 9332
			// (set) Token: 0x060041E3 RID: 16867 RVA: 0x0006D3AA File Offset: 0x0006B5AA
			public virtual bool UseDefaultSignatureOnMobile
			{
				set
				{
					base.PowerSharpParameters["UseDefaultSignatureOnMobile"] = value;
				}
			}

			// Token: 0x17002475 RID: 9333
			// (set) Token: 0x060041E4 RID: 16868 RVA: 0x0006D3C2 File Offset: 0x0006B5C2
			public virtual string DefaultFontName
			{
				set
				{
					base.PowerSharpParameters["DefaultFontName"] = value;
				}
			}

			// Token: 0x17002476 RID: 9334
			// (set) Token: 0x060041E5 RID: 16869 RVA: 0x0006D3D5 File Offset: 0x0006B5D5
			public virtual int DefaultFontSize
			{
				set
				{
					base.PowerSharpParameters["DefaultFontSize"] = value;
				}
			}

			// Token: 0x17002477 RID: 9335
			// (set) Token: 0x060041E6 RID: 16870 RVA: 0x0006D3ED File Offset: 0x0006B5ED
			public virtual string DefaultFontColor
			{
				set
				{
					base.PowerSharpParameters["DefaultFontColor"] = value;
				}
			}

			// Token: 0x17002478 RID: 9336
			// (set) Token: 0x060041E7 RID: 16871 RVA: 0x0006D400 File Offset: 0x0006B600
			public virtual FontFlags DefaultFontFlags
			{
				set
				{
					base.PowerSharpParameters["DefaultFontFlags"] = value;
				}
			}

			// Token: 0x17002479 RID: 9337
			// (set) Token: 0x060041E8 RID: 16872 RVA: 0x0006D418 File Offset: 0x0006B618
			public virtual bool AlwaysShowBcc
			{
				set
				{
					base.PowerSharpParameters["AlwaysShowBcc"] = value;
				}
			}

			// Token: 0x1700247A RID: 9338
			// (set) Token: 0x060041E9 RID: 16873 RVA: 0x0006D430 File Offset: 0x0006B630
			public virtual bool AlwaysShowFrom
			{
				set
				{
					base.PowerSharpParameters["AlwaysShowFrom"] = value;
				}
			}

			// Token: 0x1700247B RID: 9339
			// (set) Token: 0x060041EA RID: 16874 RVA: 0x0006D448 File Offset: 0x0006B648
			public virtual MailFormat DefaultFormat
			{
				set
				{
					base.PowerSharpParameters["DefaultFormat"] = value;
				}
			}

			// Token: 0x1700247C RID: 9340
			// (set) Token: 0x060041EB RID: 16875 RVA: 0x0006D460 File Offset: 0x0006B660
			public virtual ReadReceiptResponse ReadReceiptResponse
			{
				set
				{
					base.PowerSharpParameters["ReadReceiptResponse"] = value;
				}
			}

			// Token: 0x1700247D RID: 9341
			// (set) Token: 0x060041EC RID: 16876 RVA: 0x0006D478 File Offset: 0x0006B678
			public virtual PreviewMarkAsReadBehavior PreviewMarkAsReadBehavior
			{
				set
				{
					base.PowerSharpParameters["PreviewMarkAsReadBehavior"] = value;
				}
			}

			// Token: 0x1700247E RID: 9342
			// (set) Token: 0x060041ED RID: 16877 RVA: 0x0006D490 File Offset: 0x0006B690
			public virtual int PreviewMarkAsReadDelaytime
			{
				set
				{
					base.PowerSharpParameters["PreviewMarkAsReadDelaytime"] = value;
				}
			}

			// Token: 0x1700247F RID: 9343
			// (set) Token: 0x060041EE RID: 16878 RVA: 0x0006D4A8 File Offset: 0x0006B6A8
			public virtual ConversationSortOrder ConversationSortOrder
			{
				set
				{
					base.PowerSharpParameters["ConversationSortOrder"] = value;
				}
			}

			// Token: 0x17002480 RID: 9344
			// (set) Token: 0x060041EF RID: 16879 RVA: 0x0006D4C0 File Offset: 0x0006B6C0
			public virtual bool ShowConversationAsTree
			{
				set
				{
					base.PowerSharpParameters["ShowConversationAsTree"] = value;
				}
			}

			// Token: 0x17002481 RID: 9345
			// (set) Token: 0x060041F0 RID: 16880 RVA: 0x0006D4D8 File Offset: 0x0006B6D8
			public virtual bool HideDeletedItems
			{
				set
				{
					base.PowerSharpParameters["HideDeletedItems"] = value;
				}
			}

			// Token: 0x17002482 RID: 9346
			// (set) Token: 0x060041F1 RID: 16881 RVA: 0x0006D4F0 File Offset: 0x0006B6F0
			public virtual string SendAddressDefault
			{
				set
				{
					base.PowerSharpParameters["SendAddressDefault"] = value;
				}
			}

			// Token: 0x17002483 RID: 9347
			// (set) Token: 0x060041F2 RID: 16882 RVA: 0x0006D503 File Offset: 0x0006B703
			public virtual EmailComposeMode EmailComposeMode
			{
				set
				{
					base.PowerSharpParameters["EmailComposeMode"] = value;
				}
			}

			// Token: 0x17002484 RID: 9348
			// (set) Token: 0x060041F3 RID: 16883 RVA: 0x0006D51B File Offset: 0x0006B71B
			public virtual bool CheckForForgottenAttachments
			{
				set
				{
					base.PowerSharpParameters["CheckForForgottenAttachments"] = value;
				}
			}

			// Token: 0x17002485 RID: 9349
			// (set) Token: 0x060041F4 RID: 16884 RVA: 0x0006D533 File Offset: 0x0006B733
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002486 RID: 9350
			// (set) Token: 0x060041F5 RID: 16885 RVA: 0x0006D54B File Offset: 0x0006B74B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002487 RID: 9351
			// (set) Token: 0x060041F6 RID: 16886 RVA: 0x0006D563 File Offset: 0x0006B763
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002488 RID: 9352
			// (set) Token: 0x060041F7 RID: 16887 RVA: 0x0006D57B File Offset: 0x0006B77B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002489 RID: 9353
			// (set) Token: 0x060041F8 RID: 16888 RVA: 0x0006D593 File Offset: 0x0006B793
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000494 RID: 1172
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700248A RID: 9354
			// (set) Token: 0x060041FA RID: 16890 RVA: 0x0006D5B3 File Offset: 0x0006B7B3
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700248B RID: 9355
			// (set) Token: 0x060041FB RID: 16891 RVA: 0x0006D5CB File Offset: 0x0006B7CB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700248C RID: 9356
			// (set) Token: 0x060041FC RID: 16892 RVA: 0x0006D5DE File Offset: 0x0006B7DE
			public virtual AfterMoveOrDeleteBehavior AfterMoveOrDeleteBehavior
			{
				set
				{
					base.PowerSharpParameters["AfterMoveOrDeleteBehavior"] = value;
				}
			}

			// Token: 0x1700248D RID: 9357
			// (set) Token: 0x060041FD RID: 16893 RVA: 0x0006D5F6 File Offset: 0x0006B7F6
			public virtual NewItemNotification NewItemNotification
			{
				set
				{
					base.PowerSharpParameters["NewItemNotification"] = value;
				}
			}

			// Token: 0x1700248E RID: 9358
			// (set) Token: 0x060041FE RID: 16894 RVA: 0x0006D60E File Offset: 0x0006B80E
			public virtual bool EmptyDeletedItemsOnLogoff
			{
				set
				{
					base.PowerSharpParameters["EmptyDeletedItemsOnLogoff"] = value;
				}
			}

			// Token: 0x1700248F RID: 9359
			// (set) Token: 0x060041FF RID: 16895 RVA: 0x0006D626 File Offset: 0x0006B826
			public virtual bool AutoAddSignature
			{
				set
				{
					base.PowerSharpParameters["AutoAddSignature"] = value;
				}
			}

			// Token: 0x17002490 RID: 9360
			// (set) Token: 0x06004200 RID: 16896 RVA: 0x0006D63E File Offset: 0x0006B83E
			public virtual string SignatureText
			{
				set
				{
					base.PowerSharpParameters["SignatureText"] = value;
				}
			}

			// Token: 0x17002491 RID: 9361
			// (set) Token: 0x06004201 RID: 16897 RVA: 0x0006D651 File Offset: 0x0006B851
			public virtual string SignatureHtml
			{
				set
				{
					base.PowerSharpParameters["SignatureHtml"] = value;
				}
			}

			// Token: 0x17002492 RID: 9362
			// (set) Token: 0x06004202 RID: 16898 RVA: 0x0006D664 File Offset: 0x0006B864
			public virtual bool AutoAddSignatureOnMobile
			{
				set
				{
					base.PowerSharpParameters["AutoAddSignatureOnMobile"] = value;
				}
			}

			// Token: 0x17002493 RID: 9363
			// (set) Token: 0x06004203 RID: 16899 RVA: 0x0006D67C File Offset: 0x0006B87C
			public virtual string SignatureTextOnMobile
			{
				set
				{
					base.PowerSharpParameters["SignatureTextOnMobile"] = value;
				}
			}

			// Token: 0x17002494 RID: 9364
			// (set) Token: 0x06004204 RID: 16900 RVA: 0x0006D68F File Offset: 0x0006B88F
			public virtual bool UseDefaultSignatureOnMobile
			{
				set
				{
					base.PowerSharpParameters["UseDefaultSignatureOnMobile"] = value;
				}
			}

			// Token: 0x17002495 RID: 9365
			// (set) Token: 0x06004205 RID: 16901 RVA: 0x0006D6A7 File Offset: 0x0006B8A7
			public virtual string DefaultFontName
			{
				set
				{
					base.PowerSharpParameters["DefaultFontName"] = value;
				}
			}

			// Token: 0x17002496 RID: 9366
			// (set) Token: 0x06004206 RID: 16902 RVA: 0x0006D6BA File Offset: 0x0006B8BA
			public virtual int DefaultFontSize
			{
				set
				{
					base.PowerSharpParameters["DefaultFontSize"] = value;
				}
			}

			// Token: 0x17002497 RID: 9367
			// (set) Token: 0x06004207 RID: 16903 RVA: 0x0006D6D2 File Offset: 0x0006B8D2
			public virtual string DefaultFontColor
			{
				set
				{
					base.PowerSharpParameters["DefaultFontColor"] = value;
				}
			}

			// Token: 0x17002498 RID: 9368
			// (set) Token: 0x06004208 RID: 16904 RVA: 0x0006D6E5 File Offset: 0x0006B8E5
			public virtual FontFlags DefaultFontFlags
			{
				set
				{
					base.PowerSharpParameters["DefaultFontFlags"] = value;
				}
			}

			// Token: 0x17002499 RID: 9369
			// (set) Token: 0x06004209 RID: 16905 RVA: 0x0006D6FD File Offset: 0x0006B8FD
			public virtual bool AlwaysShowBcc
			{
				set
				{
					base.PowerSharpParameters["AlwaysShowBcc"] = value;
				}
			}

			// Token: 0x1700249A RID: 9370
			// (set) Token: 0x0600420A RID: 16906 RVA: 0x0006D715 File Offset: 0x0006B915
			public virtual bool AlwaysShowFrom
			{
				set
				{
					base.PowerSharpParameters["AlwaysShowFrom"] = value;
				}
			}

			// Token: 0x1700249B RID: 9371
			// (set) Token: 0x0600420B RID: 16907 RVA: 0x0006D72D File Offset: 0x0006B92D
			public virtual MailFormat DefaultFormat
			{
				set
				{
					base.PowerSharpParameters["DefaultFormat"] = value;
				}
			}

			// Token: 0x1700249C RID: 9372
			// (set) Token: 0x0600420C RID: 16908 RVA: 0x0006D745 File Offset: 0x0006B945
			public virtual ReadReceiptResponse ReadReceiptResponse
			{
				set
				{
					base.PowerSharpParameters["ReadReceiptResponse"] = value;
				}
			}

			// Token: 0x1700249D RID: 9373
			// (set) Token: 0x0600420D RID: 16909 RVA: 0x0006D75D File Offset: 0x0006B95D
			public virtual PreviewMarkAsReadBehavior PreviewMarkAsReadBehavior
			{
				set
				{
					base.PowerSharpParameters["PreviewMarkAsReadBehavior"] = value;
				}
			}

			// Token: 0x1700249E RID: 9374
			// (set) Token: 0x0600420E RID: 16910 RVA: 0x0006D775 File Offset: 0x0006B975
			public virtual int PreviewMarkAsReadDelaytime
			{
				set
				{
					base.PowerSharpParameters["PreviewMarkAsReadDelaytime"] = value;
				}
			}

			// Token: 0x1700249F RID: 9375
			// (set) Token: 0x0600420F RID: 16911 RVA: 0x0006D78D File Offset: 0x0006B98D
			public virtual ConversationSortOrder ConversationSortOrder
			{
				set
				{
					base.PowerSharpParameters["ConversationSortOrder"] = value;
				}
			}

			// Token: 0x170024A0 RID: 9376
			// (set) Token: 0x06004210 RID: 16912 RVA: 0x0006D7A5 File Offset: 0x0006B9A5
			public virtual bool ShowConversationAsTree
			{
				set
				{
					base.PowerSharpParameters["ShowConversationAsTree"] = value;
				}
			}

			// Token: 0x170024A1 RID: 9377
			// (set) Token: 0x06004211 RID: 16913 RVA: 0x0006D7BD File Offset: 0x0006B9BD
			public virtual bool HideDeletedItems
			{
				set
				{
					base.PowerSharpParameters["HideDeletedItems"] = value;
				}
			}

			// Token: 0x170024A2 RID: 9378
			// (set) Token: 0x06004212 RID: 16914 RVA: 0x0006D7D5 File Offset: 0x0006B9D5
			public virtual string SendAddressDefault
			{
				set
				{
					base.PowerSharpParameters["SendAddressDefault"] = value;
				}
			}

			// Token: 0x170024A3 RID: 9379
			// (set) Token: 0x06004213 RID: 16915 RVA: 0x0006D7E8 File Offset: 0x0006B9E8
			public virtual EmailComposeMode EmailComposeMode
			{
				set
				{
					base.PowerSharpParameters["EmailComposeMode"] = value;
				}
			}

			// Token: 0x170024A4 RID: 9380
			// (set) Token: 0x06004214 RID: 16916 RVA: 0x0006D800 File Offset: 0x0006BA00
			public virtual bool CheckForForgottenAttachments
			{
				set
				{
					base.PowerSharpParameters["CheckForForgottenAttachments"] = value;
				}
			}

			// Token: 0x170024A5 RID: 9381
			// (set) Token: 0x06004215 RID: 16917 RVA: 0x0006D818 File Offset: 0x0006BA18
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024A6 RID: 9382
			// (set) Token: 0x06004216 RID: 16918 RVA: 0x0006D830 File Offset: 0x0006BA30
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024A7 RID: 9383
			// (set) Token: 0x06004217 RID: 16919 RVA: 0x0006D848 File Offset: 0x0006BA48
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024A8 RID: 9384
			// (set) Token: 0x06004218 RID: 16920 RVA: 0x0006D860 File Offset: 0x0006BA60
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170024A9 RID: 9385
			// (set) Token: 0x06004219 RID: 16921 RVA: 0x0006D878 File Offset: 0x0006BA78
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
