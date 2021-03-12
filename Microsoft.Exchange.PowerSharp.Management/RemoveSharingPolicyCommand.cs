using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200069A RID: 1690
	public class RemoveSharingPolicyCommand : SyntheticCommandWithPipelineInput<SharingPolicy, SharingPolicy>
	{
		// Token: 0x0600597F RID: 22911 RVA: 0x0008BE22 File Offset: 0x0008A022
		private RemoveSharingPolicyCommand() : base("Remove-SharingPolicy")
		{
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x0008BE2F File Offset: 0x0008A02F
		public RemoveSharingPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x0008BE3E File Offset: 0x0008A03E
		public virtual RemoveSharingPolicyCommand SetParameters(RemoveSharingPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x0008BE48 File Offset: 0x0008A048
		public virtual RemoveSharingPolicyCommand SetParameters(RemoveSharingPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200069B RID: 1691
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003804 RID: 14340
			// (set) Token: 0x06005983 RID: 22915 RVA: 0x0008BE52 File Offset: 0x0008A052
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003805 RID: 14341
			// (set) Token: 0x06005984 RID: 22916 RVA: 0x0008BE65 File Offset: 0x0008A065
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003806 RID: 14342
			// (set) Token: 0x06005985 RID: 22917 RVA: 0x0008BE7D File Offset: 0x0008A07D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003807 RID: 14343
			// (set) Token: 0x06005986 RID: 22918 RVA: 0x0008BE95 File Offset: 0x0008A095
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003808 RID: 14344
			// (set) Token: 0x06005987 RID: 22919 RVA: 0x0008BEAD File Offset: 0x0008A0AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003809 RID: 14345
			// (set) Token: 0x06005988 RID: 22920 RVA: 0x0008BEC5 File Offset: 0x0008A0C5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700380A RID: 14346
			// (set) Token: 0x06005989 RID: 22921 RVA: 0x0008BEDD File Offset: 0x0008A0DD
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200069C RID: 1692
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700380B RID: 14347
			// (set) Token: 0x0600598B RID: 22923 RVA: 0x0008BEFD File Offset: 0x0008A0FD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700380C RID: 14348
			// (set) Token: 0x0600598C RID: 22924 RVA: 0x0008BF1B File Offset: 0x0008A11B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700380D RID: 14349
			// (set) Token: 0x0600598D RID: 22925 RVA: 0x0008BF2E File Offset: 0x0008A12E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700380E RID: 14350
			// (set) Token: 0x0600598E RID: 22926 RVA: 0x0008BF46 File Offset: 0x0008A146
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700380F RID: 14351
			// (set) Token: 0x0600598F RID: 22927 RVA: 0x0008BF5E File Offset: 0x0008A15E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003810 RID: 14352
			// (set) Token: 0x06005990 RID: 22928 RVA: 0x0008BF76 File Offset: 0x0008A176
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003811 RID: 14353
			// (set) Token: 0x06005991 RID: 22929 RVA: 0x0008BF8E File Offset: 0x0008A18E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003812 RID: 14354
			// (set) Token: 0x06005992 RID: 22930 RVA: 0x0008BFA6 File Offset: 0x0008A1A6
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
