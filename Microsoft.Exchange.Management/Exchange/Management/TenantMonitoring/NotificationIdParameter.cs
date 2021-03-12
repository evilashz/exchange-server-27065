using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CF8 RID: 3320
	[Serializable]
	public sealed class NotificationIdParameter : IIdentityParameter
	{
		// Token: 0x06007FA9 RID: 32681 RVA: 0x00209B41 File Offset: 0x00207D41
		public NotificationIdParameter(string identity)
		{
			this.rawIdentity = identity;
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x00209B50 File Offset: 0x00207D50
		public NotificationIdParameter(NotificationIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.rawIdentity = identity.ToString();
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x00209B72 File Offset: 0x00207D72
		public NotificationIdParameter(Notification notification) : this((NotificationIdentity)notification.Identity)
		{
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x00209B85 File Offset: 0x00207D85
		public NotificationIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06007FAD RID: 32685 RVA: 0x00209B9F File Offset: 0x00207D9F
		public NotificationIdParameter() : this(string.Empty)
		{
		}

		// Token: 0x06007FAE RID: 32686 RVA: 0x00209BAC File Offset: 0x00207DAC
		public static NotificationIdParameter Parse(string identity)
		{
			return new NotificationIdParameter(identity);
		}

		// Token: 0x06007FAF RID: 32687 RVA: 0x00209BB4 File Offset: 0x00207DB4
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06007FB0 RID: 32688 RVA: 0x00209BCC File Offset: 0x00207DCC
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = null;
			QueryFilter queryFilter = null;
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					optionalData.AdditionalFilter
				});
			}
			return session.FindPaged<T>(queryFilter, rootId, false, null, 0);
		}

		// Token: 0x06007FB1 RID: 32689 RVA: 0x00209C14 File Offset: 0x00207E14
		public void Initialize(ObjectId objectId)
		{
			NotificationIdentity notificationIdentity = objectId as NotificationIdentity;
			if (notificationIdentity == null)
			{
				throw new ArgumentException("objectId is of the wrong type", "objectId");
			}
			this.rawIdentity = notificationIdentity.ToString();
		}

		// Token: 0x1700279F RID: 10143
		// (get) Token: 0x06007FB2 RID: 32690 RVA: 0x00209C47 File Offset: 0x00207E47
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x04003EA0 RID: 16032
		private string rawIdentity;
	}
}
