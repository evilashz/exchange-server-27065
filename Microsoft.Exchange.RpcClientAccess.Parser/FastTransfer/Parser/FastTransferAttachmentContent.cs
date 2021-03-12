using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000155 RID: 341
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FastTransferAttachmentContent : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x00011ADF File Offset: 0x0000FCDF
		public FastTransferAttachmentContent(IAttachment attachment, bool isTopLevel) : base(isTopLevel)
		{
			this.attachment = attachment;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00011D08 File Offset: 0x0000FF08
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			base.CheckDisposed();
			IPropertyFilter handlerFilter = context.PropertyFilterFactory.GetAttachmentCopyToFilter(base.IsTopLevel);
			IPropertyFilter filter = handlerFilter;
			if (this.attachment.IsEmbeddedMessage)
			{
				filter = new AndPropertyFilter(new IPropertyFilter[]
				{
					handlerFilter,
					FastTransferAttachmentContent.ExcludeAttachmentDataObjectFilter
				});
			}
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.attachment.PropertyBag, filter)));
			if (this.attachment.IsEmbeddedMessage && handlerFilter.IncludeProperty(PropertyTag.AttachmentDataObject))
			{
				using (IMessage embeddedMessage = this.attachment.GetEmbeddedMessage())
				{
					yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferMessage(embeddedMessage, FastTransferMessage.MessageType.Embedded, false)));
				}
			}
			yield break;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00011EF0 File Offset: 0x000100F0
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			base.CheckDisposed();
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.attachment.PropertyBag)));
			PropertyTag marker;
			if (!context.NoMoreData && context.DataInterface.TryPeekMarker(out marker) && marker == PropertyTag.StartEmbed)
			{
				using (IMessage embeddedMessage = this.attachment.GetEmbeddedMessage())
				{
					yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferMessage(embeddedMessage, FastTransferMessage.MessageType.Embedded, false)));
				}
			}
			yield break;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00011F13 File Offset: 0x00010113
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferAttachmentContent>(this);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00011F1B File Offset: 0x0001011B
		protected override void InternalDispose()
		{
			this.attachment.Dispose();
			base.InternalDispose();
		}

		// Token: 0x04000338 RID: 824
		private static readonly IPropertyFilter ExcludeAttachmentDataObjectFilter = new ExcludingPropertyFilter(new PropertyTag[]
		{
			PropertyTag.AttachmentDataObject
		});

		// Token: 0x04000339 RID: 825
		private readonly IAttachment attachment;
	}
}
