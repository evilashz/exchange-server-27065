using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000366 RID: 870
	internal sealed class SuccessfulSeekRowBookmarkResult : RopResult
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00037455 File Offset: 0x00035655
		internal bool PositionChanged
		{
			get
			{
				return this.positionChanged;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0003745D File Offset: 0x0003565D
		internal bool SoughtLessThanRequested
		{
			get
			{
				return this.soughtLessThanRequested;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00037465 File Offset: 0x00035665
		internal int RowsSought
		{
			get
			{
				return this.rowsSought;
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0003746D File Offset: 0x0003566D
		internal SuccessfulSeekRowBookmarkResult(bool positionChanged, bool soughtLessThanRequested, int rowsSought) : base(RopId.SeekRowBookmark, ErrorCode.None, null)
		{
			this.positionChanged = positionChanged;
			this.soughtLessThanRequested = soughtLessThanRequested;
			this.rowsSought = rowsSought;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0003748E File Offset: 0x0003568E
		internal SuccessfulSeekRowBookmarkResult(Reader reader) : base(reader)
		{
			this.positionChanged = reader.ReadBool();
			this.soughtLessThanRequested = reader.ReadBool();
			this.rowsSought = reader.ReadInt32();
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000374BB File Offset: 0x000356BB
		public override string ToString()
		{
			return string.Format("SuccessfulSeekRowBookmarkResult: [PositionChanged: {0}] [SoughtLess: {1}] [Rows: {2}]", this.positionChanged, this.soughtLessThanRequested, this.rowsSought);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000374E8 File Offset: 0x000356E8
		internal static SuccessfulSeekRowBookmarkResult Parse(Reader reader)
		{
			return new SuccessfulSeekRowBookmarkResult(reader);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000374F0 File Offset: 0x000356F0
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.positionChanged, 1);
			writer.WriteBool(this.soughtLessThanRequested, 1);
			writer.WriteInt32(this.rowsSought);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00037520 File Offset: 0x00035720
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" PositionChanged=").Append(this.positionChanged);
			stringBuilder.Append(" SoughtLessThanRequested=").Append(this.soughtLessThanRequested);
			stringBuilder.Append(" RowsSought=").Append(this.rowsSought);
		}

		// Token: 0x04000B2A RID: 2858
		private readonly bool positionChanged;

		// Token: 0x04000B2B RID: 2859
		private readonly bool soughtLessThanRequested;

		// Token: 0x04000B2C RID: 2860
		private readonly int rowsSought;
	}
}
