using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D57 RID: 3415
	public class GetRemoteMailboxCommand : SyntheticCommandWithPipelineInput<RemoteMailboxIdParameter, RemoteMailboxIdParameter>
	{
		// Token: 0x0600B489 RID: 46217 RVA: 0x00104017 File Offset: 0x00102217
		private GetRemoteMailboxCommand() : base("Get-RemoteMailbox")
		{
		}

		// Token: 0x0600B48A RID: 46218 RVA: 0x00104024 File Offset: 0x00102224
		public GetRemoteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B48B RID: 46219 RVA: 0x00104033 File Offset: 0x00102233
		public virtual GetRemoteMailboxCommand SetParameters(GetRemoteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B48C RID: 46220 RVA: 0x0010403D File Offset: 0x0010223D
		public virtual GetRemoteMailboxCommand SetParameters(GetRemoteMailboxCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B48D RID: 46221 RVA: 0x00104047 File Offset: 0x00102247
		public virtual GetRemoteMailboxCommand SetParameters(GetRemoteMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D58 RID: 3416
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008594 RID: 34196
			// (set) Token: 0x0600B48E RID: 46222 RVA: 0x00104051 File Offset: 0x00102251
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008595 RID: 34197
			// (set) Token: 0x0600B48F RID: 46223 RVA: 0x0010406F File Offset: 0x0010226F
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008596 RID: 34198
			// (set) Token: 0x0600B490 RID: 46224 RVA: 0x00104087 File Offset: 0x00102287
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008597 RID: 34199
			// (set) Token: 0x0600B491 RID: 46225 RVA: 0x0010409A File Offset: 0x0010229A
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17008598 RID: 34200
			// (set) Token: 0x0600B492 RID: 46226 RVA: 0x001040AD File Offset: 0x001022AD
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008599 RID: 34201
			// (set) Token: 0x0600B493 RID: 46227 RVA: 0x001040C5 File Offset: 0x001022C5
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700859A RID: 34202
			// (set) Token: 0x0600B494 RID: 46228 RVA: 0x001040D8 File Offset: 0x001022D8
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700859B RID: 34203
			// (set) Token: 0x0600B495 RID: 46229 RVA: 0x001040F0 File Offset: 0x001022F0
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700859C RID: 34204
			// (set) Token: 0x0600B496 RID: 46230 RVA: 0x00104108 File Offset: 0x00102308
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700859D RID: 34205
			// (set) Token: 0x0600B497 RID: 46231 RVA: 0x0010411B File Offset: 0x0010231B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700859E RID: 34206
			// (set) Token: 0x0600B498 RID: 46232 RVA: 0x00104133 File Offset: 0x00102333
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700859F RID: 34207
			// (set) Token: 0x0600B499 RID: 46233 RVA: 0x0010414B File Offset: 0x0010234B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170085A0 RID: 34208
			// (set) Token: 0x0600B49A RID: 46234 RVA: 0x00104163 File Offset: 0x00102363
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D59 RID: 3417
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170085A1 RID: 34209
			// (set) Token: 0x0600B49C RID: 46236 RVA: 0x00104183 File Offset: 0x00102383
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170085A2 RID: 34210
			// (set) Token: 0x0600B49D RID: 46237 RVA: 0x00104196 File Offset: 0x00102396
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170085A3 RID: 34211
			// (set) Token: 0x0600B49E RID: 46238 RVA: 0x001041B4 File Offset: 0x001023B4
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170085A4 RID: 34212
			// (set) Token: 0x0600B49F RID: 46239 RVA: 0x001041CC File Offset: 0x001023CC
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170085A5 RID: 34213
			// (set) Token: 0x0600B4A0 RID: 46240 RVA: 0x001041DF File Offset: 0x001023DF
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170085A6 RID: 34214
			// (set) Token: 0x0600B4A1 RID: 46241 RVA: 0x001041F2 File Offset: 0x001023F2
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170085A7 RID: 34215
			// (set) Token: 0x0600B4A2 RID: 46242 RVA: 0x0010420A File Offset: 0x0010240A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170085A8 RID: 34216
			// (set) Token: 0x0600B4A3 RID: 46243 RVA: 0x0010421D File Offset: 0x0010241D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170085A9 RID: 34217
			// (set) Token: 0x0600B4A4 RID: 46244 RVA: 0x00104235 File Offset: 0x00102435
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170085AA RID: 34218
			// (set) Token: 0x0600B4A5 RID: 46245 RVA: 0x0010424D File Offset: 0x0010244D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170085AB RID: 34219
			// (set) Token: 0x0600B4A6 RID: 46246 RVA: 0x00104260 File Offset: 0x00102460
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170085AC RID: 34220
			// (set) Token: 0x0600B4A7 RID: 46247 RVA: 0x00104278 File Offset: 0x00102478
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170085AD RID: 34221
			// (set) Token: 0x0600B4A8 RID: 46248 RVA: 0x00104290 File Offset: 0x00102490
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170085AE RID: 34222
			// (set) Token: 0x0600B4A9 RID: 46249 RVA: 0x001042A8 File Offset: 0x001024A8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D5A RID: 3418
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170085AF RID: 34223
			// (set) Token: 0x0600B4AB RID: 46251 RVA: 0x001042C8 File Offset: 0x001024C8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RemoteMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170085B0 RID: 34224
			// (set) Token: 0x0600B4AC RID: 46252 RVA: 0x001042E6 File Offset: 0x001024E6
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170085B1 RID: 34225
			// (set) Token: 0x0600B4AD RID: 46253 RVA: 0x00104304 File Offset: 0x00102504
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170085B2 RID: 34226
			// (set) Token: 0x0600B4AE RID: 46254 RVA: 0x0010431C File Offset: 0x0010251C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170085B3 RID: 34227
			// (set) Token: 0x0600B4AF RID: 46255 RVA: 0x0010432F File Offset: 0x0010252F
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170085B4 RID: 34228
			// (set) Token: 0x0600B4B0 RID: 46256 RVA: 0x00104342 File Offset: 0x00102542
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170085B5 RID: 34229
			// (set) Token: 0x0600B4B1 RID: 46257 RVA: 0x0010435A File Offset: 0x0010255A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170085B6 RID: 34230
			// (set) Token: 0x0600B4B2 RID: 46258 RVA: 0x0010436D File Offset: 0x0010256D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170085B7 RID: 34231
			// (set) Token: 0x0600B4B3 RID: 46259 RVA: 0x00104385 File Offset: 0x00102585
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170085B8 RID: 34232
			// (set) Token: 0x0600B4B4 RID: 46260 RVA: 0x0010439D File Offset: 0x0010259D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170085B9 RID: 34233
			// (set) Token: 0x0600B4B5 RID: 46261 RVA: 0x001043B0 File Offset: 0x001025B0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170085BA RID: 34234
			// (set) Token: 0x0600B4B6 RID: 46262 RVA: 0x001043C8 File Offset: 0x001025C8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170085BB RID: 34235
			// (set) Token: 0x0600B4B7 RID: 46263 RVA: 0x001043E0 File Offset: 0x001025E0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170085BC RID: 34236
			// (set) Token: 0x0600B4B8 RID: 46264 RVA: 0x001043F8 File Offset: 0x001025F8
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
