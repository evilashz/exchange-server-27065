using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000E7 RID: 231
	public class GetMailboxComplianceConfigurationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06001D9B RID: 7579 RVA: 0x0003E20D File Offset: 0x0003C40D
		private GetMailboxComplianceConfigurationCommand() : base("Get-MailboxComplianceConfiguration")
		{
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0003E21A File Offset: 0x0003C41A
		public GetMailboxComplianceConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0003E229 File Offset: 0x0003C429
		public virtual GetMailboxComplianceConfigurationCommand SetParameters(GetMailboxComplianceConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0003E233 File Offset: 0x0003C433
		public virtual GetMailboxComplianceConfigurationCommand SetParameters(GetMailboxComplianceConfigurationCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0003E23D File Offset: 0x0003C43D
		public virtual GetMailboxComplianceConfigurationCommand SetParameters(GetMailboxComplianceConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000E8 RID: 232
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000786 RID: 1926
			// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x0003E247 File Offset: 0x0003C447
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17000787 RID: 1927
			// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x0003E25A File Offset: 0x0003C45A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000788 RID: 1928
			// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x0003E278 File Offset: 0x0003C478
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17000789 RID: 1929
			// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x0003E28B File Offset: 0x0003C48B
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700078A RID: 1930
			// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x0003E29E File Offset: 0x0003C49E
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700078B RID: 1931
			// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x0003E2BC File Offset: 0x0003C4BC
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700078C RID: 1932
			// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700078D RID: 1933
			// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x0003E2E7 File Offset: 0x0003C4E7
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700078E RID: 1934
			// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x0003E2FF File Offset: 0x0003C4FF
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700078F RID: 1935
			// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x0003E317 File Offset: 0x0003C517
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000790 RID: 1936
			// (set) Token: 0x06001DAA RID: 7594 RVA: 0x0003E32A File Offset: 0x0003C52A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000791 RID: 1937
			// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0003E342 File Offset: 0x0003C542
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000792 RID: 1938
			// (set) Token: 0x06001DAC RID: 7596 RVA: 0x0003E35A File Offset: 0x0003C55A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000793 RID: 1939
			// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0003E372 File Offset: 0x0003C572
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020000E9 RID: 233
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x17000794 RID: 1940
			// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0003E392 File Offset: 0x0003C592
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x17000795 RID: 1941
			// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x0003E3A5 File Offset: 0x0003C5A5
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17000796 RID: 1942
			// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x0003E3B8 File Offset: 0x0003C5B8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000797 RID: 1943
			// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x0003E3D6 File Offset: 0x0003C5D6
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17000798 RID: 1944
			// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x0003E3E9 File Offset: 0x0003C5E9
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17000799 RID: 1945
			// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0003E3FC File Offset: 0x0003C5FC
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700079A RID: 1946
			// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x0003E41A File Offset: 0x0003C61A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700079B RID: 1947
			// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0003E432 File Offset: 0x0003C632
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700079C RID: 1948
			// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x0003E445 File Offset: 0x0003C645
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700079D RID: 1949
			// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x0003E45D File Offset: 0x0003C65D
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700079E RID: 1950
			// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x0003E475 File Offset: 0x0003C675
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700079F RID: 1951
			// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0003E488 File Offset: 0x0003C688
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170007A0 RID: 1952
			// (set) Token: 0x06001DBB RID: 7611 RVA: 0x0003E4A0 File Offset: 0x0003C6A0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170007A1 RID: 1953
			// (set) Token: 0x06001DBC RID: 7612 RVA: 0x0003E4B8 File Offset: 0x0003C6B8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170007A2 RID: 1954
			// (set) Token: 0x06001DBD RID: 7613 RVA: 0x0003E4D0 File Offset: 0x0003C6D0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020000EA RID: 234
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170007A3 RID: 1955
			// (set) Token: 0x06001DBF RID: 7615 RVA: 0x0003E4F0 File Offset: 0x0003C6F0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170007A4 RID: 1956
			// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x0003E50E File Offset: 0x0003C70E
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170007A5 RID: 1957
			// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0003E521 File Offset: 0x0003C721
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170007A6 RID: 1958
			// (set) Token: 0x06001DC2 RID: 7618 RVA: 0x0003E53F File Offset: 0x0003C73F
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170007A7 RID: 1959
			// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x0003E552 File Offset: 0x0003C752
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170007A8 RID: 1960
			// (set) Token: 0x06001DC4 RID: 7620 RVA: 0x0003E565 File Offset: 0x0003C765
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170007A9 RID: 1961
			// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0003E583 File Offset: 0x0003C783
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170007AA RID: 1962
			// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x0003E59B File Offset: 0x0003C79B
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170007AB RID: 1963
			// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x0003E5AE File Offset: 0x0003C7AE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170007AC RID: 1964
			// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x0003E5C6 File Offset: 0x0003C7C6
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170007AD RID: 1965
			// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x0003E5DE File Offset: 0x0003C7DE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170007AE RID: 1966
			// (set) Token: 0x06001DCA RID: 7626 RVA: 0x0003E5F1 File Offset: 0x0003C7F1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170007AF RID: 1967
			// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0003E609 File Offset: 0x0003C809
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170007B0 RID: 1968
			// (set) Token: 0x06001DCC RID: 7628 RVA: 0x0003E621 File Offset: 0x0003C821
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170007B1 RID: 1969
			// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0003E639 File Offset: 0x0003C839
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
