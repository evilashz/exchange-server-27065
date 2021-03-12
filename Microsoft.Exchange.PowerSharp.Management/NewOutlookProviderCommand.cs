using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004F9 RID: 1273
	public class NewOutlookProviderCommand : SyntheticCommandWithPipelineInput<OutlookProvider, OutlookProvider>
	{
		// Token: 0x0600457F RID: 17791 RVA: 0x00071BCF File Offset: 0x0006FDCF
		private NewOutlookProviderCommand() : base("New-OutlookProvider")
		{
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x00071BDC File Offset: 0x0006FDDC
		public NewOutlookProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x00071BEB File Offset: 0x0006FDEB
		public virtual NewOutlookProviderCommand SetParameters(NewOutlookProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004FA RID: 1274
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002746 RID: 10054
			// (set) Token: 0x06004582 RID: 17794 RVA: 0x00071BF5 File Offset: 0x0006FDF5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002747 RID: 10055
			// (set) Token: 0x06004583 RID: 17795 RVA: 0x00071C13 File Offset: 0x0006FE13
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002748 RID: 10056
			// (set) Token: 0x06004584 RID: 17796 RVA: 0x00071C26 File Offset: 0x0006FE26
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002749 RID: 10057
			// (set) Token: 0x06004585 RID: 17797 RVA: 0x00071C39 File Offset: 0x0006FE39
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700274A RID: 10058
			// (set) Token: 0x06004586 RID: 17798 RVA: 0x00071C51 File Offset: 0x0006FE51
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700274B RID: 10059
			// (set) Token: 0x06004587 RID: 17799 RVA: 0x00071C69 File Offset: 0x0006FE69
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700274C RID: 10060
			// (set) Token: 0x06004588 RID: 17800 RVA: 0x00071C81 File Offset: 0x0006FE81
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700274D RID: 10061
			// (set) Token: 0x06004589 RID: 17801 RVA: 0x00071C99 File Offset: 0x0006FE99
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
