using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B60 RID: 2912
	public class GetUMAutoAttendantCommand : SyntheticCommandWithPipelineInput<UMAutoAttendant, UMAutoAttendant>
	{
		// Token: 0x06008D51 RID: 36177 RVA: 0x000CF220 File Offset: 0x000CD420
		private GetUMAutoAttendantCommand() : base("Get-UMAutoAttendant")
		{
		}

		// Token: 0x06008D52 RID: 36178 RVA: 0x000CF22D File Offset: 0x000CD42D
		public GetUMAutoAttendantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008D53 RID: 36179 RVA: 0x000CF23C File Offset: 0x000CD43C
		public virtual GetUMAutoAttendantCommand SetParameters(GetUMAutoAttendantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D54 RID: 36180 RVA: 0x000CF246 File Offset: 0x000CD446
		public virtual GetUMAutoAttendantCommand SetParameters(GetUMAutoAttendantCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B61 RID: 2913
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700624A RID: 25162
			// (set) Token: 0x06008D55 RID: 36181 RVA: 0x000CF250 File Offset: 0x000CD450
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700624B RID: 25163
			// (set) Token: 0x06008D56 RID: 36182 RVA: 0x000CF26E File Offset: 0x000CD46E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700624C RID: 25164
			// (set) Token: 0x06008D57 RID: 36183 RVA: 0x000CF28C File Offset: 0x000CD48C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700624D RID: 25165
			// (set) Token: 0x06008D58 RID: 36184 RVA: 0x000CF29F File Offset: 0x000CD49F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700624E RID: 25166
			// (set) Token: 0x06008D59 RID: 36185 RVA: 0x000CF2B7 File Offset: 0x000CD4B7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700624F RID: 25167
			// (set) Token: 0x06008D5A RID: 36186 RVA: 0x000CF2CF File Offset: 0x000CD4CF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006250 RID: 25168
			// (set) Token: 0x06008D5B RID: 36187 RVA: 0x000CF2E7 File Offset: 0x000CD4E7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B62 RID: 2914
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006251 RID: 25169
			// (set) Token: 0x06008D5D RID: 36189 RVA: 0x000CF307 File Offset: 0x000CD507
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x17006252 RID: 25170
			// (set) Token: 0x06008D5E RID: 36190 RVA: 0x000CF325 File Offset: 0x000CD525
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17006253 RID: 25171
			// (set) Token: 0x06008D5F RID: 36191 RVA: 0x000CF343 File Offset: 0x000CD543
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006254 RID: 25172
			// (set) Token: 0x06008D60 RID: 36192 RVA: 0x000CF361 File Offset: 0x000CD561
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006255 RID: 25173
			// (set) Token: 0x06008D61 RID: 36193 RVA: 0x000CF374 File Offset: 0x000CD574
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006256 RID: 25174
			// (set) Token: 0x06008D62 RID: 36194 RVA: 0x000CF38C File Offset: 0x000CD58C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006257 RID: 25175
			// (set) Token: 0x06008D63 RID: 36195 RVA: 0x000CF3A4 File Offset: 0x000CD5A4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006258 RID: 25176
			// (set) Token: 0x06008D64 RID: 36196 RVA: 0x000CF3BC File Offset: 0x000CD5BC
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
