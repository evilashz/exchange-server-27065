using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000412 RID: 1042
	public class NewComplianceSearchCommand : SyntheticCommandWithPipelineInput<ComplianceSearch, ComplianceSearch>
	{
		// Token: 0x06003D69 RID: 15721 RVA: 0x000677F5 File Offset: 0x000659F5
		private NewComplianceSearchCommand() : base("New-ComplianceSearch")
		{
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x00067802 File Offset: 0x00065A02
		public NewComplianceSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x00067811 File Offset: 0x00065A11
		public virtual NewComplianceSearchCommand SetParameters(NewComplianceSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000413 RID: 1043
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170020FE RID: 8446
			// (set) Token: 0x06003D6C RID: 15724 RVA: 0x0006781B File Offset: 0x00065A1B
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x170020FF RID: 8447
			// (set) Token: 0x06003D6D RID: 15725 RVA: 0x0006782E File Offset: 0x00065A2E
			public virtual string StatusMailRecipients
			{
				set
				{
					base.PowerSharpParameters["StatusMailRecipients"] = value;
				}
			}

			// Token: 0x17002100 RID: 8448
			// (set) Token: 0x06003D6E RID: 15726 RVA: 0x00067841 File Offset: 0x00065A41
			public virtual ComplianceJobLogLevel LogLevel
			{
				set
				{
					base.PowerSharpParameters["LogLevel"] = value;
				}
			}

			// Token: 0x17002101 RID: 8449
			// (set) Token: 0x06003D6F RID: 15727 RVA: 0x00067859 File Offset: 0x00065A59
			public virtual bool IncludeUnindexedItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnindexedItems"] = value;
				}
			}

			// Token: 0x17002102 RID: 8450
			// (set) Token: 0x06003D70 RID: 15728 RVA: 0x00067871 File Offset: 0x00065A71
			public virtual string KeywordQuery
			{
				set
				{
					base.PowerSharpParameters["KeywordQuery"] = value;
				}
			}

			// Token: 0x17002103 RID: 8451
			// (set) Token: 0x06003D71 RID: 15729 RVA: 0x00067884 File Offset: 0x00065A84
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17002104 RID: 8452
			// (set) Token: 0x06003D72 RID: 15730 RVA: 0x0006789C File Offset: 0x00065A9C
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17002105 RID: 8453
			// (set) Token: 0x06003D73 RID: 15731 RVA: 0x000678B4 File Offset: 0x00065AB4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002106 RID: 8454
			// (set) Token: 0x06003D74 RID: 15732 RVA: 0x000678C7 File Offset: 0x00065AC7
			public virtual string ExchangeBinding
			{
				set
				{
					base.PowerSharpParameters["ExchangeBinding"] = value;
				}
			}

			// Token: 0x17002107 RID: 8455
			// (set) Token: 0x06003D75 RID: 15733 RVA: 0x000678DA File Offset: 0x00065ADA
			public virtual string PublicFolderBinding
			{
				set
				{
					base.PowerSharpParameters["PublicFolderBinding"] = value;
				}
			}

			// Token: 0x17002108 RID: 8456
			// (set) Token: 0x06003D76 RID: 15734 RVA: 0x000678ED File Offset: 0x00065AED
			public virtual string SharePointBinding
			{
				set
				{
					base.PowerSharpParameters["SharePointBinding"] = value;
				}
			}

			// Token: 0x17002109 RID: 8457
			// (set) Token: 0x06003D77 RID: 15735 RVA: 0x00067900 File Offset: 0x00065B00
			public virtual bool AllExchangeBindings
			{
				set
				{
					base.PowerSharpParameters["AllExchangeBindings"] = value;
				}
			}

			// Token: 0x1700210A RID: 8458
			// (set) Token: 0x06003D78 RID: 15736 RVA: 0x00067918 File Offset: 0x00065B18
			public virtual bool AllPublicFolderBindings
			{
				set
				{
					base.PowerSharpParameters["AllPublicFolderBindings"] = value;
				}
			}

			// Token: 0x1700210B RID: 8459
			// (set) Token: 0x06003D79 RID: 15737 RVA: 0x00067930 File Offset: 0x00065B30
			public virtual bool AllSharePointBindings
			{
				set
				{
					base.PowerSharpParameters["AllSharePointBindings"] = value;
				}
			}

			// Token: 0x1700210C RID: 8460
			// (set) Token: 0x06003D7A RID: 15738 RVA: 0x00067948 File Offset: 0x00065B48
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x1700210D RID: 8461
			// (set) Token: 0x06003D7B RID: 15739 RVA: 0x0006795B File Offset: 0x00065B5B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700210E RID: 8462
			// (set) Token: 0x06003D7C RID: 15740 RVA: 0x0006796E File Offset: 0x00065B6E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700210F RID: 8463
			// (set) Token: 0x06003D7D RID: 15741 RVA: 0x00067986 File Offset: 0x00065B86
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002110 RID: 8464
			// (set) Token: 0x06003D7E RID: 15742 RVA: 0x0006799E File Offset: 0x00065B9E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002111 RID: 8465
			// (set) Token: 0x06003D7F RID: 15743 RVA: 0x000679B6 File Offset: 0x00065BB6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002112 RID: 8466
			// (set) Token: 0x06003D80 RID: 15744 RVA: 0x000679CE File Offset: 0x00065BCE
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
