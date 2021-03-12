using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E4 RID: 740
	internal abstract class SubmitStage : SynchronousPipelineStageBase
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0005FCB2 File Offset: 0x0005DEB2
		internal MessageItem MessageToSubmit
		{
			get
			{
				if (this.message == null)
				{
					this.message = base.WorkItem.Message.MessageToSubmit;
				}
				return this.message;
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0005FCD8 File Offset: 0x0005DED8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SubmitStage>(this);
		}

		// Token: 0x04000D5B RID: 3419
		private MessageItem message;
	}
}
