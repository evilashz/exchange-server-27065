using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C33 RID: 3123
	public class NewGroupMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06009847 RID: 38983 RVA: 0x000DD597 File Offset: 0x000DB797
		private NewGroupMailboxCommand() : base("New-GroupMailbox")
		{
		}

		// Token: 0x06009848 RID: 38984 RVA: 0x000DD5A4 File Offset: 0x000DB7A4
		public NewGroupMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009849 RID: 38985 RVA: 0x000DD5B3 File Offset: 0x000DB7B3
		public virtual NewGroupMailboxCommand SetParameters(NewGroupMailboxCommand.GroupMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600984A RID: 38986 RVA: 0x000DD5BD File Offset: 0x000DB7BD
		public virtual NewGroupMailboxCommand SetParameters(NewGroupMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600984B RID: 38987 RVA: 0x000DD5C7 File Offset: 0x000DB7C7
		public virtual NewGroupMailboxCommand SetParameters(NewGroupMailboxCommand.LinkedRoomMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600984C RID: 38988 RVA: 0x000DD5D1 File Offset: 0x000DB7D1
		public virtual NewGroupMailboxCommand SetParameters(NewGroupMailboxCommand.AuxMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600984D RID: 38989 RVA: 0x000DD5DB File Offset: 0x000DB7DB
		public virtual NewGroupMailboxCommand SetParameters(NewGroupMailboxCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C34 RID: 3124
		public class GroupMailboxParameters : ParametersBase
		{
			// Token: 0x17006B9A RID: 27546
			// (set) Token: 0x0600984E RID: 38990 RVA: 0x000DD5E5 File Offset: 0x000DB7E5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006B9B RID: 27547
			// (set) Token: 0x0600984F RID: 38991 RVA: 0x000DD5F8 File Offset: 0x000DB7F8
			public virtual ModernGroupTypeInfo ModernGroupType
			{
				set
				{
					base.PowerSharpParameters["ModernGroupType"] = value;
				}
			}

			// Token: 0x17006B9C RID: 27548
			// (set) Token: 0x06009850 RID: 38992 RVA: 0x000DD610 File Offset: 0x000DB810
			public virtual RecipientIdParameter Owners
			{
				set
				{
					base.PowerSharpParameters["Owners"] = value;
				}
			}

			// Token: 0x17006B9D RID: 27549
			// (set) Token: 0x06009851 RID: 38993 RVA: 0x000DD623 File Offset: 0x000DB823
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17006B9E RID: 27550
			// (set) Token: 0x06009852 RID: 38994 RVA: 0x000DD636 File Offset: 0x000DB836
			public virtual string ExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ExecutingUser"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006B9F RID: 27551
			// (set) Token: 0x06009853 RID: 38995 RVA: 0x000DD654 File Offset: 0x000DB854
			public virtual RecipientIdParameter Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17006BA0 RID: 27552
			// (set) Token: 0x06009854 RID: 38996 RVA: 0x000DD667 File Offset: 0x000DB867
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17006BA1 RID: 27553
			// (set) Token: 0x06009855 RID: 38997 RVA: 0x000DD67F File Offset: 0x000DB87F
			public virtual SwitchParameter AutoSubscribeNewGroupMembers
			{
				set
				{
					base.PowerSharpParameters["AutoSubscribeNewGroupMembers"] = value;
				}
			}

			// Token: 0x17006BA2 RID: 27554
			// (set) Token: 0x06009856 RID: 38998 RVA: 0x000DD697 File Offset: 0x000DB897
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x17006BA3 RID: 27555
			// (set) Token: 0x06009857 RID: 38999 RVA: 0x000DD6AA File Offset: 0x000DB8AA
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17006BA4 RID: 27556
			// (set) Token: 0x06009858 RID: 39000 RVA: 0x000DD6BD File Offset: 0x000DB8BD
			public virtual MultiValuedProperty<string> SharePointResources
			{
				set
				{
					base.PowerSharpParameters["SharePointResources"] = value;
				}
			}

			// Token: 0x17006BA5 RID: 27557
			// (set) Token: 0x06009859 RID: 39001 RVA: 0x000DD6D0 File Offset: 0x000DB8D0
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17006BA6 RID: 27558
			// (set) Token: 0x0600985A RID: 39002 RVA: 0x000DD6E3 File Offset: 0x000DB8E3
			public virtual RecipientIdType RecipientIdType
			{
				set
				{
					base.PowerSharpParameters["RecipientIdType"] = value;
				}
			}

			// Token: 0x17006BA7 RID: 27559
			// (set) Token: 0x0600985B RID: 39003 RVA: 0x000DD6FB File Offset: 0x000DB8FB
			public virtual SwitchParameter FromSyncClient
			{
				set
				{
					base.PowerSharpParameters["FromSyncClient"] = value;
				}
			}

			// Token: 0x17006BA8 RID: 27560
			// (set) Token: 0x0600985C RID: 39004 RVA: 0x000DD713 File Offset: 0x000DB913
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006BA9 RID: 27561
			// (set) Token: 0x0600985D RID: 39005 RVA: 0x000DD726 File Offset: 0x000DB926
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17006BAA RID: 27562
			// (set) Token: 0x0600985E RID: 39006 RVA: 0x000DD739 File Offset: 0x000DB939
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17006BAB RID: 27563
			// (set) Token: 0x0600985F RID: 39007 RVA: 0x000DD74C File Offset: 0x000DB94C
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006BAC RID: 27564
			// (set) Token: 0x06009860 RID: 39008 RVA: 0x000DD75F File Offset: 0x000DB95F
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17006BAD RID: 27565
			// (set) Token: 0x06009861 RID: 39009 RVA: 0x000DD772 File Offset: 0x000DB972
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006BAE RID: 27566
			// (set) Token: 0x06009862 RID: 39010 RVA: 0x000DD78A File Offset: 0x000DB98A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006BAF RID: 27567
			// (set) Token: 0x06009863 RID: 39011 RVA: 0x000DD79D File Offset: 0x000DB99D
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006BB0 RID: 27568
			// (set) Token: 0x06009864 RID: 39012 RVA: 0x000DD7B5 File Offset: 0x000DB9B5
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006BB1 RID: 27569
			// (set) Token: 0x06009865 RID: 39013 RVA: 0x000DD7C8 File Offset: 0x000DB9C8
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006BB2 RID: 27570
			// (set) Token: 0x06009866 RID: 39014 RVA: 0x000DD7E6 File Offset: 0x000DB9E6
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006BB3 RID: 27571
			// (set) Token: 0x06009867 RID: 39015 RVA: 0x000DD7F9 File Offset: 0x000DB9F9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006BB4 RID: 27572
			// (set) Token: 0x06009868 RID: 39016 RVA: 0x000DD817 File Offset: 0x000DBA17
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006BB5 RID: 27573
			// (set) Token: 0x06009869 RID: 39017 RVA: 0x000DD82A File Offset: 0x000DBA2A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006BB6 RID: 27574
			// (set) Token: 0x0600986A RID: 39018 RVA: 0x000DD842 File Offset: 0x000DBA42
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006BB7 RID: 27575
			// (set) Token: 0x0600986B RID: 39019 RVA: 0x000DD85A File Offset: 0x000DBA5A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006BB8 RID: 27576
			// (set) Token: 0x0600986C RID: 39020 RVA: 0x000DD872 File Offset: 0x000DBA72
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006BB9 RID: 27577
			// (set) Token: 0x0600986D RID: 39021 RVA: 0x000DD88A File Offset: 0x000DBA8A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C35 RID: 3125
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006BBA RID: 27578
			// (set) Token: 0x0600986F RID: 39023 RVA: 0x000DD8AA File Offset: 0x000DBAAA
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006BBB RID: 27579
			// (set) Token: 0x06009870 RID: 39024 RVA: 0x000DD8BD File Offset: 0x000DBABD
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17006BBC RID: 27580
			// (set) Token: 0x06009871 RID: 39025 RVA: 0x000DD8D0 File Offset: 0x000DBAD0
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17006BBD RID: 27581
			// (set) Token: 0x06009872 RID: 39026 RVA: 0x000DD8E3 File Offset: 0x000DBAE3
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006BBE RID: 27582
			// (set) Token: 0x06009873 RID: 39027 RVA: 0x000DD8F6 File Offset: 0x000DBAF6
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17006BBF RID: 27583
			// (set) Token: 0x06009874 RID: 39028 RVA: 0x000DD909 File Offset: 0x000DBB09
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006BC0 RID: 27584
			// (set) Token: 0x06009875 RID: 39029 RVA: 0x000DD921 File Offset: 0x000DBB21
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006BC1 RID: 27585
			// (set) Token: 0x06009876 RID: 39030 RVA: 0x000DD934 File Offset: 0x000DBB34
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006BC2 RID: 27586
			// (set) Token: 0x06009877 RID: 39031 RVA: 0x000DD94C File Offset: 0x000DBB4C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006BC3 RID: 27587
			// (set) Token: 0x06009878 RID: 39032 RVA: 0x000DD95F File Offset: 0x000DBB5F
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006BC4 RID: 27588
			// (set) Token: 0x06009879 RID: 39033 RVA: 0x000DD97D File Offset: 0x000DBB7D
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006BC5 RID: 27589
			// (set) Token: 0x0600987A RID: 39034 RVA: 0x000DD990 File Offset: 0x000DBB90
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006BC6 RID: 27590
			// (set) Token: 0x0600987B RID: 39035 RVA: 0x000DD9AE File Offset: 0x000DBBAE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006BC7 RID: 27591
			// (set) Token: 0x0600987C RID: 39036 RVA: 0x000DD9C1 File Offset: 0x000DBBC1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006BC8 RID: 27592
			// (set) Token: 0x0600987D RID: 39037 RVA: 0x000DD9D9 File Offset: 0x000DBBD9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006BC9 RID: 27593
			// (set) Token: 0x0600987E RID: 39038 RVA: 0x000DD9F1 File Offset: 0x000DBBF1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006BCA RID: 27594
			// (set) Token: 0x0600987F RID: 39039 RVA: 0x000DDA09 File Offset: 0x000DBC09
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006BCB RID: 27595
			// (set) Token: 0x06009880 RID: 39040 RVA: 0x000DDA21 File Offset: 0x000DBC21
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C36 RID: 3126
		public class LinkedRoomMailboxParameters : ParametersBase
		{
			// Token: 0x17006BCC RID: 27596
			// (set) Token: 0x06009882 RID: 39042 RVA: 0x000DDA41 File Offset: 0x000DBC41
			public virtual SwitchParameter LinkedRoom
			{
				set
				{
					base.PowerSharpParameters["LinkedRoom"] = value;
				}
			}

			// Token: 0x17006BCD RID: 27597
			// (set) Token: 0x06009883 RID: 39043 RVA: 0x000DDA59 File Offset: 0x000DBC59
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006BCE RID: 27598
			// (set) Token: 0x06009884 RID: 39044 RVA: 0x000DDA6C File Offset: 0x000DBC6C
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17006BCF RID: 27599
			// (set) Token: 0x06009885 RID: 39045 RVA: 0x000DDA7F File Offset: 0x000DBC7F
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17006BD0 RID: 27600
			// (set) Token: 0x06009886 RID: 39046 RVA: 0x000DDA92 File Offset: 0x000DBC92
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006BD1 RID: 27601
			// (set) Token: 0x06009887 RID: 39047 RVA: 0x000DDAA5 File Offset: 0x000DBCA5
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17006BD2 RID: 27602
			// (set) Token: 0x06009888 RID: 39048 RVA: 0x000DDAB8 File Offset: 0x000DBCB8
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006BD3 RID: 27603
			// (set) Token: 0x06009889 RID: 39049 RVA: 0x000DDAD0 File Offset: 0x000DBCD0
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006BD4 RID: 27604
			// (set) Token: 0x0600988A RID: 39050 RVA: 0x000DDAE3 File Offset: 0x000DBCE3
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006BD5 RID: 27605
			// (set) Token: 0x0600988B RID: 39051 RVA: 0x000DDAFB File Offset: 0x000DBCFB
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006BD6 RID: 27606
			// (set) Token: 0x0600988C RID: 39052 RVA: 0x000DDB0E File Offset: 0x000DBD0E
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006BD7 RID: 27607
			// (set) Token: 0x0600988D RID: 39053 RVA: 0x000DDB2C File Offset: 0x000DBD2C
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006BD8 RID: 27608
			// (set) Token: 0x0600988E RID: 39054 RVA: 0x000DDB3F File Offset: 0x000DBD3F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006BD9 RID: 27609
			// (set) Token: 0x0600988F RID: 39055 RVA: 0x000DDB5D File Offset: 0x000DBD5D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006BDA RID: 27610
			// (set) Token: 0x06009890 RID: 39056 RVA: 0x000DDB70 File Offset: 0x000DBD70
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006BDB RID: 27611
			// (set) Token: 0x06009891 RID: 39057 RVA: 0x000DDB88 File Offset: 0x000DBD88
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006BDC RID: 27612
			// (set) Token: 0x06009892 RID: 39058 RVA: 0x000DDBA0 File Offset: 0x000DBDA0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006BDD RID: 27613
			// (set) Token: 0x06009893 RID: 39059 RVA: 0x000DDBB8 File Offset: 0x000DBDB8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006BDE RID: 27614
			// (set) Token: 0x06009894 RID: 39060 RVA: 0x000DDBD0 File Offset: 0x000DBDD0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C37 RID: 3127
		public class AuxMailboxParameters : ParametersBase
		{
			// Token: 0x17006BDF RID: 27615
			// (set) Token: 0x06009896 RID: 39062 RVA: 0x000DDBF0 File Offset: 0x000DBDF0
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17006BE0 RID: 27616
			// (set) Token: 0x06009897 RID: 39063 RVA: 0x000DDC08 File Offset: 0x000DBE08
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006BE1 RID: 27617
			// (set) Token: 0x06009898 RID: 39064 RVA: 0x000DDC1B File Offset: 0x000DBE1B
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17006BE2 RID: 27618
			// (set) Token: 0x06009899 RID: 39065 RVA: 0x000DDC2E File Offset: 0x000DBE2E
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17006BE3 RID: 27619
			// (set) Token: 0x0600989A RID: 39066 RVA: 0x000DDC41 File Offset: 0x000DBE41
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006BE4 RID: 27620
			// (set) Token: 0x0600989B RID: 39067 RVA: 0x000DDC54 File Offset: 0x000DBE54
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17006BE5 RID: 27621
			// (set) Token: 0x0600989C RID: 39068 RVA: 0x000DDC67 File Offset: 0x000DBE67
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006BE6 RID: 27622
			// (set) Token: 0x0600989D RID: 39069 RVA: 0x000DDC7F File Offset: 0x000DBE7F
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006BE7 RID: 27623
			// (set) Token: 0x0600989E RID: 39070 RVA: 0x000DDC92 File Offset: 0x000DBE92
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006BE8 RID: 27624
			// (set) Token: 0x0600989F RID: 39071 RVA: 0x000DDCAA File Offset: 0x000DBEAA
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006BE9 RID: 27625
			// (set) Token: 0x060098A0 RID: 39072 RVA: 0x000DDCBD File Offset: 0x000DBEBD
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006BEA RID: 27626
			// (set) Token: 0x060098A1 RID: 39073 RVA: 0x000DDCDB File Offset: 0x000DBEDB
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006BEB RID: 27627
			// (set) Token: 0x060098A2 RID: 39074 RVA: 0x000DDCEE File Offset: 0x000DBEEE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006BEC RID: 27628
			// (set) Token: 0x060098A3 RID: 39075 RVA: 0x000DDD0C File Offset: 0x000DBF0C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006BED RID: 27629
			// (set) Token: 0x060098A4 RID: 39076 RVA: 0x000DDD1F File Offset: 0x000DBF1F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006BEE RID: 27630
			// (set) Token: 0x060098A5 RID: 39077 RVA: 0x000DDD37 File Offset: 0x000DBF37
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006BEF RID: 27631
			// (set) Token: 0x060098A6 RID: 39078 RVA: 0x000DDD4F File Offset: 0x000DBF4F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006BF0 RID: 27632
			// (set) Token: 0x060098A7 RID: 39079 RVA: 0x000DDD67 File Offset: 0x000DBF67
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006BF1 RID: 27633
			// (set) Token: 0x060098A8 RID: 39080 RVA: 0x000DDD7F File Offset: 0x000DBF7F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C38 RID: 3128
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x17006BF2 RID: 27634
			// (set) Token: 0x060098AA RID: 39082 RVA: 0x000DDD9F File Offset: 0x000DBF9F
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006BF3 RID: 27635
			// (set) Token: 0x060098AB RID: 39083 RVA: 0x000DDDB7 File Offset: 0x000DBFB7
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006BF4 RID: 27636
			// (set) Token: 0x060098AC RID: 39084 RVA: 0x000DDDCA File Offset: 0x000DBFCA
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17006BF5 RID: 27637
			// (set) Token: 0x060098AD RID: 39085 RVA: 0x000DDDDD File Offset: 0x000DBFDD
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17006BF6 RID: 27638
			// (set) Token: 0x060098AE RID: 39086 RVA: 0x000DDDF0 File Offset: 0x000DBFF0
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006BF7 RID: 27639
			// (set) Token: 0x060098AF RID: 39087 RVA: 0x000DDE03 File Offset: 0x000DC003
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17006BF8 RID: 27640
			// (set) Token: 0x060098B0 RID: 39088 RVA: 0x000DDE16 File Offset: 0x000DC016
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006BF9 RID: 27641
			// (set) Token: 0x060098B1 RID: 39089 RVA: 0x000DDE2E File Offset: 0x000DC02E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006BFA RID: 27642
			// (set) Token: 0x060098B2 RID: 39090 RVA: 0x000DDE41 File Offset: 0x000DC041
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006BFB RID: 27643
			// (set) Token: 0x060098B3 RID: 39091 RVA: 0x000DDE59 File Offset: 0x000DC059
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006BFC RID: 27644
			// (set) Token: 0x060098B4 RID: 39092 RVA: 0x000DDE6C File Offset: 0x000DC06C
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006BFD RID: 27645
			// (set) Token: 0x060098B5 RID: 39093 RVA: 0x000DDE8A File Offset: 0x000DC08A
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006BFE RID: 27646
			// (set) Token: 0x060098B6 RID: 39094 RVA: 0x000DDE9D File Offset: 0x000DC09D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006BFF RID: 27647
			// (set) Token: 0x060098B7 RID: 39095 RVA: 0x000DDEBB File Offset: 0x000DC0BB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006C00 RID: 27648
			// (set) Token: 0x060098B8 RID: 39096 RVA: 0x000DDECE File Offset: 0x000DC0CE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006C01 RID: 27649
			// (set) Token: 0x060098B9 RID: 39097 RVA: 0x000DDEE6 File Offset: 0x000DC0E6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006C02 RID: 27650
			// (set) Token: 0x060098BA RID: 39098 RVA: 0x000DDEFE File Offset: 0x000DC0FE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006C03 RID: 27651
			// (set) Token: 0x060098BB RID: 39099 RVA: 0x000DDF16 File Offset: 0x000DC116
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006C04 RID: 27652
			// (set) Token: 0x060098BC RID: 39100 RVA: 0x000DDF2E File Offset: 0x000DC12E
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
