using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000305 RID: 773
	internal sealed class RopModifyRules : InputRop
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00031919 File Offset: 0x0002FB19
		internal override RopId RopId
		{
			get
			{
				return RopId.ModifyRules;
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003191D File Offset: 0x0002FB1D
		internal static Rop CreateRop()
		{
			return new RopModifyRules();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00031924 File Offset: 0x0002FB24
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" ModifyRulesFlags=").Append(this.modifyRulesFlags);
			stringBuilder.Append(" RulesData={");
			Util.AppendToString<ModifyTableRow>(stringBuilder, this.rulesData);
			stringBuilder.Append("}");
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00031978 File Offset: 0x0002FB78
		internal void SetInput(byte logonIndex, byte handleTableIndex, ModifyRulesFlags modifyRulesFlags, ModifyTableRow[] rulesData)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.modifyRulesFlags = modifyRulesFlags;
			this.rulesData = rulesData;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00031991 File Offset: 0x0002FB91
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.modifyRulesFlags);
			writer.WriteSizedModifyTableRows(this.rulesData, string8Encoding);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x000319B4 File Offset: 0x0002FBB4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000319E2 File Offset: 0x0002FBE2
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopModifyRules.resultFactory;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000319E9 File Offset: 0x0002FBE9
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.modifyRulesFlags = (ModifyRulesFlags)reader.ReadByte();
			this.rulesData = reader.ReadSizeAndModifyTableRowArray();
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00031A0B File Offset: 0x0002FC0B
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00031A20 File Offset: 0x0002FC20
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			for (int i = 0; i < this.rulesData.Length; i++)
			{
				this.rulesData[i].ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00031A59 File Offset: 0x0002FC59
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ModifyRules(serverObject, this.modifyRulesFlags, this.rulesData, RopModifyRules.resultFactory);
		}

		// Token: 0x040009C9 RID: 2505
		private const RopId RopType = RopId.ModifyRules;

		// Token: 0x040009CA RID: 2506
		private static ModifyRulesResultFactory resultFactory = new ModifyRulesResultFactory();

		// Token: 0x040009CB RID: 2507
		private ModifyRulesFlags modifyRulesFlags;

		// Token: 0x040009CC RID: 2508
		private ModifyTableRow[] rulesData;
	}
}
