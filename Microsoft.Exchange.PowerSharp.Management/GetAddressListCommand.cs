using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004A7 RID: 1191
	public class GetAddressListCommand : SyntheticCommandWithPipelineInput<AddressBookBase, AddressBookBase>
	{
		// Token: 0x060042AD RID: 17069 RVA: 0x0006E40A File Offset: 0x0006C60A
		private GetAddressListCommand() : base("Get-AddressList")
		{
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x0006E417 File Offset: 0x0006C617
		public GetAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x0006E426 File Offset: 0x0006C626
		public virtual GetAddressListCommand SetParameters(GetAddressListCommand.ContainerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x0006E430 File Offset: 0x0006C630
		public virtual GetAddressListCommand SetParameters(GetAddressListCommand.SearchSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x0006E43A File Offset: 0x0006C63A
		public virtual GetAddressListCommand SetParameters(GetAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x0006E444 File Offset: 0x0006C644
		public virtual GetAddressListCommand SetParameters(GetAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004A8 RID: 1192
		public class ContainerParameters : ParametersBase
		{
			// Token: 0x17002518 RID: 9496
			// (set) Token: 0x060042B3 RID: 17075 RVA: 0x0006E44E File Offset: 0x0006C64E
			public virtual AddressListIdParameter Container
			{
				set
				{
					base.PowerSharpParameters["Container"] = value;
				}
			}

			// Token: 0x17002519 RID: 9497
			// (set) Token: 0x060042B4 RID: 17076 RVA: 0x0006E461 File Offset: 0x0006C661
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700251A RID: 9498
			// (set) Token: 0x060042B5 RID: 17077 RVA: 0x0006E47F File Offset: 0x0006C67F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700251B RID: 9499
			// (set) Token: 0x060042B6 RID: 17078 RVA: 0x0006E492 File Offset: 0x0006C692
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700251C RID: 9500
			// (set) Token: 0x060042B7 RID: 17079 RVA: 0x0006E4AA File Offset: 0x0006C6AA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700251D RID: 9501
			// (set) Token: 0x060042B8 RID: 17080 RVA: 0x0006E4C2 File Offset: 0x0006C6C2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700251E RID: 9502
			// (set) Token: 0x060042B9 RID: 17081 RVA: 0x0006E4DA File Offset: 0x0006C6DA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004A9 RID: 1193
		public class SearchSetParameters : ParametersBase
		{
			// Token: 0x1700251F RID: 9503
			// (set) Token: 0x060042BB RID: 17083 RVA: 0x0006E4FA File Offset: 0x0006C6FA
			public virtual string SearchText
			{
				set
				{
					base.PowerSharpParameters["SearchText"] = value;
				}
			}

			// Token: 0x17002520 RID: 9504
			// (set) Token: 0x060042BC RID: 17084 RVA: 0x0006E50D File Offset: 0x0006C70D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002521 RID: 9505
			// (set) Token: 0x060042BD RID: 17085 RVA: 0x0006E52B File Offset: 0x0006C72B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002522 RID: 9506
			// (set) Token: 0x060042BE RID: 17086 RVA: 0x0006E53E File Offset: 0x0006C73E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002523 RID: 9507
			// (set) Token: 0x060042BF RID: 17087 RVA: 0x0006E556 File Offset: 0x0006C756
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002524 RID: 9508
			// (set) Token: 0x060042C0 RID: 17088 RVA: 0x0006E56E File Offset: 0x0006C76E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002525 RID: 9509
			// (set) Token: 0x060042C1 RID: 17089 RVA: 0x0006E586 File Offset: 0x0006C786
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004AA RID: 1194
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002526 RID: 9510
			// (set) Token: 0x060042C3 RID: 17091 RVA: 0x0006E5A6 File Offset: 0x0006C7A6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002527 RID: 9511
			// (set) Token: 0x060042C4 RID: 17092 RVA: 0x0006E5C4 File Offset: 0x0006C7C4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002528 RID: 9512
			// (set) Token: 0x060042C5 RID: 17093 RVA: 0x0006E5D7 File Offset: 0x0006C7D7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002529 RID: 9513
			// (set) Token: 0x060042C6 RID: 17094 RVA: 0x0006E5EF File Offset: 0x0006C7EF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700252A RID: 9514
			// (set) Token: 0x060042C7 RID: 17095 RVA: 0x0006E607 File Offset: 0x0006C807
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700252B RID: 9515
			// (set) Token: 0x060042C8 RID: 17096 RVA: 0x0006E61F File Offset: 0x0006C81F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004AB RID: 1195
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700252C RID: 9516
			// (set) Token: 0x060042CA RID: 17098 RVA: 0x0006E63F File Offset: 0x0006C83F
			public virtual AddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700252D RID: 9517
			// (set) Token: 0x060042CB RID: 17099 RVA: 0x0006E652 File Offset: 0x0006C852
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700252E RID: 9518
			// (set) Token: 0x060042CC RID: 17100 RVA: 0x0006E670 File Offset: 0x0006C870
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700252F RID: 9519
			// (set) Token: 0x060042CD RID: 17101 RVA: 0x0006E683 File Offset: 0x0006C883
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002530 RID: 9520
			// (set) Token: 0x060042CE RID: 17102 RVA: 0x0006E69B File Offset: 0x0006C89B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002531 RID: 9521
			// (set) Token: 0x060042CF RID: 17103 RVA: 0x0006E6B3 File Offset: 0x0006C8B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002532 RID: 9522
			// (set) Token: 0x060042D0 RID: 17104 RVA: 0x0006E6CB File Offset: 0x0006C8CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
