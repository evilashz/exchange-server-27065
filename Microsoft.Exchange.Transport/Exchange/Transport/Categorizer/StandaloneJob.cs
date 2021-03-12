using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001C4 RID: 452
	internal class StandaloneJob : Job
	{
		// Token: 0x060014A6 RID: 5286 RVA: 0x000530A3 File Offset: 0x000512A3
		private StandaloneJob(long id, TransportMailItem rootTransportMailItem, AcquireToken acquireToken, ThrottlingContext context, QueuedRecipientsByAgeToken token, IList<StageInfo> stages) : base(id, context, token, stages)
		{
			this.acquireToken = acquireToken;
			this.rootTransportMailItem = rootTransportMailItem;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000530CC File Offset: 0x000512CC
		public static StandaloneJob NewJob(TransportMailItem rootTransportMailItem, AcquireToken acquireToken, ThrottlingContext context, QueuedRecipientsByAgeToken token, IList<StageInfo> stages)
		{
			long nextJobId = Job.nextJobId;
			Job.nextJobId = nextJobId + 1L;
			return new StandaloneJob(nextJobId, rootTransportMailItem, acquireToken, context, token, stages);
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x000530E7 File Offset: 0x000512E7
		protected override bool IsRetired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x000530EC File Offset: 0x000512EC
		public void RunToCompletion()
		{
			for (;;)
			{
				this.manualResetEventSlim.Reset();
				if (!base.ExecutePendingTasks() && base.IsEmpty)
				{
					break;
				}
				this.manualResetEventSlim.Wait();
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00053123 File Offset: 0x00051323
		public override bool TryGetDeferToken(TransportMailItem mailItem, out AcquireToken deferToken)
		{
			if (object.ReferenceEquals(mailItem, this.rootTransportMailItem))
			{
				deferToken = this.acquireToken;
				return true;
			}
			deferToken = null;
			return false;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00053141 File Offset: 0x00051341
		public override void MarkDeferred(TransportMailItem mailItem)
		{
			if (object.ReferenceEquals(this.rootTransportMailItem, mailItem))
			{
				base.RootMailItemDeferred = true;
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00053158 File Offset: 0x00051358
		protected override void CompletedInternal(TransportMailItem mailItem)
		{
			this.manualResetEventSlim.Set();
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00053165 File Offset: 0x00051365
		protected override void PendingInternal()
		{
			this.manualResetEventSlim.Set();
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00053172 File Offset: 0x00051372
		protected override void GoneAsyncInternal()
		{
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00053174 File Offset: 0x00051374
		protected override void RetireInternal(TransportMailItem mailItem)
		{
		}

		// Token: 0x04000A70 RID: 2672
		private readonly ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(false);

		// Token: 0x04000A71 RID: 2673
		private readonly TransportMailItem rootTransportMailItem;

		// Token: 0x04000A72 RID: 2674
		private readonly AcquireToken acquireToken;
	}
}
