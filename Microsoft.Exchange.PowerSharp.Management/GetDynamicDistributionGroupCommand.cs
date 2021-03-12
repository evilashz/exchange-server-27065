using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C00 RID: 3072
	public class GetDynamicDistributionGroupCommand : SyntheticCommandWithPipelineInput<DynamicDistributionGroup, DynamicDistributionGroup>
	{
		// Token: 0x0600950A RID: 38154 RVA: 0x000D92B2 File Offset: 0x000D74B2
		private GetDynamicDistributionGroupCommand() : base("Get-DynamicDistributionGroup")
		{
		}

		// Token: 0x0600950B RID: 38155 RVA: 0x000D92BF File Offset: 0x000D74BF
		public GetDynamicDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600950C RID: 38156 RVA: 0x000D92CE File Offset: 0x000D74CE
		public virtual GetDynamicDistributionGroupCommand SetParameters(GetDynamicDistributionGroupCommand.ManagedBySetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600950D RID: 38157 RVA: 0x000D92D8 File Offset: 0x000D74D8
		public virtual GetDynamicDistributionGroupCommand SetParameters(GetDynamicDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600950E RID: 38158 RVA: 0x000D92E2 File Offset: 0x000D74E2
		public virtual GetDynamicDistributionGroupCommand SetParameters(GetDynamicDistributionGroupCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600950F RID: 38159 RVA: 0x000D92EC File Offset: 0x000D74EC
		public virtual GetDynamicDistributionGroupCommand SetParameters(GetDynamicDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C01 RID: 3073
		public class ManagedBySetParameters : ParametersBase
		{
			// Token: 0x170068C3 RID: 26819
			// (set) Token: 0x06009510 RID: 38160 RVA: 0x000D92F6 File Offset: 0x000D74F6
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170068C4 RID: 26820
			// (set) Token: 0x06009511 RID: 38161 RVA: 0x000D9314 File Offset: 0x000D7514
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170068C5 RID: 26821
			// (set) Token: 0x06009512 RID: 38162 RVA: 0x000D9327 File Offset: 0x000D7527
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170068C6 RID: 26822
			// (set) Token: 0x06009513 RID: 38163 RVA: 0x000D9345 File Offset: 0x000D7545
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170068C7 RID: 26823
			// (set) Token: 0x06009514 RID: 38164 RVA: 0x000D9358 File Offset: 0x000D7558
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170068C8 RID: 26824
			// (set) Token: 0x06009515 RID: 38165 RVA: 0x000D936B File Offset: 0x000D756B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170068C9 RID: 26825
			// (set) Token: 0x06009516 RID: 38166 RVA: 0x000D9389 File Offset: 0x000D7589
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170068CA RID: 26826
			// (set) Token: 0x06009517 RID: 38167 RVA: 0x000D93A1 File Offset: 0x000D75A1
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170068CB RID: 26827
			// (set) Token: 0x06009518 RID: 38168 RVA: 0x000D93B4 File Offset: 0x000D75B4
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170068CC RID: 26828
			// (set) Token: 0x06009519 RID: 38169 RVA: 0x000D93CC File Offset: 0x000D75CC
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170068CD RID: 26829
			// (set) Token: 0x0600951A RID: 38170 RVA: 0x000D93E4 File Offset: 0x000D75E4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170068CE RID: 26830
			// (set) Token: 0x0600951B RID: 38171 RVA: 0x000D93F7 File Offset: 0x000D75F7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170068CF RID: 26831
			// (set) Token: 0x0600951C RID: 38172 RVA: 0x000D940F File Offset: 0x000D760F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170068D0 RID: 26832
			// (set) Token: 0x0600951D RID: 38173 RVA: 0x000D9427 File Offset: 0x000D7627
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170068D1 RID: 26833
			// (set) Token: 0x0600951E RID: 38174 RVA: 0x000D943F File Offset: 0x000D763F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C02 RID: 3074
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170068D2 RID: 26834
			// (set) Token: 0x06009520 RID: 38176 RVA: 0x000D945F File Offset: 0x000D765F
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170068D3 RID: 26835
			// (set) Token: 0x06009521 RID: 38177 RVA: 0x000D9472 File Offset: 0x000D7672
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170068D4 RID: 26836
			// (set) Token: 0x06009522 RID: 38178 RVA: 0x000D9490 File Offset: 0x000D7690
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170068D5 RID: 26837
			// (set) Token: 0x06009523 RID: 38179 RVA: 0x000D94A3 File Offset: 0x000D76A3
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170068D6 RID: 26838
			// (set) Token: 0x06009524 RID: 38180 RVA: 0x000D94B6 File Offset: 0x000D76B6
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170068D7 RID: 26839
			// (set) Token: 0x06009525 RID: 38181 RVA: 0x000D94D4 File Offset: 0x000D76D4
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170068D8 RID: 26840
			// (set) Token: 0x06009526 RID: 38182 RVA: 0x000D94EC File Offset: 0x000D76EC
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170068D9 RID: 26841
			// (set) Token: 0x06009527 RID: 38183 RVA: 0x000D94FF File Offset: 0x000D76FF
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170068DA RID: 26842
			// (set) Token: 0x06009528 RID: 38184 RVA: 0x000D9517 File Offset: 0x000D7717
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170068DB RID: 26843
			// (set) Token: 0x06009529 RID: 38185 RVA: 0x000D952F File Offset: 0x000D772F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170068DC RID: 26844
			// (set) Token: 0x0600952A RID: 38186 RVA: 0x000D9542 File Offset: 0x000D7742
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170068DD RID: 26845
			// (set) Token: 0x0600952B RID: 38187 RVA: 0x000D955A File Offset: 0x000D775A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170068DE RID: 26846
			// (set) Token: 0x0600952C RID: 38188 RVA: 0x000D9572 File Offset: 0x000D7772
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170068DF RID: 26847
			// (set) Token: 0x0600952D RID: 38189 RVA: 0x000D958A File Offset: 0x000D778A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C03 RID: 3075
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170068E0 RID: 26848
			// (set) Token: 0x0600952F RID: 38191 RVA: 0x000D95AA File Offset: 0x000D77AA
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170068E1 RID: 26849
			// (set) Token: 0x06009530 RID: 38192 RVA: 0x000D95BD File Offset: 0x000D77BD
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170068E2 RID: 26850
			// (set) Token: 0x06009531 RID: 38193 RVA: 0x000D95D0 File Offset: 0x000D77D0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170068E3 RID: 26851
			// (set) Token: 0x06009532 RID: 38194 RVA: 0x000D95EE File Offset: 0x000D77EE
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170068E4 RID: 26852
			// (set) Token: 0x06009533 RID: 38195 RVA: 0x000D9601 File Offset: 0x000D7801
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170068E5 RID: 26853
			// (set) Token: 0x06009534 RID: 38196 RVA: 0x000D9614 File Offset: 0x000D7814
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170068E6 RID: 26854
			// (set) Token: 0x06009535 RID: 38197 RVA: 0x000D9632 File Offset: 0x000D7832
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170068E7 RID: 26855
			// (set) Token: 0x06009536 RID: 38198 RVA: 0x000D964A File Offset: 0x000D784A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170068E8 RID: 26856
			// (set) Token: 0x06009537 RID: 38199 RVA: 0x000D965D File Offset: 0x000D785D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170068E9 RID: 26857
			// (set) Token: 0x06009538 RID: 38200 RVA: 0x000D9675 File Offset: 0x000D7875
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170068EA RID: 26858
			// (set) Token: 0x06009539 RID: 38201 RVA: 0x000D968D File Offset: 0x000D788D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170068EB RID: 26859
			// (set) Token: 0x0600953A RID: 38202 RVA: 0x000D96A0 File Offset: 0x000D78A0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170068EC RID: 26860
			// (set) Token: 0x0600953B RID: 38203 RVA: 0x000D96B8 File Offset: 0x000D78B8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170068ED RID: 26861
			// (set) Token: 0x0600953C RID: 38204 RVA: 0x000D96D0 File Offset: 0x000D78D0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170068EE RID: 26862
			// (set) Token: 0x0600953D RID: 38205 RVA: 0x000D96E8 File Offset: 0x000D78E8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C04 RID: 3076
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170068EF RID: 26863
			// (set) Token: 0x0600953F RID: 38207 RVA: 0x000D9708 File Offset: 0x000D7908
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DynamicGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170068F0 RID: 26864
			// (set) Token: 0x06009540 RID: 38208 RVA: 0x000D9726 File Offset: 0x000D7926
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170068F1 RID: 26865
			// (set) Token: 0x06009541 RID: 38209 RVA: 0x000D9739 File Offset: 0x000D7939
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170068F2 RID: 26866
			// (set) Token: 0x06009542 RID: 38210 RVA: 0x000D9757 File Offset: 0x000D7957
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170068F3 RID: 26867
			// (set) Token: 0x06009543 RID: 38211 RVA: 0x000D976A File Offset: 0x000D796A
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170068F4 RID: 26868
			// (set) Token: 0x06009544 RID: 38212 RVA: 0x000D977D File Offset: 0x000D797D
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170068F5 RID: 26869
			// (set) Token: 0x06009545 RID: 38213 RVA: 0x000D979B File Offset: 0x000D799B
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170068F6 RID: 26870
			// (set) Token: 0x06009546 RID: 38214 RVA: 0x000D97B3 File Offset: 0x000D79B3
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170068F7 RID: 26871
			// (set) Token: 0x06009547 RID: 38215 RVA: 0x000D97C6 File Offset: 0x000D79C6
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170068F8 RID: 26872
			// (set) Token: 0x06009548 RID: 38216 RVA: 0x000D97DE File Offset: 0x000D79DE
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170068F9 RID: 26873
			// (set) Token: 0x06009549 RID: 38217 RVA: 0x000D97F6 File Offset: 0x000D79F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170068FA RID: 26874
			// (set) Token: 0x0600954A RID: 38218 RVA: 0x000D9809 File Offset: 0x000D7A09
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170068FB RID: 26875
			// (set) Token: 0x0600954B RID: 38219 RVA: 0x000D9821 File Offset: 0x000D7A21
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170068FC RID: 26876
			// (set) Token: 0x0600954C RID: 38220 RVA: 0x000D9839 File Offset: 0x000D7A39
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170068FD RID: 26877
			// (set) Token: 0x0600954D RID: 38221 RVA: 0x000D9851 File Offset: 0x000D7A51
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
