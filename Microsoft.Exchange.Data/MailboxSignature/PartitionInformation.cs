using System;
using Microsoft.Exchange.Data.Serialization;

namespace Microsoft.Exchange.Data.MailboxSignature
{
	// Token: 0x02000238 RID: 568
	internal class PartitionInformation
	{
		// Token: 0x06001392 RID: 5010 RVA: 0x0003BE75 File Offset: 0x0003A075
		public PartitionInformation(Guid partitionGuid, PartitionInformation.ControlFlags flags)
		{
			this.partitionGuid = partitionGuid;
			this.flags = flags;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0003BE8B File Offset: 0x0003A08B
		public Guid PartitionGuid
		{
			get
			{
				return this.partitionGuid;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0003BE93 File Offset: 0x0003A093
		public PartitionInformation.ControlFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0003BE9C File Offset: 0x0003A09C
		public static PartitionInformation Parse(MailboxSignatureSectionMetadata metadata, byte[] buffer, ref int offset)
		{
			if (metadata.Length != 20)
			{
				throw new ArgumentException("Invalide PartitionInformation section");
			}
			Guid guid = Serialization.DeserializeGuid(buffer, ref offset);
			PartitionInformation.ControlFlags controlFlags = (PartitionInformation.ControlFlags)Serialization.DeserializeUInt32(buffer, ref offset);
			return new PartitionInformation(guid, controlFlags);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0003BED5 File Offset: 0x0003A0D5
		public int Serialize(byte[] buffer, int offset)
		{
			if (buffer != null)
			{
				Serialization.SerializeGuid(buffer, ref offset, this.partitionGuid);
				Serialization.SerializeUInt32(buffer, ref offset, (uint)this.flags);
			}
			return 20;
		}

		// Token: 0x04000B77 RID: 2935
		private const int SerializedSize = 20;

		// Token: 0x04000B78 RID: 2936
		public const short RequiredVersion = 1;

		// Token: 0x04000B79 RID: 2937
		private readonly Guid partitionGuid;

		// Token: 0x04000B7A RID: 2938
		private readonly PartitionInformation.ControlFlags flags;

		// Token: 0x02000239 RID: 569
		[Flags]
		public enum ControlFlags
		{
			// Token: 0x04000B7C RID: 2940
			None = 0,
			// Token: 0x04000B7D RID: 2941
			CreateNewPartition = 1
		}
	}
}
