using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SharingPolicy;

namespace Microsoft.Exchange.MailboxAssistants.SharingPolicyAssistant
{
	// Token: 0x02000161 RID: 353
	internal sealed class SharingPolicyCache
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0005663F File Offset: 0x0005483F
		public SharingPolicy Policy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00056647 File Offset: 0x00054847
		public byte[] Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0005664F File Offset: 0x0005484F
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x00056657 File Offset: 0x00054857
		public bool BelongsToDehydratedContainer { get; private set; }

		// Token: 0x06000E5B RID: 3675 RVA: 0x00056660 File Offset: 0x00054860
		private SharingPolicyCache(SharingPolicy policy, bool isDehydrated)
		{
			ArgumentValidator.ThrowIfNull("policy", policy);
			this.policy = policy;
			this.BelongsToDehydratedContainer = isDehydrated;
			this.PopulatePolicyHash();
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00056687 File Offset: 0x00054887
		internal static void PurgeCache()
		{
			SharingPolicyCache.hydratedDefaultCache = new Dictionary<OrganizationId, SharingPolicyCache>();
			SharingPolicyCache.hydratedCache = new Dictionary<Guid, SharingPolicyCache>();
			SharingPolicyCache.dehydratedDefaultCache = new Dictionary<Guid, SharingPolicyCache>();
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000566A7 File Offset: 0x000548A7
		internal static SharingPolicyCache Get(ADUser adUser)
		{
			ArgumentValidator.ThrowIfNull("adUser", adUser);
			return SharingPolicyCache.InternalGet(adUser.OrganizationId, adUser.SharingPolicy);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000566C5 File Offset: 0x000548C5
		internal static SharingPolicyCache Get(ADObjectId policyId, TenantPartitionHint tenantPartitionHint)
		{
			ArgumentValidator.ThrowIfNull("policyId", policyId);
			ArgumentValidator.ThrowIfNull("tenantPartitionHint", tenantPartitionHint);
			return SharingPolicyCache.InternalGet(ADSessionSettings.FromTenantPartitionHint(tenantPartitionHint).CurrentOrganizationId, policyId);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000566F0 File Offset: 0x000548F0
		private static SharingPolicyCache InternalGet(OrganizationId orgId, ADObjectId policyId)
		{
			orgId = (orgId ?? OrganizationId.ForestWideOrgId);
			policyId = (policyId ?? SharingPolicyCache.DynamicDefaultPolicy);
			bool flag = SharedConfiguration.IsDehydratedConfiguration(orgId);
			SharingPolicyCache.Tracer.TraceDebug<ADObjectId, string, OrganizationId>(0L, "Find Sharing policy {0} in {1} Org {2} ", policyId, flag ? "dehydrated" : "hydrated", orgId);
			if (!flag)
			{
				return SharingPolicyCache.GetHydratedPolicyFromCacheOrAD(orgId, policyId);
			}
			return SharingPolicyCache.GetDehydratedPolicyFromCacheOrAD(orgId, policyId);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00056750 File Offset: 0x00054950
		private static SharingPolicyCache GetHydratedPolicyFromCacheOrAD(OrganizationId orgId, ADObjectId policyId)
		{
			SharingPolicyCache sharingPolicyCache;
			lock (SharingPolicyCache.cacheSyncLock)
			{
				if (policyId == SharingPolicyCache.DynamicDefaultPolicy)
				{
					if (SharingPolicyCache.hydratedDefaultCache.TryGetValue(orgId, out sharingPolicyCache))
					{
						SharingPolicyCache.Tracer.TraceDebug<ADObjectId, OrganizationId>(0L, "Found Default Sharing Policy {0} in hydratedDefaultCache for Org {1}.", policyId, orgId);
						return sharingPolicyCache;
					}
				}
				else if (SharingPolicyCache.hydratedCache.TryGetValue(policyId.ObjectGuid, out sharingPolicyCache))
				{
					SharingPolicyCache.Tracer.TraceDebug<ADObjectId, OrganizationId>(0L, "Found Sharing Policy {0} in hydratedCache for Org {1}.", policyId, orgId);
					return sharingPolicyCache;
				}
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 211, "GetHydratedPolicyFromCacheOrAD", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\SharingPolicy\\SharingPolicyCache.cs");
			tenantOrTopologyConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			bool flag2 = policyId == SharingPolicyCache.DynamicDefaultPolicy;
			if (flag2)
			{
				policyId = SharingPolicyCache.GetDefaultSharingPolicyId(tenantOrTopologyConfigurationSession);
				if (policyId == null)
				{
					return null;
				}
			}
			SharingPolicy sharingPolicy = tenantOrTopologyConfigurationSession.Read<SharingPolicy>(policyId);
			if (sharingPolicy == null)
			{
				SharingPolicyCache.Tracer.TraceWarning<ADObjectId, bool>(0L, "Unable to find SharingPolicy {0} in the AD. PolicyId is default:{1}", policyId, flag2);
				sharingPolicyCache = null;
			}
			else
			{
				sharingPolicyCache = new SharingPolicyCache(sharingPolicy, false);
			}
			lock (SharingPolicyCache.cacheSyncLock)
			{
				SharingPolicyCache.Tracer.TraceDebug<ADObjectId>(0L, "Add the Sharing Policy {0} to cache.", policyId);
				SharingPolicyCache.hydratedCache[policyId.ObjectGuid] = sharingPolicyCache;
				if (sharingPolicyCache != null && flag2)
				{
					SharingPolicyCache.Tracer.TraceDebug<ADObjectId, OrganizationId>(0L, "Add Default Sharing Policy {0} of Org {1} to hydrated cache ", policyId, orgId);
					SharingPolicyCache.hydratedDefaultCache[orgId] = sharingPolicyCache;
				}
			}
			return sharingPolicyCache;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x000568D8 File Offset: 0x00054AD8
		private static SharingPolicyCache GetDehydratedPolicyFromCacheOrAD(OrganizationId orgId, ADObjectId policyId)
		{
			SharingPolicyCache sharingPolicyCache;
			if (policyId != SharingPolicyCache.DynamicDefaultPolicy)
			{
				lock (SharingPolicyCache.cacheSyncLock)
				{
					if (SharingPolicyCache.dehydratedDefaultCache.TryGetValue(policyId.ObjectGuid, out sharingPolicyCache))
					{
						SharingPolicyCache.Tracer.TraceDebug<ADObjectId>(0L, "Found Sharing Policy {0} in dehydrated cache.", policyId);
						return sharingPolicyCache;
					}
				}
			}
			SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(orgId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sharedConfiguration.GetSharedConfigurationSessionSettings(), 282, "GetDehydratedPolicyFromCacheOrAD", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\SharingPolicy\\SharingPolicyCache.cs");
			tenantOrTopologyConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			bool flag2 = policyId == SharingPolicyCache.DynamicDefaultPolicy;
			if (flag2)
			{
				SharingPolicyCache.Tracer.TraceDebug<OrganizationId>(0L, "Find Default Policy Id for Org {0} ", orgId);
				policyId = SharingPolicyCache.GetDefaultSharingPolicyId(tenantOrTopologyConfigurationSession);
				if (policyId == null)
				{
					return null;
				}
				lock (SharingPolicyCache.cacheSyncLock)
				{
					if (SharingPolicyCache.dehydratedDefaultCache.TryGetValue(policyId.ObjectGuid, out sharingPolicyCache))
					{
						SharingPolicyCache.Tracer.TraceDebug<ADObjectId>(0L, "Found Sharing Policy {0} in dehydrated cache.", policyId);
						return sharingPolicyCache;
					}
				}
			}
			SharingPolicy sharingPolicy = tenantOrTopologyConfigurationSession.Read<SharingPolicy>(policyId);
			if (sharingPolicy == null)
			{
				SharingPolicyCache.Tracer.TraceError<ADObjectId, bool>(0L, "Unable to find SharingPolicy {0} in the AD. PolicyId is default:{1}", policyId, flag2);
				return null;
			}
			sharingPolicyCache = new SharingPolicyCache(sharingPolicy, true);
			lock (SharingPolicyCache.cacheSyncLock)
			{
				SharingPolicyCache.dehydratedDefaultCache[policyId.ObjectGuid] = sharingPolicyCache;
			}
			return sharingPolicyCache;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00056A70 File Offset: 0x00054C70
		private static ADObjectId GetDefaultSharingPolicyId(IConfigurationSession configurationSession)
		{
			FederatedOrganizationId federatedOrganizationId = configurationSession.GetFederatedOrganizationId(configurationSession.SessionSettings.CurrentOrganizationId);
			if (federatedOrganizationId == null || federatedOrganizationId.DefaultSharingPolicyLink == null)
			{
				SharingPolicyCache.Tracer.TraceError<OrganizationId>(0L, "Could not find FederatedOrganizationId or DefaultSharingPolicyLink for Org {0}", configurationSession.SessionSettings.CurrentOrganizationId);
				return null;
			}
			SharingPolicyCache.Tracer.TraceDebug<OrganizationId, ADObjectId>(0L, "Found Default Policy Id {0} for Org {1} ", configurationSession.SessionSettings.CurrentOrganizationId, federatedOrganizationId.DefaultSharingPolicyLink);
			return federatedOrganizationId.DefaultSharingPolicyLink;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00056AF8 File Offset: 0x00054CF8
		private void PopulatePolicyHash()
		{
			SharingPolicyDomain[] array = null;
			if (this.policy.Domains != null)
			{
				array = new SharingPolicyDomain[this.policy.Domains.Count];
				for (int i = 0; i < this.policy.Domains.Count; i++)
				{
					array[i] = this.policy.Domains[i];
				}
				Array.Sort<SharingPolicyDomain>(array, (SharingPolicyDomain a, SharingPolicyDomain b) => StringComparer.OrdinalIgnoreCase.Compare(a.Domain, b.Domain));
			}
			StringBuilder stringBuilder = new StringBuilder("V1.0", 200);
			stringBuilder.Append(this.policy.Enabled);
			if (array != null)
			{
				foreach (SharingPolicyDomain sharingPolicyDomain in array)
				{
					stringBuilder.Append(",");
					stringBuilder.Append(sharingPolicyDomain.Domain);
					stringBuilder.Append(":");
					stringBuilder.Append(sharingPolicyDomain.Actions.ToString());
				}
			}
			string text = stringBuilder.ToString();
			SharingPolicyCache.Tracer.TraceDebug<string>(0L, "String representation of policy: {0}", text);
			byte[] bytes = Encoding.ASCII.GetBytes(text);
			using (SHA512Cng sha512Cng = new SHA512Cng())
			{
				this.hash = sha512Cng.ComputeHash(bytes);
			}
		}

		// Token: 0x04000939 RID: 2361
		internal static readonly ADObjectId DynamicDefaultPolicy = new ADObjectId(Guid.Empty);

		// Token: 0x0400093A RID: 2362
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x0400093B RID: 2363
		private static Dictionary<OrganizationId, SharingPolicyCache> hydratedDefaultCache = new Dictionary<OrganizationId, SharingPolicyCache>();

		// Token: 0x0400093C RID: 2364
		private static Dictionary<Guid, SharingPolicyCache> hydratedCache = new Dictionary<Guid, SharingPolicyCache>();

		// Token: 0x0400093D RID: 2365
		private static Dictionary<Guid, SharingPolicyCache> dehydratedDefaultCache = new Dictionary<Guid, SharingPolicyCache>();

		// Token: 0x0400093E RID: 2366
		private static readonly object cacheSyncLock = new object();

		// Token: 0x0400093F RID: 2367
		private readonly SharingPolicy policy;

		// Token: 0x04000940 RID: 2368
		private byte[] hash;
	}
}
