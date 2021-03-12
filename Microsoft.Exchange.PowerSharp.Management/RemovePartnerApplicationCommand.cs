using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002E3 RID: 739
	public class RemovePartnerApplicationCommand : SyntheticCommandWithPipelineInput<PartnerApplication, PartnerApplication>
	{
		// Token: 0x0600326C RID: 12908 RVA: 0x00059529 File Offset: 0x00057729
		private RemovePartnerApplicationCommand() : base("Remove-PartnerApplication")
		{
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x00059536 File Offset: 0x00057736
		public RemovePartnerApplicationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x00059545 File Offset: 0x00057745
		public virtual RemovePartnerApplicationCommand SetParameters(RemovePartnerApplicationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x0005954F File Offset: 0x0005774F
		public virtual RemovePartnerApplicationCommand SetParameters(RemovePartnerApplicationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002E4 RID: 740
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700185F RID: 6239
			// (set) Token: 0x06003270 RID: 12912 RVA: 0x00059559 File Offset: 0x00057759
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001860 RID: 6240
			// (set) Token: 0x06003271 RID: 12913 RVA: 0x0005956C File Offset: 0x0005776C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001861 RID: 6241
			// (set) Token: 0x06003272 RID: 12914 RVA: 0x00059584 File Offset: 0x00057784
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001862 RID: 6242
			// (set) Token: 0x06003273 RID: 12915 RVA: 0x0005959C File Offset: 0x0005779C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001863 RID: 6243
			// (set) Token: 0x06003274 RID: 12916 RVA: 0x000595B4 File Offset: 0x000577B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001864 RID: 6244
			// (set) Token: 0x06003275 RID: 12917 RVA: 0x000595CC File Offset: 0x000577CC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001865 RID: 6245
			// (set) Token: 0x06003276 RID: 12918 RVA: 0x000595E4 File Offset: 0x000577E4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020002E5 RID: 741
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001866 RID: 6246
			// (set) Token: 0x06003278 RID: 12920 RVA: 0x00059604 File Offset: 0x00057804
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PartnerApplicationIdParameter(value) : null);
				}
			}

			// Token: 0x17001867 RID: 6247
			// (set) Token: 0x06003279 RID: 12921 RVA: 0x00059622 File Offset: 0x00057822
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001868 RID: 6248
			// (set) Token: 0x0600327A RID: 12922 RVA: 0x00059635 File Offset: 0x00057835
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001869 RID: 6249
			// (set) Token: 0x0600327B RID: 12923 RVA: 0x0005964D File Offset: 0x0005784D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700186A RID: 6250
			// (set) Token: 0x0600327C RID: 12924 RVA: 0x00059665 File Offset: 0x00057865
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700186B RID: 6251
			// (set) Token: 0x0600327D RID: 12925 RVA: 0x0005967D File Offset: 0x0005787D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700186C RID: 6252
			// (set) Token: 0x0600327E RID: 12926 RVA: 0x00059695 File Offset: 0x00057895
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700186D RID: 6253
			// (set) Token: 0x0600327F RID: 12927 RVA: 0x000596AD File Offset: 0x000578AD
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
