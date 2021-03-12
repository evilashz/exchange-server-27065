using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004B4 RID: 1204
	public class NewGlobalAddressListCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600432B RID: 17195 RVA: 0x0006EDEE File Offset: 0x0006CFEE
		private NewGlobalAddressListCommand() : base("New-GlobalAddressList")
		{
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x0006EDFB File Offset: 0x0006CFFB
		public NewGlobalAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0006EE0A File Offset: 0x0006D00A
		public virtual NewGlobalAddressListCommand SetParameters(NewGlobalAddressListCommand.CustomFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0006EE14 File Offset: 0x0006D014
		public virtual NewGlobalAddressListCommand SetParameters(NewGlobalAddressListCommand.PrecannedFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x0006EE1E File Offset: 0x0006D01E
		public virtual NewGlobalAddressListCommand SetParameters(NewGlobalAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004B5 RID: 1205
		public class CustomFilterParameters : ParametersBase
		{
			// Token: 0x1700257C RID: 9596
			// (set) Token: 0x06004330 RID: 17200 RVA: 0x0006EE28 File Offset: 0x0006D028
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x1700257D RID: 9597
			// (set) Token: 0x06004331 RID: 17201 RVA: 0x0006EE3B File Offset: 0x0006D03B
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700257E RID: 9598
			// (set) Token: 0x06004332 RID: 17202 RVA: 0x0006EE59 File Offset: 0x0006D059
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700257F RID: 9599
			// (set) Token: 0x06004333 RID: 17203 RVA: 0x0006EE77 File Offset: 0x0006D077
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002580 RID: 9600
			// (set) Token: 0x06004334 RID: 17204 RVA: 0x0006EE8A File Offset: 0x0006D08A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002581 RID: 9601
			// (set) Token: 0x06004335 RID: 17205 RVA: 0x0006EE9D File Offset: 0x0006D09D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002582 RID: 9602
			// (set) Token: 0x06004336 RID: 17206 RVA: 0x0006EEB5 File Offset: 0x0006D0B5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002583 RID: 9603
			// (set) Token: 0x06004337 RID: 17207 RVA: 0x0006EECD File Offset: 0x0006D0CD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002584 RID: 9604
			// (set) Token: 0x06004338 RID: 17208 RVA: 0x0006EEE5 File Offset: 0x0006D0E5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002585 RID: 9605
			// (set) Token: 0x06004339 RID: 17209 RVA: 0x0006EEFD File Offset: 0x0006D0FD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004B6 RID: 1206
		public class PrecannedFilterParameters : ParametersBase
		{
			// Token: 0x17002586 RID: 9606
			// (set) Token: 0x0600433B RID: 17211 RVA: 0x0006EF1D File Offset: 0x0006D11D
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x17002587 RID: 9607
			// (set) Token: 0x0600433C RID: 17212 RVA: 0x0006EF35 File Offset: 0x0006D135
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x17002588 RID: 9608
			// (set) Token: 0x0600433D RID: 17213 RVA: 0x0006EF48 File Offset: 0x0006D148
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x17002589 RID: 9609
			// (set) Token: 0x0600433E RID: 17214 RVA: 0x0006EF5B File Offset: 0x0006D15B
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x1700258A RID: 9610
			// (set) Token: 0x0600433F RID: 17215 RVA: 0x0006EF6E File Offset: 0x0006D16E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700258B RID: 9611
			// (set) Token: 0x06004340 RID: 17216 RVA: 0x0006EF81 File Offset: 0x0006D181
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700258C RID: 9612
			// (set) Token: 0x06004341 RID: 17217 RVA: 0x0006EF94 File Offset: 0x0006D194
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700258D RID: 9613
			// (set) Token: 0x06004342 RID: 17218 RVA: 0x0006EFA7 File Offset: 0x0006D1A7
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700258E RID: 9614
			// (set) Token: 0x06004343 RID: 17219 RVA: 0x0006EFBA File Offset: 0x0006D1BA
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700258F RID: 9615
			// (set) Token: 0x06004344 RID: 17220 RVA: 0x0006EFCD File Offset: 0x0006D1CD
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x17002590 RID: 9616
			// (set) Token: 0x06004345 RID: 17221 RVA: 0x0006EFE0 File Offset: 0x0006D1E0
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x17002591 RID: 9617
			// (set) Token: 0x06004346 RID: 17222 RVA: 0x0006EFF3 File Offset: 0x0006D1F3
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x17002592 RID: 9618
			// (set) Token: 0x06004347 RID: 17223 RVA: 0x0006F006 File Offset: 0x0006D206
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x17002593 RID: 9619
			// (set) Token: 0x06004348 RID: 17224 RVA: 0x0006F019 File Offset: 0x0006D219
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x17002594 RID: 9620
			// (set) Token: 0x06004349 RID: 17225 RVA: 0x0006F02C File Offset: 0x0006D22C
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x17002595 RID: 9621
			// (set) Token: 0x0600434A RID: 17226 RVA: 0x0006F03F File Offset: 0x0006D23F
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x17002596 RID: 9622
			// (set) Token: 0x0600434B RID: 17227 RVA: 0x0006F052 File Offset: 0x0006D252
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x17002597 RID: 9623
			// (set) Token: 0x0600434C RID: 17228 RVA: 0x0006F065 File Offset: 0x0006D265
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x17002598 RID: 9624
			// (set) Token: 0x0600434D RID: 17229 RVA: 0x0006F078 File Offset: 0x0006D278
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17002599 RID: 9625
			// (set) Token: 0x0600434E RID: 17230 RVA: 0x0006F08B File Offset: 0x0006D28B
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700259A RID: 9626
			// (set) Token: 0x0600434F RID: 17231 RVA: 0x0006F0A9 File Offset: 0x0006D2A9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700259B RID: 9627
			// (set) Token: 0x06004350 RID: 17232 RVA: 0x0006F0C7 File Offset: 0x0006D2C7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700259C RID: 9628
			// (set) Token: 0x06004351 RID: 17233 RVA: 0x0006F0DA File Offset: 0x0006D2DA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700259D RID: 9629
			// (set) Token: 0x06004352 RID: 17234 RVA: 0x0006F0ED File Offset: 0x0006D2ED
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700259E RID: 9630
			// (set) Token: 0x06004353 RID: 17235 RVA: 0x0006F105 File Offset: 0x0006D305
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700259F RID: 9631
			// (set) Token: 0x06004354 RID: 17236 RVA: 0x0006F11D File Offset: 0x0006D31D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170025A0 RID: 9632
			// (set) Token: 0x06004355 RID: 17237 RVA: 0x0006F135 File Offset: 0x0006D335
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170025A1 RID: 9633
			// (set) Token: 0x06004356 RID: 17238 RVA: 0x0006F14D File Offset: 0x0006D34D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004B7 RID: 1207
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170025A2 RID: 9634
			// (set) Token: 0x06004358 RID: 17240 RVA: 0x0006F16D File Offset: 0x0006D36D
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170025A3 RID: 9635
			// (set) Token: 0x06004359 RID: 17241 RVA: 0x0006F18B File Offset: 0x0006D38B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170025A4 RID: 9636
			// (set) Token: 0x0600435A RID: 17242 RVA: 0x0006F1A9 File Offset: 0x0006D3A9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170025A5 RID: 9637
			// (set) Token: 0x0600435B RID: 17243 RVA: 0x0006F1BC File Offset: 0x0006D3BC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170025A6 RID: 9638
			// (set) Token: 0x0600435C RID: 17244 RVA: 0x0006F1CF File Offset: 0x0006D3CF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170025A7 RID: 9639
			// (set) Token: 0x0600435D RID: 17245 RVA: 0x0006F1E7 File Offset: 0x0006D3E7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170025A8 RID: 9640
			// (set) Token: 0x0600435E RID: 17246 RVA: 0x0006F1FF File Offset: 0x0006D3FF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170025A9 RID: 9641
			// (set) Token: 0x0600435F RID: 17247 RVA: 0x0006F217 File Offset: 0x0006D417
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170025AA RID: 9642
			// (set) Token: 0x06004360 RID: 17248 RVA: 0x0006F22F File Offset: 0x0006D42F
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
