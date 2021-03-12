using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002A2 RID: 674
	internal sealed class RopEchoString : Rop
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0002BC93 File Offset: 0x00029E93
		internal override RopId RopId
		{
			get
			{
				return RopId.EchoString;
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0002BC9A File Offset: 0x00029E9A
		internal static Rop CreateRop()
		{
			return new RopEchoString();
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002BCA1 File Offset: 0x00029EA1
		internal void SetInput(byte logonIndex, byte handleTableIndex, string inParameter)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.inParameter = inParameter;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002BCB2 File Offset: 0x00029EB2
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopEchoString.resultFactory;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002BCB9 File Offset: 0x00029EB9
		internal sealed override void Execute(IConnectionInformation connectionInfo, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer)
		{
			this.result = ropDriver.RopHandler.EchoString(this.inParameter, RopEchoString.resultFactory);
			this.result.String8Encoding = CTSGlobals.AsciiEncoding;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002BCE7 File Offset: 0x00029EE7
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.inParameter = reader.ReadAsciiString(StringFlags.Sized16);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002BCFF File Offset: 0x00029EFF
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteAsciiString(this.inParameter, StringFlags.Sized16);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002BD16 File Offset: 0x00029F16
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulEchoStringResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002BD44 File Offset: 0x00029F44
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x04000799 RID: 1945
		private const RopId RopType = RopId.EchoString;

		// Token: 0x0400079A RID: 1946
		private static EchoStringResultFactory resultFactory = new EchoStringResultFactory();

		// Token: 0x0400079B RID: 1947
		private string inParameter;
	}
}
