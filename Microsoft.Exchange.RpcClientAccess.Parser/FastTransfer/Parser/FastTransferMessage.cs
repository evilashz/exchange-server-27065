using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000166 RID: 358
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferMessage : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00014DC2 File Offset: 0x00012FC2
		public FastTransferMessage(IMessage message, FastTransferMessage.MessageType messageType, bool isTopLevel) : base(isTopLevel)
		{
			Util.ThrowOnNullArgument(message, "message");
			this.message = message;
			this.messageType = messageType;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00014DE4 File Offset: 0x00012FE4
		private PropertyTag StartMarker
		{
			get
			{
				switch (this.messageType)
				{
				case FastTransferMessage.MessageType.Normal:
					return PropertyTag.StartMessage;
				case FastTransferMessage.MessageType.Associated:
					return PropertyTag.StartFAIMsg;
				case FastTransferMessage.MessageType.Embedded:
					return PropertyTag.StartEmbed;
				default:
					throw new InvalidOperationException(string.Format("Invalid MessageType {0}.", this.messageType));
				}
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00014E38 File Offset: 0x00013038
		private PropertyTag EndMarker
		{
			get
			{
				if (this.messageType != FastTransferMessage.MessageType.Embedded)
				{
					return PropertyTag.EndMessage;
				}
				return PropertyTag.EndEmbed;
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00015104 File Offset: 0x00013304
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			context.DataInterface.PutMarker(this.StartMarker);
			if (context.IsMovingMailbox)
			{
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, this.message.PropertyBag.GetAnnotatedProperty(PropertyTag.LongTermId).PropertyValue));
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, this.message.PropertyBag.GetAnnotatedProperty(PropertyTag.InstanceIdBin).PropertyValue));
			}
			else if (this.messageType == FastTransferMessage.MessageType.Embedded)
			{
				Feature.Stubbed(86205, "Need to serialize actual MID of embedded message.");
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, FastTransferMessage.EmptyMidPropertyValue));
			}
			else
			{
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, this.message.PropertyBag.GetAnnotatedProperty(PropertyTag.Mid).PropertyValue));
			}
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.message.PropertyBag, context.PropertyFilterFactory.GetMessageFilter(base.IsTopLevel))));
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferRecipientList(this.message)));
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferAttachmentList(this.message)));
			context.DataInterface.PutMarker(this.EndMarker);
			if (this.messageType != FastTransferMessage.MessageType.Embedded)
			{
				context.IncrementProgress();
			}
			yield break;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001542C File Offset: 0x0001362C
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			context.DataInterface.ReadMarker(this.StartMarker);
			yield return null;
			if (context.IsMovingMailbox)
			{
				SingleMemberPropertyBag longTermIdPropertyBag = new SingleMemberPropertyBag(PropertyTag.LongTermId);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, longTermIdPropertyBag));
				this.message.SetLongTermId(StoreLongTermId.Parse((byte[])longTermIdPropertyBag.PropertyValue.Value, true));
				SingleMemberPropertyBag instanceIdPropertyBag = new SingleMemberPropertyBag(PropertyTag.LongTermId);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, instanceIdPropertyBag));
			}
			else if (this.messageType == FastTransferMessage.MessageType.Embedded)
			{
				Feature.Stubbed(86205, "Need to read and process actual MID if found to be needed.");
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, new SingleMemberPropertyBag(PropertyTag.Mid)));
			}
			PropertyTag propertyTag;
			for (;;)
			{
				if (context.DataInterface.TryPeekMarker(out propertyTag))
				{
					if (propertyTag == PropertyTag.NewAttach)
					{
						yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferAttachmentList(this.message)));
					}
					else
					{
						if (!(propertyTag == PropertyTag.StartRecip))
						{
							break;
						}
						yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferRecipientList(this.message)));
					}
				}
				else
				{
					if (FastTransferPropList.MetaProperties.Contains(propertyTag))
					{
						goto Block_6;
					}
					yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.message.PropertyBag)));
				}
			}
			context.DataInterface.ReadMarker(this.EndMarker);
			this.message.Save();
			yield break;
			Block_6:
			throw new RopExecutionException(string.Format("Unexpected meta-marker found: {0}.", propertyTag), ErrorCode.FxUnexpectedMarker);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001544F File Offset: 0x0001364F
		protected override void InternalDispose()
		{
			this.message.Dispose();
			base.InternalDispose();
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00015462 File Offset: 0x00013662
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferMessage>(this);
		}

		// Token: 0x0400036A RID: 874
		private static readonly PropertyValue EmptyMidPropertyValue = new PropertyValue(PropertyTag.Mid, 0L);

		// Token: 0x0400036B RID: 875
		private readonly IMessage message;

		// Token: 0x0400036C RID: 876
		private readonly FastTransferMessage.MessageType messageType;

		// Token: 0x02000167 RID: 359
		public enum MessageType
		{
			// Token: 0x0400036E RID: 878
			Normal,
			// Token: 0x0400036F RID: 879
			Associated,
			// Token: 0x04000370 RID: 880
			Embedded
		}
	}
}
