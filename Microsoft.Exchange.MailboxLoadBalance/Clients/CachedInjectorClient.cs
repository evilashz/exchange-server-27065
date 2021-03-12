using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.Clients
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CachedInjectorClient : CachedClient, IInjectorService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00006EB4 File Offset: 0x000050B4
		public CachedInjectorClient(IInjectorService client) : base(client as IWcfClient)
		{
			this.client = client;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006EC9 File Offset: 0x000050C9
		void IVersionedService.ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			this.client.ExchangeVersionInformation(clientVersion, out serverVersion);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006ED8 File Offset: 0x000050D8
		void IInjectorService.InjectMoves(Guid targetDatabase, string batchName, IEnumerable<LoadEntity> mailboxes)
		{
			this.client.InjectMoves(targetDatabase, batchName, mailboxes);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006EE8 File Offset: 0x000050E8
		void IInjectorService.InjectSingleMove(Guid targetDatabase, string batchName, LoadEntity mailbox)
		{
			this.client.InjectSingleMove(targetDatabase, batchName, mailbox);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006EF8 File Offset: 0x000050F8
		internal override void Cleanup()
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006EFA File Offset: 0x000050FA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CachedInjectorClient>(this);
		}

		// Token: 0x04000090 RID: 144
		private readonly IInjectorService client;
	}
}
