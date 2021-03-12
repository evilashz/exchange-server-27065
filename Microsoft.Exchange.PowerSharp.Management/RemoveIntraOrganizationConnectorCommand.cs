using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006D9 RID: 1753
	public class RemoveIntraOrganizationConnectorCommand : SyntheticCommandWithPipelineInput<IntraOrganizationConnector, IntraOrganizationConnector>
	{
		// Token: 0x06005B8A RID: 23434 RVA: 0x0008E6F8 File Offset: 0x0008C8F8
		private RemoveIntraOrganizationConnectorCommand() : base("Remove-IntraOrganizationConnector")
		{
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x0008E705 File Offset: 0x0008C905
		public RemoveIntraOrganizationConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x0008E714 File Offset: 0x0008C914
		public virtual RemoveIntraOrganizationConnectorCommand SetParameters(RemoveIntraOrganizationConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x0008E71E File Offset: 0x0008C91E
		public virtual RemoveIntraOrganizationConnectorCommand SetParameters(RemoveIntraOrganizationConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006DA RID: 1754
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003991 RID: 14737
			// (set) Token: 0x06005B8E RID: 23438 RVA: 0x0008E728 File Offset: 0x0008C928
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003992 RID: 14738
			// (set) Token: 0x06005B8F RID: 23439 RVA: 0x0008E73B File Offset: 0x0008C93B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003993 RID: 14739
			// (set) Token: 0x06005B90 RID: 23440 RVA: 0x0008E753 File Offset: 0x0008C953
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003994 RID: 14740
			// (set) Token: 0x06005B91 RID: 23441 RVA: 0x0008E76B File Offset: 0x0008C96B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003995 RID: 14741
			// (set) Token: 0x06005B92 RID: 23442 RVA: 0x0008E783 File Offset: 0x0008C983
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003996 RID: 14742
			// (set) Token: 0x06005B93 RID: 23443 RVA: 0x0008E79B File Offset: 0x0008C99B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003997 RID: 14743
			// (set) Token: 0x06005B94 RID: 23444 RVA: 0x0008E7B3 File Offset: 0x0008C9B3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020006DB RID: 1755
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003998 RID: 14744
			// (set) Token: 0x06005B96 RID: 23446 RVA: 0x0008E7D3 File Offset: 0x0008C9D3
			public virtual IntraOrganizationConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003999 RID: 14745
			// (set) Token: 0x06005B97 RID: 23447 RVA: 0x0008E7E6 File Offset: 0x0008C9E6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700399A RID: 14746
			// (set) Token: 0x06005B98 RID: 23448 RVA: 0x0008E7F9 File Offset: 0x0008C9F9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700399B RID: 14747
			// (set) Token: 0x06005B99 RID: 23449 RVA: 0x0008E811 File Offset: 0x0008CA11
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700399C RID: 14748
			// (set) Token: 0x06005B9A RID: 23450 RVA: 0x0008E829 File Offset: 0x0008CA29
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700399D RID: 14749
			// (set) Token: 0x06005B9B RID: 23451 RVA: 0x0008E841 File Offset: 0x0008CA41
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700399E RID: 14750
			// (set) Token: 0x06005B9C RID: 23452 RVA: 0x0008E859 File Offset: 0x0008CA59
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700399F RID: 14751
			// (set) Token: 0x06005B9D RID: 23453 RVA: 0x0008E871 File Offset: 0x0008CA71
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
