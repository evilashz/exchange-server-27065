using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004AC RID: 1196
	public class GetGlobalAddressListCommand : SyntheticCommandWithPipelineInput<AddressBookBase, AddressBookBase>
	{
		// Token: 0x060042D2 RID: 17106 RVA: 0x0006E6EB File Offset: 0x0006C8EB
		private GetGlobalAddressListCommand() : base("Get-GlobalAddressList")
		{
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		public GetGlobalAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0006E707 File Offset: 0x0006C907
		public virtual GetGlobalAddressListCommand SetParameters(GetGlobalAddressListCommand.DefaultOnlyParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0006E711 File Offset: 0x0006C911
		public virtual GetGlobalAddressListCommand SetParameters(GetGlobalAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0006E71B File Offset: 0x0006C91B
		public virtual GetGlobalAddressListCommand SetParameters(GetGlobalAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004AD RID: 1197
		public class DefaultOnlyParameters : ParametersBase
		{
			// Token: 0x17002533 RID: 9523
			// (set) Token: 0x060042D7 RID: 17111 RVA: 0x0006E725 File Offset: 0x0006C925
			public virtual SwitchParameter DefaultOnly
			{
				set
				{
					base.PowerSharpParameters["DefaultOnly"] = value;
				}
			}

			// Token: 0x17002534 RID: 9524
			// (set) Token: 0x060042D8 RID: 17112 RVA: 0x0006E73D File Offset: 0x0006C93D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002535 RID: 9525
			// (set) Token: 0x060042D9 RID: 17113 RVA: 0x0006E75B File Offset: 0x0006C95B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002536 RID: 9526
			// (set) Token: 0x060042DA RID: 17114 RVA: 0x0006E76E File Offset: 0x0006C96E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002537 RID: 9527
			// (set) Token: 0x060042DB RID: 17115 RVA: 0x0006E786 File Offset: 0x0006C986
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002538 RID: 9528
			// (set) Token: 0x060042DC RID: 17116 RVA: 0x0006E79E File Offset: 0x0006C99E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002539 RID: 9529
			// (set) Token: 0x060042DD RID: 17117 RVA: 0x0006E7B6 File Offset: 0x0006C9B6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004AE RID: 1198
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700253A RID: 9530
			// (set) Token: 0x060042DF RID: 17119 RVA: 0x0006E7D6 File Offset: 0x0006C9D6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700253B RID: 9531
			// (set) Token: 0x060042E0 RID: 17120 RVA: 0x0006E7F4 File Offset: 0x0006C9F4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700253C RID: 9532
			// (set) Token: 0x060042E1 RID: 17121 RVA: 0x0006E807 File Offset: 0x0006CA07
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700253D RID: 9533
			// (set) Token: 0x060042E2 RID: 17122 RVA: 0x0006E81F File Offset: 0x0006CA1F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700253E RID: 9534
			// (set) Token: 0x060042E3 RID: 17123 RVA: 0x0006E837 File Offset: 0x0006CA37
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700253F RID: 9535
			// (set) Token: 0x060042E4 RID: 17124 RVA: 0x0006E84F File Offset: 0x0006CA4F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004AF RID: 1199
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002540 RID: 9536
			// (set) Token: 0x060042E6 RID: 17126 RVA: 0x0006E86F File Offset: 0x0006CA6F
			public virtual GlobalAddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002541 RID: 9537
			// (set) Token: 0x060042E7 RID: 17127 RVA: 0x0006E882 File Offset: 0x0006CA82
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002542 RID: 9538
			// (set) Token: 0x060042E8 RID: 17128 RVA: 0x0006E8A0 File Offset: 0x0006CAA0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002543 RID: 9539
			// (set) Token: 0x060042E9 RID: 17129 RVA: 0x0006E8B3 File Offset: 0x0006CAB3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002544 RID: 9540
			// (set) Token: 0x060042EA RID: 17130 RVA: 0x0006E8CB File Offset: 0x0006CACB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002545 RID: 9541
			// (set) Token: 0x060042EB RID: 17131 RVA: 0x0006E8E3 File Offset: 0x0006CAE3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002546 RID: 9542
			// (set) Token: 0x060042EC RID: 17132 RVA: 0x0006E8FB File Offset: 0x0006CAFB
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
