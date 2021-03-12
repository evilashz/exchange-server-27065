using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002EA RID: 746
	internal sealed class RopImportMessageChangePartial : RopImportMessageChangeBase
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0002FF36 File Offset: 0x0002E136
		internal override RopId RopId
		{
			get
			{
				return RopId.ImportMessageChangePartial;
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0002FF3D File Offset: 0x0002E13D
		internal static Rop CreateRop()
		{
			return new RopImportMessageChangePartial();
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0002FF44 File Offset: 0x0002E144
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopImportMessageChangePartial.resultFactory;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0002FF4B File Offset: 0x0002E14B
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulImportMessageChangePartialResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0002FF79 File Offset: 0x0002E179
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ImportMessageChangePartial(serverObject, this.importMessageChangeFlags, this.propertyValues, RopImportMessageChangePartial.resultFactory);
		}

		// Token: 0x04000933 RID: 2355
		private const RopId RopType = RopId.ImportMessageChangePartial;

		// Token: 0x04000934 RID: 2356
		private static ImportMessageChangePartialResultFactory resultFactory = new ImportMessageChangePartialResultFactory();
	}
}
