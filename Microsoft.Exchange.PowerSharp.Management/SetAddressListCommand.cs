using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004BE RID: 1214
	public class SetAddressListCommand : SyntheticCommandWithPipelineInputNoOutput<AddressList>
	{
		// Token: 0x0600438E RID: 17294 RVA: 0x0006F5B1 File Offset: 0x0006D7B1
		private SetAddressListCommand() : base("Set-AddressList")
		{
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x0006F5BE File Offset: 0x0006D7BE
		public SetAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x0006F5CD File Offset: 0x0006D7CD
		public virtual SetAddressListCommand SetParameters(SetAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x0006F5D7 File Offset: 0x0006D7D7
		public virtual SetAddressListCommand SetParameters(SetAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004BF RID: 1215
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170025CB RID: 9675
			// (set) Token: 0x06004392 RID: 17298 RVA: 0x0006F5E1 File Offset: 0x0006D7E1
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x170025CC RID: 9676
			// (set) Token: 0x06004393 RID: 17299 RVA: 0x0006F5F4 File Offset: 0x0006D7F4
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170025CD RID: 9677
			// (set) Token: 0x06004394 RID: 17300 RVA: 0x0006F612 File Offset: 0x0006D812
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x170025CE RID: 9678
			// (set) Token: 0x06004395 RID: 17301 RVA: 0x0006F62A File Offset: 0x0006D82A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170025CF RID: 9679
			// (set) Token: 0x06004396 RID: 17302 RVA: 0x0006F63D File Offset: 0x0006D83D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170025D0 RID: 9680
			// (set) Token: 0x06004397 RID: 17303 RVA: 0x0006F650 File Offset: 0x0006D850
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170025D1 RID: 9681
			// (set) Token: 0x06004398 RID: 17304 RVA: 0x0006F663 File Offset: 0x0006D863
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x170025D2 RID: 9682
			// (set) Token: 0x06004399 RID: 17305 RVA: 0x0006F67B File Offset: 0x0006D87B
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x170025D3 RID: 9683
			// (set) Token: 0x0600439A RID: 17306 RVA: 0x0006F68E File Offset: 0x0006D88E
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x170025D4 RID: 9684
			// (set) Token: 0x0600439B RID: 17307 RVA: 0x0006F6A1 File Offset: 0x0006D8A1
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x170025D5 RID: 9685
			// (set) Token: 0x0600439C RID: 17308 RVA: 0x0006F6B4 File Offset: 0x0006D8B4
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x170025D6 RID: 9686
			// (set) Token: 0x0600439D RID: 17309 RVA: 0x0006F6C7 File Offset: 0x0006D8C7
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x170025D7 RID: 9687
			// (set) Token: 0x0600439E RID: 17310 RVA: 0x0006F6DA File Offset: 0x0006D8DA
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x170025D8 RID: 9688
			// (set) Token: 0x0600439F RID: 17311 RVA: 0x0006F6ED File Offset: 0x0006D8ED
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x170025D9 RID: 9689
			// (set) Token: 0x060043A0 RID: 17312 RVA: 0x0006F700 File Offset: 0x0006D900
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x170025DA RID: 9690
			// (set) Token: 0x060043A1 RID: 17313 RVA: 0x0006F713 File Offset: 0x0006D913
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x170025DB RID: 9691
			// (set) Token: 0x060043A2 RID: 17314 RVA: 0x0006F726 File Offset: 0x0006D926
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x170025DC RID: 9692
			// (set) Token: 0x060043A3 RID: 17315 RVA: 0x0006F739 File Offset: 0x0006D939
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x170025DD RID: 9693
			// (set) Token: 0x060043A4 RID: 17316 RVA: 0x0006F74C File Offset: 0x0006D94C
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x170025DE RID: 9694
			// (set) Token: 0x060043A5 RID: 17317 RVA: 0x0006F75F File Offset: 0x0006D95F
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x170025DF RID: 9695
			// (set) Token: 0x060043A6 RID: 17318 RVA: 0x0006F772 File Offset: 0x0006D972
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x170025E0 RID: 9696
			// (set) Token: 0x060043A7 RID: 17319 RVA: 0x0006F785 File Offset: 0x0006D985
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x170025E1 RID: 9697
			// (set) Token: 0x060043A8 RID: 17320 RVA: 0x0006F798 File Offset: 0x0006D998
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x170025E2 RID: 9698
			// (set) Token: 0x060043A9 RID: 17321 RVA: 0x0006F7AB File Offset: 0x0006D9AB
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x170025E3 RID: 9699
			// (set) Token: 0x060043AA RID: 17322 RVA: 0x0006F7BE File Offset: 0x0006D9BE
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x170025E4 RID: 9700
			// (set) Token: 0x060043AB RID: 17323 RVA: 0x0006F7D1 File Offset: 0x0006D9D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170025E5 RID: 9701
			// (set) Token: 0x060043AC RID: 17324 RVA: 0x0006F7E9 File Offset: 0x0006D9E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170025E6 RID: 9702
			// (set) Token: 0x060043AD RID: 17325 RVA: 0x0006F801 File Offset: 0x0006DA01
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170025E7 RID: 9703
			// (set) Token: 0x060043AE RID: 17326 RVA: 0x0006F819 File Offset: 0x0006DA19
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170025E8 RID: 9704
			// (set) Token: 0x060043AF RID: 17327 RVA: 0x0006F831 File Offset: 0x0006DA31
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004C0 RID: 1216
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170025E9 RID: 9705
			// (set) Token: 0x060043B1 RID: 17329 RVA: 0x0006F851 File Offset: 0x0006DA51
			public virtual AddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170025EA RID: 9706
			// (set) Token: 0x060043B2 RID: 17330 RVA: 0x0006F864 File Offset: 0x0006DA64
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x170025EB RID: 9707
			// (set) Token: 0x060043B3 RID: 17331 RVA: 0x0006F877 File Offset: 0x0006DA77
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170025EC RID: 9708
			// (set) Token: 0x060043B4 RID: 17332 RVA: 0x0006F895 File Offset: 0x0006DA95
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x170025ED RID: 9709
			// (set) Token: 0x060043B5 RID: 17333 RVA: 0x0006F8AD File Offset: 0x0006DAAD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170025EE RID: 9710
			// (set) Token: 0x060043B6 RID: 17334 RVA: 0x0006F8C0 File Offset: 0x0006DAC0
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170025EF RID: 9711
			// (set) Token: 0x060043B7 RID: 17335 RVA: 0x0006F8D3 File Offset: 0x0006DAD3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170025F0 RID: 9712
			// (set) Token: 0x060043B8 RID: 17336 RVA: 0x0006F8E6 File Offset: 0x0006DAE6
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x170025F1 RID: 9713
			// (set) Token: 0x060043B9 RID: 17337 RVA: 0x0006F8FE File Offset: 0x0006DAFE
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x170025F2 RID: 9714
			// (set) Token: 0x060043BA RID: 17338 RVA: 0x0006F911 File Offset: 0x0006DB11
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x170025F3 RID: 9715
			// (set) Token: 0x060043BB RID: 17339 RVA: 0x0006F924 File Offset: 0x0006DB24
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x170025F4 RID: 9716
			// (set) Token: 0x060043BC RID: 17340 RVA: 0x0006F937 File Offset: 0x0006DB37
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x170025F5 RID: 9717
			// (set) Token: 0x060043BD RID: 17341 RVA: 0x0006F94A File Offset: 0x0006DB4A
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x170025F6 RID: 9718
			// (set) Token: 0x060043BE RID: 17342 RVA: 0x0006F95D File Offset: 0x0006DB5D
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x170025F7 RID: 9719
			// (set) Token: 0x060043BF RID: 17343 RVA: 0x0006F970 File Offset: 0x0006DB70
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x170025F8 RID: 9720
			// (set) Token: 0x060043C0 RID: 17344 RVA: 0x0006F983 File Offset: 0x0006DB83
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x170025F9 RID: 9721
			// (set) Token: 0x060043C1 RID: 17345 RVA: 0x0006F996 File Offset: 0x0006DB96
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x170025FA RID: 9722
			// (set) Token: 0x060043C2 RID: 17346 RVA: 0x0006F9A9 File Offset: 0x0006DBA9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x170025FB RID: 9723
			// (set) Token: 0x060043C3 RID: 17347 RVA: 0x0006F9BC File Offset: 0x0006DBBC
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x170025FC RID: 9724
			// (set) Token: 0x060043C4 RID: 17348 RVA: 0x0006F9CF File Offset: 0x0006DBCF
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x170025FD RID: 9725
			// (set) Token: 0x060043C5 RID: 17349 RVA: 0x0006F9E2 File Offset: 0x0006DBE2
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x170025FE RID: 9726
			// (set) Token: 0x060043C6 RID: 17350 RVA: 0x0006F9F5 File Offset: 0x0006DBF5
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x170025FF RID: 9727
			// (set) Token: 0x060043C7 RID: 17351 RVA: 0x0006FA08 File Offset: 0x0006DC08
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x17002600 RID: 9728
			// (set) Token: 0x060043C8 RID: 17352 RVA: 0x0006FA1B File Offset: 0x0006DC1B
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x17002601 RID: 9729
			// (set) Token: 0x060043C9 RID: 17353 RVA: 0x0006FA2E File Offset: 0x0006DC2E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x17002602 RID: 9730
			// (set) Token: 0x060043CA RID: 17354 RVA: 0x0006FA41 File Offset: 0x0006DC41
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17002603 RID: 9731
			// (set) Token: 0x060043CB RID: 17355 RVA: 0x0006FA54 File Offset: 0x0006DC54
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002604 RID: 9732
			// (set) Token: 0x060043CC RID: 17356 RVA: 0x0006FA6C File Offset: 0x0006DC6C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002605 RID: 9733
			// (set) Token: 0x060043CD RID: 17357 RVA: 0x0006FA84 File Offset: 0x0006DC84
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002606 RID: 9734
			// (set) Token: 0x060043CE RID: 17358 RVA: 0x0006FA9C File Offset: 0x0006DC9C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002607 RID: 9735
			// (set) Token: 0x060043CF RID: 17359 RVA: 0x0006FAB4 File Offset: 0x0006DCB4
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
