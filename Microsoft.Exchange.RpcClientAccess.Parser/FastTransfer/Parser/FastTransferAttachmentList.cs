using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000157 RID: 343
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferAttachmentList : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00011FCC File Offset: 0x000101CC
		public FastTransferAttachmentList(IMessage message) : base(false)
		{
			Util.ThrowOnNullArgument(message, "message");
			this.message = message;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001214C File Offset: 0x0001034C
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			foreach (IAttachmentHandle handle in this.message.GetAttachments())
			{
				FastTransferAttachment fastTransferAttachment = this.CreateDownloadFastTransferAttachment(handle);
				yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferAttachment));
			}
			yield break;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00012170 File Offset: 0x00010370
		private FastTransferAttachment CreateDownloadFastTransferAttachment(IAttachmentHandle handle)
		{
			FastTransferAttachment result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IAttachment attachment = handle.GetAttachment();
				disposeGuard.Add<IAttachment>(attachment);
				FastTransferAttachment fastTransferAttachment = new FastTransferAttachment(attachment, false);
				disposeGuard.Add<FastTransferAttachment>(fastTransferAttachment);
				disposeGuard.Success();
				result = fastTransferAttachment;
			}
			return result;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000122B0 File Offset: 0x000104B0
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			PropertyTag marker = default(PropertyTag);
			while (!context.NoMoreData && context.DataInterface.TryPeekMarker(out marker) && marker == PropertyTag.NewAttach)
			{
				FastTransferAttachment fastTransferAttachment = this.CreateUploadFastTransferAttachment();
				yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferAttachment));
			}
			yield break;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000122D4 File Offset: 0x000104D4
		private FastTransferAttachment CreateUploadFastTransferAttachment()
		{
			FastTransferAttachment result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IAttachment attachment = this.message.CreateAttachment();
				disposeGuard.Add<IAttachment>(attachment);
				FastTransferAttachment fastTransferAttachment = new FastTransferAttachment(attachment, false);
				disposeGuard.Add<FastTransferAttachment>(fastTransferAttachment);
				disposeGuard.Success();
				result = fastTransferAttachment;
			}
			return result;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001233C File Offset: 0x0001053C
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferAttachmentList>(this);
		}

		// Token: 0x0400033A RID: 826
		private readonly IMessage message;
	}
}
