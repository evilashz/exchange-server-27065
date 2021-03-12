using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000498 RID: 1176
	public class SetMailboxRegionalConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxRegionalConfiguration>
	{
		// Token: 0x0600422E RID: 16942 RVA: 0x0006DA0C File Offset: 0x0006BC0C
		private SetMailboxRegionalConfigurationCommand() : base("Set-MailboxRegionalConfiguration")
		{
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x0006DA19 File Offset: 0x0006BC19
		public SetMailboxRegionalConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x0006DA28 File Offset: 0x0006BC28
		public virtual SetMailboxRegionalConfigurationCommand SetParameters(SetMailboxRegionalConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x0006DA32 File Offset: 0x0006BC32
		public virtual SetMailboxRegionalConfigurationCommand SetParameters(SetMailboxRegionalConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000499 RID: 1177
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170024B7 RID: 9399
			// (set) Token: 0x06004232 RID: 16946 RVA: 0x0006DA3C File Offset: 0x0006BC3C
			public virtual SwitchParameter LocalizeDefaultFolderName
			{
				set
				{
					base.PowerSharpParameters["LocalizeDefaultFolderName"] = value;
				}
			}

			// Token: 0x170024B8 RID: 9400
			// (set) Token: 0x06004233 RID: 16947 RVA: 0x0006DA54 File Offset: 0x0006BC54
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024B9 RID: 9401
			// (set) Token: 0x06004234 RID: 16948 RVA: 0x0006DA67 File Offset: 0x0006BC67
			public virtual string DateFormat
			{
				set
				{
					base.PowerSharpParameters["DateFormat"] = value;
				}
			}

			// Token: 0x170024BA RID: 9402
			// (set) Token: 0x06004235 RID: 16949 RVA: 0x0006DA7A File Offset: 0x0006BC7A
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x170024BB RID: 9403
			// (set) Token: 0x06004236 RID: 16950 RVA: 0x0006DA8D File Offset: 0x0006BC8D
			public virtual string TimeFormat
			{
				set
				{
					base.PowerSharpParameters["TimeFormat"] = value;
				}
			}

			// Token: 0x170024BC RID: 9404
			// (set) Token: 0x06004237 RID: 16951 RVA: 0x0006DAA0 File Offset: 0x0006BCA0
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x170024BD RID: 9405
			// (set) Token: 0x06004238 RID: 16952 RVA: 0x0006DAB3 File Offset: 0x0006BCB3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024BE RID: 9406
			// (set) Token: 0x06004239 RID: 16953 RVA: 0x0006DACB File Offset: 0x0006BCCB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024BF RID: 9407
			// (set) Token: 0x0600423A RID: 16954 RVA: 0x0006DAE3 File Offset: 0x0006BCE3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024C0 RID: 9408
			// (set) Token: 0x0600423B RID: 16955 RVA: 0x0006DAFB File Offset: 0x0006BCFB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170024C1 RID: 9409
			// (set) Token: 0x0600423C RID: 16956 RVA: 0x0006DB13 File Offset: 0x0006BD13
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200049A RID: 1178
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170024C2 RID: 9410
			// (set) Token: 0x0600423E RID: 16958 RVA: 0x0006DB33 File Offset: 0x0006BD33
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170024C3 RID: 9411
			// (set) Token: 0x0600423F RID: 16959 RVA: 0x0006DB51 File Offset: 0x0006BD51
			public virtual SwitchParameter LocalizeDefaultFolderName
			{
				set
				{
					base.PowerSharpParameters["LocalizeDefaultFolderName"] = value;
				}
			}

			// Token: 0x170024C4 RID: 9412
			// (set) Token: 0x06004240 RID: 16960 RVA: 0x0006DB69 File Offset: 0x0006BD69
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024C5 RID: 9413
			// (set) Token: 0x06004241 RID: 16961 RVA: 0x0006DB7C File Offset: 0x0006BD7C
			public virtual string DateFormat
			{
				set
				{
					base.PowerSharpParameters["DateFormat"] = value;
				}
			}

			// Token: 0x170024C6 RID: 9414
			// (set) Token: 0x06004242 RID: 16962 RVA: 0x0006DB8F File Offset: 0x0006BD8F
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x170024C7 RID: 9415
			// (set) Token: 0x06004243 RID: 16963 RVA: 0x0006DBA2 File Offset: 0x0006BDA2
			public virtual string TimeFormat
			{
				set
				{
					base.PowerSharpParameters["TimeFormat"] = value;
				}
			}

			// Token: 0x170024C8 RID: 9416
			// (set) Token: 0x06004244 RID: 16964 RVA: 0x0006DBB5 File Offset: 0x0006BDB5
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x170024C9 RID: 9417
			// (set) Token: 0x06004245 RID: 16965 RVA: 0x0006DBC8 File Offset: 0x0006BDC8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024CA RID: 9418
			// (set) Token: 0x06004246 RID: 16966 RVA: 0x0006DBE0 File Offset: 0x0006BDE0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024CB RID: 9419
			// (set) Token: 0x06004247 RID: 16967 RVA: 0x0006DBF8 File Offset: 0x0006BDF8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024CC RID: 9420
			// (set) Token: 0x06004248 RID: 16968 RVA: 0x0006DC10 File Offset: 0x0006BE10
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170024CD RID: 9421
			// (set) Token: 0x06004249 RID: 16969 RVA: 0x0006DC28 File Offset: 0x0006BE28
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
