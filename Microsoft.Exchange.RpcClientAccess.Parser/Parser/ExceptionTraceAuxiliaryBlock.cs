using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000018 RID: 24
	internal sealed class ExceptionTraceAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003865 File Offset: 0x00001A65
		public ExceptionTraceAuxiliaryBlock(uint ropIndex, string exceptionTrace) : base(1, AuxiliaryBlockTypes.ExceptionTrace)
		{
			this.ropIndex = ropIndex;
			this.exceptionTrace = exceptionTrace;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003889 File Offset: 0x00001A89
		internal ExceptionTraceAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.ropIndex = reader.ReadUInt32();
			this.exceptionTrace = reader.ReadString8(this.traceEncoding, StringFlags.IncludeNull);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000038BC File Offset: 0x00001ABC
		public uint RopIndex
		{
			get
			{
				return this.ropIndex;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000038C4 File Offset: 0x00001AC4
		public string ExceptionTrace
		{
			get
			{
				return this.exceptionTrace;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000038CC File Offset: 0x00001ACC
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.ropIndex);
			writer.WriteString8(this.exceptionTrace, this.traceEncoding, StringFlags.IncludeNull);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000038F4 File Offset: 0x00001AF4
		protected override int Truncate(int maxSerializedSize, int currentSize)
		{
			byte[] bytes = this.traceEncoding.GetBytes(this.ExceptionTrace);
			if (currentSize > maxSerializedSize && currentSize - maxSerializedSize < bytes.Length)
			{
				this.exceptionTrace = this.traceEncoding.GetString(bytes, 0, maxSerializedSize - (currentSize - bytes.Length));
				return maxSerializedSize;
			}
			return currentSize;
		}

		// Token: 0x04000076 RID: 118
		private readonly Encoding traceEncoding = Encoding.UTF8;

		// Token: 0x04000077 RID: 119
		private readonly uint ropIndex;

		// Token: 0x04000078 RID: 120
		private string exceptionTrace;
	}
}
