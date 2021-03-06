using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001A4 RID: 420
	internal static class TemplateTenantConfiguration
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x000567C0 File Offset: 0x000549C0
		internal static string CreateSharedConfigurationName(string programId, string offerId)
		{
			SharedConfigurationInfo sharedConfigurationInfo = SharedConfigurationInfo.FromInstalledVersion(programId, offerId);
			string text = string.Format("{0}{1}{2}{3}{4}", new object[]
			{
				SharedConfiguration.SCTNamePrefix,
				TemplateTenantConfiguration.Separator,
				sharedConfigurationInfo.ToString().ToLower().Replace("_", "-"),
				TemplateTenantConfiguration.Separator,
				Guid.NewGuid().ToString().Substring(0, 5)
			});
			if (text.Length > TemplateTenantConfiguration.maxStubLength)
			{
				text = text.Substring(0, TemplateTenantConfiguration.maxStubLength);
			}
			return text + TemplateTenantConfiguration.TopLevelDomain;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0005685F File Offset: 0x00054A5F
		internal static SmtpDomain CreateSharedConfigurationDomainName(string name)
		{
			return new SmtpDomain(name);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00056867 File Offset: 0x00054A67
		internal static ITenantRecipientSession GetTempateTenantRecipientSession()
		{
			if (TemplateTenantConfiguration.cachedTemplateUser == null)
			{
				TemplateTenantConfiguration.RetrieveLocalTempateUserContext();
			}
			return TemplateTenantConfiguration.cachedRecipientSession;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0005687A File Offset: 0x00054A7A
		internal static ADUser GetLocalTempateUser()
		{
			if (TemplateTenantConfiguration.cachedTemplateUser == null)
			{
				TemplateTenantConfiguration.RetrieveLocalTempateUserContext();
			}
			return TemplateTenantConfiguration.cachedTemplateUser;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0005688D File Offset: 0x00054A8D
		internal static SecurityDescriptor GetTemplateUserSecurityDescriptorBlob()
		{
			if (TemplateTenantConfiguration.cachedTemplateUser == null)
			{
				TemplateTenantConfiguration.RetrieveLocalTempateUserContext();
			}
			return TemplateTenantConfiguration.cachedTemplateUserSd;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000568A0 File Offset: 0x00054AA0
		private static void RetrieveLocalTempateUserContext()
		{
			if (TemplateTenantConfiguration.cachedTemplateUser == null)
			{
				ExchangeConfigurationUnit localTemplateTenant = TemplateTenantConfiguration.GetLocalTemplateTenant();
				if (localTemplateTenant == null)
				{
					throw new ADTransientException(DirectoryStrings.CannotFindTemplateTenant);
				}
				ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), localTemplateTenant.OrganizationId, null, false);
				adsessionSettings.ForceADInTemplateScope = true;
				ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(false, ConsistencyMode.IgnoreInvalid, adsessionSettings, 121, "RetrieveLocalTempateUserContext", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\TemplateTenantConfiguration.cs");
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADUser.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MailboxPlan)
				});
				ADUser[] array = tenantRecipientSession.FindADUser(null, QueryScope.OneLevel, filter, null, 1);
				if (array == null || array.Length == 0)
				{
					new ADTransientException(DirectoryStrings.CannotFindTemplateUser(localTemplateTenant.OrganizationalUnitRoot.DistinguishedName));
				}
				array[0].RecipientTypeDetails = RecipientTypeDetails.UserMailbox;
				array[0].RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.MailboxUser);
				array[0].MasterAccountSid = null;
				array[0].AcceptMessagesOnlyFrom = null;
				array[0].UseDatabaseQuotaDefaults = new bool?(false);
				array[0].ExchangeUserAccountControl = UserAccountControlFlags.None;
				TemplateTenantConfiguration.cachedTemplateUser = array[0];
				TemplateTenantConfiguration.cachedTemplateUserSd = tenantRecipientSession.ReadSecurityDescriptorBlob(array[0].Id);
				TemplateTenantConfiguration.cachedRecipientSession = tenantRecipientSession;
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000569D4 File Offset: 0x00054BD4
		private static ExchangeConfigurationUnit RetrieveLocalTempateTenant()
		{
			ADPagedReader<ExchangeConfigurationUnit> adpagedReader = TemplateTenantConfiguration.FindAllTempateTenants(TemplateTenantConfiguration.ProgramId, TemplateTenantConfiguration.OfferId, PartitionId.LocalForest);
			ExchangeConfigurationUnit[] array = adpagedReader.ReadAllPages();
			switch (array.Length)
			{
			case 0:
				return null;
			case 1:
				return array[0];
			default:
			{
				Array.Sort<ExchangeConfigurationUnit>(array, new Comparison<ExchangeConfigurationUnit>(SharedConfiguration.CompareBySharedConfigurationInfo));
				ExchangeConfigurationUnit result = array[0];
				foreach (ExchangeConfigurationUnit exchangeConfigurationUnit in array)
				{
					if (!(exchangeConfigurationUnit.SharedConfigurationInfo != null) || ((IComparable)ServerVersion.InstalledVersion).CompareTo(exchangeConfigurationUnit.SharedConfigurationInfo.CurrentVersion) < 0)
					{
						break;
					}
					result = exchangeConfigurationUnit;
				}
				return result;
			}
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00056A6E File Offset: 0x00054C6E
		internal static ExchangeConfigurationUnit GetLocalTemplateTenant()
		{
			if (TemplateTenantConfiguration.cachedTemplateTenant == null)
			{
				TemplateTenantConfiguration.cachedTemplateTenant = TemplateTenantConfiguration.RetrieveLocalTempateTenant();
			}
			return TemplateTenantConfiguration.cachedTemplateTenant;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00056A88 File Offset: 0x00054C88
		internal static ADPagedReader<ExchangeConfigurationUnit> FindAllTempateTenants(string programId, string offerId, PartitionId partitionId)
		{
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 236, "FindAllTempateTenants", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\TemplateTenantConfiguration.cs");
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(OrganizationSchema.SharedConfigurationInfo),
				new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.ResellerId, programId + "." + offerId),
				new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.Active),
				new ComparisonFilter(ComparisonOperator.Equal, OrganizationSchema.EnableAsSharedConfiguration, true)
			});
			return tenantConfigurationSession.FindPaged<ExchangeConfigurationUnit>(null, QueryScope.SubTree, filter, null, 0);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00056B20 File Offset: 0x00054D20
		internal static bool IsTemplateTenant(OrganizationId orgId)
		{
			if (orgId == null)
			{
				throw new ArgumentNullException("orgId");
			}
			ADObjectId organizationalUnit = orgId.OrganizationalUnit;
			return organizationalUnit != null && organizationalUnit.DistinguishedName != null && TemplateTenantConfiguration.IsTemplateTenantName(organizationalUnit.Name);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00056B60 File Offset: 0x00054D60
		internal static bool IsTemplateTenantName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return TemplateTenantConfiguration.templateTenantRegex.Match(name).Success;
		}

		// Token: 0x04000A3B RID: 2619
		private const int maxCNLength = 64;

		// Token: 0x04000A3C RID: 2620
		internal static readonly string DefaultDomain = "outlook.com";

		// Token: 0x04000A3D RID: 2621
		internal static readonly string TopLevelDomain = ".templateTenant";

		// Token: 0x04000A3E RID: 2622
		private static readonly Regex templateTenantRegex = new Regex("\\.templateTenant$", RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));

		// Token: 0x04000A3F RID: 2623
		private static readonly int maxStubLength = 64 - TemplateTenantConfiguration.TopLevelDomain.Length;

		// Token: 0x04000A40 RID: 2624
		public static readonly string Separator = "-";

		// Token: 0x04000A41 RID: 2625
		public static readonly string TemplateTenantExternalDirectoryOrganizationId = "84df9e7f-e9f6-40af-b435-aaaaaaaaaaaa";

		// Token: 0x04000A42 RID: 2626
		public static readonly Guid TemplateTenantExternalDirectoryOrganizationIdGuid = new Guid(TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationId);

		// Token: 0x04000A43 RID: 2627
		public static readonly string ProgramId = "MSOnline";

		// Token: 0x04000A44 RID: 2628
		public static readonly string OfferId = "Outlook";

		// Token: 0x04000A45 RID: 2629
		public static readonly string TestProgramId = "ExchangeTest";

		// Token: 0x04000A46 RID: 2630
		public static readonly string TestOfferId = "29";

		// Token: 0x04000A47 RID: 2631
		public static readonly ServerVersion RequiredTemplateTenantVersion = new ServerVersion(15, 0, 1037, 0);

		// Token: 0x04000A48 RID: 2632
		private static ADUser cachedTemplateUser;

		// Token: 0x04000A49 RID: 2633
		private static SecurityDescriptor cachedTemplateUserSd;

		// Token: 0x04000A4A RID: 2634
		private static ExchangeConfigurationUnit cachedTemplateTenant;

		// Token: 0x04000A4B RID: 2635
		private static ITenantRecipientSession cachedRecipientSession;
	}
}
