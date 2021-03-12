using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004B RID: 75
	internal sealed class ExtendedBufferHeader
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x0000735E File Offset: 0x0000555E
		public ExtendedBufferHeader(ExtendedBufferFlag flags, ushort payloadSize, ushort uncompressedSize)
		{
			this.flags = flags;
			this.payloadSize = payloadSize;
			this.uncompressedSize = uncompressedSize;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000737B File Offset: 0x0000557B
		internal ExtendedBufferHeader(Reader reader)
		{
			if (reader.ReadUInt16() != 0)
			{
				throw new BufferParseException("Extended buffer header version not correct");
			}
			this.flags = (ExtendedBufferFlag)reader.ReadUInt16();
			this.payloadSize = reader.ReadUInt16();
			this.uncompressedSize = reader.ReadUInt16();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000073BA File Offset: 0x000055BA
		internal ExtendedBufferFlag Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000073C2 File Offset: 0x000055C2
		internal ushort PayloadSize
		{
			get
			{
				return this.payloadSize;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000073CA File Offset: 0x000055CA
		internal ushort UncompressedSize
		{
			get
			{
				return this.uncompressedSize;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000073D2 File Offset: 0x000055D2
		internal bool IsCompressed
		{
			get
			{
				return (ushort)(this.flags & ExtendedBufferFlag.Compressed) != 0;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000073E3 File Offset: 0x000055E3
		internal bool IsObfuscated
		{
			get
			{
				return (ushort)(this.flags & ExtendedBufferFlag.Obfuscated) != 0;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000073F4 File Offset: 0x000055F4
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00007405 File Offset: 0x00005605
		internal bool IsLast
		{
			get
			{
				return (ushort)(this.flags & ExtendedBufferFlag.Last) != 0;
			}
			set
			{
				if (value)
				{
					this.flags |= ExtendedBufferFlag.Last;
					return;
				}
				this.flags &= ~ExtendedBufferFlag.Last;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000742D File Offset: 0x0000562D
		internal void Serialize(Writer writer)
		{
			writer.WriteUInt16(0);
			writer.WriteUInt16((ushort)this.flags);
			writer.WriteUInt16(this.payloadSize);
			writer.WriteUInt16(this.uncompressedSize);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000745C File Offset: 0x0000565C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007480 File Offset: 0x00005680
		internal void AppendToString(StringBuilder stringBuilder)
		{
			stringBuilder.Append("[ExtendedBufferHeader ");
			stringBuilder.Append(" Flags=[").Append(this.flags).Append("]");
			stringBuilder.Append(" PayloadSize=[").Append(this.payloadSize).Append("]");
			stringBuilder.Append(" UncompressedSize=[").Append(this.uncompressedSize).Append("]");
			stringBuilder.Append("]");
		}

		// Token: 0x040000E7 RID: 231
		public const int Size = 8;

		// Token: 0x040000E8 RID: 232
		private readonly ushort payloadSize;

		// Token: 0x040000E9 RID: 233
		private readonly ushort uncompressedSize;

		// Token: 0x040000EA RID: 234
		private ExtendedBufferFlag flags;
	}
}
