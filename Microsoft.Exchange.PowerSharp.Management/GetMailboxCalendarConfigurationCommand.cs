using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200047C RID: 1148
	public class GetMailboxCalendarConfigurationCommand : SyntheticCommandWithPipelineInput<MailboxCalendarConfiguration, MailboxCalendarConfiguration>
	{
		// Token: 0x0600411A RID: 16666 RVA: 0x0006C396 File Offset: 0x0006A596
		private GetMailboxCalendarConfigurationCommand() : base("Get-MailboxCalendarConfiguration")
		{
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0006C3A3 File Offset: 0x0006A5A3
		public GetMailboxCalendarConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0006C3B2 File Offset: 0x0006A5B2
		public virtual GetMailboxCalendarConfigurationCommand SetParameters(GetMailboxCalendarConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x0006C3BC File Offset: 0x0006A5BC
		public virtual GetMailboxCalendarConfigurationCommand SetParameters(GetMailboxCalendarConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200047D RID: 1149
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170023DB RID: 9179
			// (set) Token: 0x0600411E RID: 16670 RVA: 0x0006C3C6 File Offset: 0x0006A5C6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170023DC RID: 9180
			// (set) Token: 0x0600411F RID: 16671 RVA: 0x0006C3E4 File Offset: 0x0006A5E4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023DD RID: 9181
			// (set) Token: 0x06004120 RID: 16672 RVA: 0x0006C3F7 File Offset: 0x0006A5F7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023DE RID: 9182
			// (set) Token: 0x06004121 RID: 16673 RVA: 0x0006C40F File Offset: 0x0006A60F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023DF RID: 9183
			// (set) Token: 0x06004122 RID: 16674 RVA: 0x0006C427 File Offset: 0x0006A627
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023E0 RID: 9184
			// (set) Token: 0x06004123 RID: 16675 RVA: 0x0006C43F File Offset: 0x0006A63F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200047E RID: 1150
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170023E1 RID: 9185
			// (set) Token: 0x06004125 RID: 16677 RVA: 0x0006C45F File Offset: 0x0006A65F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023E2 RID: 9186
			// (set) Token: 0x06004126 RID: 16678 RVA: 0x0006C472 File Offset: 0x0006A672
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023E3 RID: 9187
			// (set) Token: 0x06004127 RID: 16679 RVA: 0x0006C48A File Offset: 0x0006A68A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023E4 RID: 9188
			// (set) Token: 0x06004128 RID: 16680 RVA: 0x0006C4A2 File Offset: 0x0006A6A2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023E5 RID: 9189
			// (set) Token: 0x06004129 RID: 16681 RVA: 0x0006C4BA File Offset: 0x0006A6BA
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
