using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000237 RID: 567
	internal class PartnerApplicationRunspaceConfiguration : WebServiceRunspaceConfiguration
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00048E94 File Offset: 0x00047094
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x00048E9C File Offset: 0x0004709C
		public ADUser LinkedAccountUser { get; private set; }

		// Token: 0x06001420 RID: 5152 RVA: 0x00048EA8 File Offset: 0x000470A8
		public static PartnerApplicationRunspaceConfiguration Create(PartnerApplication partnerApplication)
		{
			if (partnerApplication == null)
			{
				throw new ArgumentNullException("partnerApplication");
			}
			if (partnerApplication.LinkedAccount == null || partnerApplication.LinkedAccount.IsDeleted)
			{
				throw new CmdletAccessDeniedException(Strings.ErrorPartnerApplicationWithoutLinkedAccount(partnerApplication.Id.ToString()));
			}
			ADUser aduser = LinkedAccountCache.Instance.Get(partnerApplication.LinkedAccount);
			if (aduser == null)
			{
				throw new CmdletAccessDeniedException(Strings.ErrorManagementObjectNotFound(partnerApplication.LinkedAccount.ToString()));
			}
			return new PartnerApplicationRunspaceConfiguration(PartnerApplicationRunspaceConfiguration.LinkedAccountIdentity.Create(aduser));
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00048F23 File Offset: 0x00047123
		private PartnerApplicationRunspaceConfiguration(PartnerApplicationRunspaceConfiguration.LinkedAccountIdentity identity) : base(identity)
		{
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00048F2C File Offset: 0x0004712C
		public override bool IsTargetObjectInRoleScope(RoleType roleType, ADRecipient targetRecipient)
		{
			if (targetRecipient == null)
			{
				throw new ArgumentNullException("targetRecipient");
			}
			if (!base.HasRoleOfType(roleType))
			{
				ExTraceGlobals.AccessDeniedTracer.TraceWarning<string, RoleType>((long)this.GetHashCode(), "IsTargetObjectInRoleScope() returns false because identity {0} doesn't have role {1}", this.identityName, roleType);
				return false;
			}
			return (this.LinkedAccountUser.OrganizationId == OrganizationId.ForestWideOrgId && targetRecipient.OrganizationId != OrganizationId.ForestWideOrgId) || base.IsTargetObjectInRoleScope(roleType, targetRecipient);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00048FA2 File Offset: 0x000471A2
		public static bool IsWebMethodInOfficeExtensionRole(string webMethodName)
		{
			return PartnerApplicationRunspaceConfiguration.IsWebMethodInDefaultRoleType(webMethodName, RoleType.OfficeExtensionApplication, ref PartnerApplicationRunspaceConfiguration.officeExtensionEntries);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00048FBC File Offset: 0x000471BC
		private static bool IsWebMethodInDefaultRoleType(string webMethodName, RoleType roleType, ref List<string> entriesCache)
		{
			if (string.IsNullOrEmpty(webMethodName))
			{
				throw new ArgumentNullException("webMethodName");
			}
			if (entriesCache == null)
			{
				lock (PartnerApplicationRunspaceConfiguration.staticLock)
				{
					if (entriesCache == null)
					{
						ExchangeRole rootRoleForRoleType = PartnerApplicationRunspaceConfiguration.GetRootRoleForRoleType(roleType);
						if (rootRoleForRoleType != null)
						{
							entriesCache = (from x in rootRoleForRoleType.RoleEntries
							select x.Name).ToList<string>();
							entriesCache.Sort();
						}
						else
						{
							entriesCache = new List<string>();
						}
					}
				}
			}
			return entriesCache.BinarySearch(webMethodName, StringComparer.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00049074 File Offset: 0x00047274
		private static ExchangeRole GetRootRoleForRoleType(RoleType roleType)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 185, "GetRootRoleForRoleType", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\PartnerApplicationRunspaceConfiguration.cs");
			ExchangeRole[] array = tenantOrTopologyConfigurationSession.Find<ExchangeRole>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleSchema.RoleType, roleType), null, ADGenericPagedReader<ExchangeRole>.DefaultPageSize);
			ExchangeRole[] array2 = Array.FindAll<ExchangeRole>(array, (ExchangeRole r) => r.IsRootRole);
			if (array2.Length == 0)
			{
				return null;
			}
			if (array2.Length == 1)
			{
				return array2[0];
			}
			throw new DataSourceOperationException(Strings.ErrorFoundMultipleRootRole(roleType.ToString()));
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0004910C File Offset: 0x0004730C
		protected override ADRawEntry LoadExecutingUser(IIdentity identity, IList<PropertyDefinition> properties)
		{
			this.LinkedAccountUser = ((PartnerApplicationRunspaceConfiguration.LinkedAccountIdentity)identity).LinkedAccountUser;
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(this.LinkedAccountUser.Id), 224, "LoadExecutingUser", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\PartnerApplicationRunspaceConfiguration.cs");
			return this.LinkedAccountUser;
		}

		// Token: 0x040005AC RID: 1452
		private static object staticLock = new object();

		// Token: 0x040005AD RID: 1453
		private static List<string> officeExtensionEntries;

		// Token: 0x02000238 RID: 568
		private class LinkedAccountIdentity : GenericSidIdentity
		{
			// Token: 0x170003DD RID: 989
			// (get) Token: 0x0600142A RID: 5162 RVA: 0x0004916C File Offset: 0x0004736C
			// (set) Token: 0x0600142B RID: 5163 RVA: 0x00049174 File Offset: 0x00047374
			public ADUser LinkedAccountUser { get; private set; }

			// Token: 0x0600142C RID: 5164 RVA: 0x00049180 File Offset: 0x00047380
			public static PartnerApplicationRunspaceConfiguration.LinkedAccountIdentity Create(ADUser linkedAccountUser)
			{
				if (linkedAccountUser == null)
				{
					throw new ArgumentNullException("linkedAccountUser");
				}
				string partitionId = null;
				if (!linkedAccountUser.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					partitionId = linkedAccountUser.OrganizationId.PartitionId.ToString();
				}
				return new PartnerApplicationRunspaceConfiguration.LinkedAccountIdentity(linkedAccountUser.Name, "PartnerApplicationLinkedAccount", linkedAccountUser.Sid, partitionId)
				{
					LinkedAccountUser = linkedAccountUser
				};
			}

			// Token: 0x0600142D RID: 5165 RVA: 0x000491E0 File Offset: 0x000473E0
			private LinkedAccountIdentity(string name, string type, SecurityIdentifier sid, string partitionId) : base(name, type, sid, partitionId)
			{
			}

			// Token: 0x040005B1 RID: 1457
			private const string AccountType = "PartnerApplicationLinkedAccount";
		}
	}
}
