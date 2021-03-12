using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C8 RID: 712
	internal sealed class RopGetIdsFromNames : InputRop
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0002E2FE File Offset: 0x0002C4FE
		internal override RopId RopId
		{
			get
			{
				return RopId.GetIdsFromNames;
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0002E302 File Offset: 0x0002C502
		internal static Rop CreateRop()
		{
			return new RopGetIdsFromNames();
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0002E309 File Offset: 0x0002C509
		internal void SetInput(byte logonIndex, byte handleTableIndex, GetIdsFromNamesFlags flags, NamedProperty[] namedProperties)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.flags = flags;
			this.namedProperties = namedProperties;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0002E322 File Offset: 0x0002C522
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.flags);
			writer.WriteCountedNamedProperties(this.namedProperties);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0002E344 File Offset: 0x0002C544
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetIdsFromNamesResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002E372 File Offset: 0x0002C572
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetIdsFromNames.resultFactory;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002E379 File Offset: 0x0002C579
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = (GetIdsFromNamesFlags)reader.ReadByte();
			this.namedProperties = reader.ReadSizeAndNamedPropertyArray();
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0002E39B File Offset: 0x0002C59B
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetIdsFromNames(serverObject, this.flags, this.namedProperties, RopGetIdsFromNames.resultFactory);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002E3D0 File Offset: 0x0002C5D0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" Names=[");
			Util.AppendToString<NamedProperty>(stringBuilder, this.namedProperties);
			stringBuilder.Append("]");
		}

		// Token: 0x04000819 RID: 2073
		private const RopId RopType = RopId.GetIdsFromNames;

		// Token: 0x0400081A RID: 2074
		private static GetIdsFromNamesResultFactory resultFactory = new GetIdsFromNamesResultFactory();

		// Token: 0x0400081B RID: 2075
		private GetIdsFromNamesFlags flags;

		// Token: 0x0400081C RID: 2076
		private NamedProperty[] namedProperties;
	}
}
