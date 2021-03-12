using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002B7 RID: 695
	internal sealed class RopFastTransferSourceGetBufferExtended : RopFastTransferSourceGetBufferBase
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0002D181 File Offset: 0x0002B381
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferSourceGetBufferExtended;
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0002D188 File Offset: 0x0002B388
		internal static Rop CreateRop()
		{
			return new RopFastTransferSourceGetBufferExtended();
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002D18F File Offset: 0x0002B38F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			base.InternalParseOutput(reader, new RopFastTransferSourceGetBufferBase.ParseOutputDelegate(FastTransferSourceGetBufferExtendedResult.Parse), string8Encoding);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			int requestedByteCount = (int)(base.ReadMaximum ? this.maximumBufferSize.Value : this.bufferSize);
			ushort num = Rop.ComputeRemainingBufferSize(requestedByteCount, 13, outputBuffer.Count, base.ReadMaximum);
			ArraySegment<byte> outputBuffer2 = outputBuffer.SubSegment(FastTransferSourceGetBufferExtendedResult.FullHeaderSize, (int)num);
			FastTransferSourceGetBufferExtendedResultFactory resultFactory = new FastTransferSourceGetBufferExtendedResultFactory(outputBuffer2);
			this.result = ropHandler.FastTransferSourceGetBufferExtended(serverObject, num, resultFactory);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0002D212 File Offset: 0x0002B412
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return FastTransferSourceGetBufferExtendedResultFactory.Empty;
		}

		// Token: 0x040007EA RID: 2026
		private const RopId RopType = RopId.FastTransferSourceGetBufferExtended;
	}
}
