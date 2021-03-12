using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C21 RID: 3105
	public class NewEOPDistributionGroupCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x060097B7 RID: 38839 RVA: 0x000DCA78 File Offset: 0x000DAC78
		private NewEOPDistributionGroupCommand() : base("New-EOPDistributionGroup")
		{
		}

		// Token: 0x060097B8 RID: 38840 RVA: 0x000DCA85 File Offset: 0x000DAC85
		public NewEOPDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060097B9 RID: 38841 RVA: 0x000DCA94 File Offset: 0x000DAC94
		public virtual NewEOPDistributionGroupCommand SetParameters(NewEOPDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C22 RID: 3106
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B2E RID: 27438
			// (set) Token: 0x060097BA RID: 38842 RVA: 0x000DCA9E File Offset: 0x000DAC9E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006B2F RID: 27439
			// (set) Token: 0x060097BB RID: 38843 RVA: 0x000DCAB1 File Offset: 0x000DACB1
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006B30 RID: 27440
			// (set) Token: 0x060097BC RID: 38844 RVA: 0x000DCAC4 File Offset: 0x000DACC4
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17006B31 RID: 27441
			// (set) Token: 0x060097BD RID: 38845 RVA: 0x000DCAD7 File Offset: 0x000DACD7
			public virtual string Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17006B32 RID: 27442
			// (set) Token: 0x060097BE RID: 38846 RVA: 0x000DCAEA File Offset: 0x000DACEA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006B33 RID: 27443
			// (set) Token: 0x060097BF RID: 38847 RVA: 0x000DCAFD File Offset: 0x000DACFD
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006B34 RID: 27444
			// (set) Token: 0x060097C0 RID: 38848 RVA: 0x000DCB10 File Offset: 0x000DAD10
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006B35 RID: 27445
			// (set) Token: 0x060097C1 RID: 38849 RVA: 0x000DCB28 File Offset: 0x000DAD28
			public virtual GroupType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17006B36 RID: 27446
			// (set) Token: 0x060097C2 RID: 38850 RVA: 0x000DCB40 File Offset: 0x000DAD40
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B37 RID: 27447
			// (set) Token: 0x060097C3 RID: 38851 RVA: 0x000DCB5E File Offset: 0x000DAD5E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B38 RID: 27448
			// (set) Token: 0x060097C4 RID: 38852 RVA: 0x000DCB76 File Offset: 0x000DAD76
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B39 RID: 27449
			// (set) Token: 0x060097C5 RID: 38853 RVA: 0x000DCB8E File Offset: 0x000DAD8E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B3A RID: 27450
			// (set) Token: 0x060097C6 RID: 38854 RVA: 0x000DCBA6 File Offset: 0x000DADA6
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
