using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002AB RID: 683
	internal sealed class RopFastTransferDestinationPutBufferExtended : InputRop
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0002C4D4 File Offset: 0x0002A6D4
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferDestinationPutBufferExtended;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0002C4DB File Offset: 0x0002A6DB
		internal List<ArraySegment<byte>> DataChunks
		{
			get
			{
				return this.dataChunks;
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0002C4E3 File Offset: 0x0002A6E3
		internal static Rop CreateRop()
		{
			return new RopFastTransferDestinationPutBufferExtended();
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0002C4EA File Offset: 0x0002A6EA
		internal void SetInput(byte logonIndex, byte handleTableIndex, ArraySegment<byte>[] dataChunks)
		{
			Util.ThrowOnNullArgument(dataChunks, "dataChunks");
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.dataChunks = new List<ArraySegment<byte>>(dataChunks);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0002C50B File Offset: 0x0002A70B
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytesSegment(this.dataChunks[0], FieldLength.WordSize);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0002C528 File Offset: 0x0002A728
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = new FastTransferDestinationPutBufferExtendedResult(reader);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0002C53E File Offset: 0x0002A73E
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFastTransferDestinationPutBufferExtended.resultFactory;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0002C545 File Offset: 0x0002A745
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.dataChunks = new List<ArraySegment<byte>>(1);
			this.dataChunks.Add(reader.ReadSizeAndByteArraySegment());
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0002C56C File Offset: 0x0002A76C
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0002C584 File Offset: 0x0002A784
		internal override void MergeChainedData(Rop rop)
		{
			RopFastTransferDestinationPutBufferExtended ropFastTransferDestinationPutBufferExtended = rop as RopFastTransferDestinationPutBufferExtended;
			if (ropFastTransferDestinationPutBufferExtended == null)
			{
				throw new BufferParseException("Chained Rop not same type");
			}
			if (ropFastTransferDestinationPutBufferExtended.LogonIndex != base.LogonIndex)
			{
				throw new BufferParseException("Chained Rop not same logon index");
			}
			if (ropFastTransferDestinationPutBufferExtended.HandleTableIndex != base.HandleTableIndex)
			{
				throw new BufferParseException("Chained Rop not same SOHT index");
			}
			foreach (ArraySegment<byte> item in ropFastTransferDestinationPutBufferExtended.DataChunks)
			{
				this.dataChunks.Add(item);
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0002C738 File Offset: 0x0002A938
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

		// Token: 0x06000F49 RID: 3913 RVA: 0x0002C754 File Offset: 0x0002A954
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FastTransferDestinationPutBufferExtended(serverObject, this.dataChunks.ToArray(), RopFastTransferDestinationPutBufferExtended.resultFactory);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0002C774 File Offset: 0x0002A974
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

		// Token: 0x040007B5 RID: 1973
		private const RopId RopType = RopId.FastTransferDestinationPutBufferExtended;

		// Token: 0x040007B6 RID: 1974
		private static FastTransferDestinationPutBufferExtendedResultFactory resultFactory = new FastTransferDestinationPutBufferExtendedResultFactory();

		// Token: 0x040007B7 RID: 1975
		private List<ArraySegment<byte>> dataChunks;
	}
}
