using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000330 RID: 816
	internal sealed class SuccessfulSeekRowResult : RopResult
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0003442A File Offset: 0x0003262A
		internal bool SoughtLessThanRequested
		{
			get
			{
				return this.soughtLessThanRequested;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x00034432 File Offset: 0x00032632
		internal int RowsSought
		{
			get
			{
				return this.rowsSought;
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0003443A File Offset: 0x0003263A
		internal SuccessfulSeekRowResult(bool soughtLessThanRequested, int rowsSought) : base(RopId.SeekRow, ErrorCode.None, null)
		{
			this.soughtLessThanRequested = soughtLessThanRequested;
			this.rowsSought = rowsSought;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00034454 File Offset: 0x00032654
		internal SuccessfulSeekRowResult(Reader reader) : base(reader)
		{
			this.soughtLessThanRequested = (reader.ReadByte() != 0);
			this.rowsSought = reader.ReadInt32();
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0003447B File Offset: 0x0003267B
		internal static SuccessfulSeekRowResult Parse(Reader reader)
		{
			return new SuccessfulSeekRowResult(reader);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00034483 File Offset: 0x00032683
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.soughtLessThanRequested, 1);
			writer.WriteInt32(this.rowsSought);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000344A5 File Offset: 0x000326A5
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" SoughtLess=").Append(this.soughtLessThanRequested);
			stringBuilder.Append(" RowsSought=").Append(this.rowsSought);
		}

		// Token: 0x04000A5D RID: 2653
		private readonly bool soughtLessThanRequested;

		// Token: 0x04000A5E RID: 2654
		private readonly int rowsSought;
	}
}
