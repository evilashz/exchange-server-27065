using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007A3 RID: 1955
	public class GetAcceptedDomainCommand : SyntheticCommandWithPipelineInput<AcceptedDomain, AcceptedDomain>
	{
		// Token: 0x06006234 RID: 25140 RVA: 0x00096E04 File Offset: 0x00095004
		private GetAcceptedDomainCommand() : base("Get-AcceptedDomain")
		{
		}

		// Token: 0x06006235 RID: 25141 RVA: 0x00096E11 File Offset: 0x00095011
		public GetAcceptedDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006236 RID: 25142 RVA: 0x00096E20 File Offset: 0x00095020
		public virtual GetAcceptedDomainCommand SetParameters(GetAcceptedDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006237 RID: 25143 RVA: 0x00096E2A File Offset: 0x0009502A
		public virtual GetAcceptedDomainCommand SetParameters(GetAcceptedDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007A4 RID: 1956
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003EA7 RID: 16039
			// (set) Token: 0x06006238 RID: 25144 RVA: 0x00096E34 File Offset: 0x00095034
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17003EA8 RID: 16040
			// (set) Token: 0x06006239 RID: 25145 RVA: 0x00096E4C File Offset: 0x0009504C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17003EA9 RID: 16041
			// (set) Token: 0x0600623A RID: 25146 RVA: 0x00096E5F File Offset: 0x0009505F
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17003EAA RID: 16042
			// (set) Token: 0x0600623B RID: 25147 RVA: 0x00096E72 File Offset: 0x00095072
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003EAB RID: 16043
			// (set) Token: 0x0600623C RID: 25148 RVA: 0x00096E90 File Offset: 0x00095090
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003EAC RID: 16044
			// (set) Token: 0x0600623D RID: 25149 RVA: 0x00096EA3 File Offset: 0x000950A3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003EAD RID: 16045
			// (set) Token: 0x0600623E RID: 25150 RVA: 0x00096EBB File Offset: 0x000950BB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003EAE RID: 16046
			// (set) Token: 0x0600623F RID: 25151 RVA: 0x00096ED3 File Offset: 0x000950D3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003EAF RID: 16047
			// (set) Token: 0x06006240 RID: 25152 RVA: 0x00096EEB File Offset: 0x000950EB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007A5 RID: 1957
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003EB0 RID: 16048
			// (set) Token: 0x06006242 RID: 25154 RVA: 0x00096F0B File Offset: 0x0009510B
			public virtual AcceptedDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003EB1 RID: 16049
			// (set) Token: 0x06006243 RID: 25155 RVA: 0x00096F1E File Offset: 0x0009511E
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17003EB2 RID: 16050
			// (set) Token: 0x06006244 RID: 25156 RVA: 0x00096F36 File Offset: 0x00095136
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17003EB3 RID: 16051
			// (set) Token: 0x06006245 RID: 25157 RVA: 0x00096F49 File Offset: 0x00095149
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17003EB4 RID: 16052
			// (set) Token: 0x06006246 RID: 25158 RVA: 0x00096F5C File Offset: 0x0009515C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003EB5 RID: 16053
			// (set) Token: 0x06006247 RID: 25159 RVA: 0x00096F7A File Offset: 0x0009517A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003EB6 RID: 16054
			// (set) Token: 0x06006248 RID: 25160 RVA: 0x00096F8D File Offset: 0x0009518D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003EB7 RID: 16055
			// (set) Token: 0x06006249 RID: 25161 RVA: 0x00096FA5 File Offset: 0x000951A5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003EB8 RID: 16056
			// (set) Token: 0x0600624A RID: 25162 RVA: 0x00096FBD File Offset: 0x000951BD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003EB9 RID: 16057
			// (set) Token: 0x0600624B RID: 25163 RVA: 0x00096FD5 File Offset: 0x000951D5
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
