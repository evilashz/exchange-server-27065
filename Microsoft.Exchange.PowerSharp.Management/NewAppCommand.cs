using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Management.Extension;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E43 RID: 3651
	public class NewAppCommand : SyntheticCommandWithPipelineInput<App, App>
	{
		// Token: 0x0600D8A9 RID: 55465 RVA: 0x001339D9 File Offset: 0x00131BD9
		private NewAppCommand() : base("New-App")
		{
		}

		// Token: 0x0600D8AA RID: 55466 RVA: 0x001339E6 File Offset: 0x00131BE6
		public NewAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D8AB RID: 55467 RVA: 0x001339F5 File Offset: 0x00131BF5
		public virtual NewAppCommand SetParameters(NewAppCommand.ExtensionOfficeMarketplaceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D8AC RID: 55468 RVA: 0x001339FF File Offset: 0x00131BFF
		public virtual NewAppCommand SetParameters(NewAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D8AD RID: 55469 RVA: 0x00133A09 File Offset: 0x00131C09
		public virtual NewAppCommand SetParameters(NewAppCommand.ExtensionFileDataParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D8AE RID: 55470 RVA: 0x00133A13 File Offset: 0x00131C13
		public virtual NewAppCommand SetParameters(NewAppCommand.ExtensionFileStreamParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D8AF RID: 55471 RVA: 0x00133A1D File Offset: 0x00131C1D
		public virtual NewAppCommand SetParameters(NewAppCommand.ExtensionPrivateURLParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E44 RID: 3652
		public class ExtensionOfficeMarketplaceParameters : ParametersBase
		{
			// Token: 0x1700A7DC RID: 42972
			// (set) Token: 0x0600D8B0 RID: 55472 RVA: 0x00133A27 File Offset: 0x00131C27
			public virtual string MarketplaceAssetID
			{
				set
				{
					base.PowerSharpParameters["MarketplaceAssetID"] = value;
				}
			}

			// Token: 0x1700A7DD RID: 42973
			// (set) Token: 0x0600D8B1 RID: 55473 RVA: 0x00133A3A File Offset: 0x00131C3A
			public virtual string MarketplaceQueryMarket
			{
				set
				{
					base.PowerSharpParameters["MarketplaceQueryMarket"] = value;
				}
			}

			// Token: 0x1700A7DE RID: 42974
			// (set) Token: 0x0600D8B2 RID: 55474 RVA: 0x00133A4D File Offset: 0x00131C4D
			public virtual string Etoken
			{
				set
				{
					base.PowerSharpParameters["Etoken"] = value;
				}
			}

			// Token: 0x1700A7DF RID: 42975
			// (set) Token: 0x0600D8B3 RID: 55475 RVA: 0x00133A60 File Offset: 0x00131C60
			public virtual string MarketplaceServicesUrl
			{
				set
				{
					base.PowerSharpParameters["MarketplaceServicesUrl"] = value;
				}
			}

			// Token: 0x1700A7E0 RID: 42976
			// (set) Token: 0x0600D8B4 RID: 55476 RVA: 0x00133A73 File Offset: 0x00131C73
			public virtual SwitchParameter DownloadOnly
			{
				set
				{
					base.PowerSharpParameters["DownloadOnly"] = value;
				}
			}

			// Token: 0x1700A7E1 RID: 42977
			// (set) Token: 0x0600D8B5 RID: 55477 RVA: 0x00133A8B File Offset: 0x00131C8B
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7E2 RID: 42978
			// (set) Token: 0x0600D8B6 RID: 55478 RVA: 0x00133AA9 File Offset: 0x00131CA9
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A7E3 RID: 42979
			// (set) Token: 0x0600D8B7 RID: 55479 RVA: 0x00133AC1 File Offset: 0x00131CC1
			public virtual ClientExtensionProvidedTo ProvidedTo
			{
				set
				{
					base.PowerSharpParameters["ProvidedTo"] = value;
				}
			}

			// Token: 0x1700A7E4 RID: 42980
			// (set) Token: 0x0600D8B8 RID: 55480 RVA: 0x00133AD9 File Offset: 0x00131CD9
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
			{
				set
				{
					base.PowerSharpParameters["UserList"] = value;
				}
			}

			// Token: 0x1700A7E5 RID: 42981
			// (set) Token: 0x0600D8B9 RID: 55481 RVA: 0x00133AEC File Offset: 0x00131CEC
			public virtual DefaultStateForUser DefaultStateForUser
			{
				set
				{
					base.PowerSharpParameters["DefaultStateForUser"] = value;
				}
			}

			// Token: 0x1700A7E6 RID: 42982
			// (set) Token: 0x0600D8BA RID: 55482 RVA: 0x00133B04 File Offset: 0x00131D04
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A7E7 RID: 42983
			// (set) Token: 0x0600D8BB RID: 55483 RVA: 0x00133B1C File Offset: 0x00131D1C
			public virtual SwitchParameter AllowReadWriteMailbox
			{
				set
				{
					base.PowerSharpParameters["AllowReadWriteMailbox"] = value;
				}
			}

			// Token: 0x1700A7E8 RID: 42984
			// (set) Token: 0x0600D8BC RID: 55484 RVA: 0x00133B34 File Offset: 0x00131D34
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7E9 RID: 42985
			// (set) Token: 0x0600D8BD RID: 55485 RVA: 0x00133B47 File Offset: 0x00131D47
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7EA RID: 42986
			// (set) Token: 0x0600D8BE RID: 55486 RVA: 0x00133B5F File Offset: 0x00131D5F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7EB RID: 42987
			// (set) Token: 0x0600D8BF RID: 55487 RVA: 0x00133B77 File Offset: 0x00131D77
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7EC RID: 42988
			// (set) Token: 0x0600D8C0 RID: 55488 RVA: 0x00133B8F File Offset: 0x00131D8F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A7ED RID: 42989
			// (set) Token: 0x0600D8C1 RID: 55489 RVA: 0x00133BA7 File Offset: 0x00131DA7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E45 RID: 3653
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A7EE RID: 42990
			// (set) Token: 0x0600D8C3 RID: 55491 RVA: 0x00133BC7 File Offset: 0x00131DC7
			public virtual SwitchParameter DownloadOnly
			{
				set
				{
					base.PowerSharpParameters["DownloadOnly"] = value;
				}
			}

			// Token: 0x1700A7EF RID: 42991
			// (set) Token: 0x0600D8C4 RID: 55492 RVA: 0x00133BDF File Offset: 0x00131DDF
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7F0 RID: 42992
			// (set) Token: 0x0600D8C5 RID: 55493 RVA: 0x00133BFD File Offset: 0x00131DFD
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A7F1 RID: 42993
			// (set) Token: 0x0600D8C6 RID: 55494 RVA: 0x00133C15 File Offset: 0x00131E15
			public virtual ClientExtensionProvidedTo ProvidedTo
			{
				set
				{
					base.PowerSharpParameters["ProvidedTo"] = value;
				}
			}

			// Token: 0x1700A7F2 RID: 42994
			// (set) Token: 0x0600D8C7 RID: 55495 RVA: 0x00133C2D File Offset: 0x00131E2D
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
			{
				set
				{
					base.PowerSharpParameters["UserList"] = value;
				}
			}

			// Token: 0x1700A7F3 RID: 42995
			// (set) Token: 0x0600D8C8 RID: 55496 RVA: 0x00133C40 File Offset: 0x00131E40
			public virtual DefaultStateForUser DefaultStateForUser
			{
				set
				{
					base.PowerSharpParameters["DefaultStateForUser"] = value;
				}
			}

			// Token: 0x1700A7F4 RID: 42996
			// (set) Token: 0x0600D8C9 RID: 55497 RVA: 0x00133C58 File Offset: 0x00131E58
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A7F5 RID: 42997
			// (set) Token: 0x0600D8CA RID: 55498 RVA: 0x00133C70 File Offset: 0x00131E70
			public virtual SwitchParameter AllowReadWriteMailbox
			{
				set
				{
					base.PowerSharpParameters["AllowReadWriteMailbox"] = value;
				}
			}

			// Token: 0x1700A7F6 RID: 42998
			// (set) Token: 0x0600D8CB RID: 55499 RVA: 0x00133C88 File Offset: 0x00131E88
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7F7 RID: 42999
			// (set) Token: 0x0600D8CC RID: 55500 RVA: 0x00133C9B File Offset: 0x00131E9B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7F8 RID: 43000
			// (set) Token: 0x0600D8CD RID: 55501 RVA: 0x00133CB3 File Offset: 0x00131EB3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7F9 RID: 43001
			// (set) Token: 0x0600D8CE RID: 55502 RVA: 0x00133CCB File Offset: 0x00131ECB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7FA RID: 43002
			// (set) Token: 0x0600D8CF RID: 55503 RVA: 0x00133CE3 File Offset: 0x00131EE3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A7FB RID: 43003
			// (set) Token: 0x0600D8D0 RID: 55504 RVA: 0x00133CFB File Offset: 0x00131EFB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E46 RID: 3654
		public class ExtensionFileDataParameters : ParametersBase
		{
			// Token: 0x1700A7FC RID: 43004
			// (set) Token: 0x0600D8D2 RID: 55506 RVA: 0x00133D1B File Offset: 0x00131F1B
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x1700A7FD RID: 43005
			// (set) Token: 0x0600D8D3 RID: 55507 RVA: 0x00133D33 File Offset: 0x00131F33
			public virtual SwitchParameter DownloadOnly
			{
				set
				{
					base.PowerSharpParameters["DownloadOnly"] = value;
				}
			}

			// Token: 0x1700A7FE RID: 43006
			// (set) Token: 0x0600D8D4 RID: 55508 RVA: 0x00133D4B File Offset: 0x00131F4B
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7FF RID: 43007
			// (set) Token: 0x0600D8D5 RID: 55509 RVA: 0x00133D69 File Offset: 0x00131F69
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A800 RID: 43008
			// (set) Token: 0x0600D8D6 RID: 55510 RVA: 0x00133D81 File Offset: 0x00131F81
			public virtual ClientExtensionProvidedTo ProvidedTo
			{
				set
				{
					base.PowerSharpParameters["ProvidedTo"] = value;
				}
			}

			// Token: 0x1700A801 RID: 43009
			// (set) Token: 0x0600D8D7 RID: 55511 RVA: 0x00133D99 File Offset: 0x00131F99
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
			{
				set
				{
					base.PowerSharpParameters["UserList"] = value;
				}
			}

			// Token: 0x1700A802 RID: 43010
			// (set) Token: 0x0600D8D8 RID: 55512 RVA: 0x00133DAC File Offset: 0x00131FAC
			public virtual DefaultStateForUser DefaultStateForUser
			{
				set
				{
					base.PowerSharpParameters["DefaultStateForUser"] = value;
				}
			}

			// Token: 0x1700A803 RID: 43011
			// (set) Token: 0x0600D8D9 RID: 55513 RVA: 0x00133DC4 File Offset: 0x00131FC4
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A804 RID: 43012
			// (set) Token: 0x0600D8DA RID: 55514 RVA: 0x00133DDC File Offset: 0x00131FDC
			public virtual SwitchParameter AllowReadWriteMailbox
			{
				set
				{
					base.PowerSharpParameters["AllowReadWriteMailbox"] = value;
				}
			}

			// Token: 0x1700A805 RID: 43013
			// (set) Token: 0x0600D8DB RID: 55515 RVA: 0x00133DF4 File Offset: 0x00131FF4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A806 RID: 43014
			// (set) Token: 0x0600D8DC RID: 55516 RVA: 0x00133E07 File Offset: 0x00132007
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A807 RID: 43015
			// (set) Token: 0x0600D8DD RID: 55517 RVA: 0x00133E1F File Offset: 0x0013201F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A808 RID: 43016
			// (set) Token: 0x0600D8DE RID: 55518 RVA: 0x00133E37 File Offset: 0x00132037
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A809 RID: 43017
			// (set) Token: 0x0600D8DF RID: 55519 RVA: 0x00133E4F File Offset: 0x0013204F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A80A RID: 43018
			// (set) Token: 0x0600D8E0 RID: 55520 RVA: 0x00133E67 File Offset: 0x00132067
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E47 RID: 3655
		public class ExtensionFileStreamParameters : ParametersBase
		{
			// Token: 0x1700A80B RID: 43019
			// (set) Token: 0x0600D8E2 RID: 55522 RVA: 0x00133E87 File Offset: 0x00132087
			public virtual Stream FileStream
			{
				set
				{
					base.PowerSharpParameters["FileStream"] = value;
				}
			}

			// Token: 0x1700A80C RID: 43020
			// (set) Token: 0x0600D8E3 RID: 55523 RVA: 0x00133E9A File Offset: 0x0013209A
			public virtual SwitchParameter DownloadOnly
			{
				set
				{
					base.PowerSharpParameters["DownloadOnly"] = value;
				}
			}

			// Token: 0x1700A80D RID: 43021
			// (set) Token: 0x0600D8E4 RID: 55524 RVA: 0x00133EB2 File Offset: 0x001320B2
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A80E RID: 43022
			// (set) Token: 0x0600D8E5 RID: 55525 RVA: 0x00133ED0 File Offset: 0x001320D0
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A80F RID: 43023
			// (set) Token: 0x0600D8E6 RID: 55526 RVA: 0x00133EE8 File Offset: 0x001320E8
			public virtual ClientExtensionProvidedTo ProvidedTo
			{
				set
				{
					base.PowerSharpParameters["ProvidedTo"] = value;
				}
			}

			// Token: 0x1700A810 RID: 43024
			// (set) Token: 0x0600D8E7 RID: 55527 RVA: 0x00133F00 File Offset: 0x00132100
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
			{
				set
				{
					base.PowerSharpParameters["UserList"] = value;
				}
			}

			// Token: 0x1700A811 RID: 43025
			// (set) Token: 0x0600D8E8 RID: 55528 RVA: 0x00133F13 File Offset: 0x00132113
			public virtual DefaultStateForUser DefaultStateForUser
			{
				set
				{
					base.PowerSharpParameters["DefaultStateForUser"] = value;
				}
			}

			// Token: 0x1700A812 RID: 43026
			// (set) Token: 0x0600D8E9 RID: 55529 RVA: 0x00133F2B File Offset: 0x0013212B
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A813 RID: 43027
			// (set) Token: 0x0600D8EA RID: 55530 RVA: 0x00133F43 File Offset: 0x00132143
			public virtual SwitchParameter AllowReadWriteMailbox
			{
				set
				{
					base.PowerSharpParameters["AllowReadWriteMailbox"] = value;
				}
			}

			// Token: 0x1700A814 RID: 43028
			// (set) Token: 0x0600D8EB RID: 55531 RVA: 0x00133F5B File Offset: 0x0013215B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A815 RID: 43029
			// (set) Token: 0x0600D8EC RID: 55532 RVA: 0x00133F6E File Offset: 0x0013216E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A816 RID: 43030
			// (set) Token: 0x0600D8ED RID: 55533 RVA: 0x00133F86 File Offset: 0x00132186
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A817 RID: 43031
			// (set) Token: 0x0600D8EE RID: 55534 RVA: 0x00133F9E File Offset: 0x0013219E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A818 RID: 43032
			// (set) Token: 0x0600D8EF RID: 55535 RVA: 0x00133FB6 File Offset: 0x001321B6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A819 RID: 43033
			// (set) Token: 0x0600D8F0 RID: 55536 RVA: 0x00133FCE File Offset: 0x001321CE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E48 RID: 3656
		public class ExtensionPrivateURLParameters : ParametersBase
		{
			// Token: 0x1700A81A RID: 43034
			// (set) Token: 0x0600D8F2 RID: 55538 RVA: 0x00133FEE File Offset: 0x001321EE
			public virtual Uri Url
			{
				set
				{
					base.PowerSharpParameters["Url"] = value;
				}
			}

			// Token: 0x1700A81B RID: 43035
			// (set) Token: 0x0600D8F3 RID: 55539 RVA: 0x00134001 File Offset: 0x00132201
			public virtual SwitchParameter DownloadOnly
			{
				set
				{
					base.PowerSharpParameters["DownloadOnly"] = value;
				}
			}

			// Token: 0x1700A81C RID: 43036
			// (set) Token: 0x0600D8F4 RID: 55540 RVA: 0x00134019 File Offset: 0x00132219
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A81D RID: 43037
			// (set) Token: 0x0600D8F5 RID: 55541 RVA: 0x00134037 File Offset: 0x00132237
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A81E RID: 43038
			// (set) Token: 0x0600D8F6 RID: 55542 RVA: 0x0013404F File Offset: 0x0013224F
			public virtual ClientExtensionProvidedTo ProvidedTo
			{
				set
				{
					base.PowerSharpParameters["ProvidedTo"] = value;
				}
			}

			// Token: 0x1700A81F RID: 43039
			// (set) Token: 0x0600D8F7 RID: 55543 RVA: 0x00134067 File Offset: 0x00132267
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
			{
				set
				{
					base.PowerSharpParameters["UserList"] = value;
				}
			}

			// Token: 0x1700A820 RID: 43040
			// (set) Token: 0x0600D8F8 RID: 55544 RVA: 0x0013407A File Offset: 0x0013227A
			public virtual DefaultStateForUser DefaultStateForUser
			{
				set
				{
					base.PowerSharpParameters["DefaultStateForUser"] = value;
				}
			}

			// Token: 0x1700A821 RID: 43041
			// (set) Token: 0x0600D8F9 RID: 55545 RVA: 0x00134092 File Offset: 0x00132292
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A822 RID: 43042
			// (set) Token: 0x0600D8FA RID: 55546 RVA: 0x001340AA File Offset: 0x001322AA
			public virtual SwitchParameter AllowReadWriteMailbox
			{
				set
				{
					base.PowerSharpParameters["AllowReadWriteMailbox"] = value;
				}
			}

			// Token: 0x1700A823 RID: 43043
			// (set) Token: 0x0600D8FB RID: 55547 RVA: 0x001340C2 File Offset: 0x001322C2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A824 RID: 43044
			// (set) Token: 0x0600D8FC RID: 55548 RVA: 0x001340D5 File Offset: 0x001322D5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A825 RID: 43045
			// (set) Token: 0x0600D8FD RID: 55549 RVA: 0x001340ED File Offset: 0x001322ED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A826 RID: 43046
			// (set) Token: 0x0600D8FE RID: 55550 RVA: 0x00134105 File Offset: 0x00132305
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A827 RID: 43047
			// (set) Token: 0x0600D8FF RID: 55551 RVA: 0x0013411D File Offset: 0x0013231D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A828 RID: 43048
			// (set) Token: 0x0600D900 RID: 55552 RVA: 0x00134135 File Offset: 0x00132335
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
