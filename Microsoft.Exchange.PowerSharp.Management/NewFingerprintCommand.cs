using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200050D RID: 1293
	public class NewFingerprintCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06004611 RID: 17937 RVA: 0x0007271F File Offset: 0x0007091F
		private NewFingerprintCommand() : base("New-Fingerprint")
		{
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x0007272C File Offset: 0x0007092C
		public NewFingerprintCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x0007273B File Offset: 0x0007093B
		public virtual NewFingerprintCommand SetParameters(NewFingerprintCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200050E RID: 1294
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170027B0 RID: 10160
			// (set) Token: 0x06004614 RID: 17940 RVA: 0x00072745 File Offset: 0x00070945
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x170027B1 RID: 10161
			// (set) Token: 0x06004615 RID: 17941 RVA: 0x0007275D File Offset: 0x0007095D
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x170027B2 RID: 10162
			// (set) Token: 0x06004616 RID: 17942 RVA: 0x00072770 File Offset: 0x00070970
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170027B3 RID: 10163
			// (set) Token: 0x06004617 RID: 17943 RVA: 0x0007278E File Offset: 0x0007098E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027B4 RID: 10164
			// (set) Token: 0x06004618 RID: 17944 RVA: 0x000727A1 File Offset: 0x000709A1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027B5 RID: 10165
			// (set) Token: 0x06004619 RID: 17945 RVA: 0x000727B9 File Offset: 0x000709B9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027B6 RID: 10166
			// (set) Token: 0x0600461A RID: 17946 RVA: 0x000727D1 File Offset: 0x000709D1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027B7 RID: 10167
			// (set) Token: 0x0600461B RID: 17947 RVA: 0x000727E9 File Offset: 0x000709E9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027B8 RID: 10168
			// (set) Token: 0x0600461C RID: 17948 RVA: 0x00072801 File Offset: 0x00070A01
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
