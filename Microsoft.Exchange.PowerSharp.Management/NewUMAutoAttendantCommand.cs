using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B71 RID: 2929
	public class NewUMAutoAttendantCommand : SyntheticCommandWithPipelineInput<UMAutoAttendant, UMAutoAttendant>
	{
		// Token: 0x06008DDC RID: 36316 RVA: 0x000CFD3E File Offset: 0x000CDF3E
		private NewUMAutoAttendantCommand() : base("New-UMAutoAttendant")
		{
		}

		// Token: 0x06008DDD RID: 36317 RVA: 0x000CFD4B File Offset: 0x000CDF4B
		public NewUMAutoAttendantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008DDE RID: 36318 RVA: 0x000CFD5A File Offset: 0x000CDF5A
		public virtual NewUMAutoAttendantCommand SetParameters(NewUMAutoAttendantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B72 RID: 2930
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170062B3 RID: 25267
			// (set) Token: 0x06008DDF RID: 36319 RVA: 0x000CFD64 File Offset: 0x000CDF64
			public virtual MultiValuedProperty<string> PilotIdentifierList
			{
				set
				{
					base.PowerSharpParameters["PilotIdentifierList"] = value;
				}
			}

			// Token: 0x170062B4 RID: 25268
			// (set) Token: 0x06008DE0 RID: 36320 RVA: 0x000CFD77 File Offset: 0x000CDF77
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170062B5 RID: 25269
			// (set) Token: 0x06008DE1 RID: 36321 RVA: 0x000CFD95 File Offset: 0x000CDF95
			public virtual SwitchParameter SharedUMDialPlan
			{
				set
				{
					base.PowerSharpParameters["SharedUMDialPlan"] = value;
				}
			}

			// Token: 0x170062B6 RID: 25270
			// (set) Token: 0x06008DE2 RID: 36322 RVA: 0x000CFDAD File Offset: 0x000CDFAD
			public virtual StatusEnum Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170062B7 RID: 25271
			// (set) Token: 0x06008DE3 RID: 36323 RVA: 0x000CFDC5 File Offset: 0x000CDFC5
			public virtual bool SpeechEnabled
			{
				set
				{
					base.PowerSharpParameters["SpeechEnabled"] = value;
				}
			}

			// Token: 0x170062B8 RID: 25272
			// (set) Token: 0x06008DE4 RID: 36324 RVA: 0x000CFDDD File Offset: 0x000CDFDD
			public virtual string DTMFFallbackAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["DTMFFallbackAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x170062B9 RID: 25273
			// (set) Token: 0x06008DE5 RID: 36325 RVA: 0x000CFDFB File Offset: 0x000CDFFB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170062BA RID: 25274
			// (set) Token: 0x06008DE6 RID: 36326 RVA: 0x000CFE19 File Offset: 0x000CE019
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170062BB RID: 25275
			// (set) Token: 0x06008DE7 RID: 36327 RVA: 0x000CFE2C File Offset: 0x000CE02C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062BC RID: 25276
			// (set) Token: 0x06008DE8 RID: 36328 RVA: 0x000CFE3F File Offset: 0x000CE03F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062BD RID: 25277
			// (set) Token: 0x06008DE9 RID: 36329 RVA: 0x000CFE57 File Offset: 0x000CE057
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062BE RID: 25278
			// (set) Token: 0x06008DEA RID: 36330 RVA: 0x000CFE6F File Offset: 0x000CE06F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062BF RID: 25279
			// (set) Token: 0x06008DEB RID: 36331 RVA: 0x000CFE87 File Offset: 0x000CE087
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062C0 RID: 25280
			// (set) Token: 0x06008DEC RID: 36332 RVA: 0x000CFE9F File Offset: 0x000CE09F
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
