using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Extension;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E49 RID: 3657
	public class RemoveAppCommand : SyntheticCommandWithPipelineInput<App, App>
	{
		// Token: 0x0600D902 RID: 55554 RVA: 0x00134155 File Offset: 0x00132355
		private RemoveAppCommand() : base("Remove-App")
		{
		}

		// Token: 0x0600D903 RID: 55555 RVA: 0x00134162 File Offset: 0x00132362
		public RemoveAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D904 RID: 55556 RVA: 0x00134171 File Offset: 0x00132371
		public virtual RemoveAppCommand SetParameters(RemoveAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D905 RID: 55557 RVA: 0x0013417B File Offset: 0x0013237B
		public virtual RemoveAppCommand SetParameters(RemoveAppCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E4A RID: 3658
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A829 RID: 43049
			// (set) Token: 0x0600D906 RID: 55558 RVA: 0x00134185 File Offset: 0x00132385
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A82A RID: 43050
			// (set) Token: 0x0600D907 RID: 55559 RVA: 0x001341A3 File Offset: 0x001323A3
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A82B RID: 43051
			// (set) Token: 0x0600D908 RID: 55560 RVA: 0x001341BB File Offset: 0x001323BB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A82C RID: 43052
			// (set) Token: 0x0600D909 RID: 55561 RVA: 0x001341CE File Offset: 0x001323CE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A82D RID: 43053
			// (set) Token: 0x0600D90A RID: 55562 RVA: 0x001341E6 File Offset: 0x001323E6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A82E RID: 43054
			// (set) Token: 0x0600D90B RID: 55563 RVA: 0x001341FE File Offset: 0x001323FE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A82F RID: 43055
			// (set) Token: 0x0600D90C RID: 55564 RVA: 0x00134216 File Offset: 0x00132416
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A830 RID: 43056
			// (set) Token: 0x0600D90D RID: 55565 RVA: 0x0013422E File Offset: 0x0013242E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A831 RID: 43057
			// (set) Token: 0x0600D90E RID: 55566 RVA: 0x00134246 File Offset: 0x00132446
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E4B RID: 3659
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A832 RID: 43058
			// (set) Token: 0x0600D910 RID: 55568 RVA: 0x00134266 File Offset: 0x00132466
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AppIdParameter(value) : null);
				}
			}

			// Token: 0x1700A833 RID: 43059
			// (set) Token: 0x0600D911 RID: 55569 RVA: 0x00134284 File Offset: 0x00132484
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A834 RID: 43060
			// (set) Token: 0x0600D912 RID: 55570 RVA: 0x001342A2 File Offset: 0x001324A2
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A835 RID: 43061
			// (set) Token: 0x0600D913 RID: 55571 RVA: 0x001342BA File Offset: 0x001324BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A836 RID: 43062
			// (set) Token: 0x0600D914 RID: 55572 RVA: 0x001342CD File Offset: 0x001324CD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A837 RID: 43063
			// (set) Token: 0x0600D915 RID: 55573 RVA: 0x001342E5 File Offset: 0x001324E5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A838 RID: 43064
			// (set) Token: 0x0600D916 RID: 55574 RVA: 0x001342FD File Offset: 0x001324FD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A839 RID: 43065
			// (set) Token: 0x0600D917 RID: 55575 RVA: 0x00134315 File Offset: 0x00132515
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A83A RID: 43066
			// (set) Token: 0x0600D918 RID: 55576 RVA: 0x0013432D File Offset: 0x0013252D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A83B RID: 43067
			// (set) Token: 0x0600D919 RID: 55577 RVA: 0x00134345 File Offset: 0x00132545
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
