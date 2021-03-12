using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C5C RID: 3164
	public class GetLinkedUserCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06009B38 RID: 39736 RVA: 0x000E14D9 File Offset: 0x000DF6D9
		private GetLinkedUserCommand() : base("Get-LinkedUser")
		{
		}

		// Token: 0x06009B39 RID: 39737 RVA: 0x000E14E6 File Offset: 0x000DF6E6
		public GetLinkedUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009B3A RID: 39738 RVA: 0x000E14F5 File Offset: 0x000DF6F5
		public virtual GetLinkedUserCommand SetParameters(GetLinkedUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009B3B RID: 39739 RVA: 0x000E14FF File Offset: 0x000DF6FF
		public virtual GetLinkedUserCommand SetParameters(GetLinkedUserCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009B3C RID: 39740 RVA: 0x000E1509 File Offset: 0x000DF709
		public virtual GetLinkedUserCommand SetParameters(GetLinkedUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C5D RID: 3165
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006E39 RID: 28217
			// (set) Token: 0x06009B3D RID: 39741 RVA: 0x000E1513 File Offset: 0x000DF713
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006E3A RID: 28218
			// (set) Token: 0x06009B3E RID: 39742 RVA: 0x000E1526 File Offset: 0x000DF726
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006E3B RID: 28219
			// (set) Token: 0x06009B3F RID: 39743 RVA: 0x000E1544 File Offset: 0x000DF744
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006E3C RID: 28220
			// (set) Token: 0x06009B40 RID: 39744 RVA: 0x000E1557 File Offset: 0x000DF757
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006E3D RID: 28221
			// (set) Token: 0x06009B41 RID: 39745 RVA: 0x000E156A File Offset: 0x000DF76A
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006E3E RID: 28222
			// (set) Token: 0x06009B42 RID: 39746 RVA: 0x000E1588 File Offset: 0x000DF788
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E3F RID: 28223
			// (set) Token: 0x06009B43 RID: 39747 RVA: 0x000E15A0 File Offset: 0x000DF7A0
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006E40 RID: 28224
			// (set) Token: 0x06009B44 RID: 39748 RVA: 0x000E15B3 File Offset: 0x000DF7B3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006E41 RID: 28225
			// (set) Token: 0x06009B45 RID: 39749 RVA: 0x000E15CB File Offset: 0x000DF7CB
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006E42 RID: 28226
			// (set) Token: 0x06009B46 RID: 39750 RVA: 0x000E15E3 File Offset: 0x000DF7E3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E43 RID: 28227
			// (set) Token: 0x06009B47 RID: 39751 RVA: 0x000E15F6 File Offset: 0x000DF7F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E44 RID: 28228
			// (set) Token: 0x06009B48 RID: 39752 RVA: 0x000E160E File Offset: 0x000DF80E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E45 RID: 28229
			// (set) Token: 0x06009B49 RID: 39753 RVA: 0x000E1626 File Offset: 0x000DF826
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E46 RID: 28230
			// (set) Token: 0x06009B4A RID: 39754 RVA: 0x000E163E File Offset: 0x000DF83E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C5E RID: 3166
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x17006E47 RID: 28231
			// (set) Token: 0x06009B4C RID: 39756 RVA: 0x000E165E File Offset: 0x000DF85E
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x17006E48 RID: 28232
			// (set) Token: 0x06009B4D RID: 39757 RVA: 0x000E1671 File Offset: 0x000DF871
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006E49 RID: 28233
			// (set) Token: 0x06009B4E RID: 39758 RVA: 0x000E1684 File Offset: 0x000DF884
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006E4A RID: 28234
			// (set) Token: 0x06009B4F RID: 39759 RVA: 0x000E16A2 File Offset: 0x000DF8A2
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006E4B RID: 28235
			// (set) Token: 0x06009B50 RID: 39760 RVA: 0x000E16B5 File Offset: 0x000DF8B5
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006E4C RID: 28236
			// (set) Token: 0x06009B51 RID: 39761 RVA: 0x000E16C8 File Offset: 0x000DF8C8
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006E4D RID: 28237
			// (set) Token: 0x06009B52 RID: 39762 RVA: 0x000E16E6 File Offset: 0x000DF8E6
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E4E RID: 28238
			// (set) Token: 0x06009B53 RID: 39763 RVA: 0x000E16FE File Offset: 0x000DF8FE
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006E4F RID: 28239
			// (set) Token: 0x06009B54 RID: 39764 RVA: 0x000E1711 File Offset: 0x000DF911
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006E50 RID: 28240
			// (set) Token: 0x06009B55 RID: 39765 RVA: 0x000E1729 File Offset: 0x000DF929
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006E51 RID: 28241
			// (set) Token: 0x06009B56 RID: 39766 RVA: 0x000E1741 File Offset: 0x000DF941
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E52 RID: 28242
			// (set) Token: 0x06009B57 RID: 39767 RVA: 0x000E1754 File Offset: 0x000DF954
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E53 RID: 28243
			// (set) Token: 0x06009B58 RID: 39768 RVA: 0x000E176C File Offset: 0x000DF96C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E54 RID: 28244
			// (set) Token: 0x06009B59 RID: 39769 RVA: 0x000E1784 File Offset: 0x000DF984
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E55 RID: 28245
			// (set) Token: 0x06009B5A RID: 39770 RVA: 0x000E179C File Offset: 0x000DF99C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C5F RID: 3167
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006E56 RID: 28246
			// (set) Token: 0x06009B5C RID: 39772 RVA: 0x000E17BC File Offset: 0x000DF9BC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006E57 RID: 28247
			// (set) Token: 0x06009B5D RID: 39773 RVA: 0x000E17DA File Offset: 0x000DF9DA
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006E58 RID: 28248
			// (set) Token: 0x06009B5E RID: 39774 RVA: 0x000E17ED File Offset: 0x000DF9ED
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006E59 RID: 28249
			// (set) Token: 0x06009B5F RID: 39775 RVA: 0x000E180B File Offset: 0x000DFA0B
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006E5A RID: 28250
			// (set) Token: 0x06009B60 RID: 39776 RVA: 0x000E181E File Offset: 0x000DFA1E
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006E5B RID: 28251
			// (set) Token: 0x06009B61 RID: 39777 RVA: 0x000E1831 File Offset: 0x000DFA31
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006E5C RID: 28252
			// (set) Token: 0x06009B62 RID: 39778 RVA: 0x000E184F File Offset: 0x000DFA4F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E5D RID: 28253
			// (set) Token: 0x06009B63 RID: 39779 RVA: 0x000E1867 File Offset: 0x000DFA67
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006E5E RID: 28254
			// (set) Token: 0x06009B64 RID: 39780 RVA: 0x000E187A File Offset: 0x000DFA7A
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006E5F RID: 28255
			// (set) Token: 0x06009B65 RID: 39781 RVA: 0x000E1892 File Offset: 0x000DFA92
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006E60 RID: 28256
			// (set) Token: 0x06009B66 RID: 39782 RVA: 0x000E18AA File Offset: 0x000DFAAA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E61 RID: 28257
			// (set) Token: 0x06009B67 RID: 39783 RVA: 0x000E18BD File Offset: 0x000DFABD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E62 RID: 28258
			// (set) Token: 0x06009B68 RID: 39784 RVA: 0x000E18D5 File Offset: 0x000DFAD5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E63 RID: 28259
			// (set) Token: 0x06009B69 RID: 39785 RVA: 0x000E18ED File Offset: 0x000DFAED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E64 RID: 28260
			// (set) Token: 0x06009B6A RID: 39786 RVA: 0x000E1905 File Offset: 0x000DFB05
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
