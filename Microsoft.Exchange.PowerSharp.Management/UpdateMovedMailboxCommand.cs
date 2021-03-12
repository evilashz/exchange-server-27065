using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000ACE RID: 2766
	public class UpdateMovedMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x060088CE RID: 35022 RVA: 0x000C9629 File Offset: 0x000C7829
		private UpdateMovedMailboxCommand() : base("Update-MovedMailbox")
		{
		}

		// Token: 0x060088CF RID: 35023 RVA: 0x000C9636 File Offset: 0x000C7836
		public UpdateMovedMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060088D0 RID: 35024 RVA: 0x000C9645 File Offset: 0x000C7845
		public virtual UpdateMovedMailboxCommand SetParameters(UpdateMovedMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060088D1 RID: 35025 RVA: 0x000C964F File Offset: 0x000C784F
		public virtual UpdateMovedMailboxCommand SetParameters(UpdateMovedMailboxCommand.UpdateMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060088D2 RID: 35026 RVA: 0x000C9659 File Offset: 0x000C7859
		public virtual UpdateMovedMailboxCommand SetParameters(UpdateMovedMailboxCommand.MorphToMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060088D3 RID: 35027 RVA: 0x000C9663 File Offset: 0x000C7863
		public virtual UpdateMovedMailboxCommand SetParameters(UpdateMovedMailboxCommand.MorphToMailUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060088D4 RID: 35028 RVA: 0x000C966D File Offset: 0x000C786D
		public virtual UpdateMovedMailboxCommand SetParameters(UpdateMovedMailboxCommand.UpdateArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000ACF RID: 2767
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005EEB RID: 24299
			// (set) Token: 0x060088D5 RID: 35029 RVA: 0x000C9677 File Offset: 0x000C7877
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005EEC RID: 24300
			// (set) Token: 0x060088D6 RID: 35030 RVA: 0x000C9695 File Offset: 0x000C7895
			public virtual byte PartitionHint
			{
				set
				{
					base.PowerSharpParameters["PartitionHint"] = value;
				}
			}

			// Token: 0x17005EED RID: 24301
			// (set) Token: 0x060088D7 RID: 35031 RVA: 0x000C96AD File Offset: 0x000C78AD
			public virtual Guid NewArchiveMDB
			{
				set
				{
					base.PowerSharpParameters["NewArchiveMDB"] = value;
				}
			}

			// Token: 0x17005EEE RID: 24302
			// (set) Token: 0x060088D8 RID: 35032 RVA: 0x000C96C5 File Offset: 0x000C78C5
			public virtual string ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17005EEF RID: 24303
			// (set) Token: 0x060088D9 RID: 35033 RVA: 0x000C96D8 File Offset: 0x000C78D8
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x17005EF0 RID: 24304
			// (set) Token: 0x060088DA RID: 35034 RVA: 0x000C96F0 File Offset: 0x000C78F0
			public virtual Fqdn ConfigDomainController
			{
				set
				{
					base.PowerSharpParameters["ConfigDomainController"] = value;
				}
			}

			// Token: 0x17005EF1 RID: 24305
			// (set) Token: 0x060088DB RID: 35035 RVA: 0x000C9703 File Offset: 0x000C7903
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005EF2 RID: 24306
			// (set) Token: 0x060088DC RID: 35036 RVA: 0x000C9716 File Offset: 0x000C7916
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005EF3 RID: 24307
			// (set) Token: 0x060088DD RID: 35037 RVA: 0x000C972E File Offset: 0x000C792E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005EF4 RID: 24308
			// (set) Token: 0x060088DE RID: 35038 RVA: 0x000C9746 File Offset: 0x000C7946
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EF5 RID: 24309
			// (set) Token: 0x060088DF RID: 35039 RVA: 0x000C975E File Offset: 0x000C795E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AD0 RID: 2768
		public class UpdateMailboxParameters : ParametersBase
		{
			// Token: 0x17005EF6 RID: 24310
			// (set) Token: 0x060088E1 RID: 35041 RVA: 0x000C977E File Offset: 0x000C797E
			public virtual Guid NewHomeMDB
			{
				set
				{
					base.PowerSharpParameters["NewHomeMDB"] = value;
				}
			}

			// Token: 0x17005EF7 RID: 24311
			// (set) Token: 0x060088E2 RID: 35042 RVA: 0x000C9796 File Offset: 0x000C7996
			public virtual Guid? NewContainerGuid
			{
				set
				{
					base.PowerSharpParameters["NewContainerGuid"] = value;
				}
			}

			// Token: 0x17005EF8 RID: 24312
			// (set) Token: 0x060088E3 RID: 35043 RVA: 0x000C97AE File Offset: 0x000C79AE
			public virtual CrossTenantObjectId NewUnifiedMailboxId
			{
				set
				{
					base.PowerSharpParameters["NewUnifiedMailboxId"] = value;
				}
			}

			// Token: 0x17005EF9 RID: 24313
			// (set) Token: 0x060088E4 RID: 35044 RVA: 0x000C97C1 File Offset: 0x000C79C1
			public virtual SwitchParameter UpdateMailbox
			{
				set
				{
					base.PowerSharpParameters["UpdateMailbox"] = value;
				}
			}

			// Token: 0x17005EFA RID: 24314
			// (set) Token: 0x060088E5 RID: 35045 RVA: 0x000C97D9 File Offset: 0x000C79D9
			public virtual SwitchParameter SkipMailboxReleaseCheck
			{
				set
				{
					base.PowerSharpParameters["SkipMailboxReleaseCheck"] = value;
				}
			}

			// Token: 0x17005EFB RID: 24315
			// (set) Token: 0x060088E6 RID: 35046 RVA: 0x000C97F1 File Offset: 0x000C79F1
			public virtual SwitchParameter SkipProvisioningCheck
			{
				set
				{
					base.PowerSharpParameters["SkipProvisioningCheck"] = value;
				}
			}

			// Token: 0x17005EFC RID: 24316
			// (set) Token: 0x060088E7 RID: 35047 RVA: 0x000C9809 File Offset: 0x000C7A09
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005EFD RID: 24317
			// (set) Token: 0x060088E8 RID: 35048 RVA: 0x000C9827 File Offset: 0x000C7A27
			public virtual byte PartitionHint
			{
				set
				{
					base.PowerSharpParameters["PartitionHint"] = value;
				}
			}

			// Token: 0x17005EFE RID: 24318
			// (set) Token: 0x060088E9 RID: 35049 RVA: 0x000C983F File Offset: 0x000C7A3F
			public virtual Guid NewArchiveMDB
			{
				set
				{
					base.PowerSharpParameters["NewArchiveMDB"] = value;
				}
			}

			// Token: 0x17005EFF RID: 24319
			// (set) Token: 0x060088EA RID: 35050 RVA: 0x000C9857 File Offset: 0x000C7A57
			public virtual string ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17005F00 RID: 24320
			// (set) Token: 0x060088EB RID: 35051 RVA: 0x000C986A File Offset: 0x000C7A6A
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x17005F01 RID: 24321
			// (set) Token: 0x060088EC RID: 35052 RVA: 0x000C9882 File Offset: 0x000C7A82
			public virtual Fqdn ConfigDomainController
			{
				set
				{
					base.PowerSharpParameters["ConfigDomainController"] = value;
				}
			}

			// Token: 0x17005F02 RID: 24322
			// (set) Token: 0x060088ED RID: 35053 RVA: 0x000C9895 File Offset: 0x000C7A95
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F03 RID: 24323
			// (set) Token: 0x060088EE RID: 35054 RVA: 0x000C98A8 File Offset: 0x000C7AA8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F04 RID: 24324
			// (set) Token: 0x060088EF RID: 35055 RVA: 0x000C98C0 File Offset: 0x000C7AC0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F05 RID: 24325
			// (set) Token: 0x060088F0 RID: 35056 RVA: 0x000C98D8 File Offset: 0x000C7AD8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F06 RID: 24326
			// (set) Token: 0x060088F1 RID: 35057 RVA: 0x000C98F0 File Offset: 0x000C7AF0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AD1 RID: 2769
		public class MorphToMailboxParameters : ParametersBase
		{
			// Token: 0x17005F07 RID: 24327
			// (set) Token: 0x060088F3 RID: 35059 RVA: 0x000C9910 File Offset: 0x000C7B10
			public virtual Guid NewHomeMDB
			{
				set
				{
					base.PowerSharpParameters["NewHomeMDB"] = value;
				}
			}

			// Token: 0x17005F08 RID: 24328
			// (set) Token: 0x060088F4 RID: 35060 RVA: 0x000C9928 File Offset: 0x000C7B28
			public virtual SwitchParameter MorphToMailbox
			{
				set
				{
					base.PowerSharpParameters["MorphToMailbox"] = value;
				}
			}

			// Token: 0x17005F09 RID: 24329
			// (set) Token: 0x060088F5 RID: 35061 RVA: 0x000C9940 File Offset: 0x000C7B40
			public virtual ADUser RemoteRecipientData
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientData"] = value;
				}
			}

			// Token: 0x17005F0A RID: 24330
			// (set) Token: 0x060088F6 RID: 35062 RVA: 0x000C9953 File Offset: 0x000C7B53
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17005F0B RID: 24331
			// (set) Token: 0x060088F7 RID: 35063 RVA: 0x000C9966 File Offset: 0x000C7B66
			public virtual SwitchParameter SkipProvisioningCheck
			{
				set
				{
					base.PowerSharpParameters["SkipProvisioningCheck"] = value;
				}
			}

			// Token: 0x17005F0C RID: 24332
			// (set) Token: 0x060088F8 RID: 35064 RVA: 0x000C997E File Offset: 0x000C7B7E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F0D RID: 24333
			// (set) Token: 0x060088F9 RID: 35065 RVA: 0x000C999C File Offset: 0x000C7B9C
			public virtual byte PartitionHint
			{
				set
				{
					base.PowerSharpParameters["PartitionHint"] = value;
				}
			}

			// Token: 0x17005F0E RID: 24334
			// (set) Token: 0x060088FA RID: 35066 RVA: 0x000C99B4 File Offset: 0x000C7BB4
			public virtual Guid NewArchiveMDB
			{
				set
				{
					base.PowerSharpParameters["NewArchiveMDB"] = value;
				}
			}

			// Token: 0x17005F0F RID: 24335
			// (set) Token: 0x060088FB RID: 35067 RVA: 0x000C99CC File Offset: 0x000C7BCC
			public virtual string ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17005F10 RID: 24336
			// (set) Token: 0x060088FC RID: 35068 RVA: 0x000C99DF File Offset: 0x000C7BDF
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x17005F11 RID: 24337
			// (set) Token: 0x060088FD RID: 35069 RVA: 0x000C99F7 File Offset: 0x000C7BF7
			public virtual Fqdn ConfigDomainController
			{
				set
				{
					base.PowerSharpParameters["ConfigDomainController"] = value;
				}
			}

			// Token: 0x17005F12 RID: 24338
			// (set) Token: 0x060088FE RID: 35070 RVA: 0x000C9A0A File Offset: 0x000C7C0A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F13 RID: 24339
			// (set) Token: 0x060088FF RID: 35071 RVA: 0x000C9A1D File Offset: 0x000C7C1D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F14 RID: 24340
			// (set) Token: 0x06008900 RID: 35072 RVA: 0x000C9A35 File Offset: 0x000C7C35
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F15 RID: 24341
			// (set) Token: 0x06008901 RID: 35073 RVA: 0x000C9A4D File Offset: 0x000C7C4D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F16 RID: 24342
			// (set) Token: 0x06008902 RID: 35074 RVA: 0x000C9A65 File Offset: 0x000C7C65
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AD2 RID: 2770
		public class MorphToMailUserParameters : ParametersBase
		{
			// Token: 0x17005F17 RID: 24343
			// (set) Token: 0x06008904 RID: 35076 RVA: 0x000C9A85 File Offset: 0x000C7C85
			public virtual SwitchParameter MorphToMailUser
			{
				set
				{
					base.PowerSharpParameters["MorphToMailUser"] = value;
				}
			}

			// Token: 0x17005F18 RID: 24344
			// (set) Token: 0x06008905 RID: 35077 RVA: 0x000C9A9D File Offset: 0x000C7C9D
			public virtual ADUser RemoteRecipientData
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientData"] = value;
				}
			}

			// Token: 0x17005F19 RID: 24345
			// (set) Token: 0x06008906 RID: 35078 RVA: 0x000C9AB0 File Offset: 0x000C7CB0
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17005F1A RID: 24346
			// (set) Token: 0x06008907 RID: 35079 RVA: 0x000C9AC3 File Offset: 0x000C7CC3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F1B RID: 24347
			// (set) Token: 0x06008908 RID: 35080 RVA: 0x000C9AE1 File Offset: 0x000C7CE1
			public virtual byte PartitionHint
			{
				set
				{
					base.PowerSharpParameters["PartitionHint"] = value;
				}
			}

			// Token: 0x17005F1C RID: 24348
			// (set) Token: 0x06008909 RID: 35081 RVA: 0x000C9AF9 File Offset: 0x000C7CF9
			public virtual Guid NewArchiveMDB
			{
				set
				{
					base.PowerSharpParameters["NewArchiveMDB"] = value;
				}
			}

			// Token: 0x17005F1D RID: 24349
			// (set) Token: 0x0600890A RID: 35082 RVA: 0x000C9B11 File Offset: 0x000C7D11
			public virtual string ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17005F1E RID: 24350
			// (set) Token: 0x0600890B RID: 35083 RVA: 0x000C9B24 File Offset: 0x000C7D24
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x17005F1F RID: 24351
			// (set) Token: 0x0600890C RID: 35084 RVA: 0x000C9B3C File Offset: 0x000C7D3C
			public virtual Fqdn ConfigDomainController
			{
				set
				{
					base.PowerSharpParameters["ConfigDomainController"] = value;
				}
			}

			// Token: 0x17005F20 RID: 24352
			// (set) Token: 0x0600890D RID: 35085 RVA: 0x000C9B4F File Offset: 0x000C7D4F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F21 RID: 24353
			// (set) Token: 0x0600890E RID: 35086 RVA: 0x000C9B62 File Offset: 0x000C7D62
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F22 RID: 24354
			// (set) Token: 0x0600890F RID: 35087 RVA: 0x000C9B7A File Offset: 0x000C7D7A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F23 RID: 24355
			// (set) Token: 0x06008910 RID: 35088 RVA: 0x000C9B92 File Offset: 0x000C7D92
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F24 RID: 24356
			// (set) Token: 0x06008911 RID: 35089 RVA: 0x000C9BAA File Offset: 0x000C7DAA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AD3 RID: 2771
		public class UpdateArchiveParameters : ParametersBase
		{
			// Token: 0x17005F25 RID: 24357
			// (set) Token: 0x06008913 RID: 35091 RVA: 0x000C9BCA File Offset: 0x000C7DCA
			public virtual SwitchParameter UpdateArchiveOnly
			{
				set
				{
					base.PowerSharpParameters["UpdateArchiveOnly"] = value;
				}
			}

			// Token: 0x17005F26 RID: 24358
			// (set) Token: 0x06008914 RID: 35092 RVA: 0x000C9BE2 File Offset: 0x000C7DE2
			public virtual ADUser RemoteRecipientData
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientData"] = value;
				}
			}

			// Token: 0x17005F27 RID: 24359
			// (set) Token: 0x06008915 RID: 35093 RVA: 0x000C9BF5 File Offset: 0x000C7DF5
			public virtual SwitchParameter SkipMailboxReleaseCheck
			{
				set
				{
					base.PowerSharpParameters["SkipMailboxReleaseCheck"] = value;
				}
			}

			// Token: 0x17005F28 RID: 24360
			// (set) Token: 0x06008916 RID: 35094 RVA: 0x000C9C0D File Offset: 0x000C7E0D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F29 RID: 24361
			// (set) Token: 0x06008917 RID: 35095 RVA: 0x000C9C2B File Offset: 0x000C7E2B
			public virtual byte PartitionHint
			{
				set
				{
					base.PowerSharpParameters["PartitionHint"] = value;
				}
			}

			// Token: 0x17005F2A RID: 24362
			// (set) Token: 0x06008918 RID: 35096 RVA: 0x000C9C43 File Offset: 0x000C7E43
			public virtual Guid NewArchiveMDB
			{
				set
				{
					base.PowerSharpParameters["NewArchiveMDB"] = value;
				}
			}

			// Token: 0x17005F2B RID: 24363
			// (set) Token: 0x06008919 RID: 35097 RVA: 0x000C9C5B File Offset: 0x000C7E5B
			public virtual string ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17005F2C RID: 24364
			// (set) Token: 0x0600891A RID: 35098 RVA: 0x000C9C6E File Offset: 0x000C7E6E
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x17005F2D RID: 24365
			// (set) Token: 0x0600891B RID: 35099 RVA: 0x000C9C86 File Offset: 0x000C7E86
			public virtual Fqdn ConfigDomainController
			{
				set
				{
					base.PowerSharpParameters["ConfigDomainController"] = value;
				}
			}

			// Token: 0x17005F2E RID: 24366
			// (set) Token: 0x0600891C RID: 35100 RVA: 0x000C9C99 File Offset: 0x000C7E99
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F2F RID: 24367
			// (set) Token: 0x0600891D RID: 35101 RVA: 0x000C9CAC File Offset: 0x000C7EAC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F30 RID: 24368
			// (set) Token: 0x0600891E RID: 35102 RVA: 0x000C9CC4 File Offset: 0x000C7EC4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F31 RID: 24369
			// (set) Token: 0x0600891F RID: 35103 RVA: 0x000C9CDC File Offset: 0x000C7EDC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F32 RID: 24370
			// (set) Token: 0x06008920 RID: 35104 RVA: 0x000C9CF4 File Offset: 0x000C7EF4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
