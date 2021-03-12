using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DCF RID: 3535
	public class GetSyncMailUserCommand : SyntheticCommandWithPipelineInput<MailUserIdParameter, MailUserIdParameter>
	{
		// Token: 0x0600D2D0 RID: 53968 RVA: 0x0012BF23 File Offset: 0x0012A123
		private GetSyncMailUserCommand() : base("Get-SyncMailUser")
		{
		}

		// Token: 0x0600D2D1 RID: 53969 RVA: 0x0012BF30 File Offset: 0x0012A130
		public GetSyncMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D2D2 RID: 53970 RVA: 0x0012BF3F File Offset: 0x0012A13F
		public virtual GetSyncMailUserCommand SetParameters(GetSyncMailUserCommand.CookieSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D2D3 RID: 53971 RVA: 0x0012BF49 File Offset: 0x0012A149
		public virtual GetSyncMailUserCommand SetParameters(GetSyncMailUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D2D4 RID: 53972 RVA: 0x0012BF53 File Offset: 0x0012A153
		public virtual GetSyncMailUserCommand SetParameters(GetSyncMailUserCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D2D5 RID: 53973 RVA: 0x0012BF5D File Offset: 0x0012A15D
		public virtual GetSyncMailUserCommand SetParameters(GetSyncMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DD0 RID: 3536
		public class CookieSetParameters : ParametersBase
		{
			// Token: 0x1700A2EB RID: 41707
			// (set) Token: 0x0600D2D6 RID: 53974 RVA: 0x0012BF67 File Offset: 0x0012A167
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x1700A2EC RID: 41708
			// (set) Token: 0x0600D2D7 RID: 53975 RVA: 0x0012BF7F File Offset: 0x0012A17F
			public virtual int Pages
			{
				set
				{
					base.PowerSharpParameters["Pages"] = value;
				}
			}

			// Token: 0x1700A2ED RID: 41709
			// (set) Token: 0x0600D2D8 RID: 53976 RVA: 0x0012BF97 File Offset: 0x0012A197
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x1700A2EE RID: 41710
			// (set) Token: 0x0600D2D9 RID: 53977 RVA: 0x0012BFAF File Offset: 0x0012A1AF
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A2EF RID: 41711
			// (set) Token: 0x0600D2DA RID: 53978 RVA: 0x0012BFC2 File Offset: 0x0012A1C2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A2F0 RID: 41712
			// (set) Token: 0x0600D2DB RID: 53979 RVA: 0x0012BFE0 File Offset: 0x0012A1E0
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A2F1 RID: 41713
			// (set) Token: 0x0600D2DC RID: 53980 RVA: 0x0012BFF3 File Offset: 0x0012A1F3
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A2F2 RID: 41714
			// (set) Token: 0x0600D2DD RID: 53981 RVA: 0x0012C011 File Offset: 0x0012A211
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A2F3 RID: 41715
			// (set) Token: 0x0600D2DE RID: 53982 RVA: 0x0012C024 File Offset: 0x0012A224
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A2F4 RID: 41716
			// (set) Token: 0x0600D2DF RID: 53983 RVA: 0x0012C037 File Offset: 0x0012A237
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A2F5 RID: 41717
			// (set) Token: 0x0600D2E0 RID: 53984 RVA: 0x0012C04F File Offset: 0x0012A24F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A2F6 RID: 41718
			// (set) Token: 0x0600D2E1 RID: 53985 RVA: 0x0012C067 File Offset: 0x0012A267
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A2F7 RID: 41719
			// (set) Token: 0x0600D2E2 RID: 53986 RVA: 0x0012C07F File Offset: 0x0012A27F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DD1 RID: 3537
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A2F8 RID: 41720
			// (set) Token: 0x0600D2E4 RID: 53988 RVA: 0x0012C09F File Offset: 0x0012A29F
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A2F9 RID: 41721
			// (set) Token: 0x0600D2E5 RID: 53989 RVA: 0x0012C0B7 File Offset: 0x0012A2B7
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A2FA RID: 41722
			// (set) Token: 0x0600D2E6 RID: 53990 RVA: 0x0012C0CF File Offset: 0x0012A2CF
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A2FB RID: 41723
			// (set) Token: 0x0600D2E7 RID: 53991 RVA: 0x0012C0E7 File Offset: 0x0012A2E7
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A2FC RID: 41724
			// (set) Token: 0x0600D2E8 RID: 53992 RVA: 0x0012C0FA File Offset: 0x0012A2FA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A2FD RID: 41725
			// (set) Token: 0x0600D2E9 RID: 53993 RVA: 0x0012C118 File Offset: 0x0012A318
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x1700A2FE RID: 41726
			// (set) Token: 0x0600D2EA RID: 53994 RVA: 0x0012C130 File Offset: 0x0012A330
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A2FF RID: 41727
			// (set) Token: 0x0600D2EB RID: 53995 RVA: 0x0012C143 File Offset: 0x0012A343
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A300 RID: 41728
			// (set) Token: 0x0600D2EC RID: 53996 RVA: 0x0012C161 File Offset: 0x0012A361
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A301 RID: 41729
			// (set) Token: 0x0600D2ED RID: 53997 RVA: 0x0012C174 File Offset: 0x0012A374
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A302 RID: 41730
			// (set) Token: 0x0600D2EE RID: 53998 RVA: 0x0012C192 File Offset: 0x0012A392
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A303 RID: 41731
			// (set) Token: 0x0600D2EF RID: 53999 RVA: 0x0012C1A5 File Offset: 0x0012A3A5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A304 RID: 41732
			// (set) Token: 0x0600D2F0 RID: 54000 RVA: 0x0012C1B8 File Offset: 0x0012A3B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A305 RID: 41733
			// (set) Token: 0x0600D2F1 RID: 54001 RVA: 0x0012C1D0 File Offset: 0x0012A3D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A306 RID: 41734
			// (set) Token: 0x0600D2F2 RID: 54002 RVA: 0x0012C1E8 File Offset: 0x0012A3E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A307 RID: 41735
			// (set) Token: 0x0600D2F3 RID: 54003 RVA: 0x0012C200 File Offset: 0x0012A400
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DD2 RID: 3538
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x1700A308 RID: 41736
			// (set) Token: 0x0600D2F5 RID: 54005 RVA: 0x0012C220 File Offset: 0x0012A420
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A309 RID: 41737
			// (set) Token: 0x0600D2F6 RID: 54006 RVA: 0x0012C238 File Offset: 0x0012A438
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A30A RID: 41738
			// (set) Token: 0x0600D2F7 RID: 54007 RVA: 0x0012C250 File Offset: 0x0012A450
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A30B RID: 41739
			// (set) Token: 0x0600D2F8 RID: 54008 RVA: 0x0012C268 File Offset: 0x0012A468
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A30C RID: 41740
			// (set) Token: 0x0600D2F9 RID: 54009 RVA: 0x0012C27B File Offset: 0x0012A47B
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x1700A30D RID: 41741
			// (set) Token: 0x0600D2FA RID: 54010 RVA: 0x0012C28E File Offset: 0x0012A48E
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x1700A30E RID: 41742
			// (set) Token: 0x0600D2FB RID: 54011 RVA: 0x0012C2A6 File Offset: 0x0012A4A6
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A30F RID: 41743
			// (set) Token: 0x0600D2FC RID: 54012 RVA: 0x0012C2B9 File Offset: 0x0012A4B9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A310 RID: 41744
			// (set) Token: 0x0600D2FD RID: 54013 RVA: 0x0012C2D7 File Offset: 0x0012A4D7
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A311 RID: 41745
			// (set) Token: 0x0600D2FE RID: 54014 RVA: 0x0012C2EA File Offset: 0x0012A4EA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A312 RID: 41746
			// (set) Token: 0x0600D2FF RID: 54015 RVA: 0x0012C308 File Offset: 0x0012A508
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A313 RID: 41747
			// (set) Token: 0x0600D300 RID: 54016 RVA: 0x0012C31B File Offset: 0x0012A51B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A314 RID: 41748
			// (set) Token: 0x0600D301 RID: 54017 RVA: 0x0012C32E File Offset: 0x0012A52E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A315 RID: 41749
			// (set) Token: 0x0600D302 RID: 54018 RVA: 0x0012C346 File Offset: 0x0012A546
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A316 RID: 41750
			// (set) Token: 0x0600D303 RID: 54019 RVA: 0x0012C35E File Offset: 0x0012A55E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A317 RID: 41751
			// (set) Token: 0x0600D304 RID: 54020 RVA: 0x0012C376 File Offset: 0x0012A576
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DD3 RID: 3539
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A318 RID: 41752
			// (set) Token: 0x0600D306 RID: 54022 RVA: 0x0012C396 File Offset: 0x0012A596
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x1700A319 RID: 41753
			// (set) Token: 0x0600D307 RID: 54023 RVA: 0x0012C3AE File Offset: 0x0012A5AE
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A31A RID: 41754
			// (set) Token: 0x0600D308 RID: 54024 RVA: 0x0012C3C1 File Offset: 0x0012A5C1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A31B RID: 41755
			// (set) Token: 0x0600D309 RID: 54025 RVA: 0x0012C3DF File Offset: 0x0012A5DF
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A31C RID: 41756
			// (set) Token: 0x0600D30A RID: 54026 RVA: 0x0012C3F2 File Offset: 0x0012A5F2
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A31D RID: 41757
			// (set) Token: 0x0600D30B RID: 54027 RVA: 0x0012C410 File Offset: 0x0012A610
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A31E RID: 41758
			// (set) Token: 0x0600D30C RID: 54028 RVA: 0x0012C423 File Offset: 0x0012A623
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A31F RID: 41759
			// (set) Token: 0x0600D30D RID: 54029 RVA: 0x0012C436 File Offset: 0x0012A636
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A320 RID: 41760
			// (set) Token: 0x0600D30E RID: 54030 RVA: 0x0012C44E File Offset: 0x0012A64E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A321 RID: 41761
			// (set) Token: 0x0600D30F RID: 54031 RVA: 0x0012C466 File Offset: 0x0012A666
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A322 RID: 41762
			// (set) Token: 0x0600D310 RID: 54032 RVA: 0x0012C47E File Offset: 0x0012A67E
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
