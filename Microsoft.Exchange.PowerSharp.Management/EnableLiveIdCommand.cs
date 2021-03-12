using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000A8 RID: 168
	public class EnableLiveIdCommand : SyntheticCommandWithPipelineInputNoOutput<uint>
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x00039111 File Offset: 0x00037311
		private EnableLiveIdCommand() : base("Enable-LiveId")
		{
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0003911E File Offset: 0x0003731E
		public EnableLiveIdCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0003912D File Offset: 0x0003732D
		public virtual EnableLiveIdCommand SetParameters(EnableLiveIdCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00039137 File Offset: 0x00037337
		public virtual EnableLiveIdCommand SetParameters(EnableLiveIdCommand.PfxFileAndPasswordParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00039141 File Offset: 0x00037341
		public virtual EnableLiveIdCommand SetParameters(EnableLiveIdCommand.IssuedToParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0003914B File Offset: 0x0003734B
		public virtual EnableLiveIdCommand SetParameters(EnableLiveIdCommand.SHA1ThumbprintParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000A9 RID: 169
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000433 RID: 1075
			// (set) Token: 0x060019D0 RID: 6608 RVA: 0x00039155 File Offset: 0x00037355
			public virtual LiveIdInstanceType TargetInstance
			{
				set
				{
					base.PowerSharpParameters["TargetInstance"] = value;
				}
			}

			// Token: 0x17000434 RID: 1076
			// (set) Token: 0x060019D1 RID: 6609 RVA: 0x0003916D File Offset: 0x0003736D
			public virtual uint SiteId
			{
				set
				{
					base.PowerSharpParameters["SiteId"] = value;
				}
			}

			// Token: 0x17000435 RID: 1077
			// (set) Token: 0x060019D2 RID: 6610 RVA: 0x00039185 File Offset: 0x00037385
			public virtual string SiteName
			{
				set
				{
					base.PowerSharpParameters["SiteName"] = value;
				}
			}

			// Token: 0x17000436 RID: 1078
			// (set) Token: 0x060019D3 RID: 6611 RVA: 0x00039198 File Offset: 0x00037398
			public virtual uint AccrualSiteId
			{
				set
				{
					base.PowerSharpParameters["AccrualSiteId"] = value;
				}
			}

			// Token: 0x17000437 RID: 1079
			// (set) Token: 0x060019D4 RID: 6612 RVA: 0x000391B0 File Offset: 0x000373B0
			public virtual string AccrualSiteName
			{
				set
				{
					base.PowerSharpParameters["AccrualSiteName"] = value;
				}
			}

			// Token: 0x17000438 RID: 1080
			// (set) Token: 0x060019D5 RID: 6613 RVA: 0x000391C3 File Offset: 0x000373C3
			public virtual string InternalSiteName
			{
				set
				{
					base.PowerSharpParameters["InternalSiteName"] = value;
				}
			}

			// Token: 0x17000439 RID: 1081
			// (set) Token: 0x060019D6 RID: 6614 RVA: 0x000391D6 File Offset: 0x000373D6
			public virtual string O365SiteName
			{
				set
				{
					base.PowerSharpParameters["O365SiteName"] = value;
				}
			}

			// Token: 0x1700043A RID: 1082
			// (set) Token: 0x060019D7 RID: 6615 RVA: 0x000391E9 File Offset: 0x000373E9
			public virtual uint MsoSiteId
			{
				set
				{
					base.PowerSharpParameters["MsoSiteId"] = value;
				}
			}

			// Token: 0x1700043B RID: 1083
			// (set) Token: 0x060019D8 RID: 6616 RVA: 0x00039201 File Offset: 0x00037401
			public virtual string MsoSiteName
			{
				set
				{
					base.PowerSharpParameters["MsoSiteName"] = value;
				}
			}

			// Token: 0x1700043C RID: 1084
			// (set) Token: 0x060019D9 RID: 6617 RVA: 0x00039214 File Offset: 0x00037414
			public virtual TargetEnvironment TargetEnvironment
			{
				set
				{
					base.PowerSharpParameters["TargetEnvironment"] = value;
				}
			}

			// Token: 0x1700043D RID: 1085
			// (set) Token: 0x060019DA RID: 6618 RVA: 0x0003922C File Offset: 0x0003742C
			public virtual string Proxy
			{
				set
				{
					base.PowerSharpParameters["Proxy"] = value;
				}
			}

			// Token: 0x1700043E RID: 1086
			// (set) Token: 0x060019DB RID: 6619 RVA: 0x0003923F File Offset: 0x0003743F
			public virtual string MsoRpsNetworkProd
			{
				set
				{
					base.PowerSharpParameters["MsoRpsNetworkProd"] = value;
				}
			}

			// Token: 0x1700043F RID: 1087
			// (set) Token: 0x060019DC RID: 6620 RVA: 0x00039252 File Offset: 0x00037452
			public virtual string MsoRpsNetworkInt
			{
				set
				{
					base.PowerSharpParameters["MsoRpsNetworkInt"] = value;
				}
			}

			// Token: 0x17000440 RID: 1088
			// (set) Token: 0x060019DD RID: 6621 RVA: 0x00039265 File Offset: 0x00037465
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000441 RID: 1089
			// (set) Token: 0x060019DE RID: 6622 RVA: 0x0003927D File Offset: 0x0003747D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000442 RID: 1090
			// (set) Token: 0x060019DF RID: 6623 RVA: 0x00039295 File Offset: 0x00037495
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000443 RID: 1091
			// (set) Token: 0x060019E0 RID: 6624 RVA: 0x000392AD File Offset: 0x000374AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000444 RID: 1092
			// (set) Token: 0x060019E1 RID: 6625 RVA: 0x000392C5 File Offset: 0x000374C5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000AA RID: 170
		public class PfxFileAndPasswordParameters : ParametersBase
		{
			// Token: 0x17000445 RID: 1093
			// (set) Token: 0x060019E3 RID: 6627 RVA: 0x000392E5 File Offset: 0x000374E5
			public virtual string Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17000446 RID: 1094
			// (set) Token: 0x060019E4 RID: 6628 RVA: 0x000392F8 File Offset: 0x000374F8
			public virtual string CertFile
			{
				set
				{
					base.PowerSharpParameters["CertFile"] = value;
				}
			}

			// Token: 0x17000447 RID: 1095
			// (set) Token: 0x060019E5 RID: 6629 RVA: 0x0003930B File Offset: 0x0003750B
			public virtual LiveIdInstanceType TargetInstance
			{
				set
				{
					base.PowerSharpParameters["TargetInstance"] = value;
				}
			}

			// Token: 0x17000448 RID: 1096
			// (set) Token: 0x060019E6 RID: 6630 RVA: 0x00039323 File Offset: 0x00037523
			public virtual uint SiteId
			{
				set
				{
					base.PowerSharpParameters["SiteId"] = value;
				}
			}

			// Token: 0x17000449 RID: 1097
			// (set) Token: 0x060019E7 RID: 6631 RVA: 0x0003933B File Offset: 0x0003753B
			public virtual string SiteName
			{
				set
				{
					base.PowerSharpParameters["SiteName"] = value;
				}
			}

			// Token: 0x1700044A RID: 1098
			// (set) Token: 0x060019E8 RID: 6632 RVA: 0x0003934E File Offset: 0x0003754E
			public virtual uint AccrualSiteId
			{
				set
				{
					base.PowerSharpParameters["AccrualSiteId"] = value;
				}
			}

			// Token: 0x1700044B RID: 1099
			// (set) Token: 0x060019E9 RID: 6633 RVA: 0x00039366 File Offset: 0x00037566
			public virtual string AccrualSiteName
			{
				set
				{
					base.PowerSharpParameters["AccrualSiteName"] = value;
				}
			}

			// Token: 0x1700044C RID: 1100
			// (set) Token: 0x060019EA RID: 6634 RVA: 0x00039379 File Offset: 0x00037579
			public virtual string InternalSiteName
			{
				set
				{
					base.PowerSharpParameters["InternalSiteName"] = value;
				}
			}

			// Token: 0x1700044D RID: 1101
			// (set) Token: 0x060019EB RID: 6635 RVA: 0x0003938C File Offset: 0x0003758C
			public virtual string O365SiteName
			{
				set
				{
					base.PowerSharpParameters["O365SiteName"] = value;
				}
			}

			// Token: 0x1700044E RID: 1102
			// (set) Token: 0x060019EC RID: 6636 RVA: 0x0003939F File Offset: 0x0003759F
			public virtual uint MsoSiteId
			{
				set
				{
					base.PowerSharpParameters["MsoSiteId"] = value;
				}
			}

			// Token: 0x1700044F RID: 1103
			// (set) Token: 0x060019ED RID: 6637 RVA: 0x000393B7 File Offset: 0x000375B7
			public virtual string MsoSiteName
			{
				set
				{
					base.PowerSharpParameters["MsoSiteName"] = value;
				}
			}

			// Token: 0x17000450 RID: 1104
			// (set) Token: 0x060019EE RID: 6638 RVA: 0x000393CA File Offset: 0x000375CA
			public virtual TargetEnvironment TargetEnvironment
			{
				set
				{
					base.PowerSharpParameters["TargetEnvironment"] = value;
				}
			}

			// Token: 0x17000451 RID: 1105
			// (set) Token: 0x060019EF RID: 6639 RVA: 0x000393E2 File Offset: 0x000375E2
			public virtual string Proxy
			{
				set
				{
					base.PowerSharpParameters["Proxy"] = value;
				}
			}

			// Token: 0x17000452 RID: 1106
			// (set) Token: 0x060019F0 RID: 6640 RVA: 0x000393F5 File Offset: 0x000375F5
			public virtual string MsoRpsNetworkProd
			{
				set
				{
					base.PowerSharpParameters["MsoRpsNetworkProd"] = value;
				}
			}

			// Token: 0x17000453 RID: 1107
			// (set) Token: 0x060019F1 RID: 6641 RVA: 0x00039408 File Offset: 0x00037608
			public virtual string MsoRpsNetworkInt
			{
				set
				{
					base.PowerSharpParameters["MsoRpsNetworkInt"] = value;
				}
			}

			// Token: 0x17000454 RID: 1108
			// (set) Token: 0x060019F2 RID: 6642 RVA: 0x0003941B File Offset: 0x0003761B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000455 RID: 1109
			// (set) Token: 0x060019F3 RID: 6643 RVA: 0x00039433 File Offset: 0x00037633
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000456 RID: 1110
			// (set) Token: 0x060019F4 RID: 6644 RVA: 0x0003944B File Offset: 0x0003764B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000457 RID: 1111
			// (set) Token: 0x060019F5 RID: 6645 RVA: 0x00039463 File Offset: 0x00037663
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000458 RID: 1112
			// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0003947B File Offset: 0x0003767B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000AB RID: 171
		public class IssuedToParameters : ParametersBase
		{
			// Token: 0x17000459 RID: 1113
			// (set) Token: 0x060019F8 RID: 6648 RVA: 0x0003949B File Offset: 0x0003769B
			public virtual string IssuedTo
			{
				set
				{
					base.PowerSharpParameters["IssuedTo"] = value;
				}
			}

			// Token: 0x1700045A RID: 1114
			// (set) Token: 0x060019F9 RID: 6649 RVA: 0x000394AE File Offset: 0x000376AE
			public virtual LiveIdInstanceType TargetInstance
			{
				set
				{
					base.PowerSharpParameters["TargetInstance"] = value;
				}
			}

			// Token: 0x1700045B RID: 1115
			// (set) Token: 0x060019FA RID: 6650 RVA: 0x000394C6 File Offset: 0x000376C6
			public virtual uint SiteId
			{
				set
				{
					base.PowerSharpParameters["SiteId"] = value;
				}
			}

			// Token: 0x1700045C RID: 1116
			// (set) Token: 0x060019FB RID: 6651 RVA: 0x000394DE File Offset: 0x000376DE
			public virtual string SiteName
			{
				set
				{
					base.PowerSharpParameters["SiteName"] = value;
				}
			}

			// Token: 0x1700045D RID: 1117
			// (set) Token: 0x060019FC RID: 6652 RVA: 0x000394F1 File Offset: 0x000376F1
			public virtual uint AccrualSiteId
			{
				set
				{
					base.PowerSharpParameters["AccrualSiteId"] = value;
				}
			}

			// Token: 0x1700045E RID: 1118
			// (set) Token: 0x060019FD RID: 6653 RVA: 0x00039509 File Offset: 0x00037709
			public virtual string AccrualSiteName
			{
				set
				{
					base.PowerSharpParameters["AccrualSiteName"] = value;
				}
			}

			// Token: 0x1700045F RID: 1119
			// (set) Token: 0x060019FE RID: 6654 RVA: 0x0003951C File Offset: 0x0003771C
			public virtual string InternalSiteName
			{
				set
				{
					base.PowerSharpParameters["InternalSiteName"] = value;
				}
			}

			// Token: 0x17000460 RID: 1120
			// (set) Token: 0x060019FF RID: 6655 RVA: 0x0003952F File Offset: 0x0003772F
			public virtual string O365SiteName
			{
				set
				{
					base.PowerSharpParameters["O365SiteName"] = value;
				}
			}

			// Token: 0x17000461 RID: 1121
			// (set) Token: 0x06001A00 RID: 6656 RVA: 0x00039542 File Offset: 0x00037742
			public virtual uint MsoSiteId
			{
				set
				{
					base.PowerSharpParameters["MsoSiteId"] = value;
				}
			}

			// Token: 0x17000462 RID: 1122
			// (set) Token: 0x06001A01 RID: 6657 RVA: 0x0003955A File Offset: 0x0003775A
			public virtual string MsoSiteName
			{
				set
				{
					base.PowerSharpParameters["MsoSiteName"] = value;
				}
			}

			// Token: 0x17000463 RID: 1123
			// (set) Token: 0x06001A02 RID: 6658 RVA: 0x0003956D File Offset: 0x0003776D
			public virtual TargetEnvironment TargetEnvironment
			{
				set
				{
					base.PowerSharpParameters["TargetEnvironment"] = value;
				}
			}

			// Token: 0x17000464 RID: 1124
			// (set) Token: 0x06001A03 RID: 6659 RVA: 0x00039585 File Offset: 0x00037785
			public virtual string Proxy
			{
				set
				{
					base.PowerSharpParameters["Proxy"] = value;
				}
			}

			// Token: 0x17000465 RID: 1125
			// (set) Token: 0x06001A04 RID: 6660 RVA: 0x00039598 File Offset: 0x00037798
			public virtual string MsoRpsNetworkProd
			{
				set
				{
					base.PowerSharpParameters["MsoRpsNetworkProd"] = value;
				}
			}

			// Token: 0x17000466 RID: 1126
			// (set) Token: 0x06001A05 RID: 6661 RVA: 0x000395AB File Offset: 0x000377AB
			public virtual string MsoRpsNetworkInt
			{
				set
				{
					base.PowerSharpParameters["MsoRpsNetworkInt"] = value;
				}
			}

			// Token: 0x17000467 RID: 1127
			// (set) Token: 0x06001A06 RID: 6662 RVA: 0x000395BE File Offset: 0x000377BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000468 RID: 1128
			// (set) Token: 0x06001A07 RID: 6663 RVA: 0x000395D6 File Offset: 0x000377D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000469 RID: 1129
			// (set) Token: 0x06001A08 RID: 6664 RVA: 0x000395EE File Offset: 0x000377EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700046A RID: 1130
			// (set) Token: 0x06001A09 RID: 6665 RVA: 0x00039606 File Offset: 0x00037806
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700046B RID: 1131
			// (set) Token: 0x06001A0A RID: 6666 RVA: 0x0003961E File Offset: 0x0003781E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000AC RID: 172
		public class SHA1ThumbprintParameters : ParametersBase
		{
			// Token: 0x1700046C RID: 1132
			// (set) Token: 0x06001A0C RID: 6668 RVA: 0x0003963E File Offset: 0x0003783E
			public virtual string SHA1Thumbprint
			{
				set
				{
					base.PowerSharpParameters["SHA1Thumbprint"] = value;
				}
			}

			// Token: 0x1700046D RID: 1133
			// (set) Token: 0x06001A0D RID: 6669 RVA: 0x00039651 File Offset: 0x00037851
			public virtual string MsoSHA1Thumbprint
			{
				set
				{
					base.PowerSharpParameters["MsoSHA1Thumbprint"] = value;
				}
			}

			// Token: 0x1700046E RID: 1134
			// (set) Token: 0x06001A0E RID: 6670 RVA: 0x00039664 File Offset: 0x00037864
			public virtual LiveIdInstanceType TargetInstance
			{
				set
				{
					base.PowerSharpParameters["TargetInstance"] = value;
				}
			}

			// Token: 0x1700046F RID: 1135
			// (set) Token: 0x06001A0F RID: 6671 RVA: 0x0003967C File Offset: 0x0003787C
			public virtual uint SiteId
			{
				set
				{
					base.PowerSharpParameters["SiteId"] = value;
				}
			}

			// Token: 0x17000470 RID: 1136
			// (set) Token: 0x06001A10 RID: 6672 RVA: 0x00039694 File Offset: 0x00037894
			public virtual string SiteName
			{
				set
				{
					base.PowerSharpParameters["SiteName"] = value;
				}
			}

			// Token: 0x17000471 RID: 1137
			// (set) Token: 0x06001A11 RID: 6673 RVA: 0x000396A7 File Offset: 0x000378A7
			public virtual uint AccrualSiteId
			{
				set
				{
					base.PowerSharpParameters["AccrualSiteId"] = value;
				}
			}

			// Token: 0x17000472 RID: 1138
			// (set) Token: 0x06001A12 RID: 6674 RVA: 0x000396BF File Offset: 0x000378BF
			public virtual string AccrualSiteName
			{
				set
				{
					base.PowerSharpParameters["AccrualSiteName"] = value;
				}
			}

			// Token: 0x17000473 RID: 1139
			// (set) Token: 0x06001A13 RID: 6675 RVA: 0x000396D2 File Offset: 0x000378D2
			public virtual string InternalSiteName
			{
				set
				{
					base.PowerSharpParameters["InternalSiteName"] = value;
				}
			}

			// Token: 0x17000474 RID: 1140
			// (set) Token: 0x06001A14 RID: 6676 RVA: 0x000396E5 File Offset: 0x000378E5
			public virtual string O365SiteName
			{
				set
				{
					base.PowerSharpParameters["O365SiteName"] = value;
				}
			}

			// Token: 0x17000475 RID: 1141
			// (set) Token: 0x06001A15 RID: 6677 RVA: 0x000396F8 File Offset: 0x000378F8
			public virtual uint MsoSiteId
			{
				set
				{
					base.PowerSharpParameters["MsoSiteId"] = value;
				}
			}

			// Token: 0x17000476 RID: 1142
			// (set) Token: 0x06001A16 RID: 6678 RVA: 0x00039710 File Offset: 0x00037910
			public virtual string MsoSiteName
			{
				set
				{
					base.PowerSharpParameters["MsoSiteName"] = value;
				}
			}

			// Token: 0x17000477 RID: 1143
			// (set) Token: 0x06001A17 RID: 6679 RVA: 0x00039723 File Offset: 0x00037923
			public virtual TargetEnvironment TargetEnvironment
			{
				set
				{
					base.PowerSharpParameters["TargetEnvironment"] = value;
				}
			}

			// Token: 0x17000478 RID: 1144
			// (set) Token: 0x06001A18 RID: 6680 RVA: 0x0003973B File Offset: 0x0003793B
			public virtual string Proxy
			{
				set
				{
					base.PowerSharpParameters["Proxy"] = value;
				}
			}

			// Token: 0x17000479 RID: 1145
			// (set) Token: 0x06001A19 RID: 6681 RVA: 0x0003974E File Offset: 0x0003794E
			public virtual string MsoRpsNetworkProd
			{
				set
				{
					base.PowerSharpParameters["MsoRpsNetworkProd"] = value;
				}
			}

			// Token: 0x1700047A RID: 1146
			// (set) Token: 0x06001A1A RID: 6682 RVA: 0x00039761 File Offset: 0x00037961
			public virtual string MsoRpsNetworkInt
			{
				set
				{
					base.PowerSharpParameters["MsoRpsNetworkInt"] = value;
				}
			}

			// Token: 0x1700047B RID: 1147
			// (set) Token: 0x06001A1B RID: 6683 RVA: 0x00039774 File Offset: 0x00037974
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700047C RID: 1148
			// (set) Token: 0x06001A1C RID: 6684 RVA: 0x0003978C File Offset: 0x0003798C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700047D RID: 1149
			// (set) Token: 0x06001A1D RID: 6685 RVA: 0x000397A4 File Offset: 0x000379A4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700047E RID: 1150
			// (set) Token: 0x06001A1E RID: 6686 RVA: 0x000397BC File Offset: 0x000379BC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700047F RID: 1151
			// (set) Token: 0x06001A1F RID: 6687 RVA: 0x000397D4 File Offset: 0x000379D4
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
