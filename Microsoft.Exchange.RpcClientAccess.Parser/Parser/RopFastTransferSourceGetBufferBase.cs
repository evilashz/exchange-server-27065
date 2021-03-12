using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002B4 RID: 692
	internal abstract class RopFastTransferSourceGetBufferBase : InputRop
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0002CEDB File Offset: 0x0002B0DB
		protected bool ReadMaximum
		{
			get
			{
				return this.bufferSize == 47806;
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		internal void SetInput(byte logonIndex, byte handleTableIndex, ushort bufferSize, ushort? maximumBufferSize)
		{
			if (bufferSize != 47806 && maximumBufferSize != null)
			{
				throw new ArgumentException("maximumBufferSize must not have a value when bufferSize is not 0xBABE");
			}
			if (bufferSize == 47806 && maximumBufferSize == null)
			{
				throw new ArgumentException("maximumBufferSize must have a value when bufferSize is 0xBABE");
			}
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.bufferSize = bufferSize;
			this.maximumBufferSize = maximumBufferSize;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0002CF48 File Offset: 0x0002B148
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.bufferSize);
			if (this.ReadMaximum)
			{
				if (this.maximumBufferSize == null)
				{
					throw new InvalidOperationException("BufferSize is set to 0xBABE, but there was no maximumBufferSize");
				}
				writer.WriteUInt16(this.maximumBufferSize.Value);
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0002CF9C File Offset: 0x0002B19C
		protected void InternalParseOutput(Reader reader, RopFastTransferSourceGetBufferBase.ParseOutputDelegate parseOutput, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			RopResult ropResult = parseOutput(reader);
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

		// Token: 0x06000F8A RID: 3978 RVA: 0x0002CFEC File Offset: 0x0002B1EC
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.bufferSize = reader.ReadUInt16();
			if (this.bufferSize == 47806)
			{
				this.maximumBufferSize = new ushort?(reader.ReadUInt16());
				return;
			}
			this.maximumBufferSize = null;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002D038 File Offset: 0x0002B238
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0002D050 File Offset: 0x0002B250
		internal override byte[] CreateFakeRopRequest(RopResult result, ServerObjectHandleTable serverObjectHandleTable)
		{
			FastTransferSourceGetBufferResultBase fastTransferSourceGetBufferResultBase = result as FastTransferSourceGetBufferResultBase;
			if ((fastTransferSourceGetBufferResultBase.State == FastTransferState.Partial || fastTransferSourceGetBufferResultBase.State == FastTransferState.NoRoom) && this.ReadMaximum)
			{
				return RopDriver.CreateFakeRopRequest(this, serverObjectHandleTable);
			}
			return null;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0002D088 File Offset: 0x0002B288
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" BufferSize=").Append(this.bufferSize);
			if (this.maximumBufferSize != null)
			{
				stringBuilder.Append(" MaximumBufferSize=").Append(this.maximumBufferSize.Value);
			}
		}

		// Token: 0x040007E6 RID: 2022
		public const ushort MaximumSizeRequestedValue = 47806;

		// Token: 0x040007E7 RID: 2023
		protected ushort bufferSize;

		// Token: 0x040007E8 RID: 2024
		protected ushort? maximumBufferSize;

		// Token: 0x020002B5 RID: 693
		// (Invoke) Token: 0x06000F90 RID: 3984
		protected delegate RopResult ParseOutputDelegate(Reader reader);
	}
}
