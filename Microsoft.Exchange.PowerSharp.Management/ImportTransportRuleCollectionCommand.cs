using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008F1 RID: 2289
	public class ImportTransportRuleCollectionCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06007299 RID: 29337 RVA: 0x000AC70A File Offset: 0x000AA90A
		private ImportTransportRuleCollectionCommand() : base("Import-TransportRuleCollection")
		{
		}

		// Token: 0x0600729A RID: 29338 RVA: 0x000AC717 File Offset: 0x000AA917
		public ImportTransportRuleCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600729B RID: 29339 RVA: 0x000AC726 File Offset: 0x000AA926
		public virtual ImportTransportRuleCollectionCommand SetParameters(ImportTransportRuleCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x000AC730 File Offset: 0x000AA930
		public virtual ImportTransportRuleCollectionCommand SetParameters(ImportTransportRuleCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008F2 RID: 2290
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004C70 RID: 19568
			// (set) Token: 0x0600729D RID: 29341 RVA: 0x000AC73A File Offset: 0x000AA93A
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17004C71 RID: 19569
			// (set) Token: 0x0600729E RID: 29342 RVA: 0x000AC752 File Offset: 0x000AA952
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004C72 RID: 19570
			// (set) Token: 0x0600729F RID: 29343 RVA: 0x000AC76A File Offset: 0x000AA96A
			public virtual MigrationSourceType MigrationSource
			{
				set
				{
					base.PowerSharpParameters["MigrationSource"] = value;
				}
			}

			// Token: 0x17004C73 RID: 19571
			// (set) Token: 0x060072A0 RID: 29344 RVA: 0x000AC782 File Offset: 0x000AA982
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004C74 RID: 19572
			// (set) Token: 0x060072A1 RID: 29345 RVA: 0x000AC7A0 File Offset: 0x000AA9A0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C75 RID: 19573
			// (set) Token: 0x060072A2 RID: 29346 RVA: 0x000AC7B3 File Offset: 0x000AA9B3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C76 RID: 19574
			// (set) Token: 0x060072A3 RID: 29347 RVA: 0x000AC7CB File Offset: 0x000AA9CB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C77 RID: 19575
			// (set) Token: 0x060072A4 RID: 29348 RVA: 0x000AC7E3 File Offset: 0x000AA9E3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C78 RID: 19576
			// (set) Token: 0x060072A5 RID: 29349 RVA: 0x000AC7FB File Offset: 0x000AA9FB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C79 RID: 19577
			// (set) Token: 0x060072A6 RID: 29350 RVA: 0x000AC813 File Offset: 0x000AAA13
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008F3 RID: 2291
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004C7A RID: 19578
			// (set) Token: 0x060072A8 RID: 29352 RVA: 0x000AC833 File Offset: 0x000AAA33
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004C7B RID: 19579
			// (set) Token: 0x060072A9 RID: 29353 RVA: 0x000AC851 File Offset: 0x000AAA51
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17004C7C RID: 19580
			// (set) Token: 0x060072AA RID: 29354 RVA: 0x000AC869 File Offset: 0x000AAA69
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004C7D RID: 19581
			// (set) Token: 0x060072AB RID: 29355 RVA: 0x000AC881 File Offset: 0x000AAA81
			public virtual MigrationSourceType MigrationSource
			{
				set
				{
					base.PowerSharpParameters["MigrationSource"] = value;
				}
			}

			// Token: 0x17004C7E RID: 19582
			// (set) Token: 0x060072AC RID: 29356 RVA: 0x000AC899 File Offset: 0x000AAA99
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004C7F RID: 19583
			// (set) Token: 0x060072AD RID: 29357 RVA: 0x000AC8B7 File Offset: 0x000AAAB7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C80 RID: 19584
			// (set) Token: 0x060072AE RID: 29358 RVA: 0x000AC8CA File Offset: 0x000AAACA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C81 RID: 19585
			// (set) Token: 0x060072AF RID: 29359 RVA: 0x000AC8E2 File Offset: 0x000AAAE2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C82 RID: 19586
			// (set) Token: 0x060072B0 RID: 29360 RVA: 0x000AC8FA File Offset: 0x000AAAFA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C83 RID: 19587
			// (set) Token: 0x060072B1 RID: 29361 RVA: 0x000AC912 File Offset: 0x000AAB12
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C84 RID: 19588
			// (set) Token: 0x060072B2 RID: 29362 RVA: 0x000AC92A File Offset: 0x000AAB2A
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
