using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000C6 RID: 198
	public class SetSecondaryDomainCommand : SyntheticCommandWithPipelineInputNoOutput<AcceptedDomainIdParameter>
	{
		// Token: 0x06001C8D RID: 7309 RVA: 0x0003CC88 File Offset: 0x0003AE88
		private SetSecondaryDomainCommand() : base("Set-SecondaryDomain")
		{
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0003CC95 File Offset: 0x0003AE95
		public SetSecondaryDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0003CCA4 File Offset: 0x0003AEA4
		public virtual SetSecondaryDomainCommand SetParameters(SetSecondaryDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0003CCAE File Offset: 0x0003AEAE
		public virtual SetSecondaryDomainCommand SetParameters(SetSecondaryDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000C7 RID: 199
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170006BA RID: 1722
			// (set) Token: 0x06001C91 RID: 7313 RVA: 0x0003CCB8 File Offset: 0x0003AEB8
			public virtual AcceptedDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170006BB RID: 1723
			// (set) Token: 0x06001C92 RID: 7314 RVA: 0x0003CCCB File Offset: 0x0003AECB
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170006BC RID: 1724
			// (set) Token: 0x06001C93 RID: 7315 RVA: 0x0003CCE3 File Offset: 0x0003AEE3
			public virtual bool OutBoundOnly
			{
				set
				{
					base.PowerSharpParameters["OutBoundOnly"] = value;
				}
			}

			// Token: 0x170006BD RID: 1725
			// (set) Token: 0x06001C94 RID: 7316 RVA: 0x0003CCFB File Offset: 0x0003AEFB
			public virtual bool MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x170006BE RID: 1726
			// (set) Token: 0x06001C95 RID: 7317 RVA: 0x0003CD13 File Offset: 0x0003AF13
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170006BF RID: 1727
			// (set) Token: 0x06001C96 RID: 7318 RVA: 0x0003CD26 File Offset: 0x0003AF26
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170006C0 RID: 1728
			// (set) Token: 0x06001C97 RID: 7319 RVA: 0x0003CD3E File Offset: 0x0003AF3E
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170006C1 RID: 1729
			// (set) Token: 0x06001C98 RID: 7320 RVA: 0x0003CD56 File Offset: 0x0003AF56
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170006C2 RID: 1730
			// (set) Token: 0x06001C99 RID: 7321 RVA: 0x0003CD6E File Offset: 0x0003AF6E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170006C3 RID: 1731
			// (set) Token: 0x06001C9A RID: 7322 RVA: 0x0003CD86 File Offset: 0x0003AF86
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170006C4 RID: 1732
			// (set) Token: 0x06001C9B RID: 7323 RVA: 0x0003CD9E File Offset: 0x0003AF9E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170006C5 RID: 1733
			// (set) Token: 0x06001C9C RID: 7324 RVA: 0x0003CDB6 File Offset: 0x0003AFB6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170006C6 RID: 1734
			// (set) Token: 0x06001C9D RID: 7325 RVA: 0x0003CDCE File Offset: 0x0003AFCE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170006C7 RID: 1735
			// (set) Token: 0x06001C9E RID: 7326 RVA: 0x0003CDE6 File Offset: 0x0003AFE6
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020000C8 RID: 200
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170006C8 RID: 1736
			// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x0003CE06 File Offset: 0x0003B006
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170006C9 RID: 1737
			// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x0003CE1E File Offset: 0x0003B01E
			public virtual bool OutBoundOnly
			{
				set
				{
					base.PowerSharpParameters["OutBoundOnly"] = value;
				}
			}

			// Token: 0x170006CA RID: 1738
			// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x0003CE36 File Offset: 0x0003B036
			public virtual bool MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x170006CB RID: 1739
			// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x0003CE4E File Offset: 0x0003B04E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170006CC RID: 1740
			// (set) Token: 0x06001CA4 RID: 7332 RVA: 0x0003CE61 File Offset: 0x0003B061
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170006CD RID: 1741
			// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x0003CE79 File Offset: 0x0003B079
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170006CE RID: 1742
			// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x0003CE91 File Offset: 0x0003B091
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170006CF RID: 1743
			// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x0003CEA9 File Offset: 0x0003B0A9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170006D0 RID: 1744
			// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x0003CEC1 File Offset: 0x0003B0C1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170006D1 RID: 1745
			// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x0003CED9 File Offset: 0x0003B0D9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170006D2 RID: 1746
			// (set) Token: 0x06001CAA RID: 7338 RVA: 0x0003CEF1 File Offset: 0x0003B0F1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170006D3 RID: 1747
			// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0003CF09 File Offset: 0x0003B109
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170006D4 RID: 1748
			// (set) Token: 0x06001CAC RID: 7340 RVA: 0x0003CF21 File Offset: 0x0003B121
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
