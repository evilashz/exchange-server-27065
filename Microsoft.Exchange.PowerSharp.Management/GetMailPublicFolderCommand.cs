using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000233 RID: 563
	public class GetMailPublicFolderCommand : SyntheticCommandWithPipelineInput<ADPublicFolder, ADPublicFolder>
	{
		// Token: 0x06002AD5 RID: 10965 RVA: 0x0004F578 File Offset: 0x0004D778
		private GetMailPublicFolderCommand() : base("Get-MailPublicFolder")
		{
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0004F585 File Offset: 0x0004D785
		public GetMailPublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0004F594 File Offset: 0x0004D794
		public virtual GetMailPublicFolderCommand SetParameters(GetMailPublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0004F59E File Offset: 0x0004D79E
		public virtual GetMailPublicFolderCommand SetParameters(GetMailPublicFolderCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0004F5A8 File Offset: 0x0004D7A8
		public virtual GetMailPublicFolderCommand SetParameters(GetMailPublicFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000234 RID: 564
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001228 RID: 4648
			// (set) Token: 0x06002ADA RID: 10970 RVA: 0x0004F5B2 File Offset: 0x0004D7B2
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17001229 RID: 4649
			// (set) Token: 0x06002ADB RID: 10971 RVA: 0x0004F5C5 File Offset: 0x0004D7C5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700122A RID: 4650
			// (set) Token: 0x06002ADC RID: 10972 RVA: 0x0004F5E3 File Offset: 0x0004D7E3
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700122B RID: 4651
			// (set) Token: 0x06002ADD RID: 10973 RVA: 0x0004F5F6 File Offset: 0x0004D7F6
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700122C RID: 4652
			// (set) Token: 0x06002ADE RID: 10974 RVA: 0x0004F609 File Offset: 0x0004D809
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700122D RID: 4653
			// (set) Token: 0x06002ADF RID: 10975 RVA: 0x0004F621 File Offset: 0x0004D821
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700122E RID: 4654
			// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x0004F634 File Offset: 0x0004D834
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700122F RID: 4655
			// (set) Token: 0x06002AE1 RID: 10977 RVA: 0x0004F64C File Offset: 0x0004D84C
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17001230 RID: 4656
			// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x0004F664 File Offset: 0x0004D864
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001231 RID: 4657
			// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x0004F677 File Offset: 0x0004D877
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001232 RID: 4658
			// (set) Token: 0x06002AE4 RID: 10980 RVA: 0x0004F68F File Offset: 0x0004D88F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001233 RID: 4659
			// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x0004F6A7 File Offset: 0x0004D8A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001234 RID: 4660
			// (set) Token: 0x06002AE6 RID: 10982 RVA: 0x0004F6BF File Offset: 0x0004D8BF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000235 RID: 565
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x17001235 RID: 4661
			// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x0004F6DF File Offset: 0x0004D8DF
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x17001236 RID: 4662
			// (set) Token: 0x06002AE9 RID: 10985 RVA: 0x0004F6F2 File Offset: 0x0004D8F2
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17001237 RID: 4663
			// (set) Token: 0x06002AEA RID: 10986 RVA: 0x0004F705 File Offset: 0x0004D905
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001238 RID: 4664
			// (set) Token: 0x06002AEB RID: 10987 RVA: 0x0004F723 File Offset: 0x0004D923
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17001239 RID: 4665
			// (set) Token: 0x06002AEC RID: 10988 RVA: 0x0004F736 File Offset: 0x0004D936
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700123A RID: 4666
			// (set) Token: 0x06002AED RID: 10989 RVA: 0x0004F749 File Offset: 0x0004D949
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700123B RID: 4667
			// (set) Token: 0x06002AEE RID: 10990 RVA: 0x0004F761 File Offset: 0x0004D961
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700123C RID: 4668
			// (set) Token: 0x06002AEF RID: 10991 RVA: 0x0004F774 File Offset: 0x0004D974
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700123D RID: 4669
			// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x0004F78C File Offset: 0x0004D98C
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700123E RID: 4670
			// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x0004F7A4 File Offset: 0x0004D9A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700123F RID: 4671
			// (set) Token: 0x06002AF2 RID: 10994 RVA: 0x0004F7B7 File Offset: 0x0004D9B7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001240 RID: 4672
			// (set) Token: 0x06002AF3 RID: 10995 RVA: 0x0004F7CF File Offset: 0x0004D9CF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001241 RID: 4673
			// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x0004F7E7 File Offset: 0x0004D9E7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001242 RID: 4674
			// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x0004F7FF File Offset: 0x0004D9FF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000236 RID: 566
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001243 RID: 4675
			// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x0004F81F File Offset: 0x0004DA1F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailPublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001244 RID: 4676
			// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x0004F83D File Offset: 0x0004DA3D
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17001245 RID: 4677
			// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x0004F850 File Offset: 0x0004DA50
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001246 RID: 4678
			// (set) Token: 0x06002AFA RID: 11002 RVA: 0x0004F86E File Offset: 0x0004DA6E
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17001247 RID: 4679
			// (set) Token: 0x06002AFB RID: 11003 RVA: 0x0004F881 File Offset: 0x0004DA81
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17001248 RID: 4680
			// (set) Token: 0x06002AFC RID: 11004 RVA: 0x0004F894 File Offset: 0x0004DA94
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17001249 RID: 4681
			// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0004F8AC File Offset: 0x0004DAAC
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700124A RID: 4682
			// (set) Token: 0x06002AFE RID: 11006 RVA: 0x0004F8BF File Offset: 0x0004DABF
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700124B RID: 4683
			// (set) Token: 0x06002AFF RID: 11007 RVA: 0x0004F8D7 File Offset: 0x0004DAD7
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700124C RID: 4684
			// (set) Token: 0x06002B00 RID: 11008 RVA: 0x0004F8EF File Offset: 0x0004DAEF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700124D RID: 4685
			// (set) Token: 0x06002B01 RID: 11009 RVA: 0x0004F902 File Offset: 0x0004DB02
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700124E RID: 4686
			// (set) Token: 0x06002B02 RID: 11010 RVA: 0x0004F91A File Offset: 0x0004DB1A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700124F RID: 4687
			// (set) Token: 0x06002B03 RID: 11011 RVA: 0x0004F932 File Offset: 0x0004DB32
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001250 RID: 4688
			// (set) Token: 0x06002B04 RID: 11012 RVA: 0x0004F94A File Offset: 0x0004DB4A
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
