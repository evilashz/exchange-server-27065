using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BA2 RID: 2978
	public class GetUserPhotoCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600904D RID: 36941 RVA: 0x000D3078 File Offset: 0x000D1278
		private GetUserPhotoCommand() : base("Get-UserPhoto")
		{
		}

		// Token: 0x0600904E RID: 36942 RVA: 0x000D3085 File Offset: 0x000D1285
		public GetUserPhotoCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600904F RID: 36943 RVA: 0x000D3094 File Offset: 0x000D1294
		public virtual GetUserPhotoCommand SetParameters(GetUserPhotoCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009050 RID: 36944 RVA: 0x000D309E File Offset: 0x000D129E
		public virtual GetUserPhotoCommand SetParameters(GetUserPhotoCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009051 RID: 36945 RVA: 0x000D30A8 File Offset: 0x000D12A8
		public virtual GetUserPhotoCommand SetParameters(GetUserPhotoCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BA3 RID: 2979
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170064C2 RID: 25794
			// (set) Token: 0x06009052 RID: 36946 RVA: 0x000D30B2 File Offset: 0x000D12B2
			public virtual SwitchParameter Preview
			{
				set
				{
					base.PowerSharpParameters["Preview"] = value;
				}
			}

			// Token: 0x170064C3 RID: 25795
			// (set) Token: 0x06009053 RID: 36947 RVA: 0x000D30CA File Offset: 0x000D12CA
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170064C4 RID: 25796
			// (set) Token: 0x06009054 RID: 36948 RVA: 0x000D30DD File Offset: 0x000D12DD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170064C5 RID: 25797
			// (set) Token: 0x06009055 RID: 36949 RVA: 0x000D30FB File Offset: 0x000D12FB
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170064C6 RID: 25798
			// (set) Token: 0x06009056 RID: 36950 RVA: 0x000D310E File Offset: 0x000D130E
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170064C7 RID: 25799
			// (set) Token: 0x06009057 RID: 36951 RVA: 0x000D3121 File Offset: 0x000D1321
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170064C8 RID: 25800
			// (set) Token: 0x06009058 RID: 36952 RVA: 0x000D313F File Offset: 0x000D133F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170064C9 RID: 25801
			// (set) Token: 0x06009059 RID: 36953 RVA: 0x000D3157 File Offset: 0x000D1357
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170064CA RID: 25802
			// (set) Token: 0x0600905A RID: 36954 RVA: 0x000D316A File Offset: 0x000D136A
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170064CB RID: 25803
			// (set) Token: 0x0600905B RID: 36955 RVA: 0x000D3182 File Offset: 0x000D1382
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170064CC RID: 25804
			// (set) Token: 0x0600905C RID: 36956 RVA: 0x000D319A File Offset: 0x000D139A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064CD RID: 25805
			// (set) Token: 0x0600905D RID: 36957 RVA: 0x000D31AD File Offset: 0x000D13AD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064CE RID: 25806
			// (set) Token: 0x0600905E RID: 36958 RVA: 0x000D31C5 File Offset: 0x000D13C5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064CF RID: 25807
			// (set) Token: 0x0600905F RID: 36959 RVA: 0x000D31DD File Offset: 0x000D13DD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064D0 RID: 25808
			// (set) Token: 0x06009060 RID: 36960 RVA: 0x000D31F5 File Offset: 0x000D13F5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BA4 RID: 2980
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170064D1 RID: 25809
			// (set) Token: 0x06009062 RID: 36962 RVA: 0x000D3215 File Offset: 0x000D1415
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170064D2 RID: 25810
			// (set) Token: 0x06009063 RID: 36963 RVA: 0x000D3228 File Offset: 0x000D1428
			public virtual SwitchParameter Preview
			{
				set
				{
					base.PowerSharpParameters["Preview"] = value;
				}
			}

			// Token: 0x170064D3 RID: 25811
			// (set) Token: 0x06009064 RID: 36964 RVA: 0x000D3240 File Offset: 0x000D1440
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170064D4 RID: 25812
			// (set) Token: 0x06009065 RID: 36965 RVA: 0x000D3253 File Offset: 0x000D1453
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170064D5 RID: 25813
			// (set) Token: 0x06009066 RID: 36966 RVA: 0x000D3271 File Offset: 0x000D1471
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170064D6 RID: 25814
			// (set) Token: 0x06009067 RID: 36967 RVA: 0x000D3284 File Offset: 0x000D1484
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170064D7 RID: 25815
			// (set) Token: 0x06009068 RID: 36968 RVA: 0x000D3297 File Offset: 0x000D1497
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170064D8 RID: 25816
			// (set) Token: 0x06009069 RID: 36969 RVA: 0x000D32B5 File Offset: 0x000D14B5
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170064D9 RID: 25817
			// (set) Token: 0x0600906A RID: 36970 RVA: 0x000D32CD File Offset: 0x000D14CD
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170064DA RID: 25818
			// (set) Token: 0x0600906B RID: 36971 RVA: 0x000D32E0 File Offset: 0x000D14E0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170064DB RID: 25819
			// (set) Token: 0x0600906C RID: 36972 RVA: 0x000D32F8 File Offset: 0x000D14F8
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170064DC RID: 25820
			// (set) Token: 0x0600906D RID: 36973 RVA: 0x000D3310 File Offset: 0x000D1510
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064DD RID: 25821
			// (set) Token: 0x0600906E RID: 36974 RVA: 0x000D3323 File Offset: 0x000D1523
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064DE RID: 25822
			// (set) Token: 0x0600906F RID: 36975 RVA: 0x000D333B File Offset: 0x000D153B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064DF RID: 25823
			// (set) Token: 0x06009070 RID: 36976 RVA: 0x000D3353 File Offset: 0x000D1553
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064E0 RID: 25824
			// (set) Token: 0x06009071 RID: 36977 RVA: 0x000D336B File Offset: 0x000D156B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BA5 RID: 2981
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170064E1 RID: 25825
			// (set) Token: 0x06009073 RID: 36979 RVA: 0x000D338B File Offset: 0x000D158B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170064E2 RID: 25826
			// (set) Token: 0x06009074 RID: 36980 RVA: 0x000D33A9 File Offset: 0x000D15A9
			public virtual SwitchParameter Preview
			{
				set
				{
					base.PowerSharpParameters["Preview"] = value;
				}
			}

			// Token: 0x170064E3 RID: 25827
			// (set) Token: 0x06009075 RID: 36981 RVA: 0x000D33C1 File Offset: 0x000D15C1
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170064E4 RID: 25828
			// (set) Token: 0x06009076 RID: 36982 RVA: 0x000D33D4 File Offset: 0x000D15D4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170064E5 RID: 25829
			// (set) Token: 0x06009077 RID: 36983 RVA: 0x000D33F2 File Offset: 0x000D15F2
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170064E6 RID: 25830
			// (set) Token: 0x06009078 RID: 36984 RVA: 0x000D3405 File Offset: 0x000D1605
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170064E7 RID: 25831
			// (set) Token: 0x06009079 RID: 36985 RVA: 0x000D3418 File Offset: 0x000D1618
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170064E8 RID: 25832
			// (set) Token: 0x0600907A RID: 36986 RVA: 0x000D3436 File Offset: 0x000D1636
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170064E9 RID: 25833
			// (set) Token: 0x0600907B RID: 36987 RVA: 0x000D344E File Offset: 0x000D164E
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170064EA RID: 25834
			// (set) Token: 0x0600907C RID: 36988 RVA: 0x000D3461 File Offset: 0x000D1661
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170064EB RID: 25835
			// (set) Token: 0x0600907D RID: 36989 RVA: 0x000D3479 File Offset: 0x000D1679
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170064EC RID: 25836
			// (set) Token: 0x0600907E RID: 36990 RVA: 0x000D3491 File Offset: 0x000D1691
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064ED RID: 25837
			// (set) Token: 0x0600907F RID: 36991 RVA: 0x000D34A4 File Offset: 0x000D16A4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064EE RID: 25838
			// (set) Token: 0x06009080 RID: 36992 RVA: 0x000D34BC File Offset: 0x000D16BC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064EF RID: 25839
			// (set) Token: 0x06009081 RID: 36993 RVA: 0x000D34D4 File Offset: 0x000D16D4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064F0 RID: 25840
			// (set) Token: 0x06009082 RID: 36994 RVA: 0x000D34EC File Offset: 0x000D16EC
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
