using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000341 RID: 833
	internal sealed class RopSetReadFlags : InputRop
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000352FE File Offset: 0x000334FE
		internal override RopId RopId
		{
			get
			{
				return RopId.SetReadFlags;
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00035302 File Offset: 0x00033502
		internal static Rop CreateRop()
		{
			return new RopSetReadFlags();
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0003530C File Offset: 0x0003350C
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			SetReadFlagsResultFactory resultFactory = new SetReadFlagsResultFactory(base.LogonIndex);
			this.result = ropHandler.SetReadFlags(serverObject, this.reportProgress, this.flags, this.messageIds, resultFactory);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00035345 File Offset: 0x00033545
		internal void SetInput(byte logonIndex, byte handleTableIndex, bool reportProgress, SetReadFlagFlags flags, StoreId[] messageIds)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.reportProgress = reportProgress;
			this.flags = flags;
			this.messageIds = messageIds;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00035366 File Offset: 0x00033566
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.reportProgress);
			writer.WriteByte((byte)this.flags);
			writer.WriteCountedStoreIds(this.messageIds);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00035394 File Offset: 0x00033594
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = SetReadFlagsResultFactory.Parse(reader);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000353AA File Offset: 0x000335AA
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new SetReadFlagsResultFactory(base.LogonIndex);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000353B7 File Offset: 0x000335B7
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.reportProgress = reader.ReadBool();
			this.flags = (SetReadFlagFlags)reader.ReadByte();
			this.messageIds = reader.ReadSizeAndStoreIdArray();
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000353E6 File Offset: 0x000335E6
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x000353FC File Offset: 0x000335FC
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Progress=").Append(this.reportProgress);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" MIDs=[");
			Util.AppendToString<StoreId>(stringBuilder, this.messageIds);
			stringBuilder.Append("]");
		}

		// Token: 0x04000A9B RID: 2715
		private const RopId RopType = RopId.SetReadFlags;

		// Token: 0x04000A9C RID: 2716
		private bool reportProgress;

		// Token: 0x04000A9D RID: 2717
		private SetReadFlagFlags flags;

		// Token: 0x04000A9E RID: 2718
		private StoreId[] messageIds;
	}
}
