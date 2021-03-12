using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000364 RID: 868
	internal sealed class RopWriteStreamExtended : InputRop
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00037071 File Offset: 0x00035271
		internal override RopId RopId
		{
			get
			{
				return RopId.WriteStreamExtended;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00037078 File Offset: 0x00035278
		internal List<ArraySegment<byte>> DataChunks
		{
			get
			{
				return this.dataChunks;
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00037080 File Offset: 0x00035280
		internal static Rop CreateRop()
		{
			return new RopWriteStreamExtended();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00037087 File Offset: 0x00035287
		internal void SetInput(byte logonIndex, byte handleTableIndex, ArraySegment<byte>[] dataChunks)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.dataChunks = new List<ArraySegment<byte>>(dataChunks);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0003709D File Offset: 0x0003529D
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytesSegment(this.dataChunks[0], FieldLength.WordSize);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x000370BA File Offset: 0x000352BA
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = WriteStreamExtendedResult.Parse(reader);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000370D0 File Offset: 0x000352D0
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopWriteStreamExtended.resultFactory;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000370D7 File Offset: 0x000352D7
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.dataChunks = new List<ArraySegment<byte>>(1);
			this.dataChunks.Add(reader.ReadSizeAndByteArraySegment());
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000370FE File Offset: 0x000352FE
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00037114 File Offset: 0x00035314
		internal override void MergeChainedData(Rop rop)
		{
			RopWriteStreamExtended ropWriteStreamExtended = rop as RopWriteStreamExtended;
			if (ropWriteStreamExtended == null)
			{
				throw new BufferParseException("Chained Rop not same type");
			}
			if (ropWriteStreamExtended.LogonIndex != base.LogonIndex)
			{
				throw new BufferParseException("Chained Rop not same logon index");
			}
			if (ropWriteStreamExtended.HandleTableIndex != base.HandleTableIndex)
			{
				throw new BufferParseException("Chained Rop not same SOHT index");
			}
			foreach (ArraySegment<byte> item in ropWriteStreamExtended.DataChunks)
			{
				this.dataChunks.Add(item);
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000372C8 File Offset: 0x000354C8
		internal override IEnumerator<Rop> SplitChainedData()
		{
			if (this.dataChunks.Count > 1)
			{
				for (int i = 1; i < this.dataChunks.Count; i++)
				{
					RopWriteStreamExtended rop = new RopWriteStreamExtended();
					rop.SetInput(base.LogonIndex, base.HandleTableIndex, new ArraySegment<byte>[]
					{
						this.dataChunks[i]
					});
					yield return rop;
				}
			}
			yield break;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000372E4 File Offset: 0x000354E4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			if ((long)outputBuffer.Count < RopWriteStreamExtended.resultFactory.SuccessfulResultMinimalSize)
			{
				throw new BufferTooSmallException();
			}
			this.result = ropHandler.WriteStreamExtended(serverObject, this.dataChunks.ToArray(), RopWriteStreamExtended.resultFactory);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00037320 File Offset: 0x00035520
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			foreach (ArraySegment<byte> arraySegment in this.dataChunks)
			{
				stringBuilder.Append(" Size=").Append(arraySegment.Count);
			}
		}

		// Token: 0x04000B25 RID: 2853
		private const RopId RopType = RopId.WriteStreamExtended;

		// Token: 0x04000B26 RID: 2854
		private static WriteStreamExtendedResultFactory resultFactory = new WriteStreamExtendedResultFactory();

		// Token: 0x04000B27 RID: 2855
		private List<ArraySegment<byte>> dataChunks;
	}
}
