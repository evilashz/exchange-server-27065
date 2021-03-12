using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200029C RID: 668
	public class SetMigrationEndpointCommand : SyntheticCommandWithPipelineInputNoOutput<MigrationEndpoint>
	{
		// Token: 0x06002FE2 RID: 12258 RVA: 0x00056206 File Offset: 0x00054406
		private SetMigrationEndpointCommand() : base("Set-MigrationEndpoint")
		{
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x00056213 File Offset: 0x00054413
		public SetMigrationEndpointCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x00056222 File Offset: 0x00054422
		public virtual SetMigrationEndpointCommand SetParameters(SetMigrationEndpointCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x0005622C File Offset: 0x0005442C
		public virtual SetMigrationEndpointCommand SetParameters(SetMigrationEndpointCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200029D RID: 669
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001663 RID: 5731
			// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x00056236 File Offset: 0x00054436
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationEndpointIdParameter(value) : null);
				}
			}

			// Token: 0x17001664 RID: 5732
			// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x00056254 File Offset: 0x00054454
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17001665 RID: 5733
			// (set) Token: 0x06002FE8 RID: 12264 RVA: 0x0005626C File Offset: 0x0005446C
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001666 RID: 5734
			// (set) Token: 0x06002FE9 RID: 12265 RVA: 0x00056284 File Offset: 0x00054484
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x17001667 RID: 5735
			// (set) Token: 0x06002FEA RID: 12266 RVA: 0x00056297 File Offset: 0x00054497
			public virtual MigrationMailboxPermission MailboxPermission
			{
				set
				{
					base.PowerSharpParameters["MailboxPermission"] = value;
				}
			}

			// Token: 0x17001668 RID: 5736
			// (set) Token: 0x06002FEB RID: 12267 RVA: 0x000562AF File Offset: 0x000544AF
			public virtual string ExchangeServer
			{
				set
				{
					base.PowerSharpParameters["ExchangeServer"] = value;
				}
			}

			// Token: 0x17001669 RID: 5737
			// (set) Token: 0x06002FEC RID: 12268 RVA: 0x000562C2 File Offset: 0x000544C2
			public virtual Fqdn RemoteServer
			{
				set
				{
					base.PowerSharpParameters["RemoteServer"] = value;
				}
			}

			// Token: 0x1700166A RID: 5738
			// (set) Token: 0x06002FED RID: 12269 RVA: 0x000562D5 File Offset: 0x000544D5
			public virtual Fqdn RpcProxyServer
			{
				set
				{
					base.PowerSharpParameters["RpcProxyServer"] = value;
				}
			}

			// Token: 0x1700166B RID: 5739
			// (set) Token: 0x06002FEE RID: 12270 RVA: 0x000562E8 File Offset: 0x000544E8
			public virtual string NspiServer
			{
				set
				{
					base.PowerSharpParameters["NspiServer"] = value;
				}
			}

			// Token: 0x1700166C RID: 5740
			// (set) Token: 0x06002FEF RID: 12271 RVA: 0x000562FB File Offset: 0x000544FB
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x1700166D RID: 5741
			// (set) Token: 0x06002FF0 RID: 12272 RVA: 0x00056313 File Offset: 0x00054513
			public virtual AuthenticationMethod Authentication
			{
				set
				{
					base.PowerSharpParameters["Authentication"] = value;
				}
			}

			// Token: 0x1700166E RID: 5742
			// (set) Token: 0x06002FF1 RID: 12273 RVA: 0x0005632B File Offset: 0x0005452B
			public virtual IMAPSecurityMechanism Security
			{
				set
				{
					base.PowerSharpParameters["Security"] = value;
				}
			}

			// Token: 0x1700166F RID: 5743
			// (set) Token: 0x06002FF2 RID: 12274 RVA: 0x00056343 File Offset: 0x00054543
			public virtual string TestMailbox
			{
				set
				{
					base.PowerSharpParameters["TestMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001670 RID: 5744
			// (set) Token: 0x06002FF3 RID: 12275 RVA: 0x00056361 File Offset: 0x00054561
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x17001671 RID: 5745
			// (set) Token: 0x06002FF4 RID: 12276 RVA: 0x00056379 File Offset: 0x00054579
			public virtual string SourceMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["SourceMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17001672 RID: 5746
			// (set) Token: 0x06002FF5 RID: 12277 RVA: 0x0005638C File Offset: 0x0005458C
			public virtual string PublicFolderDatabaseServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["PublicFolderDatabaseServerLegacyDN"] = value;
				}
			}

			// Token: 0x17001673 RID: 5747
			// (set) Token: 0x06002FF6 RID: 12278 RVA: 0x0005639F File Offset: 0x0005459F
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x17001674 RID: 5748
			// (set) Token: 0x06002FF7 RID: 12279 RVA: 0x000563B7 File Offset: 0x000545B7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001675 RID: 5749
			// (set) Token: 0x06002FF8 RID: 12280 RVA: 0x000563D5 File Offset: 0x000545D5
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001676 RID: 5750
			// (set) Token: 0x06002FF9 RID: 12281 RVA: 0x000563F3 File Offset: 0x000545F3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001677 RID: 5751
			// (set) Token: 0x06002FFA RID: 12282 RVA: 0x00056406 File Offset: 0x00054606
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001678 RID: 5752
			// (set) Token: 0x06002FFB RID: 12283 RVA: 0x0005641E File Offset: 0x0005461E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001679 RID: 5753
			// (set) Token: 0x06002FFC RID: 12284 RVA: 0x00056436 File Offset: 0x00054636
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700167A RID: 5754
			// (set) Token: 0x06002FFD RID: 12285 RVA: 0x0005644E File Offset: 0x0005464E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700167B RID: 5755
			// (set) Token: 0x06002FFE RID: 12286 RVA: 0x00056466 File Offset: 0x00054666
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200029E RID: 670
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700167C RID: 5756
			// (set) Token: 0x06003000 RID: 12288 RVA: 0x00056486 File Offset: 0x00054686
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x1700167D RID: 5757
			// (set) Token: 0x06003001 RID: 12289 RVA: 0x0005649E File Offset: 0x0005469E
			public virtual Unlimited<int> MaxConcurrentIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentIncrementalSyncs"] = value;
				}
			}

			// Token: 0x1700167E RID: 5758
			// (set) Token: 0x06003002 RID: 12290 RVA: 0x000564B6 File Offset: 0x000546B6
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x1700167F RID: 5759
			// (set) Token: 0x06003003 RID: 12291 RVA: 0x000564C9 File Offset: 0x000546C9
			public virtual MigrationMailboxPermission MailboxPermission
			{
				set
				{
					base.PowerSharpParameters["MailboxPermission"] = value;
				}
			}

			// Token: 0x17001680 RID: 5760
			// (set) Token: 0x06003004 RID: 12292 RVA: 0x000564E1 File Offset: 0x000546E1
			public virtual string ExchangeServer
			{
				set
				{
					base.PowerSharpParameters["ExchangeServer"] = value;
				}
			}

			// Token: 0x17001681 RID: 5761
			// (set) Token: 0x06003005 RID: 12293 RVA: 0x000564F4 File Offset: 0x000546F4
			public virtual Fqdn RemoteServer
			{
				set
				{
					base.PowerSharpParameters["RemoteServer"] = value;
				}
			}

			// Token: 0x17001682 RID: 5762
			// (set) Token: 0x06003006 RID: 12294 RVA: 0x00056507 File Offset: 0x00054707
			public virtual Fqdn RpcProxyServer
			{
				set
				{
					base.PowerSharpParameters["RpcProxyServer"] = value;
				}
			}

			// Token: 0x17001683 RID: 5763
			// (set) Token: 0x06003007 RID: 12295 RVA: 0x0005651A File Offset: 0x0005471A
			public virtual string NspiServer
			{
				set
				{
					base.PowerSharpParameters["NspiServer"] = value;
				}
			}

			// Token: 0x17001684 RID: 5764
			// (set) Token: 0x06003008 RID: 12296 RVA: 0x0005652D File Offset: 0x0005472D
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x17001685 RID: 5765
			// (set) Token: 0x06003009 RID: 12297 RVA: 0x00056545 File Offset: 0x00054745
			public virtual AuthenticationMethod Authentication
			{
				set
				{
					base.PowerSharpParameters["Authentication"] = value;
				}
			}

			// Token: 0x17001686 RID: 5766
			// (set) Token: 0x0600300A RID: 12298 RVA: 0x0005655D File Offset: 0x0005475D
			public virtual IMAPSecurityMechanism Security
			{
				set
				{
					base.PowerSharpParameters["Security"] = value;
				}
			}

			// Token: 0x17001687 RID: 5767
			// (set) Token: 0x0600300B RID: 12299 RVA: 0x00056575 File Offset: 0x00054775
			public virtual string TestMailbox
			{
				set
				{
					base.PowerSharpParameters["TestMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001688 RID: 5768
			// (set) Token: 0x0600300C RID: 12300 RVA: 0x00056593 File Offset: 0x00054793
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x17001689 RID: 5769
			// (set) Token: 0x0600300D RID: 12301 RVA: 0x000565AB File Offset: 0x000547AB
			public virtual string SourceMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["SourceMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x1700168A RID: 5770
			// (set) Token: 0x0600300E RID: 12302 RVA: 0x000565BE File Offset: 0x000547BE
			public virtual string PublicFolderDatabaseServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["PublicFolderDatabaseServerLegacyDN"] = value;
				}
			}

			// Token: 0x1700168B RID: 5771
			// (set) Token: 0x0600300F RID: 12303 RVA: 0x000565D1 File Offset: 0x000547D1
			public virtual SwitchParameter SkipVerification
			{
				set
				{
					base.PowerSharpParameters["SkipVerification"] = value;
				}
			}

			// Token: 0x1700168C RID: 5772
			// (set) Token: 0x06003010 RID: 12304 RVA: 0x000565E9 File Offset: 0x000547E9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700168D RID: 5773
			// (set) Token: 0x06003011 RID: 12305 RVA: 0x00056607 File Offset: 0x00054807
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700168E RID: 5774
			// (set) Token: 0x06003012 RID: 12306 RVA: 0x00056625 File Offset: 0x00054825
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700168F RID: 5775
			// (set) Token: 0x06003013 RID: 12307 RVA: 0x00056638 File Offset: 0x00054838
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001690 RID: 5776
			// (set) Token: 0x06003014 RID: 12308 RVA: 0x00056650 File Offset: 0x00054850
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001691 RID: 5777
			// (set) Token: 0x06003015 RID: 12309 RVA: 0x00056668 File Offset: 0x00054868
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001692 RID: 5778
			// (set) Token: 0x06003016 RID: 12310 RVA: 0x00056680 File Offset: 0x00054880
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001693 RID: 5779
			// (set) Token: 0x06003017 RID: 12311 RVA: 0x00056698 File Offset: 0x00054898
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
