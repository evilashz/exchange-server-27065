using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal class ItemNotFoundException : MessageDepotPermanentException
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002291 File Offset: 0x00000491
		public ItemNotFoundException(TransportMessageId messageId, LocalizedString errorMessage, Exception innerException = null) : base(errorMessage, innerException)
		{
			if (messageId == null)
			{
				throw new ArgumentNullException("messageId");
			}
			this.messageId = messageId;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000022B6 File Offset: 0x000004B6
		protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.messageId = (TransportMessageId)info.GetValue("messageId", typeof(TransportMessageId));
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000022E0 File Offset: 0x000004E0
		public TransportMessageId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000022E8 File Offset: 0x000004E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("messageId", this.messageId);
		}

		// Token: 0x0400001D RID: 29
		private const string MessageIdSerializedName = "messageId";

		// Token: 0x0400001E RID: 30
		private readonly TransportMessageId messageId;
	}
}
