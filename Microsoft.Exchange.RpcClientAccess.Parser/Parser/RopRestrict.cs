using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000329 RID: 809
	internal sealed class RopRestrict : InputRop
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00033F4A File Offset: 0x0003214A
		internal override RopId RopId
		{
			get
			{
				return RopId.Restrict;
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00033F4E File Offset: 0x0003214E
		internal static Rop CreateRop()
		{
			return new RopRestrict();
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00033F55 File Offset: 0x00032155
		internal void SetInput(byte logonIndex, byte handleTableIndex, RestrictFlags flags, Restriction restriction)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.flags = flags;
			this.restriction = restriction;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00033F6E File Offset: 0x0003216E
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.flags);
			writer.WriteSizedRestriction(this.restriction, string8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00033F92 File Offset: 0x00032192
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulRestrictResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00033FC0 File Offset: 0x000321C0
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopRestrict.resultFactory;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00033FC7 File Offset: 0x000321C7
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = (RestrictFlags)reader.ReadByte();
			this.restriction = reader.ReadSizeAndRestriction(WireFormatStyle.Rop);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00033FEA File Offset: 0x000321EA
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00033FFF File Offset: 0x000321FF
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			if (this.restriction != null)
			{
				this.restriction.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0003401C File Offset: 0x0003221C
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.Restrict(serverObject, this.flags, this.restriction, RopRestrict.resultFactory);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0003403C File Offset: 0x0003223C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" Restriction=[");
			if (this.restriction != null)
			{
				this.restriction.AppendToString(stringBuilder);
			}
			stringBuilder.Append("]");
		}

		// Token: 0x04000A49 RID: 2633
		private const RopId RopType = RopId.Restrict;

		// Token: 0x04000A4A RID: 2634
		private static RestrictResultFactory resultFactory = new RestrictResultFactory();

		// Token: 0x04000A4B RID: 2635
		private RestrictFlags flags;

		// Token: 0x04000A4C RID: 2636
		private Restriction restriction;
	}
}
