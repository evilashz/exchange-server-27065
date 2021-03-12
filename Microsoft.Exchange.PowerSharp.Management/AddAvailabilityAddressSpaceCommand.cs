using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004E3 RID: 1251
	public class AddAvailabilityAddressSpaceCommand : SyntheticCommandWithPipelineInput<AvailabilityAddressSpace, AvailabilityAddressSpace>
	{
		// Token: 0x060044EB RID: 17643 RVA: 0x00071054 File Offset: 0x0006F254
		private AddAvailabilityAddressSpaceCommand() : base("Add-AvailabilityAddressSpace")
		{
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x00071061 File Offset: 0x0006F261
		public AddAvailabilityAddressSpaceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x00071070 File Offset: 0x0006F270
		public virtual AddAvailabilityAddressSpaceCommand SetParameters(AddAvailabilityAddressSpaceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004E4 RID: 1252
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170026DE RID: 9950
			// (set) Token: 0x060044EE RID: 17646 RVA: 0x0007107A File Offset: 0x0006F27A
			public virtual string ForestName
			{
				set
				{
					base.PowerSharpParameters["ForestName"] = value;
				}
			}

			// Token: 0x170026DF RID: 9951
			// (set) Token: 0x060044EF RID: 17647 RVA: 0x0007108D File Offset: 0x0006F28D
			public virtual AvailabilityAccessMethod AccessMethod
			{
				set
				{
					base.PowerSharpParameters["AccessMethod"] = value;
				}
			}

			// Token: 0x170026E0 RID: 9952
			// (set) Token: 0x060044F0 RID: 17648 RVA: 0x000710A5 File Offset: 0x0006F2A5
			public virtual bool UseServiceAccount
			{
				set
				{
					base.PowerSharpParameters["UseServiceAccount"] = value;
				}
			}

			// Token: 0x170026E1 RID: 9953
			// (set) Token: 0x060044F1 RID: 17649 RVA: 0x000710BD File Offset: 0x0006F2BD
			public virtual PSCredential Credentials
			{
				set
				{
					base.PowerSharpParameters["Credentials"] = value;
				}
			}

			// Token: 0x170026E2 RID: 9954
			// (set) Token: 0x060044F2 RID: 17650 RVA: 0x000710D0 File Offset: 0x0006F2D0
			public virtual Uri ProxyUrl
			{
				set
				{
					base.PowerSharpParameters["ProxyUrl"] = value;
				}
			}

			// Token: 0x170026E3 RID: 9955
			// (set) Token: 0x060044F3 RID: 17651 RVA: 0x000710E3 File Offset: 0x0006F2E3
			public virtual Uri TargetAutodiscoverEpr
			{
				set
				{
					base.PowerSharpParameters["TargetAutodiscoverEpr"] = value;
				}
			}

			// Token: 0x170026E4 RID: 9956
			// (set) Token: 0x060044F4 RID: 17652 RVA: 0x000710F6 File Offset: 0x0006F2F6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170026E5 RID: 9957
			// (set) Token: 0x060044F5 RID: 17653 RVA: 0x00071114 File Offset: 0x0006F314
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026E6 RID: 9958
			// (set) Token: 0x060044F6 RID: 17654 RVA: 0x00071127 File Offset: 0x0006F327
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026E7 RID: 9959
			// (set) Token: 0x060044F7 RID: 17655 RVA: 0x0007113F File Offset: 0x0006F33F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026E8 RID: 9960
			// (set) Token: 0x060044F8 RID: 17656 RVA: 0x00071157 File Offset: 0x0006F357
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026E9 RID: 9961
			// (set) Token: 0x060044F9 RID: 17657 RVA: 0x0007116F File Offset: 0x0006F36F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170026EA RID: 9962
			// (set) Token: 0x060044FA RID: 17658 RVA: 0x00071187 File Offset: 0x0006F387
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
