using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001F0 RID: 496
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoAuthorization
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x0004D00A File Offset: 0x0004B20A
		public PhotoAuthorization(LazyLookupTimeoutCache<OrganizationId, OrganizationIdCacheValue> organizationConfigCache, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("organizationConfigCache", organizationConfigCache);
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.organizationConfigCache = organizationConfigCache;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0004D040 File Offset: 0x0004B240
		public void Authorize(PhotoPrincipal requestor, PhotoPrincipal target)
		{
			if (requestor == null)
			{
				throw new ArgumentNullException("requestor");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (requestor.IsSame(target))
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Photo authorization: authorized: requestor and target are same principal.");
				return;
			}
			if (requestor.InSameOrganization(target))
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Photo authorization: authorized: requestor and target are in same organization.");
				return;
			}
			if (this.PhotoSharingEnabled(requestor, target))
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Photo authorization: authorized: photo sharing enabled.");
				return;
			}
			this.tracer.TraceDebug((long)this.GetHashCode(), "Photo authorization: ACCESS DENIED.");
			throw new AccessDeniedException(Strings.UserPhotoAccessDenied);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0004D0F0 File Offset: 0x0004B2F0
		private bool PhotoSharingEnabled(PhotoPrincipal requestor, PhotoPrincipal target)
		{
			OrganizationIdCacheValue organizationIdCacheValue = this.organizationConfigCache.Get((target.OrganizationId == null) ? OrganizationId.ForestWideOrgId : target.OrganizationId);
			if (organizationIdCacheValue == null)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "Photo authorization: target organization's configuration not available in cache.");
				return false;
			}
			foreach (string domain in requestor.GetEmailAddressDomains())
			{
				OrganizationRelationship organizationRelationship = organizationIdCacheValue.GetOrganizationRelationship(domain);
				if (organizationRelationship != null && organizationRelationship.Enabled && organizationRelationship.PhotosEnabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040009B0 RID: 2480
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x040009B1 RID: 2481
		private readonly LazyLookupTimeoutCache<OrganizationId, OrganizationIdCacheValue> organizationConfigCache;
	}
}
