using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200029E RID: 670
	internal sealed class RopEchoBinary : Rop
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0002BA15 File Offset: 0x00029C15
		internal override RopId RopId
		{
			get
			{
				return RopId.EchoBinary;
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002BA1C File Offset: 0x00029C1C
		internal static Rop CreateRop()
		{
			return new RopEchoBinary();
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002BA23 File Offset: 0x00029C23
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte[] inParameter)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.inParameter = inParameter;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002BA34 File Offset: 0x00029C34
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopEchoBinary.resultFactory;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002BA3B File Offset: 0x00029C3B
		internal sealed override void Execute(IConnectionInformation connectionInfo, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer)
		{
			this.result = ropDriver.RopHandler.EchoBinary(this.inParameter, RopEchoBinary.resultFactory);
			this.result.String8Encoding = CTSGlobals.AsciiEncoding;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002BA69 File Offset: 0x00029C69
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.inParameter = reader.ReadSizeAndByteArray();
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002BA80 File Offset: 0x00029C80
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytes(this.inParameter);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002BA96 File Offset: 0x00029C96
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulEchoBinaryResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002BAC4 File Offset: 0x00029CC4
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0400078F RID: 1935
		private const RopId RopType = RopId.EchoBinary;

		// Token: 0x04000790 RID: 1936
		private static EchoBinaryResultFactory resultFactory = new EchoBinaryResultFactory();

		// Token: 0x04000791 RID: 1937
		private byte[] inParameter;
	}
}
