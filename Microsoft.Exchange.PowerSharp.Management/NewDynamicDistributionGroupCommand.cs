using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C0B RID: 3083
	public class NewDynamicDistributionGroupCommand : SyntheticCommandWithPipelineInput<ADDynamicGroup, ADDynamicGroup>
	{
		// Token: 0x060095A7 RID: 38311 RVA: 0x000D9FCB File Offset: 0x000D81CB
		private NewDynamicDistributionGroupCommand() : base("New-DynamicDistributionGroup")
		{
		}

		// Token: 0x060095A8 RID: 38312 RVA: 0x000D9FD8 File Offset: 0x000D81D8
		public NewDynamicDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060095A9 RID: 38313 RVA: 0x000D9FE7 File Offset: 0x000D81E7
		public virtual NewDynamicDistributionGroupCommand SetParameters(NewDynamicDistributionGroupCommand.CustomFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060095AA RID: 38314 RVA: 0x000D9FF1 File Offset: 0x000D81F1
		public virtual NewDynamicDistributionGroupCommand SetParameters(NewDynamicDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060095AB RID: 38315 RVA: 0x000D9FFB File Offset: 0x000D81FB
		public virtual NewDynamicDistributionGroupCommand SetParameters(NewDynamicDistributionGroupCommand.PrecannedFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C0C RID: 3084
		public class CustomFilterParameters : ParametersBase
		{
			// Token: 0x1700694A RID: 26954
			// (set) Token: 0x060095AC RID: 38316 RVA: 0x000DA005 File Offset: 0x000D8205
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x1700694B RID: 26955
			// (set) Token: 0x060095AD RID: 38317 RVA: 0x000DA018 File Offset: 0x000D8218
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700694C RID: 26956
			// (set) Token: 0x060095AE RID: 38318 RVA: 0x000DA036 File Offset: 0x000D8236
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700694D RID: 26957
			// (set) Token: 0x060095AF RID: 38319 RVA: 0x000DA054 File Offset: 0x000D8254
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700694E RID: 26958
			// (set) Token: 0x060095B0 RID: 38320 RVA: 0x000DA067 File Offset: 0x000D8267
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700694F RID: 26959
			// (set) Token: 0x060095B1 RID: 38321 RVA: 0x000DA07F File Offset: 0x000D827F
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17006950 RID: 26960
			// (set) Token: 0x060095B2 RID: 38322 RVA: 0x000DA097 File Offset: 0x000D8297
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006951 RID: 26961
			// (set) Token: 0x060095B3 RID: 38323 RVA: 0x000DA0AF File Offset: 0x000D82AF
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006952 RID: 26962
			// (set) Token: 0x060095B4 RID: 38324 RVA: 0x000DA0C2 File Offset: 0x000D82C2
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006953 RID: 26963
			// (set) Token: 0x060095B5 RID: 38325 RVA: 0x000DA0DA File Offset: 0x000D82DA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006954 RID: 26964
			// (set) Token: 0x060095B6 RID: 38326 RVA: 0x000DA0ED File Offset: 0x000D82ED
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006955 RID: 26965
			// (set) Token: 0x060095B7 RID: 38327 RVA: 0x000DA100 File Offset: 0x000D8300
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006956 RID: 26966
			// (set) Token: 0x060095B8 RID: 38328 RVA: 0x000DA11E File Offset: 0x000D831E
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006957 RID: 26967
			// (set) Token: 0x060095B9 RID: 38329 RVA: 0x000DA131 File Offset: 0x000D8331
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006958 RID: 26968
			// (set) Token: 0x060095BA RID: 38330 RVA: 0x000DA14F File Offset: 0x000D834F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006959 RID: 26969
			// (set) Token: 0x060095BB RID: 38331 RVA: 0x000DA162 File Offset: 0x000D8362
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700695A RID: 26970
			// (set) Token: 0x060095BC RID: 38332 RVA: 0x000DA17A File Offset: 0x000D837A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700695B RID: 26971
			// (set) Token: 0x060095BD RID: 38333 RVA: 0x000DA192 File Offset: 0x000D8392
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700695C RID: 26972
			// (set) Token: 0x060095BE RID: 38334 RVA: 0x000DA1AA File Offset: 0x000D83AA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700695D RID: 26973
			// (set) Token: 0x060095BF RID: 38335 RVA: 0x000DA1C2 File Offset: 0x000D83C2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C0D RID: 3085
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700695E RID: 26974
			// (set) Token: 0x060095C1 RID: 38337 RVA: 0x000DA1E2 File Offset: 0x000D83E2
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700695F RID: 26975
			// (set) Token: 0x060095C2 RID: 38338 RVA: 0x000DA200 File Offset: 0x000D8400
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006960 RID: 26976
			// (set) Token: 0x060095C3 RID: 38339 RVA: 0x000DA21E File Offset: 0x000D841E
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17006961 RID: 26977
			// (set) Token: 0x060095C4 RID: 38340 RVA: 0x000DA231 File Offset: 0x000D8431
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17006962 RID: 26978
			// (set) Token: 0x060095C5 RID: 38341 RVA: 0x000DA249 File Offset: 0x000D8449
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17006963 RID: 26979
			// (set) Token: 0x060095C6 RID: 38342 RVA: 0x000DA261 File Offset: 0x000D8461
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006964 RID: 26980
			// (set) Token: 0x060095C7 RID: 38343 RVA: 0x000DA279 File Offset: 0x000D8479
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006965 RID: 26981
			// (set) Token: 0x060095C8 RID: 38344 RVA: 0x000DA28C File Offset: 0x000D848C
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006966 RID: 26982
			// (set) Token: 0x060095C9 RID: 38345 RVA: 0x000DA2A4 File Offset: 0x000D84A4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006967 RID: 26983
			// (set) Token: 0x060095CA RID: 38346 RVA: 0x000DA2B7 File Offset: 0x000D84B7
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006968 RID: 26984
			// (set) Token: 0x060095CB RID: 38347 RVA: 0x000DA2CA File Offset: 0x000D84CA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006969 RID: 26985
			// (set) Token: 0x060095CC RID: 38348 RVA: 0x000DA2E8 File Offset: 0x000D84E8
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700696A RID: 26986
			// (set) Token: 0x060095CD RID: 38349 RVA: 0x000DA2FB File Offset: 0x000D84FB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700696B RID: 26987
			// (set) Token: 0x060095CE RID: 38350 RVA: 0x000DA319 File Offset: 0x000D8519
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700696C RID: 26988
			// (set) Token: 0x060095CF RID: 38351 RVA: 0x000DA32C File Offset: 0x000D852C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700696D RID: 26989
			// (set) Token: 0x060095D0 RID: 38352 RVA: 0x000DA344 File Offset: 0x000D8544
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700696E RID: 26990
			// (set) Token: 0x060095D1 RID: 38353 RVA: 0x000DA35C File Offset: 0x000D855C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700696F RID: 26991
			// (set) Token: 0x060095D2 RID: 38354 RVA: 0x000DA374 File Offset: 0x000D8574
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006970 RID: 26992
			// (set) Token: 0x060095D3 RID: 38355 RVA: 0x000DA38C File Offset: 0x000D858C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C0E RID: 3086
		public class PrecannedFilterParameters : ParametersBase
		{
			// Token: 0x17006971 RID: 26993
			// (set) Token: 0x060095D5 RID: 38357 RVA: 0x000DA3AC File Offset: 0x000D85AC
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x17006972 RID: 26994
			// (set) Token: 0x060095D6 RID: 38358 RVA: 0x000DA3C4 File Offset: 0x000D85C4
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x17006973 RID: 26995
			// (set) Token: 0x060095D7 RID: 38359 RVA: 0x000DA3D7 File Offset: 0x000D85D7
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x17006974 RID: 26996
			// (set) Token: 0x060095D8 RID: 38360 RVA: 0x000DA3EA File Offset: 0x000D85EA
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x17006975 RID: 26997
			// (set) Token: 0x060095D9 RID: 38361 RVA: 0x000DA3FD File Offset: 0x000D85FD
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x17006976 RID: 26998
			// (set) Token: 0x060095DA RID: 38362 RVA: 0x000DA410 File Offset: 0x000D8610
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x17006977 RID: 26999
			// (set) Token: 0x060095DB RID: 38363 RVA: 0x000DA423 File Offset: 0x000D8623
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x17006978 RID: 27000
			// (set) Token: 0x060095DC RID: 38364 RVA: 0x000DA436 File Offset: 0x000D8636
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x17006979 RID: 27001
			// (set) Token: 0x060095DD RID: 38365 RVA: 0x000DA449 File Offset: 0x000D8649
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700697A RID: 27002
			// (set) Token: 0x060095DE RID: 38366 RVA: 0x000DA45C File Offset: 0x000D865C
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x1700697B RID: 27003
			// (set) Token: 0x060095DF RID: 38367 RVA: 0x000DA46F File Offset: 0x000D866F
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x1700697C RID: 27004
			// (set) Token: 0x060095E0 RID: 38368 RVA: 0x000DA482 File Offset: 0x000D8682
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x1700697D RID: 27005
			// (set) Token: 0x060095E1 RID: 38369 RVA: 0x000DA495 File Offset: 0x000D8695
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x1700697E RID: 27006
			// (set) Token: 0x060095E2 RID: 38370 RVA: 0x000DA4A8 File Offset: 0x000D86A8
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x1700697F RID: 27007
			// (set) Token: 0x060095E3 RID: 38371 RVA: 0x000DA4BB File Offset: 0x000D86BB
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x17006980 RID: 27008
			// (set) Token: 0x060095E4 RID: 38372 RVA: 0x000DA4CE File Offset: 0x000D86CE
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x17006981 RID: 27009
			// (set) Token: 0x060095E5 RID: 38373 RVA: 0x000DA4E1 File Offset: 0x000D86E1
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x17006982 RID: 27010
			// (set) Token: 0x060095E6 RID: 38374 RVA: 0x000DA4F4 File Offset: 0x000D86F4
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x17006983 RID: 27011
			// (set) Token: 0x060095E7 RID: 38375 RVA: 0x000DA507 File Offset: 0x000D8707
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17006984 RID: 27012
			// (set) Token: 0x060095E8 RID: 38376 RVA: 0x000DA51A File Offset: 0x000D871A
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006985 RID: 27013
			// (set) Token: 0x060095E9 RID: 38377 RVA: 0x000DA538 File Offset: 0x000D8738
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006986 RID: 27014
			// (set) Token: 0x060095EA RID: 38378 RVA: 0x000DA556 File Offset: 0x000D8756
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17006987 RID: 27015
			// (set) Token: 0x060095EB RID: 38379 RVA: 0x000DA569 File Offset: 0x000D8769
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17006988 RID: 27016
			// (set) Token: 0x060095EC RID: 38380 RVA: 0x000DA581 File Offset: 0x000D8781
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17006989 RID: 27017
			// (set) Token: 0x060095ED RID: 38381 RVA: 0x000DA599 File Offset: 0x000D8799
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700698A RID: 27018
			// (set) Token: 0x060095EE RID: 38382 RVA: 0x000DA5B1 File Offset: 0x000D87B1
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700698B RID: 27019
			// (set) Token: 0x060095EF RID: 38383 RVA: 0x000DA5C4 File Offset: 0x000D87C4
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700698C RID: 27020
			// (set) Token: 0x060095F0 RID: 38384 RVA: 0x000DA5DC File Offset: 0x000D87DC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700698D RID: 27021
			// (set) Token: 0x060095F1 RID: 38385 RVA: 0x000DA5EF File Offset: 0x000D87EF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700698E RID: 27022
			// (set) Token: 0x060095F2 RID: 38386 RVA: 0x000DA602 File Offset: 0x000D8802
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700698F RID: 27023
			// (set) Token: 0x060095F3 RID: 38387 RVA: 0x000DA620 File Offset: 0x000D8820
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006990 RID: 27024
			// (set) Token: 0x060095F4 RID: 38388 RVA: 0x000DA633 File Offset: 0x000D8833
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006991 RID: 27025
			// (set) Token: 0x060095F5 RID: 38389 RVA: 0x000DA651 File Offset: 0x000D8851
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006992 RID: 27026
			// (set) Token: 0x060095F6 RID: 38390 RVA: 0x000DA664 File Offset: 0x000D8864
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006993 RID: 27027
			// (set) Token: 0x060095F7 RID: 38391 RVA: 0x000DA67C File Offset: 0x000D887C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006994 RID: 27028
			// (set) Token: 0x060095F8 RID: 38392 RVA: 0x000DA694 File Offset: 0x000D8894
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006995 RID: 27029
			// (set) Token: 0x060095F9 RID: 38393 RVA: 0x000DA6AC File Offset: 0x000D88AC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006996 RID: 27030
			// (set) Token: 0x060095FA RID: 38394 RVA: 0x000DA6C4 File Offset: 0x000D88C4
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
