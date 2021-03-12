using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal class DuplicateItemException : MessageDepotPermanentException
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		public DuplicateItemException(TransportMessageId messageId, MessageDepotItemState messageState, LocalizedString errorMessage, Exception innerException = null) : base(errorMessage, innerException)
		{
			if (messageId == null)
			{
				throw new ArgumentNullException("messageId");
			}
			this.messageId = messageId;
			this.messageState = messageState;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002215 File Offset: 0x00000415
		protected DuplicateItemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.messageId = (TransportMessageId)info.GetValue("messageId", typeof(TransportMessageId));
			this.messageState = (MessageDepotItemState)info.GetInt32("messageState");
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002250 File Offset: 0x00000450
		public TransportMessageId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002258 File Offset: 0x00000458
		public MessageDepotItemState MessageState
		{
			get
			{
				return this.messageState;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002260 File Offset: 0x00000460
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("messageId", this.messageId);
			info.AddValue("messageState", this.messageState);
		}

		// Token: 0x04000004 RID: 4
		private const string MessageIdSerializedName = "messageId";

		// Token: 0x04000005 RID: 5
		private const string MessageStateSerializedName = "messageState";

		// Token: 0x04000006 RID: 6
		private readonly TransportMessageId messageId;

		// Token: 0x04000007 RID: 7
		private readonly MessageDepotItemState messageState;
	}
}
