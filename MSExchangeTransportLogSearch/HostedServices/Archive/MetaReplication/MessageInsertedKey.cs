using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000056 RID: 86
	public sealed class MessageInsertedKey : MetadataKey<MessageInsertedKey>, IEquatable<MessageInsertedKey>
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000BC96 File Offset: 0x00009E96
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000BC9E File Offset: 0x00009E9E
		public byte[] MessageId { get; set; }

		// Token: 0x060001D0 RID: 464 RVA: 0x0000BCA7 File Offset: 0x00009EA7
		public override bool Equals(MessageInsertedKey other)
		{
			return other != null && ByteArrayHelper.Equal(other.MessageId, this.MessageId);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000BCBF File Offset: 0x00009EBF
		protected override int ComputeHashCode()
		{
			return ByteArrayHelper.GetHash(this.MessageId);
		}
	}
}
