using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x020001A0 RID: 416
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ProgressInformation
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x0001CD85 File Offset: 0x0001AF85
		public ProgressInformation(ushort version, int normalMessageCount, int associatedMessageCount, ulong normalMessagesTotalSize, ulong associatedMessagesTotalSize)
		{
			this.version = version;
			this.associatedMessageCount = associatedMessageCount;
			this.normalMessageCount = normalMessageCount;
			this.normalMessagesTotalSize = normalMessagesTotalSize;
			this.associatedMessagesTotalSize = associatedMessagesTotalSize;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		public static ProgressInformation Parse(Reader reader)
		{
			ushort num = reader.ReadUInt16();
			reader.ReadUInt16();
			int num2 = reader.ReadInt32();
			ulong num3 = reader.ReadUInt64();
			int num4 = reader.ReadInt32();
			reader.ReadUInt32();
			ulong num5 = reader.ReadUInt64();
			return new ProgressInformation(num, num4, num2, num5, num3);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001CE00 File Offset: 0x0001B000
		public void Serialize(Writer writer)
		{
			writer.WriteUInt16(this.version);
			writer.WriteUInt16(0);
			writer.WriteInt32(this.associatedMessageCount);
			writer.WriteUInt64(this.associatedMessagesTotalSize);
			writer.WriteInt32(this.normalMessageCount);
			writer.WriteUInt32(0U);
			writer.WriteUInt64(this.normalMessagesTotalSize);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001CE58 File Offset: 0x0001B058
		public override string ToString()
		{
			return string.Format("ProgressInformation Version = {0}. NormalMessageCount = {1}. AssociatedMessageCount = {2}. NormalMessagesTotalSize {3}. AssociatedMessagesTotalSize = {4}.", new object[]
			{
				this.version,
				this.normalMessageCount,
				this.associatedMessageCount,
				this.normalMessagesTotalSize,
				this.associatedMessagesTotalSize
			});
		}

		// Token: 0x040003EB RID: 1003
		public const int SerializedSize = 32;

		// Token: 0x040003EC RID: 1004
		private ushort version;

		// Token: 0x040003ED RID: 1005
		private int associatedMessageCount;

		// Token: 0x040003EE RID: 1006
		private int normalMessageCount;

		// Token: 0x040003EF RID: 1007
		private ulong normalMessagesTotalSize;

		// Token: 0x040003F0 RID: 1008
		private ulong associatedMessagesTotalSize;
	}
}
