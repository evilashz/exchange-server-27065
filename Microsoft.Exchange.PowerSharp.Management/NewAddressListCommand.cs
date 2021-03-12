using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004B0 RID: 1200
	public class NewAddressListCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x060042EE RID: 17134 RVA: 0x0006E91B File Offset: 0x0006CB1B
		private NewAddressListCommand() : base("New-AddressList")
		{
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0006E928 File Offset: 0x0006CB28
		public NewAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0006E937 File Offset: 0x0006CB37
		public virtual NewAddressListCommand SetParameters(NewAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0006E941 File Offset: 0x0006CB41
		public virtual NewAddressListCommand SetParameters(NewAddressListCommand.CustomFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0006E94B File Offset: 0x0006CB4B
		public virtual NewAddressListCommand SetParameters(NewAddressListCommand.PrecannedFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004B1 RID: 1201
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002547 RID: 9543
			// (set) Token: 0x060042F3 RID: 17139 RVA: 0x0006E955 File Offset: 0x0006CB55
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17002548 RID: 9544
			// (set) Token: 0x060042F4 RID: 17140 RVA: 0x0006E968 File Offset: 0x0006CB68
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002549 RID: 9545
			// (set) Token: 0x060042F5 RID: 17141 RVA: 0x0006E97B File Offset: 0x0006CB7B
			public virtual AddressListIdParameter Container
			{
				set
				{
					base.PowerSharpParameters["Container"] = value;
				}
			}

			// Token: 0x1700254A RID: 9546
			// (set) Token: 0x060042F6 RID: 17142 RVA: 0x0006E98E File Offset: 0x0006CB8E
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700254B RID: 9547
			// (set) Token: 0x060042F7 RID: 17143 RVA: 0x0006E9AC File Offset: 0x0006CBAC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700254C RID: 9548
			// (set) Token: 0x060042F8 RID: 17144 RVA: 0x0006E9CA File Offset: 0x0006CBCA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700254D RID: 9549
			// (set) Token: 0x060042F9 RID: 17145 RVA: 0x0006E9DD File Offset: 0x0006CBDD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700254E RID: 9550
			// (set) Token: 0x060042FA RID: 17146 RVA: 0x0006E9F5 File Offset: 0x0006CBF5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700254F RID: 9551
			// (set) Token: 0x060042FB RID: 17147 RVA: 0x0006EA0D File Offset: 0x0006CC0D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002550 RID: 9552
			// (set) Token: 0x060042FC RID: 17148 RVA: 0x0006EA25 File Offset: 0x0006CC25
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002551 RID: 9553
			// (set) Token: 0x060042FD RID: 17149 RVA: 0x0006EA3D File Offset: 0x0006CC3D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004B2 RID: 1202
		public class CustomFilterParameters : ParametersBase
		{
			// Token: 0x17002552 RID: 9554
			// (set) Token: 0x060042FF RID: 17151 RVA: 0x0006EA5D File Offset: 0x0006CC5D
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x17002553 RID: 9555
			// (set) Token: 0x06004300 RID: 17152 RVA: 0x0006EA70 File Offset: 0x0006CC70
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17002554 RID: 9556
			// (set) Token: 0x06004301 RID: 17153 RVA: 0x0006EA83 File Offset: 0x0006CC83
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002555 RID: 9557
			// (set) Token: 0x06004302 RID: 17154 RVA: 0x0006EA96 File Offset: 0x0006CC96
			public virtual AddressListIdParameter Container
			{
				set
				{
					base.PowerSharpParameters["Container"] = value;
				}
			}

			// Token: 0x17002556 RID: 9558
			// (set) Token: 0x06004303 RID: 17155 RVA: 0x0006EAA9 File Offset: 0x0006CCA9
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17002557 RID: 9559
			// (set) Token: 0x06004304 RID: 17156 RVA: 0x0006EAC7 File Offset: 0x0006CCC7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002558 RID: 9560
			// (set) Token: 0x06004305 RID: 17157 RVA: 0x0006EAE5 File Offset: 0x0006CCE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002559 RID: 9561
			// (set) Token: 0x06004306 RID: 17158 RVA: 0x0006EAF8 File Offset: 0x0006CCF8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700255A RID: 9562
			// (set) Token: 0x06004307 RID: 17159 RVA: 0x0006EB10 File Offset: 0x0006CD10
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700255B RID: 9563
			// (set) Token: 0x06004308 RID: 17160 RVA: 0x0006EB28 File Offset: 0x0006CD28
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700255C RID: 9564
			// (set) Token: 0x06004309 RID: 17161 RVA: 0x0006EB40 File Offset: 0x0006CD40
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700255D RID: 9565
			// (set) Token: 0x0600430A RID: 17162 RVA: 0x0006EB58 File Offset: 0x0006CD58
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004B3 RID: 1203
		public class PrecannedFilterParameters : ParametersBase
		{
			// Token: 0x1700255E RID: 9566
			// (set) Token: 0x0600430C RID: 17164 RVA: 0x0006EB78 File Offset: 0x0006CD78
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x1700255F RID: 9567
			// (set) Token: 0x0600430D RID: 17165 RVA: 0x0006EB90 File Offset: 0x0006CD90
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x17002560 RID: 9568
			// (set) Token: 0x0600430E RID: 17166 RVA: 0x0006EBA3 File Offset: 0x0006CDA3
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x17002561 RID: 9569
			// (set) Token: 0x0600430F RID: 17167 RVA: 0x0006EBB6 File Offset: 0x0006CDB6
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x17002562 RID: 9570
			// (set) Token: 0x06004310 RID: 17168 RVA: 0x0006EBC9 File Offset: 0x0006CDC9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x17002563 RID: 9571
			// (set) Token: 0x06004311 RID: 17169 RVA: 0x0006EBDC File Offset: 0x0006CDDC
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x17002564 RID: 9572
			// (set) Token: 0x06004312 RID: 17170 RVA: 0x0006EBEF File Offset: 0x0006CDEF
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x17002565 RID: 9573
			// (set) Token: 0x06004313 RID: 17171 RVA: 0x0006EC02 File Offset: 0x0006CE02
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x17002566 RID: 9574
			// (set) Token: 0x06004314 RID: 17172 RVA: 0x0006EC15 File Offset: 0x0006CE15
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x17002567 RID: 9575
			// (set) Token: 0x06004315 RID: 17173 RVA: 0x0006EC28 File Offset: 0x0006CE28
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x17002568 RID: 9576
			// (set) Token: 0x06004316 RID: 17174 RVA: 0x0006EC3B File Offset: 0x0006CE3B
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x17002569 RID: 9577
			// (set) Token: 0x06004317 RID: 17175 RVA: 0x0006EC4E File Offset: 0x0006CE4E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x1700256A RID: 9578
			// (set) Token: 0x06004318 RID: 17176 RVA: 0x0006EC61 File Offset: 0x0006CE61
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x1700256B RID: 9579
			// (set) Token: 0x06004319 RID: 17177 RVA: 0x0006EC74 File Offset: 0x0006CE74
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x1700256C RID: 9580
			// (set) Token: 0x0600431A RID: 17178 RVA: 0x0006EC87 File Offset: 0x0006CE87
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x1700256D RID: 9581
			// (set) Token: 0x0600431B RID: 17179 RVA: 0x0006EC9A File Offset: 0x0006CE9A
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x1700256E RID: 9582
			// (set) Token: 0x0600431C RID: 17180 RVA: 0x0006ECAD File Offset: 0x0006CEAD
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x1700256F RID: 9583
			// (set) Token: 0x0600431D RID: 17181 RVA: 0x0006ECC0 File Offset: 0x0006CEC0
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x17002570 RID: 9584
			// (set) Token: 0x0600431E RID: 17182 RVA: 0x0006ECD3 File Offset: 0x0006CED3
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17002571 RID: 9585
			// (set) Token: 0x0600431F RID: 17183 RVA: 0x0006ECE6 File Offset: 0x0006CEE6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17002572 RID: 9586
			// (set) Token: 0x06004320 RID: 17184 RVA: 0x0006ECF9 File Offset: 0x0006CEF9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002573 RID: 9587
			// (set) Token: 0x06004321 RID: 17185 RVA: 0x0006ED0C File Offset: 0x0006CF0C
			public virtual AddressListIdParameter Container
			{
				set
				{
					base.PowerSharpParameters["Container"] = value;
				}
			}

			// Token: 0x17002574 RID: 9588
			// (set) Token: 0x06004322 RID: 17186 RVA: 0x0006ED1F File Offset: 0x0006CF1F
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17002575 RID: 9589
			// (set) Token: 0x06004323 RID: 17187 RVA: 0x0006ED3D File Offset: 0x0006CF3D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002576 RID: 9590
			// (set) Token: 0x06004324 RID: 17188 RVA: 0x0006ED5B File Offset: 0x0006CF5B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002577 RID: 9591
			// (set) Token: 0x06004325 RID: 17189 RVA: 0x0006ED6E File Offset: 0x0006CF6E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002578 RID: 9592
			// (set) Token: 0x06004326 RID: 17190 RVA: 0x0006ED86 File Offset: 0x0006CF86
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002579 RID: 9593
			// (set) Token: 0x06004327 RID: 17191 RVA: 0x0006ED9E File Offset: 0x0006CF9E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700257A RID: 9594
			// (set) Token: 0x06004328 RID: 17192 RVA: 0x0006EDB6 File Offset: 0x0006CFB6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700257B RID: 9595
			// (set) Token: 0x06004329 RID: 17193 RVA: 0x0006EDCE File Offset: 0x0006CFCE
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
