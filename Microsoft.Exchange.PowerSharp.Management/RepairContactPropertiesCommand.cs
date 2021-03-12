using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200008C RID: 140
	public class RepairContactPropertiesCommand : SyntheticCommandWithPipelineInputNoOutput<SwitchParameter>
	{
		// Token: 0x060018CC RID: 6348 RVA: 0x00037CA4 File Offset: 0x00035EA4
		private RepairContactPropertiesCommand() : base("Repair-ContactProperties")
		{
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00037CB1 File Offset: 0x00035EB1
		public RepairContactPropertiesCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00037CC0 File Offset: 0x00035EC0
		public virtual RepairContactPropertiesCommand SetParameters(RepairContactPropertiesCommand.DisplayNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00037CCA File Offset: 0x00035ECA
		public virtual RepairContactPropertiesCommand SetParameters(RepairContactPropertiesCommand.ConversationIndexParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00037CD4 File Offset: 0x00035ED4
		public virtual RepairContactPropertiesCommand SetParameters(RepairContactPropertiesCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200008D RID: 141
		public class DisplayNameParameterSetParameters : ParametersBase
		{
			// Token: 0x1700036D RID: 877
			// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00037CDE File Offset: 0x00035EDE
			public virtual SwitchParameter FixDisplayName
			{
				set
				{
					base.PowerSharpParameters["FixDisplayName"] = value;
				}
			}

			// Token: 0x1700036E RID: 878
			// (set) Token: 0x060018D2 RID: 6354 RVA: 0x00037CF6 File Offset: 0x00035EF6
			public virtual SwitchParameter FixConversationIndexTracking
			{
				set
				{
					base.PowerSharpParameters["FixConversationIndexTracking"] = value;
				}
			}

			// Token: 0x1700036F RID: 879
			// (set) Token: 0x060018D3 RID: 6355 RVA: 0x00037D0E File Offset: 0x00035F0E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000370 RID: 880
			// (set) Token: 0x060018D4 RID: 6356 RVA: 0x00037D2C File Offset: 0x00035F2C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000371 RID: 881
			// (set) Token: 0x060018D5 RID: 6357 RVA: 0x00037D3F File Offset: 0x00035F3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000372 RID: 882
			// (set) Token: 0x060018D6 RID: 6358 RVA: 0x00037D57 File Offset: 0x00035F57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000373 RID: 883
			// (set) Token: 0x060018D7 RID: 6359 RVA: 0x00037D6F File Offset: 0x00035F6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000374 RID: 884
			// (set) Token: 0x060018D8 RID: 6360 RVA: 0x00037D87 File Offset: 0x00035F87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200008E RID: 142
		public class ConversationIndexParameterSetParameters : ParametersBase
		{
			// Token: 0x17000375 RID: 885
			// (set) Token: 0x060018DA RID: 6362 RVA: 0x00037DA7 File Offset: 0x00035FA7
			public virtual SwitchParameter FixConversationIndexTracking
			{
				set
				{
					base.PowerSharpParameters["FixConversationIndexTracking"] = value;
				}
			}

			// Token: 0x17000376 RID: 886
			// (set) Token: 0x060018DB RID: 6363 RVA: 0x00037DBF File Offset: 0x00035FBF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000377 RID: 887
			// (set) Token: 0x060018DC RID: 6364 RVA: 0x00037DDD File Offset: 0x00035FDD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000378 RID: 888
			// (set) Token: 0x060018DD RID: 6365 RVA: 0x00037DF0 File Offset: 0x00035FF0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000379 RID: 889
			// (set) Token: 0x060018DE RID: 6366 RVA: 0x00037E08 File Offset: 0x00036008
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700037A RID: 890
			// (set) Token: 0x060018DF RID: 6367 RVA: 0x00037E20 File Offset: 0x00036020
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700037B RID: 891
			// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00037E38 File Offset: 0x00036038
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200008F RID: 143
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700037C RID: 892
			// (set) Token: 0x060018E2 RID: 6370 RVA: 0x00037E58 File Offset: 0x00036058
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700037D RID: 893
			// (set) Token: 0x060018E3 RID: 6371 RVA: 0x00037E76 File Offset: 0x00036076
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700037E RID: 894
			// (set) Token: 0x060018E4 RID: 6372 RVA: 0x00037E89 File Offset: 0x00036089
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700037F RID: 895
			// (set) Token: 0x060018E5 RID: 6373 RVA: 0x00037EA1 File Offset: 0x000360A1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000380 RID: 896
			// (set) Token: 0x060018E6 RID: 6374 RVA: 0x00037EB9 File Offset: 0x000360B9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000381 RID: 897
			// (set) Token: 0x060018E7 RID: 6375 RVA: 0x00037ED1 File Offset: 0x000360D1
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
