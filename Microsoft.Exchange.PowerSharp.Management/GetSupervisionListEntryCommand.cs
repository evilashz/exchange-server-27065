using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E33 RID: 3635
	public class GetSupervisionListEntryCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x0600D82A RID: 55338 RVA: 0x00132F8F File Offset: 0x0013118F
		private GetSupervisionListEntryCommand() : base("Get-SupervisionListEntry")
		{
		}

		// Token: 0x0600D82B RID: 55339 RVA: 0x00132F9C File Offset: 0x0013119C
		public GetSupervisionListEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D82C RID: 55340 RVA: 0x00132FAB File Offset: 0x001311AB
		public virtual GetSupervisionListEntryCommand SetParameters(GetSupervisionListEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D82D RID: 55341 RVA: 0x00132FB5 File Offset: 0x001311B5
		public virtual GetSupervisionListEntryCommand SetParameters(GetSupervisionListEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E34 RID: 3636
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A77D RID: 42877
			// (set) Token: 0x0600D82E RID: 55342 RVA: 0x00132FBF File Offset: 0x001311BF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A77E RID: 42878
			// (set) Token: 0x0600D82F RID: 55343 RVA: 0x00132FDD File Offset: 0x001311DD
			public virtual string Tag
			{
				set
				{
					base.PowerSharpParameters["Tag"] = value;
				}
			}

			// Token: 0x1700A77F RID: 42879
			// (set) Token: 0x0600D830 RID: 55344 RVA: 0x00132FF0 File Offset: 0x001311F0
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A780 RID: 42880
			// (set) Token: 0x0600D831 RID: 55345 RVA: 0x00133003 File Offset: 0x00131203
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A781 RID: 42881
			// (set) Token: 0x0600D832 RID: 55346 RVA: 0x0013301B File Offset: 0x0013121B
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A782 RID: 42882
			// (set) Token: 0x0600D833 RID: 55347 RVA: 0x00133033 File Offset: 0x00131233
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A783 RID: 42883
			// (set) Token: 0x0600D834 RID: 55348 RVA: 0x00133046 File Offset: 0x00131246
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A784 RID: 42884
			// (set) Token: 0x0600D835 RID: 55349 RVA: 0x0013305E File Offset: 0x0013125E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A785 RID: 42885
			// (set) Token: 0x0600D836 RID: 55350 RVA: 0x00133076 File Offset: 0x00131276
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A786 RID: 42886
			// (set) Token: 0x0600D837 RID: 55351 RVA: 0x0013308E File Offset: 0x0013128E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E35 RID: 3637
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A787 RID: 42887
			// (set) Token: 0x0600D839 RID: 55353 RVA: 0x001330AE File Offset: 0x001312AE
			public virtual string Tag
			{
				set
				{
					base.PowerSharpParameters["Tag"] = value;
				}
			}

			// Token: 0x1700A788 RID: 42888
			// (set) Token: 0x0600D83A RID: 55354 RVA: 0x001330C1 File Offset: 0x001312C1
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A789 RID: 42889
			// (set) Token: 0x0600D83B RID: 55355 RVA: 0x001330D4 File Offset: 0x001312D4
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A78A RID: 42890
			// (set) Token: 0x0600D83C RID: 55356 RVA: 0x001330EC File Offset: 0x001312EC
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A78B RID: 42891
			// (set) Token: 0x0600D83D RID: 55357 RVA: 0x00133104 File Offset: 0x00131304
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A78C RID: 42892
			// (set) Token: 0x0600D83E RID: 55358 RVA: 0x00133117 File Offset: 0x00131317
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A78D RID: 42893
			// (set) Token: 0x0600D83F RID: 55359 RVA: 0x0013312F File Offset: 0x0013132F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A78E RID: 42894
			// (set) Token: 0x0600D840 RID: 55360 RVA: 0x00133147 File Offset: 0x00131347
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A78F RID: 42895
			// (set) Token: 0x0600D841 RID: 55361 RVA: 0x0013315F File Offset: 0x0013135F
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
