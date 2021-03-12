using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BF2 RID: 3058
	public class SetContactCommand : SyntheticCommandWithPipelineInputNoOutput<Contact>
	{
		// Token: 0x0600942C RID: 37932 RVA: 0x000D80DF File Offset: 0x000D62DF
		private SetContactCommand() : base("Set-Contact")
		{
		}

		// Token: 0x0600942D RID: 37933 RVA: 0x000D80EC File Offset: 0x000D62EC
		public SetContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600942E RID: 37934 RVA: 0x000D80FB File Offset: 0x000D62FB
		public virtual SetContactCommand SetParameters(SetContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600942F RID: 37935 RVA: 0x000D8105 File Offset: 0x000D6305
		public virtual SetContactCommand SetParameters(SetContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BF3 RID: 3059
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006801 RID: 26625
			// (set) Token: 0x06009430 RID: 37936 RVA: 0x000D810F File Offset: 0x000D630F
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17006802 RID: 26626
			// (set) Token: 0x06009431 RID: 37937 RVA: 0x000D812D File Offset: 0x000D632D
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17006803 RID: 26627
			// (set) Token: 0x06009432 RID: 37938 RVA: 0x000D8145 File Offset: 0x000D6345
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006804 RID: 26628
			// (set) Token: 0x06009433 RID: 37939 RVA: 0x000D815D File Offset: 0x000D635D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006805 RID: 26629
			// (set) Token: 0x06009434 RID: 37940 RVA: 0x000D8170 File Offset: 0x000D6370
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17006806 RID: 26630
			// (set) Token: 0x06009435 RID: 37941 RVA: 0x000D8183 File Offset: 0x000D6383
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17006807 RID: 26631
			// (set) Token: 0x06009436 RID: 37942 RVA: 0x000D8196 File Offset: 0x000D6396
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17006808 RID: 26632
			// (set) Token: 0x06009437 RID: 37943 RVA: 0x000D81A9 File Offset: 0x000D63A9
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17006809 RID: 26633
			// (set) Token: 0x06009438 RID: 37944 RVA: 0x000D81BC File Offset: 0x000D63BC
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700680A RID: 26634
			// (set) Token: 0x06009439 RID: 37945 RVA: 0x000D81CF File Offset: 0x000D63CF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700680B RID: 26635
			// (set) Token: 0x0600943A RID: 37946 RVA: 0x000D81E2 File Offset: 0x000D63E2
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700680C RID: 26636
			// (set) Token: 0x0600943B RID: 37947 RVA: 0x000D81F5 File Offset: 0x000D63F5
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700680D RID: 26637
			// (set) Token: 0x0600943C RID: 37948 RVA: 0x000D8208 File Offset: 0x000D6408
			public virtual GeoCoordinates GeoCoordinates
			{
				set
				{
					base.PowerSharpParameters["GeoCoordinates"] = value;
				}
			}

			// Token: 0x1700680E RID: 26638
			// (set) Token: 0x0600943D RID: 37949 RVA: 0x000D821B File Offset: 0x000D641B
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700680F RID: 26639
			// (set) Token: 0x0600943E RID: 37950 RVA: 0x000D822E File Offset: 0x000D642E
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17006810 RID: 26640
			// (set) Token: 0x0600943F RID: 37951 RVA: 0x000D8241 File Offset: 0x000D6441
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006811 RID: 26641
			// (set) Token: 0x06009440 RID: 37952 RVA: 0x000D8254 File Offset: 0x000D6454
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17006812 RID: 26642
			// (set) Token: 0x06009441 RID: 37953 RVA: 0x000D8267 File Offset: 0x000D6467
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006813 RID: 26643
			// (set) Token: 0x06009442 RID: 37954 RVA: 0x000D827A File Offset: 0x000D647A
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17006814 RID: 26644
			// (set) Token: 0x06009443 RID: 37955 RVA: 0x000D828D File Offset: 0x000D648D
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17006815 RID: 26645
			// (set) Token: 0x06009444 RID: 37956 RVA: 0x000D82A0 File Offset: 0x000D64A0
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17006816 RID: 26646
			// (set) Token: 0x06009445 RID: 37957 RVA: 0x000D82B3 File Offset: 0x000D64B3
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17006817 RID: 26647
			// (set) Token: 0x06009446 RID: 37958 RVA: 0x000D82C6 File Offset: 0x000D64C6
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17006818 RID: 26648
			// (set) Token: 0x06009447 RID: 37959 RVA: 0x000D82D9 File Offset: 0x000D64D9
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17006819 RID: 26649
			// (set) Token: 0x06009448 RID: 37960 RVA: 0x000D82EC File Offset: 0x000D64EC
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700681A RID: 26650
			// (set) Token: 0x06009449 RID: 37961 RVA: 0x000D82FF File Offset: 0x000D64FF
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700681B RID: 26651
			// (set) Token: 0x0600944A RID: 37962 RVA: 0x000D8312 File Offset: 0x000D6512
			public virtual MultiValuedProperty<string> PostOfficeBox
			{
				set
				{
					base.PowerSharpParameters["PostOfficeBox"] = value;
				}
			}

			// Token: 0x1700681C RID: 26652
			// (set) Token: 0x0600944B RID: 37963 RVA: 0x000D8325 File Offset: 0x000D6525
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700681D RID: 26653
			// (set) Token: 0x0600944C RID: 37964 RVA: 0x000D8338 File Offset: 0x000D6538
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700681E RID: 26654
			// (set) Token: 0x0600944D RID: 37965 RVA: 0x000D834B File Offset: 0x000D654B
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700681F RID: 26655
			// (set) Token: 0x0600944E RID: 37966 RVA: 0x000D835E File Offset: 0x000D655E
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17006820 RID: 26656
			// (set) Token: 0x0600944F RID: 37967 RVA: 0x000D8371 File Offset: 0x000D6571
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17006821 RID: 26657
			// (set) Token: 0x06009450 RID: 37968 RVA: 0x000D8384 File Offset: 0x000D6584
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x17006822 RID: 26658
			// (set) Token: 0x06009451 RID: 37969 RVA: 0x000D839C File Offset: 0x000D659C
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17006823 RID: 26659
			// (set) Token: 0x06009452 RID: 37970 RVA: 0x000D83AF File Offset: 0x000D65AF
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17006824 RID: 26660
			// (set) Token: 0x06009453 RID: 37971 RVA: 0x000D83C2 File Offset: 0x000D65C2
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006825 RID: 26661
			// (set) Token: 0x06009454 RID: 37972 RVA: 0x000D83DA File Offset: 0x000D65DA
			public virtual MultiValuedProperty<string> UMCallingLineIds
			{
				set
				{
					base.PowerSharpParameters["UMCallingLineIds"] = value;
				}
			}

			// Token: 0x17006826 RID: 26662
			// (set) Token: 0x06009455 RID: 37973 RVA: 0x000D83ED File Offset: 0x000D65ED
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17006827 RID: 26663
			// (set) Token: 0x06009456 RID: 37974 RVA: 0x000D8405 File Offset: 0x000D6605
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006828 RID: 26664
			// (set) Token: 0x06009457 RID: 37975 RVA: 0x000D8418 File Offset: 0x000D6618
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006829 RID: 26665
			// (set) Token: 0x06009458 RID: 37976 RVA: 0x000D8430 File Offset: 0x000D6630
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700682A RID: 26666
			// (set) Token: 0x06009459 RID: 37977 RVA: 0x000D8448 File Offset: 0x000D6648
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700682B RID: 26667
			// (set) Token: 0x0600945A RID: 37978 RVA: 0x000D8460 File Offset: 0x000D6660
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700682C RID: 26668
			// (set) Token: 0x0600945B RID: 37979 RVA: 0x000D8478 File Offset: 0x000D6678
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BF4 RID: 3060
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700682D RID: 26669
			// (set) Token: 0x0600945D RID: 37981 RVA: 0x000D8498 File Offset: 0x000D6698
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700682E RID: 26670
			// (set) Token: 0x0600945E RID: 37982 RVA: 0x000D84B6 File Offset: 0x000D66B6
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700682F RID: 26671
			// (set) Token: 0x0600945F RID: 37983 RVA: 0x000D84D4 File Offset: 0x000D66D4
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17006830 RID: 26672
			// (set) Token: 0x06009460 RID: 37984 RVA: 0x000D84EC File Offset: 0x000D66EC
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006831 RID: 26673
			// (set) Token: 0x06009461 RID: 37985 RVA: 0x000D8504 File Offset: 0x000D6704
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006832 RID: 26674
			// (set) Token: 0x06009462 RID: 37986 RVA: 0x000D8517 File Offset: 0x000D6717
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17006833 RID: 26675
			// (set) Token: 0x06009463 RID: 37987 RVA: 0x000D852A File Offset: 0x000D672A
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17006834 RID: 26676
			// (set) Token: 0x06009464 RID: 37988 RVA: 0x000D853D File Offset: 0x000D673D
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17006835 RID: 26677
			// (set) Token: 0x06009465 RID: 37989 RVA: 0x000D8550 File Offset: 0x000D6750
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17006836 RID: 26678
			// (set) Token: 0x06009466 RID: 37990 RVA: 0x000D8563 File Offset: 0x000D6763
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17006837 RID: 26679
			// (set) Token: 0x06009467 RID: 37991 RVA: 0x000D8576 File Offset: 0x000D6776
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006838 RID: 26680
			// (set) Token: 0x06009468 RID: 37992 RVA: 0x000D8589 File Offset: 0x000D6789
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17006839 RID: 26681
			// (set) Token: 0x06009469 RID: 37993 RVA: 0x000D859C File Offset: 0x000D679C
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700683A RID: 26682
			// (set) Token: 0x0600946A RID: 37994 RVA: 0x000D85AF File Offset: 0x000D67AF
			public virtual GeoCoordinates GeoCoordinates
			{
				set
				{
					base.PowerSharpParameters["GeoCoordinates"] = value;
				}
			}

			// Token: 0x1700683B RID: 26683
			// (set) Token: 0x0600946B RID: 37995 RVA: 0x000D85C2 File Offset: 0x000D67C2
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700683C RID: 26684
			// (set) Token: 0x0600946C RID: 37996 RVA: 0x000D85D5 File Offset: 0x000D67D5
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700683D RID: 26685
			// (set) Token: 0x0600946D RID: 37997 RVA: 0x000D85E8 File Offset: 0x000D67E8
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700683E RID: 26686
			// (set) Token: 0x0600946E RID: 37998 RVA: 0x000D85FB File Offset: 0x000D67FB
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700683F RID: 26687
			// (set) Token: 0x0600946F RID: 37999 RVA: 0x000D860E File Offset: 0x000D680E
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006840 RID: 26688
			// (set) Token: 0x06009470 RID: 38000 RVA: 0x000D8621 File Offset: 0x000D6821
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17006841 RID: 26689
			// (set) Token: 0x06009471 RID: 38001 RVA: 0x000D8634 File Offset: 0x000D6834
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17006842 RID: 26690
			// (set) Token: 0x06009472 RID: 38002 RVA: 0x000D8647 File Offset: 0x000D6847
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17006843 RID: 26691
			// (set) Token: 0x06009473 RID: 38003 RVA: 0x000D865A File Offset: 0x000D685A
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17006844 RID: 26692
			// (set) Token: 0x06009474 RID: 38004 RVA: 0x000D866D File Offset: 0x000D686D
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17006845 RID: 26693
			// (set) Token: 0x06009475 RID: 38005 RVA: 0x000D8680 File Offset: 0x000D6880
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17006846 RID: 26694
			// (set) Token: 0x06009476 RID: 38006 RVA: 0x000D8693 File Offset: 0x000D6893
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006847 RID: 26695
			// (set) Token: 0x06009477 RID: 38007 RVA: 0x000D86A6 File Offset: 0x000D68A6
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17006848 RID: 26696
			// (set) Token: 0x06009478 RID: 38008 RVA: 0x000D86B9 File Offset: 0x000D68B9
			public virtual MultiValuedProperty<string> PostOfficeBox
			{
				set
				{
					base.PowerSharpParameters["PostOfficeBox"] = value;
				}
			}

			// Token: 0x17006849 RID: 26697
			// (set) Token: 0x06009479 RID: 38009 RVA: 0x000D86CC File Offset: 0x000D68CC
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700684A RID: 26698
			// (set) Token: 0x0600947A RID: 38010 RVA: 0x000D86DF File Offset: 0x000D68DF
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700684B RID: 26699
			// (set) Token: 0x0600947B RID: 38011 RVA: 0x000D86F2 File Offset: 0x000D68F2
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700684C RID: 26700
			// (set) Token: 0x0600947C RID: 38012 RVA: 0x000D8705 File Offset: 0x000D6905
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700684D RID: 26701
			// (set) Token: 0x0600947D RID: 38013 RVA: 0x000D8718 File Offset: 0x000D6918
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700684E RID: 26702
			// (set) Token: 0x0600947E RID: 38014 RVA: 0x000D872B File Offset: 0x000D692B
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700684F RID: 26703
			// (set) Token: 0x0600947F RID: 38015 RVA: 0x000D8743 File Offset: 0x000D6943
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17006850 RID: 26704
			// (set) Token: 0x06009480 RID: 38016 RVA: 0x000D8756 File Offset: 0x000D6956
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17006851 RID: 26705
			// (set) Token: 0x06009481 RID: 38017 RVA: 0x000D8769 File Offset: 0x000D6969
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006852 RID: 26706
			// (set) Token: 0x06009482 RID: 38018 RVA: 0x000D8781 File Offset: 0x000D6981
			public virtual MultiValuedProperty<string> UMCallingLineIds
			{
				set
				{
					base.PowerSharpParameters["UMCallingLineIds"] = value;
				}
			}

			// Token: 0x17006853 RID: 26707
			// (set) Token: 0x06009483 RID: 38019 RVA: 0x000D8794 File Offset: 0x000D6994
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17006854 RID: 26708
			// (set) Token: 0x06009484 RID: 38020 RVA: 0x000D87AC File Offset: 0x000D69AC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006855 RID: 26709
			// (set) Token: 0x06009485 RID: 38021 RVA: 0x000D87BF File Offset: 0x000D69BF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006856 RID: 26710
			// (set) Token: 0x06009486 RID: 38022 RVA: 0x000D87D7 File Offset: 0x000D69D7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006857 RID: 26711
			// (set) Token: 0x06009487 RID: 38023 RVA: 0x000D87EF File Offset: 0x000D69EF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006858 RID: 26712
			// (set) Token: 0x06009488 RID: 38024 RVA: 0x000D8807 File Offset: 0x000D6A07
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006859 RID: 26713
			// (set) Token: 0x06009489 RID: 38025 RVA: 0x000D881F File Offset: 0x000D6A1F
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
