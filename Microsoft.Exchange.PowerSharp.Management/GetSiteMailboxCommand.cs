using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DF9 RID: 3577
	public class GetSiteMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600D528 RID: 54568 RVA: 0x0012F011 File Offset: 0x0012D211
		private GetSiteMailboxCommand() : base("Get-SiteMailbox")
		{
		}

		// Token: 0x0600D529 RID: 54569 RVA: 0x0012F01E File Offset: 0x0012D21E
		public GetSiteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D52A RID: 54570 RVA: 0x0012F02D File Offset: 0x0012D22D
		public virtual GetSiteMailboxCommand SetParameters(GetSiteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D52B RID: 54571 RVA: 0x0012F037 File Offset: 0x0012D237
		public virtual GetSiteMailboxCommand SetParameters(GetSiteMailboxCommand.TeamMailboxITProParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DFA RID: 3578
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A4EF RID: 42223
			// (set) Token: 0x0600D52C RID: 54572 RVA: 0x0012F041 File Offset: 0x0012D241
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4F0 RID: 42224
			// (set) Token: 0x0600D52D RID: 54573 RVA: 0x0012F05F File Offset: 0x0012D25F
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x1700A4F1 RID: 42225
			// (set) Token: 0x0600D52E RID: 54574 RVA: 0x0012F072 File Offset: 0x0012D272
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A4F2 RID: 42226
			// (set) Token: 0x0600D52F RID: 54575 RVA: 0x0012F085 File Offset: 0x0012D285
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A4F3 RID: 42227
			// (set) Token: 0x0600D530 RID: 54576 RVA: 0x0012F09D File Offset: 0x0012D29D
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A4F4 RID: 42228
			// (set) Token: 0x0600D531 RID: 54577 RVA: 0x0012F0B5 File Offset: 0x0012D2B5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A4F5 RID: 42229
			// (set) Token: 0x0600D532 RID: 54578 RVA: 0x0012F0C8 File Offset: 0x0012D2C8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A4F6 RID: 42230
			// (set) Token: 0x0600D533 RID: 54579 RVA: 0x0012F0E0 File Offset: 0x0012D2E0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A4F7 RID: 42231
			// (set) Token: 0x0600D534 RID: 54580 RVA: 0x0012F0F8 File Offset: 0x0012D2F8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A4F8 RID: 42232
			// (set) Token: 0x0600D535 RID: 54581 RVA: 0x0012F110 File Offset: 0x0012D310
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DFB RID: 3579
		public class TeamMailboxITProParameters : ParametersBase
		{
			// Token: 0x1700A4F9 RID: 42233
			// (set) Token: 0x0600D537 RID: 54583 RVA: 0x0012F130 File Offset: 0x0012D330
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4FA RID: 42234
			// (set) Token: 0x0600D538 RID: 54584 RVA: 0x0012F14E File Offset: 0x0012D34E
			public virtual SwitchParameter DeletedSiteMailbox
			{
				set
				{
					base.PowerSharpParameters["DeletedSiteMailbox"] = value;
				}
			}

			// Token: 0x1700A4FB RID: 42235
			// (set) Token: 0x0600D539 RID: 54585 RVA: 0x0012F166 File Offset: 0x0012D366
			public virtual SwitchParameter BypassOwnerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassOwnerCheck"] = value;
				}
			}

			// Token: 0x1700A4FC RID: 42236
			// (set) Token: 0x0600D53A RID: 54586 RVA: 0x0012F17E File Offset: 0x0012D37E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4FD RID: 42237
			// (set) Token: 0x0600D53B RID: 54587 RVA: 0x0012F19C File Offset: 0x0012D39C
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x1700A4FE RID: 42238
			// (set) Token: 0x0600D53C RID: 54588 RVA: 0x0012F1AF File Offset: 0x0012D3AF
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A4FF RID: 42239
			// (set) Token: 0x0600D53D RID: 54589 RVA: 0x0012F1C2 File Offset: 0x0012D3C2
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A500 RID: 42240
			// (set) Token: 0x0600D53E RID: 54590 RVA: 0x0012F1DA File Offset: 0x0012D3DA
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A501 RID: 42241
			// (set) Token: 0x0600D53F RID: 54591 RVA: 0x0012F1F2 File Offset: 0x0012D3F2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A502 RID: 42242
			// (set) Token: 0x0600D540 RID: 54592 RVA: 0x0012F205 File Offset: 0x0012D405
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A503 RID: 42243
			// (set) Token: 0x0600D541 RID: 54593 RVA: 0x0012F21D File Offset: 0x0012D41D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A504 RID: 42244
			// (set) Token: 0x0600D542 RID: 54594 RVA: 0x0012F235 File Offset: 0x0012D435
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A505 RID: 42245
			// (set) Token: 0x0600D543 RID: 54595 RVA: 0x0012F24D File Offset: 0x0012D44D
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
