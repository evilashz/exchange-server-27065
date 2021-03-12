using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002B6 RID: 694
	internal sealed class RopFastTransferSourceGetBuffer : RopFastTransferSourceGetBufferBase
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0002D0E4 File Offset: 0x0002B2E4
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferSourceGetBuffer;
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0002D0E8 File Offset: 0x0002B2E8
		internal static Rop CreateRop()
		{
			return new RopFastTransferSourceGetBuffer();
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002D0EF File Offset: 0x0002B2EF
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			base.InternalParseOutput(reader, new RopFastTransferSourceGetBufferBase.ParseOutputDelegate(FastTransferSourceGetBufferResult.Parse), string8Encoding);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0002D110 File Offset: 0x0002B310
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			int requestedByteCount = (int)(base.ReadMaximum ? this.maximumBufferSize.Value : this.bufferSize);
			ushort num = Rop.ComputeRemainingBufferSize(requestedByteCount, 9, outputBuffer.Count, base.ReadMaximum);
			ArraySegment<byte> outputBuffer2 = outputBuffer.SubSegment(FastTransferSourceGetBufferResult.FullHeaderSize, (int)num);
			FastTransferSourceGetBufferResultFactory resultFactory = new FastTransferSourceGetBufferResultFactory(outputBuffer2);
			this.result = ropHandler.FastTransferSourceGetBuffer(serverObject, num, resultFactory);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0002D172 File Offset: 0x0002B372
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return FastTransferSourceGetBufferResultFactory.Empty;
		}

		// Token: 0x040007E9 RID: 2025
		private const RopId RopType = RopId.FastTransferSourceGetBuffer;
	}
}
