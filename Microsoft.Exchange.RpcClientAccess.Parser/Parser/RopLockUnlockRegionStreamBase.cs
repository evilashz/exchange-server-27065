using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002EE RID: 750
	internal abstract class RopLockUnlockRegionStreamBase : InputRop
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x0003051C File Offset: 0x0002E71C
		internal void SetInput(byte logonIndex, byte handleTableIndex, ulong offset, ulong regionLength, LockTypeFlag lockType)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.offset = offset;
			this.regionLength = regionLength;
			this.lockType = lockType;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003053D File Offset: 0x0002E73D
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt64(this.offset);
			writer.WriteUInt64(this.regionLength);
			writer.WriteInt32((int)this.lockType);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003056B File Offset: 0x0002E76B
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00030599 File Offset: 0x0002E799
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.offset = reader.ReadUInt64();
			this.regionLength = reader.ReadUInt64();
			this.lockType = (LockTypeFlag)reader.ReadInt32();
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000305C7 File Offset: 0x0002E7C7
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x04000948 RID: 2376
		protected ulong offset;

		// Token: 0x04000949 RID: 2377
		protected ulong regionLength;

		// Token: 0x0400094A RID: 2378
		protected LockTypeFlag lockType;
	}
}
