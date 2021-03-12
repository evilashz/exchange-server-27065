using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000286 RID: 646
	internal sealed class RopCloneStream : InputOutputRop
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0002A3C3 File Offset: 0x000285C3
		internal override RopId RopId
		{
			get
			{
				return RopId.CloneStream;
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0002A3C7 File Offset: 0x000285C7
		internal static Rop CreateRop()
		{
			return new RopCloneStream();
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0002A3CE File Offset: 0x000285CE
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0002A3D9 File Offset: 0x000285D9
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulCloneStreamResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0002A407 File Offset: 0x00028607
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopCloneStream.resultFactory;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0002A40E File Offset: 0x0002860E
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0002A423 File Offset: 0x00028623
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.CloneStream(serverObject, RopCloneStream.resultFactory);
		}

		// Token: 0x0400073B RID: 1851
		private const RopId RopType = RopId.CloneStream;

		// Token: 0x0400073C RID: 1852
		private static CloneStreamResultFactory resultFactory = new CloneStreamResultFactory();
	}
}
