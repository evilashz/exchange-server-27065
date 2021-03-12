using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002A0 RID: 672
	internal sealed class RopEchoInt : Rop
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0002BB54 File Offset: 0x00029D54
		internal override RopId RopId
		{
			get
			{
				return RopId.EchoInt;
			}
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002BB5B File Offset: 0x00029D5B
		internal static Rop CreateRop()
		{
			return new RopEchoInt();
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002BB62 File Offset: 0x00029D62
		internal void SetInput(byte logonIndex, byte handleTableIndex, int inParameter)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.inParameter = inParameter;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002BB73 File Offset: 0x00029D73
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopEchoInt.resultFactory;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002BB7A File Offset: 0x00029D7A
		internal sealed override void Execute(IConnectionInformation connectionInfo, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer)
		{
			this.result = ropDriver.RopHandler.EchoInt(this.inParameter, RopEchoInt.resultFactory);
			this.result.String8Encoding = CTSGlobals.AsciiEncoding;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002BBA8 File Offset: 0x00029DA8
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.inParameter = reader.ReadInt32();
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002BBBF File Offset: 0x00029DBF
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteInt32(this.inParameter);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0002BBD5 File Offset: 0x00029DD5
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulEchoIntResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002BC03 File Offset: 0x00029E03
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x04000794 RID: 1940
		private const RopId RopType = RopId.EchoInt;

		// Token: 0x04000795 RID: 1941
		private static EchoIntResultFactory resultFactory = new EchoIntResultFactory();

		// Token: 0x04000796 RID: 1942
		private int inParameter;
	}
}
