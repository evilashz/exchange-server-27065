using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DB9 RID: 3513
	public class GetSyncMailContactCommand : SyntheticCommandWithPipelineInput<SyncMailContact, SyncMailContact>
	{
		// Token: 0x0600CB30 RID: 52016 RVA: 0x00121F33 File Offset: 0x00120133
		private GetSyncMailContactCommand() : base("Get-SyncMailContact")
		{
		}

		// Token: 0x0600CB31 RID: 52017 RVA: 0x00121F40 File Offset: 0x00120140
		public GetSyncMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600CB32 RID: 52018 RVA: 0x00121F4F File Offset: 0x0012014F
		public virtual GetSyncMailContactCommand SetParameters(GetSyncMailContactCommand.CookieSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB33 RID: 52019 RVA: 0x00121F59 File Offset: 0x00120159
		public virtual GetSyncMailContactCommand SetParameters(GetSyncMailContactCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB34 RID: 52020 RVA: 0x00121F63 File Offset: 0x00120163
		public virtual GetSyncMailContactCommand SetParameters(GetSyncMailContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB35 RID: 52021 RVA: 0x00121F6D File Offset: 0x0012016D
		public virtual GetSyncMailContactCommand SetParameters(GetSyncMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DBA RID: 3514
		public class CookieSetParameters : ParametersBase
		{
			// Token: 0x17009B77 RID: 39799
			// (set) Token: 0x0600CB36 RID: 52022 RVA: 0x00121F77 File Offset: 0x00120177
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x17009B78 RID: 39800
			// (set) Token: 0x0600CB37 RID: 52023 RVA: 0x00121F8F File Offset: 0x0012018F
			public virtual int Pages
			{
				set
				{
					base.PowerSharpParameters["Pages"] = value;
				}
			}

			// Token: 0x17009B79 RID: 39801
			// (set) Token: 0x0600CB38 RID: 52024 RVA: 0x00121FA7 File Offset: 0x001201A7
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17009B7A RID: 39802
			// (set) Token: 0x0600CB39 RID: 52025 RVA: 0x00121FBF File Offset: 0x001201BF
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009B7B RID: 39803
			// (set) Token: 0x0600CB3A RID: 52026 RVA: 0x00121FD2 File Offset: 0x001201D2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009B7C RID: 39804
			// (set) Token: 0x0600CB3B RID: 52027 RVA: 0x00121FF0 File Offset: 0x001201F0
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009B7D RID: 39805
			// (set) Token: 0x0600CB3C RID: 52028 RVA: 0x00122003 File Offset: 0x00120203
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009B7E RID: 39806
			// (set) Token: 0x0600CB3D RID: 52029 RVA: 0x00122021 File Offset: 0x00120221
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17009B7F RID: 39807
			// (set) Token: 0x0600CB3E RID: 52030 RVA: 0x00122034 File Offset: 0x00120234
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009B80 RID: 39808
			// (set) Token: 0x0600CB3F RID: 52031 RVA: 0x00122047 File Offset: 0x00120247
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009B81 RID: 39809
			// (set) Token: 0x0600CB40 RID: 52032 RVA: 0x0012205F File Offset: 0x0012025F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009B82 RID: 39810
			// (set) Token: 0x0600CB41 RID: 52033 RVA: 0x00122077 File Offset: 0x00120277
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009B83 RID: 39811
			// (set) Token: 0x0600CB42 RID: 52034 RVA: 0x0012208F File Offset: 0x0012028F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DBB RID: 3515
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x17009B84 RID: 39812
			// (set) Token: 0x0600CB44 RID: 52036 RVA: 0x001220AF File Offset: 0x001202AF
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17009B85 RID: 39813
			// (set) Token: 0x0600CB45 RID: 52037 RVA: 0x001220C7 File Offset: 0x001202C7
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17009B86 RID: 39814
			// (set) Token: 0x0600CB46 RID: 52038 RVA: 0x001220DF File Offset: 0x001202DF
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009B87 RID: 39815
			// (set) Token: 0x0600CB47 RID: 52039 RVA: 0x001220F7 File Offset: 0x001202F7
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17009B88 RID: 39816
			// (set) Token: 0x0600CB48 RID: 52040 RVA: 0x0012210A File Offset: 0x0012030A
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x17009B89 RID: 39817
			// (set) Token: 0x0600CB49 RID: 52041 RVA: 0x0012211D File Offset: 0x0012031D
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17009B8A RID: 39818
			// (set) Token: 0x0600CB4A RID: 52042 RVA: 0x00122135 File Offset: 0x00120335
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009B8B RID: 39819
			// (set) Token: 0x0600CB4B RID: 52043 RVA: 0x00122148 File Offset: 0x00120348
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009B8C RID: 39820
			// (set) Token: 0x0600CB4C RID: 52044 RVA: 0x00122166 File Offset: 0x00120366
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009B8D RID: 39821
			// (set) Token: 0x0600CB4D RID: 52045 RVA: 0x00122179 File Offset: 0x00120379
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009B8E RID: 39822
			// (set) Token: 0x0600CB4E RID: 52046 RVA: 0x00122197 File Offset: 0x00120397
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17009B8F RID: 39823
			// (set) Token: 0x0600CB4F RID: 52047 RVA: 0x001221AA File Offset: 0x001203AA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009B90 RID: 39824
			// (set) Token: 0x0600CB50 RID: 52048 RVA: 0x001221BD File Offset: 0x001203BD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009B91 RID: 39825
			// (set) Token: 0x0600CB51 RID: 52049 RVA: 0x001221D5 File Offset: 0x001203D5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009B92 RID: 39826
			// (set) Token: 0x0600CB52 RID: 52050 RVA: 0x001221ED File Offset: 0x001203ED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009B93 RID: 39827
			// (set) Token: 0x0600CB53 RID: 52051 RVA: 0x00122205 File Offset: 0x00120405
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DBC RID: 3516
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17009B94 RID: 39828
			// (set) Token: 0x0600CB55 RID: 52053 RVA: 0x00122225 File Offset: 0x00120425
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17009B95 RID: 39829
			// (set) Token: 0x0600CB56 RID: 52054 RVA: 0x0012223D File Offset: 0x0012043D
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17009B96 RID: 39830
			// (set) Token: 0x0600CB57 RID: 52055 RVA: 0x00122255 File Offset: 0x00120455
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009B97 RID: 39831
			// (set) Token: 0x0600CB58 RID: 52056 RVA: 0x0012226D File Offset: 0x0012046D
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17009B98 RID: 39832
			// (set) Token: 0x0600CB59 RID: 52057 RVA: 0x00122280 File Offset: 0x00120480
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009B99 RID: 39833
			// (set) Token: 0x0600CB5A RID: 52058 RVA: 0x0012229E File Offset: 0x0012049E
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17009B9A RID: 39834
			// (set) Token: 0x0600CB5B RID: 52059 RVA: 0x001222B6 File Offset: 0x001204B6
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009B9B RID: 39835
			// (set) Token: 0x0600CB5C RID: 52060 RVA: 0x001222C9 File Offset: 0x001204C9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009B9C RID: 39836
			// (set) Token: 0x0600CB5D RID: 52061 RVA: 0x001222E7 File Offset: 0x001204E7
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009B9D RID: 39837
			// (set) Token: 0x0600CB5E RID: 52062 RVA: 0x001222FA File Offset: 0x001204FA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009B9E RID: 39838
			// (set) Token: 0x0600CB5F RID: 52063 RVA: 0x00122318 File Offset: 0x00120518
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17009B9F RID: 39839
			// (set) Token: 0x0600CB60 RID: 52064 RVA: 0x0012232B File Offset: 0x0012052B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009BA0 RID: 39840
			// (set) Token: 0x0600CB61 RID: 52065 RVA: 0x0012233E File Offset: 0x0012053E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009BA1 RID: 39841
			// (set) Token: 0x0600CB62 RID: 52066 RVA: 0x00122356 File Offset: 0x00120556
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009BA2 RID: 39842
			// (set) Token: 0x0600CB63 RID: 52067 RVA: 0x0012236E File Offset: 0x0012056E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009BA3 RID: 39843
			// (set) Token: 0x0600CB64 RID: 52068 RVA: 0x00122386 File Offset: 0x00120586
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DBD RID: 3517
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17009BA4 RID: 39844
			// (set) Token: 0x0600CB66 RID: 52070 RVA: 0x001223A6 File Offset: 0x001205A6
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17009BA5 RID: 39845
			// (set) Token: 0x0600CB67 RID: 52071 RVA: 0x001223BE File Offset: 0x001205BE
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009BA6 RID: 39846
			// (set) Token: 0x0600CB68 RID: 52072 RVA: 0x001223D1 File Offset: 0x001205D1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009BA7 RID: 39847
			// (set) Token: 0x0600CB69 RID: 52073 RVA: 0x001223EF File Offset: 0x001205EF
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009BA8 RID: 39848
			// (set) Token: 0x0600CB6A RID: 52074 RVA: 0x00122402 File Offset: 0x00120602
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009BA9 RID: 39849
			// (set) Token: 0x0600CB6B RID: 52075 RVA: 0x00122420 File Offset: 0x00120620
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17009BAA RID: 39850
			// (set) Token: 0x0600CB6C RID: 52076 RVA: 0x00122433 File Offset: 0x00120633
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009BAB RID: 39851
			// (set) Token: 0x0600CB6D RID: 52077 RVA: 0x00122446 File Offset: 0x00120646
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009BAC RID: 39852
			// (set) Token: 0x0600CB6E RID: 52078 RVA: 0x0012245E File Offset: 0x0012065E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009BAD RID: 39853
			// (set) Token: 0x0600CB6F RID: 52079 RVA: 0x00122476 File Offset: 0x00120676
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009BAE RID: 39854
			// (set) Token: 0x0600CB70 RID: 52080 RVA: 0x0012248E File Offset: 0x0012068E
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
