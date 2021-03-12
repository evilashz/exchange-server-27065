using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200027A RID: 634
	internal sealed class SuccessfulProgressResult : RopResult
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x00029862 File Offset: 0x00027A62
		internal SuccessfulProgressResult(byte logonId, uint completedTaskCount, uint totalTaskCount) : base(RopId.Progress, ErrorCode.None, null)
		{
			this.logonId = logonId;
			this.completedTaskCount = completedTaskCount;
			this.totalTaskCount = totalTaskCount;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00029883 File Offset: 0x00027A83
		internal SuccessfulProgressResult(Reader reader) : base(reader)
		{
			this.logonId = reader.ReadByte();
			this.completedTaskCount = reader.ReadUInt32();
			this.totalTaskCount = reader.ReadUInt32();
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000298B0 File Offset: 0x00027AB0
		internal byte LogonId
		{
			get
			{
				return this.logonId;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x000298B8 File Offset: 0x00027AB8
		internal uint CompletedTaskCount
		{
			get
			{
				return this.completedTaskCount;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x000298C0 File Offset: 0x00027AC0
		internal uint TotalTaskCount
		{
			get
			{
				return this.totalTaskCount;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000298C8 File Offset: 0x00027AC8
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte(this.logonId);
			writer.WriteUInt32(this.completedTaskCount);
			writer.WriteUInt32(this.totalTaskCount);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000298F8 File Offset: 0x00027AF8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" LogonId=").Append(this.logonId);
			stringBuilder.Append(" CompletedTaskCount=").Append(this.completedTaskCount);
			stringBuilder.Append(" TotalTaskCount=").Append(this.totalTaskCount);
		}

		// Token: 0x04000723 RID: 1827
		private readonly byte logonId;

		// Token: 0x04000724 RID: 1828
		private readonly uint completedTaskCount;

		// Token: 0x04000725 RID: 1829
		private readonly uint totalTaskCount;
	}
}
