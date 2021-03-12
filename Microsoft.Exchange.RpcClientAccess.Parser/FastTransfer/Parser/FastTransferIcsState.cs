using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000165 RID: 357
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferIcsState : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x00014AFC File Offset: 0x00012CFC
		public FastTransferIcsState(IIcsState icsState) : base(true)
		{
			this.icsState = icsState;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00014BEC File Offset: 0x00012DEC
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			context.DataInterface.PutMarker(PropertyTag.IncrSyncStateBegin);
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.icsState.PropertyBag, FastTransferIcsState.AllIcsStateProperties)
			{
				SkipPropertyError = true
			}));
			context.DataInterface.PutMarker(PropertyTag.IncrSyncStateEnd);
			yield break;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00014CE8 File Offset: 0x00012EE8
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			context.DataInterface.ReadMarker(PropertyTag.IncrSyncStateBegin);
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.icsState.PropertyBag, FastTransferIcsState.AllIcsStateProperties)));
			context.DataInterface.ReadMarker(PropertyTag.IncrSyncStateEnd);
			this.icsState.Flush();
			yield break;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00014D0B File Offset: 0x00012F0B
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.icsState);
			base.InternalDispose();
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00014D1E File Offset: 0x00012F1E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferIcsState>(this);
		}

		// Token: 0x04000364 RID: 868
		private readonly IIcsState icsState;

		// Token: 0x04000365 RID: 869
		public static readonly PropertyTag CnsetSeen = new PropertyTag(1737883906U);

		// Token: 0x04000366 RID: 870
		public static readonly PropertyTag CnsetSeenAssociated = new PropertyTag(1742340354U);

		// Token: 0x04000367 RID: 871
		public static readonly PropertyTag IdsetGiven = new PropertyTag(1075249410U);

		// Token: 0x04000368 RID: 872
		public static readonly PropertyTag CnsetRead = new PropertyTag(1741816066U);

		// Token: 0x04000369 RID: 873
		public static readonly PropertyTag[] AllIcsStateProperties = new PropertyTag[]
		{
			FastTransferIcsState.CnsetSeen,
			FastTransferIcsState.CnsetSeenAssociated,
			FastTransferIcsState.IdsetGiven,
			FastTransferIcsState.CnsetRead
		};
	}
}
