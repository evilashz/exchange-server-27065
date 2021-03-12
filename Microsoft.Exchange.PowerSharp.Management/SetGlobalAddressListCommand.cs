using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004C1 RID: 1217
	public class SetGlobalAddressListCommand : SyntheticCommandWithPipelineInputNoOutput<GlobalAddressList>
	{
		// Token: 0x060043D1 RID: 17361 RVA: 0x0006FAD4 File Offset: 0x0006DCD4
		private SetGlobalAddressListCommand() : base("Set-GlobalAddressList")
		{
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x0006FAE1 File Offset: 0x0006DCE1
		public SetGlobalAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x0006FAF0 File Offset: 0x0006DCF0
		public virtual SetGlobalAddressListCommand SetParameters(SetGlobalAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x0006FAFA File Offset: 0x0006DCFA
		public virtual SetGlobalAddressListCommand SetParameters(SetGlobalAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004C2 RID: 1218
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002608 RID: 9736
			// (set) Token: 0x060043D5 RID: 17365 RVA: 0x0006FB04 File Offset: 0x0006DD04
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x17002609 RID: 9737
			// (set) Token: 0x060043D6 RID: 17366 RVA: 0x0006FB17 File Offset: 0x0006DD17
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700260A RID: 9738
			// (set) Token: 0x060043D7 RID: 17367 RVA: 0x0006FB35 File Offset: 0x0006DD35
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x1700260B RID: 9739
			// (set) Token: 0x060043D8 RID: 17368 RVA: 0x0006FB4D File Offset: 0x0006DD4D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700260C RID: 9740
			// (set) Token: 0x060043D9 RID: 17369 RVA: 0x0006FB60 File Offset: 0x0006DD60
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700260D RID: 9741
			// (set) Token: 0x060043DA RID: 17370 RVA: 0x0006FB73 File Offset: 0x0006DD73
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x1700260E RID: 9742
			// (set) Token: 0x060043DB RID: 17371 RVA: 0x0006FB8B File Offset: 0x0006DD8B
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x1700260F RID: 9743
			// (set) Token: 0x060043DC RID: 17372 RVA: 0x0006FB9E File Offset: 0x0006DD9E
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x17002610 RID: 9744
			// (set) Token: 0x060043DD RID: 17373 RVA: 0x0006FBB1 File Offset: 0x0006DDB1
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x17002611 RID: 9745
			// (set) Token: 0x060043DE RID: 17374 RVA: 0x0006FBC4 File Offset: 0x0006DDC4
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x17002612 RID: 9746
			// (set) Token: 0x060043DF RID: 17375 RVA: 0x0006FBD7 File Offset: 0x0006DDD7
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x17002613 RID: 9747
			// (set) Token: 0x060043E0 RID: 17376 RVA: 0x0006FBEA File Offset: 0x0006DDEA
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x17002614 RID: 9748
			// (set) Token: 0x060043E1 RID: 17377 RVA: 0x0006FBFD File Offset: 0x0006DDFD
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x17002615 RID: 9749
			// (set) Token: 0x060043E2 RID: 17378 RVA: 0x0006FC10 File Offset: 0x0006DE10
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x17002616 RID: 9750
			// (set) Token: 0x060043E3 RID: 17379 RVA: 0x0006FC23 File Offset: 0x0006DE23
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x17002617 RID: 9751
			// (set) Token: 0x060043E4 RID: 17380 RVA: 0x0006FC36 File Offset: 0x0006DE36
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x17002618 RID: 9752
			// (set) Token: 0x060043E5 RID: 17381 RVA: 0x0006FC49 File Offset: 0x0006DE49
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x17002619 RID: 9753
			// (set) Token: 0x060043E6 RID: 17382 RVA: 0x0006FC5C File Offset: 0x0006DE5C
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x1700261A RID: 9754
			// (set) Token: 0x060043E7 RID: 17383 RVA: 0x0006FC6F File Offset: 0x0006DE6F
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x1700261B RID: 9755
			// (set) Token: 0x060043E8 RID: 17384 RVA: 0x0006FC82 File Offset: 0x0006DE82
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x1700261C RID: 9756
			// (set) Token: 0x060043E9 RID: 17385 RVA: 0x0006FC95 File Offset: 0x0006DE95
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x1700261D RID: 9757
			// (set) Token: 0x060043EA RID: 17386 RVA: 0x0006FCA8 File Offset: 0x0006DEA8
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x1700261E RID: 9758
			// (set) Token: 0x060043EB RID: 17387 RVA: 0x0006FCBB File Offset: 0x0006DEBB
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x1700261F RID: 9759
			// (set) Token: 0x060043EC RID: 17388 RVA: 0x0006FCCE File Offset: 0x0006DECE
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17002620 RID: 9760
			// (set) Token: 0x060043ED RID: 17389 RVA: 0x0006FCE1 File Offset: 0x0006DEE1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002621 RID: 9761
			// (set) Token: 0x060043EE RID: 17390 RVA: 0x0006FCF9 File Offset: 0x0006DEF9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002622 RID: 9762
			// (set) Token: 0x060043EF RID: 17391 RVA: 0x0006FD11 File Offset: 0x0006DF11
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002623 RID: 9763
			// (set) Token: 0x060043F0 RID: 17392 RVA: 0x0006FD29 File Offset: 0x0006DF29
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002624 RID: 9764
			// (set) Token: 0x060043F1 RID: 17393 RVA: 0x0006FD41 File Offset: 0x0006DF41
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004C3 RID: 1219
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002625 RID: 9765
			// (set) Token: 0x060043F3 RID: 17395 RVA: 0x0006FD61 File Offset: 0x0006DF61
			public virtual GlobalAddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002626 RID: 9766
			// (set) Token: 0x060043F4 RID: 17396 RVA: 0x0006FD74 File Offset: 0x0006DF74
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x17002627 RID: 9767
			// (set) Token: 0x060043F5 RID: 17397 RVA: 0x0006FD87 File Offset: 0x0006DF87
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17002628 RID: 9768
			// (set) Token: 0x060043F6 RID: 17398 RVA: 0x0006FDA5 File Offset: 0x0006DFA5
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17002629 RID: 9769
			// (set) Token: 0x060043F7 RID: 17399 RVA: 0x0006FDBD File Offset: 0x0006DFBD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700262A RID: 9770
			// (set) Token: 0x060043F8 RID: 17400 RVA: 0x0006FDD0 File Offset: 0x0006DFD0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700262B RID: 9771
			// (set) Token: 0x060043F9 RID: 17401 RVA: 0x0006FDE3 File Offset: 0x0006DFE3
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x1700262C RID: 9772
			// (set) Token: 0x060043FA RID: 17402 RVA: 0x0006FDFB File Offset: 0x0006DFFB
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x1700262D RID: 9773
			// (set) Token: 0x060043FB RID: 17403 RVA: 0x0006FE0E File Offset: 0x0006E00E
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x1700262E RID: 9774
			// (set) Token: 0x060043FC RID: 17404 RVA: 0x0006FE21 File Offset: 0x0006E021
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x1700262F RID: 9775
			// (set) Token: 0x060043FD RID: 17405 RVA: 0x0006FE34 File Offset: 0x0006E034
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x17002630 RID: 9776
			// (set) Token: 0x060043FE RID: 17406 RVA: 0x0006FE47 File Offset: 0x0006E047
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x17002631 RID: 9777
			// (set) Token: 0x060043FF RID: 17407 RVA: 0x0006FE5A File Offset: 0x0006E05A
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x17002632 RID: 9778
			// (set) Token: 0x06004400 RID: 17408 RVA: 0x0006FE6D File Offset: 0x0006E06D
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x17002633 RID: 9779
			// (set) Token: 0x06004401 RID: 17409 RVA: 0x0006FE80 File Offset: 0x0006E080
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x17002634 RID: 9780
			// (set) Token: 0x06004402 RID: 17410 RVA: 0x0006FE93 File Offset: 0x0006E093
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x17002635 RID: 9781
			// (set) Token: 0x06004403 RID: 17411 RVA: 0x0006FEA6 File Offset: 0x0006E0A6
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x17002636 RID: 9782
			// (set) Token: 0x06004404 RID: 17412 RVA: 0x0006FEB9 File Offset: 0x0006E0B9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x17002637 RID: 9783
			// (set) Token: 0x06004405 RID: 17413 RVA: 0x0006FECC File Offset: 0x0006E0CC
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x17002638 RID: 9784
			// (set) Token: 0x06004406 RID: 17414 RVA: 0x0006FEDF File Offset: 0x0006E0DF
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x17002639 RID: 9785
			// (set) Token: 0x06004407 RID: 17415 RVA: 0x0006FEF2 File Offset: 0x0006E0F2
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x1700263A RID: 9786
			// (set) Token: 0x06004408 RID: 17416 RVA: 0x0006FF05 File Offset: 0x0006E105
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x1700263B RID: 9787
			// (set) Token: 0x06004409 RID: 17417 RVA: 0x0006FF18 File Offset: 0x0006E118
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x1700263C RID: 9788
			// (set) Token: 0x0600440A RID: 17418 RVA: 0x0006FF2B File Offset: 0x0006E12B
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x1700263D RID: 9789
			// (set) Token: 0x0600440B RID: 17419 RVA: 0x0006FF3E File Offset: 0x0006E13E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x1700263E RID: 9790
			// (set) Token: 0x0600440C RID: 17420 RVA: 0x0006FF51 File Offset: 0x0006E151
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700263F RID: 9791
			// (set) Token: 0x0600440D RID: 17421 RVA: 0x0006FF69 File Offset: 0x0006E169
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002640 RID: 9792
			// (set) Token: 0x0600440E RID: 17422 RVA: 0x0006FF81 File Offset: 0x0006E181
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002641 RID: 9793
			// (set) Token: 0x0600440F RID: 17423 RVA: 0x0006FF99 File Offset: 0x0006E199
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002642 RID: 9794
			// (set) Token: 0x06004410 RID: 17424 RVA: 0x0006FFB1 File Offset: 0x0006E1B1
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
