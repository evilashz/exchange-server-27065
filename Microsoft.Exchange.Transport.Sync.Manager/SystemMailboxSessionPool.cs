using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SystemMailboxSessionPool : Pool<MailboxSession>
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00006ED8 File Offset: 0x000050D8
		internal SystemMailboxSessionPool(int capacity, int maxCapacity, Guid databaseGuid, Guid systemMailboxGuid) : base(capacity, maxCapacity, ContentAggregationConfig.MaxSystemMailboxSessionsUnusedPeriod)
		{
			this.databaseGuid = databaseGuid;
			this.systemMailboxGuid = systemMailboxGuid;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006EF8 File Offset: 0x000050F8
		internal MailboxSession GetSystemMailbox(string clientConnectionString)
		{
			SyncUtilities.ThrowIfGuidEmpty("systemMailboxGuid", this.systemMailboxGuid);
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromLocalServerMailboxGuid(ADSessionSettings.FromRootOrgScopeSet(), this.databaseGuid, this.systemMailboxGuid);
			return MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, clientConnectionString, true);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006F3C File Offset: 0x0000513C
		protected override MailboxSession CreateItem(out bool needsBackOff)
		{
			needsBackOff = false;
			try
			{
				return this.GetSystemMailbox(SystemMailboxSessionPool.ClientInfoString);
			}
			catch (StorageTransientException ex)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)160UL, SystemMailboxSessionPool.Tracer, (long)this.GetHashCode(), "CreateItem: Encountered a transient exception when trying to get the system mailbox for database {0}: {1}", new object[]
				{
					this.databaseGuid,
					ex
				});
			}
			catch (StoragePermanentException ex2)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)161UL, SystemMailboxSessionPool.Tracer, (long)this.GetHashCode(), "CreateItem: Encountered a permanent exception when trying to get the system mailbox for database {0}: {1}", new object[]
				{
					this.databaseGuid,
					ex2
				});
			}
			needsBackOff = true;
			return null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000700C File Offset: 0x0000520C
		protected override void DestroyItem(MailboxSession item)
		{
			try
			{
				item.Dispose();
			}
			catch (StorageTransientException ex)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)162UL, SystemMailboxSessionPool.Tracer, (long)this.GetHashCode(), "DestroyItem: Encountered a transient exception when trying to destroy the system mailbox for database {0}: {1}", new object[]
				{
					this.databaseGuid,
					ex
				});
			}
			catch (StoragePermanentException ex2)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)163UL, SystemMailboxSessionPool.Tracer, (long)this.GetHashCode(), "DestroyItem: Encountered a permanent exception when trying to destroy the system mailbox for database {0}: {1}", new object[]
				{
					this.databaseGuid,
					ex2
				});
			}
		}

		// Token: 0x04000056 RID: 86
		private static readonly Trace Tracer = ExTraceGlobals.SystemMailboxSessionPoolTracer;

		// Token: 0x04000057 RID: 87
		private static readonly string ClientInfoString = "Client=TransportSync;Action=SystemMailboxSessionPool";

		// Token: 0x04000058 RID: 88
		private readonly Guid databaseGuid;

		// Token: 0x04000059 RID: 89
		private Guid systemMailboxGuid;
	}
}
