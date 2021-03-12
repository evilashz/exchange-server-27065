using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200009A RID: 154
	internal abstract class Rop
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000D5A9 File Offset: 0x0000B7A9
		internal byte LogonIndex
		{
			get
			{
				return this.logonIndex;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000D5B1 File Offset: 0x0000B7B1
		internal byte HandleTableIndex
		{
			get
			{
				return this.handleTableIndex;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000D5B9 File Offset: 0x0000B7B9
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000D5C1 File Offset: 0x0000B7C1
		internal List<RopResult> ChainedResults
		{
			get
			{
				return this.chainedResults;
			}
			set
			{
				this.chainedResults = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D5CA File Offset: 0x0000B7CA
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000D5D2 File Offset: 0x0000B7D2
		internal int InputBufferSize
		{
			get
			{
				return this.inputBufferSize;
			}
			set
			{
				this.inputBufferSize = value;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public virtual void ParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			int num = (int)reader.Position;
			this.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.inputBufferSize = (int)reader.Position - num;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000D609 File Offset: 0x0000B809
		public void SerializeInput(Writer writer, Encoding string8Encoding)
		{
			this.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000D613 File Offset: 0x0000B813
		public void ParseOutput(Reader reader, Encoding string8Encoding)
		{
			this.InternalParseOutput(reader, string8Encoding);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000D61D File Offset: 0x0000B81D
		public void SerializeOutput(Writer writer)
		{
			this.InternalSerializeOutput(writer);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000D628 File Offset: 0x0000B828
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(this.RopId).Append(" [");
			this.AppendToString(stringBuilder);
			stringBuilder.Append("]");
			if (this.Result != null)
			{
				stringBuilder.Append(" Result [");
				this.Result.AppendToString(stringBuilder);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000D69E File Offset: 0x0000B89E
		internal static int ComputeResultHeaderSize(int specificRopHeaderSize)
		{
			return 6 + specificRopHeaderSize;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		protected static byte ReadHandleTableIndex(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			byte index = reader.ReadByte();
			serverObjectHandleTable.AccessIndex((int)index);
			return index;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		protected static ushort ComputeRemainingBufferSize(int requestedByteCount, ushort specificRopHeaderSize, int availableOutputBufferSize, bool readMaximum)
		{
			int num = requestedByteCount;
			int num2 = availableOutputBufferSize - Rop.ComputeResultHeaderSize((int)specificRopHeaderSize);
			if (num2 < 0)
			{
				throw new BufferTooSmallException();
			}
			if (readMaximum)
			{
				if (num > num2)
				{
					num = num2;
				}
			}
			else if (num > num2)
			{
				throw new BufferTooSmallException();
			}
			if (num > 65535 || num < 0)
			{
				string message = string.Format("Invalid byte count (must fit in the range of a ushort): {0}", num);
				throw new InvalidOperationException(message);
			}
			return (ushort)num;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000D71D File Offset: 0x0000B91D
		protected void SetCommonInput(byte logonIndex, byte handleTableIndex)
		{
			this.logonIndex = logonIndex;
			this.handleTableIndex = handleTableIndex;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003BB RID: 955
		internal abstract RopId RopId { get; }

		// Token: 0x060003BC RID: 956
		internal abstract void Execute(IConnectionInformation connection, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer);

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000D72D File Offset: 0x0000B92D
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000D735 File Offset: 0x0000B935
		internal RopResult Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x060003BF RID: 959
		protected abstract IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer);

		// Token: 0x060003C0 RID: 960 RVA: 0x0000D740 File Offset: 0x0000B940
		protected virtual void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			RopId ropId = (RopId)reader.ReadByte();
			if (ropId != this.RopId)
			{
				throw new BufferParseException(string.Format("RopId does not match.  Found {0} but expecting {1}", ropId, this.RopId));
			}
			this.logonIndex = reader.ReadByte();
			this.handleTableIndex = Rop.ReadHandleTableIndex(reader, serverObjectHandleTable);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000D797 File Offset: 0x0000B997
		protected virtual void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			writer.WriteByte((byte)this.RopId);
			writer.WriteByte(this.logonIndex);
			writer.WriteByte(this.handleTableIndex);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000D7BD File Offset: 0x0000B9BD
		protected virtual void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000D7BF File Offset: 0x0000B9BF
		protected virtual void InternalSerializeOutput(Writer writer)
		{
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000D7C1 File Offset: 0x0000B9C1
		internal virtual void AppendToString(StringBuilder stringBuilder)
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000D7C3 File Offset: 0x0000B9C3
		internal virtual byte[] CreateFakeRopRequest(RopResult result, ServerObjectHandleTable serverObjectHandleTable)
		{
			return null;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000D7C6 File Offset: 0x0000B9C6
		internal virtual void ResolveString8Values(Encoding string8Encoding)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		internal virtual void MergeChainedData(Rop rop)
		{
			throw new BufferParseException(string.Format("Not a chainable input rop: {0}", this.RopId));
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000D82C File Offset: 0x0000BA2C
		internal virtual IEnumerator<Rop> SplitChainedData()
		{
			yield break;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000D848 File Offset: 0x0000BA48
		internal virtual int? MinimumResponseSizeRequired
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400025A RID: 602
		private int inputBufferSize;

		// Token: 0x0400025B RID: 603
		private byte logonIndex;

		// Token: 0x0400025C RID: 604
		private byte handleTableIndex;

		// Token: 0x0400025D RID: 605
		private List<RopResult> chainedResults;

		// Token: 0x0400025E RID: 606
		protected RopResult result;

		// Token: 0x0200009B RID: 155
		// (Invoke) Token: 0x060003CC RID: 972
		internal delegate Rop CreateRopDelegate();
	}
}
