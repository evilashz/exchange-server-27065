using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200016A RID: 362
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferMessageIterator : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x00016DEA File Offset: 0x00014FEA
		public FastTransferMessageIterator(IMessageIterator messageIterator, FastTransferCopyMessagesFlag options, bool isTopLevel) : base(isTopLevel)
		{
			Util.ThrowOnNullArgument(messageIterator, "messageIterator");
			this.messageIterator = messageIterator;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00016E05 File Offset: 0x00015005
		public FastTransferMessageIterator(IMessageIteratorClient messageIteratorClient, bool isTopLevel) : base(isTopLevel)
		{
			Util.ThrowOnNullArgument(messageIteratorClient, "messageIteratorClient");
			this.messageIteratorClient = messageIteratorClient;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00016EDC File Offset: 0x000150DC
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			yield return new FastTransferStateMachine?(FastTransferPropertyValue.SkipPropertyIfExists(context, PropertyTag.DNPrefix));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.ParseMessages(context)));
			yield break;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00016EFF File Offset: 0x000150FF
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			return this.SerializeMessages(context);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00016F08 File Offset: 0x00015108
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.messageIterator);
			Util.DisposeIfPresent(this.messageIteratorClient);
			base.InternalDispose();
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00016F26 File Offset: 0x00015126
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferMessageIterator>(this);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00017140 File Offset: 0x00015340
		private IEnumerator<FastTransferStateMachine?> ParseMessages(FastTransferUploadContext context)
		{
			while (!context.NoMoreData)
			{
				PropertyTag propertyTag;
				if (context.DataInterface.TryPeekMarker(out propertyTag))
				{
					if (!(propertyTag == PropertyTag.StartMessage) && !(propertyTag == PropertyTag.StartFAIMsg))
					{
						break;
					}
					bool isAssociatedMessage = propertyTag == PropertyTag.StartFAIMsg;
					FastTransferMessage.MessageType messageType = (!isAssociatedMessage) ? FastTransferMessage.MessageType.Normal : FastTransferMessage.MessageType.Associated;
					using (IMessage message = this.messageIteratorClient.UploadMessage(isAssociatedMessage))
					{
						yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferMessage(message, messageType, base.IsTopLevel)));
					}
				}
				else
				{
					if (!propertyTag.IsMetaProperty)
					{
						break;
					}
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.SkipPropertyIfExists(context, propertyTag));
				}
			}
			yield break;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00017308 File Offset: 0x00015508
		private IEnumerator<FastTransferStateMachine?> SerializeMessages(FastTransferDownloadContext context)
		{
			using (IEnumerator<IMessage> messages = this.messageIterator.GetMessages())
			{
				while (messages.MoveNext())
				{
					IMessage message = messages.Current;
					if (message != null)
					{
						FastTransferMessage fastTransferMessage = this.CreateFastTransferMessage(message);
						yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferMessage));
					}
					else
					{
						yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, FastTransferMessageIterator.partialCompletionWarningPropertyValue));
					}
				}
			}
			yield break;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001732C File Offset: 0x0001552C
		private FastTransferMessage CreateFastTransferMessage(IMessage message)
		{
			FastTransferMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<IMessage>(message);
				FastTransferMessage.MessageType messageType = message.IsAssociated ? FastTransferMessage.MessageType.Associated : FastTransferMessage.MessageType.Normal;
				FastTransferMessage fastTransferMessage = new FastTransferMessage(message, messageType, base.IsTopLevel);
				disposeGuard.Add<FastTransferMessage>(fastTransferMessage);
				disposeGuard.Success();
				result = fastTransferMessage;
			}
			return result;
		}

		// Token: 0x04000382 RID: 898
		private static readonly PropertyValue partialCompletionWarningPropertyValue = new PropertyValue(PropertyTag.EcWarning, 263808);

		// Token: 0x04000383 RID: 899
		private readonly IMessageIterator messageIterator;

		// Token: 0x04000384 RID: 900
		private readonly IMessageIteratorClient messageIteratorClient;
	}
}
