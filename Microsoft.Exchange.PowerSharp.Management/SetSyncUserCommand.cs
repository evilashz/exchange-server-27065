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
	// Token: 0x02000DE1 RID: 3553
	public class SetSyncUserCommand : SyntheticCommandWithPipelineInputNoOutput<SyncUser>
	{
		// Token: 0x0600D3A6 RID: 54182 RVA: 0x0012D0AB File Offset: 0x0012B2AB
		private SetSyncUserCommand() : base("Set-SyncUser")
		{
		}

		// Token: 0x0600D3A7 RID: 54183 RVA: 0x0012D0B8 File Offset: 0x0012B2B8
		public SetSyncUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D3A8 RID: 54184 RVA: 0x0012D0C7 File Offset: 0x0012B2C7
		public virtual SetSyncUserCommand SetParameters(SetSyncUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D3A9 RID: 54185 RVA: 0x0012D0D1 File Offset: 0x0012B2D1
		public virtual SetSyncUserCommand SetParameters(SetSyncUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DE2 RID: 3554
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A39D RID: 41885
			// (set) Token: 0x0600D3AA RID: 54186 RVA: 0x0012D0DB File Offset: 0x0012B2DB
			public virtual SwitchParameter SoftDeletedUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedUser"] = value;
				}
			}

			// Token: 0x1700A39E RID: 41886
			// (set) Token: 0x0600D3AB RID: 54187 RVA: 0x0012D0F3 File Offset: 0x0012B2F3
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700A39F RID: 41887
			// (set) Token: 0x0600D3AC RID: 54188 RVA: 0x0012D111 File Offset: 0x0012B311
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700A3A0 RID: 41888
			// (set) Token: 0x0600D3AD RID: 54189 RVA: 0x0012D129 File Offset: 0x0012B329
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A3A1 RID: 41889
			// (set) Token: 0x0600D3AE RID: 54190 RVA: 0x0012D141 File Offset: 0x0012B341
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A3A2 RID: 41890
			// (set) Token: 0x0600D3AF RID: 54191 RVA: 0x0012D154 File Offset: 0x0012B354
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A3A3 RID: 41891
			// (set) Token: 0x0600D3B0 RID: 54192 RVA: 0x0012D167 File Offset: 0x0012B367
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A3A4 RID: 41892
			// (set) Token: 0x0600D3B1 RID: 54193 RVA: 0x0012D17F File Offset: 0x0012B37F
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A3A5 RID: 41893
			// (set) Token: 0x0600D3B2 RID: 54194 RVA: 0x0012D197 File Offset: 0x0012B397
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A3A6 RID: 41894
			// (set) Token: 0x0600D3B3 RID: 54195 RVA: 0x0012D1AA File Offset: 0x0012B3AA
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700A3A7 RID: 41895
			// (set) Token: 0x0600D3B4 RID: 54196 RVA: 0x0012D1BD File Offset: 0x0012B3BD
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A3A8 RID: 41896
			// (set) Token: 0x0600D3B5 RID: 54197 RVA: 0x0012D1D5 File Offset: 0x0012B3D5
			public virtual bool AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A3A9 RID: 41897
			// (set) Token: 0x0600D3B6 RID: 54198 RVA: 0x0012D1ED File Offset: 0x0012B3ED
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A3AA RID: 41898
			// (set) Token: 0x0600D3B7 RID: 54199 RVA: 0x0012D205 File Offset: 0x0012B405
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A3AB RID: 41899
			// (set) Token: 0x0600D3B8 RID: 54200 RVA: 0x0012D218 File Offset: 0x0012B418
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700A3AC RID: 41900
			// (set) Token: 0x0600D3B9 RID: 54201 RVA: 0x0012D22B File Offset: 0x0012B42B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A3AD RID: 41901
			// (set) Token: 0x0600D3BA RID: 54202 RVA: 0x0012D243 File Offset: 0x0012B443
			public virtual MultiValuedProperty<X509Identifier> CertificateSubject
			{
				set
				{
					base.PowerSharpParameters["CertificateSubject"] = value;
				}
			}

			// Token: 0x1700A3AE RID: 41902
			// (set) Token: 0x0600D3BB RID: 54203 RVA: 0x0012D256 File Offset: 0x0012B456
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700A3AF RID: 41903
			// (set) Token: 0x0600D3BC RID: 54204 RVA: 0x0012D26E File Offset: 0x0012B46E
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A3B0 RID: 41904
			// (set) Token: 0x0600D3BD RID: 54205 RVA: 0x0012D286 File Offset: 0x0012B486
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700A3B1 RID: 41905
			// (set) Token: 0x0600D3BE RID: 54206 RVA: 0x0012D29E File Offset: 0x0012B49E
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700A3B2 RID: 41906
			// (set) Token: 0x0600D3BF RID: 54207 RVA: 0x0012D2B1 File Offset: 0x0012B4B1
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A3B3 RID: 41907
			// (set) Token: 0x0600D3C0 RID: 54208 RVA: 0x0012D2C9 File Offset: 0x0012B4C9
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x1700A3B4 RID: 41908
			// (set) Token: 0x0600D3C1 RID: 54209 RVA: 0x0012D2E1 File Offset: 0x0012B4E1
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x1700A3B5 RID: 41909
			// (set) Token: 0x0600D3C2 RID: 54210 RVA: 0x0012D2F9 File Offset: 0x0012B4F9
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A3B6 RID: 41910
			// (set) Token: 0x0600D3C3 RID: 54211 RVA: 0x0012D30C File Offset: 0x0012B50C
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700A3B7 RID: 41911
			// (set) Token: 0x0600D3C4 RID: 54212 RVA: 0x0012D31F File Offset: 0x0012B51F
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700A3B8 RID: 41912
			// (set) Token: 0x0600D3C5 RID: 54213 RVA: 0x0012D332 File Offset: 0x0012B532
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700A3B9 RID: 41913
			// (set) Token: 0x0600D3C6 RID: 54214 RVA: 0x0012D345 File Offset: 0x0012B545
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700A3BA RID: 41914
			// (set) Token: 0x0600D3C7 RID: 54215 RVA: 0x0012D358 File Offset: 0x0012B558
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700A3BB RID: 41915
			// (set) Token: 0x0600D3C8 RID: 54216 RVA: 0x0012D36B File Offset: 0x0012B56B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A3BC RID: 41916
			// (set) Token: 0x0600D3C9 RID: 54217 RVA: 0x0012D37E File Offset: 0x0012B57E
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700A3BD RID: 41917
			// (set) Token: 0x0600D3CA RID: 54218 RVA: 0x0012D391 File Offset: 0x0012B591
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A3BE RID: 41918
			// (set) Token: 0x0600D3CB RID: 54219 RVA: 0x0012D3A4 File Offset: 0x0012B5A4
			public virtual GeoCoordinates GeoCoordinates
			{
				set
				{
					base.PowerSharpParameters["GeoCoordinates"] = value;
				}
			}

			// Token: 0x1700A3BF RID: 41919
			// (set) Token: 0x0600D3CC RID: 54220 RVA: 0x0012D3B7 File Offset: 0x0012B5B7
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700A3C0 RID: 41920
			// (set) Token: 0x0600D3CD RID: 54221 RVA: 0x0012D3CA File Offset: 0x0012B5CA
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A3C1 RID: 41921
			// (set) Token: 0x0600D3CE RID: 54222 RVA: 0x0012D3DD File Offset: 0x0012B5DD
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A3C2 RID: 41922
			// (set) Token: 0x0600D3CF RID: 54223 RVA: 0x0012D3F0 File Offset: 0x0012B5F0
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700A3C3 RID: 41923
			// (set) Token: 0x0600D3D0 RID: 54224 RVA: 0x0012D403 File Offset: 0x0012B603
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A3C4 RID: 41924
			// (set) Token: 0x0600D3D1 RID: 54225 RVA: 0x0012D416 File Offset: 0x0012B616
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A3C5 RID: 41925
			// (set) Token: 0x0600D3D2 RID: 54226 RVA: 0x0012D429 File Offset: 0x0012B629
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A3C6 RID: 41926
			// (set) Token: 0x0600D3D3 RID: 54227 RVA: 0x0012D43C File Offset: 0x0012B63C
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A3C7 RID: 41927
			// (set) Token: 0x0600D3D4 RID: 54228 RVA: 0x0012D44F File Offset: 0x0012B64F
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A3C8 RID: 41928
			// (set) Token: 0x0600D3D5 RID: 54229 RVA: 0x0012D462 File Offset: 0x0012B662
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A3C9 RID: 41929
			// (set) Token: 0x0600D3D6 RID: 54230 RVA: 0x0012D475 File Offset: 0x0012B675
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A3CA RID: 41930
			// (set) Token: 0x0600D3D7 RID: 54231 RVA: 0x0012D488 File Offset: 0x0012B688
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A3CB RID: 41931
			// (set) Token: 0x0600D3D8 RID: 54232 RVA: 0x0012D49B File Offset: 0x0012B69B
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A3CC RID: 41932
			// (set) Token: 0x0600D3D9 RID: 54233 RVA: 0x0012D4AE File Offset: 0x0012B6AE
			public virtual MultiValuedProperty<string> PostOfficeBox
			{
				set
				{
					base.PowerSharpParameters["PostOfficeBox"] = value;
				}
			}

			// Token: 0x1700A3CD RID: 41933
			// (set) Token: 0x0600D3DA RID: 54234 RVA: 0x0012D4C1 File Offset: 0x0012B6C1
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700A3CE RID: 41934
			// (set) Token: 0x0600D3DB RID: 54235 RVA: 0x0012D4D4 File Offset: 0x0012B6D4
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A3CF RID: 41935
			// (set) Token: 0x0600D3DC RID: 54236 RVA: 0x0012D4E7 File Offset: 0x0012B6E7
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A3D0 RID: 41936
			// (set) Token: 0x0600D3DD RID: 54237 RVA: 0x0012D4FA File Offset: 0x0012B6FA
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A3D1 RID: 41937
			// (set) Token: 0x0600D3DE RID: 54238 RVA: 0x0012D50D File Offset: 0x0012B70D
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700A3D2 RID: 41938
			// (set) Token: 0x0600D3DF RID: 54239 RVA: 0x0012D520 File Offset: 0x0012B720
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700A3D3 RID: 41939
			// (set) Token: 0x0600D3E0 RID: 54240 RVA: 0x0012D538 File Offset: 0x0012B738
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A3D4 RID: 41940
			// (set) Token: 0x0600D3E1 RID: 54241 RVA: 0x0012D54B File Offset: 0x0012B74B
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A3D5 RID: 41941
			// (set) Token: 0x0600D3E2 RID: 54242 RVA: 0x0012D55E File Offset: 0x0012B75E
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700A3D6 RID: 41942
			// (set) Token: 0x0600D3E3 RID: 54243 RVA: 0x0012D576 File Offset: 0x0012B776
			public virtual MultiValuedProperty<string> UMCallingLineIds
			{
				set
				{
					base.PowerSharpParameters["UMCallingLineIds"] = value;
				}
			}

			// Token: 0x1700A3D7 RID: 41943
			// (set) Token: 0x0600D3E4 RID: 54244 RVA: 0x0012D589 File Offset: 0x0012B789
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A3D8 RID: 41944
			// (set) Token: 0x0600D3E5 RID: 54245 RVA: 0x0012D5A1 File Offset: 0x0012B7A1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A3D9 RID: 41945
			// (set) Token: 0x0600D3E6 RID: 54246 RVA: 0x0012D5B4 File Offset: 0x0012B7B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A3DA RID: 41946
			// (set) Token: 0x0600D3E7 RID: 54247 RVA: 0x0012D5CC File Offset: 0x0012B7CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A3DB RID: 41947
			// (set) Token: 0x0600D3E8 RID: 54248 RVA: 0x0012D5E4 File Offset: 0x0012B7E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A3DC RID: 41948
			// (set) Token: 0x0600D3E9 RID: 54249 RVA: 0x0012D5FC File Offset: 0x0012B7FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A3DD RID: 41949
			// (set) Token: 0x0600D3EA RID: 54250 RVA: 0x0012D614 File Offset: 0x0012B814
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DE3 RID: 3555
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A3DE RID: 41950
			// (set) Token: 0x0600D3EC RID: 54252 RVA: 0x0012D634 File Offset: 0x0012B834
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new NonMailEnabledUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A3DF RID: 41951
			// (set) Token: 0x0600D3ED RID: 54253 RVA: 0x0012D652 File Offset: 0x0012B852
			public virtual SwitchParameter SoftDeletedUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedUser"] = value;
				}
			}

			// Token: 0x1700A3E0 RID: 41952
			// (set) Token: 0x0600D3EE RID: 54254 RVA: 0x0012D66A File Offset: 0x0012B86A
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700A3E1 RID: 41953
			// (set) Token: 0x0600D3EF RID: 54255 RVA: 0x0012D688 File Offset: 0x0012B888
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700A3E2 RID: 41954
			// (set) Token: 0x0600D3F0 RID: 54256 RVA: 0x0012D6A0 File Offset: 0x0012B8A0
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A3E3 RID: 41955
			// (set) Token: 0x0600D3F1 RID: 54257 RVA: 0x0012D6B8 File Offset: 0x0012B8B8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A3E4 RID: 41956
			// (set) Token: 0x0600D3F2 RID: 54258 RVA: 0x0012D6CB File Offset: 0x0012B8CB
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A3E5 RID: 41957
			// (set) Token: 0x0600D3F3 RID: 54259 RVA: 0x0012D6DE File Offset: 0x0012B8DE
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A3E6 RID: 41958
			// (set) Token: 0x0600D3F4 RID: 54260 RVA: 0x0012D6F6 File Offset: 0x0012B8F6
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A3E7 RID: 41959
			// (set) Token: 0x0600D3F5 RID: 54261 RVA: 0x0012D70E File Offset: 0x0012B90E
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A3E8 RID: 41960
			// (set) Token: 0x0600D3F6 RID: 54262 RVA: 0x0012D721 File Offset: 0x0012B921
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700A3E9 RID: 41961
			// (set) Token: 0x0600D3F7 RID: 54263 RVA: 0x0012D734 File Offset: 0x0012B934
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A3EA RID: 41962
			// (set) Token: 0x0600D3F8 RID: 54264 RVA: 0x0012D74C File Offset: 0x0012B94C
			public virtual bool AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A3EB RID: 41963
			// (set) Token: 0x0600D3F9 RID: 54265 RVA: 0x0012D764 File Offset: 0x0012B964
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A3EC RID: 41964
			// (set) Token: 0x0600D3FA RID: 54266 RVA: 0x0012D77C File Offset: 0x0012B97C
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A3ED RID: 41965
			// (set) Token: 0x0600D3FB RID: 54267 RVA: 0x0012D78F File Offset: 0x0012B98F
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700A3EE RID: 41966
			// (set) Token: 0x0600D3FC RID: 54268 RVA: 0x0012D7A2 File Offset: 0x0012B9A2
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A3EF RID: 41967
			// (set) Token: 0x0600D3FD RID: 54269 RVA: 0x0012D7BA File Offset: 0x0012B9BA
			public virtual MultiValuedProperty<X509Identifier> CertificateSubject
			{
				set
				{
					base.PowerSharpParameters["CertificateSubject"] = value;
				}
			}

			// Token: 0x1700A3F0 RID: 41968
			// (set) Token: 0x0600D3FE RID: 54270 RVA: 0x0012D7CD File Offset: 0x0012B9CD
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700A3F1 RID: 41969
			// (set) Token: 0x0600D3FF RID: 54271 RVA: 0x0012D7E5 File Offset: 0x0012B9E5
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A3F2 RID: 41970
			// (set) Token: 0x0600D400 RID: 54272 RVA: 0x0012D7FD File Offset: 0x0012B9FD
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700A3F3 RID: 41971
			// (set) Token: 0x0600D401 RID: 54273 RVA: 0x0012D815 File Offset: 0x0012BA15
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700A3F4 RID: 41972
			// (set) Token: 0x0600D402 RID: 54274 RVA: 0x0012D828 File Offset: 0x0012BA28
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A3F5 RID: 41973
			// (set) Token: 0x0600D403 RID: 54275 RVA: 0x0012D840 File Offset: 0x0012BA40
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x1700A3F6 RID: 41974
			// (set) Token: 0x0600D404 RID: 54276 RVA: 0x0012D858 File Offset: 0x0012BA58
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x1700A3F7 RID: 41975
			// (set) Token: 0x0600D405 RID: 54277 RVA: 0x0012D870 File Offset: 0x0012BA70
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A3F8 RID: 41976
			// (set) Token: 0x0600D406 RID: 54278 RVA: 0x0012D883 File Offset: 0x0012BA83
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700A3F9 RID: 41977
			// (set) Token: 0x0600D407 RID: 54279 RVA: 0x0012D896 File Offset: 0x0012BA96
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700A3FA RID: 41978
			// (set) Token: 0x0600D408 RID: 54280 RVA: 0x0012D8A9 File Offset: 0x0012BAA9
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700A3FB RID: 41979
			// (set) Token: 0x0600D409 RID: 54281 RVA: 0x0012D8BC File Offset: 0x0012BABC
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700A3FC RID: 41980
			// (set) Token: 0x0600D40A RID: 54282 RVA: 0x0012D8CF File Offset: 0x0012BACF
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700A3FD RID: 41981
			// (set) Token: 0x0600D40B RID: 54283 RVA: 0x0012D8E2 File Offset: 0x0012BAE2
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A3FE RID: 41982
			// (set) Token: 0x0600D40C RID: 54284 RVA: 0x0012D8F5 File Offset: 0x0012BAF5
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700A3FF RID: 41983
			// (set) Token: 0x0600D40D RID: 54285 RVA: 0x0012D908 File Offset: 0x0012BB08
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A400 RID: 41984
			// (set) Token: 0x0600D40E RID: 54286 RVA: 0x0012D91B File Offset: 0x0012BB1B
			public virtual GeoCoordinates GeoCoordinates
			{
				set
				{
					base.PowerSharpParameters["GeoCoordinates"] = value;
				}
			}

			// Token: 0x1700A401 RID: 41985
			// (set) Token: 0x0600D40F RID: 54287 RVA: 0x0012D92E File Offset: 0x0012BB2E
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700A402 RID: 41986
			// (set) Token: 0x0600D410 RID: 54288 RVA: 0x0012D941 File Offset: 0x0012BB41
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A403 RID: 41987
			// (set) Token: 0x0600D411 RID: 54289 RVA: 0x0012D954 File Offset: 0x0012BB54
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A404 RID: 41988
			// (set) Token: 0x0600D412 RID: 54290 RVA: 0x0012D967 File Offset: 0x0012BB67
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700A405 RID: 41989
			// (set) Token: 0x0600D413 RID: 54291 RVA: 0x0012D97A File Offset: 0x0012BB7A
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A406 RID: 41990
			// (set) Token: 0x0600D414 RID: 54292 RVA: 0x0012D98D File Offset: 0x0012BB8D
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A407 RID: 41991
			// (set) Token: 0x0600D415 RID: 54293 RVA: 0x0012D9A0 File Offset: 0x0012BBA0
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A408 RID: 41992
			// (set) Token: 0x0600D416 RID: 54294 RVA: 0x0012D9B3 File Offset: 0x0012BBB3
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A409 RID: 41993
			// (set) Token: 0x0600D417 RID: 54295 RVA: 0x0012D9C6 File Offset: 0x0012BBC6
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A40A RID: 41994
			// (set) Token: 0x0600D418 RID: 54296 RVA: 0x0012D9D9 File Offset: 0x0012BBD9
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A40B RID: 41995
			// (set) Token: 0x0600D419 RID: 54297 RVA: 0x0012D9EC File Offset: 0x0012BBEC
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A40C RID: 41996
			// (set) Token: 0x0600D41A RID: 54298 RVA: 0x0012D9FF File Offset: 0x0012BBFF
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A40D RID: 41997
			// (set) Token: 0x0600D41B RID: 54299 RVA: 0x0012DA12 File Offset: 0x0012BC12
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A40E RID: 41998
			// (set) Token: 0x0600D41C RID: 54300 RVA: 0x0012DA25 File Offset: 0x0012BC25
			public virtual MultiValuedProperty<string> PostOfficeBox
			{
				set
				{
					base.PowerSharpParameters["PostOfficeBox"] = value;
				}
			}

			// Token: 0x1700A40F RID: 41999
			// (set) Token: 0x0600D41D RID: 54301 RVA: 0x0012DA38 File Offset: 0x0012BC38
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700A410 RID: 42000
			// (set) Token: 0x0600D41E RID: 54302 RVA: 0x0012DA4B File Offset: 0x0012BC4B
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A411 RID: 42001
			// (set) Token: 0x0600D41F RID: 54303 RVA: 0x0012DA5E File Offset: 0x0012BC5E
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A412 RID: 42002
			// (set) Token: 0x0600D420 RID: 54304 RVA: 0x0012DA71 File Offset: 0x0012BC71
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A413 RID: 42003
			// (set) Token: 0x0600D421 RID: 54305 RVA: 0x0012DA84 File Offset: 0x0012BC84
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700A414 RID: 42004
			// (set) Token: 0x0600D422 RID: 54306 RVA: 0x0012DA97 File Offset: 0x0012BC97
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700A415 RID: 42005
			// (set) Token: 0x0600D423 RID: 54307 RVA: 0x0012DAAF File Offset: 0x0012BCAF
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A416 RID: 42006
			// (set) Token: 0x0600D424 RID: 54308 RVA: 0x0012DAC2 File Offset: 0x0012BCC2
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A417 RID: 42007
			// (set) Token: 0x0600D425 RID: 54309 RVA: 0x0012DAD5 File Offset: 0x0012BCD5
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700A418 RID: 42008
			// (set) Token: 0x0600D426 RID: 54310 RVA: 0x0012DAED File Offset: 0x0012BCED
			public virtual MultiValuedProperty<string> UMCallingLineIds
			{
				set
				{
					base.PowerSharpParameters["UMCallingLineIds"] = value;
				}
			}

			// Token: 0x1700A419 RID: 42009
			// (set) Token: 0x0600D427 RID: 54311 RVA: 0x0012DB00 File Offset: 0x0012BD00
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A41A RID: 42010
			// (set) Token: 0x0600D428 RID: 54312 RVA: 0x0012DB18 File Offset: 0x0012BD18
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A41B RID: 42011
			// (set) Token: 0x0600D429 RID: 54313 RVA: 0x0012DB2B File Offset: 0x0012BD2B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A41C RID: 42012
			// (set) Token: 0x0600D42A RID: 54314 RVA: 0x0012DB43 File Offset: 0x0012BD43
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A41D RID: 42013
			// (set) Token: 0x0600D42B RID: 54315 RVA: 0x0012DB5B File Offset: 0x0012BD5B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A41E RID: 42014
			// (set) Token: 0x0600D42C RID: 54316 RVA: 0x0012DB73 File Offset: 0x0012BD73
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A41F RID: 42015
			// (set) Token: 0x0600D42D RID: 54317 RVA: 0x0012DB8B File Offset: 0x0012BD8B
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
