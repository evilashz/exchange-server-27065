using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security.ExternalAuthentication;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Data.Storage.Authentication
{
	// Token: 0x02000DEF RID: 3567
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TargetUriResolver
	{
		// Token: 0x06007AA8 RID: 31400 RVA: 0x0021E744 File Offset: 0x0021C944
		public static TokenTarget Resolve(SmtpAddress smtpAddress, OrganizationId organizationId)
		{
			return TargetUriResolver.Resolve(smtpAddress.Domain.ToString(), organizationId);
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x0021E758 File Offset: 0x0021C958
		public static TokenTarget Resolve(string domain, OrganizationId organizationId)
		{
			TokenTarget tokenTarget = TargetUriResolver.FromOrganizationRelationship(domain, organizationId);
			if (tokenTarget != null)
			{
				return tokenTarget;
			}
			tokenTarget = TargetUriViaGetFederationInformation.Singleton.Get(domain);
			if (tokenTarget != null)
			{
				return tokenTarget;
			}
			tokenTarget = TargetUriViaSCP.Singleton.Get(domain);
			if (tokenTarget != null)
			{
				return tokenTarget;
			}
			return null;
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x0021E795 File Offset: 0x0021C995
		internal static void ClearCache()
		{
			TargetUriViaSCP.Singleton.Clear();
			TargetUriViaGetFederationInformation.Singleton.Clear();
			OrganizationIdCache.Singleton.Clear();
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x0021E7B8 File Offset: 0x0021C9B8
		private static TokenTarget FromOrganizationRelationship(string domain, OrganizationId organizationId)
		{
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			TargetUriResolver.Tracer.TraceDebug<string, OrganizationId>(0L, "Searching for OrganizationRelationship that matches domain {0} in organization {1}", domain, organizationId);
			OrganizationRelationship organizationRelationship = organizationIdCacheValue.GetOrganizationRelationship(domain);
			if (organizationRelationship == null)
			{
				TargetUriResolver.Tracer.TraceError<string, OrganizationId>(0L, "Found no OrganizationRelationship that matches domain {0} in organization {1}", domain, organizationId);
				return null;
			}
			if (organizationRelationship.TargetApplicationUri == null)
			{
				TargetUriResolver.Tracer.TraceError<string, OrganizationId, ADObjectId>(0L, "Found OrganizationRelationship that matches domain {0} in organization {1}, but it has not TargetApplicationUri. OrganizationRelationship is {2}", domain, organizationId, organizationRelationship.Id);
				return null;
			}
			TokenTarget tokenTarget = organizationRelationship.GetTokenTarget();
			TargetUriResolver.Tracer.TraceDebug(0L, "Found OrganizationRelationship that matches domain {0} in organization {1}. Target is '{2}'. OrganizationRelationship is {3}", new object[]
			{
				domain,
				organizationId,
				tokenTarget,
				organizationRelationship.Id
			});
			return tokenTarget;
		}

		// Token: 0x04005484 RID: 21636
		private static readonly Trace Tracer = ExTraceGlobals.TargetUriResolverTracer;
	}
}
