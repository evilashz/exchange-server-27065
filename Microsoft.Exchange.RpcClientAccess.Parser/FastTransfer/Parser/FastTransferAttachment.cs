using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000154 RID: 340
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferAttachment : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x000113FA File Offset: 0x0000F5FA
		public FastTransferAttachment(IAttachment attachment, bool isTopLevel) : base(isTopLevel)
		{
			Util.ThrowOnNullArgument(attachment, "attachment");
			this.attachment = attachment;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00011624 File Offset: 0x0000F824
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			context.DataInterface.PutMarker(PropertyTag.NewAttach);
			PropertyValue attachmentNumber = this.attachment.PropertyBag.GetAnnotatedProperty(PropertyTag.AttachmentNumber).PropertyValue;
			if (attachmentNumber.IsError)
			{
				throw new NotSupportedException("Found an attachment without an attachment number.");
			}
			yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, attachmentNumber));
			if (context.IsMovingMailbox)
			{
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, this.attachment.PropertyBag.GetAnnotatedProperty(PropertyTag.InstanceIdBin).PropertyValue));
			}
			IPropertyFilter filter;
			if (this.attachment.IsEmbeddedMessage)
			{
				filter = context.PropertyFilterFactory.GetEmbeddedMessageFilter(false);
			}
			else
			{
				filter = context.PropertyFilterFactory.GetAttachmentFilter(base.IsTopLevel);
			}
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.attachment.PropertyBag, filter)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.SerializeEmbeddedMessage(context)));
			context.DataInterface.PutMarker(PropertyTag.EndAttach);
			yield break;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000116F0 File Offset: 0x0000F8F0
		private IEnumerator<FastTransferStateMachine?> SerializeEmbeddedMessage(FastTransferDownloadContext context)
		{
			if (this.attachment.IsEmbeddedMessage)
			{
				FastTransferMessage fastTransferMessage = this.CreateDownloadFastTransferEmbeddedMessage();
				yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferMessage));
			}
			yield break;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00011714 File Offset: 0x0000F914
		private FastTransferMessage CreateDownloadFastTransferEmbeddedMessage()
		{
			FastTransferMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IMessage embeddedMessage = this.attachment.GetEmbeddedMessage();
				disposeGuard.Add<IMessage>(embeddedMessage);
				FastTransferMessage fastTransferMessage = new FastTransferMessage(embeddedMessage, FastTransferMessage.MessageType.Embedded, false);
				disposeGuard.Add<FastTransferMessage>(fastTransferMessage);
				disposeGuard.Success();
				result = fastTransferMessage;
			}
			return result;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00011954 File Offset: 0x0000FB54
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			context.DataInterface.ReadMarker(PropertyTag.NewAttach);
			SingleMemberPropertyBag singleMemberPropertyBag = new SingleMemberPropertyBag(PropertyTag.AttachmentNumber);
			yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, singleMemberPropertyBag));
			int attachmentNumber = this.attachment.AttachmentNumber;
			singleMemberPropertyBag.PropertyValue.GetValue<int>();
			this.attachment.PropertyBag.SetProperty(singleMemberPropertyBag.PropertyValue);
			if (context.IsMovingMailbox)
			{
				SingleMemberPropertyBag longTermIdPropertyBag = new SingleMemberPropertyBag(PropertyTag.LongTermId);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, longTermIdPropertyBag));
			}
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.attachment.PropertyBag)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.ParseEmbeddedMessage(context)));
			context.DataInterface.ReadMarker(PropertyTag.EndAttach);
			this.attachment.Save();
			yield break;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00011A38 File Offset: 0x0000FC38
		private IEnumerator<FastTransferStateMachine?> ParseEmbeddedMessage(FastTransferUploadContext context)
		{
			PropertyTag marker;
			if (context.DataInterface.TryPeekMarker(out marker) && marker == PropertyTag.StartEmbed)
			{
				FastTransferMessage fastTransferMessage = this.CreateUploadFastTransferEmbeddedMessage();
				yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferMessage));
			}
			yield break;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00011A5C File Offset: 0x0000FC5C
		private FastTransferMessage CreateUploadFastTransferEmbeddedMessage()
		{
			FastTransferMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IMessage embeddedMessage = this.attachment.GetEmbeddedMessage();
				disposeGuard.Add<IMessage>(embeddedMessage);
				FastTransferMessage fastTransferMessage = new FastTransferMessage(embeddedMessage, FastTransferMessage.MessageType.Embedded, false);
				disposeGuard.Add<FastTransferMessage>(fastTransferMessage);
				disposeGuard.Success();
				result = fastTransferMessage;
			}
			return result;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.attachment);
			base.InternalDispose();
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00011AD7 File Offset: 0x0000FCD7
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferAttachment>(this);
		}

		// Token: 0x04000337 RID: 823
		private readonly IAttachment attachment;
	}
}
