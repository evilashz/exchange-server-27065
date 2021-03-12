using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000280 RID: 640
	internal sealed class ReadStreamResult : RopResult
	{
		// Token: 0x06000DDA RID: 3546 RVA: 0x00029D9C File Offset: 0x00027F9C
		internal ReadStreamResult(ErrorCode errorCode, ArraySegment<byte> dataSegment) : base(RopId.ReadStream, errorCode, null)
		{
			if (dataSegment.Count > 65535)
			{
				throw new ArgumentException("Buffers larger than ushort.MaxValue are not supported", "dataSegment");
			}
			if (errorCode != ErrorCode.None && dataSegment.Count != 0)
			{
				throw new ArgumentException("Cannot use a non-empty buffer with an error", "dataSegment");
			}
			this.dataSegment = dataSegment;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00029DF4 File Offset: 0x00027FF4
		internal ReadStreamResult(Reader reader) : base(reader)
		{
			ushort count = reader.ReadUInt16();
			this.dataSegment = reader.ReadArraySegment((uint)count);
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00029E1C File Offset: 0x0002801C
		public ushort ByteCount
		{
			get
			{
				return (ushort)this.dataSegment.Count;
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00029E38 File Offset: 0x00028038
		public byte[] GetData()
		{
			byte[] array = new byte[this.dataSegment.Count];
			Array.Copy(this.dataSegment.Array, this.dataSegment.Offset, array, 0, this.dataSegment.Count);
			return array;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00029E8C File Offset: 0x0002808C
		public ushort GetData(byte[] dest, uint offset)
		{
			Buffer.BlockCopy(this.dataSegment.Array, this.dataSegment.Offset, dest, (int)offset, this.dataSegment.Count);
			return (ushort)this.dataSegment.Count;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00029ED9 File Offset: 0x000280D9
		internal static RopResult Parse(Reader reader)
		{
			return new ReadStreamResult(reader);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00029EE4 File Offset: 0x000280E4
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16((ushort)this.dataSegment.Count);
			writer.WriteBytesSegment(this.dataSegment);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00029F1C File Offset: 0x0002811C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" BytesRead=0x").Append(this.dataSegment.Count.ToString("X"));
		}

		// Token: 0x0400072E RID: 1838
		internal const int SpecificRopHeaderSize = 2;

		// Token: 0x0400072F RID: 1839
		private readonly ArraySegment<byte> dataSegment;
	}
}
