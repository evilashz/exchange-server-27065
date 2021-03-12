using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000340 RID: 832
	internal sealed class RopSetReadFlag : InputRop
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x0003512A File Offset: 0x0003332A
		internal override RopId RopId
		{
			get
			{
				return RopId.SetReadFlag;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0003512E File Offset: 0x0003332E
		internal bool IsPublicLogon
		{
			get
			{
				return this.isPublicLogon;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00035136 File Offset: 0x00033336
		internal override byte InputHandleTableIndex
		{
			get
			{
				return this.realHandleTableIndex;
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0003513E File Offset: 0x0003333E
		internal static Rop CreateRop()
		{
			return new RopSetReadFlag();
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00035148 File Offset: 0x00033348
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			SetReadFlagResultFactory resultFactory = new SetReadFlagResultFactory(base.LogonIndex, this.longTermId, this.IsPublicLogon);
			this.result = ropHandler.SetReadFlag(serverObject, this.flags, resultFactory);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00035181 File Offset: 0x00033381
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte realHandleTableIndex, SetReadFlagFlags flags, StoreLongTermId longTermId, bool isPublicLogon)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.realHandleTableIndex = realHandleTableIndex;
			this.flags = flags;
			this.longTermId = longTermId;
			this.isPublicLogon = isPublicLogon;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x000351AA File Offset: 0x000333AA
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte(this.realHandleTableIndex);
			writer.WriteByte((byte)this.flags);
			if (this.isPublicLogon)
			{
				this.longTermId.Serialize(writer);
			}
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x000351E0 File Offset: 0x000333E0
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSetReadFlagResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0003520E File Offset: 0x0003340E
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new SetReadFlagResultFactory(base.LogonIndex, this.longTermId, this.IsPublicLogon);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00035228 File Offset: 0x00033428
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.isPublicLogon = logonTracker.ParseIsPublicLogon(base.LogonIndex);
			this.realHandleTableIndex = reader.ReadByte();
			this.flags = (SetReadFlagFlags)reader.ReadByte();
			if (this.isPublicLogon)
			{
				this.longTermId = StoreLongTermId.Parse(reader);
				return;
			}
			this.longTermId = StoreLongTermId.Null;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00035288 File Offset: 0x00033488
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000352A0 File Offset: 0x000334A0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" LTID=[").Append(this.longTermId).Append("]");
		}

		// Token: 0x04000A96 RID: 2710
		private const RopId RopType = RopId.SetReadFlag;

		// Token: 0x04000A97 RID: 2711
		private byte realHandleTableIndex;

		// Token: 0x04000A98 RID: 2712
		private SetReadFlagFlags flags;

		// Token: 0x04000A99 RID: 2713
		private StoreLongTermId longTermId;

		// Token: 0x04000A9A RID: 2714
		private bool isPublicLogon;
	}
}
