using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.Injector
{
	// Token: 0x0200008D RID: 141
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MissingCapabilityInjectorClientDecorator : DisposeTrackableBase, IInjectorService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x0000D40F File Offset: 0x0000B60F
		protected MissingCapabilityInjectorClientDecorator(IInjectorService service)
		{
			this.service = service;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000D41E File Offset: 0x0000B61E
		public virtual void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			this.service.ExchangeVersionInformation(clientVersion, out serverVersion);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000D42D File Offset: 0x0000B62D
		public virtual void InjectMoves(Guid targetDatabase, string batchName, IEnumerable<LoadEntity> mailboxes)
		{
			this.service.InjectMoves(targetDatabase, batchName, mailboxes);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000D43D File Offset: 0x0000B63D
		public virtual void InjectSingleMove(Guid targetDatabase, string batchName, LoadEntity mailbox)
		{
			this.service.InjectSingleMove(targetDatabase, batchName, mailbox);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000D44D File Offset: 0x0000B64D
		protected override void InternalDispose(bool disposing)
		{
			if (this.service != null)
			{
				this.service.Dispose();
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000D462 File Offset: 0x0000B662
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MissingCapabilityInjectorClientDecorator>(this);
		}

		// Token: 0x040001A7 RID: 423
		private readonly IInjectorService service;
	}
}
