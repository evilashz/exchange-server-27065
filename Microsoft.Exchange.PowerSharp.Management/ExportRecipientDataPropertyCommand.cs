using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D49 RID: 3401
	public class ExportRecipientDataPropertyCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x0600B3F4 RID: 46068 RVA: 0x001033EF File Offset: 0x001015EF
		private ExportRecipientDataPropertyCommand() : base("Export-RecipientDataProperty")
		{
		}

		// Token: 0x0600B3F5 RID: 46069 RVA: 0x001033FC File Offset: 0x001015FC
		public ExportRecipientDataPropertyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B3F6 RID: 46070 RVA: 0x0010340B File Offset: 0x0010160B
		public virtual ExportRecipientDataPropertyCommand SetParameters(ExportRecipientDataPropertyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B3F7 RID: 46071 RVA: 0x00103415 File Offset: 0x00101615
		public virtual ExportRecipientDataPropertyCommand SetParameters(ExportRecipientDataPropertyCommand.ExportPictureParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B3F8 RID: 46072 RVA: 0x0010341F File Offset: 0x0010161F
		public virtual ExportRecipientDataPropertyCommand SetParameters(ExportRecipientDataPropertyCommand.ExportSpokenNameParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D4A RID: 3402
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700851B RID: 34075
			// (set) Token: 0x0600B3F9 RID: 46073 RVA: 0x00103429 File Offset: 0x00101629
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxUserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700851C RID: 34076
			// (set) Token: 0x0600B3FA RID: 46074 RVA: 0x00103447 File Offset: 0x00101647
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700851D RID: 34077
			// (set) Token: 0x0600B3FB RID: 46075 RVA: 0x0010345A File Offset: 0x0010165A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700851E RID: 34078
			// (set) Token: 0x0600B3FC RID: 46076 RVA: 0x00103472 File Offset: 0x00101672
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700851F RID: 34079
			// (set) Token: 0x0600B3FD RID: 46077 RVA: 0x0010348A File Offset: 0x0010168A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008520 RID: 34080
			// (set) Token: 0x0600B3FE RID: 46078 RVA: 0x001034A2 File Offset: 0x001016A2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008521 RID: 34081
			// (set) Token: 0x0600B3FF RID: 46079 RVA: 0x001034BA File Offset: 0x001016BA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D4B RID: 3403
		public class ExportPictureParameters : ParametersBase
		{
			// Token: 0x17008522 RID: 34082
			// (set) Token: 0x0600B401 RID: 46081 RVA: 0x001034DA File Offset: 0x001016DA
			public virtual SwitchParameter Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008523 RID: 34083
			// (set) Token: 0x0600B402 RID: 46082 RVA: 0x001034F2 File Offset: 0x001016F2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxUserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008524 RID: 34084
			// (set) Token: 0x0600B403 RID: 46083 RVA: 0x00103510 File Offset: 0x00101710
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008525 RID: 34085
			// (set) Token: 0x0600B404 RID: 46084 RVA: 0x00103523 File Offset: 0x00101723
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008526 RID: 34086
			// (set) Token: 0x0600B405 RID: 46085 RVA: 0x0010353B File Offset: 0x0010173B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008527 RID: 34087
			// (set) Token: 0x0600B406 RID: 46086 RVA: 0x00103553 File Offset: 0x00101753
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008528 RID: 34088
			// (set) Token: 0x0600B407 RID: 46087 RVA: 0x0010356B File Offset: 0x0010176B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008529 RID: 34089
			// (set) Token: 0x0600B408 RID: 46088 RVA: 0x00103583 File Offset: 0x00101783
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D4C RID: 3404
		public class ExportSpokenNameParameters : ParametersBase
		{
			// Token: 0x1700852A RID: 34090
			// (set) Token: 0x0600B40A RID: 46090 RVA: 0x001035A3 File Offset: 0x001017A3
			public virtual SwitchParameter SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x1700852B RID: 34091
			// (set) Token: 0x0600B40B RID: 46091 RVA: 0x001035BB File Offset: 0x001017BB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxUserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700852C RID: 34092
			// (set) Token: 0x0600B40C RID: 46092 RVA: 0x001035D9 File Offset: 0x001017D9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700852D RID: 34093
			// (set) Token: 0x0600B40D RID: 46093 RVA: 0x001035EC File Offset: 0x001017EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700852E RID: 34094
			// (set) Token: 0x0600B40E RID: 46094 RVA: 0x00103604 File Offset: 0x00101804
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700852F RID: 34095
			// (set) Token: 0x0600B40F RID: 46095 RVA: 0x0010361C File Offset: 0x0010181C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008530 RID: 34096
			// (set) Token: 0x0600B410 RID: 46096 RVA: 0x00103634 File Offset: 0x00101834
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008531 RID: 34097
			// (set) Token: 0x0600B411 RID: 46097 RVA: 0x0010364C File Offset: 0x0010184C
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
