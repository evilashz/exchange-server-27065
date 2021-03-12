using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200052C RID: 1324
	public class SetClientAccessRuleCommand : SyntheticCommandWithPipelineInputNoOutput<ADClientAccessRule>
	{
		// Token: 0x06004706 RID: 18182 RVA: 0x00073A46 File Offset: 0x00071C46
		private SetClientAccessRuleCommand() : base("Set-ClientAccessRule")
		{
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x00073A53 File Offset: 0x00071C53
		public SetClientAccessRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00073A62 File Offset: 0x00071C62
		public virtual SetClientAccessRuleCommand SetParameters(SetClientAccessRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x00073A6C File Offset: 0x00071C6C
		public virtual SetClientAccessRuleCommand SetParameters(SetClientAccessRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200052D RID: 1325
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002867 RID: 10343
			// (set) Token: 0x0600470A RID: 18186 RVA: 0x00073A76 File Offset: 0x00071C76
			public virtual SwitchParameter DatacenterAdminsOnly
			{
				set
				{
					base.PowerSharpParameters["DatacenterAdminsOnly"] = value;
				}
			}

			// Token: 0x17002868 RID: 10344
			// (set) Token: 0x0600470B RID: 18187 RVA: 0x00073A8E File Offset: 0x00071C8E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002869 RID: 10345
			// (set) Token: 0x0600470C RID: 18188 RVA: 0x00073AA1 File Offset: 0x00071CA1
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700286A RID: 10346
			// (set) Token: 0x0600470D RID: 18189 RVA: 0x00073AB9 File Offset: 0x00071CB9
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700286B RID: 10347
			// (set) Token: 0x0600470E RID: 18190 RVA: 0x00073AD1 File Offset: 0x00071CD1
			public virtual ClientAccessRulesAction Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x1700286C RID: 10348
			// (set) Token: 0x0600470F RID: 18191 RVA: 0x00073AE9 File Offset: 0x00071CE9
			public virtual MultiValuedProperty<IPRange> AnyOfClientIPAddressesOrRanges
			{
				set
				{
					base.PowerSharpParameters["AnyOfClientIPAddressesOrRanges"] = value;
				}
			}

			// Token: 0x1700286D RID: 10349
			// (set) Token: 0x06004710 RID: 18192 RVA: 0x00073AFC File Offset: 0x00071CFC
			public virtual MultiValuedProperty<IPRange> ExceptAnyOfClientIPAddressesOrRanges
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfClientIPAddressesOrRanges"] = value;
				}
			}

			// Token: 0x1700286E RID: 10350
			// (set) Token: 0x06004711 RID: 18193 RVA: 0x00073B0F File Offset: 0x00071D0F
			public virtual MultiValuedProperty<IntRange> AnyOfSourceTcpPortNumbers
			{
				set
				{
					base.PowerSharpParameters["AnyOfSourceTcpPortNumbers"] = value;
				}
			}

			// Token: 0x1700286F RID: 10351
			// (set) Token: 0x06004712 RID: 18194 RVA: 0x00073B22 File Offset: 0x00071D22
			public virtual MultiValuedProperty<IntRange> ExceptAnyOfSourceTcpPortNumbers
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfSourceTcpPortNumbers"] = value;
				}
			}

			// Token: 0x17002870 RID: 10352
			// (set) Token: 0x06004713 RID: 18195 RVA: 0x00073B35 File Offset: 0x00071D35
			public virtual MultiValuedProperty<string> UsernameMatchesAnyOfPatterns
			{
				set
				{
					base.PowerSharpParameters["UsernameMatchesAnyOfPatterns"] = value;
				}
			}

			// Token: 0x17002871 RID: 10353
			// (set) Token: 0x06004714 RID: 18196 RVA: 0x00073B48 File Offset: 0x00071D48
			public virtual MultiValuedProperty<string> ExceptUsernameMatchesAnyOfPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptUsernameMatchesAnyOfPatterns"] = value;
				}
			}

			// Token: 0x17002872 RID: 10354
			// (set) Token: 0x06004715 RID: 18197 RVA: 0x00073B5B File Offset: 0x00071D5B
			public virtual MultiValuedProperty<string> UserIsMemberOf
			{
				set
				{
					base.PowerSharpParameters["UserIsMemberOf"] = value;
				}
			}

			// Token: 0x17002873 RID: 10355
			// (set) Token: 0x06004716 RID: 18198 RVA: 0x00073B6E File Offset: 0x00071D6E
			public virtual MultiValuedProperty<string> ExceptUserIsMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptUserIsMemberOf"] = value;
				}
			}

			// Token: 0x17002874 RID: 10356
			// (set) Token: 0x06004717 RID: 18199 RVA: 0x00073B81 File Offset: 0x00071D81
			public virtual MultiValuedProperty<ClientAccessAuthenticationMethod> AnyOfAuthenticationTypes
			{
				set
				{
					base.PowerSharpParameters["AnyOfAuthenticationTypes"] = value;
				}
			}

			// Token: 0x17002875 RID: 10357
			// (set) Token: 0x06004718 RID: 18200 RVA: 0x00073B94 File Offset: 0x00071D94
			public virtual MultiValuedProperty<ClientAccessAuthenticationMethod> ExceptAnyOfAuthenticationTypes
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfAuthenticationTypes"] = value;
				}
			}

			// Token: 0x17002876 RID: 10358
			// (set) Token: 0x06004719 RID: 18201 RVA: 0x00073BA7 File Offset: 0x00071DA7
			public virtual MultiValuedProperty<ClientAccessProtocol> AnyOfProtocols
			{
				set
				{
					base.PowerSharpParameters["AnyOfProtocols"] = value;
				}
			}

			// Token: 0x17002877 RID: 10359
			// (set) Token: 0x0600471A RID: 18202 RVA: 0x00073BBA File Offset: 0x00071DBA
			public virtual MultiValuedProperty<ClientAccessProtocol> ExceptAnyOfProtocols
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfProtocols"] = value;
				}
			}

			// Token: 0x17002878 RID: 10360
			// (set) Token: 0x0600471B RID: 18203 RVA: 0x00073BCD File Offset: 0x00071DCD
			public virtual string UserRecipientFilter
			{
				set
				{
					base.PowerSharpParameters["UserRecipientFilter"] = value;
				}
			}

			// Token: 0x17002879 RID: 10361
			// (set) Token: 0x0600471C RID: 18204 RVA: 0x00073BE0 File Offset: 0x00071DE0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700287A RID: 10362
			// (set) Token: 0x0600471D RID: 18205 RVA: 0x00073BF3 File Offset: 0x00071DF3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700287B RID: 10363
			// (set) Token: 0x0600471E RID: 18206 RVA: 0x00073C0B File Offset: 0x00071E0B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700287C RID: 10364
			// (set) Token: 0x0600471F RID: 18207 RVA: 0x00073C23 File Offset: 0x00071E23
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700287D RID: 10365
			// (set) Token: 0x06004720 RID: 18208 RVA: 0x00073C3B File Offset: 0x00071E3B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700287E RID: 10366
			// (set) Token: 0x06004721 RID: 18209 RVA: 0x00073C53 File Offset: 0x00071E53
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200052E RID: 1326
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700287F RID: 10367
			// (set) Token: 0x06004723 RID: 18211 RVA: 0x00073C73 File Offset: 0x00071E73
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ClientAccessRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17002880 RID: 10368
			// (set) Token: 0x06004724 RID: 18212 RVA: 0x00073C91 File Offset: 0x00071E91
			public virtual SwitchParameter DatacenterAdminsOnly
			{
				set
				{
					base.PowerSharpParameters["DatacenterAdminsOnly"] = value;
				}
			}

			// Token: 0x17002881 RID: 10369
			// (set) Token: 0x06004725 RID: 18213 RVA: 0x00073CA9 File Offset: 0x00071EA9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002882 RID: 10370
			// (set) Token: 0x06004726 RID: 18214 RVA: 0x00073CBC File Offset: 0x00071EBC
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17002883 RID: 10371
			// (set) Token: 0x06004727 RID: 18215 RVA: 0x00073CD4 File Offset: 0x00071ED4
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17002884 RID: 10372
			// (set) Token: 0x06004728 RID: 18216 RVA: 0x00073CEC File Offset: 0x00071EEC
			public virtual ClientAccessRulesAction Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17002885 RID: 10373
			// (set) Token: 0x06004729 RID: 18217 RVA: 0x00073D04 File Offset: 0x00071F04
			public virtual MultiValuedProperty<IPRange> AnyOfClientIPAddressesOrRanges
			{
				set
				{
					base.PowerSharpParameters["AnyOfClientIPAddressesOrRanges"] = value;
				}
			}

			// Token: 0x17002886 RID: 10374
			// (set) Token: 0x0600472A RID: 18218 RVA: 0x00073D17 File Offset: 0x00071F17
			public virtual MultiValuedProperty<IPRange> ExceptAnyOfClientIPAddressesOrRanges
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfClientIPAddressesOrRanges"] = value;
				}
			}

			// Token: 0x17002887 RID: 10375
			// (set) Token: 0x0600472B RID: 18219 RVA: 0x00073D2A File Offset: 0x00071F2A
			public virtual MultiValuedProperty<IntRange> AnyOfSourceTcpPortNumbers
			{
				set
				{
					base.PowerSharpParameters["AnyOfSourceTcpPortNumbers"] = value;
				}
			}

			// Token: 0x17002888 RID: 10376
			// (set) Token: 0x0600472C RID: 18220 RVA: 0x00073D3D File Offset: 0x00071F3D
			public virtual MultiValuedProperty<IntRange> ExceptAnyOfSourceTcpPortNumbers
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfSourceTcpPortNumbers"] = value;
				}
			}

			// Token: 0x17002889 RID: 10377
			// (set) Token: 0x0600472D RID: 18221 RVA: 0x00073D50 File Offset: 0x00071F50
			public virtual MultiValuedProperty<string> UsernameMatchesAnyOfPatterns
			{
				set
				{
					base.PowerSharpParameters["UsernameMatchesAnyOfPatterns"] = value;
				}
			}

			// Token: 0x1700288A RID: 10378
			// (set) Token: 0x0600472E RID: 18222 RVA: 0x00073D63 File Offset: 0x00071F63
			public virtual MultiValuedProperty<string> ExceptUsernameMatchesAnyOfPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptUsernameMatchesAnyOfPatterns"] = value;
				}
			}

			// Token: 0x1700288B RID: 10379
			// (set) Token: 0x0600472F RID: 18223 RVA: 0x00073D76 File Offset: 0x00071F76
			public virtual MultiValuedProperty<string> UserIsMemberOf
			{
				set
				{
					base.PowerSharpParameters["UserIsMemberOf"] = value;
				}
			}

			// Token: 0x1700288C RID: 10380
			// (set) Token: 0x06004730 RID: 18224 RVA: 0x00073D89 File Offset: 0x00071F89
			public virtual MultiValuedProperty<string> ExceptUserIsMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptUserIsMemberOf"] = value;
				}
			}

			// Token: 0x1700288D RID: 10381
			// (set) Token: 0x06004731 RID: 18225 RVA: 0x00073D9C File Offset: 0x00071F9C
			public virtual MultiValuedProperty<ClientAccessAuthenticationMethod> AnyOfAuthenticationTypes
			{
				set
				{
					base.PowerSharpParameters["AnyOfAuthenticationTypes"] = value;
				}
			}

			// Token: 0x1700288E RID: 10382
			// (set) Token: 0x06004732 RID: 18226 RVA: 0x00073DAF File Offset: 0x00071FAF
			public virtual MultiValuedProperty<ClientAccessAuthenticationMethod> ExceptAnyOfAuthenticationTypes
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfAuthenticationTypes"] = value;
				}
			}

			// Token: 0x1700288F RID: 10383
			// (set) Token: 0x06004733 RID: 18227 RVA: 0x00073DC2 File Offset: 0x00071FC2
			public virtual MultiValuedProperty<ClientAccessProtocol> AnyOfProtocols
			{
				set
				{
					base.PowerSharpParameters["AnyOfProtocols"] = value;
				}
			}

			// Token: 0x17002890 RID: 10384
			// (set) Token: 0x06004734 RID: 18228 RVA: 0x00073DD5 File Offset: 0x00071FD5
			public virtual MultiValuedProperty<ClientAccessProtocol> ExceptAnyOfProtocols
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfProtocols"] = value;
				}
			}

			// Token: 0x17002891 RID: 10385
			// (set) Token: 0x06004735 RID: 18229 RVA: 0x00073DE8 File Offset: 0x00071FE8
			public virtual string UserRecipientFilter
			{
				set
				{
					base.PowerSharpParameters["UserRecipientFilter"] = value;
				}
			}

			// Token: 0x17002892 RID: 10386
			// (set) Token: 0x06004736 RID: 18230 RVA: 0x00073DFB File Offset: 0x00071FFB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002893 RID: 10387
			// (set) Token: 0x06004737 RID: 18231 RVA: 0x00073E0E File Offset: 0x0007200E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002894 RID: 10388
			// (set) Token: 0x06004738 RID: 18232 RVA: 0x00073E26 File Offset: 0x00072026
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002895 RID: 10389
			// (set) Token: 0x06004739 RID: 18233 RVA: 0x00073E3E File Offset: 0x0007203E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002896 RID: 10390
			// (set) Token: 0x0600473A RID: 18234 RVA: 0x00073E56 File Offset: 0x00072056
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002897 RID: 10391
			// (set) Token: 0x0600473B RID: 18235 RVA: 0x00073E6E File Offset: 0x0007206E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
