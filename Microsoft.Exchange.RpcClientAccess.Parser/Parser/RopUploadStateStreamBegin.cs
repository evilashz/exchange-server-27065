using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200035E RID: 862
	internal sealed class RopUploadStateStreamBegin : InputRop
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0003697C File Offset: 0x00034B7C
		internal override RopId RopId
		{
			get
			{
				return RopId.UploadStateStreamBegin;
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00036980 File Offset: 0x00034B80
		internal static Rop CreateRop()
		{
			return new RopUploadStateStreamBegin();
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00036987 File Offset: 0x00034B87
		internal void SetInput(byte logonIndex, byte handleTableIndex, PropertyTag propertyTag, uint size)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.propertyTag = propertyTag;
			this.size = size;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x000369A0 File Offset: 0x00034BA0
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WritePropertyTag(this.propertyTag);
			writer.WriteUInt32(this.size);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000369C2 File Offset: 0x00034BC2
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000369F0 File Offset: 0x00034BF0
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopUploadStateStreamBegin.resultFactory;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000369F7 File Offset: 0x00034BF7
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.propertyTag = reader.ReadPropertyTag();
			this.size = reader.ReadUInt32();
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00036A19 File Offset: 0x00034C19
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00036A2E File Offset: 0x00034C2E
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.UploadStateStreamBegin(serverObject, this.propertyTag, this.size, RopUploadStateStreamBegin.resultFactory);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00036A50 File Offset: 0x00034C50
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" propertyTag=").Append(this.propertyTag.ToString());
			stringBuilder.Append(" size=").Append(this.size);
		}

		// Token: 0x04000B0F RID: 2831
		private const RopId RopType = RopId.UploadStateStreamBegin;

		// Token: 0x04000B10 RID: 2832
		private static UploadStateStreamBeginResultFactory resultFactory = new UploadStateStreamBeginResultFactory();

		// Token: 0x04000B11 RID: 2833
		private PropertyTag propertyTag;

		// Token: 0x04000B12 RID: 2834
		private uint size;
	}
}
