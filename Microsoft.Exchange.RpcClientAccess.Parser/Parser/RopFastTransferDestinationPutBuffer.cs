using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002AA RID: 682
	internal sealed class RopFastTransferDestinationPutBuffer : InputRop
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferDestinationPutBuffer;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0002C1B0 File Offset: 0x0002A3B0
		internal List<ArraySegment<byte>> DataChunks
		{
			get
			{
				return this.dataChunks;
			}
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
		internal static Rop CreateRop()
		{
			return new RopFastTransferDestinationPutBuffer();
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002C1BF File Offset: 0x0002A3BF
		internal void SetInput(byte logonIndex, byte handleTableIndex, ArraySegment<byte>[] dataChunks)
		{
			Util.ThrowOnNullArgument(dataChunks, "dataChunks");
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.dataChunks = new List<ArraySegment<byte>>(dataChunks);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytesSegment(this.dataChunks[0], FieldLength.WordSize);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0002C1FD File Offset: 0x0002A3FD
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = new FastTransferDestinationPutBufferResult(reader);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002C213 File Offset: 0x0002A413
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFastTransferDestinationPutBuffer.resultFactory;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002C21A File Offset: 0x0002A41A
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.dataChunks = new List<ArraySegment<byte>>(1);
			this.dataChunks.Add(reader.ReadSizeAndByteArraySegment());
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002C241 File Offset: 0x0002A441
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0002C258 File Offset: 0x0002A458
		internal override void MergeChainedData(Rop rop)
		{
			RopFastTransferDestinationPutBuffer ropFastTransferDestinationPutBuffer = rop as RopFastTransferDestinationPutBuffer;
			if (ropFastTransferDestinationPutBuffer == null)
			{
				throw new BufferParseException("Chained Rop not same type");
			}
			if (ropFastTransferDestinationPutBuffer.LogonIndex != base.LogonIndex)
			{
				throw new BufferParseException("Chained Rop not same logon index");
			}
			if (ropFastTransferDestinationPutBuffer.HandleTableIndex != base.HandleTableIndex)
			{
				throw new BufferParseException("Chained Rop not same SOHT index");
			}
			foreach (ArraySegment<byte> item in ropFastTransferDestinationPutBuffer.DataChunks)
			{
				this.dataChunks.Add(item);
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0002C40C File Offset: 0x0002A60C
		internal override IEnumerator<Rop> SplitChainedData()
		{
			if (this.dataChunks.Count > 1)
			{
				for (int i = 1; i < this.dataChunks.Count; i++)
				{
					RopFastTransferDestinationPutBuffer rop = new RopFastTransferDestinationPutBuffer();
					rop.SetInput(base.LogonIndex, base.HandleTableIndex, new ArraySegment<byte>[]
					{
						this.dataChunks[i]
					});
					yield return rop;
				}
			}
			yield break;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0002C428 File Offset: 0x0002A628
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FastTransferDestinationPutBuffer(serverObject, this.dataChunks.ToArray(), RopFastTransferDestinationPutBuffer.resultFactory);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002C448 File Offset: 0x0002A648
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			foreach (ArraySegment<byte> arraySegment in this.dataChunks)
			{
				stringBuilder.Append(" data=[");
				Util.AppendToString<byte>(stringBuilder, arraySegment);
				stringBuilder.Append("]");
			}
		}

		// Token: 0x040007B2 RID: 1970
		private const RopId RopType = RopId.FastTransferDestinationPutBuffer;

		// Token: 0x040007B3 RID: 1971
		private static FastTransferDestinationPutBufferResultFactory resultFactory = new FastTransferDestinationPutBufferResultFactory();

		// Token: 0x040007B4 RID: 1972
		private List<ArraySegment<byte>> dataChunks;
	}
}
