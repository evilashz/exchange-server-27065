using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DFF RID: 3583
	public class NewSiteMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<Uri>
	{
		// Token: 0x0600D55B RID: 54619 RVA: 0x0012F43F File Offset: 0x0012D63F
		private NewSiteMailboxCommand() : base("New-SiteMailbox")
		{
		}

		// Token: 0x0600D55C RID: 54620 RVA: 0x0012F44C File Offset: 0x0012D64C
		public NewSiteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D55D RID: 54621 RVA: 0x0012F45B File Offset: 0x0012D65B
		public virtual NewSiteMailboxCommand SetParameters(NewSiteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D55E RID: 54622 RVA: 0x0012F465 File Offset: 0x0012D665
		public virtual NewSiteMailboxCommand SetParameters(NewSiteMailboxCommand.TeamMailboxITProParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E00 RID: 3584
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A516 RID: 42262
			// (set) Token: 0x0600D55F RID: 54623 RVA: 0x0012F46F File Offset: 0x0012D66F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A517 RID: 42263
			// (set) Token: 0x0600D560 RID: 54624 RVA: 0x0012F482 File Offset: 0x0012D682
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A518 RID: 42264
			// (set) Token: 0x0600D561 RID: 54625 RVA: 0x0012F495 File Offset: 0x0012D695
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A519 RID: 42265
			// (set) Token: 0x0600D562 RID: 54626 RVA: 0x0012F4A8 File Offset: 0x0012D6A8
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700A51A RID: 42266
			// (set) Token: 0x0600D563 RID: 54627 RVA: 0x0012F4C0 File Offset: 0x0012D6C0
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700A51B RID: 42267
			// (set) Token: 0x0600D564 RID: 54628 RVA: 0x0012F4D8 File Offset: 0x0012D6D8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A51C RID: 42268
			// (set) Token: 0x0600D565 RID: 54629 RVA: 0x0012F4EB File Offset: 0x0012D6EB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A51D RID: 42269
			// (set) Token: 0x0600D566 RID: 54630 RVA: 0x0012F503 File Offset: 0x0012D703
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A51E RID: 42270
			// (set) Token: 0x0600D567 RID: 54631 RVA: 0x0012F51B File Offset: 0x0012D71B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A51F RID: 42271
			// (set) Token: 0x0600D568 RID: 54632 RVA: 0x0012F533 File Offset: 0x0012D733
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A520 RID: 42272
			// (set) Token: 0x0600D569 RID: 54633 RVA: 0x0012F54B File Offset: 0x0012D74B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E01 RID: 3585
		public class TeamMailboxITProParameters : ParametersBase
		{
			// Token: 0x1700A521 RID: 42273
			// (set) Token: 0x0600D56B RID: 54635 RVA: 0x0012F56B File Offset: 0x0012D76B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A522 RID: 42274
			// (set) Token: 0x0600D56C RID: 54636 RVA: 0x0012F589 File Offset: 0x0012D789
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A523 RID: 42275
			// (set) Token: 0x0600D56D RID: 54637 RVA: 0x0012F5A7 File Offset: 0x0012D7A7
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700A524 RID: 42276
			// (set) Token: 0x0600D56E RID: 54638 RVA: 0x0012F5BA File Offset: 0x0012D7BA
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700A525 RID: 42277
			// (set) Token: 0x0600D56F RID: 54639 RVA: 0x0012F5CD File Offset: 0x0012D7CD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A526 RID: 42278
			// (set) Token: 0x0600D570 RID: 54640 RVA: 0x0012F5E0 File Offset: 0x0012D7E0
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A527 RID: 42279
			// (set) Token: 0x0600D571 RID: 54641 RVA: 0x0012F5F3 File Offset: 0x0012D7F3
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A528 RID: 42280
			// (set) Token: 0x0600D572 RID: 54642 RVA: 0x0012F606 File Offset: 0x0012D806
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700A529 RID: 42281
			// (set) Token: 0x0600D573 RID: 54643 RVA: 0x0012F61E File Offset: 0x0012D81E
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700A52A RID: 42282
			// (set) Token: 0x0600D574 RID: 54644 RVA: 0x0012F636 File Offset: 0x0012D836
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A52B RID: 42283
			// (set) Token: 0x0600D575 RID: 54645 RVA: 0x0012F649 File Offset: 0x0012D849
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A52C RID: 42284
			// (set) Token: 0x0600D576 RID: 54646 RVA: 0x0012F661 File Offset: 0x0012D861
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A52D RID: 42285
			// (set) Token: 0x0600D577 RID: 54647 RVA: 0x0012F679 File Offset: 0x0012D879
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A52E RID: 42286
			// (set) Token: 0x0600D578 RID: 54648 RVA: 0x0012F691 File Offset: 0x0012D891
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A52F RID: 42287
			// (set) Token: 0x0600D579 RID: 54649 RVA: 0x0012F6A9 File Offset: 0x0012D8A9
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
