using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C65 RID: 3173
	public class SetLinkedUserCommand : SyntheticCommandWithPipelineInputNoOutput<LinkedUser>
	{
		// Token: 0x06009B96 RID: 39830 RVA: 0x000E1C7E File Offset: 0x000DFE7E
		private SetLinkedUserCommand() : base("Set-LinkedUser")
		{
		}

		// Token: 0x06009B97 RID: 39831 RVA: 0x000E1C8B File Offset: 0x000DFE8B
		public SetLinkedUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009B98 RID: 39832 RVA: 0x000E1C9A File Offset: 0x000DFE9A
		public virtual SetLinkedUserCommand SetParameters(SetLinkedUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009B99 RID: 39833 RVA: 0x000E1CA4 File Offset: 0x000DFEA4
		public virtual SetLinkedUserCommand SetParameters(SetLinkedUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C66 RID: 3174
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006E85 RID: 28293
			// (set) Token: 0x06009B9A RID: 39834 RVA: 0x000E1CAE File Offset: 0x000DFEAE
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006E86 RID: 28294
			// (set) Token: 0x06009B9B RID: 39835 RVA: 0x000E1CCC File Offset: 0x000DFECC
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17006E87 RID: 28295
			// (set) Token: 0x06009B9C RID: 39836 RVA: 0x000E1CDF File Offset: 0x000DFEDF
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17006E88 RID: 28296
			// (set) Token: 0x06009B9D RID: 39837 RVA: 0x000E1CF2 File Offset: 0x000DFEF2
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17006E89 RID: 28297
			// (set) Token: 0x06009B9E RID: 39838 RVA: 0x000E1D10 File Offset: 0x000DFF10
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17006E8A RID: 28298
			// (set) Token: 0x06009B9F RID: 39839 RVA: 0x000E1D28 File Offset: 0x000DFF28
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E8B RID: 28299
			// (set) Token: 0x06009BA0 RID: 39840 RVA: 0x000E1D40 File Offset: 0x000DFF40
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E8C RID: 28300
			// (set) Token: 0x06009BA1 RID: 39841 RVA: 0x000E1D53 File Offset: 0x000DFF53
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17006E8D RID: 28301
			// (set) Token: 0x06009BA2 RID: 39842 RVA: 0x000E1D66 File Offset: 0x000DFF66
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17006E8E RID: 28302
			// (set) Token: 0x06009BA3 RID: 39843 RVA: 0x000E1D79 File Offset: 0x000DFF79
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17006E8F RID: 28303
			// (set) Token: 0x06009BA4 RID: 39844 RVA: 0x000E1D91 File Offset: 0x000DFF91
			public virtual MultiValuedProperty<X509Identifier> CertificateSubject
			{
				set
				{
					base.PowerSharpParameters["CertificateSubject"] = value;
				}
			}

			// Token: 0x17006E90 RID: 28304
			// (set) Token: 0x06009BA5 RID: 39845 RVA: 0x000E1DA4 File Offset: 0x000DFFA4
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17006E91 RID: 28305
			// (set) Token: 0x06009BA6 RID: 39846 RVA: 0x000E1DBC File Offset: 0x000DFFBC
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17006E92 RID: 28306
			// (set) Token: 0x06009BA7 RID: 39847 RVA: 0x000E1DD4 File Offset: 0x000DFFD4
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17006E93 RID: 28307
			// (set) Token: 0x06009BA8 RID: 39848 RVA: 0x000E1DE7 File Offset: 0x000DFFE7
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17006E94 RID: 28308
			// (set) Token: 0x06009BA9 RID: 39849 RVA: 0x000E1DFF File Offset: 0x000DFFFF
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17006E95 RID: 28309
			// (set) Token: 0x06009BAA RID: 39850 RVA: 0x000E1E12 File Offset: 0x000E0012
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17006E96 RID: 28310
			// (set) Token: 0x06009BAB RID: 39851 RVA: 0x000E1E25 File Offset: 0x000E0025
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17006E97 RID: 28311
			// (set) Token: 0x06009BAC RID: 39852 RVA: 0x000E1E38 File Offset: 0x000E0038
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17006E98 RID: 28312
			// (set) Token: 0x06009BAD RID: 39853 RVA: 0x000E1E4B File Offset: 0x000E004B
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17006E99 RID: 28313
			// (set) Token: 0x06009BAE RID: 39854 RVA: 0x000E1E5E File Offset: 0x000E005E
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17006E9A RID: 28314
			// (set) Token: 0x06009BAF RID: 39855 RVA: 0x000E1E71 File Offset: 0x000E0071
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006E9B RID: 28315
			// (set) Token: 0x06009BB0 RID: 39856 RVA: 0x000E1E84 File Offset: 0x000E0084
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17006E9C RID: 28316
			// (set) Token: 0x06009BB1 RID: 39857 RVA: 0x000E1E97 File Offset: 0x000E0097
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17006E9D RID: 28317
			// (set) Token: 0x06009BB2 RID: 39858 RVA: 0x000E1EAA File Offset: 0x000E00AA
			public virtual GeoCoordinates GeoCoordinates
			{
				set
				{
					base.PowerSharpParameters["GeoCoordinates"] = value;
				}
			}

			// Token: 0x17006E9E RID: 28318
			// (set) Token: 0x06009BB3 RID: 39859 RVA: 0x000E1EBD File Offset: 0x000E00BD
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17006E9F RID: 28319
			// (set) Token: 0x06009BB4 RID: 39860 RVA: 0x000E1ED0 File Offset: 0x000E00D0
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17006EA0 RID: 28320
			// (set) Token: 0x06009BB5 RID: 39861 RVA: 0x000E1EE3 File Offset: 0x000E00E3
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006EA1 RID: 28321
			// (set) Token: 0x06009BB6 RID: 39862 RVA: 0x000E1EF6 File Offset: 0x000E00F6
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17006EA2 RID: 28322
			// (set) Token: 0x06009BB7 RID: 39863 RVA: 0x000E1F09 File Offset: 0x000E0109
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006EA3 RID: 28323
			// (set) Token: 0x06009BB8 RID: 39864 RVA: 0x000E1F1C File Offset: 0x000E011C
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17006EA4 RID: 28324
			// (set) Token: 0x06009BB9 RID: 39865 RVA: 0x000E1F2F File Offset: 0x000E012F
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17006EA5 RID: 28325
			// (set) Token: 0x06009BBA RID: 39866 RVA: 0x000E1F42 File Offset: 0x000E0142
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17006EA6 RID: 28326
			// (set) Token: 0x06009BBB RID: 39867 RVA: 0x000E1F55 File Offset: 0x000E0155
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17006EA7 RID: 28327
			// (set) Token: 0x06009BBC RID: 39868 RVA: 0x000E1F68 File Offset: 0x000E0168
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17006EA8 RID: 28328
			// (set) Token: 0x06009BBD RID: 39869 RVA: 0x000E1F7B File Offset: 0x000E017B
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17006EA9 RID: 28329
			// (set) Token: 0x06009BBE RID: 39870 RVA: 0x000E1F8E File Offset: 0x000E018E
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006EAA RID: 28330
			// (set) Token: 0x06009BBF RID: 39871 RVA: 0x000E1FA1 File Offset: 0x000E01A1
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17006EAB RID: 28331
			// (set) Token: 0x06009BC0 RID: 39872 RVA: 0x000E1FB4 File Offset: 0x000E01B4
			public virtual MultiValuedProperty<string> PostOfficeBox
			{
				set
				{
					base.PowerSharpParameters["PostOfficeBox"] = value;
				}
			}

			// Token: 0x17006EAC RID: 28332
			// (set) Token: 0x06009BC1 RID: 39873 RVA: 0x000E1FC7 File Offset: 0x000E01C7
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17006EAD RID: 28333
			// (set) Token: 0x06009BC2 RID: 39874 RVA: 0x000E1FDA File Offset: 0x000E01DA
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17006EAE RID: 28334
			// (set) Token: 0x06009BC3 RID: 39875 RVA: 0x000E1FED File Offset: 0x000E01ED
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17006EAF RID: 28335
			// (set) Token: 0x06009BC4 RID: 39876 RVA: 0x000E2000 File Offset: 0x000E0200
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17006EB0 RID: 28336
			// (set) Token: 0x06009BC5 RID: 39877 RVA: 0x000E2013 File Offset: 0x000E0213
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17006EB1 RID: 28337
			// (set) Token: 0x06009BC6 RID: 39878 RVA: 0x000E2026 File Offset: 0x000E0226
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x17006EB2 RID: 28338
			// (set) Token: 0x06009BC7 RID: 39879 RVA: 0x000E203E File Offset: 0x000E023E
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17006EB3 RID: 28339
			// (set) Token: 0x06009BC8 RID: 39880 RVA: 0x000E2051 File Offset: 0x000E0251
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17006EB4 RID: 28340
			// (set) Token: 0x06009BC9 RID: 39881 RVA: 0x000E2064 File Offset: 0x000E0264
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006EB5 RID: 28341
			// (set) Token: 0x06009BCA RID: 39882 RVA: 0x000E207C File Offset: 0x000E027C
			public virtual MultiValuedProperty<string> UMCallingLineIds
			{
				set
				{
					base.PowerSharpParameters["UMCallingLineIds"] = value;
				}
			}

			// Token: 0x17006EB6 RID: 28342
			// (set) Token: 0x06009BCB RID: 39883 RVA: 0x000E208F File Offset: 0x000E028F
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17006EB7 RID: 28343
			// (set) Token: 0x06009BCC RID: 39884 RVA: 0x000E20A7 File Offset: 0x000E02A7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006EB8 RID: 28344
			// (set) Token: 0x06009BCD RID: 39885 RVA: 0x000E20BA File Offset: 0x000E02BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006EB9 RID: 28345
			// (set) Token: 0x06009BCE RID: 39886 RVA: 0x000E20D2 File Offset: 0x000E02D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006EBA RID: 28346
			// (set) Token: 0x06009BCF RID: 39887 RVA: 0x000E20EA File Offset: 0x000E02EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006EBB RID: 28347
			// (set) Token: 0x06009BD0 RID: 39888 RVA: 0x000E2102 File Offset: 0x000E0302
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006EBC RID: 28348
			// (set) Token: 0x06009BD1 RID: 39889 RVA: 0x000E211A File Offset: 0x000E031A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C67 RID: 3175
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006EBD RID: 28349
			// (set) Token: 0x06009BD3 RID: 39891 RVA: 0x000E213A File Offset: 0x000E033A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006EBE RID: 28350
			// (set) Token: 0x06009BD4 RID: 39892 RVA: 0x000E2158 File Offset: 0x000E0358
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006EBF RID: 28351
			// (set) Token: 0x06009BD5 RID: 39893 RVA: 0x000E2176 File Offset: 0x000E0376
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17006EC0 RID: 28352
			// (set) Token: 0x06009BD6 RID: 39894 RVA: 0x000E2189 File Offset: 0x000E0389
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17006EC1 RID: 28353
			// (set) Token: 0x06009BD7 RID: 39895 RVA: 0x000E219C File Offset: 0x000E039C
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17006EC2 RID: 28354
			// (set) Token: 0x06009BD8 RID: 39896 RVA: 0x000E21BA File Offset: 0x000E03BA
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17006EC3 RID: 28355
			// (set) Token: 0x06009BD9 RID: 39897 RVA: 0x000E21D2 File Offset: 0x000E03D2
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006EC4 RID: 28356
			// (set) Token: 0x06009BDA RID: 39898 RVA: 0x000E21EA File Offset: 0x000E03EA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006EC5 RID: 28357
			// (set) Token: 0x06009BDB RID: 39899 RVA: 0x000E21FD File Offset: 0x000E03FD
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17006EC6 RID: 28358
			// (set) Token: 0x06009BDC RID: 39900 RVA: 0x000E2210 File Offset: 0x000E0410
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17006EC7 RID: 28359
			// (set) Token: 0x06009BDD RID: 39901 RVA: 0x000E2223 File Offset: 0x000E0423
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17006EC8 RID: 28360
			// (set) Token: 0x06009BDE RID: 39902 RVA: 0x000E223B File Offset: 0x000E043B
			public virtual MultiValuedProperty<X509Identifier> CertificateSubject
			{
				set
				{
					base.PowerSharpParameters["CertificateSubject"] = value;
				}
			}

			// Token: 0x17006EC9 RID: 28361
			// (set) Token: 0x06009BDF RID: 39903 RVA: 0x000E224E File Offset: 0x000E044E
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17006ECA RID: 28362
			// (set) Token: 0x06009BE0 RID: 39904 RVA: 0x000E2266 File Offset: 0x000E0466
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17006ECB RID: 28363
			// (set) Token: 0x06009BE1 RID: 39905 RVA: 0x000E227E File Offset: 0x000E047E
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17006ECC RID: 28364
			// (set) Token: 0x06009BE2 RID: 39906 RVA: 0x000E2291 File Offset: 0x000E0491
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17006ECD RID: 28365
			// (set) Token: 0x06009BE3 RID: 39907 RVA: 0x000E22A9 File Offset: 0x000E04A9
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17006ECE RID: 28366
			// (set) Token: 0x06009BE4 RID: 39908 RVA: 0x000E22BC File Offset: 0x000E04BC
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17006ECF RID: 28367
			// (set) Token: 0x06009BE5 RID: 39909 RVA: 0x000E22CF File Offset: 0x000E04CF
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17006ED0 RID: 28368
			// (set) Token: 0x06009BE6 RID: 39910 RVA: 0x000E22E2 File Offset: 0x000E04E2
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17006ED1 RID: 28369
			// (set) Token: 0x06009BE7 RID: 39911 RVA: 0x000E22F5 File Offset: 0x000E04F5
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17006ED2 RID: 28370
			// (set) Token: 0x06009BE8 RID: 39912 RVA: 0x000E2308 File Offset: 0x000E0508
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17006ED3 RID: 28371
			// (set) Token: 0x06009BE9 RID: 39913 RVA: 0x000E231B File Offset: 0x000E051B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006ED4 RID: 28372
			// (set) Token: 0x06009BEA RID: 39914 RVA: 0x000E232E File Offset: 0x000E052E
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17006ED5 RID: 28373
			// (set) Token: 0x06009BEB RID: 39915 RVA: 0x000E2341 File Offset: 0x000E0541
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17006ED6 RID: 28374
			// (set) Token: 0x06009BEC RID: 39916 RVA: 0x000E2354 File Offset: 0x000E0554
			public virtual GeoCoordinates GeoCoordinates
			{
				set
				{
					base.PowerSharpParameters["GeoCoordinates"] = value;
				}
			}

			// Token: 0x17006ED7 RID: 28375
			// (set) Token: 0x06009BED RID: 39917 RVA: 0x000E2367 File Offset: 0x000E0567
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17006ED8 RID: 28376
			// (set) Token: 0x06009BEE RID: 39918 RVA: 0x000E237A File Offset: 0x000E057A
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17006ED9 RID: 28377
			// (set) Token: 0x06009BEF RID: 39919 RVA: 0x000E238D File Offset: 0x000E058D
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006EDA RID: 28378
			// (set) Token: 0x06009BF0 RID: 39920 RVA: 0x000E23A0 File Offset: 0x000E05A0
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17006EDB RID: 28379
			// (set) Token: 0x06009BF1 RID: 39921 RVA: 0x000E23B3 File Offset: 0x000E05B3
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006EDC RID: 28380
			// (set) Token: 0x06009BF2 RID: 39922 RVA: 0x000E23C6 File Offset: 0x000E05C6
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17006EDD RID: 28381
			// (set) Token: 0x06009BF3 RID: 39923 RVA: 0x000E23D9 File Offset: 0x000E05D9
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17006EDE RID: 28382
			// (set) Token: 0x06009BF4 RID: 39924 RVA: 0x000E23EC File Offset: 0x000E05EC
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17006EDF RID: 28383
			// (set) Token: 0x06009BF5 RID: 39925 RVA: 0x000E23FF File Offset: 0x000E05FF
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17006EE0 RID: 28384
			// (set) Token: 0x06009BF6 RID: 39926 RVA: 0x000E2412 File Offset: 0x000E0612
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17006EE1 RID: 28385
			// (set) Token: 0x06009BF7 RID: 39927 RVA: 0x000E2425 File Offset: 0x000E0625
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17006EE2 RID: 28386
			// (set) Token: 0x06009BF8 RID: 39928 RVA: 0x000E2438 File Offset: 0x000E0638
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006EE3 RID: 28387
			// (set) Token: 0x06009BF9 RID: 39929 RVA: 0x000E244B File Offset: 0x000E064B
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17006EE4 RID: 28388
			// (set) Token: 0x06009BFA RID: 39930 RVA: 0x000E245E File Offset: 0x000E065E
			public virtual MultiValuedProperty<string> PostOfficeBox
			{
				set
				{
					base.PowerSharpParameters["PostOfficeBox"] = value;
				}
			}

			// Token: 0x17006EE5 RID: 28389
			// (set) Token: 0x06009BFB RID: 39931 RVA: 0x000E2471 File Offset: 0x000E0671
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17006EE6 RID: 28390
			// (set) Token: 0x06009BFC RID: 39932 RVA: 0x000E2484 File Offset: 0x000E0684
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17006EE7 RID: 28391
			// (set) Token: 0x06009BFD RID: 39933 RVA: 0x000E2497 File Offset: 0x000E0697
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17006EE8 RID: 28392
			// (set) Token: 0x06009BFE RID: 39934 RVA: 0x000E24AA File Offset: 0x000E06AA
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17006EE9 RID: 28393
			// (set) Token: 0x06009BFF RID: 39935 RVA: 0x000E24BD File Offset: 0x000E06BD
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17006EEA RID: 28394
			// (set) Token: 0x06009C00 RID: 39936 RVA: 0x000E24D0 File Offset: 0x000E06D0
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x17006EEB RID: 28395
			// (set) Token: 0x06009C01 RID: 39937 RVA: 0x000E24E8 File Offset: 0x000E06E8
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17006EEC RID: 28396
			// (set) Token: 0x06009C02 RID: 39938 RVA: 0x000E24FB File Offset: 0x000E06FB
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17006EED RID: 28397
			// (set) Token: 0x06009C03 RID: 39939 RVA: 0x000E250E File Offset: 0x000E070E
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006EEE RID: 28398
			// (set) Token: 0x06009C04 RID: 39940 RVA: 0x000E2526 File Offset: 0x000E0726
			public virtual MultiValuedProperty<string> UMCallingLineIds
			{
				set
				{
					base.PowerSharpParameters["UMCallingLineIds"] = value;
				}
			}

			// Token: 0x17006EEF RID: 28399
			// (set) Token: 0x06009C05 RID: 39941 RVA: 0x000E2539 File Offset: 0x000E0739
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17006EF0 RID: 28400
			// (set) Token: 0x06009C06 RID: 39942 RVA: 0x000E2551 File Offset: 0x000E0751
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006EF1 RID: 28401
			// (set) Token: 0x06009C07 RID: 39943 RVA: 0x000E2564 File Offset: 0x000E0764
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006EF2 RID: 28402
			// (set) Token: 0x06009C08 RID: 39944 RVA: 0x000E257C File Offset: 0x000E077C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006EF3 RID: 28403
			// (set) Token: 0x06009C09 RID: 39945 RVA: 0x000E2594 File Offset: 0x000E0794
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006EF4 RID: 28404
			// (set) Token: 0x06009C0A RID: 39946 RVA: 0x000E25AC File Offset: 0x000E07AC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006EF5 RID: 28405
			// (set) Token: 0x06009C0B RID: 39947 RVA: 0x000E25C4 File Offset: 0x000E07C4
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
