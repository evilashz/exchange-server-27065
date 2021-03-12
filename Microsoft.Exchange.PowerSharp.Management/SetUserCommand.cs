using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E2A RID: 3626
	public class SetUserCommand : SyntheticCommandWithPipelineInputNoOutput<User>
	{
		// Token: 0x0600D763 RID: 55139 RVA: 0x00131F6D File Offset: 0x0013016D
		private SetUserCommand() : base("Set-User")
		{
		}

		// Token: 0x0600D764 RID: 55140 RVA: 0x00131F7A File Offset: 0x0013017A
		public SetUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D765 RID: 55141 RVA: 0x00131F89 File Offset: 0x00130189
		public virtual SetUserCommand SetParameters(SetUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D766 RID: 55142 RVA: 0x00131F93 File Offset: 0x00130193
		public virtual SetUserCommand SetParameters(SetUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E2B RID: 3627
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A6C8 RID: 42696
			// (set) Token: 0x0600D767 RID: 55143 RVA: 0x00131F9D File Offset: 0x0013019D
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700A6C9 RID: 42697
			// (set) Token: 0x0600D768 RID: 55144 RVA: 0x00131FB5 File Offset: 0x001301B5
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700A6CA RID: 42698
			// (set) Token: 0x0600D769 RID: 55145 RVA: 0x00131FCD File Offset: 0x001301CD
			public virtual SwitchParameter EnableAccount
			{
				set
				{
					base.PowerSharpParameters["EnableAccount"] = value;
				}
			}

			// Token: 0x1700A6CB RID: 42699
			// (set) Token: 0x0600D76A RID: 55146 RVA: 0x00131FE5 File Offset: 0x001301E5
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A6CC RID: 42700
			// (set) Token: 0x0600D76B RID: 55147 RVA: 0x00132003 File Offset: 0x00130203
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x1700A6CD RID: 42701
			// (set) Token: 0x0600D76C RID: 55148 RVA: 0x00132016 File Offset: 0x00130216
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x1700A6CE RID: 42702
			// (set) Token: 0x0600D76D RID: 55149 RVA: 0x00132029 File Offset: 0x00130229
			public virtual NetID BusinessNetID
			{
				set
				{
					base.PowerSharpParameters["BusinessNetID"] = value;
				}
			}

			// Token: 0x1700A6CF RID: 42703
			// (set) Token: 0x0600D76E RID: 55150 RVA: 0x0013203C File Offset: 0x0013023C
			public virtual SwitchParameter CopyShadowAttributes
			{
				set
				{
					base.PowerSharpParameters["CopyShadowAttributes"] = value;
				}
			}

			// Token: 0x1700A6D0 RID: 42704
			// (set) Token: 0x0600D76F RID: 55151 RVA: 0x00132054 File Offset: 0x00130254
			public virtual SwitchParameter GenerateExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["GenerateExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700A6D1 RID: 42705
			// (set) Token: 0x0600D770 RID: 55152 RVA: 0x0013206C File Offset: 0x0013026C
			public virtual bool LEOEnabled
			{
				set
				{
					base.PowerSharpParameters["LEOEnabled"] = value;
				}
			}

			// Token: 0x1700A6D2 RID: 42706
			// (set) Token: 0x0600D771 RID: 55153 RVA: 0x00132084 File Offset: 0x00130284
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x1700A6D3 RID: 42707
			// (set) Token: 0x0600D772 RID: 55154 RVA: 0x00132097 File Offset: 0x00130297
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x1700A6D4 RID: 42708
			// (set) Token: 0x0600D773 RID: 55155 RVA: 0x001320AA File Offset: 0x001302AA
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x1700A6D5 RID: 42709
			// (set) Token: 0x0600D774 RID: 55156 RVA: 0x001320C2 File Offset: 0x001302C2
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x1700A6D6 RID: 42710
			// (set) Token: 0x0600D775 RID: 55157 RVA: 0x001320DA File Offset: 0x001302DA
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x1700A6D7 RID: 42711
			// (set) Token: 0x0600D776 RID: 55158 RVA: 0x001320F2 File Offset: 0x001302F2
			public virtual MailboxRelease ArchiveRelease
			{
				set
				{
					base.PowerSharpParameters["ArchiveRelease"] = value;
				}
			}

			// Token: 0x1700A6D8 RID: 42712
			// (set) Token: 0x0600D777 RID: 55159 RVA: 0x0013210A File Offset: 0x0013030A
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700A6D9 RID: 42713
			// (set) Token: 0x0600D778 RID: 55160 RVA: 0x00132128 File Offset: 0x00130328
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700A6DA RID: 42714
			// (set) Token: 0x0600D779 RID: 55161 RVA: 0x00132140 File Offset: 0x00130340
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A6DB RID: 42715
			// (set) Token: 0x0600D77A RID: 55162 RVA: 0x00132158 File Offset: 0x00130358
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A6DC RID: 42716
			// (set) Token: 0x0600D77B RID: 55163 RVA: 0x0013216B File Offset: 0x0013036B
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A6DD RID: 42717
			// (set) Token: 0x0600D77C RID: 55164 RVA: 0x0013217E File Offset: 0x0013037E
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700A6DE RID: 42718
			// (set) Token: 0x0600D77D RID: 55165 RVA: 0x00132191 File Offset: 0x00130391
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A6DF RID: 42719
			// (set) Token: 0x0600D77E RID: 55166 RVA: 0x001321A9 File Offset: 0x001303A9
			public virtual MultiValuedProperty<X509Identifier> CertificateSubject
			{
				set
				{
					base.PowerSharpParameters["CertificateSubject"] = value;
				}
			}

			// Token: 0x1700A6E0 RID: 42720
			// (set) Token: 0x0600D77F RID: 55167 RVA: 0x001321BC File Offset: 0x001303BC
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700A6E1 RID: 42721
			// (set) Token: 0x0600D780 RID: 55168 RVA: 0x001321D4 File Offset: 0x001303D4
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A6E2 RID: 42722
			// (set) Token: 0x0600D781 RID: 55169 RVA: 0x001321EC File Offset: 0x001303EC
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700A6E3 RID: 42723
			// (set) Token: 0x0600D782 RID: 55170 RVA: 0x00132204 File Offset: 0x00130404
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700A6E4 RID: 42724
			// (set) Token: 0x0600D783 RID: 55171 RVA: 0x00132217 File Offset: 0x00130417
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A6E5 RID: 42725
			// (set) Token: 0x0600D784 RID: 55172 RVA: 0x0013222F File Offset: 0x0013042F
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x1700A6E6 RID: 42726
			// (set) Token: 0x0600D785 RID: 55173 RVA: 0x00132247 File Offset: 0x00130447
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x1700A6E7 RID: 42727
			// (set) Token: 0x0600D786 RID: 55174 RVA: 0x0013225F File Offset: 0x0013045F
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A6E8 RID: 42728
			// (set) Token: 0x0600D787 RID: 55175 RVA: 0x00132272 File Offset: 0x00130472
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700A6E9 RID: 42729
			// (set) Token: 0x0600D788 RID: 55176 RVA: 0x00132285 File Offset: 0x00130485
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700A6EA RID: 42730
			// (set) Token: 0x0600D789 RID: 55177 RVA: 0x00132298 File Offset: 0x00130498
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700A6EB RID: 42731
			// (set) Token: 0x0600D78A RID: 55178 RVA: 0x001322AB File Offset: 0x001304AB
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700A6EC RID: 42732
			// (set) Token: 0x0600D78B RID: 55179 RVA: 0x001322BE File Offset: 0x001304BE
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700A6ED RID: 42733
			// (set) Token: 0x0600D78C RID: 55180 RVA: 0x001322D1 File Offset: 0x001304D1
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A6EE RID: 42734
			// (set) Token: 0x0600D78D RID: 55181 RVA: 0x001322E4 File Offset: 0x001304E4
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700A6EF RID: 42735
			// (set) Token: 0x0600D78E RID: 55182 RVA: 0x001322F7 File Offset: 0x001304F7
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A6F0 RID: 42736
			// (set) Token: 0x0600D78F RID: 55183 RVA: 0x0013230A File Offset: 0x0013050A
			public virtual GeoCoordinates GeoCoordinates
			{
				set
				{
					base.PowerSharpParameters["GeoCoordinates"] = value;
				}
			}

			// Token: 0x1700A6F1 RID: 42737
			// (set) Token: 0x0600D790 RID: 55184 RVA: 0x0013231D File Offset: 0x0013051D
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700A6F2 RID: 42738
			// (set) Token: 0x0600D791 RID: 55185 RVA: 0x00132330 File Offset: 0x00130530
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A6F3 RID: 42739
			// (set) Token: 0x0600D792 RID: 55186 RVA: 0x00132343 File Offset: 0x00130543
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A6F4 RID: 42740
			// (set) Token: 0x0600D793 RID: 55187 RVA: 0x00132356 File Offset: 0x00130556
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700A6F5 RID: 42741
			// (set) Token: 0x0600D794 RID: 55188 RVA: 0x00132369 File Offset: 0x00130569
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A6F6 RID: 42742
			// (set) Token: 0x0600D795 RID: 55189 RVA: 0x0013237C File Offset: 0x0013057C
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A6F7 RID: 42743
			// (set) Token: 0x0600D796 RID: 55190 RVA: 0x0013238F File Offset: 0x0013058F
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A6F8 RID: 42744
			// (set) Token: 0x0600D797 RID: 55191 RVA: 0x001323A2 File Offset: 0x001305A2
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A6F9 RID: 42745
			// (set) Token: 0x0600D798 RID: 55192 RVA: 0x001323B5 File Offset: 0x001305B5
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A6FA RID: 42746
			// (set) Token: 0x0600D799 RID: 55193 RVA: 0x001323C8 File Offset: 0x001305C8
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A6FB RID: 42747
			// (set) Token: 0x0600D79A RID: 55194 RVA: 0x001323DB File Offset: 0x001305DB
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A6FC RID: 42748
			// (set) Token: 0x0600D79B RID: 55195 RVA: 0x001323EE File Offset: 0x001305EE
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A6FD RID: 42749
			// (set) Token: 0x0600D79C RID: 55196 RVA: 0x00132401 File Offset: 0x00130601
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A6FE RID: 42750
			// (set) Token: 0x0600D79D RID: 55197 RVA: 0x00132414 File Offset: 0x00130614
			public virtual MultiValuedProperty<string> PostOfficeBox
			{
				set
				{
					base.PowerSharpParameters["PostOfficeBox"] = value;
				}
			}

			// Token: 0x1700A6FF RID: 42751
			// (set) Token: 0x0600D79E RID: 55198 RVA: 0x00132427 File Offset: 0x00130627
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700A700 RID: 42752
			// (set) Token: 0x0600D79F RID: 55199 RVA: 0x0013243A File Offset: 0x0013063A
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A701 RID: 42753
			// (set) Token: 0x0600D7A0 RID: 55200 RVA: 0x0013244D File Offset: 0x0013064D
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A702 RID: 42754
			// (set) Token: 0x0600D7A1 RID: 55201 RVA: 0x00132460 File Offset: 0x00130660
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A703 RID: 42755
			// (set) Token: 0x0600D7A2 RID: 55202 RVA: 0x00132473 File Offset: 0x00130673
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700A704 RID: 42756
			// (set) Token: 0x0600D7A3 RID: 55203 RVA: 0x00132486 File Offset: 0x00130686
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700A705 RID: 42757
			// (set) Token: 0x0600D7A4 RID: 55204 RVA: 0x0013249E File Offset: 0x0013069E
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A706 RID: 42758
			// (set) Token: 0x0600D7A5 RID: 55205 RVA: 0x001324B1 File Offset: 0x001306B1
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A707 RID: 42759
			// (set) Token: 0x0600D7A6 RID: 55206 RVA: 0x001324C4 File Offset: 0x001306C4
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700A708 RID: 42760
			// (set) Token: 0x0600D7A7 RID: 55207 RVA: 0x001324DC File Offset: 0x001306DC
			public virtual MultiValuedProperty<string> UMCallingLineIds
			{
				set
				{
					base.PowerSharpParameters["UMCallingLineIds"] = value;
				}
			}

			// Token: 0x1700A709 RID: 42761
			// (set) Token: 0x0600D7A8 RID: 55208 RVA: 0x001324EF File Offset: 0x001306EF
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A70A RID: 42762
			// (set) Token: 0x0600D7A9 RID: 55209 RVA: 0x00132507 File Offset: 0x00130707
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A70B RID: 42763
			// (set) Token: 0x0600D7AA RID: 55210 RVA: 0x0013251A File Offset: 0x0013071A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A70C RID: 42764
			// (set) Token: 0x0600D7AB RID: 55211 RVA: 0x00132532 File Offset: 0x00130732
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A70D RID: 42765
			// (set) Token: 0x0600D7AC RID: 55212 RVA: 0x0013254A File Offset: 0x0013074A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A70E RID: 42766
			// (set) Token: 0x0600D7AD RID: 55213 RVA: 0x00132562 File Offset: 0x00130762
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A70F RID: 42767
			// (set) Token: 0x0600D7AE RID: 55214 RVA: 0x0013257A File Offset: 0x0013077A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E2C RID: 3628
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A710 RID: 42768
			// (set) Token: 0x0600D7B0 RID: 55216 RVA: 0x0013259A File Offset: 0x0013079A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A711 RID: 42769
			// (set) Token: 0x0600D7B1 RID: 55217 RVA: 0x001325B8 File Offset: 0x001307B8
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700A712 RID: 42770
			// (set) Token: 0x0600D7B2 RID: 55218 RVA: 0x001325D0 File Offset: 0x001307D0
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700A713 RID: 42771
			// (set) Token: 0x0600D7B3 RID: 55219 RVA: 0x001325E8 File Offset: 0x001307E8
			public virtual SwitchParameter EnableAccount
			{
				set
				{
					base.PowerSharpParameters["EnableAccount"] = value;
				}
			}

			// Token: 0x1700A714 RID: 42772
			// (set) Token: 0x0600D7B4 RID: 55220 RVA: 0x00132600 File Offset: 0x00130800
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A715 RID: 42773
			// (set) Token: 0x0600D7B5 RID: 55221 RVA: 0x0013261E File Offset: 0x0013081E
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x1700A716 RID: 42774
			// (set) Token: 0x0600D7B6 RID: 55222 RVA: 0x00132631 File Offset: 0x00130831
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x1700A717 RID: 42775
			// (set) Token: 0x0600D7B7 RID: 55223 RVA: 0x00132644 File Offset: 0x00130844
			public virtual NetID BusinessNetID
			{
				set
				{
					base.PowerSharpParameters["BusinessNetID"] = value;
				}
			}

			// Token: 0x1700A718 RID: 42776
			// (set) Token: 0x0600D7B8 RID: 55224 RVA: 0x00132657 File Offset: 0x00130857
			public virtual SwitchParameter CopyShadowAttributes
			{
				set
				{
					base.PowerSharpParameters["CopyShadowAttributes"] = value;
				}
			}

			// Token: 0x1700A719 RID: 42777
			// (set) Token: 0x0600D7B9 RID: 55225 RVA: 0x0013266F File Offset: 0x0013086F
			public virtual SwitchParameter GenerateExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["GenerateExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700A71A RID: 42778
			// (set) Token: 0x0600D7BA RID: 55226 RVA: 0x00132687 File Offset: 0x00130887
			public virtual bool LEOEnabled
			{
				set
				{
					base.PowerSharpParameters["LEOEnabled"] = value;
				}
			}

			// Token: 0x1700A71B RID: 42779
			// (set) Token: 0x0600D7BB RID: 55227 RVA: 0x0013269F File Offset: 0x0013089F
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x1700A71C RID: 42780
			// (set) Token: 0x0600D7BC RID: 55228 RVA: 0x001326B2 File Offset: 0x001308B2
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x1700A71D RID: 42781
			// (set) Token: 0x0600D7BD RID: 55229 RVA: 0x001326C5 File Offset: 0x001308C5
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x1700A71E RID: 42782
			// (set) Token: 0x0600D7BE RID: 55230 RVA: 0x001326DD File Offset: 0x001308DD
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x1700A71F RID: 42783
			// (set) Token: 0x0600D7BF RID: 55231 RVA: 0x001326F5 File Offset: 0x001308F5
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x1700A720 RID: 42784
			// (set) Token: 0x0600D7C0 RID: 55232 RVA: 0x0013270D File Offset: 0x0013090D
			public virtual MailboxRelease ArchiveRelease
			{
				set
				{
					base.PowerSharpParameters["ArchiveRelease"] = value;
				}
			}

			// Token: 0x1700A721 RID: 42785
			// (set) Token: 0x0600D7C1 RID: 55233 RVA: 0x00132725 File Offset: 0x00130925
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700A722 RID: 42786
			// (set) Token: 0x0600D7C2 RID: 55234 RVA: 0x00132743 File Offset: 0x00130943
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700A723 RID: 42787
			// (set) Token: 0x0600D7C3 RID: 55235 RVA: 0x0013275B File Offset: 0x0013095B
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A724 RID: 42788
			// (set) Token: 0x0600D7C4 RID: 55236 RVA: 0x00132773 File Offset: 0x00130973
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A725 RID: 42789
			// (set) Token: 0x0600D7C5 RID: 55237 RVA: 0x00132786 File Offset: 0x00130986
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A726 RID: 42790
			// (set) Token: 0x0600D7C6 RID: 55238 RVA: 0x00132799 File Offset: 0x00130999
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700A727 RID: 42791
			// (set) Token: 0x0600D7C7 RID: 55239 RVA: 0x001327AC File Offset: 0x001309AC
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A728 RID: 42792
			// (set) Token: 0x0600D7C8 RID: 55240 RVA: 0x001327C4 File Offset: 0x001309C4
			public virtual MultiValuedProperty<X509Identifier> CertificateSubject
			{
				set
				{
					base.PowerSharpParameters["CertificateSubject"] = value;
				}
			}

			// Token: 0x1700A729 RID: 42793
			// (set) Token: 0x0600D7C9 RID: 55241 RVA: 0x001327D7 File Offset: 0x001309D7
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700A72A RID: 42794
			// (set) Token: 0x0600D7CA RID: 55242 RVA: 0x001327EF File Offset: 0x001309EF
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A72B RID: 42795
			// (set) Token: 0x0600D7CB RID: 55243 RVA: 0x00132807 File Offset: 0x00130A07
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700A72C RID: 42796
			// (set) Token: 0x0600D7CC RID: 55244 RVA: 0x0013281F File Offset: 0x00130A1F
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700A72D RID: 42797
			// (set) Token: 0x0600D7CD RID: 55245 RVA: 0x00132832 File Offset: 0x00130A32
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A72E RID: 42798
			// (set) Token: 0x0600D7CE RID: 55246 RVA: 0x0013284A File Offset: 0x00130A4A
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x1700A72F RID: 42799
			// (set) Token: 0x0600D7CF RID: 55247 RVA: 0x00132862 File Offset: 0x00130A62
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x1700A730 RID: 42800
			// (set) Token: 0x0600D7D0 RID: 55248 RVA: 0x0013287A File Offset: 0x00130A7A
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A731 RID: 42801
			// (set) Token: 0x0600D7D1 RID: 55249 RVA: 0x0013288D File Offset: 0x00130A8D
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700A732 RID: 42802
			// (set) Token: 0x0600D7D2 RID: 55250 RVA: 0x001328A0 File Offset: 0x00130AA0
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700A733 RID: 42803
			// (set) Token: 0x0600D7D3 RID: 55251 RVA: 0x001328B3 File Offset: 0x00130AB3
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700A734 RID: 42804
			// (set) Token: 0x0600D7D4 RID: 55252 RVA: 0x001328C6 File Offset: 0x00130AC6
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700A735 RID: 42805
			// (set) Token: 0x0600D7D5 RID: 55253 RVA: 0x001328D9 File Offset: 0x00130AD9
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700A736 RID: 42806
			// (set) Token: 0x0600D7D6 RID: 55254 RVA: 0x001328EC File Offset: 0x00130AEC
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A737 RID: 42807
			// (set) Token: 0x0600D7D7 RID: 55255 RVA: 0x001328FF File Offset: 0x00130AFF
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700A738 RID: 42808
			// (set) Token: 0x0600D7D8 RID: 55256 RVA: 0x00132912 File Offset: 0x00130B12
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A739 RID: 42809
			// (set) Token: 0x0600D7D9 RID: 55257 RVA: 0x00132925 File Offset: 0x00130B25
			public virtual GeoCoordinates GeoCoordinates
			{
				set
				{
					base.PowerSharpParameters["GeoCoordinates"] = value;
				}
			}

			// Token: 0x1700A73A RID: 42810
			// (set) Token: 0x0600D7DA RID: 55258 RVA: 0x00132938 File Offset: 0x00130B38
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700A73B RID: 42811
			// (set) Token: 0x0600D7DB RID: 55259 RVA: 0x0013294B File Offset: 0x00130B4B
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A73C RID: 42812
			// (set) Token: 0x0600D7DC RID: 55260 RVA: 0x0013295E File Offset: 0x00130B5E
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A73D RID: 42813
			// (set) Token: 0x0600D7DD RID: 55261 RVA: 0x00132971 File Offset: 0x00130B71
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700A73E RID: 42814
			// (set) Token: 0x0600D7DE RID: 55262 RVA: 0x00132984 File Offset: 0x00130B84
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A73F RID: 42815
			// (set) Token: 0x0600D7DF RID: 55263 RVA: 0x00132997 File Offset: 0x00130B97
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A740 RID: 42816
			// (set) Token: 0x0600D7E0 RID: 55264 RVA: 0x001329AA File Offset: 0x00130BAA
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A741 RID: 42817
			// (set) Token: 0x0600D7E1 RID: 55265 RVA: 0x001329BD File Offset: 0x00130BBD
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A742 RID: 42818
			// (set) Token: 0x0600D7E2 RID: 55266 RVA: 0x001329D0 File Offset: 0x00130BD0
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A743 RID: 42819
			// (set) Token: 0x0600D7E3 RID: 55267 RVA: 0x001329E3 File Offset: 0x00130BE3
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A744 RID: 42820
			// (set) Token: 0x0600D7E4 RID: 55268 RVA: 0x001329F6 File Offset: 0x00130BF6
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A745 RID: 42821
			// (set) Token: 0x0600D7E5 RID: 55269 RVA: 0x00132A09 File Offset: 0x00130C09
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A746 RID: 42822
			// (set) Token: 0x0600D7E6 RID: 55270 RVA: 0x00132A1C File Offset: 0x00130C1C
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A747 RID: 42823
			// (set) Token: 0x0600D7E7 RID: 55271 RVA: 0x00132A2F File Offset: 0x00130C2F
			public virtual MultiValuedProperty<string> PostOfficeBox
			{
				set
				{
					base.PowerSharpParameters["PostOfficeBox"] = value;
				}
			}

			// Token: 0x1700A748 RID: 42824
			// (set) Token: 0x0600D7E8 RID: 55272 RVA: 0x00132A42 File Offset: 0x00130C42
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700A749 RID: 42825
			// (set) Token: 0x0600D7E9 RID: 55273 RVA: 0x00132A55 File Offset: 0x00130C55
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A74A RID: 42826
			// (set) Token: 0x0600D7EA RID: 55274 RVA: 0x00132A68 File Offset: 0x00130C68
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A74B RID: 42827
			// (set) Token: 0x0600D7EB RID: 55275 RVA: 0x00132A7B File Offset: 0x00130C7B
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A74C RID: 42828
			// (set) Token: 0x0600D7EC RID: 55276 RVA: 0x00132A8E File Offset: 0x00130C8E
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700A74D RID: 42829
			// (set) Token: 0x0600D7ED RID: 55277 RVA: 0x00132AA1 File Offset: 0x00130CA1
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700A74E RID: 42830
			// (set) Token: 0x0600D7EE RID: 55278 RVA: 0x00132AB9 File Offset: 0x00130CB9
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A74F RID: 42831
			// (set) Token: 0x0600D7EF RID: 55279 RVA: 0x00132ACC File Offset: 0x00130CCC
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A750 RID: 42832
			// (set) Token: 0x0600D7F0 RID: 55280 RVA: 0x00132ADF File Offset: 0x00130CDF
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700A751 RID: 42833
			// (set) Token: 0x0600D7F1 RID: 55281 RVA: 0x00132AF7 File Offset: 0x00130CF7
			public virtual MultiValuedProperty<string> UMCallingLineIds
			{
				set
				{
					base.PowerSharpParameters["UMCallingLineIds"] = value;
				}
			}

			// Token: 0x1700A752 RID: 42834
			// (set) Token: 0x0600D7F2 RID: 55282 RVA: 0x00132B0A File Offset: 0x00130D0A
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A753 RID: 42835
			// (set) Token: 0x0600D7F3 RID: 55283 RVA: 0x00132B22 File Offset: 0x00130D22
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A754 RID: 42836
			// (set) Token: 0x0600D7F4 RID: 55284 RVA: 0x00132B35 File Offset: 0x00130D35
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A755 RID: 42837
			// (set) Token: 0x0600D7F5 RID: 55285 RVA: 0x00132B4D File Offset: 0x00130D4D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A756 RID: 42838
			// (set) Token: 0x0600D7F6 RID: 55286 RVA: 0x00132B65 File Offset: 0x00130D65
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A757 RID: 42839
			// (set) Token: 0x0600D7F7 RID: 55287 RVA: 0x00132B7D File Offset: 0x00130D7D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A758 RID: 42840
			// (set) Token: 0x0600D7F8 RID: 55288 RVA: 0x00132B95 File Offset: 0x00130D95
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
