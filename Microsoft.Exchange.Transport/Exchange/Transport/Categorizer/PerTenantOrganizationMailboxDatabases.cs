using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001BC RID: 444
	internal sealed class PerTenantOrganizationMailboxDatabases : TenantConfigurationCacheableItemBase
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00052597 File Offset: 0x00050797
		public IList<ADObjectId> Databases
		{
			get
			{
				return this.databases;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0005259F File Offset: 0x0005079F
		public override long ItemSize
		{
			get
			{
				if (this.databases == null)
				{
					throw new InvalidOperationException("ItemSize is invokes before the object is initialized");
				}
				return (long)(IntPtr.Size + 50 * this.databases.Count);
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000525C9 File Offset: 0x000507C9
		public override bool InitializeWithoutRegistration(IConfigurationSession session, bool allowExceptions)
		{
			throw new NotSupportedException("InitializeWithoutRegistration is not supported in PerTenantOrganizationMailboxDatabases");
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00052600 File Offset: 0x00050800
		public override bool TryInitialize(OrganizationId organizationId, CacheNotificationHandler cacheNotificationHandler, object state)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				this.Initialize(organizationId, cacheNotificationHandler, state);
			}, 0);
			if (adoperationResult.Succeeded)
			{
				return true;
			}
			ProxyHubSelectorComponent.Tracer.TraceError<OrganizationId, object>(0L, "Failed to read organization mailboxes for organization <{0}>; exception details: {1}", organizationId, adoperationResult.Exception ?? "<none>");
			return false;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000526A0 File Offset: 0x000508A0
		public override bool Initialize(OrganizationId organizationId, CacheNotificationHandler cacheNotificationHandler, object state)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			ADUser[] array = OrganizationMailbox.FindByOrganizationId(organizationId, OrganizationCapability.MailRouting);
			if (array != null && array.Length > 0)
			{
				HashSet<ADObjectId> source = new HashSet<ADObjectId>(from mailbox in array
				select mailbox.Database into databaseId
				where databaseId != null
				select databaseId);
				this.databases = Array.AsReadOnly<ADObjectId>(source.ToArray<ADObjectId>());
			}
			else
			{
				this.databases = PerTenantOrganizationMailboxDatabases.NoDatabases;
			}
			if (this.databases.Count == 0)
			{
				ProxyHubSelectorComponent.Tracer.TraceError<OrganizationId>(0L, "Failed to find any organization mailboxes with non-null databases for organization <{0}>", organizationId);
			}
			return true;
		}

		// Token: 0x04000A59 RID: 2649
		private const int EstimatedADObjectIdSize = 50;

		// Token: 0x04000A5A RID: 2650
		private static readonly IList<ADObjectId> NoDatabases = Array.AsReadOnly<ADObjectId>(new ADObjectId[0]);

		// Token: 0x04000A5B RID: 2651
		private IList<ADObjectId> databases;
	}
}
