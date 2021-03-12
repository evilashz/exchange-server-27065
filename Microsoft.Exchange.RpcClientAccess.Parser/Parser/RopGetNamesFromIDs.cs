using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002CB RID: 715
	internal sealed class RopGetNamesFromIDs : InputRop
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0002E603 File Offset: 0x0002C803
		internal override RopId RopId
		{
			get
			{
				return RopId.GetNamesFromIDs;
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0002E607 File Offset: 0x0002C807
		internal static Rop CreateRop()
		{
			return new RopGetNamesFromIDs();
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0002E60E File Offset: 0x0002C80E
		internal void SetInput(byte logonIndex, byte handleTableIndex, PropertyId[] propertyIds)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.propertyIds = propertyIds;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0002E61F File Offset: 0x0002C81F
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountedPropertyIds(this.propertyIds);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0002E635 File Offset: 0x0002C835
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetNamesFromIDsResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0002E663 File Offset: 0x0002C863
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetNamesFromIDs.resultFactory;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0002E66A File Offset: 0x0002C86A
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.propertyIds = reader.ReadSizeAndPropertyIdArray();
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0002E680 File Offset: 0x0002C880
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002E695 File Offset: 0x0002C895
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetNamesFromIDs(serverObject, this.propertyIds, RopGetNamesFromIDs.resultFactory);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0002E6AF File Offset: 0x0002C8AF
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			if (this.propertyIds != null)
			{
				stringBuilder.Append(" IDs=[");
				Util.AppendToString<PropertyId>(stringBuilder, this.propertyIds);
				stringBuilder.Append("]");
			}
		}

		// Token: 0x04000823 RID: 2083
		private const RopId RopType = RopId.GetNamesFromIDs;

		// Token: 0x04000824 RID: 2084
		private static GetNamesFromIDsResultFactory resultFactory = new GetNamesFromIDsResultFactory();

		// Token: 0x04000825 RID: 2085
		private PropertyId[] propertyIds;
	}
}
