using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	internal sealed class TransportMessageId : IEquatable<TransportMessageId>
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000025DB File Offset: 0x000007DB
		public TransportMessageId(string messageId)
		{
			if (string.IsNullOrEmpty(messageId))
			{
				throw new ArgumentNullException("messageId");
			}
			this.messageId = messageId;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000025FD File Offset: 0x000007FD
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002605 File Offset: 0x00000805
		public static bool operator ==(TransportMessageId obj1, TransportMessageId obj2)
		{
			return object.ReferenceEquals(obj1, obj2) || (!object.ReferenceEquals(obj1, null) && !object.ReferenceEquals(obj2, null) && string.Equals(obj1.MessageId, obj2.MessageId));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002637 File Offset: 0x00000837
		public static bool operator !=(TransportMessageId obj1, TransportMessageId obj2)
		{
			return !(obj1 == obj2);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002643 File Offset: 0x00000843
		public bool Equals(TransportMessageId other)
		{
			return this == other;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000264C File Offset: 0x0000084C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransportMessageId);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000265A File Offset: 0x0000085A
		public override string ToString()
		{
			return this.messageId;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002662 File Offset: 0x00000862
		public override int GetHashCode()
		{
			return this.messageId.GetHashCode();
		}

		// Token: 0x04000031 RID: 49
		private readonly string messageId;
	}
}
