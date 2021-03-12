using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D1 RID: 721
	internal sealed class RopGetPropertiesAll : InputRop
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0002EBF8 File Offset: 0x0002CDF8
		internal override RopId RopId
		{
			get
			{
				return RopId.GetPropertiesAll;
			}
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0002EBFB File Offset: 0x0002CDFB
		internal static Rop CreateRop()
		{
			return new RopGetPropertiesAll();
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002EC02 File Offset: 0x0002CE02
		internal void SetInput(byte logonIndex, byte handleTableIndex, ushort streamLimit, GetPropertiesFlags flags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.streamLimit = streamLimit;
			this.flags = flags;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0002EC1B File Offset: 0x0002CE1B
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.streamLimit);
			writer.WriteUInt16((this.flags == GetPropertiesFlags.None) ? 0 : 1);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0002EC5C File Offset: 0x0002CE5C
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => SuccessfulGetPropertiesAllResult.Parse(readerParameter, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0002ECA7 File Offset: 0x0002CEA7
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetPropertiesAllResultFactory(outputBuffer.Count, connection.String8Encoding);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0002ECBB File Offset: 0x0002CEBB
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.streamLimit = reader.ReadUInt16();
			this.flags = ((reader.ReadUInt16() == 0) ? GetPropertiesFlags.None : ((GetPropertiesFlags)int.MinValue));
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0002ECE9 File Offset: 0x0002CEE9
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002ED00 File Offset: 0x0002CF00
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetPropertiesAllResultFactory resultFactory = new GetPropertiesAllResultFactory(outputBuffer.Count, serverObject.String8Encoding);
			this.result = ropHandler.GetPropertiesAll(serverObject, this.streamLimit, this.flags, resultFactory);
		}

		// Token: 0x04000835 RID: 2101
		private const RopId RopType = RopId.GetPropertiesAll;

		// Token: 0x04000836 RID: 2102
		private ushort streamLimit;

		// Token: 0x04000837 RID: 2103
		private GetPropertiesFlags flags;
	}
}
