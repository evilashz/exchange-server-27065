using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000317 RID: 791
	internal sealed class RopQueryNamedProperties : InputRop
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00032A2F File Offset: 0x00030C2F
		internal override RopId RopId
		{
			get
			{
				return RopId.QueryNamedProperties;
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00032A33 File Offset: 0x00030C33
		internal static Rop CreateRop()
		{
			return new RopQueryNamedProperties();
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00032A3A File Offset: 0x00030C3A
		internal void SetInput(byte logonIndex, byte handleTableIndex, QueryNamedPropertyFlags queryFlags, Guid? propertyGuid)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.queryFlags = queryFlags;
			this.propertyGuid = propertyGuid;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00032A54 File Offset: 0x00030C54
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.queryFlags);
			writer.WriteBool(this.propertyGuid != null, 1);
			if (this.propertyGuid != null)
			{
				writer.WriteGuid(this.propertyGuid.Value);
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00032AA5 File Offset: 0x00030CA5
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulQueryNamedPropertiesResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00032AD3 File Offset: 0x00030CD3
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopQueryNamedProperties.resultFactory;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00032ADA File Offset: 0x00030CDA
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.queryFlags = (QueryNamedPropertyFlags)reader.ReadByte();
			if (reader.ReadBool())
			{
				this.propertyGuid = new Guid?(reader.ReadGuid());
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00032B09 File Offset: 0x00030D09
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00032B1E File Offset: 0x00030D1E
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.QueryNamedProperties(serverObject, this.queryFlags, this.propertyGuid, RopQueryNamedProperties.resultFactory);
		}

		// Token: 0x040009FC RID: 2556
		private const RopId RopType = RopId.QueryNamedProperties;

		// Token: 0x040009FD RID: 2557
		private static QueryNamedPropertiesResultFactory resultFactory = new QueryNamedPropertiesResultFactory();

		// Token: 0x040009FE RID: 2558
		private QueryNamedPropertyFlags queryFlags;

		// Token: 0x040009FF RID: 2559
		private Guid? propertyGuid;
	}
}
