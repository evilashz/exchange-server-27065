using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ClassificationDefinitions;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000512 RID: 1298
	public class SetDataClassificationCommand : SyntheticCommandWithPipelineInputNoOutput<TransportRule>
	{
		// Token: 0x06004633 RID: 17971 RVA: 0x000729BA File Offset: 0x00070BBA
		private SetDataClassificationCommand() : base("Set-DataClassification")
		{
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000729C7 File Offset: 0x00070BC7
		public SetDataClassificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x000729D6 File Offset: 0x00070BD6
		public virtual SetDataClassificationCommand SetParameters(SetDataClassificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x000729E0 File Offset: 0x00070BE0
		public virtual SetDataClassificationCommand SetParameters(SetDataClassificationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000513 RID: 1299
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170027C8 RID: 10184
			// (set) Token: 0x06004637 RID: 17975 RVA: 0x000729EA File Offset: 0x00070BEA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170027C9 RID: 10185
			// (set) Token: 0x06004638 RID: 17976 RVA: 0x000729FD File Offset: 0x00070BFD
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x170027CA RID: 10186
			// (set) Token: 0x06004639 RID: 17977 RVA: 0x00072A10 File Offset: 0x00070C10
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x170027CB RID: 10187
			// (set) Token: 0x0600463A RID: 17978 RVA: 0x00072A23 File Offset: 0x00070C23
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x170027CC RID: 10188
			// (set) Token: 0x0600463B RID: 17979 RVA: 0x00072A3B File Offset: 0x00070C3B
			public virtual MultiValuedProperty<Fingerprint> Fingerprints
			{
				set
				{
					base.PowerSharpParameters["Fingerprints"] = value;
				}
			}

			// Token: 0x170027CD RID: 10189
			// (set) Token: 0x0600463C RID: 17980 RVA: 0x00072A4E File Offset: 0x00070C4E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027CE RID: 10190
			// (set) Token: 0x0600463D RID: 17981 RVA: 0x00072A61 File Offset: 0x00070C61
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027CF RID: 10191
			// (set) Token: 0x0600463E RID: 17982 RVA: 0x00072A79 File Offset: 0x00070C79
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027D0 RID: 10192
			// (set) Token: 0x0600463F RID: 17983 RVA: 0x00072A91 File Offset: 0x00070C91
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027D1 RID: 10193
			// (set) Token: 0x06004640 RID: 17984 RVA: 0x00072AA9 File Offset: 0x00070CA9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027D2 RID: 10194
			// (set) Token: 0x06004641 RID: 17985 RVA: 0x00072AC1 File Offset: 0x00070CC1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000514 RID: 1300
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170027D3 RID: 10195
			// (set) Token: 0x06004643 RID: 17987 RVA: 0x00072AE1 File Offset: 0x00070CE1
			public virtual DataClassificationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170027D4 RID: 10196
			// (set) Token: 0x06004644 RID: 17988 RVA: 0x00072AF4 File Offset: 0x00070CF4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170027D5 RID: 10197
			// (set) Token: 0x06004645 RID: 17989 RVA: 0x00072B07 File Offset: 0x00070D07
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x170027D6 RID: 10198
			// (set) Token: 0x06004646 RID: 17990 RVA: 0x00072B1A File Offset: 0x00070D1A
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x170027D7 RID: 10199
			// (set) Token: 0x06004647 RID: 17991 RVA: 0x00072B2D File Offset: 0x00070D2D
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x170027D8 RID: 10200
			// (set) Token: 0x06004648 RID: 17992 RVA: 0x00072B45 File Offset: 0x00070D45
			public virtual MultiValuedProperty<Fingerprint> Fingerprints
			{
				set
				{
					base.PowerSharpParameters["Fingerprints"] = value;
				}
			}

			// Token: 0x170027D9 RID: 10201
			// (set) Token: 0x06004649 RID: 17993 RVA: 0x00072B58 File Offset: 0x00070D58
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027DA RID: 10202
			// (set) Token: 0x0600464A RID: 17994 RVA: 0x00072B6B File Offset: 0x00070D6B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027DB RID: 10203
			// (set) Token: 0x0600464B RID: 17995 RVA: 0x00072B83 File Offset: 0x00070D83
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027DC RID: 10204
			// (set) Token: 0x0600464C RID: 17996 RVA: 0x00072B9B File Offset: 0x00070D9B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027DD RID: 10205
			// (set) Token: 0x0600464D RID: 17997 RVA: 0x00072BB3 File Offset: 0x00070DB3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027DE RID: 10206
			// (set) Token: 0x0600464E RID: 17998 RVA: 0x00072BCB File Offset: 0x00070DCB
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
