using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AFC RID: 2812
	public class RemoveRecipientEnforcementProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<RecipientEnforcementProvisioningPolicy, RecipientEnforcementProvisioningPolicy>
	{
		// Token: 0x06008A39 RID: 35385 RVA: 0x000CB31A File Offset: 0x000C951A
		private RemoveRecipientEnforcementProvisioningPolicyCommand() : base("Remove-RecipientEnforcementProvisioningPolicy")
		{
		}

		// Token: 0x06008A3A RID: 35386 RVA: 0x000CB327 File Offset: 0x000C9527
		public RemoveRecipientEnforcementProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008A3B RID: 35387 RVA: 0x000CB336 File Offset: 0x000C9536
		public virtual RemoveRecipientEnforcementProvisioningPolicyCommand SetParameters(RemoveRecipientEnforcementProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008A3C RID: 35388 RVA: 0x000CB340 File Offset: 0x000C9540
		public virtual RemoveRecipientEnforcementProvisioningPolicyCommand SetParameters(RemoveRecipientEnforcementProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AFD RID: 2813
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005FFA RID: 24570
			// (set) Token: 0x06008A3D RID: 35389 RVA: 0x000CB34A File Offset: 0x000C954A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FFB RID: 24571
			// (set) Token: 0x06008A3E RID: 35390 RVA: 0x000CB35D File Offset: 0x000C955D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FFC RID: 24572
			// (set) Token: 0x06008A3F RID: 35391 RVA: 0x000CB375 File Offset: 0x000C9575
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FFD RID: 24573
			// (set) Token: 0x06008A40 RID: 35392 RVA: 0x000CB38D File Offset: 0x000C958D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FFE RID: 24574
			// (set) Token: 0x06008A41 RID: 35393 RVA: 0x000CB3A5 File Offset: 0x000C95A5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005FFF RID: 24575
			// (set) Token: 0x06008A42 RID: 35394 RVA: 0x000CB3BD File Offset: 0x000C95BD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006000 RID: 24576
			// (set) Token: 0x06008A43 RID: 35395 RVA: 0x000CB3D5 File Offset: 0x000C95D5
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000AFE RID: 2814
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006001 RID: 24577
			// (set) Token: 0x06008A45 RID: 35397 RVA: 0x000CB3F5 File Offset: 0x000C95F5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientEnforcementProvisioningPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006002 RID: 24578
			// (set) Token: 0x06008A46 RID: 35398 RVA: 0x000CB413 File Offset: 0x000C9613
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006003 RID: 24579
			// (set) Token: 0x06008A47 RID: 35399 RVA: 0x000CB426 File Offset: 0x000C9626
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006004 RID: 24580
			// (set) Token: 0x06008A48 RID: 35400 RVA: 0x000CB43E File Offset: 0x000C963E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006005 RID: 24581
			// (set) Token: 0x06008A49 RID: 35401 RVA: 0x000CB456 File Offset: 0x000C9656
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006006 RID: 24582
			// (set) Token: 0x06008A4A RID: 35402 RVA: 0x000CB46E File Offset: 0x000C966E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006007 RID: 24583
			// (set) Token: 0x06008A4B RID: 35403 RVA: 0x000CB486 File Offset: 0x000C9686
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006008 RID: 24584
			// (set) Token: 0x06008A4C RID: 35404 RVA: 0x000CB49E File Offset: 0x000C969E
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
