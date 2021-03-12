using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tools;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B0D RID: 2829
	public class GetToolInformationCommand : SyntheticCommandWithPipelineInputNoOutput<ToolId>
	{
		// Token: 0x06008AD1 RID: 35537 RVA: 0x000CBF89 File Offset: 0x000CA189
		private GetToolInformationCommand() : base("Get-ToolInformation")
		{
		}

		// Token: 0x06008AD2 RID: 35538 RVA: 0x000CBF96 File Offset: 0x000CA196
		public GetToolInformationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008AD3 RID: 35539 RVA: 0x000CBFA5 File Offset: 0x000CA1A5
		public virtual GetToolInformationCommand SetParameters(GetToolInformationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B0E RID: 2830
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006070 RID: 24688
			// (set) Token: 0x06008AD4 RID: 35540 RVA: 0x000CBFAF File Offset: 0x000CA1AF
			public virtual ToolId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006071 RID: 24689
			// (set) Token: 0x06008AD5 RID: 35541 RVA: 0x000CBFC7 File Offset: 0x000CA1C7
			public virtual Version Version
			{
				set
				{
					base.PowerSharpParameters["Version"] = value;
				}
			}

			// Token: 0x17006072 RID: 24690
			// (set) Token: 0x06008AD6 RID: 35542 RVA: 0x000CBFDA File Offset: 0x000CA1DA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006073 RID: 24691
			// (set) Token: 0x06008AD7 RID: 35543 RVA: 0x000CBFF8 File Offset: 0x000CA1F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006074 RID: 24692
			// (set) Token: 0x06008AD8 RID: 35544 RVA: 0x000CC010 File Offset: 0x000CA210
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006075 RID: 24693
			// (set) Token: 0x06008AD9 RID: 35545 RVA: 0x000CC028 File Offset: 0x000CA228
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006076 RID: 24694
			// (set) Token: 0x06008ADA RID: 35546 RVA: 0x000CC040 File Offset: 0x000CA240
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
