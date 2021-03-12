using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000B1 RID: 177
	internal sealed class OrganizationCache : LazyLookupTimeoutCache<OrganizationId, bool?>
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x00033E11 File Offset: 0x00032011
		internal OrganizationCache() : base(OrganizationCache.OrganizationCacheBuckets, OrganizationCache.OrganizationCacheBucketSize, false, TimeSpan.FromSeconds((double)OrganizationCache.OrganizationCacheTimeoutInSecond), TimeSpan.FromSeconds((double)OrganizationCache.OrganizationCacheTimeoutInSecond))
		{
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00033E3C File Offset: 0x0003203C
		protected override bool? CreateOnCacheMiss(OrganizationId key, ref bool shouldAdd)
		{
			OrganizationCache.Tracer.TraceDebug<OrganizationCache>((long)this.GetHashCode(), "{0}: start loading cache...", this);
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(key);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 79, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\elc\\OrganizationCache.cs");
			Organization orgContainer = tenantOrTopologyConfigurationSession.GetOrgContainer();
			if (orgContainer != null)
			{
				OrganizationCache.Tracer.TraceDebug<OrganizationCache, bool>((long)this.GetHashCode(), "{0}: Loading cache successfully. Value: {1}", this, orgContainer.IsProcessEhaMigratedMessagesEnabled);
				shouldAdd = true;
				return new bool?(orgContainer.IsProcessEhaMigratedMessagesEnabled);
			}
			OrganizationCache.Tracer.TraceDebug<OrganizationCache>((long)this.GetHashCode(), "{0}: Loading cache failed.", this);
			shouldAdd = false;
			return null;
		}

		// Token: 0x04000540 RID: 1344
		private static readonly int OrganizationCacheBuckets = 1;

		// Token: 0x04000541 RID: 1345
		private static readonly int OrganizationCacheBucketSize = 1000;

		// Token: 0x04000542 RID: 1346
		private static readonly int OrganizationCacheTimeoutInSecond = 10800;

		// Token: 0x04000543 RID: 1347
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;
	}
}
