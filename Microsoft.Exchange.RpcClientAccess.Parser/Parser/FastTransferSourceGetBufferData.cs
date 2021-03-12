using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000245 RID: 581
	internal sealed class FastTransferSourceGetBufferData
	{
		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002799F File Offset: 0x00025B9F
		internal FastTransferSourceGetBufferData(FastTransferState state, uint progress, uint steps, bool isMoveUser, ArraySegment<byte> data, bool isExtendedRop)
		{
			this.State = state;
			this.Progress = progress;
			this.Steps = steps;
			this.IsMoveUser = isMoveUser;
			this.Data = data;
			this.isExtendedRop = isExtendedRop;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000279DF File Offset: 0x00025BDF
		internal FastTransferSourceGetBufferData(uint backOffTime, bool isExtendedRop)
		{
			this.isServerBusy = true;
			this.isExtendedRop = isExtendedRop;
			this.BackOffTime = backOffTime;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00027A07 File Offset: 0x00025C07
		internal FastTransferSourceGetBufferData(FastTransferState state, bool isExtendedRop)
		{
			this.State = state;
			this.isExtendedRop = isExtendedRop;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00027A28 File Offset: 0x00025C28
		internal FastTransferSourceGetBufferData(Reader reader, bool isExtendedRop, bool isServerBusy)
		{
			this.isExtendedRop = isExtendedRop;
			this.isServerBusy = isServerBusy;
			this.State = (FastTransferState)reader.ReadUInt16();
			if (this.isExtendedRop)
			{
				this.Progress = reader.ReadUInt32();
				this.Steps = reader.ReadUInt32();
			}
			else
			{
				this.Progress = (uint)reader.ReadUInt16();
				this.Steps = (uint)reader.ReadUInt16();
			}
			this.IsMoveUser = reader.ReadBool();
			ushort num = reader.ReadUInt16();
			if (this.isServerBusy)
			{
				this.BackOffTime = reader.ReadUInt32();
			}
			if (num > 0)
			{
				this.Data = reader.ReadArraySegment((uint)num);
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00027AD4 File Offset: 0x00025CD4
		internal void Serialize(Writer writer)
		{
			writer.WriteUInt16((ushort)this.State);
			if (this.isExtendedRop)
			{
				writer.WriteUInt32(this.Progress);
				writer.WriteUInt32(this.Steps);
			}
			else
			{
				writer.WriteUInt16((ushort)this.Progress);
				writer.WriteUInt16((ushort)this.Steps);
			}
			writer.WriteBool(this.IsMoveUser);
			writer.WriteUInt16((ushort)this.Data.Count);
			if (this.isServerBusy)
			{
				writer.WriteUInt32(this.BackOffTime);
			}
			if (this.Data.Count > 0)
			{
				writer.SkipArraySegment(this.Data);
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00027B7C File Offset: 0x00025D7C
		public override string ToString()
		{
			return string.Format("FastTransferStream({0}, {1}b)", this.State, this.Data.Count);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00027BB4 File Offset: 0x00025DB4
		internal void AppendToString(StringBuilder stringBuilder)
		{
			stringBuilder.Append(" state=").Append(this.State);
			stringBuilder.Append(" progress=").Append(this.Progress);
			stringBuilder.Append(" steps=").Append(this.Steps);
			stringBuilder.Append(" moveUser=").Append(this.IsMoveUser);
			stringBuilder.Append(" serverBusy=").Append(this.isServerBusy);
			if (this.isServerBusy)
			{
				stringBuilder.Append(" backoffTime=").Append(this.BackOffTime);
			}
			stringBuilder.Append(" data=[");
			Util.AppendToString(stringBuilder, this.Data.Array, this.Data.Offset, this.Data.Count);
			stringBuilder.Append("]");
		}

		// Token: 0x040006E4 RID: 1764
		internal readonly FastTransferState State;

		// Token: 0x040006E5 RID: 1765
		internal readonly uint Progress;

		// Token: 0x040006E6 RID: 1766
		internal readonly uint Steps;

		// Token: 0x040006E7 RID: 1767
		internal readonly bool IsMoveUser;

		// Token: 0x040006E8 RID: 1768
		internal readonly uint BackOffTime;

		// Token: 0x040006E9 RID: 1769
		internal readonly ArraySegment<byte> Data = Array<byte>.EmptySegment;

		// Token: 0x040006EA RID: 1770
		private bool isExtendedRop;

		// Token: 0x040006EB RID: 1771
		private bool isServerBusy;
	}
}
