using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000707 RID: 1799
	public class NewHostedConnectionFilterPolicyCommand : SyntheticCommandWithPipelineInput<HostedConnectionFilterPolicy, HostedConnectionFilterPolicy>
	{
		// Token: 0x06005CED RID: 23789 RVA: 0x00090326 File Offset: 0x0008E526
		private NewHostedConnectionFilterPolicyCommand() : base("New-HostedConnectionFilterPolicy")
		{
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x00090333 File Offset: 0x0008E533
		public NewHostedConnectionFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x00090342 File Offset: 0x0008E542
		public virtual NewHostedConnectionFilterPolicyCommand SetParameters(NewHostedConnectionFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000708 RID: 1800
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A98 RID: 15000
			// (set) Token: 0x06005CF0 RID: 23792 RVA: 0x0009034C File Offset: 0x0008E54C
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003A99 RID: 15001
			// (set) Token: 0x06005CF1 RID: 23793 RVA: 0x00090364 File Offset: 0x0008E564
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003A9A RID: 15002
			// (set) Token: 0x06005CF2 RID: 23794 RVA: 0x00090377 File Offset: 0x0008E577
			public virtual string AdminDisplayName
			{
				set
				{
					base.PowerSharpParameters["AdminDisplayName"] = value;
				}
			}

			// Token: 0x17003A9B RID: 15003
			// (set) Token: 0x06005CF3 RID: 23795 RVA: 0x0009038A File Offset: 0x0008E58A
			public virtual MultiValuedProperty<IPRange> IPAllowList
			{
				set
				{
					base.PowerSharpParameters["IPAllowList"] = value;
				}
			}

			// Token: 0x17003A9C RID: 15004
			// (set) Token: 0x06005CF4 RID: 23796 RVA: 0x0009039D File Offset: 0x0008E59D
			public virtual MultiValuedProperty<IPRange> IPBlockList
			{
				set
				{
					base.PowerSharpParameters["IPBlockList"] = value;
				}
			}

			// Token: 0x17003A9D RID: 15005
			// (set) Token: 0x06005CF5 RID: 23797 RVA: 0x000903B0 File Offset: 0x0008E5B0
			public virtual bool EnableSafeList
			{
				set
				{
					base.PowerSharpParameters["EnableSafeList"] = value;
				}
			}

			// Token: 0x17003A9E RID: 15006
			// (set) Token: 0x06005CF6 RID: 23798 RVA: 0x000903C8 File Offset: 0x0008E5C8
			public virtual DirectoryBasedEdgeBlockMode DirectoryBasedEdgeBlockMode
			{
				set
				{
					base.PowerSharpParameters["DirectoryBasedEdgeBlockMode"] = value;
				}
			}

			// Token: 0x17003A9F RID: 15007
			// (set) Token: 0x06005CF7 RID: 23799 RVA: 0x000903E0 File Offset: 0x0008E5E0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003AA0 RID: 15008
			// (set) Token: 0x06005CF8 RID: 23800 RVA: 0x000903FE File Offset: 0x0008E5FE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003AA1 RID: 15009
			// (set) Token: 0x06005CF9 RID: 23801 RVA: 0x00090411 File Offset: 0x0008E611
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003AA2 RID: 15010
			// (set) Token: 0x06005CFA RID: 23802 RVA: 0x00090429 File Offset: 0x0008E629
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003AA3 RID: 15011
			// (set) Token: 0x06005CFB RID: 23803 RVA: 0x00090441 File Offset: 0x0008E641
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003AA4 RID: 15012
			// (set) Token: 0x06005CFC RID: 23804 RVA: 0x00090459 File Offset: 0x0008E659
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003AA5 RID: 15013
			// (set) Token: 0x06005CFD RID: 23805 RVA: 0x00090471 File Offset: 0x0008E671
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
