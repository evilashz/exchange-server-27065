using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200023F RID: 575
	internal sealed class FastTransferDestinationPutBufferResult : RopResult
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002782B File Offset: 0x00025A2B
		internal ushort Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00027833 File Offset: 0x00025A33
		internal ushort Steps
		{
			get
			{
				return this.steps;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002783B File Offset: 0x00025A3B
		internal bool IsMoveUser
		{
			get
			{
				return this.isMoveUser;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00027843 File Offset: 0x00025A43
		internal ushort UsedBufferSize
		{
			get
			{
				return this.usedBufferSize;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002784B File Offset: 0x00025A4B
		internal FastTransferDestinationPutBufferResult(ErrorCode errorCode, ushort progress, ushort steps, bool isMoveUser, ushort usedBufferSize) : base(RopId.FastTransferDestinationPutBuffer, errorCode, null)
		{
			this.progress = progress;
			this.steps = steps;
			this.isMoveUser = isMoveUser;
			this.usedBufferSize = usedBufferSize;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00027878 File Offset: 0x00025A78
		internal FastTransferDestinationPutBufferResult(Reader reader) : base(reader)
		{
			reader.Position += 2L;
			this.progress = reader.ReadUInt16();
			this.steps = reader.ReadUInt16();
			this.isMoveUser = reader.ReadBool();
			this.usedBufferSize = reader.ReadUInt16();
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000278CB File Offset: 0x00025ACB
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(0);
			writer.WriteUInt16(this.progress);
			writer.WriteUInt16(this.steps);
			writer.WriteBool(this.isMoveUser);
			writer.WriteUInt16(this.usedBufferSize);
		}

		// Token: 0x040006E0 RID: 1760
		private readonly ushort progress;

		// Token: 0x040006E1 RID: 1761
		private readonly ushort steps;

		// Token: 0x040006E2 RID: 1762
		private readonly bool isMoveUser;

		// Token: 0x040006E3 RID: 1763
		private readonly ushort usedBufferSize;
	}
}
