using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.Injector
{
	// Token: 0x0200008C RID: 140
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BackCompatibleInjectorClient : DisposeTrackableBase, IInjectorService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000D394 File Offset: 0x0000B594
		public BackCompatibleInjectorClient(MailboxLoadBalanceService service, MoveInjector moveInjector)
		{
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			this.service = service;
			this.moveInjector = moveInjector;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			serverVersion = LoadBalancerVersionInformation.InjectorVersion;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000D3C1 File Offset: 0x0000B5C1
		public void InjectMoves(Guid targetDatabase, string batchName, IEnumerable<LoadEntity> mailboxes)
		{
			this.moveInjector.InjectMovesOnCompatibilityMode(this.service.GetDatabaseData(targetDatabase, false), BatchName.FromString(batchName), mailboxes, false);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000D3E4 File Offset: 0x0000B5E4
		public void InjectSingleMove(Guid targetDatabase, string batchName, LoadEntity mailbox)
		{
			this.InjectMoves(targetDatabase, batchName, new LoadEntity[]
			{
				mailbox
			});
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000D405 File Offset: 0x0000B605
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BackCompatibleInjectorClient>(this);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000D40D File Offset: 0x0000B60D
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x040001A5 RID: 421
		private readonly MailboxLoadBalanceService service;

		// Token: 0x040001A6 RID: 422
		private readonly MoveInjector moveInjector;
	}
}
