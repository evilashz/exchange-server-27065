using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200031F RID: 799
	internal sealed class RopReadStream : InputRop
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x000333FB File Offset: 0x000315FB
		internal override RopId RopId
		{
			get
			{
				return RopId.ReadStream;
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000333FF File Offset: 0x000315FF
		internal static Rop CreateRop()
		{
			return new RopReadStream();
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00033408 File Offset: 0x00031608
		internal void SetInput(byte logonIndex, byte handleTableIndex, ushort byteCountInput, uint? maximumByteCountInput)
		{
			if (byteCountInput != 47806 && maximumByteCountInput != null)
			{
				throw new ArgumentException("maximumByteCount must not have a value when byteCount is not 0xBABE");
			}
			if (byteCountInput == 47806 && maximumByteCountInput == null)
			{
				throw new ArgumentException("maximumByteCount must have a value when byteCount is 0xBABE");
			}
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.byteCount = byteCountInput;
			this.maximumByteCount = maximumByteCountInput;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00033464 File Offset: 0x00031664
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.byteCount);
			if (this.byteCount == 47806)
			{
				if (this.maximumByteCount == null)
				{
					throw new InvalidOperationException("ByteCount is set to 0xBABE, but there was no maximumByteCount");
				}
				writer.WriteUInt32(this.maximumByteCount.Value);
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000334BC File Offset: 0x000316BC
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			RopResult ropResult = RopResult.Parse(reader, new RopResult.ResultParserDelegate(ReadStreamResult.Parse), new RopResult.ResultParserDelegate(ReadStreamResult.Parse));
			if (this.result == null)
			{
				this.result = ropResult;
			}
			if (base.ChainedResults == null)
			{
				base.ChainedResults = new List<RopResult>(10);
			}
			base.ChainedResults.Add(ropResult);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00033520 File Offset: 0x00031720
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopReadStream.resultFactory;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00033528 File Offset: 0x00031728
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.byteCount = reader.ReadUInt16();
			if (this.byteCount == 47806)
			{
				this.maximumByteCount = new uint?(reader.ReadUInt32());
				if (this.maximumByteCount > 2147483647U)
				{
					throw new BufferParseException("Invalid maximum number of bytes to read from stream.");
				}
			}
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00033593 File Offset: 0x00031793
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000335A8 File Offset: 0x000317A8
		internal override byte[] CreateFakeRopRequest(RopResult result, ServerObjectHandleTable serverObjectHandleTable)
		{
			ReadStreamResult readStreamResult = result as ReadStreamResult;
			if (this.maximumByteCount != null && readStreamResult.ByteCount > 0 && this.maximumByteCount.Value > (uint)readStreamResult.ByteCount)
			{
				RopReadStream ropReadStream = (RopReadStream)RopReadStream.CreateRop();
				ropReadStream.SetInput(base.LogonIndex, base.HandleTableIndex, 47806, new uint?(this.maximumByteCount.Value - (uint)readStreamResult.ByteCount));
				return RopDriver.CreateFakeRopRequest(ropReadStream, serverObjectHandleTable);
			}
			return null;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00033628 File Offset: 0x00031828
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			bool flag = this.byteCount == 47806;
			int requestedByteCount = (int)(flag ? this.maximumByteCount.Value : ((uint)this.byteCount));
			ushort num = Rop.ComputeRemainingBufferSize(requestedByteCount, 2, outputBuffer.Count, flag);
			this.result = ropHandler.ReadStream(serverObject, num, RopReadStream.resultFactory);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00033680 File Offset: 0x00031880
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Bytes=").Append(this.byteCount);
			if (this.maximumByteCount != null)
			{
				stringBuilder.Append(" Maximum Bytes=").Append(this.maximumByteCount.Value);
			}
		}

		// Token: 0x04000A1D RID: 2589
		private const RopId RopType = RopId.ReadStream;

		// Token: 0x04000A1E RID: 2590
		public const int MaximumSizeRequestedValue = 47806;

		// Token: 0x04000A1F RID: 2591
		private static ReadStreamResultFactory resultFactory = new ReadStreamResultFactory();

		// Token: 0x04000A20 RID: 2592
		private ushort byteCount;

		// Token: 0x04000A21 RID: 2593
		private uint? maximumByteCount;
	}
}
