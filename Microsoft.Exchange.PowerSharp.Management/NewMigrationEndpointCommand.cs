using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000290 RID: 656
	public class NewMigrationEndpointCommand : SyntheticCommandWithPipelineInput<MigrationEndpoint, MigrationEndpoint>
	{
		// Token: 0x06002F30 RID: 12080 RVA: 0x000552C9 File Offset: 0x000534C9
		private NewMigrationEndpointCommand() : base("New-MigrationEndpoint")
		{
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000552D6 File Offset: 0x000534D6
		public NewMigrationEndpointCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000552E5 File Offset: 0x000534E5
		public virtual NewMigrationEndpointCommand SetParameters(NewMigrationEndpointCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000552EF File Offset: 0x000534EF
		public virtual NewMigrationEndpointCommand SetParameters(NewMigrationEndpointCommand.ExchangeOutlookAnywhereParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000552F9 File Offset: 0x000534F9
		public virtual NewMigrationEndpointCommand SetParameters(NewMigrationEndpointCommand.ExchangeOutlookAnywhereAutoDiscoverParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x00055303 File Offset: 0x00053503
		public virtual NewMigrationEndpointCommand SetParameters(NewMigrationEndpointCommand.ExchangeRemoteMoveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x0005530D File Offset: 0x0005350D
		public virtual NewMigrationEndpointCommand SetParameters(NewMigrationEndpointCommand.ExchangeRemoteMoveAutoDiscoverParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x00055317 File Offset: 0x00053517
		public virtual NewMigrationEndpointCommand SetParameters(NewMigrationEndpointCommand.PSTImportParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x00055321 File Offset: 0x00053521
		public virtual NewMigrationEndpointCommand SetParameters(NewMigrationEndpointCommand.PublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x0005532B File Offset: 0x0005352B
		public virtual NewMigrationEndpointCommand SetParameters(NewMigrationEndpointCommand.IMAPParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000291 RID: 657
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170015C9 RID: 5577
			// (set) Token: 0x06002F3A RID: 12090 RVA: 0x00055335 File Offset: 0x00053535
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170015CA RID: 5578
			// (set) Token: 0x06002F3B RID: 12091 RVA: 0x00055348 File Offset: 0x00053548
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x170015CB RID: 5579
			// (set) Token: 0x06002F3C RID: 12092 RVA: 0x00055360 File Offset: 0x00053560
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x170015CC RID: 5580
			// (set) Token: 0x06002F3D RID: 12093 RVA: 0x00055378 File Offset: 0x00053578
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x170015CD RID: 5581
			// (set) Token: 0x06002F3E RID: 12094 RVA: 0x00055390 File Offset: 0x00053590
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170015CE RID: 5582
			// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000553AE File Offset: 0x000535AE
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015CF RID: 5583
			// (set) Token: 0x06002F40 RID: 12096 RVA: 0x000553CC File Offset: 0x000535CC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170015D0 RID: 5584
			// (set) Token: 0x06002F41 RID: 12097 RVA: 0x000553DF File Offset: 0x000535DF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170015D1 RID: 5585
			// (set) Token: 0x06002F42 RID: 12098 RVA: 0x000553F7 File Offset: 0x000535F7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170015D2 RID: 5586
			// (set) Token: 0x06002F43 RID: 12099 RVA: 0x0005540F File Offset: 0x0005360F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170015D3 RID: 5587
			// (set) Token: 0x06002F44 RID: 12100 RVA: 0x00055427 File Offset: 0x00053627
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170015D4 RID: 5588
			// (set) Token: 0x06002F45 RID: 12101 RVA: 0x0005543F File Offset: 0x0005363F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000292 RID: 658
		public class ExchangeOutlookAnywhereParameters : ParametersBase
		{
			// Token: 0x170015D5 RID: 5589
			// (set) Token: 0x06002F47 RID: 12103 RVA: 0x0005545F File Offset: 0x0005365F
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x170015D6 RID: 5590
			// (set) Token: 0x06002F48 RID: 12104 RVA: 0x00055472 File Offset: 0x00053672
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x170015D7 RID: 5591
			// (set) Token: 0x06002F49 RID: 12105 RVA: 0x0005548A File Offset: 0x0005368A
			public virtual MigrationMailboxPermission MailboxPermission
			{
				set
				{
					base.PowerSharpParameters["MailboxPermission"] = value;
				}
			}

			// Token: 0x170015D8 RID: 5592
			// (set) Token: 0x06002F4A RID: 12106 RVA: 0x000554A2 File Offset: 0x000536A2
			public virtual string SourceMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["SourceMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x170015D9 RID: 5593
			// (set) Token: 0x06002F4B RID: 12107 RVA: 0x000554B5 File Offset: 0x000536B5
			public virtual string TestMailbox
			{
				set
				{
					base.PowerSharpParameters["TestMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015DA RID: 5594
			// (set) Token: 0x06002F4C RID: 12108 RVA: 0x000554D3 File Offset: 0x000536D3
			public virtual string ExchangeServer
			{
				set
				{
					base.PowerSharpParameters["ExchangeServer"] = value;
				}
			}

			// Token: 0x170015DB RID: 5595
			// (set) Token: 0x06002F4D RID: 12109 RVA: 0x000554E6 File Offset: 0x000536E6
			public virtual Fqdn RpcProxyServer
			{
				set
				{
					base.PowerSharpParameters["RpcProxyServer"] = value;
				}
			}

			// Token: 0x170015DC RID: 5596
			// (set) Token: 0x06002F4E RID: 12110 RVA: 0x000554F9 File Offset: 0x000536F9
			public virtual string NspiServer
			{
				set
				{
					base.PowerSharpParameters["NspiServer"] = value;
				}
			}

			// Token: 0x170015DD RID: 5597
			// (set) Token: 0x06002F4F RID: 12111 RVA: 0x0005550C File Offset: 0x0005370C
			public virtual AuthenticationMethod Authentication
			{
				set
				{
					base.PowerSharpParameters["Authentication"] = value;
				}
			}

			// Token: 0x170015DE RID: 5598
			// (set) Token: 0x06002F50 RID: 12112 RVA: 0x00055524 File Offset: 0x00053724
			public virtual SwitchParameter ExchangeOutlookAnywhere
			{
				set
				{
					base.PowerSharpParameters["ExchangeOutlookAnywhere"] = value;
				}
			}

			// Token: 0x170015DF RID: 5599
			// (set) Token: 0x06002F51 RID: 12113 RVA: 0x0005553C File Offset: 0x0005373C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170015E0 RID: 5600
			// (set) Token: 0x06002F52 RID: 12114 RVA: 0x0005554F File Offset: 0x0005374F
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x170015E1 RID: 5601
			// (set) Token: 0x06002F53 RID: 12115 RVA: 0x00055567 File Offset: 0x00053767
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x170015E2 RID: 5602
			// (set) Token: 0x06002F54 RID: 12116 RVA: 0x0005557F File Offset: 0x0005377F
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x170015E3 RID: 5603
			// (set) Token: 0x06002F55 RID: 12117 RVA: 0x00055597 File Offset: 0x00053797
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170015E4 RID: 5604
			// (set) Token: 0x06002F56 RID: 12118 RVA: 0x000555B5 File Offset: 0x000537B5
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015E5 RID: 5605
			// (set) Token: 0x06002F57 RID: 12119 RVA: 0x000555D3 File Offset: 0x000537D3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170015E6 RID: 5606
			// (set) Token: 0x06002F58 RID: 12120 RVA: 0x000555E6 File Offset: 0x000537E6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170015E7 RID: 5607
			// (set) Token: 0x06002F59 RID: 12121 RVA: 0x000555FE File Offset: 0x000537FE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170015E8 RID: 5608
			// (set) Token: 0x06002F5A RID: 12122 RVA: 0x00055616 File Offset: 0x00053816
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170015E9 RID: 5609
			// (set) Token: 0x06002F5B RID: 12123 RVA: 0x0005562E File Offset: 0x0005382E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170015EA RID: 5610
			// (set) Token: 0x06002F5C RID: 12124 RVA: 0x00055646 File Offset: 0x00053846
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000293 RID: 659
		public class ExchangeOutlookAnywhereAutoDiscoverParameters : ParametersBase
		{
			// Token: 0x170015EB RID: 5611
			// (set) Token: 0x06002F5E RID: 12126 RVA: 0x00055666 File Offset: 0x00053866
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x170015EC RID: 5612
			// (set) Token: 0x06002F5F RID: 12127 RVA: 0x00055679 File Offset: 0x00053879
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x170015ED RID: 5613
			// (set) Token: 0x06002F60 RID: 12128 RVA: 0x00055691 File Offset: 0x00053891
			public virtual MigrationMailboxPermission MailboxPermission
			{
				set
				{
					base.PowerSharpParameters["MailboxPermission"] = value;
				}
			}

			// Token: 0x170015EE RID: 5614
			// (set) Token: 0x06002F61 RID: 12129 RVA: 0x000556A9 File Offset: 0x000538A9
			public virtual string SourceMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["SourceMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x170015EF RID: 5615
			// (set) Token: 0x06002F62 RID: 12130 RVA: 0x000556BC File Offset: 0x000538BC
			public virtual string TestMailbox
			{
				set
				{
					base.PowerSharpParameters["TestMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015F0 RID: 5616
			// (set) Token: 0x06002F63 RID: 12131 RVA: 0x000556DA File Offset: 0x000538DA
			public virtual SwitchParameter ExchangeOutlookAnywhere
			{
				set
				{
					base.PowerSharpParameters["ExchangeOutlookAnywhere"] = value;
				}
			}

			// Token: 0x170015F1 RID: 5617
			// (set) Token: 0x06002F64 RID: 12132 RVA: 0x000556F2 File Offset: 0x000538F2
			public virtual SwitchParameter Autodiscover
			{
				set
				{
					base.PowerSharpParameters["Autodiscover"] = value;
				}
			}

			// Token: 0x170015F2 RID: 5618
			// (set) Token: 0x06002F65 RID: 12133 RVA: 0x0005570A File Offset: 0x0005390A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170015F3 RID: 5619
			// (set) Token: 0x06002F66 RID: 12134 RVA: 0x0005571D File Offset: 0x0005391D
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x170015F4 RID: 5620
			// (set) Token: 0x06002F67 RID: 12135 RVA: 0x00055735 File Offset: 0x00053935
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x170015F5 RID: 5621
			// (set) Token: 0x06002F68 RID: 12136 RVA: 0x0005574D File Offset: 0x0005394D
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x170015F6 RID: 5622
			// (set) Token: 0x06002F69 RID: 12137 RVA: 0x00055765 File Offset: 0x00053965
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170015F7 RID: 5623
			// (set) Token: 0x06002F6A RID: 12138 RVA: 0x00055783 File Offset: 0x00053983
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015F8 RID: 5624
			// (set) Token: 0x06002F6B RID: 12139 RVA: 0x000557A1 File Offset: 0x000539A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170015F9 RID: 5625
			// (set) Token: 0x06002F6C RID: 12140 RVA: 0x000557B4 File Offset: 0x000539B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170015FA RID: 5626
			// (set) Token: 0x06002F6D RID: 12141 RVA: 0x000557CC File Offset: 0x000539CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170015FB RID: 5627
			// (set) Token: 0x06002F6E RID: 12142 RVA: 0x000557E4 File Offset: 0x000539E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170015FC RID: 5628
			// (set) Token: 0x06002F6F RID: 12143 RVA: 0x000557FC File Offset: 0x000539FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170015FD RID: 5629
			// (set) Token: 0x06002F70 RID: 12144 RVA: 0x00055814 File Offset: 0x00053A14
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000294 RID: 660
		public class ExchangeRemoteMoveParameters : ParametersBase
		{
			// Token: 0x170015FE RID: 5630
			// (set) Token: 0x06002F72 RID: 12146 RVA: 0x00055834 File Offset: 0x00053A34
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x170015FF RID: 5631
			// (set) Token: 0x06002F73 RID: 12147 RVA: 0x00055847 File Offset: 0x00053A47
			public virtual Fqdn RemoteServer
			{
				set
				{
					base.PowerSharpParameters["RemoteServer"] = value;
				}
			}

			// Token: 0x17001600 RID: 5632
			// (set) Token: 0x06002F74 RID: 12148 RVA: 0x0005585A File Offset: 0x00053A5A
			public virtual SwitchParameter ExchangeRemoteMove
			{
				set
				{
					base.PowerSharpParameters["ExchangeRemoteMove"] = value;
				}
			}

			// Token: 0x17001601 RID: 5633
			// (set) Token: 0x06002F75 RID: 12149 RVA: 0x00055872 File Offset: 0x00053A72
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001602 RID: 5634
			// (set) Token: 0x06002F76 RID: 12150 RVA: 0x00055885 File Offset: 0x00053A85
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17001603 RID: 5635
			// (set) Token: 0x06002F77 RID: 12151 RVA: 0x0005589D File Offset: 0x00053A9D
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001604 RID: 5636
			// (set) Token: 0x06002F78 RID: 12152 RVA: 0x000558B5 File Offset: 0x00053AB5
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x17001605 RID: 5637
			// (set) Token: 0x06002F79 RID: 12153 RVA: 0x000558CD File Offset: 0x00053ACD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001606 RID: 5638
			// (set) Token: 0x06002F7A RID: 12154 RVA: 0x000558EB File Offset: 0x00053AEB
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001607 RID: 5639
			// (set) Token: 0x06002F7B RID: 12155 RVA: 0x00055909 File Offset: 0x00053B09
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001608 RID: 5640
			// (set) Token: 0x06002F7C RID: 12156 RVA: 0x0005591C File Offset: 0x00053B1C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001609 RID: 5641
			// (set) Token: 0x06002F7D RID: 12157 RVA: 0x00055934 File Offset: 0x00053B34
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700160A RID: 5642
			// (set) Token: 0x06002F7E RID: 12158 RVA: 0x0005594C File Offset: 0x00053B4C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700160B RID: 5643
			// (set) Token: 0x06002F7F RID: 12159 RVA: 0x00055964 File Offset: 0x00053B64
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700160C RID: 5644
			// (set) Token: 0x06002F80 RID: 12160 RVA: 0x0005597C File Offset: 0x00053B7C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000295 RID: 661
		public class ExchangeRemoteMoveAutoDiscoverParameters : ParametersBase
		{
			// Token: 0x1700160D RID: 5645
			// (set) Token: 0x06002F82 RID: 12162 RVA: 0x0005599C File Offset: 0x00053B9C
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x1700160E RID: 5646
			// (set) Token: 0x06002F83 RID: 12163 RVA: 0x000559AF File Offset: 0x00053BAF
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x1700160F RID: 5647
			// (set) Token: 0x06002F84 RID: 12164 RVA: 0x000559C7 File Offset: 0x00053BC7
			public virtual SwitchParameter ExchangeRemoteMove
			{
				set
				{
					base.PowerSharpParameters["ExchangeRemoteMove"] = value;
				}
			}

			// Token: 0x17001610 RID: 5648
			// (set) Token: 0x06002F85 RID: 12165 RVA: 0x000559DF File Offset: 0x00053BDF
			public virtual SwitchParameter Autodiscover
			{
				set
				{
					base.PowerSharpParameters["Autodiscover"] = value;
				}
			}

			// Token: 0x17001611 RID: 5649
			// (set) Token: 0x06002F86 RID: 12166 RVA: 0x000559F7 File Offset: 0x00053BF7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001612 RID: 5650
			// (set) Token: 0x06002F87 RID: 12167 RVA: 0x00055A0A File Offset: 0x00053C0A
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17001613 RID: 5651
			// (set) Token: 0x06002F88 RID: 12168 RVA: 0x00055A22 File Offset: 0x00053C22
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001614 RID: 5652
			// (set) Token: 0x06002F89 RID: 12169 RVA: 0x00055A3A File Offset: 0x00053C3A
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x17001615 RID: 5653
			// (set) Token: 0x06002F8A RID: 12170 RVA: 0x00055A52 File Offset: 0x00053C52
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001616 RID: 5654
			// (set) Token: 0x06002F8B RID: 12171 RVA: 0x00055A70 File Offset: 0x00053C70
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001617 RID: 5655
			// (set) Token: 0x06002F8C RID: 12172 RVA: 0x00055A8E File Offset: 0x00053C8E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001618 RID: 5656
			// (set) Token: 0x06002F8D RID: 12173 RVA: 0x00055AA1 File Offset: 0x00053CA1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001619 RID: 5657
			// (set) Token: 0x06002F8E RID: 12174 RVA: 0x00055AB9 File Offset: 0x00053CB9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700161A RID: 5658
			// (set) Token: 0x06002F8F RID: 12175 RVA: 0x00055AD1 File Offset: 0x00053CD1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700161B RID: 5659
			// (set) Token: 0x06002F90 RID: 12176 RVA: 0x00055AE9 File Offset: 0x00053CE9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700161C RID: 5660
			// (set) Token: 0x06002F91 RID: 12177 RVA: 0x00055B01 File Offset: 0x00053D01
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000296 RID: 662
		public class PSTImportParameters : ParametersBase
		{
			// Token: 0x1700161D RID: 5661
			// (set) Token: 0x06002F93 RID: 12179 RVA: 0x00055B21 File Offset: 0x00053D21
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x1700161E RID: 5662
			// (set) Token: 0x06002F94 RID: 12180 RVA: 0x00055B34 File Offset: 0x00053D34
			public virtual Fqdn RemoteServer
			{
				set
				{
					base.PowerSharpParameters["RemoteServer"] = value;
				}
			}

			// Token: 0x1700161F RID: 5663
			// (set) Token: 0x06002F95 RID: 12181 RVA: 0x00055B47 File Offset: 0x00053D47
			public virtual SwitchParameter PSTImport
			{
				set
				{
					base.PowerSharpParameters["PSTImport"] = value;
				}
			}

			// Token: 0x17001620 RID: 5664
			// (set) Token: 0x06002F96 RID: 12182 RVA: 0x00055B5F File Offset: 0x00053D5F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001621 RID: 5665
			// (set) Token: 0x06002F97 RID: 12183 RVA: 0x00055B72 File Offset: 0x00053D72
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17001622 RID: 5666
			// (set) Token: 0x06002F98 RID: 12184 RVA: 0x00055B8A File Offset: 0x00053D8A
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001623 RID: 5667
			// (set) Token: 0x06002F99 RID: 12185 RVA: 0x00055BA2 File Offset: 0x00053DA2
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x17001624 RID: 5668
			// (set) Token: 0x06002F9A RID: 12186 RVA: 0x00055BBA File Offset: 0x00053DBA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001625 RID: 5669
			// (set) Token: 0x06002F9B RID: 12187 RVA: 0x00055BD8 File Offset: 0x00053DD8
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001626 RID: 5670
			// (set) Token: 0x06002F9C RID: 12188 RVA: 0x00055BF6 File Offset: 0x00053DF6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001627 RID: 5671
			// (set) Token: 0x06002F9D RID: 12189 RVA: 0x00055C09 File Offset: 0x00053E09
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001628 RID: 5672
			// (set) Token: 0x06002F9E RID: 12190 RVA: 0x00055C21 File Offset: 0x00053E21
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001629 RID: 5673
			// (set) Token: 0x06002F9F RID: 12191 RVA: 0x00055C39 File Offset: 0x00053E39
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700162A RID: 5674
			// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x00055C51 File Offset: 0x00053E51
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700162B RID: 5675
			// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x00055C69 File Offset: 0x00053E69
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000297 RID: 663
		public class PublicFolderParameters : ParametersBase
		{
			// Token: 0x1700162C RID: 5676
			// (set) Token: 0x06002FA3 RID: 12195 RVA: 0x00055C89 File Offset: 0x00053E89
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x1700162D RID: 5677
			// (set) Token: 0x06002FA4 RID: 12196 RVA: 0x00055C9C File Offset: 0x00053E9C
			public virtual string SourceMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["SourceMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x1700162E RID: 5678
			// (set) Token: 0x06002FA5 RID: 12197 RVA: 0x00055CAF File Offset: 0x00053EAF
			public virtual string PublicFolderDatabaseServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["PublicFolderDatabaseServerLegacyDN"] = value;
				}
			}

			// Token: 0x1700162F RID: 5679
			// (set) Token: 0x06002FA6 RID: 12198 RVA: 0x00055CC2 File Offset: 0x00053EC2
			public virtual string TestMailbox
			{
				set
				{
					base.PowerSharpParameters["TestMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001630 RID: 5680
			// (set) Token: 0x06002FA7 RID: 12199 RVA: 0x00055CE0 File Offset: 0x00053EE0
			public virtual Fqdn RpcProxyServer
			{
				set
				{
					base.PowerSharpParameters["RpcProxyServer"] = value;
				}
			}

			// Token: 0x17001631 RID: 5681
			// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x00055CF3 File Offset: 0x00053EF3
			public virtual AuthenticationMethod Authentication
			{
				set
				{
					base.PowerSharpParameters["Authentication"] = value;
				}
			}

			// Token: 0x17001632 RID: 5682
			// (set) Token: 0x06002FA9 RID: 12201 RVA: 0x00055D0B File Offset: 0x00053F0B
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17001633 RID: 5683
			// (set) Token: 0x06002FAA RID: 12202 RVA: 0x00055D23 File Offset: 0x00053F23
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001634 RID: 5684
			// (set) Token: 0x06002FAB RID: 12203 RVA: 0x00055D36 File Offset: 0x00053F36
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17001635 RID: 5685
			// (set) Token: 0x06002FAC RID: 12204 RVA: 0x00055D4E File Offset: 0x00053F4E
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001636 RID: 5686
			// (set) Token: 0x06002FAD RID: 12205 RVA: 0x00055D66 File Offset: 0x00053F66
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x17001637 RID: 5687
			// (set) Token: 0x06002FAE RID: 12206 RVA: 0x00055D7E File Offset: 0x00053F7E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001638 RID: 5688
			// (set) Token: 0x06002FAF RID: 12207 RVA: 0x00055D9C File Offset: 0x00053F9C
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001639 RID: 5689
			// (set) Token: 0x06002FB0 RID: 12208 RVA: 0x00055DBA File Offset: 0x00053FBA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700163A RID: 5690
			// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x00055DCD File Offset: 0x00053FCD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700163B RID: 5691
			// (set) Token: 0x06002FB2 RID: 12210 RVA: 0x00055DE5 File Offset: 0x00053FE5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700163C RID: 5692
			// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x00055DFD File Offset: 0x00053FFD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700163D RID: 5693
			// (set) Token: 0x06002FB4 RID: 12212 RVA: 0x00055E15 File Offset: 0x00054015
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700163E RID: 5694
			// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x00055E2D File Offset: 0x0005402D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000298 RID: 664
		public class IMAPParameters : ParametersBase
		{
			// Token: 0x1700163F RID: 5695
			// (set) Token: 0x06002FB7 RID: 12215 RVA: 0x00055E4D File Offset: 0x0005404D
			public virtual Fqdn RemoteServer
			{
				set
				{
					base.PowerSharpParameters["RemoteServer"] = value;
				}
			}

			// Token: 0x17001640 RID: 5696
			// (set) Token: 0x06002FB8 RID: 12216 RVA: 0x00055E60 File Offset: 0x00054060
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x17001641 RID: 5697
			// (set) Token: 0x06002FB9 RID: 12217 RVA: 0x00055E78 File Offset: 0x00054078
			public virtual AuthenticationMethod Authentication
			{
				set
				{
					base.PowerSharpParameters["Authentication"] = value;
				}
			}

			// Token: 0x17001642 RID: 5698
			// (set) Token: 0x06002FBA RID: 12218 RVA: 0x00055E90 File Offset: 0x00054090
			public virtual IMAPSecurityMechanism Security
			{
				set
				{
					base.PowerSharpParameters["Security"] = value;
				}
			}

			// Token: 0x17001643 RID: 5699
			// (set) Token: 0x06002FBB RID: 12219 RVA: 0x00055EA8 File Offset: 0x000540A8
			public virtual SwitchParameter IMAP
			{
				set
				{
					base.PowerSharpParameters["IMAP"] = value;
				}
			}

			// Token: 0x17001644 RID: 5700
			// (set) Token: 0x06002FBC RID: 12220 RVA: 0x00055EC0 File Offset: 0x000540C0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001645 RID: 5701
			// (set) Token: 0x06002FBD RID: 12221 RVA: 0x00055ED3 File Offset: 0x000540D3
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17001646 RID: 5702
			// (set) Token: 0x06002FBE RID: 12222 RVA: 0x00055EEB File Offset: 0x000540EB
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001647 RID: 5703
			// (set) Token: 0x06002FBF RID: 12223 RVA: 0x00055F03 File Offset: 0x00054103
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x17001648 RID: 5704
			// (set) Token: 0x06002FC0 RID: 12224 RVA: 0x00055F1B File Offset: 0x0005411B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001649 RID: 5705
			// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x00055F39 File Offset: 0x00054139
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700164A RID: 5706
			// (set) Token: 0x06002FC2 RID: 12226 RVA: 0x00055F57 File Offset: 0x00054157
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700164B RID: 5707
			// (set) Token: 0x06002FC3 RID: 12227 RVA: 0x00055F6A File Offset: 0x0005416A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700164C RID: 5708
			// (set) Token: 0x06002FC4 RID: 12228 RVA: 0x00055F82 File Offset: 0x00054182
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700164D RID: 5709
			// (set) Token: 0x06002FC5 RID: 12229 RVA: 0x00055F9A File Offset: 0x0005419A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700164E RID: 5710
			// (set) Token: 0x06002FC6 RID: 12230 RVA: 0x00055FB2 File Offset: 0x000541B2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700164F RID: 5711
			// (set) Token: 0x06002FC7 RID: 12231 RVA: 0x00055FCA File Offset: 0x000541CA
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
