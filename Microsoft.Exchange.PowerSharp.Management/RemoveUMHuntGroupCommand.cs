using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B73 RID: 2931
	public class RemoveUMHuntGroupCommand : SyntheticCommandWithPipelineInput<UMHuntGroup, UMHuntGroup>
	{
		// Token: 0x06008DEE RID: 36334 RVA: 0x000CFEBF File Offset: 0x000CE0BF
		private RemoveUMHuntGroupCommand() : base("Remove-UMHuntGroup")
		{
		}

		// Token: 0x06008DEF RID: 36335 RVA: 0x000CFECC File Offset: 0x000CE0CC
		public RemoveUMHuntGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008DF0 RID: 36336 RVA: 0x000CFEDB File Offset: 0x000CE0DB
		public virtual RemoveUMHuntGroupCommand SetParameters(RemoveUMHuntGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008DF1 RID: 36337 RVA: 0x000CFEE5 File Offset: 0x000CE0E5
		public virtual RemoveUMHuntGroupCommand SetParameters(RemoveUMHuntGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B74 RID: 2932
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170062C1 RID: 25281
			// (set) Token: 0x06008DF2 RID: 36338 RVA: 0x000CFEEF File Offset: 0x000CE0EF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062C2 RID: 25282
			// (set) Token: 0x06008DF3 RID: 36339 RVA: 0x000CFF02 File Offset: 0x000CE102
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062C3 RID: 25283
			// (set) Token: 0x06008DF4 RID: 36340 RVA: 0x000CFF1A File Offset: 0x000CE11A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062C4 RID: 25284
			// (set) Token: 0x06008DF5 RID: 36341 RVA: 0x000CFF32 File Offset: 0x000CE132
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062C5 RID: 25285
			// (set) Token: 0x06008DF6 RID: 36342 RVA: 0x000CFF4A File Offset: 0x000CE14A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062C6 RID: 25286
			// (set) Token: 0x06008DF7 RID: 36343 RVA: 0x000CFF62 File Offset: 0x000CE162
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170062C7 RID: 25287
			// (set) Token: 0x06008DF8 RID: 36344 RVA: 0x000CFF7A File Offset: 0x000CE17A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B75 RID: 2933
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170062C8 RID: 25288
			// (set) Token: 0x06008DFA RID: 36346 RVA: 0x000CFF9A File Offset: 0x000CE19A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMHuntGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170062C9 RID: 25289
			// (set) Token: 0x06008DFB RID: 36347 RVA: 0x000CFFB8 File Offset: 0x000CE1B8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062CA RID: 25290
			// (set) Token: 0x06008DFC RID: 36348 RVA: 0x000CFFCB File Offset: 0x000CE1CB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062CB RID: 25291
			// (set) Token: 0x06008DFD RID: 36349 RVA: 0x000CFFE3 File Offset: 0x000CE1E3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062CC RID: 25292
			// (set) Token: 0x06008DFE RID: 36350 RVA: 0x000CFFFB File Offset: 0x000CE1FB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062CD RID: 25293
			// (set) Token: 0x06008DFF RID: 36351 RVA: 0x000D0013 File Offset: 0x000CE213
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062CE RID: 25294
			// (set) Token: 0x06008E00 RID: 36352 RVA: 0x000D002B File Offset: 0x000CE22B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170062CF RID: 25295
			// (set) Token: 0x06008E01 RID: 36353 RVA: 0x000D0043 File Offset: 0x000CE243
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
