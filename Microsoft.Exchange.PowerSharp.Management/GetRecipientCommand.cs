using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D39 RID: 3385
	public class GetRecipientCommand : SyntheticCommandWithPipelineInput<ReducedRecipient, ReducedRecipient>
	{
		// Token: 0x0600B32E RID: 45870 RVA: 0x001023B1 File Offset: 0x001005B1
		private GetRecipientCommand() : base("Get-Recipient")
		{
		}

		// Token: 0x0600B32F RID: 45871 RVA: 0x001023BE File Offset: 0x001005BE
		public GetRecipientCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B330 RID: 45872 RVA: 0x001023CD File Offset: 0x001005CD
		public virtual GetRecipientCommand SetParameters(GetRecipientCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B331 RID: 45873 RVA: 0x001023D7 File Offset: 0x001005D7
		public virtual GetRecipientCommand SetParameters(GetRecipientCommand.RecipientPreviewFilterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B332 RID: 45874 RVA: 0x001023E1 File Offset: 0x001005E1
		public virtual GetRecipientCommand SetParameters(GetRecipientCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B333 RID: 45875 RVA: 0x001023EB File Offset: 0x001005EB
		public virtual GetRecipientCommand SetParameters(GetRecipientCommand.DatabaseSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B334 RID: 45876 RVA: 0x001023F5 File Offset: 0x001005F5
		public virtual GetRecipientCommand SetParameters(GetRecipientCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D3A RID: 3386
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008475 RID: 33909
			// (set) Token: 0x0600B335 RID: 45877 RVA: 0x001023FF File Offset: 0x001005FF
			public virtual RecipientType RecipientType
			{
				set
				{
					base.PowerSharpParameters["RecipientType"] = value;
				}
			}

			// Token: 0x17008476 RID: 33910
			// (set) Token: 0x0600B336 RID: 45878 RVA: 0x00102417 File Offset: 0x00100617
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17008477 RID: 33911
			// (set) Token: 0x0600B337 RID: 45879 RVA: 0x0010242F File Offset: 0x0010062F
			public virtual PropertySet PropertySet
			{
				set
				{
					base.PowerSharpParameters["PropertySet"] = value;
				}
			}

			// Token: 0x17008478 RID: 33912
			// (set) Token: 0x0600B338 RID: 45880 RVA: 0x00102447 File Offset: 0x00100647
			public virtual string Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x17008479 RID: 33913
			// (set) Token: 0x0600B339 RID: 45881 RVA: 0x0010245A File Offset: 0x0010065A
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x1700847A RID: 33914
			// (set) Token: 0x0600B33A RID: 45882 RVA: 0x00102472 File Offset: 0x00100672
			public virtual MultiValuedProperty<Capability> Capabilities
			{
				set
				{
					base.PowerSharpParameters["Capabilities"] = value;
				}
			}

			// Token: 0x1700847B RID: 33915
			// (set) Token: 0x0600B33B RID: 45883 RVA: 0x00102485 File Offset: 0x00100685
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700847C RID: 33916
			// (set) Token: 0x0600B33C RID: 45884 RVA: 0x00102498 File Offset: 0x00100698
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700847D RID: 33917
			// (set) Token: 0x0600B33D RID: 45885 RVA: 0x001024B6 File Offset: 0x001006B6
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700847E RID: 33918
			// (set) Token: 0x0600B33E RID: 45886 RVA: 0x001024C9 File Offset: 0x001006C9
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700847F RID: 33919
			// (set) Token: 0x0600B33F RID: 45887 RVA: 0x001024DC File Offset: 0x001006DC
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008480 RID: 33920
			// (set) Token: 0x0600B340 RID: 45888 RVA: 0x001024FA File Offset: 0x001006FA
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008481 RID: 33921
			// (set) Token: 0x0600B341 RID: 45889 RVA: 0x00102512 File Offset: 0x00100712
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17008482 RID: 33922
			// (set) Token: 0x0600B342 RID: 45890 RVA: 0x00102525 File Offset: 0x00100725
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17008483 RID: 33923
			// (set) Token: 0x0600B343 RID: 45891 RVA: 0x0010253D File Offset: 0x0010073D
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008484 RID: 33924
			// (set) Token: 0x0600B344 RID: 45892 RVA: 0x00102555 File Offset: 0x00100755
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008485 RID: 33925
			// (set) Token: 0x0600B345 RID: 45893 RVA: 0x00102568 File Offset: 0x00100768
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008486 RID: 33926
			// (set) Token: 0x0600B346 RID: 45894 RVA: 0x00102580 File Offset: 0x00100780
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008487 RID: 33927
			// (set) Token: 0x0600B347 RID: 45895 RVA: 0x00102598 File Offset: 0x00100798
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008488 RID: 33928
			// (set) Token: 0x0600B348 RID: 45896 RVA: 0x001025B0 File Offset: 0x001007B0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D3B RID: 3387
		public class RecipientPreviewFilterSetParameters : ParametersBase
		{
			// Token: 0x17008489 RID: 33929
			// (set) Token: 0x0600B34A RID: 45898 RVA: 0x001025D0 File Offset: 0x001007D0
			public virtual string RecipientPreviewFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientPreviewFilter"] = value;
				}
			}

			// Token: 0x1700848A RID: 33930
			// (set) Token: 0x0600B34B RID: 45899 RVA: 0x001025E3 File Offset: 0x001007E3
			public virtual RecipientType RecipientType
			{
				set
				{
					base.PowerSharpParameters["RecipientType"] = value;
				}
			}

			// Token: 0x1700848B RID: 33931
			// (set) Token: 0x0600B34C RID: 45900 RVA: 0x001025FB File Offset: 0x001007FB
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700848C RID: 33932
			// (set) Token: 0x0600B34D RID: 45901 RVA: 0x00102613 File Offset: 0x00100813
			public virtual PropertySet PropertySet
			{
				set
				{
					base.PowerSharpParameters["PropertySet"] = value;
				}
			}

			// Token: 0x1700848D RID: 33933
			// (set) Token: 0x0600B34E RID: 45902 RVA: 0x0010262B File Offset: 0x0010082B
			public virtual string Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x1700848E RID: 33934
			// (set) Token: 0x0600B34F RID: 45903 RVA: 0x0010263E File Offset: 0x0010083E
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x1700848F RID: 33935
			// (set) Token: 0x0600B350 RID: 45904 RVA: 0x00102656 File Offset: 0x00100856
			public virtual MultiValuedProperty<Capability> Capabilities
			{
				set
				{
					base.PowerSharpParameters["Capabilities"] = value;
				}
			}

			// Token: 0x17008490 RID: 33936
			// (set) Token: 0x0600B351 RID: 45905 RVA: 0x00102669 File Offset: 0x00100869
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008491 RID: 33937
			// (set) Token: 0x0600B352 RID: 45906 RVA: 0x0010267C File Offset: 0x0010087C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008492 RID: 33938
			// (set) Token: 0x0600B353 RID: 45907 RVA: 0x0010269A File Offset: 0x0010089A
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17008493 RID: 33939
			// (set) Token: 0x0600B354 RID: 45908 RVA: 0x001026AD File Offset: 0x001008AD
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17008494 RID: 33940
			// (set) Token: 0x0600B355 RID: 45909 RVA: 0x001026C0 File Offset: 0x001008C0
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008495 RID: 33941
			// (set) Token: 0x0600B356 RID: 45910 RVA: 0x001026DE File Offset: 0x001008DE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008496 RID: 33942
			// (set) Token: 0x0600B357 RID: 45911 RVA: 0x001026F6 File Offset: 0x001008F6
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17008497 RID: 33943
			// (set) Token: 0x0600B358 RID: 45912 RVA: 0x00102709 File Offset: 0x00100909
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17008498 RID: 33944
			// (set) Token: 0x0600B359 RID: 45913 RVA: 0x00102721 File Offset: 0x00100921
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008499 RID: 33945
			// (set) Token: 0x0600B35A RID: 45914 RVA: 0x00102739 File Offset: 0x00100939
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700849A RID: 33946
			// (set) Token: 0x0600B35B RID: 45915 RVA: 0x0010274C File Offset: 0x0010094C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700849B RID: 33947
			// (set) Token: 0x0600B35C RID: 45916 RVA: 0x00102764 File Offset: 0x00100964
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700849C RID: 33948
			// (set) Token: 0x0600B35D RID: 45917 RVA: 0x0010277C File Offset: 0x0010097C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700849D RID: 33949
			// (set) Token: 0x0600B35E RID: 45918 RVA: 0x00102794 File Offset: 0x00100994
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D3C RID: 3388
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700849E RID: 33950
			// (set) Token: 0x0600B360 RID: 45920 RVA: 0x001027B4 File Offset: 0x001009B4
			public virtual string BookmarkDisplayName
			{
				set
				{
					base.PowerSharpParameters["BookmarkDisplayName"] = value;
				}
			}

			// Token: 0x1700849F RID: 33951
			// (set) Token: 0x0600B361 RID: 45921 RVA: 0x001027C7 File Offset: 0x001009C7
			public virtual bool IncludeBookmarkObject
			{
				set
				{
					base.PowerSharpParameters["IncludeBookmarkObject"] = value;
				}
			}

			// Token: 0x170084A0 RID: 33952
			// (set) Token: 0x0600B362 RID: 45922 RVA: 0x001027DF File Offset: 0x001009DF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170084A1 RID: 33953
			// (set) Token: 0x0600B363 RID: 45923 RVA: 0x001027FD File Offset: 0x001009FD
			public virtual RecipientType RecipientType
			{
				set
				{
					base.PowerSharpParameters["RecipientType"] = value;
				}
			}

			// Token: 0x170084A2 RID: 33954
			// (set) Token: 0x0600B364 RID: 45924 RVA: 0x00102815 File Offset: 0x00100A15
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170084A3 RID: 33955
			// (set) Token: 0x0600B365 RID: 45925 RVA: 0x0010282D File Offset: 0x00100A2D
			public virtual PropertySet PropertySet
			{
				set
				{
					base.PowerSharpParameters["PropertySet"] = value;
				}
			}

			// Token: 0x170084A4 RID: 33956
			// (set) Token: 0x0600B366 RID: 45926 RVA: 0x00102845 File Offset: 0x00100A45
			public virtual string Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x170084A5 RID: 33957
			// (set) Token: 0x0600B367 RID: 45927 RVA: 0x00102858 File Offset: 0x00100A58
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170084A6 RID: 33958
			// (set) Token: 0x0600B368 RID: 45928 RVA: 0x00102870 File Offset: 0x00100A70
			public virtual MultiValuedProperty<Capability> Capabilities
			{
				set
				{
					base.PowerSharpParameters["Capabilities"] = value;
				}
			}

			// Token: 0x170084A7 RID: 33959
			// (set) Token: 0x0600B369 RID: 45929 RVA: 0x00102883 File Offset: 0x00100A83
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170084A8 RID: 33960
			// (set) Token: 0x0600B36A RID: 45930 RVA: 0x00102896 File Offset: 0x00100A96
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170084A9 RID: 33961
			// (set) Token: 0x0600B36B RID: 45931 RVA: 0x001028B4 File Offset: 0x00100AB4
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170084AA RID: 33962
			// (set) Token: 0x0600B36C RID: 45932 RVA: 0x001028C7 File Offset: 0x00100AC7
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170084AB RID: 33963
			// (set) Token: 0x0600B36D RID: 45933 RVA: 0x001028DA File Offset: 0x00100ADA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170084AC RID: 33964
			// (set) Token: 0x0600B36E RID: 45934 RVA: 0x001028F8 File Offset: 0x00100AF8
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170084AD RID: 33965
			// (set) Token: 0x0600B36F RID: 45935 RVA: 0x00102910 File Offset: 0x00100B10
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170084AE RID: 33966
			// (set) Token: 0x0600B370 RID: 45936 RVA: 0x00102923 File Offset: 0x00100B23
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170084AF RID: 33967
			// (set) Token: 0x0600B371 RID: 45937 RVA: 0x0010293B File Offset: 0x00100B3B
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170084B0 RID: 33968
			// (set) Token: 0x0600B372 RID: 45938 RVA: 0x00102953 File Offset: 0x00100B53
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170084B1 RID: 33969
			// (set) Token: 0x0600B373 RID: 45939 RVA: 0x00102966 File Offset: 0x00100B66
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170084B2 RID: 33970
			// (set) Token: 0x0600B374 RID: 45940 RVA: 0x0010297E File Offset: 0x00100B7E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170084B3 RID: 33971
			// (set) Token: 0x0600B375 RID: 45941 RVA: 0x00102996 File Offset: 0x00100B96
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170084B4 RID: 33972
			// (set) Token: 0x0600B376 RID: 45942 RVA: 0x001029AE File Offset: 0x00100BAE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D3D RID: 3389
		public class DatabaseSetParameters : ParametersBase
		{
			// Token: 0x170084B5 RID: 33973
			// (set) Token: 0x0600B378 RID: 45944 RVA: 0x001029CE File Offset: 0x00100BCE
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170084B6 RID: 33974
			// (set) Token: 0x0600B379 RID: 45945 RVA: 0x001029E1 File Offset: 0x00100BE1
			public virtual RecipientType RecipientType
			{
				set
				{
					base.PowerSharpParameters["RecipientType"] = value;
				}
			}

			// Token: 0x170084B7 RID: 33975
			// (set) Token: 0x0600B37A RID: 45946 RVA: 0x001029F9 File Offset: 0x00100BF9
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170084B8 RID: 33976
			// (set) Token: 0x0600B37B RID: 45947 RVA: 0x00102A11 File Offset: 0x00100C11
			public virtual PropertySet PropertySet
			{
				set
				{
					base.PowerSharpParameters["PropertySet"] = value;
				}
			}

			// Token: 0x170084B9 RID: 33977
			// (set) Token: 0x0600B37C RID: 45948 RVA: 0x00102A29 File Offset: 0x00100C29
			public virtual string Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x170084BA RID: 33978
			// (set) Token: 0x0600B37D RID: 45949 RVA: 0x00102A3C File Offset: 0x00100C3C
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170084BB RID: 33979
			// (set) Token: 0x0600B37E RID: 45950 RVA: 0x00102A54 File Offset: 0x00100C54
			public virtual MultiValuedProperty<Capability> Capabilities
			{
				set
				{
					base.PowerSharpParameters["Capabilities"] = value;
				}
			}

			// Token: 0x170084BC RID: 33980
			// (set) Token: 0x0600B37F RID: 45951 RVA: 0x00102A67 File Offset: 0x00100C67
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170084BD RID: 33981
			// (set) Token: 0x0600B380 RID: 45952 RVA: 0x00102A7A File Offset: 0x00100C7A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170084BE RID: 33982
			// (set) Token: 0x0600B381 RID: 45953 RVA: 0x00102A98 File Offset: 0x00100C98
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170084BF RID: 33983
			// (set) Token: 0x0600B382 RID: 45954 RVA: 0x00102AAB File Offset: 0x00100CAB
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170084C0 RID: 33984
			// (set) Token: 0x0600B383 RID: 45955 RVA: 0x00102ABE File Offset: 0x00100CBE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170084C1 RID: 33985
			// (set) Token: 0x0600B384 RID: 45956 RVA: 0x00102ADC File Offset: 0x00100CDC
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170084C2 RID: 33986
			// (set) Token: 0x0600B385 RID: 45957 RVA: 0x00102AF4 File Offset: 0x00100CF4
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170084C3 RID: 33987
			// (set) Token: 0x0600B386 RID: 45958 RVA: 0x00102B07 File Offset: 0x00100D07
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170084C4 RID: 33988
			// (set) Token: 0x0600B387 RID: 45959 RVA: 0x00102B1F File Offset: 0x00100D1F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170084C5 RID: 33989
			// (set) Token: 0x0600B388 RID: 45960 RVA: 0x00102B37 File Offset: 0x00100D37
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170084C6 RID: 33990
			// (set) Token: 0x0600B389 RID: 45961 RVA: 0x00102B4A File Offset: 0x00100D4A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170084C7 RID: 33991
			// (set) Token: 0x0600B38A RID: 45962 RVA: 0x00102B62 File Offset: 0x00100D62
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170084C8 RID: 33992
			// (set) Token: 0x0600B38B RID: 45963 RVA: 0x00102B7A File Offset: 0x00100D7A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170084C9 RID: 33993
			// (set) Token: 0x0600B38C RID: 45964 RVA: 0x00102B92 File Offset: 0x00100D92
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D3E RID: 3390
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170084CA RID: 33994
			// (set) Token: 0x0600B38E RID: 45966 RVA: 0x00102BB2 File Offset: 0x00100DB2
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170084CB RID: 33995
			// (set) Token: 0x0600B38F RID: 45967 RVA: 0x00102BC5 File Offset: 0x00100DC5
			public virtual RecipientType RecipientType
			{
				set
				{
					base.PowerSharpParameters["RecipientType"] = value;
				}
			}

			// Token: 0x170084CC RID: 33996
			// (set) Token: 0x0600B390 RID: 45968 RVA: 0x00102BDD File Offset: 0x00100DDD
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170084CD RID: 33997
			// (set) Token: 0x0600B391 RID: 45969 RVA: 0x00102BF5 File Offset: 0x00100DF5
			public virtual PropertySet PropertySet
			{
				set
				{
					base.PowerSharpParameters["PropertySet"] = value;
				}
			}

			// Token: 0x170084CE RID: 33998
			// (set) Token: 0x0600B392 RID: 45970 RVA: 0x00102C0D File Offset: 0x00100E0D
			public virtual string Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x170084CF RID: 33999
			// (set) Token: 0x0600B393 RID: 45971 RVA: 0x00102C20 File Offset: 0x00100E20
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170084D0 RID: 34000
			// (set) Token: 0x0600B394 RID: 45972 RVA: 0x00102C38 File Offset: 0x00100E38
			public virtual MultiValuedProperty<Capability> Capabilities
			{
				set
				{
					base.PowerSharpParameters["Capabilities"] = value;
				}
			}

			// Token: 0x170084D1 RID: 34001
			// (set) Token: 0x0600B395 RID: 45973 RVA: 0x00102C4B File Offset: 0x00100E4B
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170084D2 RID: 34002
			// (set) Token: 0x0600B396 RID: 45974 RVA: 0x00102C5E File Offset: 0x00100E5E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170084D3 RID: 34003
			// (set) Token: 0x0600B397 RID: 45975 RVA: 0x00102C7C File Offset: 0x00100E7C
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170084D4 RID: 34004
			// (set) Token: 0x0600B398 RID: 45976 RVA: 0x00102C8F File Offset: 0x00100E8F
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170084D5 RID: 34005
			// (set) Token: 0x0600B399 RID: 45977 RVA: 0x00102CA2 File Offset: 0x00100EA2
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170084D6 RID: 34006
			// (set) Token: 0x0600B39A RID: 45978 RVA: 0x00102CC0 File Offset: 0x00100EC0
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170084D7 RID: 34007
			// (set) Token: 0x0600B39B RID: 45979 RVA: 0x00102CD8 File Offset: 0x00100ED8
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170084D8 RID: 34008
			// (set) Token: 0x0600B39C RID: 45980 RVA: 0x00102CEB File Offset: 0x00100EEB
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170084D9 RID: 34009
			// (set) Token: 0x0600B39D RID: 45981 RVA: 0x00102D03 File Offset: 0x00100F03
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170084DA RID: 34010
			// (set) Token: 0x0600B39E RID: 45982 RVA: 0x00102D1B File Offset: 0x00100F1B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170084DB RID: 34011
			// (set) Token: 0x0600B39F RID: 45983 RVA: 0x00102D2E File Offset: 0x00100F2E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170084DC RID: 34012
			// (set) Token: 0x0600B3A0 RID: 45984 RVA: 0x00102D46 File Offset: 0x00100F46
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170084DD RID: 34013
			// (set) Token: 0x0600B3A1 RID: 45985 RVA: 0x00102D5E File Offset: 0x00100F5E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170084DE RID: 34014
			// (set) Token: 0x0600B3A2 RID: 45986 RVA: 0x00102D76 File Offset: 0x00100F76
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
