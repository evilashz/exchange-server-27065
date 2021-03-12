using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000008 RID: 8
	internal class OrganizationIdConverter
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002484 File Offset: 0x00000684
		public OrganizationId GetOrganizationId(string externalDirectoryOrgId)
		{
			OrganizationId result = OrganizationId.ForestWideOrgId;
			if (!string.IsNullOrEmpty(externalDirectoryOrgId))
			{
				try
				{
					result = OrganizationId.FromExternalDirectoryOrganizationId(Guid.Parse(externalDirectoryOrgId));
				}
				catch (CannotResolveExternalDirectoryOrganizationIdException exception)
				{
					PushNotificationsCrimsonEvents.CannotResolveTenantId.LogPeriodic<string>(externalDirectoryOrgId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, exception.ToTraceString());
				}
			}
			return result;
		}

		// Token: 0x04000007 RID: 7
		public static readonly OrganizationIdConverter Default = new OrganizationIdConverter();
	}
}
