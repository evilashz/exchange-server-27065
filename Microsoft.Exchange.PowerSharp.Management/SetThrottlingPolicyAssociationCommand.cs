using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200087E RID: 2174
	public class SetThrottlingPolicyAssociationCommand : SyntheticCommandWithPipelineInputNoOutput<ThrottlingPolicyAssociation>
	{
		// Token: 0x06006C93 RID: 27795 RVA: 0x000A4817 File Offset: 0x000A2A17
		private SetThrottlingPolicyAssociationCommand() : base("Set-ThrottlingPolicyAssociation")
		{
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x000A4824 File Offset: 0x000A2A24
		public SetThrottlingPolicyAssociationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x000A4833 File Offset: 0x000A2A33
		public virtual SetThrottlingPolicyAssociationCommand SetParameters(SetThrottlingPolicyAssociationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x000A483D File Offset: 0x000A2A3D
		public virtual SetThrottlingPolicyAssociationCommand SetParameters(SetThrottlingPolicyAssociationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200087F RID: 2175
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004750 RID: 18256
			// (set) Token: 0x06006C97 RID: 27799 RVA: 0x000A4847 File Offset: 0x000A2A47
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17004751 RID: 18257
			// (set) Token: 0x06006C98 RID: 27800 RVA: 0x000A4865 File Offset: 0x000A2A65
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17004752 RID: 18258
			// (set) Token: 0x06006C99 RID: 27801 RVA: 0x000A487D File Offset: 0x000A2A7D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004753 RID: 18259
			// (set) Token: 0x06006C9A RID: 27802 RVA: 0x000A4890 File Offset: 0x000A2A90
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004754 RID: 18260
			// (set) Token: 0x06006C9B RID: 27803 RVA: 0x000A48A8 File Offset: 0x000A2AA8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004755 RID: 18261
			// (set) Token: 0x06006C9C RID: 27804 RVA: 0x000A48C0 File Offset: 0x000A2AC0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004756 RID: 18262
			// (set) Token: 0x06006C9D RID: 27805 RVA: 0x000A48D8 File Offset: 0x000A2AD8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004757 RID: 18263
			// (set) Token: 0x06006C9E RID: 27806 RVA: 0x000A48F0 File Offset: 0x000A2AF0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000880 RID: 2176
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004758 RID: 18264
			// (set) Token: 0x06006CA0 RID: 27808 RVA: 0x000A4910 File Offset: 0x000A2B10
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ThrottlingPolicyAssociationIdParameter(value) : null);
				}
			}

			// Token: 0x17004759 RID: 18265
			// (set) Token: 0x06006CA1 RID: 27809 RVA: 0x000A492E File Offset: 0x000A2B2E
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700475A RID: 18266
			// (set) Token: 0x06006CA2 RID: 27810 RVA: 0x000A494C File Offset: 0x000A2B4C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700475B RID: 18267
			// (set) Token: 0x06006CA3 RID: 27811 RVA: 0x000A4964 File Offset: 0x000A2B64
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700475C RID: 18268
			// (set) Token: 0x06006CA4 RID: 27812 RVA: 0x000A4977 File Offset: 0x000A2B77
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700475D RID: 18269
			// (set) Token: 0x06006CA5 RID: 27813 RVA: 0x000A498F File Offset: 0x000A2B8F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700475E RID: 18270
			// (set) Token: 0x06006CA6 RID: 27814 RVA: 0x000A49A7 File Offset: 0x000A2BA7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700475F RID: 18271
			// (set) Token: 0x06006CA7 RID: 27815 RVA: 0x000A49BF File Offset: 0x000A2BBF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004760 RID: 18272
			// (set) Token: 0x06006CA8 RID: 27816 RVA: 0x000A49D7 File Offset: 0x000A2BD7
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
