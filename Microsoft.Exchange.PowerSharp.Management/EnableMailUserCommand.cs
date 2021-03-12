using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CF9 RID: 3321
	public class EnableMailUserCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x0600AECD RID: 44749 RVA: 0x000FC779 File Offset: 0x000FA979
		private EnableMailUserCommand() : base("Enable-MailUser")
		{
		}

		// Token: 0x0600AECE RID: 44750 RVA: 0x000FC786 File Offset: 0x000FA986
		public EnableMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AECF RID: 44751 RVA: 0x000FC795 File Offset: 0x000FA995
		public virtual EnableMailUserCommand SetParameters(EnableMailUserCommand.ArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AED0 RID: 44752 RVA: 0x000FC79F File Offset: 0x000FA99F
		public virtual EnableMailUserCommand SetParameters(EnableMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AED1 RID: 44753 RVA: 0x000FC7A9 File Offset: 0x000FA9A9
		public virtual EnableMailUserCommand SetParameters(EnableMailUserCommand.EnabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CFA RID: 3322
		public class ArchiveParameters : ParametersBase
		{
			// Token: 0x17008094 RID: 32916
			// (set) Token: 0x0600AED2 RID: 44754 RVA: 0x000FC7B3 File Offset: 0x000FA9B3
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008095 RID: 32917
			// (set) Token: 0x0600AED3 RID: 44755 RVA: 0x000FC7CB File Offset: 0x000FA9CB
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008096 RID: 32918
			// (set) Token: 0x0600AED4 RID: 44756 RVA: 0x000FC7E3 File Offset: 0x000FA9E3
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008097 RID: 32919
			// (set) Token: 0x0600AED5 RID: 44757 RVA: 0x000FC7F6 File Offset: 0x000FA9F6
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x17008098 RID: 32920
			// (set) Token: 0x0600AED6 RID: 44758 RVA: 0x000FC80E File Offset: 0x000FAA0E
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x17008099 RID: 32921
			// (set) Token: 0x0600AED7 RID: 44759 RVA: 0x000FC826 File Offset: 0x000FAA26
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700809A RID: 32922
			// (set) Token: 0x0600AED8 RID: 44760 RVA: 0x000FC83E File Offset: 0x000FAA3E
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700809B RID: 32923
			// (set) Token: 0x0600AED9 RID: 44761 RVA: 0x000FC851 File Offset: 0x000FAA51
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700809C RID: 32924
			// (set) Token: 0x0600AEDA RID: 44762 RVA: 0x000FC869 File Offset: 0x000FAA69
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x1700809D RID: 32925
			// (set) Token: 0x0600AEDB RID: 44763 RVA: 0x000FC881 File Offset: 0x000FAA81
			public virtual SwitchParameter PreserveEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["PreserveEmailAddresses"] = value;
				}
			}

			// Token: 0x1700809E RID: 32926
			// (set) Token: 0x0600AEDC RID: 44764 RVA: 0x000FC899 File Offset: 0x000FAA99
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700809F RID: 32927
			// (set) Token: 0x0600AEDD RID: 44765 RVA: 0x000FC8B7 File Offset: 0x000FAAB7
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170080A0 RID: 32928
			// (set) Token: 0x0600AEDE RID: 44766 RVA: 0x000FC8CA File Offset: 0x000FAACA
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170080A1 RID: 32929
			// (set) Token: 0x0600AEDF RID: 44767 RVA: 0x000FC8DD File Offset: 0x000FAADD
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170080A2 RID: 32930
			// (set) Token: 0x0600AEE0 RID: 44768 RVA: 0x000FC8F5 File Offset: 0x000FAAF5
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170080A3 RID: 32931
			// (set) Token: 0x0600AEE1 RID: 44769 RVA: 0x000FC90D File Offset: 0x000FAB0D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170080A4 RID: 32932
			// (set) Token: 0x0600AEE2 RID: 44770 RVA: 0x000FC920 File Offset: 0x000FAB20
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170080A5 RID: 32933
			// (set) Token: 0x0600AEE3 RID: 44771 RVA: 0x000FC938 File Offset: 0x000FAB38
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170080A6 RID: 32934
			// (set) Token: 0x0600AEE4 RID: 44772 RVA: 0x000FC950 File Offset: 0x000FAB50
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170080A7 RID: 32935
			// (set) Token: 0x0600AEE5 RID: 44773 RVA: 0x000FC968 File Offset: 0x000FAB68
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170080A8 RID: 32936
			// (set) Token: 0x0600AEE6 RID: 44774 RVA: 0x000FC980 File Offset: 0x000FAB80
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CFB RID: 3323
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170080A9 RID: 32937
			// (set) Token: 0x0600AEE8 RID: 44776 RVA: 0x000FC9A0 File Offset: 0x000FABA0
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x170080AA RID: 32938
			// (set) Token: 0x0600AEE9 RID: 44777 RVA: 0x000FC9B8 File Offset: 0x000FABB8
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170080AB RID: 32939
			// (set) Token: 0x0600AEEA RID: 44778 RVA: 0x000FC9D0 File Offset: 0x000FABD0
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170080AC RID: 32940
			// (set) Token: 0x0600AEEB RID: 44779 RVA: 0x000FC9E8 File Offset: 0x000FABE8
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170080AD RID: 32941
			// (set) Token: 0x0600AEEC RID: 44780 RVA: 0x000FC9FB File Offset: 0x000FABFB
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170080AE RID: 32942
			// (set) Token: 0x0600AEED RID: 44781 RVA: 0x000FCA13 File Offset: 0x000FAC13
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170080AF RID: 32943
			// (set) Token: 0x0600AEEE RID: 44782 RVA: 0x000FCA2B File Offset: 0x000FAC2B
			public virtual SwitchParameter PreserveEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["PreserveEmailAddresses"] = value;
				}
			}

			// Token: 0x170080B0 RID: 32944
			// (set) Token: 0x0600AEEF RID: 44783 RVA: 0x000FCA43 File Offset: 0x000FAC43
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x170080B1 RID: 32945
			// (set) Token: 0x0600AEF0 RID: 44784 RVA: 0x000FCA61 File Offset: 0x000FAC61
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170080B2 RID: 32946
			// (set) Token: 0x0600AEF1 RID: 44785 RVA: 0x000FCA74 File Offset: 0x000FAC74
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170080B3 RID: 32947
			// (set) Token: 0x0600AEF2 RID: 44786 RVA: 0x000FCA87 File Offset: 0x000FAC87
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170080B4 RID: 32948
			// (set) Token: 0x0600AEF3 RID: 44787 RVA: 0x000FCA9F File Offset: 0x000FAC9F
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170080B5 RID: 32949
			// (set) Token: 0x0600AEF4 RID: 44788 RVA: 0x000FCAB7 File Offset: 0x000FACB7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170080B6 RID: 32950
			// (set) Token: 0x0600AEF5 RID: 44789 RVA: 0x000FCACA File Offset: 0x000FACCA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170080B7 RID: 32951
			// (set) Token: 0x0600AEF6 RID: 44790 RVA: 0x000FCAE2 File Offset: 0x000FACE2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170080B8 RID: 32952
			// (set) Token: 0x0600AEF7 RID: 44791 RVA: 0x000FCAFA File Offset: 0x000FACFA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170080B9 RID: 32953
			// (set) Token: 0x0600AEF8 RID: 44792 RVA: 0x000FCB12 File Offset: 0x000FAD12
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170080BA RID: 32954
			// (set) Token: 0x0600AEF9 RID: 44793 RVA: 0x000FCB2A File Offset: 0x000FAD2A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CFC RID: 3324
		public class EnabledUserParameters : ParametersBase
		{
			// Token: 0x170080BB RID: 32955
			// (set) Token: 0x0600AEFB RID: 44795 RVA: 0x000FCB4A File Offset: 0x000FAD4A
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x170080BC RID: 32956
			// (set) Token: 0x0600AEFC RID: 44796 RVA: 0x000FCB5D File Offset: 0x000FAD5D
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170080BD RID: 32957
			// (set) Token: 0x0600AEFD RID: 44797 RVA: 0x000FCB70 File Offset: 0x000FAD70
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x170080BE RID: 32958
			// (set) Token: 0x0600AEFE RID: 44798 RVA: 0x000FCB88 File Offset: 0x000FAD88
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x170080BF RID: 32959
			// (set) Token: 0x0600AEFF RID: 44799 RVA: 0x000FCBA0 File Offset: 0x000FADA0
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x170080C0 RID: 32960
			// (set) Token: 0x0600AF00 RID: 44800 RVA: 0x000FCBB8 File Offset: 0x000FADB8
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x170080C1 RID: 32961
			// (set) Token: 0x0600AF01 RID: 44801 RVA: 0x000FCBD0 File Offset: 0x000FADD0
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x170080C2 RID: 32962
			// (set) Token: 0x0600AF02 RID: 44802 RVA: 0x000FCBE8 File Offset: 0x000FADE8
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170080C3 RID: 32963
			// (set) Token: 0x0600AF03 RID: 44803 RVA: 0x000FCC00 File Offset: 0x000FAE00
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170080C4 RID: 32964
			// (set) Token: 0x0600AF04 RID: 44804 RVA: 0x000FCC18 File Offset: 0x000FAE18
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170080C5 RID: 32965
			// (set) Token: 0x0600AF05 RID: 44805 RVA: 0x000FCC2B File Offset: 0x000FAE2B
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170080C6 RID: 32966
			// (set) Token: 0x0600AF06 RID: 44806 RVA: 0x000FCC43 File Offset: 0x000FAE43
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170080C7 RID: 32967
			// (set) Token: 0x0600AF07 RID: 44807 RVA: 0x000FCC5B File Offset: 0x000FAE5B
			public virtual SwitchParameter PreserveEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["PreserveEmailAddresses"] = value;
				}
			}

			// Token: 0x170080C8 RID: 32968
			// (set) Token: 0x0600AF08 RID: 44808 RVA: 0x000FCC73 File Offset: 0x000FAE73
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x170080C9 RID: 32969
			// (set) Token: 0x0600AF09 RID: 44809 RVA: 0x000FCC91 File Offset: 0x000FAE91
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170080CA RID: 32970
			// (set) Token: 0x0600AF0A RID: 44810 RVA: 0x000FCCA4 File Offset: 0x000FAEA4
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170080CB RID: 32971
			// (set) Token: 0x0600AF0B RID: 44811 RVA: 0x000FCCB7 File Offset: 0x000FAEB7
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170080CC RID: 32972
			// (set) Token: 0x0600AF0C RID: 44812 RVA: 0x000FCCCF File Offset: 0x000FAECF
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170080CD RID: 32973
			// (set) Token: 0x0600AF0D RID: 44813 RVA: 0x000FCCE7 File Offset: 0x000FAEE7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170080CE RID: 32974
			// (set) Token: 0x0600AF0E RID: 44814 RVA: 0x000FCCFA File Offset: 0x000FAEFA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170080CF RID: 32975
			// (set) Token: 0x0600AF0F RID: 44815 RVA: 0x000FCD12 File Offset: 0x000FAF12
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170080D0 RID: 32976
			// (set) Token: 0x0600AF10 RID: 44816 RVA: 0x000FCD2A File Offset: 0x000FAF2A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170080D1 RID: 32977
			// (set) Token: 0x0600AF11 RID: 44817 RVA: 0x000FCD42 File Offset: 0x000FAF42
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170080D2 RID: 32978
			// (set) Token: 0x0600AF12 RID: 44818 RVA: 0x000FCD5A File Offset: 0x000FAF5A
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
