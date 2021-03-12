using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200023E RID: 574
	internal sealed class FastTransferDestinationPutBufferExtendedResult : RopResult
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002774B File Offset: 0x0002594B
		internal uint Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00027753 File Offset: 0x00025953
		internal uint Steps
		{
			get
			{
				return this.steps;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002775B File Offset: 0x0002595B
		internal bool IsMoveUser
		{
			get
			{
				return this.isMoveUser;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00027763 File Offset: 0x00025963
		internal ushort UsedBufferSize
		{
			get
			{
				return this.usedBufferSize;
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002776B File Offset: 0x0002596B
		internal FastTransferDestinationPutBufferExtendedResult(ErrorCode errorCode, uint progress, uint steps, bool isMoveUser, ushort usedBufferSize) : base(RopId.FastTransferDestinationPutBufferExtended, errorCode, null)
		{
			this.progress = progress;
			this.steps = steps;
			this.isMoveUser = isMoveUser;
			this.usedBufferSize = usedBufferSize;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00027798 File Offset: 0x00025998
		internal FastTransferDestinationPutBufferExtendedResult(Reader reader) : base(reader)
		{
			reader.Position += 2L;
			this.progress = reader.ReadUInt32();
			this.steps = reader.ReadUInt32();
			this.isMoveUser = reader.ReadBool();
			this.usedBufferSize = reader.ReadUInt16();
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000277EB File Offset: 0x000259EB
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(0);
			writer.WriteUInt32(this.progress);
			writer.WriteUInt32(this.steps);
			writer.WriteBool(this.isMoveUser);
			writer.WriteUInt16(this.usedBufferSize);
		}

		// Token: 0x040006DC RID: 1756
		private readonly uint progress;

		// Token: 0x040006DD RID: 1757
		private readonly uint steps;

		// Token: 0x040006DE RID: 1758
		private readonly bool isMoveUser;

		// Token: 0x040006DF RID: 1759
		private readonly ushort usedBufferSize;
	}
}
