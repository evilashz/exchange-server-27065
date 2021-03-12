using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ClassificationDefinitions;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200050F RID: 1295
	public class RemoveDataClassificationCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x0600461E RID: 17950 RVA: 0x00072821 File Offset: 0x00070A21
		private RemoveDataClassificationCommand() : base("Remove-DataClassification")
		{
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x0007282E File Offset: 0x00070A2E
		public RemoveDataClassificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x0007283D File Offset: 0x00070A3D
		public virtual RemoveDataClassificationCommand SetParameters(RemoveDataClassificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x00072847 File Offset: 0x00070A47
		public virtual RemoveDataClassificationCommand SetParameters(RemoveDataClassificationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000510 RID: 1296
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170027B9 RID: 10169
			// (set) Token: 0x06004622 RID: 17954 RVA: 0x00072851 File Offset: 0x00070A51
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027BA RID: 10170
			// (set) Token: 0x06004623 RID: 17955 RVA: 0x00072864 File Offset: 0x00070A64
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027BB RID: 10171
			// (set) Token: 0x06004624 RID: 17956 RVA: 0x0007287C File Offset: 0x00070A7C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027BC RID: 10172
			// (set) Token: 0x06004625 RID: 17957 RVA: 0x00072894 File Offset: 0x00070A94
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027BD RID: 10173
			// (set) Token: 0x06004626 RID: 17958 RVA: 0x000728AC File Offset: 0x00070AAC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027BE RID: 10174
			// (set) Token: 0x06004627 RID: 17959 RVA: 0x000728C4 File Offset: 0x00070AC4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170027BF RID: 10175
			// (set) Token: 0x06004628 RID: 17960 RVA: 0x000728DC File Offset: 0x00070ADC
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000511 RID: 1297
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170027C0 RID: 10176
			// (set) Token: 0x0600462A RID: 17962 RVA: 0x000728FC File Offset: 0x00070AFC
			public virtual DataClassificationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170027C1 RID: 10177
			// (set) Token: 0x0600462B RID: 17963 RVA: 0x0007290F File Offset: 0x00070B0F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027C2 RID: 10178
			// (set) Token: 0x0600462C RID: 17964 RVA: 0x00072922 File Offset: 0x00070B22
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027C3 RID: 10179
			// (set) Token: 0x0600462D RID: 17965 RVA: 0x0007293A File Offset: 0x00070B3A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027C4 RID: 10180
			// (set) Token: 0x0600462E RID: 17966 RVA: 0x00072952 File Offset: 0x00070B52
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027C5 RID: 10181
			// (set) Token: 0x0600462F RID: 17967 RVA: 0x0007296A File Offset: 0x00070B6A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027C6 RID: 10182
			// (set) Token: 0x06004630 RID: 17968 RVA: 0x00072982 File Offset: 0x00070B82
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170027C7 RID: 10183
			// (set) Token: 0x06004631 RID: 17969 RVA: 0x0007299A File Offset: 0x00070B9A
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
